using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Yumiki.Web.Master.App_Config
{
    /// <summary>
    /// Configuration under web.config.
    /// </summary>
    public class YumikiConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public YumikiCollection Instances
        {
            get { return (YumikiCollection)this[""]; }
            set { this[""] = value; }
        }

        ////Sample Code
        //YumikiConfiguration yumiki = ConfigurationManager.GetSection("yumiki") as YumikiConfiguration;
        //foreach (YumikiElement node in yumiki.Instances)
        //{

        //}
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
        public string Type
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }
    }
}