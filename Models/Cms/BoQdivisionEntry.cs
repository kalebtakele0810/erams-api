// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class BoQdivisionEntry
    {
        public BoQdivisionEntry()
        {
            BoQitemEntries = new HashSet<BoQitemEntry>();
        }

        public int BoQdivisionEntryId { get; set; }
        public int? BoQseriesEntryId { get; set; }
        public string DivisionCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string SpecificationName { get; set; }

        public virtual BoQseriesEntry BoQseriesEntry { get; set; }
        public virtual Division Division { get; set; }
        public virtual ICollection<BoQitemEntry> BoQitemEntries { get; set; }
    }
}