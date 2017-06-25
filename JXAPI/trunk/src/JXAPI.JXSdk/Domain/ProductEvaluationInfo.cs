using System;
using System.Collections.Generic;

namespace JXAPI.JXSdk.Domain
{
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public class ProductEvaluationInfo
    {
        /// <summary>
        /// ��ѯID
        ///</summary>
        public int evaluationID { get; set; }

        /// <summary>
        /// ��ƷID
        /// </summary>
        public int productID { get; set; }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// �û�ID
        /// </summary>
        public int uid { get; set; }
        
        /// <summary>
        ///  �û�����
        ///</summary>
        public string userName { get; set; }
                
        /// <summary>
        ///  ��ѯʱ��
        ///</summary>
        public long evaTime { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string title { get; set; }

        /// <summary>
        ///  ����
        ///</summary>
        public string content { get; set; }

        /// <summary>
        /// �ϼ�ID
        /// </summary>
        public int parentID { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public short score { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public short status { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public short source { get; set; }

        public IList<EvaluationKeyword> evaluationKeywordList { get; set; }
    }
}
