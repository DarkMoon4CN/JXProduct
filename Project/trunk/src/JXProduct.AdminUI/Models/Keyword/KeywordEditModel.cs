using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXProduct.AdminUI.Models.Keyword
{
    public class KeywordEditModel
    {
        public int SectionID { get; set; }
        public int CFID { get; set; }
        public int KeywordID { get; set; }
        public string KeywordName { get; set; }
        public string Method { get; set; }


        /// <summary>
        /// 1 成功 2存在  3错误
        /// </summary>
        public int Result { get; set; }
        public string Title
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.KeywordName))
                {
                    return "添加";
                }
                else
                {
                    return "修改";
                }
            }
        }

        public string ResultToString
        {
            get
            {
                switch (this.Result)
                {
                    case 1:
                        return "<p class='gou'>成功标签" + this.Title + "</p>";
                    case 2:
                        return "<p class='dateBtn redColor'>对不起此关键字已存在添加项！</p>";
                    case 3:
                        return "<p class='dateBtn'>标签过长，最多支持8个汉字、24个英文字母和数字</p>";
                    default: break;
                }
                return string.Empty;
            }
        }


    }
}