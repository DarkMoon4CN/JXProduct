using AutoMapper;
using JXProduct.AdminUI.Models.Product;
using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXProduct.AdminUI.App_Start.AutoMapper
{
    public class RegisterAutomapper
    {
        /// <summary>
        /// 注册autoMapper
        /// </summary>
        public static bool Execute()
        {
            //product
            Mapper.CreateMap<ProductInfo, ProductModel>();
            Mapper.CreateMap<ProductModel, ProductInfo>()
                .ForMember(t => t.Type, m => m.Ignore())
                //.ForMember(t => t.IsBasePowder, m => m.MapFrom(t => (short)(t.IsBasePowder ? 1 : 0)))
                //.ForMember(t => t.IsDeluxe, m => m.MapFrom(t => (short)(t.IsDeluxe ? 1 : 0)))
                .ForMember(t => t.IsFragile, m => m.MapFrom(t => (short)(t.IsFragile ? 1 : 0)))
                .ForMember(t => t.IsOdour, m => m.MapFrom(t => (short)(t.IsOdour ? 1 : 0)))
                .ForMember(t => t.IsProtection, m => m.MapFrom(t => (short)(t.IsProtection ? 1 : 0)))
                //.ForMember(t => t.IsYiBao, m => m.MapFrom(t => (short)(t.IsYiBao ? 1 : 0)))
                .ForMember(t => t.ContainMHJ, m => m.MapFrom(t => (short)(t.ContainMHJ ? 1 : 0)))
                ;

            //activity
            Mapper.CreateMap<ProductActivityInfo, ActivityModel>()
                .ForMember(t => t.Type, m => m.MapFrom(e => e.Type.ToString()))
                .ForMember(t => t.StartDate, m => m.MapFrom(e => e.StartDate.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(t => t.EndDate, m => m.MapFrom(e => e.EndDate.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(t => t.isBuyCard, m => m.MapFrom(e => e.ExtType.Contains("shoppingcard")))
                .ForMember(t => t.isCoupon, m => m.MapFrom(e => e.ExtType.Contains("coupon")));

            Mapper.CreateMap<ActivityModel, ProductActivityInfo>()
                .ForMember(t => t.Type, m => m.MapFrom(e => Convert.ToInt16(e.Type.Substring(0, 1))))
                .ForMember(t => t.StartDate, m => m.MapFrom(e => Convert.ToDateTime(e.StartDate)))
                .ForMember(t => t.EndDate, m => m.MapFrom(e => Convert.ToDateTime(e.EndDate)));
            return true;
        }
    }
}
