using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GumpStudio.Elements;
using GumpStudio.Enums;
using GumpStudio.Forms;
using GumpStudio.Plugins;
using HtmlElement = GumpStudio.Elements.HtmlElement;

namespace POLGumpExport
{
    public class PolExporter : BasePlugin
    {
        public override string Name => GetPluginInfo().PluginName;

        public override PluginInfo GetPluginInfo()
        {
            return new PluginInfo
            {
                AuthorEmail = "rozenblit@gmail.com",
                AuthorName = "Fernando Rozenblit",
                Description = "Exports the Gump into a POL script. Based on Sphere Exporter by Francesco Furiani & Mark Chandler.",
                PluginName = "POLGumpExporter",
                Version = "1.1"
            };
        }

        public override void Load(DesignerForm frmDesigner)
        {
            Designer = frmDesigner;

            MnuFileExportPolExport = new MenuItem("POL");
            MnuFileExportPolExport.Click += FileExportPOLExport_Click;

            if (!Designer.MnuFileExport.Enabled)
            {
                Designer.MnuFileExport.Enabled = true;
            }

            Designer.MnuFileExport.MenuItems.Add(MnuFileExportPolExport);

            base.Load(frmDesigner);
        }

        private void FileExportPOLExport_Click(object sender, EventArgs e)
        {
            FrmPolExportForm = new PolExportForm(this);
            FrmPolExportForm.ShowDialog();
        }

        //public StringWriter GetPolScript(bool useDistro)
        //{
        //    return GetPolScript(useDistro, true, true, true);
        //}

        public StringWriter GetPolScript(bool bUseDistro, bool bShowComment, bool bShowNames, bool bDefaultTexts)
        {
            return bUseDistro
                ? CreateDistroScript(bShowComment, bShowNames, bDefaultTexts)
                : CreateBareScript();
        }

        private static string GetCommentString(BaseElement beElem, bool bShowComments, bool bShowNames)
        {
            if (beElem == null)
            {
                return "";
            }

            string text = "";
            string text2 = "";

            if (beElem.Comment != null)
            {
                text = beElem.Comment;
            }

            if (bShowNames)
            {
                // TODO: wtf?
                //if (beElem.Name != null)
                //{
                //    string name = beElem.Name;
                //}

                text2 = beElem.Name + (text != string.Empty && bShowComments ? ": " : "");
            }

            if (bShowComments)
            {
                text2 += text;
            }

            return "//" + text2;
        }

        private string GetGumpName()
        {
            const string result = "gump";

            string gumpName = FrmPolExportForm.GumpName;
            string[] array = null;

            if (!string.IsNullOrEmpty(gumpName))
            {
                array = gumpName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (array == null)
            {
                return result;
            }

            return array.Length > 0 ? array[0] : result;
        }

        private StringWriter CreateDistroScript(bool bShowComment, bool bShowNames, bool bDefaultTexts)
        {
            var stringWriter = new StringWriter();
            var list = new List<string>();

            _bGetDefaultText = bDefaultTexts;

            string gumpName = GetGumpName();

            list.Add(DistroGump_GFCreateGump(gumpName, Designer.GumpProperties.Location));
            list.Add("");

            if (!Designer.GumpProperties.Moveable)
            {
                list.Add($"GFMovable({gumpName}, 0);");
            }

            if (!Designer.GumpProperties.Closeable)
            {
                list.Add($"GFClosable({gumpName}, 0);");
            }

            if (!Designer.GumpProperties.Disposable)
            {
                list.Add($"GFDisposable({gumpName}, 0);");
            }

            if (Designer.Stacks.Count > 0)
            {
                int num = -1;
                int num2 = 0;

                foreach (var groupElement in Designer.Stacks)
                {
                    if (num2 > 0)
                    {
                        list.Add("");
                    }

                    list.Add(DistroGump_GFPage(gumpName, ref num2));

                    if (groupElement == null)
                    {
                        continue;
                    }

                    foreach (var baseElement in groupElement.GetElementsRecursive().Where(baseElement => baseElement != null))
                    {
                        if (bShowComment || bShowNames)
                        {
                            string commentString = GetCommentString(baseElement, bShowComment, bShowNames);
                            if (commentString != string.Empty)
                            {
                                list.Add("");
                                list.Add(commentString);
                            }
                        }

                        var type = baseElement.GetType();

                        if (type == typeof(HtmlElement))
                        {
                            var htmlelement = baseElement as HtmlElement;
                            list.Add(htmlelement.TextType == HtmlElementType.Html
                                ? DistroGump_GFHTMLArea(gumpName, htmlelement)
                                : DistroGump_GFAddHTMLLocalized(gumpName, htmlelement));
                        }
                        else if (type == typeof(TextEntryElement))
                        {
                            var elem = baseElement as TextEntryElement;
                            list.Add(DistroGump_GFTextEntry(gumpName, elem));
                        }
                        else if (type == typeof(LabelElement))
                        {
                            var elem2 = baseElement as LabelElement;
                            list.Add(DistroGump_GFTextLine(gumpName, elem2));
                        }
                        else if (type == typeof(AlphaElement))
                        {
                            var elem3 = baseElement as AlphaElement;
                            list.Add(DistroGump_GFAddAlphaRegion(gumpName, elem3));
                        }
                        else if (type == typeof(BackgroundElement))
                        {
                            var elem4 = baseElement as BackgroundElement;
                            list.Add(DistroGump_GFResizePic(gumpName, elem4));
                        }
                        else if (type == typeof(ImageElement))
                        {
                            var elem5 = baseElement as ImageElement;
                            list.Add(DistroGump_GFGumpPic(gumpName, elem5));
                        }
                        else if (type == typeof(ItemElement))
                        {
                            var elem6 = baseElement as ItemElement;
                            list.Add(DistroGump_GFTilePic(gumpName, elem6));
                        }
                        else if (type == typeof(TiledElement))
                        {
                            var elem7 = baseElement as TiledElement;
                            list.Add("");
                            list.Add("//Gump package does not support GumpPicTiled");
                            list.Add("//" + Gump_WriteGumpPicTiled(elem7));
                            list.Add("");
                        }
                        else if (type == typeof(ButtonElement))
                        {
                            var elem8 = baseElement as ButtonElement;
                            list.Add(DistroGump_GFAddButton(gumpName, elem8));
                        }
                        else if (type == typeof(CheckboxElement))
                        {
                            var elem9 = baseElement as CheckboxElement;
                            list.Add(DistroGump_GFCheckBox(gumpName, elem9));
                        }
                        else if (type == typeof(RadioElement))
                        {
                            var radioElement = baseElement as RadioElement;
                            if (radioElement.Group != num)
                            {
                                list.Add($"GFSetRadioGroup({gumpName}, {radioElement.Group});");
                                num = radioElement.Group;
                            }
                            list.Add(DistroGump_GFRadioButton(gumpName, radioElement));
                        }
                    }
                }
            }

            stringWriter.WriteLine("// Created {0}, with Gump Studio.", DateTime.Now);
            stringWriter.WriteLine("// Exported with {0} ver {1} for gump pkg", GetPluginInfo().PluginName, GetPluginInfo().Version);
            stringWriter.WriteLine();
            stringWriter.WriteLine("use uo;");
            stringWriter.WriteLine("use os;");
            stringWriter.WriteLine();
            stringWriter.WriteLine("include \":gumps:gumps\";");
            stringWriter.WriteLine();
            stringWriter.WriteLine("program gump_{0}(who)", gumpName);
            stringWriter.WriteLine();

            foreach (string str in list)
            {
                stringWriter.WriteLine("\t" + str);
            }

            stringWriter.WriteLine();
            stringWriter.WriteLine("\tGFSendGump(who, {0});", gumpName);
            stringWriter.WriteLine();
            stringWriter.WriteLine("endprogram");

            return stringWriter;
        }

        private static string DistroGump_GFRadioButton(string gumpName, RadioElement elem)
        {
            return $"GFRadioButton({gumpName}, {elem.X}, {elem.Y}, {elem.UnCheckedId}, {elem.CheckedId}, {BoolToString(elem.Checked)}, {elem.Value});";
        }

        private static string DistroGump_GFCheckBox(string gumpName, CheckboxElement elem)
        {
            return $"GFCheckBox({gumpName}, {elem.X}, {elem.Y}, {elem.UnCheckedId}, {elem.CheckedId}, {BoolToString(elem.Checked)}, {elem.Group});";
        }

        private static string DistroGump_GFAddButton(string gumpName, ButtonElement elem)
        {
            var text = elem.ButtonType == ButtonTypeEnum.Page ? "GF_PAGE_BTN" : "GF_CLOSE_BTN";
            return $"GFAddButton({gumpName}, {elem.X}, {elem.Y}, {elem.NormalId}, {elem.PressedId}, {text}, {elem.Param});";
        }

        private static string DistroGump_GFTilePic(string gumpName, ItemElement elem)
        {
            string text = elem.Hue != null ? elem.Hue.ToString() : "0";
            return $"GFTilePic({gumpName}, {elem.X}, {elem.Y}, {elem.ItemID}, {text});";
        }

        private static string DistroGump_GFGumpPic(string gumpName, ImageElement elem)
        {
            string text = elem.Hue != null ? elem.Hue.ToString() : "0";
            return $"GFGumpPic({gumpName}, {elem.X}, {elem.Y}, {elem.GumpId}, {text});";
        }

        private static string DistroGump_GFResizePic(string gumpName, BackgroundElement elem)
        {
            return $"GFResizePic({gumpName}, {elem.X}, {elem.Y}, {elem.GumpId}, {elem.Width}, {elem.Height});";
        }

        private static string DistroGump_GFAddAlphaRegion(string gumpName, AlphaElement elem)
        {
            return $"GFAddAlphaRegion({gumpName}, {elem.X}, {elem.Y}, {elem.Width}, {elem.Height});";
        }

        private string DistroGump_GFTextLine(string gumpName, LabelElement elem)
        {
            string text = elem.Hue != null ? elem.Hue.ToString() : "0";
            string text2 = _bGetDefaultText ? "TextLine" : "";
            if (!string.IsNullOrEmpty(elem.Text))
            {
                text2 = elem.Text;
            }

            return $"GFTextLine({gumpName}, {elem.X}, {elem.Y}, {text}, \"{text2}\");";
        }

        private string DistroGump_GFTextEntry(string gumpName, TextEntryElement elem)
        {
            string text = _bGetDefaultText ? "TextEntry" : "";
            if (!string.IsNullOrEmpty(elem.InitialText))
            {
                text = elem.InitialText;
            }

            string text2 = elem.Hue != null ? elem.Hue.ToString() : "0";

            return $"GFTextEntry({gumpName}, {elem.X}, {elem.Y}, {elem.Width}, {elem.Height}, {text2}, \"{text}\", {elem.Id});";
        }

        private static string DistroGump_GFAddHTMLLocalized(string gumpName, HtmlElement elem)
        {
            if (elem.ShowScrollbar || elem.ShowBackground)
            {
                return
                    $"GFAddHTMLLocalized({gumpName}, {elem.X}, {elem.Y}, {elem.Width}, {elem.Height}, {elem.CliLocId}, {BoolToString(elem.ShowBackground)}, {BoolToString(elem.ShowScrollbar)});";
            }

            return $"GFAddHTMLLocalized({gumpName}, {elem.X}, {elem.Y}, {elem.Width}, {elem.Height}, {elem.CliLocId});";
        }

        private string DistroGump_GFHTMLArea(string gumpName, HtmlElement elem)
        {
            string text = _bGetDefaultText ? "HtmlElement" : "";
            if (!string.IsNullOrEmpty(elem.Html))
            {
                text = elem.Html;
            }

            if (elem.ShowScrollbar || elem.ShowBackground)
            {
                return $"GFHTMLArea({gumpName}, {elem.X}, {elem.Y}, {elem.Width}, {elem.Height}, \"{text}\", {BoolToString(elem.ShowBackground)}, {BoolToString(elem.ShowScrollbar)});";
            }

            return $"GFHTMLArea({gumpName}, {elem.X}, {elem.Y}, {elem.Width}, {elem.Height}, \"{text}\");";
        }

        private static string DistroGump_GFPage(string gumpName, ref int pageindex)
        {
            return $"GFPage({gumpName}, {pageindex++});";
        }

        private static string DistroGump_GFCreateGump(string gumpName, Point loc)
        {
            if (loc.X != 0 || loc.Y != 0)
            {
                return $"var {gumpName} := GFCreateGump({loc.X},{loc.Y});";
            }

            return $"var {gumpName} := GFCreateGump();";
        }

        private StringWriter CreateBareScript()
        {
            var stringWriter = new StringWriter();
            var list = new List<string>();
            var list2 = new List<string>();

            stringWriter.WriteLine("// Created {0}, with Gump Studio.", DateTime.Now);
            stringWriter.WriteLine("// Exported with {0} ver {1}.", GetPluginInfo().PluginName, GetPluginInfo().Version);
            stringWriter.WriteLine("");

            if (!Designer.GumpProperties.Moveable)
            {
                list.Add("NoMove");
            }

            if (!Designer.GumpProperties.Closeable)
            {
                list.Add("NoClose");
            }

            if (!Designer.GumpProperties.Disposable)
            {
                list.Add("NoDispose");
            }

            if (Designer.Stacks.Count > 0)
            {
                int num = -1;
                int num2 = 0;

                foreach (var groupElement in Designer.Stacks)
                {
                    list.Add(Gump_WritePage(ref num2));

                    if (groupElement == null)
                    {
                        continue;
                    }

                    foreach (var baseElement in groupElement.GetElementsRecursive())
                    {
                        if (baseElement == null)
                        {
                            continue;
                        }

                        var type = baseElement.GetType();
                        if (type == typeof(HtmlElement))
                        {
                            var htmlelement = baseElement as HtmlElement;
                            if (htmlelement.TextType == HtmlElementType.Html)
                            {
                                string item = Gump_WriteHTMLGump(htmlelement, ref list2);
                                list.Add(item);
                            }
                            else
                            {
                                string item2 = Gump_WriteXMFHtmlGump(htmlelement);
                                list.Add(item2);
                            }
                        }
                        else if (type == typeof(TextEntryElement))
                        {
                            var elem = baseElement as TextEntryElement;
                            string item3 = Gump_WriteTextEntry(elem, ref list2);
                            list.Add(item3);
                        }
                        else if (type == typeof(LabelElement))
                        {
                            var elem2 = baseElement as LabelElement;
                            string item4 = Gump_WriteText(elem2, ref list2);
                            list.Add(item4);
                        }
                        else if (type == typeof(AlphaElement))
                        {
                            var elem3 = baseElement as AlphaElement;
                            string item5 = Gump_WriteCheckerTrans(elem3);
                            list.Add(item5);
                        }
                        else if (type == typeof(BackgroundElement))
                        {
                            var elem4 = baseElement as BackgroundElement;
                            string item6 = Gump_WriteResizePic(elem4);
                            list.Add(item6);
                        }
                        else if (type == typeof(ImageElement))
                        {
                            var elem5 = baseElement as ImageElement;
                            string item7 = Gump_WriteGumpPic(elem5);
                            list.Add(item7);
                        }
                        else if (type == typeof(ItemElement))
                        {
                            var elem6 = baseElement as ItemElement;
                            string item8 = Gump_WriteTilePic(elem6);
                            list.Add(item8);
                        }
                        else if (type == typeof(TiledElement))
                        {
                            var elem7 = baseElement as TiledElement;
                            string item9 = Gump_WriteGumpPicTiled(elem7);
                            list.Add(item9);
                        }
                        else if (type == typeof(ButtonElement))
                        {
                            var elem8 = baseElement as ButtonElement;
                            string item10 = Gump_WriteButton(elem8);
                            list.Add(item10);
                        }
                        else if (type == typeof(CheckboxElement))
                        {
                            var elem9 = baseElement as CheckboxElement;
                            string item11 = Gump_WriteCheckBox(elem9);
                            list.Add(item11);
                        }
                        else if (type == typeof(RadioElement))
                        {
                            var radioElement = baseElement as RadioElement;
                            if (radioElement != null && radioElement.Group != num)
                            {
                                list.Add("group " + radioElement.Group);
                                num = radioElement.Group;
                            }
                            string item12 = Gump_WriteRadioBox(radioElement);
                            list.Add(item12);
                        }
                    }
                }
            }
            stringWriter.WriteLine("");
            stringWriter.WriteLine("use uo;");
            stringWriter.WriteLine("use os;");
            stringWriter.WriteLine("");
            stringWriter.WriteLine("program gump_{0}(who)", FrmPolExportForm.GumpName);
            stringWriter.WriteLine("");
            stringWriter.WriteLine("\tvar gump := array {");

            int num3 = 1;

            foreach (string arg in list)
            {
                stringWriter.Write("\t\t\"{0}\"", arg);
                if (num3 == list.Count)
                {
                    stringWriter.WriteLine("");
                }
                else
                {
                    stringWriter.WriteLine(","); // , arg// TODO: wtf no format string?
                }
                num3++;
            }

            stringWriter.WriteLine("\t};");
            stringWriter.WriteLine("\tvar data := array {");

            num3 = 1;

            foreach (string arg2 in list2)
            {
                stringWriter.Write("\t\t\"{0}\"", arg2);
                stringWriter.WriteLine(num3 == list2.Count ? "" : ",");
                num3++;
            }

            stringWriter.WriteLine("\t};");
            stringWriter.WriteLine("");
            stringWriter.WriteLine("\tSendDialogGump(who, gump, data{0});", Gump_Location(Designer.GumpProperties.Location));
            stringWriter.WriteLine("");
            stringWriter.WriteLine("endprogram");

            return stringWriter;
        }

        private static string Gump_WriteRadioBox(RadioElement elem)
        {
            return $"radio {elem.X} {elem.Y} {elem.UnCheckedId} {elem.CheckedId} {BoolToString(elem.Checked)} {elem.Value}";
        }

        private static string Gump_WriteButton(ButtonElement elem)
        {
            bool check = true;
            string text = "0";
            string text2 = elem.Param.ToString();
            if (elem.ButtonType == ButtonTypeEnum.Reply) // TODO: Page or Reply???
            {
                check = false;
                text = elem.Param.ToString();
                text2 = "0";
            }
            return $"button {elem.X} {elem.Y} {elem.NormalId} {elem.PressedId} {BoolToString(check)} {text} {text2}";
        }

        private static string Gump_WriteCheckBox(CheckboxElement elem)
        {
            return $"checkbox {elem.X} {elem.Y} {elem.UnCheckedId} {elem.CheckedId} {BoolToString(elem.Checked)} {elem.Group}";
        }

        private static string Gump_WriteGumpPicTiled(TiledElement elem)
        {
            return $"gumppictiled {elem.X} {elem.Y} {elem.Width} {elem.Height} {elem.GumpId}";
        }

        private static string Gump_WriteTilePic(ItemElement elem)
        {
            return IsHued(elem.Hue.ToString())
                ? $"tilepichue {elem.X} {elem.Y} {elem.ItemID} {elem.Hue}"
                : $"tilepic {elem.X} {elem.Y} {elem.ItemID}";
        }

        private static string Gump_WriteGumpPic(ImageElement elem)
        {
            return IsHued(elem.Hue.ToString())
                ? $"gumppic {elem.X} {elem.Y} {elem.GumpId} {elem.Hue}"
                : $"gumppic {elem.X} {elem.Y} {elem.GumpId}";
        }

        private static string Gump_WriteResizePic(BackgroundElement elem)
        {
            return $"resizepic {elem.X} {elem.Y} {elem.GumpId} {elem.Width} {elem.Height}";
        }

        private static string Gump_WriteCheckerTrans(AlphaElement elem)
        {
            return $"checkertrans {elem.X} {elem.Y} {elem.Width} {elem.Height}";
        }

        private static string Gump_WriteText(LabelElement elem, ref List<string> texts)
        {
            int count = texts.Count;
            string text = "Text id." + count;
            if (elem.Text != null)
            {
                text = elem.Text?.Length == 0 ? text : elem.Text;
            }

            texts.Add(text);

            return $"text {elem.X} {elem.Y} {elem.Hue} {count}";
        }

        private static string Gump_WritePage(ref int pageId)
        {
            return $"page {pageId++}";
        }

        private static string Gump_WriteHTMLGump(HtmlElement elem, ref List<string> texts)
        {
            int count = texts.Count;
            string text = "HtmlGump id." + count;
            if (elem.Html != null)
            {
                text = elem.Html?.Length == 0 ? text : elem.Html;
            }
            texts.Add(text);
            return $"htmlgump {elem.X} {elem.Y} {elem.Width} {elem.Height} {count} {BoolToString(elem.ShowBackground)} {BoolToString(elem.ShowScrollbar)}";
        }

        private static string Gump_WriteXMFHtmlGump(HtmlElement elem)
        {
            return $"xmfhtmlgump {elem.X} {elem.Y} {elem.Width} {elem.Height} {elem.CliLocId} {BoolToString(elem.ShowBackground)} {BoolToString(elem.ShowScrollbar)}";
        }

        private static string Gump_WriteTextEntry(TextEntryElement elem, ref List<string> texts)
        {
            int count = texts.Count;
            string text = "TextEntry id." + count;
            if (elem.InitialText != null)
            {
                text = elem.InitialText?.Length == 0 ? text : elem.InitialText;
            }

            texts.Add(text);

            return $"textentry {elem.X} {elem.Y} {elem.Width} {elem.Height} {elem.Hue} {elem.Id} {count}";
        }

        private static string Gump_Location(Point loc)
        {
            if (loc.X == 0 && loc.Y == 0)
            {
                return "";
            }

            return $", {loc.X}, {loc.Y}";
        }

        private static string BoolToString(bool check)
        {
            return check ? "1" : "0";
        }

        private static bool IsHued(string hue)
        {
            return !string.IsNullOrEmpty(hue) && hue != "0";
        }

        protected DesignerForm Designer;

        protected MenuItem MnuFileExportPolExport;

        //protected MenuItem MnuMenuItem;

        protected PolExportForm FrmPolExportForm;

        private bool _bGetDefaultText;
    }
}
