using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using BestBrightnesss.DataLogic.Reports;

namespace BestBrightnesss.DataLogic
{
    public class DefaultContext : DbContext
    {
        public DefaultContext()
        {

        }
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            //This is the connection String - Read from the environment variable.
            //optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString"));
        }

        //entities
        public DbSet<Sales.RecordSales> Sales { get; set; }
        public DbSet<Invetory.Invetory> Invetory { get; set; }
        public DbSet<Profile.Register> Register { get; set; }
        public DbSet<Profile.UserRole> UserRole { get; set; }
        public DbSet<Reports.DailySalesReports> Reports { get; set; }
        public DbSet<Reports.DailySalesReports> DailySalesReports { get; set; }
        public DbSet<Reports.InvetoryReport> InvetoryReport { get; set; }

    }
}
