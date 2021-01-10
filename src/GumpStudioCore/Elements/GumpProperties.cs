using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GumpStudio.Elements
{
    [Serializable]
    public class GumpProperties : ISerializable, ICloneable
    {
        public bool Closeable { get; set; }

        public bool Disposable { get; set; }

        public Point Location { get; set; }

        public bool Moveable { get; set; }

        public int Type { get; set; }

        public GumpProperties()
        {
            Moveable = true;
            Closeable = true;
            Disposable = true;
        }

        protected GumpProperties(SerializationInfo info, StreamingContext context)
        {
            Moveable = true;
            Closeable = true;
            Disposable = true;

            int _ = info.GetInt32("Version"); // TODO: do we need version here?
            Location = (Point)info.GetValue("Location", typeof(Point));
            Moveable = info.GetBoolean("Moveable");
            Closeable = info.GetBoolean("Closeable");
            Disposable = info.GetBoolean("Disposable");
            Type = info.GetInt32("Type");
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Version", 1);
            info.AddValue("Location", Location);
            info.AddValue("Moveable", Moveable);
            info.AddValue("Closeable", Closeable);
            info.AddValue("Disposable", Disposable);
            info.AddValue("Type", Type);
        }
    }
}