﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JXAPI.JXSdk.Response;

namespace JXAPI.JXSdk.Request
{
    /// <summary>
    /// 商品评论
    /// </summary>
    public class ProductEvaluationRequest : Venus.IVenusRequest<ProductEvaluationResponse>
    {
        #region 参数
        /// <summary>
        /// 商品ID
        /// </summary>
        public int productId { get; set; }

        /// <summary>
        /// 评论ID
        /// </summary>
        public int evaluationId { get; set; }

        /// <summary>
        /// 操作类型 0 为删除  1为通过
        /// </summary>
        public short check { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string checker { get; set; }
        #endregion

        #region ITopRequest Members
        public string GetApiName()
        {
            return string.Empty;
        }

        public IDictionary<string, string> GetParameters()
        {
            return null;
        }

        public void Validate()
        {
            Venus.Utils.RequestValidator.ValidateRequired("evaluationId", this.evaluationId);
        }
        #endregion
    }
}
