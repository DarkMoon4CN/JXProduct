using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    public class UsersAccountInfo
    {
        public int Ident { get; set; }

        public int UID { get; set; }

        public decimal Deposit { get; set; }

        public decimal RemainingSum { get; set; }

        public string Creator { get;set; }
        

        public DateTime CreateTime { get;set; }

        public string Remarks { get; set; }

        public short Status { get; set; }

        public DateTime UpdateTime { get;set;}

        public string OrderID { get; set; }
    }
}
