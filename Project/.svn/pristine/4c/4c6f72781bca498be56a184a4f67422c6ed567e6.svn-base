using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.Model
{
    public class Qualificatoin
    {
        public enum Type
        {
            企业资质 = 0,
            企业分类资质 = 1,
            商品资质 = 2
        }
        public enum Category
        {
            药品 = 1,
            医疗器械,
            保健品,
            食品,
            化妆品,
            消毒用品,
            百货
        }
    }
    public class QualificationInfo
    {
        public QualificationInfo()
        {


        }
        /// <summary>
        /// 自增长ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 资质名称
        /// </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 类型 ：
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 商品专有资质
        /// </summary>
        public int ProductID { get; set; }
    }

    public class ProductQualificationInfo
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public int PQID { get; set; }

        /// <summary>
        /// 资质分类ID
        /// </summary>
        public int CID { get; set; }
        /// <summary>
        /// 资质ID
        /// </summary>
        public int QualificationID { get; set; }

        public string QualificationName { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 企业ID
        /// </summary>
        public int ManufacturerID { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态 0正常 1删除
        /// </summary>
        public short Status { get; set; }
    }
    public class QualificationCategoryInfo
    {
        public int Cid { get; set; }
        public int Qid { get; set; }
    }
}