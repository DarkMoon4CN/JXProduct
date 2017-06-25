using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace JXProduct.AdminUI.Models.Product
{
    public class EvaluationModel
    {
        public EvaluationModel()
        {
            this.UserID = new Random().Next(5000, 10000);
            this.Source = 10;
            this.Score = 5;
        }
        //  商品ID
        [Required]
        public Int32 ProductID { get; set; }

        //  金象码
        public String ProductCode { get; set; }

        public string ChineseName { get; set; }

        public int UserID { get; set; }

        [Required]
        public String UserName { get; set; }

        [Required]
        public short Score { get; set; }

        public short Source { get; set; }

        [Required]
        public String Title { get; set; }

        //  内容
        [Required]
        public String Content { get; set; }
    }
}