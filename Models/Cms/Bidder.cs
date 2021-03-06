// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class Bidder
    {
        public Bidder()
        {
            Bids = new HashSet<Bid>();
            CacReleasedTenders = new HashSet<CacReleasedTender>();
            TacReleasedTenders = new HashSet<TacReleasedTender>();
        }

        public int BidderId { get; set; }
        public int? TenderId { get; set; }
        public int? FirmId { get; set; }
        public decimal? BidAmount { get; set; }
        public decimal? TotalofBills { get; set; }
        public decimal? Sps { get; set; }
        public decimal? Dayworks { get; set; }
        public decimal? Contingency { get; set; }
        public decimal? Vat { get; set; }
        public bool? IsWinner { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool? IsBoqSet { get; set; }
        public decimal? DiscountRate { get; set; }
        public bool IsDiscountApplied { get; set; }
        public bool IsDiscountAppliedToDayworks { get; set; }
        public bool IsDiscountAppliedToPs { get; set; }

        public virtual Firm Firm { get; set; }
        public virtual Tender Tender { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<CacReleasedTender> CacReleasedTenders { get; set; }
        public virtual ICollection<TacReleasedTender> TacReleasedTenders { get; set; }
    }
}