using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JXAPI.Common.Utils
{
    public static class StringUtil
    {
        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60)
                return dt.ToShortDateString();
            else if (span.TotalDays > 30)
                return "1个月前";
            else if (span.TotalDays > 14)
                return "2周前";
            else if (span.TotalDays > 7)
                return "1周前";
            else if (span.TotalDays > 1)
                return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            else if (span.TotalHours > 1)
                return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            else if (span.TotalMinutes > 1)
                return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            else if (span.TotalSeconds >= 1)
                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            else
                return "1秒前";
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="c">截取多少位</param>
        public static string SubStr(this string s, int c)
        {
            if (string.IsNullOrWhiteSpace(s))
                return string.Empty;
            if (c < s.Length)
                return s.Substring(0, c) + "...";
            return s;
        }

        /// <summary>
        /// 判断是不是数字
        /// </summary>
        public static bool IsInt(this string num)
        {
            if (string.IsNullOrEmpty(num))
                return false;
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[0-9]+$");
            return r.IsMatch(num);
        }
        
        /// <summary>
        /// 过滤搜索关键字
        /// </summary>
        /// <param name="inputstr"></param>
        /// <returns></returns>
        public static string FilterSearchKeyword(string inputstr)
        {
            if (!string.IsNullOrEmpty(inputstr))
            {
                StringBuilder pattern = new StringBuilder("不能|有效|理想|控制|本品|引起|病人|采用|并用|减轻|协同|功效|辅料|用于|每支|每克");
                pattern.Append("|更能|运动|成份|单独|缓解|普通|替代|应用|联合|毫克|饮食|所致|使用|上述|姜制|中度|早期|主要|症状|症见");
                pattern.Append("|治疗|证候者|流行性|局部性|混合性|为主的|轻|至|如|炒|烫|见|经|可|与|但|不|对|其|时|比|各|自|为|以|及|代偿|成年");
                pattern.Append("|的|或|等|也|含|和|克|适|急性|发作|症候着|证者|感染性|疾病|小|面积|开放性|外科|创伤|慢性|功能|衰竭|气道|相关|长期");
                pattern.Append("|是|接受|良症|术后|低下|特发性|提高|能力|促进|病体|康复|老年|体虚|初期|老年性|轻度|并发性|前|后|诸证|系统");
                pattern.Append("|更年期|综合征|需要|刺激|生效|说明书|预防|附加|药物|辅助|常规|短效|按需|很好地|长效|已得到|完全|患者|阻塞|并发性");
                pattern.Append("|注意|用药|形式|包括|成人|儿童|维持|剂量|仍有|扩张|规律|仍然|患有|重度|规格|喘息性|伴有|反应|增高|肺部|活动|复制");
                pattern.Append("|反复|妇科|病症|缩短|病程|减少|次数|感染|抗菌|基础|炎症|减缓|加速|下降|白天|夜间|收缩|季节性|常年性|过敏|诱发");
                pattern.Append("|到|地|需|吸入|皮质|激素|逆性|这|激动|剂|原发性|延缓|进展|因|而|导致|住院|全|事件|发生率|稳定型|稳定|升高|病毒");
                pattern.Append("|组成|由|成分|量|原料|标志性|每|片|配料|提|取液|取物|色素");
                pattern.Append("|[－：“”；。、，. %()（）［ ］a-zA-Z0-9]");
                string resultString = Regex.Replace(inputstr, pattern.ToString(), " ", RegexOptions.IgnoreCase);
                return Regex.Replace(resultString, "\\s{2,}", " ");
            }
            return inputstr;
        }

        /// <summary>
        /// 过滤企业名称中无效副词
        /// </summary>
        /// <param name="inputstr"></param>
        /// <returns></returns>
        public static string FilterManufacter(string inputstr)
        {
            if (!string.IsNullOrEmpty(inputstr))
            {
                StringBuilder pattern = new StringBuilder("开发区|责任|药业|制药|科技|眼睛护理|出品|制造|高科技|股份|有限|国际|品厂|委托|器械");
                pattern.Append("|公司|健康用品|连锁|乳胶|监制|设备|卫生|总部|纺织品|分包装|产业|高新|代理|研制|仪器厂|集团|控股|技术|分厂|卫生用品");
                pattern.Append("|饮片|化工|研发|仪表|药厂|开发|剂厂|中药|发展|食品|研究所|企业|材料|营销|劳保用品|医用|实业|母婴用品|服饰|药品");
                pattern.Append("|总厂|保健|生产|销售|婴儿用品|隐形眼镜|老年用品|产地|天然|化妆品|工厂|器材|产品|医疗器械|中心|制品|个人|日用|化学品");
                pattern.Append("|[（）()【】：:/+]");
                string resultString = Regex.Replace(inputstr, pattern.ToString(), " ", RegexOptions.IgnoreCase);
                return Regex.Replace(resultString, "\\s{2,}", " ");
            }
            return inputstr;
        }

        /// <summary>
        /// Lucene.Net 关键词替换
        /// </summary>
        /// <param name="instr"></param>
        /// <returns></returns>
        public static string LuceneReplace(string instr)
        {
            StringBuilder tempStr = new StringBuilder(instr);
            tempStr.Replace("+", "");
            tempStr.Replace("-", "");
            tempStr.Replace("(", "");
            tempStr.Replace(")", "");
            tempStr.Replace("*", "");
            tempStr.Replace("&", "");
            tempStr.Replace("@", "");
            tempStr.Replace("!", "");
            tempStr.Replace("~", "");
            tempStr.Replace("[", "");
            tempStr.Replace("]", "");
            tempStr.Replace("{", "");
            tempStr.Replace("}", "");
            tempStr.Replace("“", "");
            tempStr.Replace("”", "");
            tempStr.Replace("QUOTED", "");
            tempStr.Replace("TERM", "");
            tempStr.Replace("NOT", "");
            tempStr.Replace("PREFIXTERM", "");
            tempStr.Replace("WILDTERM", "");
            tempStr.Replace("NUMBER", "");
            return tempStr.ToString();
        }

        /// <summary>
        /// 匹配数字、字母
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>TRUE=数字、字母</returns>
        public static bool IsNatural_Number(string str)
        {
            Regex reg = new Regex(@"^[A-Za-z0-9]+$");
            return reg.IsMatch(str);
        }

        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="htmlString">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string NoHTML(string htmlString)
        {
            //删除脚本
            htmlString = Regex.Replace(htmlString, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            htmlString = Regex.Replace(htmlString, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"-->", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"<!--.*", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            
            return htmlString;
        }
    }
}
