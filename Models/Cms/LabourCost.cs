﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class LabourCost
    {
        public int LabourCostId { get; set; }
        public int? ItemId { get; set; }
        public int? HauledMaterialId { get; set; }
        public int? LabourId { get; set; }
        public int? OperatedEquipmentCostId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Cost { get; set; }
        public string Activity { get; set; }
        public string Note { get; set; }

        public virtual Item Item { get; set; }
        public virtual Labour Labour { get; set; }
        public virtual EquipmentCost OperatedEquipmentCost { get; set; }
    }
}