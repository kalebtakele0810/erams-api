// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class ContractorProgram
    {
        public int ProgramId { get; set; }
        public int? ContractId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string LastEditedBy { get; set; }

        public virtual Contract Contract { get; set; }
    }
}