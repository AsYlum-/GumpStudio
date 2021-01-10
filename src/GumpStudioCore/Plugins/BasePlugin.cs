using GumpStudio.Elements;
using GumpStudio.Forms;

namespace GumpStudio.Plugins
{
    public abstract class BasePlugin
    {
        public bool IsLoaded { get; protected set; }

        public virtual string Name { get; }

        public abstract PluginInfo GetPluginInfo();

        public virtual void InitializeElementExtenders(BaseElement element)
        {
        }

        public virtual void Load(DesignerForm frmDesigner)
        {
            IsLoaded = true;
        }

        public virtual void MouseMoveHook(ref MouseMoveHookEventArgs e)
        {
        }

        public virtual void Unload()
        {
        }
    }
}
