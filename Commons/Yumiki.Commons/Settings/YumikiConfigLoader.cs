using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Settings
{
    /// <summary>
    /// Configuration under web.config.
    /// </summary>
    public class YumikiConfigLoader : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public YumikiCollection Instances
        {
            get { return (YumikiCollection)this[""]; }
            set { this[""] = value; }
        }

        private static YumikiConfigLoader configuration;
        public static YumikiConfigLoader GetInstance()
        {
            if (configuration == null)
            {
                configuration = ConfigurationManager.GetSection("yumiki") as YumikiConfigLoader;
                foreach (YumikiElement node in configuration.Instances)
                {
                    try
                    {
                        PropertyInfo property = typeof(CustomConfigurations).GetProperty(node.Name);
                        property.SetValue(null, node.Value);
                    }
                    catch
                    {
                        //To Do: Log
                    }
                }
            }
            return configuration;
        }
    }

    public class YumikiCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new YumikiElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((YumikiElement)element).Name;
        }
    }

    public class YumikiElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string)base["value"]; }
            set { base["value"] = value; }
        }
    }
}
