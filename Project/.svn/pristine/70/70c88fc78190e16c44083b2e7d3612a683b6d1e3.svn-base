using System;
using System.Collections.Generic;
using JXProduct.Component.Model;
namespace JXProduct.Component.BLL
{
    /// <summary>
    /// Manufacturer 
    /// </summary>
    public class ManufacturerBLL
    {
        private ManufacturerBLL() { }
        private static ManufacturerBLL _instance;
        public static ManufacturerBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ManufacturerBLL();
                }
                return _instance;
            }
        }

        private static readonly JXProduct.Component.SQLServerDAL.ManufacturerDAL dal = new JXProduct.Component.SQLServerDAL.ManufacturerDAL();

        /// <summary>
        /// Manufacturer_Insert Method
        /// </summary>
        /// <param name="ManufacturerInfo">Manufacturer object</param>
        /// <returns>The new ID : int </returns>
        public int Manufacturer_Insert(ManufacturerInfo manufacterinfo)
        {
            return dal.Manufacturer_Insert(manufacterinfo);
        }

        /// <summary>
        /// Manufacturer_Update Method		
        /// </summary>
        /// <param name="ManufacturerInfo">Manufacturer object</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Manufacturer_Update(ManufacturerInfo manufacterinfo)
        {
            return dal.Manufacturer_Update(manufacterinfo);
        }


        /// <summary>
        /// Manufacturer_Get Method
        /// </summary>
        public ManufacturerInfo Manufacturer_Get(Int32 manuid)
        {
            return dal.Manufacturer_Get(manuid);
        }

        /// <summary>
        /// Manufacturer_Delete Method
        /// </summary>
        /// <returns>true:成功 false:失败</returns>	
        public bool Manufacturer_Delete(Int32 manuid)
        {
            return dal.Manufacturer_Delete(manuid);
        }

        /// <summary>
        /// Manufacturer_GetList Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of ManufacturerInfo</returns>
        public IList<ManufacturerInfo> Manufacturer_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            return dal.Manufacturer_GetList(pageIndex, pageSize, orderType, strWhere, out recordCount);
        }
    }
}
