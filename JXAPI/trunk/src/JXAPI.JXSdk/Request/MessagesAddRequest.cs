
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Venus.Utils;

namespace JXAPI.JXSdk.Response
{
    public class MessagesAddRequest : Venus.IVenusRequest<MessagesAddResposne>
    {
        #region 参数
        /// <summary>
        /// 用户ID
        /// </summary>
        public int userID { get; set; } 

        /// <summary>
        /// 消息类型  优惠促销=1,系统通知=2,物流通知=3,商品通知=4
        /// </summary>
        public int typeID { get; set; }

        /// <summary>
        /// 数据Id
        /// </summary>
        public string  dataID { get; set; }


        /// <summary>
        /// 数据类型
        /// </summary>
        public int dataTypeID { get;set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string contents { get; set; }

        /// <summary>
        ///  0新增
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string creator { get; set; }
        #endregion
        
        #region ITopRequest Members
        public string GetApiName()
        {
            return string.Empty;
        }

        public IDictionary<string, string> GetParameters()
        {
            return null;
        }

        public void Validate()
        {
            
        }
        #endregion
    }
}
