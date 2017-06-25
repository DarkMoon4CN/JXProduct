using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace JXAPI.JXSdk.Utils
{
    public class ImageConvertUtil
    {

        /// <summary>
        ///  将图片文件转换成Base64
        /// </summary>
        /// <param name="imageFileStream">图片流</param>
        /// <returns></returns>
        public static string ConvertImageToBase64(Stream imageFileStream)
        {
            string byte64String = string.Empty;
            BinaryReader br = new BinaryReader(imageFileStream);

            byte[] bytes = br.ReadBytes((Int32)imageFileStream.Length);
            byte[] buffer = new byte[imageFileStream.Length];
            imageFileStream.Read(buffer, 0, buffer.Length);
            imageFileStream.Close();
            byte64String = Convert.ToBase64String(bytes);
            return byte64String;
        }

        /// <summary>
        /// 将Base64编码文本转换成Byte[]
        /// </summary>
        /// <param name="base64">Base64编码文本</param>
        /// <returns></returns>
        public static Byte[] FromBase64ToByte(string base64)
        {
            char[] charBuffer = base64.ToCharArray();
            byte[] bytes = Convert.FromBase64CharArray(charBuffer, 0, charBuffer.Length);
            return bytes;
        }
    }
}
