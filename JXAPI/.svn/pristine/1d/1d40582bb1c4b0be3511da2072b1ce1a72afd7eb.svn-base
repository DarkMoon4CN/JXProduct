using JXAPI.Component.IBLL;
using JXAPI.Component.Model;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JXAPI.Component.BLL
{
    public class OrdersBLL
    {
        private static readonly MySqlOrderDAL dal = new MySqlOrderDAL();

        public static OrdersBLL Instance
        {
            get
            {
                return new OrdersBLL();
            }
        }

        /// <summary>
        /// 查询Mysql订单
        /// </summary>
        /// <param name="updateTime">查询时间</param>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public DataTable GetList(string updateTime, string tableName)
        {
            return dal.GetList(0, updateTime, 0, 0, tableName, 0);
        }
        
        /// <summary>
        /// 查询Mysql数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="createTime">截止时间</param>
        /// <param name="start">开始记录行</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public DataTable GetList(int ID, string updateTime, int start, int pageSize, string tableName, int status = 0)
        {
            return dal.GetList(ID, updateTime, start, pageSize, tableName, status);
        }

        /// <summary>
        /// 更新订单 指定列的值
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="colName">列名</param>
        /// <param name="colValue">列值</param>
        /// <returns>true、false</returns>
        public bool Orders_Update(int orderId, string[] colName, object[] colValue)
        {
            return dal.Orders_Update(orderId, colName, colValue);
        }

        /// <summary>
        /// 新增订单快递单号
        /// </summary>
        /// <param name="info">快递信息实体</param>
        /// <returns>返回：true=成功，false=失败</returns>
        public bool OrderLogistics_Insert(OrderLogisticsInfo info)
        {
            return dal.OrderLogistics_Insert(info);
        }

        /// <summary>
        /// 添加订单操作日志
        /// </summary>
        /// <param name="ool">实体</param>
        /// <returns>返回数据：0=成功，1=失败，其它自定义消息</returns>
        public bool OrderOperateLog_Insert(OrderOperateLogInfo ool)
        {
            return dal.OrderOperateLog_Insert(ool);
        }
    }
}
