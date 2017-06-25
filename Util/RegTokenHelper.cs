using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JXUtil
{
    public class RegTokenHelper
    {
        /// <summary>
        /// 查找 token
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static bool findToken(string key)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request["Token"])) 
                return false;

            if (HttpContext.Current.Session[key] == null)
                return false;

            return HttpContext.Current.Session[key].ToString().Contains(HttpContext.Current.Request["Token"].ToString());
        }

        /// <summary>
        /// 获取新 token 
        /// </summary>
        /// <returns></returns>
        public static string generateToken(string key)
        {
            var token = Guid.NewGuid().ToString("N").ToUpper();
            HttpContext.Current.Session[key] = token;
            return token;
        }

        /// <summary>
        /// 删除 token
        /// </summary>
        public static void  deleteToken(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
    }
}
