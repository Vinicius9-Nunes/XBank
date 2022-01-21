using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;

namespace XBank.Repository.Persistence
{
    public class XBankDbContext : DbContext
    {
        private EntityConfiguration entityConfiguration;
        private readonly IConfiguration _configuration;
        public XBankDbContext(DbContextOptions<XBankDbContext> options, IConfiguration configuration) : base(options)
        {
            this.entityConfiguration = new EntityConfiguration();
            this._configuration = configuration;
        }

        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("XBank");

            entityConfiguration.AccountConfigure(modelBuilder);
            entityConfiguration.TransactionConfigure(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
