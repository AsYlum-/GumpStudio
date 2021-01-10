using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;
using GumpStudio.Editors;
using GumpStudio.Enums;
using Ultima;

namespace GumpStudio.Elements
{
    [Serializable]
    public class ButtonElement : BaseElement
    {
        protected Bitmap Cache;

        protected string CodeBehind;

        protected int mNormalId;

        protected int mParam;

        protected int mPressedId;

        protected ButtonStateEnum mState;

        protected ButtonTypeEnum mType;

        [Description("The Type of button. Page buttons change the current page, and Reply buttons return a value to the script.")]
        public ButtonTypeEnum ButtonType
        {
            get => mType;
            set
            {
                mType = value;
                RefreshCache();
            }
        }

        [Editor(typeof(LargeTextPropEditor), typeof(UITypeEditor))]
        [Description("This contains script code to be executed when this button is pressed. (must be supported by the export plugin)")]
        public string Code
        {
            get => CodeBehind;
            set => CodeBehind = value;
        }

        [Description("The ID of the image to display when the button is not being pressed.")]
        [Editor(typeof(GumpIdPropEditor), typeof(UITypeEditor))]
        public int NormalId
        {
            get => mNormalId;
            set
            {
                mNormalId = value;
                RefreshCache();
            }
        }

        [Description("For Page Buttons this represents the page to switch to.  For Reply buttons this represents the value to return to the script.")]
        public int Param
        {
            get => mParam;
            set => mParam = value;
        }

        [Description("The ID of the image to display when the button is being pressed by the user.")]
        [Editor(typeof(GumpIdPropEditor), typeof(UITypeEditor))]
        public int PressedId
        {
            get => mPressedId;
            set
            {
                mPressedId = value;
                RefreshCache();
            }
        }

        [Description("Change this to see the button in it's different states.")]
        public ButtonStateEnum State
        {
            get => mState;
            set
            {
                mState = value;
                RefreshCache();
            }
        }

        public override string Type => "Button";

        public ButtonElement()
        {
            mType = ButtonTypeEnum.Reply;
            mState = ButtonStateEnum.Normal;
            mType = ButtonTypeEnum.Reply;
            mState = ButtonStateEnum.Normal;
            mPressedId = 248;
            mNormalId = 247;
            RefreshCache();
        }

        protected ButtonElement(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            mType = ButtonTypeEnum.Reply;
            mState = ButtonStateEnum.Normal;
            info.GetInt32("ButtonElementVersion");
            mPressedId = info.GetInt32("PressedID");
            mNormalId = info.GetInt32("NormalID");
            mType = (ButtonTypeEnum)info.GetInt32("Type");
            mState = (ButtonStateEnum)info.GetInt32("State");
            CodeBehind = info.GetString("CodeBehind");
            mParam = info.GetInt32("Param");
            RefreshCache();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ButtonElementVersion", 1);
            info.AddValue("PressedID", mPressedId);
            info.AddValue("NormalID", mNormalId);
            info.AddValue("Type", (int)mType);
            info.AddValue("State", (int)mState);
            info.AddValue("CodeBehind", CodeBehind);
            info.AddValue("Param", mParam);
        }

        public override void RefreshCache()
        {
            Cache?.Dispose();

            Cache = mState != 0 ? Gumps.GetGump(mPressedId) : Gumps.GetGump(mNormalId);

            if (Cache != null)
            {
                mSize = Cache.Size;
            }
        }

        public override void Render(Graphics target)
        {
            if (Cache == null)
            {
                RefreshCache();
            }

            target.DrawImage(Cache, Location);
        }
    }
}
