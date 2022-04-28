﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class QuantityChange
    {
        public int? ContractId { get; set; }
        public int QuantityChange1 { get; set; }
        public int? ItemId { get; set; }
        public string ItemCode { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public decimal? EstimatedAdditionalCost { get; set; }
        public decimal? EstimatedAdditionalQuantity { get; set; }
        public decimal? PercentageAboveOriginal { get; set; }
        public decimal? PercentageAboveOriginalContractAmount { get; set; }
        public string Description { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual QuantityChangeCategory CategoryNavigation { get; set; }
        public virtual Contract Contract { get; set; }
    }
}