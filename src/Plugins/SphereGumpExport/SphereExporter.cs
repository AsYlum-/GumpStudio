using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using GumpStudio.Elements;
using GumpStudio.Enums;
using GumpStudio.Forms;
using GumpStudio.Plugins;
using HtmlElement = GumpStudio.Elements.HtmlElement;

namespace SphereGumpExport
{
    public class SphereExporter : BasePlugin
    {
        private DesignerForm _designer;
        private MenuItem _mnuFileExportSphereExport;
        protected MenuItem MnuMenuItem;
        private SphereExportForm _frmSphereExportForm;

        public override string Name => GetPluginInfo().PluginName;

        public override PluginInfo GetPluginInfo()
        {
            return new PluginInfo
            {
                AuthorEmail = "furio@sphere-italia.org",
                AuthorName = "Francesco Furiani",
                Description = "Exports the Gump into a Sphere script.",
                PluginName = nameof(SphereExporter),
                Version = "1.1"
            };
        }

        public override void Load(DesignerForm frmDesigner)
        {
            _designer = frmDesigner;
            _mnuFileExportSphereExport = new MenuItem("Sphere");
            _mnuFileExportSphereExport.Click += Mnu_FileExportSphereExport_Click;
            if (!_designer.MnuFileExport.Enabled)
            {
                _designer.MnuFileExport.Enabled = true;
            }

            _designer.MnuFileExport.MenuItems.Add(_mnuFileExportSphereExport);
            base.Load(frmDesigner);
        }

        private void Mnu_FileExportSphereExport_Click(object sender, EventArgs e)
        {
            _frmSphereExportForm = new SphereExportForm(this);
            _frmSphereExportForm.ShowDialog();
        }

        public StringWriter GetSphereScript(bool bIsRevision)
        {
            StringWriter stringWriter1 = new StringWriter();

            ArrayList arrayList1 = new ArrayList();
            ArrayList arrayList2 = new ArrayList();

            stringWriter1.WriteLine("// Created {0}, with Gump Studio.", DateTime.Now);
            stringWriter1.WriteLine("// Exported with with {0} ver {1}.", GetPluginInfo().PluginName, GetPluginInfo().Version);
            stringWriter1.WriteLine("// Script for {0}", bIsRevision ? "0.56/Revisions" : "0.99/1.0");
            stringWriter1.WriteLine("");
            stringWriter1.WriteLine("[DIALOG {0}]", _frmSphereExportForm.GumpName);
            StringWriter stringWriter2 = stringWriter1;
            int num1 = bIsRevision ? 1 : 0;
            Point location = _designer.GumpProperties.Location;
            int x = location.X;
            location = _designer.GumpProperties.Location;
            int y = location.Y;
            string str = Gump_WriteLocation(num1 != 0, x, y);
            stringWriter2.WriteLine("{0}", str);
            if (!_designer.GumpProperties.Closeable)
            {
                stringWriter1.WriteLine("{0}", bIsRevision ? "NOCLOSE" : "NoClose");
            }

            if (!_designer.GumpProperties.Moveable)
            {
                stringWriter1.WriteLine("{0}", bIsRevision ? "NOMOVE" : "NoMove");
            }

            if (!_designer.GumpProperties.Disposable)
            {
                stringWriter1.WriteLine("{0}", bIsRevision ? "NODISPOSE" : "NoDispose");
            }

            if (_designer.Stacks.Count > 0)
            {
                int id1 = 0;
                int id2 = 0;
                int id3 = 0;
                int num2 = -1;
                for (int iPage = 0; iPage < _designer.Stacks.Count; ++iPage)
                {
                    stringWriter1.WriteLine("{0}", Gump_WritePage(bIsRevision, iPage));
                    if (!(_designer.Stacks[iPage] is GroupElement stack))
                    {
                        continue;
                    }

                    List<BaseElement> elementsRecursive = stack.GetElementsRecursive();
                    if (elementsRecursive.Count == 0)
                    {
                        continue;
                    }

                    for (int index = 0; index < elementsRecursive.Count; ++index)
                    {
                        if (elementsRecursive[index] is BaseElement baseElement)
                        {
                            switch (baseElement)
                            {
                                case HtmlElement htmlElement:
                                    if (htmlElement.TextType == HtmlElementType.Html)
                                    {
                                        if (bIsRevision)
                                        {
                                            string text = $"HtmlGump id.{id1}";
                                            if (htmlElement.Html != null)
                                            {
                                                arrayList2.Add(new SphereElement(
                                                    htmlElement.Html.Length == 0 ? text : htmlElement.Html,
                                                    id1));
                                            }
                                            else
                                            {
                                                arrayList2.Add(new SphereElement(text, id1));
                                            }
                                        }

                                        stringWriter1.WriteLine("{0}",
                                            Gump_WriteHTML(bIsRevision, htmlElement.X, htmlElement.Y,
                                                htmlElement.Width, htmlElement.Height,
                                                htmlElement.ShowBackground, htmlElement.ShowScrollbar, ref id1,
                                                htmlElement.Html));
                                        continue;
                                    }

                                    stringWriter1.WriteLine("{0}",
                                        Gump_WriteXFHTML(bIsRevision, htmlElement.X, htmlElement.Y,
                                            htmlElement.Width, htmlElement.Height, htmlElement.ShowBackground,
                                            htmlElement.ShowScrollbar, htmlElement.CliLocId));
                                    continue;
                                case AlphaElement alphaElement:
                                    stringWriter1.WriteLine("{0}",
                                        Gump_WriteCheckerTrans(bIsRevision, alphaElement.X,
                                            alphaElement.Y, alphaElement.Width, alphaElement.Height));
                                    continue;
                                case BackgroundElement backgroundElement:
                                    stringWriter1.WriteLine("{0}",
                                        Gump_WriteResizePic(bIsRevision, backgroundElement.X,
                                            backgroundElement.Y, backgroundElement.Width,
                                            backgroundElement.Height, backgroundElement.GumpId));
                                    continue;
                                case ButtonElement buttonElement:
                                    arrayList1.Add(new SphereElement(
                                        $"// {buttonElement.Name}\n// {buttonElement.Code}",
                                        buttonElement.ButtonType == ButtonTypeEnum.Reply
                                            ? buttonElement.Param
                                            : id2));
                                    stringWriter1.WriteLine("{0}",
                                        Gump_WriteButton(bIsRevision, buttonElement.X, buttonElement.Y,
                                            buttonElement.NormalId, buttonElement.PressedId,
                                            buttonElement.ButtonType == ButtonTypeEnum.Reply,
                                            buttonElement.Param, ref id2));
                                    continue;
                                case ImageElement imageElement:
                                    stringWriter1.WriteLine("{0}",
                                        Gump_WriteGumpPic(bIsRevision, imageElement.X, imageElement.Y,
                                            imageElement.GumpId, imageElement.Hue.ToString()));
                                    continue;
                                case ItemElement itemElement:
                                    stringWriter1.WriteLine("{0}",
                                        Gump_WriteTilePic(bIsRevision, itemElement.X, itemElement.Y,
                                            itemElement.ItemID, itemElement.Hue.ToString()));
                                    continue;
                                case LabelElement labelElement:
                                    if (bIsRevision)
                                    {
                                        string text = $"Text id.{id1}";
                                        if (labelElement.Text != null)
                                        {
                                            arrayList2.Add(new SphereElement(
                                                labelElement.Text.Length == 0 ? text : labelElement.Text, id1));
                                        }
                                        else
                                        {
                                            arrayList2.Add(new SphereElement(text, id1));
                                        }
                                    }

                                    stringWriter1.WriteLine("{0}",
                                        Gump_WriteText(bIsRevision, labelElement.X, labelElement.Y,
                                            labelElement.Hue.ToString(), labelElement.Text, ref id1));
                                    continue;
                                case RadioElement radioElement:
                                    if (radioElement.Group != num2)
                                    {
                                        stringWriter1.WriteLine("Group{0}",
                                            bIsRevision
                                                ? $" {radioElement.Group}"
                                                : $"({radioElement.Group})");
                                        num2 = radioElement.Group;
                                    }

                                    stringWriter1.WriteLine("{0}",
                                        Gump_WriteRadioBox(bIsRevision, radioElement.X, radioElement.Y,
                                            radioElement.UnCheckedId, radioElement.CheckedId,
                                            radioElement.Checked, radioElement.Value));
                                    continue;
                                case CheckboxElement checkboxElement:
                                    stringWriter1.WriteLine("{0}",
                                        Gump_WriteCheckBox(bIsRevision, checkboxElement.X,
                                            checkboxElement.Y, checkboxElement.UnCheckedId,
                                            checkboxElement.CheckedId, checkboxElement.Checked, ref id3));
                                    continue;
                                case TextEntryElement textEntryElement:
                                    if (bIsRevision)
                                    {
                                        string text = $"Textentry id.{textEntryElement.Id}";
                                        if (textEntryElement.InitialText != null)
                                        {
                                            arrayList2.Add(new SphereElement(
                                                textEntryElement.InitialText.Length == 0
                                                    ? text
                                                    : textEntryElement.InitialText, id1));
                                        }
                                        else
                                        {
                                            arrayList2.Add(new SphereElement(text, id1));
                                        }
                                    }

                                    stringWriter1.WriteLine("{0}",
                                        Gump_WriteTextEntry(bIsRevision, textEntryElement.X,
                                            textEntryElement.Y, textEntryElement.Width, textEntryElement.Height,
                                            textEntryElement.Hue.ToString(), textEntryElement.InitialText,
                                            textEntryElement.Id, ref id1));
                                    continue;
                                case TiledElement tiledElement:
                                    stringWriter1.WriteLine("{0}",
                                        Gump_WriteGumpPicTiled(bIsRevision, tiledElement.X,
                                            tiledElement.Y, tiledElement.Width, tiledElement.Height,
                                            tiledElement.GumpId));
                                    continue;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }

            stringWriter1.WriteLine("");

            if (bIsRevision)
            {
                stringWriter1.WriteLine("[DIALOG {0} text]", _frmSphereExportForm.GumpName);
                foreach (SphereElement sphereElement in arrayList2)
                {
                    stringWriter1.WriteLine("{0}", sphereElement.Text);
                }

                stringWriter1.WriteLine("");
            }

            stringWriter1.WriteLine("[DIALOG {0} button]", _frmSphereExportForm.GumpName);
            foreach (SphereElement sphereElement in arrayList1)
            {
                stringWriter1.WriteLine("ON={0}", sphereElement.Id.ToString());
                stringWriter1.WriteLine("{0}", sphereElement.Text);
                stringWriter1.WriteLine("");
            }

            stringWriter1.WriteLine("{0}", Gump_WriteEOF());
            return stringWriter1;
        }

        private static string Gump_WriteEOF()
        {
            return "\n[EOF]";
        }

        private static string Gump_WriteLocation(bool bIsRevision, int x, int y)
        {
            return bIsRevision ? $"{x},{y}" : $"SetLocation={x},{y}";
        }

        private static string Gump_WritePage(bool bIsRevision, int iPage)
        {
            return bIsRevision ? $"page {iPage}" : $"Page({iPage})";
        }

        private static string Gump_WriteHTML(
          bool bIsRevision,
          int x,
          int y,
          int width,
          int height,
          bool background,
          bool scrollbar,
          ref int id,
          string text)
        {
            StringWriter stringWriter = new StringWriter();
            if (bIsRevision)
            {
                stringWriter.Write("htmlgump {0} {1} {2} {3} {4} {5} {6}", x.ToString(), y.ToString(), width.ToString(), height.ToString(), id.ToString(), background ? "1" : "0", scrollbar ? "1" : "0");
                ++id;
            }
            else
            {
                if (text == null)
                {
                    text = " ";
                }

                string str = text;
                str = str.Replace("\"", "\\\"");
                stringWriter.Write("HtmlGumpA({0},{1},{2},{3},\"{4}\",{5},{6})", x.ToString(), y.ToString(), width.ToString(), height.ToString(), str, background ? "1" : "0", scrollbar ? "1" : "0");
            }
            return stringWriter.ToString();
        }

        private static string Gump_WriteXFHTML(
          bool bIsRevision,
          int x,
          int y,
          int width,
          int height,
          bool background,
          bool scrollbar,
          int cliloc)
        {
            StringWriter stringWriter = new StringWriter();

            stringWriter.Write(
                bIsRevision ? "xmfhtmlgump {0} {1} {2} {3} {4} {5} {6}" : "XmfHtmlGump({0},{1},{2},{3},{4},{5},{6})",
                x.ToString(), y.ToString(), width.ToString(), height.ToString(), cliloc.ToString(),
                background ? "1" : "0", scrollbar ? "1" : "0");

            return stringWriter.ToString();
        }

        private string Gump_WriteCheckerTrans(bool bIsRevision, int x, int y, int width, int height)
        {
            StringWriter stringWriter = new StringWriter();
            stringWriter.Write(
                bIsRevision
                    ? "checkertrans {0} {1} {2} {3}"
                    : "CheckerTrans({0},{1},{2},{3})",
                x.ToString(), y.ToString(), width.ToString(), height.ToString());

            return stringWriter.ToString();
        }

        private static string Gump_WriteResizePic(
          bool bIsRevision,
          int x,
          int y,
          int width,
          int height,
          int gumpId)
        {
            StringWriter stringWriter = new StringWriter();
            stringWriter.Write(
                bIsRevision
                    ? "resizepic {0} {1} {2} {3} {4}"
                    : "ResizePic({0},{1},{2},{3},{4})",
                x.ToString(), y.ToString(), gumpId.ToString(), width.ToString(),
                height.ToString());

            return stringWriter.ToString();
        }

        private static string Gump_WriteGumpPic(bool bIsRevision, int x, int y, int gumpId, string hue)
        {
            StringWriter stringWriter = new StringWriter();
            bool flag = false;
            switch (hue)
            {
                case "":
                case null:
                    if (bIsRevision)
                    {
                        stringWriter.Write("gumppic {0} {1} {2}{3}", x.ToString(), y.ToString(), gumpId.ToString(), flag ? $" {hue}"
                                                                                                                        : "");
                    }
                    else
                    {
                        stringWriter.Write("GumpPic({0},{1},{2}{3})", x.ToString(), y.ToString(), gumpId.ToString(), flag ? $",{hue}"
                                                                                                                         : "");
                    }

                    return stringWriter.ToString();
                default:
                    if (!string.Equals(hue, "0", StringComparison.OrdinalIgnoreCase))
                    {
                        flag = true;
                        goto case "";
                    }
                    else
                    {
                        goto case "";
                    }
            }
        }

        private string Gump_WriteTilePic(bool bIsRevision, int x, int y, int itemId, string hue)
        {
            StringWriter stringWriter = new StringWriter();
            bool flag = false;
            switch (hue)
            {
                case "":
                case null:
                    if (bIsRevision)
                    {
                        stringWriter.Write("tilepic{0} {1} {2} {3}{4}", flag ? nameof(hue) : "", x.ToString(), y.ToString(), itemId.ToString(), flag ? $" {hue}"
                                                                                                                                                    : "");
                    }
                    else
                    {
                        stringWriter.Write("TilePic{0}({1},{2},{3}{4})", flag ? "Hue" : "", x.ToString(), y.ToString(), itemId.ToString(), flag ? $",{hue}"
                                                                                                                                               : "");
                    }

                    return stringWriter.ToString();
                default:
                    if (!string.Equals(hue, "0", StringComparison.OrdinalIgnoreCase))
                    {
                        flag = true;
                        goto case "";
                    }
                    else
                    {
                        goto case "";
                    }
            }
        }

        private static string Gump_WriteGumpPicTiled(
          bool bIsRevision,
          int x,
          int y,
          int width,
          int height,
          int gumpId)
        {
            StringWriter stringWriter = new StringWriter();
            stringWriter.Write(
                bIsRevision
                    ? "gumppictiled {0} {1} {2} {3} {4}"
                    : "GumpPicTiled({0},{1},{2},{3},{4})",
                x.ToString(), y.ToString(), width.ToString(), height.ToString(),
                gumpId.ToString());

            return stringWriter.ToString();
        }

        private static string Gump_WriteButton(
          bool bIsRevision,
          int x,
          int y,
          int normalId,
          int pressedId,
          bool isReply,
          int pageTo,
          ref int id)
        {
            StringWriter stringWriter = new StringWriter();

            stringWriter.Write(
                bIsRevision ? "button {0} {1} {2} {3} {4} {5} {6}" : "Button({0},{1},{2},{3},{4},{5},{6})",
                x.ToString(), y.ToString(), normalId.ToString(), pressedId.ToString(), "1",
                isReply ? "0" : pageTo.ToString(), isReply ? pageTo.ToString() : id.ToString());

            if (!isReply)
            {
                ++id;
            }

            return stringWriter.ToString();
        }

        private string Gump_WriteText(
          bool bIsRevision,
          int x,
          int y,
          string hue,
          string text,
          ref int id)
        {
            StringWriter stringWriter = new StringWriter();
            if (bIsRevision)
            {
                stringWriter.Write("text {0} {1} {2} {3}", x.ToString(), y.ToString(), hue, id.ToString());
                ++id;
            }
            else
            {
                if (text == null)
                {
                    text = " ";
                }

                string str = text;
                str = str.Replace("\"", "\\\"");
                stringWriter.Write("TextA({0},{1},{2},\"{3}\")", x.ToString(), y.ToString(), hue, str);
            }
            return stringWriter.ToString();
        }

        private static string Gump_WriteCheckBox(
          bool bIsRevision,
          int x,
          int y,
          int uncheckedId,
          int checkedId,
          bool isChecked,
          ref int id)
        {
            StringWriter stringWriter = new StringWriter();

            stringWriter.Write(bIsRevision ? "checkbox {0} {1} {2} {3} {4} {5}" : "CheckBox({0},{1},{2},{3},{4},{5})",
                x.ToString(), y.ToString(), checkedId.ToString(), uncheckedId.ToString(), isChecked ? "1" : "0",
                id.ToString());

            ++id;
            return stringWriter.ToString();
        }

        private string Gump_WriteTextEntry(
          bool bIsRevision,
          int x,
          int y,
          int width,
          int height,
          string hue,
          string text,
          int returnId,
          ref int id)
        {
            StringWriter stringWriter = new StringWriter();
            if (bIsRevision)
            {
                stringWriter.Write("textentry {0} {1} {2} {3} {4} {5} {6}", x.ToString(), y.ToString(), width.ToString(), height.ToString(), hue, returnId.ToString(), id.ToString());
                ++id;
            }
            else
            {
                if (text == null)
                {
                    text = " ";
                }

                string str = text;
                str = str.Replace("\"", "\\\"");
                stringWriter.Write("TextEntryA({0},{1},{2},{3},{4},{5},\"{6}\")", x.ToString(), y.ToString(), width.ToString(), height.ToString(), hue, returnId.ToString(), str);
            }
            return stringWriter.ToString();
        }

        private static string Gump_WriteRadioBox(
          bool bIsRevision,
          int x,
          int y,
          int uncheckedId,
          int checkedId,
          bool isChecked,
          int id)
        {
            StringWriter stringWriter = new StringWriter();

            stringWriter.Write(bIsRevision ? "radio {0} {1} {2} {3} {4} {5}" : "Radio({0},{1},{2},{3},{4},{5})",
                x.ToString(), y.ToString(), checkedId.ToString(), uncheckedId.ToString(), isChecked ? "1" : "0",
                id.ToString());

            return stringWriter.ToString();
        }

        private class SphereElement
        {
            public readonly string Text;
            public readonly int Id;

            public SphereElement(string text, int id)
            {
                Text = text;
                Id = id;
            }
        }
    }
}
