using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JXProduct.ConfigComponent
{
    public class ImagesConfig : ConfigurationSection
    {
        public static ImagesConfig Instance
        {
            get
            {
                return ConfigurationManager.GetSection("images") as ImagesConfig;
            }
        }

        [ConfigurationProperty("temppath", IsRequired = true)]
        public string TempPath
        {
            get
            {
                return this["temppath"].ToString();
            }
        }

        [ConfigurationProperty("isdeleted", IsRequired = true)]
        public bool IsDeleted
        {
            get
            {
                return bool.Parse(this["isdeleted"].ToString());
            }
        }

        [ConfigurationProperty("rules", IsRequired = true)]
        public string Rules
        {
            get
            {
                return this["rules"].ToString();
            }
        }

        [ConfigurationProperty("productimage", IsRequired = true)]
        public ProductImageCollection ProductImage
        {
            get { return this["productimage"] as ProductImageCollection; }
        }
    }

    public class ProductImageCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SizeMapping();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SizeMapping)element).Key;
        }

        [ConfigurationProperty("savepath", IsRequired = true)]
        public string SavePath
        {
            get
            {
                return this["savepath"].ToString();
            }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string URL
        {
            get
            {
                return this["url"].ToString();
            }
        }

        [ConfigurationProperty("size", IsRequired = true)]
        public int Size
        {
            get
            {
                return (int)this["size"];
            }
        }

        [ConfigurationProperty("watermark", IsRequired = true)]
        public string WaterMark
        {
            get
            {
                return this["watermark"].ToString();
            }
        }

        [ConfigurationProperty("filename", IsRequired = true)]
        public string FileName
        {
            get
            {
                return (string)this["filename"];
            }
        }

        public SizeMapping this[int index]
        {
            get
            {
                return this.BaseGet(index) as SizeMapping;
            }
        }

    }

    public class SizeMapping : ConfigurationElement
    {
        [ConfigurationProperty("key")]
        public string Key
        {
            get
            {
                return (string)this["key"];
            }
        }

        [ConfigurationProperty("size")]
        public int Size
        {
            get
            {
                return (int)this["size"];
            }
        }

        [ConfigurationProperty("suffix")]
        public string Suffix
        {
            get
            {
                return (string)this["suffix"];
            }
        }
    }

}
