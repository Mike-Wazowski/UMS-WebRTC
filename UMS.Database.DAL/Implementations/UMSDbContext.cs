using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Database.Models;

namespace UMS.Database.DAL.Implementations
{
    public class UmsDbContext : DbContext, IUmsDbContext
    {
        public IDbSet<User> Users { get; set; }

        public UmsDbContext() : base("name=UmsDbContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
