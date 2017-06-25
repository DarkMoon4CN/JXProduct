using JXProduct.Component.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.Model
{
    public class ProductProcessInfo
    {
        public ProductProcessInfo(int productid, byte p1, byte p2, byte p3, byte p4, string Updater)
        {
            this.ProductID = productid;
            this.Process1 = p1;
            this.Process2 = p2;
            this.Process3 = p3;
            this.Process4 = p4;

            this.Updater = Updater;
            this.UpdateTime = DateTime.Now;
        }
        public int ProductID { get; set; }

        /// <summary>
        /// 商品信息维护
        /// </summary>
        public byte Process1 { get; set; }

        /// <summary>
        /// 质管信息维护
        /// </summary>
        public byte Process2 { get; set; }

        /// <summary>
        /// 质管部信息审核
        /// </summary>
        public byte Process3 { get; set; }

        /// <summary>
        /// 运营编辑价格
        /// </summary>
        public byte Process4 { get; set; }

        public string Updater { get; set; }
        public DateTime UpdateTime { get; set; }


        /// <summary>
        /// 根据类型,设置当前状态是否编辑,并且是否操作相关审核
        /// </summary>
        /// <param name="pp"></param>
        public void SetProcess(Product.Process pp)
        {
           
        }
    }
}