using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    [Serializable]
    public class ExpressNewsInfo
    {
        public int NewsID { get; set; }

        public string Author { get; set; }

        public byte Type { get; set; }

        public string Source { get; set; }

        public string Link { get; set; }

        public string Title { get; set; }

        public string Keywords { get; set; }

        public string Abstract { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        public string Creator { get; set; }

        public string Updater { get; set; }

        public DateTime UpdateTime { get; set; }

        public byte IsStick { get; set; }

        public byte IsRed { get; set; }
    }
}
