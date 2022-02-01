using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;
using XBank.Domain.Models.DTOs.Transactions;
using XBank.Domain.Models.InputModel;

namespace XBank.Domain.Mapping
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionEntity, TransactionInputModelCreate>()
                .ReverseMap();

            CreateMap<TransactionEntity, TransactionDTO>()
                .ReverseMap();

            CreateMap<TransactionEntity, TransactionCreateDTO>()
                .ReverseMap();
        }
    }
}
