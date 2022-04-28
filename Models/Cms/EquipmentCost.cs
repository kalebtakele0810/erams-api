﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class EquipmentCost
    {
        public EquipmentCost()
        {
            LabourCosts = new HashSet<LabourCost>();
        }

        public int PlantCostId { get; set; }
        public int? ItemId { get; set; }
        public int? HauledMaterialId { get; set; }
        public string HaulType { get; set; }
        public int? EquipmentId { get; set; }
        public int? MaterialCostId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Cost { get; set; }
        public string Activity { get; set; }
        public bool? RentalRate { get; set; }
        public bool? CalculatedRate { get; set; }
        public string Note { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Item Item { get; set; }
        public virtual MaterialCost MaterialCost { get; set; }
        public virtual ICollection<LabourCost> LabourCosts { get; set; }
    }
}