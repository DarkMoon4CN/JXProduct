using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    [Serializable]
    public class ClassificationInfo
    {
        public int CFID { get; set; }

        public string ChineseName { get; set; }

        public string PinyinName { get; set; }

        public string ImagesLogo { get; set; }

        public short Level { get; set; }

        public string Path { get; set; }

        public int ParentID { get; set; }

        public short PathCount { get; set; }

        public string Title { get; set; }

        public string Keywords { get; set; }

        public string Description { get; set; }

        public int Sort { get; set; }

        public short Status { get; set; }

        public short IsTop { get; set; }

        public short IsChannelHot { get; set; }

        public short IsKeyParent { get; set; }

        public int keywordID { get; set; }
    }
}
