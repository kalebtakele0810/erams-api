﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class EstimateCommitte
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int ProjectPerKmId { get; set; }

        public virtual ProjectPerKm ProjectPerKm { get; set; }
    }
}