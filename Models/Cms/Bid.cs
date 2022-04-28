﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class Bid
    {
        public Bid()
        {
            BidSeries = new HashSet<BidSeries>();
        }

        public int BidId { get; set; }
        public int? TenderId { get; set; }
        public int? EngineeringEstimateId { get; set; }
        public int? BillNumber { get; set; }
        public string BillDescription { get; set; }
        public int? BidderId { get; set; }
        public bool? IsQuantityChanged { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public virtual Bidder Bidder { get; set; }
        public virtual EngineeringEstimate EngineeringEstimate { get; set; }
        public virtual Tender Tender { get; set; }
        public virtual ICollection<BidSeries> BidSeries { get; set; }
    }
}