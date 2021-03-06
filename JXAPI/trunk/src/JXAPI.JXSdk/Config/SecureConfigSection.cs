﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JXAPI.JXSdk.Config
{
    public class SecureConfigSection : ConfigurationSection
    {
        private static SecureConfigSection _Instance = null;

        public static SecureConfigSection Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = ConfigurationManager.GetSection("securevalid") as SecureConfigSection;
                }
                return _Instance;
            }
        }

        [ConfigurationProperty("ipvalid", IsRequired = true)]
        public string IPValid
        {
            get
            {
                return this["ipvalid"].ToString();
            }
        }

        [ConfigurationProperty("secures", IsDefaultCollection = true)]
        public SecureCollection Secures
        {
            get { return this["secures"] as SecureCollection; }
        }

    }

    public class SecureCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SecureElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SecureElement)element).Source;
        }

        public SecureElement this[int index]
        {
            get
            {
                return this.BaseGet(index) as SecureElement;
            }
        }
        new public SecureElement this[string Name]
        {
            get
            {
                return (SecureElement)BaseGet(Name);
            }
        }
        new public int Count
        {
            get { return base.Count; }
        }
    }

    public class SecureElement : ConfigurationElement
    {
        [ConfigurationProperty("source", IsRequired = true)]
        public string Source
        {
            get { return this["source"].ToString(); }
        }

        [ConfigurationProperty("pwd", IsRequired = true)]
        public string APPPWD
        {
            get { return this["pwd"].ToString(); }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string URL
        {
            get { return this["url"].ToString(); }
        }

        [ConfigurationProperty("ip", IsRequired = true)]
        public string IP
        {
            get { return this["ip"].ToString(); }
        }
    }
}