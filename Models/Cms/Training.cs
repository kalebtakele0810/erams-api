// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class Training
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public int TotalNumberOfParticipant { get; set; }
        public int NumberOfFemales { get; set; }
        public int NumberOfMales { get; set; }
        public DateTime TrainingDate { get; set; }
        public string Directorate { get; set; }
        public string TrainingGivenBy { get; set; }
        public string ProjectName { get; set; }
        public string TrainingPlace { get; set; }
        public string RegisteredBy { get; set; }
        public string AprovedBy { get; set; }
    }
}