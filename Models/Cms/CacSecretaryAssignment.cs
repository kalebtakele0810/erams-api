﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class CacSecretaryAssignment
    {
        public int AssignmentId { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public DateTime? AssignmentRevocationDate { get; set; }
        public string AssignedBy { get; set; }
    }
}