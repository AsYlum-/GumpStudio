using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;
using GumpStudio.Editors;
using Ultima;

namespace GumpStudio.Elements
{
    [Serializable]
    public class CheckboxElement : BaseElement
    {
        protected Image Image1Cache;

        protected Image Image2Cache;

        protected bool mChecked;

        protected int mCheckedID;

        protected int GroupId;

        protected int UncheckedId;

        [Description("Sets the initial state of the checkbox.")]
        public virtual bool Checked
        {
            get => mChecked;
            set
            {
                mChecked = value;
                RefreshCache();
            }
        }

        [Editor(typeof(GumpIdPropEditor), typeof(UITypeEditor))]
        public virtual int CheckedId
        {
            get => mCheckedID;
            set
            {
                mCheckedID = value;
                RefreshCache();
            }
        }

        [Description("The Value of the checkbox returned to the script.")]
        public virtual int Group
        {
            get => GroupId;
            set => GroupId = value;
        }

        public override string Type => "Checkbox";

        [Editor(typeof(GumpIdPropEditor), typeof(UITypeEditor))]
        public virtual int UnCheckedId
        {
            get => UncheckedId;
            set
            {
                UncheckedId = value;
                RefreshCache();
            }
        }

        public CheckboxElement()
        {
            UncheckedId = 210;
            mCheckedID = 211;
            RefreshCache();
        }

        public CheckboxElement(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            info.GetInt32("CheckboxVersion");
            mChecked = info.GetBoolean("Checked");
            mCheckedID = info.GetInt32("CheckedID");
            UncheckedId = info.GetInt32("UncheckedID");
            GroupId = info.GetInt32("GroupID");
            RefreshCache();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("CheckboxVersion", 1);
            info.AddValue("Checked", mChecked);
            info.AddValue("CheckedID", mCheckedID);
            info.AddValue("UncheckedID", UncheckedId);
            info.AddValue("GroupID", GroupId);
        }

        public override void RefreshCache()
        {
            Image1Cache?.Dispose();

            if (Image2Cache != null)
            {
                Image1Cache?.Dispose();
            }

            Image1Cache = Gumps.GetGump(UncheckedId);
            if (Image1Cache == null)
            {
                UnCheckedId = 210;
            }

            Image2Cache = Gumps.GetGump(mCheckedID);
            if (Image2Cache == null)
            {
                CheckedId = 211;
            }

            mSize = mChecked ? Image2Cache.Size : Image1Cache.Size; // TODO: wtf?
        }

        public override void Render(Graphics target)
        {
            if (Image1Cache == null || Image2Cache == null)
            {
                RefreshCache();
            }

            target.DrawImage(mChecked ? Image2Cache : Image1Cache, Location);
        }
    }
}
