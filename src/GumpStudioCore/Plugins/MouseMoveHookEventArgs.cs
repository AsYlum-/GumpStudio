using System;
using System.Drawing;
using System.Windows.Forms;
using GumpStudio.Enums;

namespace GumpStudio.Plugins
{
    public class MouseMoveHookEventArgs : EventArgs
    {
        public Keys Keys;
        public MouseButtons MouseButtons;
        public Point MouseLocation;
        public MoveModeType MoveMode;
    }
}
