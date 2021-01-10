using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace GumpStudio.Elements
{
    [Serializable]
    public class RadioElement : CheckboxElement
    {
        protected int MValue;

        public override bool Checked
        {
            get => base.Checked;
            set
            {
                mChecked = value;
                if (!mChecked)
                {
                    return;
                }

                foreach (BaseElement element in mParent.GetElementsRecursive())
                {
                    if (!(element is RadioElement radioElement))
                    {
                        continue;
                    }

                    if (radioElement != this && radioElement.Checked && radioElement.Group == Group)
                    {
                        radioElement.Checked = false;
                    }
                }
            }
        }

        [Description("The Group that the radio buttons belongs to.  Only one button in a group may be selected at a time.")]
        public override int Group
        {
            get => GroupId;
            set => GroupId = value;
        }

        public override string Type => "Radio Button";

        [MergableProperty(false)]
        [Description("The value fo this radio button returned to the script")]
        public int Value
        {
            get => MValue;
            set => MValue = value;
        }

        public RadioElement()
        {
            CheckedId = 208;
            UnCheckedId = 209;
        }

        public RadioElement(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info.GetInt32("RadioElementVersion") >= 2)
            {
                MValue = info.GetInt32("Value");
            }
            RefreshCache();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("RadioElementVersion", 2);
            info.AddValue("Value", MValue);
        }
    }
}
