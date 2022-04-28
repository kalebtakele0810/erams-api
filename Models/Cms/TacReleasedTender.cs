﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class TacReleasedTender
    {
        public int ReleaseId { get; set; }
        public int? TenderId { get; set; }
        public decimal? TenderValue { get; set; }
        public int? WinningBidderId { get; set; }
        public string MinuteReference { get; set; }
        public DateTime? DateReleased { get; set; }
        public string ReleasedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public virtual Tender Tender { get; set; }
        public virtual Bidder WinningBidder { get; set; }
    }
}