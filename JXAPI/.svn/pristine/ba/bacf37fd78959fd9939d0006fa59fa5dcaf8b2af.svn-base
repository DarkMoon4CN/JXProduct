using JXAPI.JXSdk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace JXAPI.JXSdk.Response
{
    public class ShoppingCardValidateResponse : Venus.VenusClientResponse
    {
       [XmlElement("card")]
        public ShoppingCardInfo card { get; set; }

       [XmlElement("code")]
       public int code { get; set; }

       [JsonProperty("message")]
       public string ErrMsg { get; set; }
    }
}
