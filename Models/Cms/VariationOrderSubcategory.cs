// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class VariationOrderSubcategory
    {
        public int SubCategoryId { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }

        public virtual VariationOrderCategory CategoryNavigation { get; set; }
    }
}