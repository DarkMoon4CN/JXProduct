using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JXAPI.Component.Model;

namespace JXAPI.Component.BLL
{
    public class ProductOutBookSqlBLL
    {
        private static ProductOutBookSqlBLL _instance;
        private static readonly ProductOutBookSqlDAL dal = new ProductOutBookSqlDAL();

        public static ProductOutBookSqlBLL Instance
        {
            get
            {
                return new ProductOutBookSqlBLL();
            }
        }
        
        /// <summary>
        /// 新增药师回拨
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Add(ProductOutBookInfo info)
        {
            return dal.AddProductOutBook(info);
        }
    }
}
