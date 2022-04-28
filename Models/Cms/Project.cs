﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class Project
    {
        public Project()
        {
            Contracts = new HashSet<Contract>();
            Payments = new HashSet<Payment>();
            ProjectCurrencyBreakdowns = new HashSet<ProjectCurrencyBreakdown>();
            ProjectFunders = new HashSet<ProjectFunder>();
            ProjectPerKms = new HashSet<ProjectPerKm>();
        }

        public int ProjectId { get; set; }
        public string Rsdp { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public double? Duration { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? RoadLength { get; set; }
        public string CreatedBy { get; set; }
        public string LastEditedBy { get; set; }
        public string CreatingDirectorate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public virtual Rsdp RsdpNavigation { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<ProjectCurrencyBreakdown> ProjectCurrencyBreakdowns { get; set; }
        public virtual ICollection<ProjectFunder> ProjectFunders { get; set; }
        public virtual ICollection<ProjectPerKm> ProjectPerKms { get; set; }
    }
}