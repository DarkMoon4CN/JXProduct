using JXBigData.Component.BLL;
using JXBigData.Component.Model;
using JXUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JXBigData.AdminUI.Controllers
{
    public class AnalyseController : Controller
    {
        //
        // GET: /Analyse/ 搜索分析与 人群分析

        /// <summary>
        ///  SearchList 页面载体
        /// </summary>
        /// <param name="typeID">类型： 0：当天 1：昨天 2：7天 3:30天 4：自定义时间</param>
        /// <param name="sourceID">来源</param>
        /// <param name="orderType">0:热门,1:相同搜索</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public ActionResult SearchList(int typeID = 0, int sourceID = 0, int orderType = 0, string startTime = null, string endTime = null)
        {
            DateTime sTime = DateTime.Now;
            DateTime eTime = DateTime.Now;
            GetTime(startTime, endTime, typeID, ref sTime, ref eTime);

            if (orderType == 0) //查询某一Source 下的查询总量
            {
                OperationResult<IList<SearchKeywordDailyInfo>> skInfo =
                      SearchKeywordDailyBLL.Instance.SearchKeywordDaily_DefaultGet(sourceID,sTime,eTime);
                ViewBag.SKInfoList = skInfo.AppendData;
            }
            else //if(orderType==1) 查询所有总集合总量
            {
                OperationResult<IList<SearchKeywordCountInfo>> skcInfo =
                        SearchKeywordDailyBLL.Instance.SearchKeywordDaily_AllGet(sTime, eTime);
                ViewBag.SKCInfoList = skcInfo.AppendData;
            }
            ViewBag.OrderType = orderType;
            ViewBag.TypeID = typeID;
            ViewBag.SourceID = sourceID;
            ViewBag.StartTime = startTime;
            ViewBag.EndTime = endTime;
            return View();
        }

        private void GetTime(string startTime, string endTime, int typeID, ref DateTime sTime, ref DateTime eTime)
        {
            //设定当前时间和结束时间
            switch (typeID)
            {
                case 0:
                    sTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " 00:00:00");
                    eTime = eTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");
                    break;
                case 1:
                    sTime = DateTime.Parse(DateTime.Now.AddDays(-1).ToShortDateString() + " 00:00:00");
                    eTime = eTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");
                    break;
                case 2:
                    sTime = DateTime.Parse(DateTime.Now.AddDays(-7).ToShortDateString() + " 00:00:00");
                    eTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");
                    break;
                case 3:
                    sTime = DateTime.Parse(DateTime.Now.AddDays(-30).ToShortDateString() + " 00:00:00");
                    eTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");
                    break;
                case 4:
                    sTime = DateTime.Parse(startTime + " 00:00:00");
                    eTime = DateTime.Parse(endTime + " 23:59:59");
                    break;
                case 5:
                    sTime = DateTime.Parse(DateTime.Now.AddDays(-90).ToShortDateString() + " 00:00:00");
                    eTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");
                    break;
            }
        }


        public ActionResult Region() 
        {
            return View();
        }

        /// <summary>
        /// 获取省信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetProvinceData(string startTime, string endTime,int typeID) 
        {
            var result = new JsonResultObject();
            DateTime sTime = DateTime.Now;
            DateTime eTime = DateTime.Now;

            //<bei_jing,北京> 使用 TempData 进行存储
            Dictionary<string, string> dist = new Dictionary<string, string>();
            //获取出所有省的人数
            GetTime(startTime, endTime, typeID, ref sTime, ref eTime);
            OperationResult<IList<RegionDailyInfo>> rdResult=
                                              RegionDailyBLL.Instance.RegionDaily_GetProvince(sTime, eTime);
            IList<RegionDailyInfo> rdInfoList=rdResult.AppendData;

            //将北京市 转换成 bei_jing
            for (int i = 0; i <rdInfoList.Count; i++)
            {
                //去除特定的字符串
                string chineseProvince = rdInfoList[i].Province;//中文省
                string pinYinProvince = string.Empty;//拼音省
                rdInfoList[i].Province = 
                    System.Text.RegularExpressions.Regex.Replace(rdInfoList[i].Province, "[省市区街道]", "");
                if (rdInfoList[i].Province.IndexOf("山西")!=-1) 
                {
                    rdInfoList[i].Province = "shan_xi_2";
                    dist.Add(rdInfoList[i].Province, chineseProvince);
                }
                else if (rdInfoList[i].Province.IndexOf("陕西") !=-1)
                {
                    rdInfoList[i].Province = "shan_xi_1";
                    dist.Add(rdInfoList[i].Province, chineseProvince);
                }
                else 
                {
                   //将  中文 转换为 zhong_wen
                    char[] province = rdInfoList[i].Province.ToCharArray();
                    rdInfoList[i].Province = string.Empty;
                    for (int j = 0; j < province.Length; j++)
                    {
                        rdInfoList[i].Province += PinyinUtil.Get(province[j]).ToLower();
                       if (province.Length != (j + 1))
                       {
                           rdInfoList[i].Province += "_";
                       }
                    }
                }
                pinYinProvince = rdInfoList[i].Province;
                dist.Add(pinYinProvince,chineseProvince);
            }
            System.Web.HttpContext.Current.Session["Province"] = dist;
            result.data =JsonHelper.ConvertToJson(rdInfoList);
            result.status = true;
            return Json(result);
        }

        /// <summary>
        /// 根据省名获取市级数据信息
        /// </summary>
        /// <param name="provinceName"></param>
        /// <returns></returns>
        public JsonResult GetCityData(string startTime, string endTime, int typeID,string province) 
        {
            var result = new JsonResultObject();
            DateTime sTime = DateTime.Now;
            DateTime eTime = DateTime.Now;
            GetTime(startTime, endTime, typeID, ref sTime, ref eTime);
            Dictionary<string, string> dist = (Dictionary<string, string>)System.Web.HttpContext.Current.Session["Province"];

            if (dist == null || dist.Count == 0 || dist.ContainsKey(province.Trim())==false) 
            {
                result.data = null;
                result.status = true;
                return Json(result);
            }
            string chineseProvince=dist[province.Trim()];

            //查询市级所有数据
            OperationResult<IList<RegionDailyInfo>> rdResult = 
                                             RegionDailyBLL.Instance.RegionDaily_GetCity(chineseProvince, sTime, eTime);
            IList<RegionDailyInfo> rdInfoList = rdResult.AppendData;
            for (int i = 0; i < rdInfoList.Count; i++)
            {
                //去除特定的字符串
                rdInfoList[i].City =
                   System.Text.RegularExpressions.Regex.Replace(rdInfoList[i].City, "[省市区县街道]", "");

                //只抓取市的前2个字
                rdInfoList[i].City = rdInfoList[i].City.Substring(0, 2);
            }
            result.data = JsonHelper.ConvertToJson(rdInfoList);
            result.status = true;
            return Json(result);
        }

        /// <summary>
        /// 依靠 省名 查出所有 市信息
        /// </summary>
        /// <param name="province">bei_jing</param>
        /// <returns></returns>
        public JsonResult GetCityMap(string province) 
        {
            var result = new JsonResultObject();
            try
            {
                string path = Server.MapPath("/js/json/"+province+".geo.json");
                FileStream read = new FileStream(path, FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(read,   //文件流对象  
                                       Encoding.UTF8);  //字符编码  
                string jsonStr= sr.ReadToEnd();

                sr.Close();
                read.Close();
                result.data = jsonStr;
                result.status = true;
            }
            catch (IOException ex)
            {
                result.status = false;
                result.msg = "无法读取文件"+ex.Message;
            }
            return Json(result);
        }
    }
}


