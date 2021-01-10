using System;

namespace GumpStudio.Plugins
{
    [Serializable]
    public class PluginInfo : IEquatable<PluginInfo>
    {
        public string AuthorEmail;
        public string AuthorName;
        public string Description;
        public string PluginName;
        public string Version;

        public bool Equals(PluginInfo info)
        {
            return AuthorEmail == info?.AuthorEmail
                   && AuthorName == info?.AuthorName
                   && Description == info?.Description
                   && PluginName == info?.PluginName
                   && Version == info?.Version;
        }

        public override string ToString()
        {
            return PluginName;
        }
    }
}
