﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AflyatunovAutoservice
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShaurmaEntities : DbContext
    {
        private static ShaurmaEntities _context;
        public static ShaurmaEntities GetContext()
        {
            if(_context == null)
                _context = new ShaurmaEntities();   
            return _context;
        }
        public ShaurmaEntities()
            : base("name=ShaurmaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Entering> Entering { get; set; }
        public virtual DbSet<Posishen> Posishen { get; set; }
    }
}
