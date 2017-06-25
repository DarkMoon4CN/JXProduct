
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace JXAPI.Common.Utils
{
    /// <summary>
    /// Venus系统工具类。
    /// </summary>
    public sealed class SignUtil
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="postData">POST数据</param>
        /// <param name="secret">密钥</param>
        /// <returns></returns>
        public static string SignVenusRequest(string postData, string secret)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}{1}", postData, secret)));
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
    }
}
