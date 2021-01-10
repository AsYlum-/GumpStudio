using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace GumpStudio.Classes
{
    [CompilerGenerated]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed class AppSettings : ApplicationSettingsBase
    {
        private static readonly AppSettings _mDefaultInstance = (AppSettings)Synchronized(new AppSettings());

        [DefaultSettingValue("1.05")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public Decimal ArrowKeyAccelerationRate
        {
            get => Convert.ToDecimal(this[nameof(ArrowKeyAccelerationRate)]);
            set => this[nameof(ArrowKeyAccelerationRate)] = value;
        }

        [DefaultSettingValue("")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public string ClientPath
        {
            get => Convert.ToString(this[nameof(ClientPath)]);
            set => this[nameof(ClientPath)] = value;
        }

        public static AppSettings Default
        {
            get
            {
                AppSettings defaultInstance = _mDefaultInstance;
                return defaultInstance;
            }
        }

        [DefaultSettingValue("920, 602")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public Size DesignerFormSize
        {
            get => (Size)this[nameof(DesignerFormSize)];
            private set => this[nameof(DesignerFormSize)] = value;
        }

        [DefaultSettingValue("25")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public int UndoLevels
        {
            get => Convert.ToInt32(this[nameof(UndoLevels)]);
            private set => this[nameof(UndoLevels)] = value;
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        [UserScopedSetting]
        public bool UsePixelPerfectSelection
        {
            get => Convert.ToBoolean(this[nameof(UsePixelPerfectSelection)]);
            set => this[nameof(UsePixelPerfectSelection)] = value;
        }
    }
}
