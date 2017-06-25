using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;

namespace JXAPI.Common.Utils
{
    public class EncryptHelper
    {

        /// <summary>
        /// 进行MD5转换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code">16 / 32 分别返回16位或者32位的MD5加密结果</param>
        /// <returns></returns>
        public static string ToMD5(string str, int code)
        {
            if (code == 16)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            if (code == 32)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
            return "00000000000000000000000000000000";
        }

        public static string ToMD5(string str)
        {
            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            string resultCode = System.BitConverter.ToString(result).Replace("-", "");
            return resultCode;
        }

        public static string ToMD5OnHealcare(string str)
        {
            byte[] data = System.Text.Encoding.GetEncoding("GBK").GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            string resultCode = System.BitConverter.ToString(result).Replace("-", "");
            return resultCode;
        }

        /// <summary>
        /// 获得输入字符串的md5签名，去除“-”，并转为小写格式
        /// </summary>
        /// <param name="inputstr"></param>
        /// <returns></returns>
        public static string GetMd5Sign(string inputstr)
        {
            byte[] data = System.Text.Encoding.Default.GetBytes(inputstr);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            string resultCode = System.BitConverter.ToString(result).Replace("-", "").ToLower();
            return resultCode;
        }

        #region 带密钥和加密向量加解密
        /// <summary>
        /// 加密指定字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="g1">加密key</param>
        /// <param name="g2">加密向量</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string source, Guid g1, Guid g2)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(source);
            byte[] key = g1.ToByteArray();
            byte[] iv = g2.ToByteArray();

            MemoryStream mem = new MemoryStream();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(mem, tdes.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            encStream.Write(clearBytes, 0, clearBytes.Length);
            encStream.FlushFinalBlock();

            byte[] result = new byte[mem.Length];
            Array.Copy(mem.GetBuffer(), 0, result, 0, mem.Length);
            string myResult = BitConverter.ToString(result, 0);
            return myResult;
        }

        /// <summary>
        /// 解密指定的字符串
        /// </summary>
        /// <param name="encrypt">被加密的字符串</param>
        /// <param name="strKey">解密key</param>
        /// <param name="strIV">解密向量</param>
        /// <returns>字符串原文</returns>
        public static string Decrypt(string encrypt, string strKey, string strIV)
        {
            Guid guid1 = new Guid(strKey);
            Guid guid2 = new Guid(strIV);
            byte[] key = guid1.ToByteArray();
            byte[] iv = guid2.ToByteArray();

            string[] toRecover = encrypt.Split(new char[] { '-' });
            byte[] br = new byte[toRecover.Length];
            for (int i = 0; i < toRecover.Length; i++)
            {
                br[i] = byte.Parse(toRecover[i], NumberStyles.HexNumber);
            }

            MemoryStream mem = new MemoryStream();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(mem, tdes.CreateDecryptor(key, iv), CryptoStreamMode.Write);
            encStream.Write(br, 0, br.Length);
            encStream.FlushFinalBlock();

            byte[] result = new byte[mem.Length];
            Array.Copy(mem.GetBuffer(), 0, result, 0, mem.Length);

            return Encoding.UTF8.GetString(result, 0, result.Length);
        }
        #endregion

        #region 单密钥加解密
        /// <summary> 
        /// 加密字符串
        /// </summary> 
        /// <param name="source">源字符串</param> 
        /// <param name="key">密钥</param> 
        /// <returns></returns> 
        public static string Encrypt(string source, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(source);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary> 
        /// 解密字符串
        /// </summary> 
        /// <param name="encrypt">被加密字符串</param> 
        /// <param name="key">密钥</param> 
        /// <returns></returns> 
        public static string Decrypt(string encrypt, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = encrypt.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(encrypt.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion


       
        #region DES
        ///<summary><![CDATA[字符串DES加密函数]]></summary>
        ///<param name="str"><![CDATA[被加密字符串]]></param>
        ///<param name="key"><![CDATA[密钥]]></param>
        ///<returns><![CDATA[加密后字符串]]></returns>
        public static string DESEncrypt(string str, string key)
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                provider.IV = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(str);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                StringBuilder builder = new StringBuilder();
                foreach (byte num in stream.ToArray())
                {
                    builder.AppendFormat("{0:X2}", num);
                }
                stream.Close();
                return builder.ToString();
            }
            catch (Exception) { return "xxxx"; }
        }
        ///<summary><![CDATA[字符串DES解密函数]]></summary>
        ///<param name="str"><![CDATA[被解密字符串]]></param>
        ///<param name="key"><![CDATA[密钥]]></param>
        ///<returns><![CDATA[解密后字符串]]></returns>
        public static string DESDecrypt(string str, string key)
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                provider.IV = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] buffer = new byte[str.Length / 2];
                for (int i = 0; i < (str.Length / 2); i++)
                {
                    int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
                    buffer[i] = (byte)num2;
                }
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                stream.Close();
                return Encoding.GetEncoding("UTF-8").GetString(stream.ToArray());
            }
            catch (Exception) { return ""; }
        }
        #endregion
    }

}
