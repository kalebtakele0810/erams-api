﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class BidView
    {
        public int EngineeringEstimateId { get; set; }
        public string SeriesDescription { get; set; }
        public int BidItemId { get; set; }
        public int? ItemId { get; set; }
        public decimal? OriginalQuantity { get; set; }
        public decimal? NewQuantity { get; set; }
        public decimal? ContractorRate { get; set; }
        public bool? IsSps { get; set; }
        public int? PsCode { get; set; }
        public bool? ItemRateIncludesVat { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Contingency { get; set; }
        public int TenderId { get; set; }
        public string UnitMeasure { get; set; }
        public bool? DeductDayworksForContingency { get; set; }
        public string SeriesSpec { get; set; }
        public string ItemSpec { get; set; }
    }
}