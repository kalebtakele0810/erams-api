﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class Terrain
    {
        public int TerrainId { get; set; }
        public int? EngineeringEstimateId { get; set; }
        public decimal? FromKm { get; set; }
        public decimal? ToKm { get; set; }
        public string Terrain1 { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastEditedBy { get; set; }

        public virtual EngineeringEstimate EngineeringEstimate { get; set; }
    }
}