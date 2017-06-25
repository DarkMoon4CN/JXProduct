﻿using JXProduct.Component.Enums;
using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class RoleMessagesBLL
    {
        private RoleMessagesBLL() { }
        private static RoleMessagesBLL _instance;
        public static RoleMessagesBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new RoleMessagesBLL());
            }
        }
        private static readonly SQLServerDAL.RoleMessagesDAL dal = new SQLServerDAL.RoleMessagesDAL();

        /// <summary>
        /// RoleMessages 分页
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="orderType">排序</param>
        /// <param name="recordCount">out 总条数</param>
        /// <returns></returns>
        public IList<RoleMessagesInfo> RoleMessages_GetList(int roleID, int status, int pageIndex, int pageSize, string orderType, out int recordCount)
        {
            var strWhere = new StringBuilder(" 1=1 ");
            switch ((RoleType)roleID)
            {
                case RoleType.质管审核:
                    strWhere.AppendFormat("  AND RoleID IN ({0},{1}) ", (int)RoleType.商品信息, (int)RoleType.商品资质);
                    break;
                default:
                    strWhere.AppendFormat(" AND RoleID={0} ", roleID);
                    break;
            }
            strWhere.AppendFormat(" AND Status ={0}", status == 1 ? 1 : 0);

            return dal.RoleMessages_GetList(pageIndex, pageSize, orderType, strWhere.ToString(), out recordCount);
        }

        public RoleMessagesInfo RoleMessages_Get(int messageID)
        {
            return dal.RoleMessages_Get(messageID);
        }

        /// <summary>
        /// RoleMessages_Insert Method
        /// </summary>
        /// <param name="RoleMessagesInfo">RoleMessages object</param>
        /// <returns>The new ID : int </returns>
        public int RoleMessages_Insert(RoleMessagesInfo roleMessagesInfo)
        {
            return dal.RoleMessages_Insert(roleMessagesInfo);
        }

        /// <summary>
        /// RoleMessages_Update Method		
        /// </summary>
        /// <param name="RoleMessagesInfo">RoleMessages object</param>
        /// <returns>true:成功 false:失败</returns>
        public bool RoleMessages_Update(RoleMessagesInfo roleMessagesInfo)
        {
            return dal.RoleMessages_Update(roleMessagesInfo);
        }

        /// <summary>
        /// RoleMessages_Delete Method
        /// </summary>
        /// <param name="messageID">RoleMessages Main ID</param>
        /// <returns>true:成功 false:失败</returns>	
        public bool RoleMessages_Delete(int messageID)
        {
            return dal.RoleMessages_Delete(messageID);
        }


        /// <summary>
        /// RoleMessages_GetList Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of RoleMessagesInfo</returns>
        public IList<RoleMessagesInfo> RoleMessages_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            return dal.RoleMessages_GetList(pageIndex, pageSize, orderType, strWhere, out recordCount);
        }

        public bool RoleMessages_IsExist(int roleID, int pid)
        {
            return dal.RoleMessages_IsExist(roleID, pid);
        }

        public int RoleMessages_Count(int roleID)
        {
            string strWhere = string.Empty;
            switch ((RoleType)roleID)
            {
                case RoleType.质管审核:
                    strWhere = string.Format(" AND RoleID IN ({0},{1}) ", (int)RoleType.商品信息, (int)RoleType.商品资质);
                    break;
                default:
                    strWhere = string.Format(" AND RoleID={0}   ", roleID);
                    break;
            }
            return dal.RoleMessages_Count(strWhere);
        }

        /// <summary>
        /// 设置当前Role下 Product已编辑
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="productid"></param>
        /// <returns></returns>
        public bool RoleMessages_SetProcessed(RoleType rt, int productid)
        {
            return dal.RoleMessages_SetProcessed((int)rt, productid);
        }
    }
}
