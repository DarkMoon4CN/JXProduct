using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    public class UsersScoreInfo
    {
        public int ScoreID { get; set; }

        public int UID { get; set; }

        public string UserName { get; set; }

        public int Score { get; set; }

        public short ScoreType { get; set; }


        public string OrderID { get; set; }

        public string GetMethod { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ExpiredTime { get; set; }

        public short Status { get; set; }
    }
}
