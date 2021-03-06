// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class KeyStaffRegister
    {
        public KeyStaffRegister()
        {
            AssessmentMetadata = new HashSet<AssessmentMetadatum>();
            ContactDetails = new HashSet<ContactDetail>();
        }

        public int StaffId { get; set; }
        public string FullName { get; set; }
        public string IdType { get; set; }
        public string IdNumber { get; set; }
        public bool? IsLocalStaff { get; set; }
        public string Landline { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public virtual ICollection<AssessmentMetadatum> AssessmentMetadata { get; set; }
        public virtual ICollection<ContactDetail> ContactDetails { get; set; }
    }
}