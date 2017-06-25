using JXAPI.JXSdk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JXAPI.JXSdk.Response
{
    public class ShoppingCardListResponse :Venus.VenusClientResponse
    {
        [XmlElement("shoppingCards")]
        public IList<ShoppingCardInfo> list { get; set; }

        [XmlElement("code")]
        public int code { get; set; }

        [XmlElement("count")]
        public int count { get; set; }
    }
}
