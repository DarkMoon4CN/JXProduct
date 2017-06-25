using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    public class UsersReceiverInfo
    {
        public int ReceiverID { get; set; }

        public int UID { get; set; }

        public string TrueName { get; set; }

        public string Province { get; set; }

        public string City { get; set; }


        public string County { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Postalcode { get; set; }


        public short IsDefault { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public short PaymentTypeID { get; set; }

        public short ShipTypeID { get; set; }


        public short ShipTimeID { get; set; }

        public short IsSplit { get; set; }

        public int HasBindFreeShipCard { get; set; }
    }
}
