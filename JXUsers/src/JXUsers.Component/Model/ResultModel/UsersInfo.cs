using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXUsers.Component.Model.ResultModel
{
    public class UsersInfo
    {
        public int UID { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
        public int Status { get; set; }
        public string Address { get; set; }
        public string TrueName { get; set; }
        public string Mobile { get; set; }
    }
}
