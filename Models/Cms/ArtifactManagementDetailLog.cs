// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class ArtifactManagementDetailLog
    {
        public int LogDetailId { get; set; }
        public int LogId { get; set; }
        public string RelatedArtifactType { get; set; }
        public int RelatedArtifactKey { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
        public int? MoveTargetKey { get; set; }
        public string MoveTargetName { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }

        public virtual ArtifactManagementLog Log { get; set; }
    }
}