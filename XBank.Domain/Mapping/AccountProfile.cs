using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Models.InputModel;

namespace XBank.Domain.Mapping
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountEntity, AccountInputModelCreate>()
                .ReverseMap();

            CreateMap<AccountEntity, AccountInputModelUpdate>()
                .ReverseMap();
        }
    }
}
