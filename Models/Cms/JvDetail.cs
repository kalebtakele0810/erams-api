// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class JvDetail
    {
        public int EntryId { get; set; }
        public int? FirmId { get; set; }
        public int? PartnerId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual Firm Firm { get; set; }
    }
}