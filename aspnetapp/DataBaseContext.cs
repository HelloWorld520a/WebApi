using System;
using System.Collections.Generic;
using aspnetapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace aspnetapp
{
    public partial class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
        }
        public DbSet<Counter> Counters { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var username = "root";//Environment.GetEnvironmentVariable("MYSQL_USERNAME");
                var password = "Lsp19920724";//Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
                var addressParts = "10.30.106.77:3306".Split(':');//"sh-cynosdbmysql-grp-5uyirrg6.sql.tencentcdb.com:21012".Split(':');//Environment.GetEnvironmentVariable("MYSQL_ADDRESS")?.Split(':');//"10.30.106.77:3306".Split(':');
                var host = addressParts?[0];
                var port = addressParts?[1];
                var connstr = $"server={host};port={port};user={username};password={password};database=aspnet_demo";
                optionsBuilder.UseMySql(connstr, Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.18-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci").HasCharSet("utf8");
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        // 这个方法用于调用存储过程
        public List<Product> GetEmployeeDetails(string name)
        {
            var res = this.Set<Product>().FromSqlInterpolated($"SELECT * FROM T_Product where name ={name}").ToList();//FromSqlRaw($"SELECT * FROM T_Product", employeeId).ToList();
            return res;
        }
    }
}
