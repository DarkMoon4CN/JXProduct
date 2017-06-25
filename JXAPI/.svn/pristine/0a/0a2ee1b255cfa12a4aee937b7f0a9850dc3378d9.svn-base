using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using JXAPI.JXSdk.Service;
using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using JXAPI.JXSdk.Utils;
using JXAPI.JXSdk.Request;

namespace JXAPI.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 意见反馈
            /*
            //  1.查询意见反馈
            FeedBackInfo info = FeedBackService.Instance.Get(9);
            
            //  2.删除意见反馈
            bool bol = FeedBackService.Instance.Delete(5, "LCJ");
            
            //  3.回复意见反馈
            JXAPI.JXSdk.Request.FeedBackReplyRequest rReply = new JXSdk.Request.FeedBackReplyRequest()
            {
                feedBackId = 2,
                replyContents = "感谢您对金象网一如既往的支持！",
                uid = 1,
                userName = "LCJ"
            };
            bool bol = FeedBackService.Instance.Reply(rReply);
            
            //  4.意见反馈列表 
            JXAPI.JXSdk.Request.FeedBackAppRequest request = new JXSdk.Request.FeedBackAppRequest()
            {
                startTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-01-10")),
                endTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-05-10")),
                pageForm = new PageFormInfo()
            };
            FeedBackFristResponse list = FeedBackService.Instance.List(request);
            */
            #endregion

            #region 商品评论
            /*                        
            //  2.查询评论信息
            ProductEvaluationInfo info = ProductEvaluationService.Instance.Get(88012);
            Console.WriteLine(info.content);
            
            //  3.商品的评论的关键字
            IList<EvaluationKeyword> keywordList = ProductEvaluationService.Instance.GetKeywordList(364);
            
            //  4.新增评论
            ProductEvaluationInfo infox = new ProductEvaluationInfo()
            {
                uid = 5,
                productID = 364,
                userName = "LCJ",
                parentID = 0,
                title = "评论测试1",
                content = "比实体店贵一点！可是有金象方便多了不用出门就有想要的！",
                score = 5,
                source = 2
            };
            bool bolx = ProductEvaluationService.Instance.Add(infox);
            Console.WriteLine(bolx);
            
            //  0.审核评论
            bool bol = ProductEvaluationService.Instance.CheckEvaluation(2, 0, "LCJ");
            Console.WriteLine(bol);
            */
            //  1.商品评论列表
            ProductEvaluationListResponse list = ProductEvaluationService.Instance.List(
            new JXSdk.Request.ProductEvaluationListRequest()
            {
                pageForm = new PageFormInfo() { page = 2, size = 10 }
            });
            
            #endregion

            #region 健康头条
            /*
            //  1.新增健康头条
            HeadLinesInfo info = new HeadLinesInfo()
            {
                categoryID = 1,
                title = "大哥大-12",
                contents = "小哥小",
                picture = "http://img.jxdyf.com/product/0000/002/2130_L1.jpg",
                smallPicture = "http://img.jxdyf.com/product/0000/002/2130_S1.jpg",
                creator = "LCJ",
                keywords = "健康养生"
            };
            bool result = HeadLinesService.Instance.Add(info);
            Console.WriteLine(result);
            
            //  2.健康头条列表
            HeadLinesAppResponse list = HeadLinesService.Instance.List(
                new JXSdk.Request.HeadLinesAppRequest()
                {
                    pageForm = new PageFormInfo(),
                    startTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-06-12")),
                    endTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-06-12 23:59:59")),
                    soft = 1
                });
            Console.WriteLine(list.list[0].contents);

            //  3.编辑健康头条
            HeadLinesInfo info = new HeadLinesInfo()
            {
                headID = 57,
                categoryID = 1,
                title = "今日健康头条-1111",
                contents = "大花猫2",
                picture = "http://img.jxdyf.com/product/0000/014/14138_L1.jpgg",
                smallPicture = "http://img.jxdyf.com/product/0000/014/14138_S1.jpg",
                creator = "LCJ",
                keywords = "健康养生"
            };
            bool result = HeadLinesService.Instance.Update(info);
            Console.WriteLine(result);
            
            //  4.审核健康头条
            bool result = HeadLinesService.Instance.Verify(57, 1, "LCJ");
            Console.WriteLine(result);
            
            //  5.健康头条信息
            HeadLinesInfo infox = HeadLinesService.Instance.Get(57);
            Console.WriteLine(infox.contents);            

            //  7.新增健康头条评论
            HeadLinesCommentInfo commentInfo = new HeadLinesCommentInfo()
            {
                headID = 57,
                contents = "不错不错",
                uid = 1
            };
            bool commentbol = HeadLinesService.Instance.AddComment(commentInfo);
            Console.WriteLine(commentbol);
                        
            //  6.健康头条评论
            HeadLinesCommentRequest request = new HeadLinesCommentRequest()
            {
                headID = 4,
                soft = "desc",
                pageForm = new PageFormInfo()
            };
            var list = HeadLinesService.Instance.CommentList(request);
            */
            #endregion

            #region 健康说
            /*            
            //  2.审核健康说
            bool bol = TipsService.Instance.Verify(4, 2, "LCJ");
            Console.WriteLine(bol);

            //  3.健康说明细
            TipsInfo info = TipsService.Instance.Get(4);
            Console.WriteLine(info.contents);

            //  6.健康说评论列表
            TipsCommentRequest request = new TipsCommentRequest()
            {
                tipsID = 4,
                soft = "desc",
                pageform = new PageFormInfo()
            };
            var listx = TipsService.Instance.CommentList(request);

            //  7.评论审核
            bool bolx = TipsService.Instance.VerifyComment(58, 2, "LCJ");

            //  4.新增健康说
            TipsInfo info = new TipsInfo()
            {
                categoryID = 1,
                type = 0,
                contents = "话说今天不下雨，下雨只下大暴雨",
                picture = "http://img.jxdyf.com/product/0000/002/2439_L1.jpg",
                creator = "LCJ",
                channel = 0
            };
            bool bol = TipsService.Instance.Add(info);
            
            //  5.编辑健康说
            TipsInfo info = new TipsInfo()
            {
                tipsID = 98,
                categoryID = 1,
                type = 1,
                contents = "打伞就不下雨",
                picture = "http://img.jxdyf.com/product/0000/002/2439_L1.jpg",
                updater = "LCJ",
                channel = 0
            };
            bool bol = TipsService.Instance.Update(info);

            //  1.健康说列表
            TipsAppResponse list = TipsService.Instance.List(
                new JXSdk.Request.TipsAppRequest()
                {
                    pageForm = new PageFormInfo(),
                    startTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-06-15")),
                    endTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-06-15 23:59:59")),
                    soft = 1
                });
            Console.WriteLine(list.list[0].contents);
            */
            #endregion

            #region 意见反馈
            /*
            //  1.查询意见反馈
            FeedBackInfo info = FeedBackService.Instance.Get(9);
            
            //  2.删除意见反馈
            bool bol = FeedBackService.Instance.Delete(5, "LCJ");
            
            //  3.回复意见反馈
            JXAPI.JXSdk.Request.FeedBackReplyRequest rReply = new JXSdk.Request.FeedBackReplyRequest()
            {
                feedBackId = 2,
                replyContents = "感谢您对金象网一如既往的支持！",
                uid = 1,
                userName = "LCJ"
            };
            bool bol = FeedBackService.Instance.Reply(rReply);

            //  4.意见反馈列表 
            JXAPI.JXSdk.Request.FeedBackListRequest request = new JXSdk.Request.FeedBackListRequest()
            {
                startTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-01-10")),
                endTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-05-10")),
                pageForm = new PageFormInfo()
            };
            FeedBackListResponse list = FeedBackService.Instance.List(request);
            #endregion

            #region 健康头条
            //  2.健康头条列表
            HeadLinesListResponse listx = HeadLinesService.Instance.List(
            new JXSdk.Request.HeadLinesListRequest()
            {
                pageForm = new PageFormInfo(),
                startTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-06-12")),
                endTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-06-12 23:59:59")),
                soft = 1
            });
            Console.WriteLine(listx.list[0].contents);
            */

            /*
            //  1.新增健康头条
            HeadLinesInfo info = new HeadLinesInfo()
            {
                categoryID = 1,
                title = "大哥大-12",
                contents = "小哥小",
                picture = "http://img.jxdyf.com/product/0000/002/2130_L1.jpg",
                smallPicture = "http://img.jxdyf.com/product/0000/002/2130_S1.jpg",
                creator = "LCJ",
                keyWords = "健康养生,健康有道"
            };
            int headID = HeadLinesService.Instance.Add(info);
            Console.WriteLine(headID);
            
            //  2.健康头条列表
            HeadLinesListResponse list = HeadLinesService.Instance.List(
            new JXSdk.Request.HeadLinesListRequest()
            {
                pageForm = new PageFormInfo(),
                startTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-07-16")),
                endTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-08-12 23:59:59")),
                soft = 1
            });
            Console.WriteLine(list.list[0].contents);

            //  1.新增头条商品
            HeadlinesProductInfo infox = new HeadlinesProductInfo()
            {
                headID = 10,
                productID = 241,
                title = "好商品",
                intro = "不错的商品",
                picture = "F/lg/vZP/ryBw/0btzdDmU_L.jpg"
            };
            bool addResult = HeadLinesService.Instance.AddHeadProduct(infox);
            Console.WriteLine(addResult);
            
            //  2.新增头条商品
            infox = new HeadlinesProductInfo()
            {
                headID = 10,
                productID = 241,
                title = "好商xxxx品",
                intro = "不错的xxxx商品",
                picture = "F/lg/vZP/ryBw/0btzdDmU_L.jpg"
            };
            bool updResult = HeadLinesService.Instance.UpdateHeadProduct(infox);
            Console.WriteLine(updResult);
            
            //  3.删除头条商品
            bool delResult = HeadLinesService.Instance.DeleteHeadProduct(10, 241);
            Console.WriteLine(delResult);
            
            //  4.商品列表
            HeadLinesProductRequest request = new HeadLinesProductRequest()
            {
                headID = 10,
                pageForm = new PageFormInfo()
            };
            HeadLinesProductResponse list = HeadLinesService.Instance.GetHeadProductList(request);
            */

            //  1.新增精彩活动
            /*
            ActivityInfo info = new ActivityInfo()
            {
                categoryID = 1,
                contents = "火热的夏天，冰点促销",
                picture = "F/lg/vZP/ryBw/0btzdDmU_L.jpg",
                subPicture = "F/lg/vZP/ryBw/0btzdDmU_L.jpg",
                title = "疯狂夏天",
                status = 1,
                creator = "LCJ"
            };
            int actID = ActivityService.Instance.Add(info);
            Console.WriteLine(actID);

            //  2.编辑精彩活动
            info = new ActivityInfo()
            {
                actID = 11,
                categoryID = 1,
                contents = "20150716-火热的夏天，冰点促销",
                picture = "F/lg/vZP/ryBw/0btzdDmU_L.jpg",
                subPicture = "F/lg/vZP/ryBw/0btzdDmU_L.jpg",
                title = "疯狂夏天，XX的夏天",
                status = 1,
                creator = "LCJ"
            };
            bool bol = ActivityService.Instance.Update(info);
            Console.WriteLine(bol);

            //  3.删除精彩活动
            bol = ActivityService.Instance.Delete(12);
            Console.WriteLine(bol);

            //  4.精彩活动列表
            ActivityListResponse list = ActivityService.Instance.List(
            new JXSdk.Request.ActivityListRequest()
            {
                pageForm = new PageFormInfo() { page = 1, size = 2 },
                startTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-07-16")),
                endTime = ConvertDataTimeHelper.ConvertDataTimeLong(DateTime.Parse("2015-08-12 23:59:59")),
                sort = 1
            });
            Console.WriteLine(list.list[0].contents);
            */

            //  
            ActivityService.Instance.Get(11);

            /*
            //  5.新增活动商品
            bool bol = ActivityService.Instance.AddActivityProduct(11, 241, 1);
            Console.WriteLine(bol);

            bol = ActivityService.Instance.AddActivityProduct(11, 247, 2);
            Console.WriteLine(bol);

            //  6.编辑精彩活动商品
            bol = ActivityService.Instance.UpdateActivityProduct(11, 241, 2);
            Console.WriteLine(bol);

            //  7.删除精彩活动商品
            bol = ActivityService.Instance.DeleteActivityProduct(11, 247);
            Console.WriteLine(bol);

            //  8.活动商品列表
            ActivityProductResponse list = ActivityService.Instance.GetProductList(
            new JXSdk.Request.ActivityProductRequest()
            {
                actID = 11,
                pageForm = new PageFormInfo()
            });
            */
            #endregion
            Console.WriteLine();
        }
    }
}