using System.Collections.Generic;
using GumpStudio.Elements;
using GumpStudio.Forms;

namespace GumpStudio.Classes
{
    public class UndoPoint
    {
        public readonly GroupElement ElementStack;
        public readonly GumpProperties GumpProperties;
        public readonly List<GroupElement> Stack;
        public string Text;

        public UndoPoint(DesignerForm designer)
        {
            Stack = new List<GroupElement>();
            GumpProperties = (GumpProperties)designer.GumpProperties.Clone();

            foreach (var groupElement in designer.Stacks)
            {
                var clone = (GroupElement)groupElement.Clone();

                Stack.Add(clone);

                if (groupElement == designer.ElementStack)
                {
                    ElementStack = clone;
                }
            }
        }
    }
}
