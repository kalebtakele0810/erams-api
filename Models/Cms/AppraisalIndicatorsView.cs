// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class AppraisalIndicatorsView
    {
        public int IssueId { get; set; }
        public int PhaseId { get; set; }
        public string Phase { get; set; }
        public string Head { get; set; }
        public string Reference { get; set; }
        public string Issue { get; set; }
        public string Indicator { get; set; }
        public bool? Designer { get; set; }
        public bool? Supervisor { get; set; }
        public bool? Contractor { get; set; }
        public bool? Monitor { get; set; }
        public bool? Evaluation { get; set; }
        public int HeadId { get; set; }
        public string Keywords { get; set; }
        public bool? Era1 { get; set; }
        public bool? Era2 { get; set; }
        public bool? MaintenanceSupervisor { get; set; }
        public bool? MaintenanceContractor { get; set; }
        public bool? DesignBuildDesign { get; set; }
        public bool? DesignBuildBuild { get; set; }
        public bool? DesignBuildEmployerRep { get; set; }
    }
}