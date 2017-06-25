using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Domain
{
    public class UserInfo
    {
        public string passport { get; set; }
        public string trueName { get; set; }
        public int? sex { get; set; }
        public int? ageRange { get; set; }
        public int sourceID { get; set; }
        public int passportID { get; set; }
        public int purchaseType { get; set; }
        
    }
}
