﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhotoMeasureCalibrated {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.11.0.0")]
    internal sealed partial class SettingsWpf : global::System.Configuration.ApplicationSettingsBase {
        
        private static SettingsWpf defaultInstance = ((SettingsWpf)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new SettingsWpf())));
        
        public static SettingsWpf Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("HR")]
        public string Creator {
            get {
                return ((string)(this["Creator"]));
            }
            set {
                this["Creator"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7")]
        public double Eichungsdistanz {
            get {
                return ((double)(this["Eichungsdistanz"]));
            }
            set {
                this["Eichungsdistanz"] = value;
            }
        }
    }
}