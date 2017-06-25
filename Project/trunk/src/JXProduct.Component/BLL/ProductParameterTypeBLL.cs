using JXProduct.Component.Model;
using JXProduct.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class ProductParameterTypeBLL
    {
        private ProductParameterTypeBLL() { }
        private static ProductParameterTypeBLL _instance;
        public static ProductParameterTypeBLL Instance { get { return _instance ?? (_instance = new ProductParameterTypeBLL()); } }
        private static readonly ProductParameterTypeDAL dal = new ProductParameterTypeDAL();


        public ProductParameterTypeInfo ProductParameterType_Get(int typeid, int productid)
        {
            var p = this.parameterTypeList.FirstOrDefault(t => t.TypeID == typeid);
            if (productid > 0)
                p.detailList = this.ProductParameterType_GetDetail(typeid, productid);
            else
                p.detailList = this.ProductParameterType_GetDetail(typeid);
            return p;

        }

        public List<ProductParameterTypeDetailInfo> ProductParameterType_GetDetail(int typeid, int productid)
        {
            return dal.ProductParameterType_GetDetail(typeid, productid);
        }
        public List<ProductParameterTypeDetailInfo> ProductParameterType_GetDetail(int typeid)
        {
            return dal.ProductParameterType_GetDetail(typeid);
        }

        public bool ProductParameterType_Insert(int productid, List<ProductParameterTypeDetailInfo> list)
        {
            return dal.ProductParameterType_Insert(productid, list);
        }

        public List<ProductParameterTypeInfo> parameterTypeList
        {
            get
            {
                var _pList = new List<ProductParameterTypeInfo>();
                if (_pList.Count < 1)
                {
                    _pList.Add(new ProductParameterTypeInfo() { TypeID = 1, Name = "药品", Title = "药品的储存、不良反应、禁忌、注意事项等信息请详见药品说明书。" });
                    _pList.Add(new ProductParameterTypeInfo() { TypeID = 2, Name = "蓝帽子/食健字产品", Title = "本品为保健食品（食健字产品），产品的详细信息以产品说明书或标签为准。" });
                    _pList.Add(new ProductParameterTypeInfo() { TypeID = 3, Name = "非食健字产品", Title = "本品（非食健字产品）的详细信息以产品说明书或标签为准。" });
                    _pList.Add(new ProductParameterTypeInfo() { TypeID = 4, Name = "食药监械字号产品", Title = "本品为医疗器械（食药监械字号产品），产品详细信息以产品说明书或标签为准。" });
                    _pList.Add(new ProductParameterTypeInfo() { TypeID = 5, Name = "非食药监械字号产品", Title = "本品（非食药监械字号产品）的详细信息以产品说明书或标签为准。" });
                    _pList.Add(new ProductParameterTypeInfo() { TypeID = 6, Name = "卫妆准字产品", Title = "本品（卫妆准字产品）的详细信息以产品标签标识为准。" });
                    _pList.Add(new ProductParameterTypeInfo() { TypeID = 7, Name = "卫消证字号产品", Title = "本品（卫消证字号产品）的详细信息以产品说明书或标签为准。" });
                }
                return _pList;
            }
        }
    }
}
