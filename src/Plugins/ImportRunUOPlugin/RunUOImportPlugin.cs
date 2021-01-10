using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using GumpStudio.Elements;
using GumpStudio.Enums;
using GumpStudio.Forms;
using GumpStudio.Plugins;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Ultima;
using HtmlElement = GumpStudio.Elements.HtmlElement;

namespace ImportRunUOPlugin
{
    public class RunUoImportPlugin : BasePlugin
    {
        private readonly PluginInfo _mInfo;
        private DesignerForm _mDesigner;
        //private readonly Regex ex;
        //private readonly Hashtable m_PageLookup;
        private readonly Dictionary<string, string> _paramLookup;

        public RunUoImportPlugin()
        {
            _mInfo = new PluginInfo
            {
                AuthorEmail = "buffner@tkpups.com",
                AuthorName = "Bradley Uffner",
                Description = "Imports a RunUO Gump script into a Gump studio file.",
                Version =
                    $"{Assembly.GetExecutingAssembly().GetName().Version.Major}.{Assembly.GetExecutingAssembly().GetName().Version.Minor}",
                PluginName = Name
            };

            //this.ex = new Regex("((?<field>[^\",\\r\\n]+)|\"(?<field>([^\"]|\"\")+)\")(,|(?<rowbreak>\\r\\n|\\n|$))");
            // m_PageLookup = new Hashtable
            // {
            //     { 0, 0 }
            // };

            _paramLookup = new Dictionary<string, string>();
        }

        public override PluginInfo GetPluginInfo()
        {
            return _mInfo;
        }

        public sealed override string Name => "RunUO Import";

        public override void Load(DesignerForm frmDesigner)
        {
            _mDesigner = frmDesigner;
            _mDesigner.MnuFileImport.Enabled = true;
            _mDesigner.MnuFileImport.MenuItems.Add(new MenuItem("RunUO Script", DoImportMenu));
        }

        private void DoImportMenu(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "C# (*.cs)|*.cs",
                CheckFileExists = true,
                DefaultExt = ".cs"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _mDesigner.ClearGump();
                BreakFile(openFileDialog.FileName);
            }
            openFileDialog.Dispose();
        }

        private void BreakFile(string fileName)
        {
            ArrayList lines = new ArrayList();

            using (StreamReader streamReader = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                while (!streamReader.EndOfStream)
                {
                    lines.Add(streamReader.ReadLine());
                }
            }

            string extension = Path.GetExtension(fileName);
            if (extension == ".cs")
            {
                ParseCSharp(lines);
            }
            else
            {
                MessageBox.Show("Unsupported Language");
            }
        }

        private void ParseCSharp(ArrayList lines)
        {
            long lineCounter = 0;

            foreach (object line in lines)
            {
                string str1 = Convert.ToString(line);
                ++lineCounter;

                if (!string.Equals(str1.Substring(0, 2), "//", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                BaseElement element = null;

                if (str1.Contains("AddBackground("))
                {
                    string str2 = Strings.Mid(str1, Strings.InStr(str1, "AddBackground(") + 14);

                    ArrayList arrayList = CParse(Strings.Left(str2, str2.Length - 2));

                    if (IsNumber((string)arrayList[0]) &&
                        IsNumber((string)arrayList[1]) &&
                        IsNumber((string)arrayList[2]) &&
                        IsNumber((string)arrayList[3]) &&
                        IsNumber((string)arrayList[4]))
                    {
                        //FrmInterpret frmInterpret = new FrmInterpret(); // TODO: ??

                        int index = 0;
                        do
                        {
                            arrayList[index] = Interpret((string)arrayList[index], lineCounter, lines);
                            ++index;
                        }
                        while (index <= 4);

                        element = new BackgroundElement
                        {
                            X = (int) arrayList[0],
                            Y = (int) arrayList[1],
                            Width = (int) arrayList[2],
                            Height = (int) arrayList[3],
                            GumpId = (int) arrayList[4]
                        };
                    }
                }

                if (str1.Contains("AddPage("))
                {
                    string str2 = Strings.Mid(str1, Strings.InStr(str1, "AddPage(") + 8);
                    ArrayList arrayList = CParse(Strings.Left(str2, str2.Length - 2));
                    arrayList[0] = Interpret((string)arrayList[0], lineCounter, lines);
                    // if ((int)arrayList[0] != 0)
                    // {
                    //     m_PageLookup.Add(m_MDesigner.AddPage(), (int)arrayList[0]);
                    // }
                }

                if (str1.Contains("AddHtml("))
                {
                    string str2 = Strings.Mid(str1, Strings.InStr(str1, "AddHtml(") + 8);
                    ArrayList arrayList = CParse(Strings.Left(str2, str2.Length - 2));
                    arrayList[0] = Interpret((string)arrayList[0], lineCounter, lines);

                    int index = 0;
                    do
                    {
                        arrayList[index] = Interpret((string)arrayList[index], lineCounter, lines);
                        ++index;
                    }
                    while (index <= 3);

                    element = new HtmlElement
                    {
                        X = (int)arrayList[0],
                        Y = (int)arrayList[1],
                        Width = (int)arrayList[2],
                        Height = (int)arrayList[3],
                        Html = (string)arrayList[4],
                        ShowBackground = bool.Parse((string)arrayList[5]),
                        ShowScrollbar = bool.Parse((string)arrayList[6]),
                        TextType = HtmlElementType.Html
                    };
                }

                if (str1.Contains("AddHtmlLocalized("))
                {
                    string str2 = Strings.Mid(str1, Strings.InStr(str1, "AddHtmlLocalized(") + "AddHtmlLocalized(".Length);
                    ArrayList arrayList = CParse(Strings.Left(str2, str2.Length - 2));
                    HtmlElement htmlElement = new HtmlElement
                    {
                        X = int.Parse(Interpret((string)arrayList[0], lineCounter, lines)),
                        Y = int.Parse(Interpret((string)arrayList[1], lineCounter, lines)),
                        Width = int.Parse(Interpret((string)arrayList[2], lineCounter, lines)),
                        Height = int.Parse(Interpret((string)arrayList[3], lineCounter, lines)),
                        TextType = HtmlElementType.Localized,
                        Html = "Localized message #" + int.Parse(Interpret((string)arrayList[4], lineCounter, lines))
                    };
                    switch (arrayList.Count)
                    {
                        case 7:
                            htmlElement.CliLocId = int.Parse(Interpret((string)arrayList[4], lineCounter, lines));
                            htmlElement.ShowBackground = bool.Parse((string)arrayList[5]);
                            htmlElement.ShowScrollbar = bool.Parse((string)arrayList[6]);
                            break;
                        case 8:
                            htmlElement.CliLocId = int.Parse(Interpret((string)arrayList[4], lineCounter, lines));
                            htmlElement.ShowBackground = bool.Parse((string)arrayList[6]);
                            htmlElement.ShowScrollbar = bool.Parse((string)arrayList[7]);
                            break;
                    }
                    element = htmlElement;
                }

                if (str1.Contains("AddButton("))
                {
                    string str2 = Strings.Mid(str1, Strings.InStr(str1, "AddButton(") + 10);
                    ArrayList arrayList = CParse(Strings.Left(str2, str2.Length - 2));
                    ButtonElement buttonElement = new ButtonElement
                    {
                        X = int.Parse(Interpret((string)arrayList[0], lineCounter, lines)),
                        Y = int.Parse(Interpret((string)arrayList[1], lineCounter, lines)),
                        NormalId = int.Parse(Interpret((string)arrayList[2], lineCounter, lines)),
                        PressedId = int.Parse(Interpret((string)arrayList[3], lineCounter, lines))
                    };
                    //ButtonTypeEnum buttonTypeEnum = ButtonTypeEnum.Reply;
                    int num = int.Parse(Interpret((string)arrayList[6], lineCounter, lines));
                    buttonElement.Param = num;
                    if (Strings.InStr((string)arrayList[5], "Page") > 0)
                    {
                        //buttonTypeEnum = ButtonTypeEnum.Page;
                    }

                    element = buttonElement;
                }

                if (str1.Contains("AddLabel("))
                {
                    string str2 = Strings.Mid(str1, Strings.InStr(str1, "AddLabel(") + 9);
                    ArrayList arrayList = CParse(Strings.Left(str2, str2.Length - 2));
                    element = new LabelElement
                    {
                        X = int.Parse(Interpret((string)arrayList[0], lineCounter, lines)),
                        Y = int.Parse(Interpret((string)arrayList[1], lineCounter, lines)),
                        Hue = Hues.GetHue(int.Parse(Interpret((string)arrayList[2], lineCounter, lines))),
                        Text = (string)arrayList[3]
                    };
                }

                if (str1.Contains("AddImage("))
                {
                    string str2 = Strings.Mid(str1, Strings.InStr(str1, "AddImage(") + 9);
                    ArrayList arrayList = CParse(Strings.Left(str2, str2.Length - 2));
                    ImageElement imageElement = new ImageElement();
                    if (arrayList.Count == 3)
                    {
                        imageElement.X = int.Parse(Interpret((string)arrayList[0], lineCounter, lines));
                        imageElement.Y = int.Parse(Interpret((string)arrayList[1], lineCounter, lines));
                        imageElement.GumpId = int.Parse(Interpret((string)arrayList[2], lineCounter, lines));
                    }
                    else
                    {
                        imageElement.X = int.Parse(Interpret((string)arrayList[0], lineCounter, lines));
                        imageElement.Y = int.Parse(Interpret((string)arrayList[1], lineCounter, lines));
                        imageElement.GumpId = int.Parse(Interpret((string)arrayList[2], lineCounter, lines));
                        imageElement.Hue = Hues.GetHue(int.Parse(Interpret((string)arrayList[3], lineCounter, lines)));
                    }
                    element = imageElement;
                }

                if (str1.Contains("AddTextEntry("))
                {
                    string str2 = Strings.Mid(str1, Strings.InStr(str1, "AddTextEntry(") + 13);
                    ArrayList arrayList = CParse(Strings.Left(str2, str2.Length - 2));
                    element = new TextEntryElement
                    {
                        X = int.Parse(Interpret((string)arrayList[0], lineCounter, lines)),
                        Y = int.Parse(Interpret((string)arrayList[1], lineCounter, lines)),
                        Width = int.Parse(Interpret((string)arrayList[2], lineCounter, lines)),
                        Height = int.Parse(Interpret((string)arrayList[3], lineCounter, lines)),
                        Hue = Hues.GetHue(int.Parse(Interpret((string)arrayList[5], lineCounter, lines))),
                        Id = int.Parse(Interpret((string)arrayList[6], lineCounter, lines)),
                        InitialText = (string)arrayList[7]
                    };
                }

                if (element != null)
                {
                    _mDesigner.AddElement(element);
                }
            }
        }

        private static ArrayList CParse(string text)
        {
            bool flag = false;
            int num1 = 0;
            string sLeft1 = "";
            ArrayList arrayList1 = new ArrayList();
            string str1 = "";
            int num2 = text.Length;
            int start = 1;
            while (start <= num2)
            {
                string sLeft2 = Strings.Mid(text, start, 1);
                if (StringType.StrCmp(sLeft2, "\"", false) == 0 && StringType.StrCmp(sLeft1, "\\", false) != 0)
                {
                    flag = !flag;
                }

                if (StringType.StrCmp(sLeft2, "(", false) == 0 && !flag)
                {
                    ++num1;
                }

                if (StringType.StrCmp(sLeft2, ")", false) == 0 && !flag)
                {
                    --num1;
                    if (num1 < 0)
                    {
                        break;
                    }
                }
                if (StringType.StrCmp(sLeft2, ",", false) == 0 && !flag && num1 == 0)
                {
                    arrayList1.Add(Strings.Trim(str1));
                    str1 = "";
                }
                else
                {
                    str1 += sLeft2;
                }

                sLeft1 = sLeft2;
                ++start;
            }
            if (StringType.StrCmp(str1, "", false) != 0)
            {
                arrayList1.Add(Strings.Trim(str1));
            }

            ArrayList arrayList2 = new ArrayList();
            IEnumerator enumerator = null;
            try
            {
                enumerator = arrayList1.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    object objectValue = RuntimeHelpers.GetObjectValue(enumerator.Current);
                    string str2 = (string)objectValue;
                    if (StringType.StrCmp(Strings.Left(str2, 1), "\"", false) == 0 && StringType.StrCmp(Strings.Right(str2, 1), "\"", false) == 0)
                    {
                        arrayList2.Add(Strings.Mid(str2, 2, str2.Length - 2));
                    }
                    else
                    {
                        arrayList2.Add(RuntimeHelpers.GetObjectValue(objectValue));
                    }
                }
            }
            finally
            {
                if (enumerator is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            return arrayList2;
        }

        private string Interpret(string text, long line, ArrayList lines)
        {
            if (StringType.StrCmp(Strings.Left(text, 2), "0x", false) == 0)
            {
                text = HexToDec(text).ToString();
            }

            if (IsNumber(text))
            {
                return text;
            }

            string str = text;
            if (_paramLookup.ContainsKey(text))
            {
                return _paramLookup[text];
            }

            text = new FrmInterpret().Interpret(text, line, lines);

            _paramLookup.Add(str, text);

            return text;
        }

        private bool IsNumber(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(char.IsDigit);
        }

        private static int HexToDec(string value)
        {
            if (value.Length <= 2 || value.Length > 6 || !string.Equals(value.Substring(0, 2), "0X", StringComparison.OrdinalIgnoreCase))
            {
                return 0;
            }

            return Convert.ToInt32(value, 16);

            //if (StringType.StrCmp(Strings.Left(hex, 2), "0x", false) == 0)
            //{
            //    hex = Strings.Right(hex, hex.Length - 2);
            //}

            //int num = 0;
            //int start = hex.Length;
            //while (start >= 1)
            //{
            //    num += (int)Math.Round(Strings.InStr("0123456789ABCDEF", Strings.Mid(hex, start, 1)) - (1 * Math.Pow(16.0, hex.Length - start)));
            //    start += -1;
            //}
            //return num;
        }
    }
}
