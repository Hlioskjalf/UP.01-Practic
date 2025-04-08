using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using Zadanie9.Models;

namespace Zadanie9.Models
{
    public partial class ShopModel : DbContext
    {
        internal object Database;

        public ShopModel()
            : base("name=ShopModel")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<Sell> Sells { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}