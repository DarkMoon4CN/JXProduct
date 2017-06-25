using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class CategoryBLL
    {
        private CategoryBLL() { }
        private static CategoryBLL _instance;
        private static readonly JXProduct.Component.SQLServerDAL.CategoryDAL dal = new JXProduct.Component.SQLServerDAL.CategoryDAL();
        public static CategoryBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new CategoryBLL());
            }
        }

        /// <summary>
        /// Category_Update Method		
        /// </summary>
        /// <param name="categoryInfo">Category_Update</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Category_Update(CategoryInfo categoryInfo,int inputTypeID)
        {
            return dal.Category_Update(categoryInfo,inputTypeID);
        }

        /// <summary>
        /// 根据CFID 查询
        /// </summary>
        /// <param name="cfid"></param>
        /// <returns></returns>
        public List<CategoryInfo> Category_Get(int cfid)
        {
            return dal.Category_Get(cfid);
        }	
    }
}
