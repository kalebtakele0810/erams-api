﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class PurchaseOrder
    {
        public int PurchaseNo { get; set; }
        public string RequisitionNo { get; set; }
        public string RequestingUnit { get; set; }
        public string AccountNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public string PurchasedTo { get; set; }
        public string ShippingMark { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string Cff { get; set; }
        public string Terms { get; set; }
        public string Directorate { get; set; }
        public string Under80000 { get; set; }
        public string Over80000 { get; set; }
        public string Reff { get; set; }
        public string Nb { get; set; }
    }
}