using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    [Serializable]
    public class ProductAllInfo
    {
        public int AllID { get; set; }

        public string Like { get; set; }

        public string Promotion { get; set; }

        public string Sales { get; set; }

        public string Salled { get; set; }

        public string Keywords { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public string Updater { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
