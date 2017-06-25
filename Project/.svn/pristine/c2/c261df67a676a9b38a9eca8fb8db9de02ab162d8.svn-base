using System;
using System.Collections.Generic;
using JXProduct.Component.Model;
namespace JXProduct.Component.BLL
{
    /// <summary>
    /// SectionOffice 
    /// </summary>
    public class SectionBLL
    {
        private SectionBLL() { }
        private static SectionBLL _instance;

        private static readonly SQLServerDAL.SectionDAL dal = new SQLServerDAL.SectionDAL();

        public static SectionBLL Instance { get { return _instance ?? (_instance = new SectionBLL()); } }


        #region CURD

        /// <summary>
        /// Section_Insert Method
        /// </summary>
        /// <param name="SectionInfo">Section object</param>
        /// <returns>The new ID : int </returns>
        public int Section_Insert(SectionInfo sectionInfo)
        {
            return dal.Section_Insert(sectionInfo);
        }

        /// <summary>
        /// Section_Update Method		
        /// </summary>
        /// <param name="SectionInfo">Section object</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Section_Update(SectionInfo sectionInfo)
        {
            return dal.Section_Update(sectionInfo);
        }

        /// <summary>
        /// Section_Delete Method
        /// </summary>
        /// <param name="sectionID">Section Main ID</param>
        /// <returns>true:成功 false:失败</returns>	
        public bool Section_Delete(int sectionID)
        {
            return dal.Section_Delete(sectionID);
        }

        /// <summary>
        /// Section_Get Method
        /// </summary>
        /// <param name="sectionID">Section Main ID</param>
        /// <returns>SectionInfo get from Section table.</returns>	
        public SectionInfo Section_Get(int sectionID)
        {
            return dal.Section_Get(sectionID);
        }

        /// <summary>
        /// Section_GetList Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of SectionInfo</returns>
        public IList<SectionInfo> Section_GetList(int parentid)
        {
            return dal.Section_GetList(parentid);
        }
        public IList<SectionInfo> Section_GetList(string sectionname)
        {
            return dal.Section_GetList(sectionname);
        }


        #endregion

        //#region KeyWord 搜索

        ////获取关键字
        ///// <summary>
        ///// 搜索科室关键词ID
        ///// </summary>
        //public List<int> Section_GetKeywordIDsBySectionID(int sectionid)
        //{
        //    return dal.Section_GetKeywordIDsBySectionID(sectionid);
        //}

        ////添加关键字
        ///// <summary>
        ///// 添加科室->关键词对应关系
        ///// </summary>
        //public bool Section_AddKeyword(int sectionid, int keywordid)
        //{
        //    return dal.Section_AddKeyword(sectionid, keywordid);
        //}
        ///// <summary>
        /////  分类添加关键字关联
        ///// </summary>
        //public bool Section_AddKeyword(int sectionid, string ChineseName, string PinyinName, string FirstLetter, string Creator)
        //{
        //    return dal.Section_AddKeyword(sectionid, ChineseName, PinyinName, FirstLetter, Creator);
        //}
        ////删除关键字
        //public bool Section_DelKeyword(int sectionid, int keywordid)
        //{
        //    return dal.Section_DelKeyword(sectionid, keywordid);
        //}
        //#endregion
    }
}
