﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeBot.Core.Model.Context
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HomeBotEntities : DbContext, IHomeBotContext
    {
        public HomeBotEntities()
            : base("name=HomeBotEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ReceivedEvent> ReceivedEvents { get; set; }
        public virtual DbSet<SentEvent> SentEvents { get; set; }
        public virtual DbSet<EventHistory> EventHistories { get; set; }
        public virtual DbSet<EventState> EventStates { get; set; }
    }
}
