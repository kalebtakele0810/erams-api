﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ERAManagementSystem.Models
{
    public partial class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? PropertyTypeId { get; set; }

        public virtual PropertyType PropertyType { get; set; }
    }
}