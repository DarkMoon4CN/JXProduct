using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using log4net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using JXAPI.Component.Model;
using JXAPI.Component.BLL;
using JXAPI.Common;
using JXAPI.Common.Utils;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Net;
namespace JXAPI.ConsoleAppSpider
{
    public class Spider
    {
         private static ILog myLog = log4net.LogManager.GetLogger(typeof(Spider));
         private SpiderType type = SpiderType.金象产品页面探知;
         private static string path ="NotExist.txt";
         public Spider() { }
        
         public void Init() 
         {
             //log4net初始化
             log4net.Config.XmlConfigurator.Configure();
             Log(string.Format("页面抓取开始！时间:{0}", System.DateTime.Now));
             if (type == SpiderType.壹药店商品抓取)
             {
                 //获取目标站点数据
                 GetWebSiteForYiYaoWang();
             }
             else if (type == SpiderType.金象产品页面探知) 
             {
                 GetWebSiteForJinXiang();
                 GetAppointWebSiteForJinXiang();
             }

             Log(string.Format("页面抓取完毕！时间:{0}", System.DateTime.Now));
             Log(string.Format("==========================================="));
         }

         #region www.111.com.cn
         #region  抓取逻辑
         public void  GetWebSiteForYiYaoWang()
         {
             //用于执行页面的分页计数器
             int SNUMBER = 0; 
         
             //获取目标分类地址数据
             IList<string> categoryURLList = GetURLList();

             //网页获取辅助类
             HttpHelper helper = new HttpHelper();

             #region 商品分类采集
             //遍历分类 
             for (int i = 0; i < categoryURLList.Count; i++) 
             {
                 string webStieUrl = categoryURLList[i];
                 while (true) 
                 {
                     SNUMBER++;
                     string url = string.Format(webStieUrl,SNUMBER);
                     HttpItem item = new HttpItem();
                     item.URL = url;
                     item.Timeout = 10000;
                     item.ResultType = ResultType.String;
                     item.UserAgent = "Baiduspider+(+http://www.baidu.com/search/spider.htm)";//伪装百度爬虫名
                     HttpResult result = helper.GetHtml(item);

                     //没有数据时进入下一个分类
                     if (result.Html.IndexOf("itemid") == -1)
                     {
                         SNUMBER = 0;
                         break;
                     }
                     IList<SpiderProductInfo> productInfoList= AnalyticalHtml(result);
                    
                     Log("---------------------------------------------------");
                     Log("===目标：" + url + " ===");
                     Log("===已完成抓取,总条数：" + productInfoList.Count + "时间：" + System.DateTime.Now + "===");
                     if (productInfoList != null && productInfoList.Count > 0) 
                     {
                         #region 抓取单个商品的详细
                         for (int j = 0; j < productInfoList.Count; j++)
                         {
                             SpiderProductInfo spInfo = productInfoList[j];
                             HttpItem detailItem = new HttpItem();
                             detailItem.URL = spInfo.Url;
                             detailItem.Timeout = 10000;
                             detailItem.ResultType = ResultType.String;
                             detailItem.UserAgent = "Baiduspider+(+http://www.baidu.com/search/spider.htm)";
                             HttpResult detailResult = helper.GetHtml(detailItem);
                             AnalyticalDetailHtml(detailResult,spInfo);
                         }
                         #endregion
                     }
                     Log("===目标：" + url + "===");
                     Log("===已完成存储！===");
                 }
             }
             #endregion

         }
        /// <summary>
         /// 将目标商品分类页结果中的商品拆分成List 商品信息
        /// </summary>
         /// <param name="result">result.html 字符串 </param>
         /// <returns>返回数据集合</returns>
        private IList<SpiderProductInfo> AnalyticalHtml(HttpResult result) 
        {
            IList<SpiderProductInfo> productInfoList = new List<SpiderProductInfo>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(result.Html);
            var collection = doc.DocumentNode.SelectNodes("//div[@class='itemSearchResultCon']");
            foreach (HtmlAgilityPack.HtmlNode item in collection) //div
            {
                SpiderProductInfo spInfo = null;
                //子节点：a标签中href数据, img中的alt数据
                HtmlNodeCollection cNodes = item.ChildNodes;     
                foreach (var citem in cNodes)
                {
                    //a标签
                    if (citem.Name.ToLower() == "a")
                    {
                        spInfo = new SpiderProductInfo();
                        spInfo.SpiderPID=Convert.ToInt32(item.Attributes["itemid"].Value);
                        spInfo.Url=citem.Attributes["href"].Value;
                        HtmlNodeCollection ccNodes = citem.ChildNodes;
                        foreach (var ccItem in ccNodes)
                        {
                            //img标签
                            if (ccItem.Name == "img") 
                            {
                                spInfo.ProductName = ccItem.Attributes["alt"].Value;
                            }
                        }
                    }     
                }
                if (spInfo != null)
                {
                    productInfoList.Add(spInfo);
                }
            }
            return productInfoList;
        }

        /// <summary>
        /// 进入商品详情页后，解析出商品说明书Table,写入数据库
        /// </summary>
        /// <param name="detailResult">result.html 字符串</param>
        /// <param name="spInfo">包含 ID　name  url</param>
        private void AnalyticalDetailHtml(HttpResult detailResult, SpiderProductInfo spInfo)
        {
            IList<SpiderProductInfo> spInfoList = new List<SpiderProductInfo>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(detailResult.Html);
            var nameCollection = doc.DocumentNode.SelectNodes("//div[@class='middle_property']");//获取名字
            if (nameCollection == null)
            {
                nameCollection = doc.DocumentNode.SelectNodes("//div[@class='middle_property middle_propertyO2o']");//获取名字
                if (nameCollection == null)
                {
                    return;
                }
            }

            spInfo.ProductName= nameCollection[0].ChildNodes["h1"].InnerHtml; //h1中内嵌了其他标签
            if (spInfo.ProductName.IndexOf("<") != -1) 
            {
                spInfo.ProductName = spInfo.ProductName.Split('<')[0];
            }

            var collection = doc.DocumentNode.SelectNodes("//table[@class='specificationBox']");
            if (collection == null)
            {
                return;
            }
            foreach (HtmlAgilityPack.HtmlNode tableItem in collection) //table
            {
                HtmlNodeCollection trNodes = tableItem.ChildNodes["tbody"].ChildNodes; //tr 节点
                foreach (var trItem in trNodes) 
                {
                    if (trItem.ChildNodes.Count == 1)
                    {
                        continue;
                    }
                    string keyName = trItem.ChildNodes["th"].InnerText.Trim();
                    string keyValue = trItem.ChildNodes["td"].InnerHtml.Trim();
                    SpiderProductInfo spInfoFlag = new SpiderProductInfo();
                    spInfoFlag.SpiderPID = spInfo.SpiderPID;
                    spInfoFlag.ProductName = spInfo.ProductName;
                    spInfoFlag.Url = spInfo.Url;
                    spInfoFlag.KeyName = keyName;
                    spInfoFlag.KeyValue = keyValue;
                    spInfoList.Add(spInfoFlag);
                }
            }
            if (spInfoList != null && spInfoList.Count !=0) 
            {
               var oResult = SpiderBLL.Instance.InsertSpider(spInfoList);
               if (oResult.ResultType != OperationResultType.Success)
               {
                   Log(string.Format("写入异常：数据ID {0},异常内容：{1}", spInfo.SpiderPID, oResult.Message));
               }
               else
               {
                   Console.WriteLine(string.Format("数据ID:{0},名称:{1}, 已完成添加",spInfo.SpiderPID,spInfo.ProductName));
               }
            }
        }
         #endregion

         /// <summary>
        ///  返回出 www.111.com.cn的一级目录 注：页数为站位符
        /// </summary>
        /// <returns></returns>
         private IList<string> GetURLList() 
        {
            IList<string> categoryURLList = new List<string>();
            categoryURLList.Add("http://www.111.com.cn/list/953710-0-0-0-0-0-0-{0}.html");//中西药
            categoryURLList.Add("http://www.111.com.cn/list/964106-0-0-0-0-0-0-{0}.html");//营养保健
            categoryURLList.Add("http://www.111.com.cn/list/962285-0-0-0-0-0-0-{0}.html");//维生素
            categoryURLList.Add("http://www.111.com.cn/list/960464-0-0-0-0-0-0-{0}.html");//隐形眼镜
            categoryURLList.Add("http://www.111.com.cn/list/107-0-0-0-0-0-0-{0}.html");//医疗器械
            categoryURLList.Add("http://www.111.com.cn/list/955306-0-0-0-0-0-0-{0}.html");//成人用品
            categoryURLList.Add("http://www.111.com.cn/list/971495-0-0-0-0-0-0-{0}.html");//母婴专区
            categoryURLList.Add("http://www.111.com.cn/list/955405-0-0-0-0-0-0-{0}.html");//美妆
            categoryURLList.Add("http://www.111.com.cn/list/103-0-0-0-0-0-0-{0}.html");//荣生花茶
            categoryURLList.Add("http://www.111.com.cn/list/964286-0-0-0-0-0-0-{0}.html");//体检医疗
            return categoryURLList;
        }
         #endregion

         #region www.jxdyf.com 金象产品页面探知
         public void GetWebSiteForJinXiang()
         {
             int startCount = 1;
             int endCount = 0;
             int size = 100;
             int SNUMBER = 0;//计数器
             string url = "http://www.jxdyf.com/product/{0}.html";

             //网页获取辅助类
             HttpHelper helper = new HttpHelper();
             while (true)
             {
                 //获取商品列表
                 startCount = SNUMBER * size + 1;
                 endCount =(SNUMBER+1) * size;
                 OperationResult<IList<int>> result = SpiderBLL.Instance.ProductID_GetList(startCount, endCount);
                 if (result == null || result.AppendData == null || result.AppendData.Count == 0)
                 {
                     //当没有数据时结束
                     break;
                 }
                 IList<int> productIDList = result.AppendData;
                 for (int i = 0; i < productIDList.Count; i++)
                 {
                     HttpItem item = new HttpItem();
                     item.URL = string.Format(url, productIDList[i]);
                     item.Timeout = 10000;
                     item.ResultType = ResultType.String;
                     item.UserAgent = "Baiduspider+(+http://www.baidu.com/search/spider.htm)";
                     HttpResult detailResult = helper.GetHtml(item);
                     bool isExist = ExistJinXiangDetailHtml(detailResult);
                     if (isExist == false)
                     {
                         //写入文件
                         WriterFile(productIDList[i]);
                         Console.WriteLine("ID:{0} 已写入文件/bin/Debug/NotExist.txt中，请核对！", productIDList[i]);
                     }
                     else 
                     {
                         Console.WriteLine("ID:{0} 正常打开",productIDList[i]);
                     }
                 }
                 SNUMBER++;
             }
         }


        /// <summary>
        /// 验证金象大药房详情页是否存在
        /// </summary>
        /// <param name="result">result.html 字符串</param>
        /// <returns> true 存在  false 不存在</returns>
        public bool ExistJinXiangDetailHtml(HttpResult detailResult) 
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(detailResult.Html);
            var nameCollection = doc.DocumentNode.SelectNodes("//div[@class='name']");//获取名字样式
            if (nameCollection == null) 
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 将商品ID写入到文件中
        /// </summary>
        /// <param name="productId"></param>
        public void WriterFile(int productId)
        {
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.WriteLine(productId + "\n");
                sw.Close();
                fs.Close();
            }
            catch (Exception e) 
            {
                Log(e.Message);
                sw.Dispose();
                fs.Dispose();
            }
        }
        #endregion

         #region www.jxdyf.com 网站页面指定抓取

        /// <summary>
        ///  www.jxdyf.com 网站页面指定抓取
        /// </summary>
        public void GetAppointWebSiteForJinXiang() 
        {
           HttpHelper helper = new HttpHelper();
           IList<SpiderInfo> spiderInfoList= GetSpiderConfigInfo();
           for (int i = 0; i < spiderInfoList.Count; i++)
           {
               SpiderInfo spiderInfo = spiderInfoList[i];
               for (int j = 0; j < spiderInfo.UrlList.Count; j++)
               {
                   HttpItem item = new HttpItem();
                   item.URL = spiderInfo.UrlList[j];
                   item.Timeout = 10000;
                   item.ResultType = ResultType.String;
                   item.UserAgent = "Baiduspider+(+http://www.baidu.com/search/spider.htm)";
                   HttpResult detailResult = helper.GetHtml(item);
                   if (detailResult.StatusCode != HttpStatusCode.OK)
                   {
                       int status = (int)detailResult.StatusCode;
                       if (status == 0)
                       {
                           //发送给李传俊 服务器断网
                           SMSHelper.Send(string.Format("金象网页异常状态:{0},地址:{1}", detailResult.StatusDescription, item.URL), "13718179873");
                       }
                       else 
                       {
                           //发送给配置文件指定手机
                           SMSHelper.Send(string.Format(spiderInfo.Name + " 你好,金象网页异常状态:{0},地址:{1}", detailResult.StatusDescription, item.URL), spiderInfo.Phone);
                       }
                       return;
                   }
                   bool isExist = ExistAppointJinXiangDetailHtml(spiderInfo.Template, detailResult);
                   if (isExist == false)
                   {
                       //发送短信
                       Log(string.Format("已向 责任人:{0} 手机:{1}发送了",spiderInfo.Name,spiderInfo.Phone));
                       Log(string.Format("目标地址:{0} 异常信息", item.URL));
                       SMSHelper.Send(string.Format("页面访问正常,但页面缺少必要的数据！,地址:{0}",  item.URL), spiderInfo.Phone);
                       break;
                   }
                   else
                   {
                       Console.WriteLine("责任人:{0},目标地址:{1} 正常打开", spiderInfo.Name,item.URL);
                   }
               }
           }
        }

        /// <summary>
        /// 验证金象网站是否正确打开
        /// </summary>
        /// <param name="template">验证类型</param>
        /// <param name="detailResult">result.html 字符串</param>
        /// <returns>true 存在  false 不存在</returns>
        public bool ExistAppointJinXiangDetailHtml(int template, HttpResult detailResult)
        {
            if ((SpiderTemplate)template == SpiderTemplate.logo)
            {
                if (detailResult.Html.IndexOf("images/logo.png") != -1 
                        && detailResult.Html.IndexOf("images/maintaincePic.jpg")==-1)
                {
                    return true;
                }
            }
            else if ((SpiderTemplate)template == SpiderTemplate.json)
            {
                if (detailResult.Html.IndexOf("[{") != -1 && detailResult.Html.IndexOf("}]") != -1)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
 



         #region 获取WebConfig.Spides下的某个子节点
        /// <summary>
        ///  获取WebConfig.Spides下的某个子节点
        /// </summary>
        /// <returns></returns>
        public IList<SpiderInfo> GetSpiderConfigInfo()
        {
            IList<SpiderInfo> spiderInfoList = new List<SpiderInfo>();
            var imagesSection = ConfigurationManager.GetSection("spiders") as XmlNode;
            XmlNodeList gradesNode = imagesSection.ChildNodes;
            for (int i = 0; i < gradesNode.Count; i++)
            {
                SpiderInfo spiderInfo = new SpiderInfo();
                List<string> urlList = new List<string>();
                XmlNode spiderNode = gradesNode[i];
                for (int j = 0; j < spiderNode.Attributes.Count; j++)
                {
                    string name = spiderNode.Attributes[j].Name;
                    string value = spiderNode.Attributes[name].Value;
                    switch (name)
                    {
                        case "name":
                            spiderInfo.Name = value; break;
                        case "phone":
                            spiderInfo.Phone = value.Trim(); break;
                        case "template":
                            spiderInfo.Template = Convert.ToInt32(value.Trim()); break;
                    }
                }
                XmlNodeList childNode = spiderNode.ChildNodes;
                for (int m = 0; m  < childNode.Count; m ++)
                {
                    XmlNode pageNode = childNode[m];
                    for (int n = 0; n < pageNode.Attributes.Count; n++)
                    {
                        string name = pageNode.Attributes[n].Name;
                        string value = pageNode.Attributes[name].Value;
                        switch (name)
                        {
                            case "url":
                                urlList.Add(value); break;
                        }
                    }
                }
                spiderInfo.UrlList = urlList;
                spiderInfoList.Add(spiderInfo);
            }
            return spiderInfoList;
        }
        #endregion

         #region 执行输出与日志记录
        /// <summary>
        /// 执行输出与日志记录
        /// </summary>
        /// <param name="message"></param>
        private void Log(string message)
        {
            myLog.InfoFormat(message);
            Console.WriteLine(message);
        }
        #endregion
    }

    #region 配置文件实体
    public class SpiderInfo
    {
        /// <summary>
        ///  消息接收者
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 判定接收类型
        /// </summary>
        public int Template { get; set; } 

        /// <summary>
        /// 页面集合
        /// </summary>
        public List<string> UrlList { get; set; }
    }

    #endregion

    #region 枚举类
    public enum SpiderType 
    {
        金象产品页面探知=1,
        壹药店商品抓取=2,
    }

    public enum SpiderTemplate 
    {
         logo=1,    //遍历页面是否有logo图片
         json=2     //判定返回的数据是否是json
    }
    #endregion
}
