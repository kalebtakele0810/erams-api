// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class BudetProblem
    {
        public int ContractId { get; set; }
        public string ContractNumber { get; set; }
        public string ContractName { get; set; }
        public int? EngineeringEstimateId { get; set; }
        public int? SubmittedEqPq { get; set; }
        public string InvitedBidders { get; set; }
        public int? SubmittedBids { get; set; }
        public DateTime? FinancialBidOpeningDate { get; set; }
        public DateTime? FinancialEvaluationApprovalDate { get; set; }
        public int? OpenedBids { get; set; }
        public string ContractType { get; set; }
        public decimal? Budget { get; set; }
        public int? BudgetYear { get; set; }
        public bool? IsAwarded { get; set; }
    }
}