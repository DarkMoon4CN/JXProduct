using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.Enums
{
    /// <summary>
    ///  角色类型
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        /// 采购录入
        /// </summary>
        采购编辑 = 34,
        /// <summary>
        /// 商品信息编辑
        /// </summary>
        产品编辑 = 35,
        /// <summary>
        /// 质管信息编辑
        /// </summary>
        质管编辑 = 36,
        /// <summary>
        /// 质管审核
        /// </summary>
        质管审核 = 37,
        /// <summary>
        /// 审核商品信息
        /// </summary>
        商品信息 = 38, //37_1
        /// <summary>
        /// 审核商品资质
        /// </summary>
        商品资质 = 39, //37_2

        /// <summary>
        /// 报价员编辑
        /// </summary>
        运营报价员 = 40,

    }
}
