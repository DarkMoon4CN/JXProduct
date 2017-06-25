using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    public class UserInfo
    {
        public int UID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string GUID1 { get; set; }

        public string GUID2 { get; set; }


        public DateTime LastLoginTime { get; set; }

        private int _sourceID;
        public int SourceID
        {
            get
            {
                if (_sourceID < 0)
                {
                    _sourceID = 0;
                }
                return _sourceID;
            }
            set
            {
                _sourceID = value;
            }
        }

        public short Rank { get; set; }

        public DateTime RankTime { get; set; }

        public int Score { get; set; }


        public int ScoreFreeze { get; set; }

        public decimal ConsumeTotal { get; set; }

        public decimal Remaining { get; set; }

        public decimal RemainingFreeze { get; set; }

        public int LoginCount { get; set; }


        public short Status { get; set; }

        public string InvitePara { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }


        public string LastUpdater { get; set; }

        public string UnionIden { get; set; }

        public short IsClaim { get; set; }

        public int EmployeeID { get; set; }

        public DateTime ClaimTime { get; set; }


        public int TodayLoginCount { get; set; }

        public decimal RebateFee { get; set; }

        public short PurchaseType { get; set; }

        public short AgeRange { get; set; }
    }
}
