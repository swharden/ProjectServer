﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AbfBrowser.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AbfBrowser.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;/body&gt;
        ///&lt;/html&gt;.
        /// </summary>
        internal static string bot {
            get {
                return ResourceManager.GetString("bot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;html&gt;
        ///&lt;head&gt;
        ///    &lt;title&gt;phpABF&lt;/title&gt;
        ///&lt;/head&gt;
        ///&lt;frameset cols = &quot;300, 2*&quot;&gt;
        ///    &lt;frame name=&quot;menu&quot; src=&quot;~URL1~&quot; frameborder=&quot;0&quot; /&gt;
        ///    &lt;frame name=&quot;content&quot; src=&quot;~URL2~&quot; frameborder=&quot;0&quot; /&gt;
        ///&lt;/frameset&gt;
        ///&lt;/html&gt;.
        /// </summary>
        internal static string frames {
            get {
                return ResourceManager.GetString("frames", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;h1&gt;HOME&lt;/h1&gt;
        ///&lt;li&gt;&lt;a href=&apos;?display=frames&apos;&gt;frames&lt;/a&gt;
        ///&lt;li&gt;&lt;a href=&apos;?display=menu&amp;action=scanFolderFast&amp;path=D:\demoData\abfs-2019&apos;&gt;menu&lt;/a&gt;.
        /// </summary>
        internal static string home {
            get {
                return ResourceManager.GetString("home", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to body {
        ///	background-color: #EEE;
        ///}.
        /// </summary>
        internal static string style {
            get {
                return ResourceManager.GetString("style", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;html&gt;
        ///  &lt;head&gt;
        ///    &lt;title&gt;~TITLE~&lt;/title&gt;
        ///  &lt;/head&gt;
        ///  &lt;style&gt;
        ///    ~CSS~
        ///  &lt;/style&gt;
        ///&lt;body&gt;.
        /// </summary>
        internal static string top {
            get {
                return ResourceManager.GetString("top", resourceCulture);
            }
        }
    }
}