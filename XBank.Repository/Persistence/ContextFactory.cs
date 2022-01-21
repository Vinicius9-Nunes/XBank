using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBank.Repository.Persistence
{
    public class ContextFactory : IDbContextFactory<XBankDbContext>
    {

        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
         
        public ContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Default");
        }

        public XBankDbContext CreateDbContext()
        {
            string connectionString = _connectionString;
            DbContextOptionsBuilder<XBankDbContext> optionBuilder = new DbContextOptionsBuilder<XBankDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            return new XBankDbContext(optionBuilder.Options, _configuration);
        }
    }
}
