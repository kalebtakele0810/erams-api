﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class BidSeries
    {
        public BidSeries()
        {
            BidDivisions = new HashSet<BidDivision>();
        }

        public int BidSeriesId { get; set; }
        public int? BidId { get; set; }
        public string SeriesCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string SpecificationName { get; set; }

        public virtual Bid Bid { get; set; }
        public virtual Series S { get; set; }
        public virtual ICollection<BidDivision> BidDivisions { get; set; }
    }
}