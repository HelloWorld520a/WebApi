using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace aspnetapp
{
    public partial class CounterContext : DbContext
    {
        public CounterContext()
        {
        }
        public DbSet<Counter> Counters { get; set; } = null!;
        public CounterContext(DbContextOptions<CounterContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var username = "root";//Environment.GetEnvironmentVariable("MYSQL_USERNAME");
                var password = "Lsp19920724";//Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
                var addressParts = "10.30.106.77:3306".Split(':');//"sh-cynosdbmysql-grp-5uyirrg6.sql.tencentcdb.com:21012".Split(':');//Environment.GetEnvironmentVariable("MYSQL_ADDRESS")?.Split(':');
                var host = addressParts?[0];
                var port = addressParts?[1];
                var connstr = $"server={host};port={port};user={username};password={password};database=aspnet_demo";
                optionsBuilder.UseMySql(connstr, Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.18-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            modelBuilder.Entity<Counter>().ToTable("Counters");
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
