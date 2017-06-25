using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class MvcExtensions
    {
        /// <summary>
        ///  下拉框
        /// </summary>
        /// <param name="textname">名称</param>
        /// <param name="inputname">input的名字</param>
        /// <param name="list">数据列表</param>
        /// <param name="defaultvalue">默认值</param>
        /// <param name="haszero">是否加上请选择</param>
        public static IHtmlString Select1(this HtmlHelper html, string textname, string inputname, Dictionary<string, string> list, string defaultvalue, bool haszero = false)
        {
            var str = new StringBuilder();
            str.Append("<div class='selecWrap'>");
            str.Append("<div class='titleSelecWrap'>");
            str.AppendFormat("<span>{0}</span>", !string.IsNullOrEmpty(textname) ? textname + "：" : string.Empty);
            str.Append("<div class='imitationSelect fl'>");
            str.Append("<div class='selectTitle'><span class='selectTitleLeft'>请选择</span><span class='selectTitleRight'></span></div>");
            str.Append("<div class='imitaSelectList'>");
            str.Append("<ul>");
            if (haszero)
            {
                str.Append("<li><a href='#' t='0' >请选择</a></li>");
            }
            if (list != null)
                foreach (var l in list)
                {
                    str.Append("<li><a href='#' t='" + l.Key + "' >" + l.Value + "</a></li>");
                }
            str.Append("</ul>");
            str.Append("</div>");
            str.Append("</div>");
            str.Append("</div>");
            str.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", inputname, defaultvalue);
            str.Append("</div>");
            return html.Raw(str.ToString());
        }
    }
}