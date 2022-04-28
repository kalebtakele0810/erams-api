﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class Item
    {
        public Item()
        {
            BidItems = new HashSet<BidItem>();
            BoQitemEntries = new HashSet<BoQitemEntry>();
            EquipmentCosts = new HashSet<EquipmentCost>();
            ItemWorkMethods = new HashSet<ItemWorkMethod>();
            LabourCosts = new HashSet<LabourCost>();
            MaterialCosts = new HashSet<MaterialCost>();
        }

        public string DivisionCode { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string ItemDescription1 { get; set; }
        public string UnitMeasure { get; set; }
        public decimal UnitRate { get; set; }
        public string ParentItemCode { get; set; }
        public int? PsCode { get; set; }
        public int AnalysisType { get; set; }
        public bool? Custom { get; set; }
        public decimal? PlacingPerformanceRate { get; set; }
        public string Note { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string SpecificationName { get; set; }

        public virtual Division Division { get; set; }
        public virtual UnitMeasure UnitMeasureNavigation { get; set; }
        public virtual ICollection<BidItem> BidItems { get; set; }
        public virtual ICollection<BoQitemEntry> BoQitemEntries { get; set; }
        public virtual ICollection<EquipmentCost> EquipmentCosts { get; set; }
        public virtual ICollection<ItemWorkMethod> ItemWorkMethods { get; set; }
        public virtual ICollection<LabourCost> LabourCosts { get; set; }
        public virtual ICollection<MaterialCost> MaterialCosts { get; set; }
    }
}