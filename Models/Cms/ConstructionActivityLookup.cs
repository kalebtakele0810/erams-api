// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class ConstructionActivityLookup
    {
        public ConstructionActivityLookup()
        {
            VariationOrders = new HashSet<VariationOrder>();
        }

        public string ConstructionActitvity { get; set; }

        public virtual ICollection<VariationOrder> VariationOrders { get; set; }
    }
}