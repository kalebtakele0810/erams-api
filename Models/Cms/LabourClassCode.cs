// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class LabourClassCode
    {
        public LabourClassCode()
        {
            Labours = new HashSet<Labour>();
        }

        public string ClassCode { get; set; }

        public virtual ICollection<Labour> Labours { get; set; }
    }
}