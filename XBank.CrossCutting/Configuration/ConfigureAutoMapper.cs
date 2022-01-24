using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Mapping;

namespace XBank.CrossCutting.Configuration
{
    public static class ConfigureAutoMapper
    {
        public static void Configure(IServiceCollection services)
        {
            MapperConfiguration configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<AccountProfile>();
                config.AddProfile<TransactionProfile>();
            });

            IMapper mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
