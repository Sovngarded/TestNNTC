using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNNTC;
using TestNNTC.DAL.Entities;


namespace TestNNTC.DAL
{
    public class TestNNTCDbContext : DbContext
    {
       
        public TestNNTCDbContext(DbContextOptions<TestNNTCDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<CatalogueDataEntity> CatalogueList { get; set; }
        public DbSet<CatalogueDataProduct> CatalogueListProducts { get; set; }


    }
    
}
