﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CavityQuantities.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class M2K_DATA_WHSEEntities : DbContext
    {
        public M2K_DATA_WHSEEntities()
            : base("name=M2K_DATA_WHSEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<IM_IM> IM_IM { get; set; }
        public virtual DbSet<vSA_Sales> vSA_Sales { get; set; }
    }
}
