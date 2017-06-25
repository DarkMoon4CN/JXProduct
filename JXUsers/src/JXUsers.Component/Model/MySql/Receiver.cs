﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace JXUsers.Component.Model.MySql
{
 
    [Table("Receiver")]
    public class Receiver
    {
        [Key]//主键
        public int ReceiverID{get;set;}
        public int UID { get; set; }
        public string TrueName { get; set; }
        public int Province { get; set; }
        public int City { get; set; }
        public int County { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Postalcode { get; set; }
        public int IsDefault { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int PaymentMethodID { get; set; }
        public int ShipMethodID { get; set; }
        public int sendTimeInfo { get; set; }
        public int splitType { get; set; }
    }
}