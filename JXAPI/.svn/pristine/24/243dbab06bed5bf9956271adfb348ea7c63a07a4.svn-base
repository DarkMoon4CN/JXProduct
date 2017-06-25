using JXAPI.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace JXPAI.Service.Base
{
    public class Security
    {
        /// <summary>
        /// 用户获取配置文件中权限列表
        /// </summary>
        /// <returns></returns>
        public static IList<Passport> PassportConfig() 
        {
            var pps = ConfigurationManager.GetSection("passports") as XmlNode;
            XmlNodeList gradesNode = pps.ChildNodes ;
            IList<Passport> passportList=new List<Passport>();
            for (int i = 0; i < gradesNode.Count; i++)
            {
                XmlNode g= gradesNode[i];
                Passport ppt=new Passport();
                for (int j = 0; j < g.Attributes.Count; j++)
                {
                    string name = g.Attributes[j].Name;
                    string value = g.Attributes[name].Value;
                    switch (name) 
                    {
                        case "name": ppt.Name = value;break;
                        case "appid": ppt.AppId = value; break;
                        case "appkey": ppt.AppKey = value; break;
                        case "appsecret": ppt.AppSecret = value; break;
                    }
                }
                passportList.Add(ppt);
            }
            return passportList;
        }


        /// <summary>
        /// 验证合法性
        /// </summary>
        /// <param name="appId">id</param>
        /// <param name="sign">唯一标志</param>
        /// <param name="jsonData">数据</param>
        /// <returns></returns>
        public static bool SecurityCheck(string appId, string sign, string jsonData)
        {
            IList<Passport> pptList = PassportConfig();
            IList<Passport> source = (from p in pptList
                                      where p.AppId == appId
                                      select p).ToList<Passport>();
            //密码核对
            if (source.Count <= 0)
            {
                return false;
            }
            byte[] bkByte = Encoding.Default.GetBytes(source[0].AppSecret+jsonData);   
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bKey = md5.ComputeHash(bkByte);
            string bKeyStr = Convert.ToBase64String(bKey);
            if (sign.ToLower().Trim() == bKeyStr.ToLower().Trim())
            {
                return true;
            }
            return false;
        }
    }
    public class Passport 
    {
        public string Name { get; set; }
        public string AppId { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
    }



}