using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Mapping;

namespace XBank.Tests.Core
{
    public static class InstanceFactory
    {
        private static IMapper _mapper;
        public static IMapper GetValidMapper()
        {
            if (_mapper == null)
            {
                MapperConfiguration configuration = new MapperConfiguration(config =>
                {
                    config.AddProfile<AccountProfile>();
                    config.AddProfile<TransactionProfile>();
                });

                IMapper mapper = configuration.CreateMapper();
                _mapper = mapper;
            }

            return _mapper;
        }
    }
}
