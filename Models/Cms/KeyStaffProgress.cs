﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class KeyStaffProgress
    {
        public int StaffProgressId { get; set; }
        public int StaffId { get; set; }
        public int ContractId { get; set; }
        public decimal? AssignmentProgress { get; set; }
        public DateTime? AssignmentProgressDate { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public bool? IsApproval { get; set; }
        public bool? IsApprovalByDirector { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string LastEditedBy { get; set; }
    }
}