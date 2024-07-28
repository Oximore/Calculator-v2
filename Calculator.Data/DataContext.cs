using Calculator.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculator.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Operation> Operations { get; set; }

        public DataContext(DbContextOptions<DataContext> option) : base(option)
        {
            Database.EnsureCreated();
        }

        //public DataContext()
        //{
        //    Database.EnsureCreated();
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    string filename = "calculator_data.db3";
        //    optionsBuilder.UseSqlite($"DataSource = {filename}");
        //}
    }
}