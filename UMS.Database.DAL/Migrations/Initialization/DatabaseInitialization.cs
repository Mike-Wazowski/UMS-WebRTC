using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Database.DAL.Implementations;

namespace UMS.Database.DAL.Migrations.Initialization
{
    public class DatabaseInitialization : System.Data.Entity.CreateDatabaseIfNotExists<UmsDbContext>
    {
        protected override void Seed(UmsDbContext context)
        {
        }
    }
}
