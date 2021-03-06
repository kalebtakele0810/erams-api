// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class VariationOrder
    {
        public VariationOrder()
        {
            Claims = new HashSet<Claim>();
            Correspondences = new HashSet<Correspondence>();
        }

        public int? ContractId { get; set; }
        public int VariationOrderId { get; set; }
        public string VariationOrderNumber { get; set; }
        public string ReasonForIssue { get; set; }
        public string FurtherDetail { get; set; }
        public string Description { get; set; }
        public decimal? ContractorEstimatedAdditionalCost { get; set; }
        public decimal? ErEstimatedAdditionalCost { get; set; }
        public DateTime? DateErApproved { get; set; }
        public decimal? DreEstimatedAdditionalCost { get; set; }
        public DateTime? DateDreApproved { get; set; }
        public decimal? ArbitratorDecision { get; set; }
        public string NecessaryOrAppropriate { get; set; }
        public bool IsReferedToDre { get; set; }
        public bool IsReferedToArbitration { get; set; }
        public string Activity { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual ConstructionActivityLookup ActivityNavigation { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual VariationOrderCategory ReasonForIssueNavigation { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
        public virtual ICollection<Correspondence> Correspondences { get; set; }
    }
}