﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreelancerArticle.Server
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FreelancerArticleBaseEntities : DbContext
    {
        public FreelancerArticleBaseEntities()
            : base("name=FreelancerArticleBaseEntities")
        {
    base.Configuration.ProxyCreationEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Freelancer> Freelancer { get; set; }
        public virtual DbSet<Messenger> Messenger { get; set; }
        public virtual DbSet<Moderator> Moderator { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Wallets> Wallets { get; set; }
    }
}
