﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BHShows.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CinemazEntities : DbContext
    {
        public CinemazEntities()
            : base("name=CinemazEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cinema_T> Cinema_T { get; set; }
        public virtual DbSet<City_T> City_T { get; set; }
        public virtual DbSet<Country_T> Country_T { get; set; }
        public virtual DbSet<Movies_T> Movies_T { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Language_T> Language_T { get; set; }
        public virtual DbSet<Media_T> Media_T { get; set; }
        public virtual DbSet<ShowTime_T> ShowTime_T { get; set; }
    }
}