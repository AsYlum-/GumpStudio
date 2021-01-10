using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;

namespace SnapToGrid
{
    [Serializable]
    public class GridConfiguration : SerializationBinder
    {
        public Size GridSize;
        public Color GridColor;
        public bool ShowGrid;

        public GridConfiguration()
        {
        }

        public GridConfiguration(Size gridSize, Color gridColor, bool showGrid)
        {
            GridSize = gridSize;
            GridColor = gridColor;
            ShowGrid = showGrid;
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            Type bindToType = null;
            string shortAssemblyName = assemblyName.Split(',')[0];

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (shortAssemblyName == assembly.FullName.Split(',')[0])
                {
                    bindToType = assembly.GetType(typeName);
                    break;
                }
            }

            return bindToType;
        }
    }
}
