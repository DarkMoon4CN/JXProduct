using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Domain
{
    public class AccountBalanceInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int accountID { get; set; }

        /// <summary>
        /// 用户通行证(账号名)
        /// </summary>
        public int account { get; set; }    

        /// <summary>
        /// 存入金额，负值为支出
        /// </summary>
        public decimal deposit { get; set; } 

        /// <summary>
        /// 当前余额
        /// </summary>
        public decimal remainingSum { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public long? createTime { get; set; }  

        /// <summary>
        /// 备注
        /// </summary>
        public string remarks { get; set; }   

        /// <summary>
        /// 审核状态(0: 未审核1:审核通过)
        /// </summary>
        public int status { get; set; }  
        /// <summary>
        /// 更新时间
        /// </summary>
        public long? updateTime { get; set; }   

        /// <summary>
        /// 订单号
        /// </summary>
        public long orderID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
    }
}
