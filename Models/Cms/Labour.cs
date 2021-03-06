// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class Labour
    {
        public Labour()
        {
            Equipment = new HashSet<Equipment>();
            LabourCostLocals = new HashSet<LabourCostLocal>();
            LabourCosts = new HashSet<LabourCost>();
        }

        public int LabourId { get; set; }
        public string LaborClassCode { get; set; }
        public string Classification { get; set; }
        public string Trade { get; set; }
        public decimal? MonthlyAvgSalary { get; set; }
        public string SalaryGrade { get; set; }
        public decimal? IndexedHourlyRate { get; set; }
        public string Note { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IsMaintenanceLabour { get; set; }

        public virtual LabourClassification ClassificationNavigation { get; set; }
        public virtual LabourClassCode LaborClassCodeNavigation { get; set; }
        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<LabourCostLocal> LabourCostLocals { get; set; }
        public virtual ICollection<LabourCost> LabourCosts { get; set; }
    }
}