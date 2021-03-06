// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class ContactDetail
    {
        public int ContactDetailId { get; set; }
        public int? ContractId { get; set; }
        public int? KeyStaffId { get; set; }
        public string Position { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public string Landline { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string ContactType { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public virtual Contract Contract { get; set; }
        public virtual KeyStaffRegister KeyStaff { get; set; }
    }
}