using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Interfaces.Repository;
using XBank.Repository.Repository;

namespace XBank.CrossCutting.Configuration
{
    public static class ConfigureRepository
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
        }
    }
}
