// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class Claim
    {
        public Claim()
        {
            Correspondences = new HashSet<Correspondence>();
        }

        public int? ContractId { get; set; }
        public int ClaimId { get; set; }
        public string ClaimNumber { get; set; }
        public string CostOrTime { get; set; }
        public string ClaimOrDetermination { get; set; }
        public string ContractorOrEra { get; set; }
        public string Description { get; set; }
        public string ReasonForIssue { get; set; }
        public decimal? ContractorEstimatedAdditionalCost { get; set; }
        public decimal? ErEstimatedAdditionalCost { get; set; }
        public DateTime? DateErApproved { get; set; }
        public decimal? DreEstimatedAdditionalCost { get; set; }
        public DateTime? DateDreApproved { get; set; }
        public decimal? ArbitratorDecision { get; set; }
        public bool IsReferedToDre { get; set; }
        public bool IsReferedToArbitration { get; set; }
        public string ContractClause { get; set; }
        public int? RelatedVo { get; set; }
        public int? ContractorTimeExtension { get; set; }
        public int? ErtimeExtension { get; set; }
        public int? DretimeExtension { get; set; }
        public int? ArbitratorTimeExtension { get; set; }
        public decimal? EraestimatedAdditionalCost { get; set; }
        public int? EratimeExtension { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IncludeInFinancialProgress { get; set; }
        public string CreatedBy { get; set; }

        public virtual Contract Contract { get; set; }
        public virtual ClaimCategory ReasonForIssueNavigation { get; set; }
        public virtual VariationOrder RelatedVoNavigation { get; set; }
        public virtual ICollection<Correspondence> Correspondences { get; set; }
    }
}