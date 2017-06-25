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
        /// <returns>true:�ɹ� false:ʧ��</returns>
        public bool Section_Update(SectionInfo sectionInfo)
        {
            return dal.Section_Update(sectionInfo);
        }

        /// <summary>
        /// Section_Delete Method
        /// </summary>
        /// <param name="sectionID">Section Main ID</param>
        /// <returns>true:�ɹ� false:ʧ��</returns>	
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
        /// <param name="pageIndex">��ʼҳ��</param>
        /// <param name="pageSize">ÿҳ������</param>
        /// <param name="orderType">��������'':û������Ҫ�� 0���������� 1���������� �ַ������û��Զ���������� �磺��SubmitTime DESC , ID DESC��</param>
        /// <param name="strWhere">��ѯ����(ע��: ��Ҫ�� WHERE)</param>
        /// <param name="recordCount">�ܼ�¼��</param>
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

        //#region KeyWord ����

        ////��ȡ�ؼ���
        ///// <summary>
        ///// �������ҹؼ���ID
        ///// </summary>
        //public List<int> Section_GetKeywordIDsBySectionID(int sectionid)
        //{
        //    return dal.Section_GetKeywordIDsBySectionID(sectionid);
        //}

        ////��ӹؼ���
        ///// <summary>
        ///// ��ӿ���->�ؼ��ʶ�Ӧ��ϵ
        ///// </summary>
        //public bool Section_AddKeyword(int sectionid, int keywordid)
        //{
        //    return dal.Section_AddKeyword(sectionid, keywordid);
        //}
        ///// <summary>
        /////  ������ӹؼ��ֹ���
        ///// </summary>
        //public bool Section_AddKeyword(int sectionid, string ChineseName, string PinyinName, string FirstLetter, string Creator)
        //{
        //    return dal.Section_AddKeyword(sectionid, ChineseName, PinyinName, FirstLetter, Creator);
        //}
        ////ɾ���ؼ���
        //public bool Section_DelKeyword(int sectionid, int keywordid)
        //{
        //    return dal.Section_DelKeyword(sectionid, keywordid);
        //}
        //#endregion
    }
}
