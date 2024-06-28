using Microsoft.EntityFrameworkCore;

namespace SudLife_ProtectShield.APILayer.Database.Context
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Model.ClsEmployee> Employees { get; set; }
    }
}
