﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class ProjectCurrencyBreakdown
    {
        public int ProjectCurrencyBreakDownId { get; set; }
        public int? ProjectId { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public virtual Project Project { get; set; }
    }
}