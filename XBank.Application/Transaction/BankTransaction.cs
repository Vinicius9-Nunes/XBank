using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Application.Services.Core;
using XBank.Domain.Entities;
using XBank.Domain.Enums.Transaction;
using XBank.Domain.Interfaces.Core;
using XBank.Domain.Models.DTOs;

namespace XBank.Application.Transaction
{
    public abstract class BankTransaction
    {
        protected IConfiguration _configuration;
        protected readonly IMapper _mapper;
        protected BankTransaction(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public abstract Task<bool> MakeTransaction(TransactionEntity transaction);
        protected async Task<AccountDTO> GetAccount(long id)
        {
            ILocalRequestHttp<ModelDTO> localRequestHttp = new LocalRequestHttp<ModelDTO>();
            string baseUrl = UtilitiesLibrary.GetSectionFromSettings(_configuration, "EndPoints", "BaseEndPointAccount");
            string fullUrl = string.Concat(baseUrl);
            return await localRequestHttp.Get<AccountDTO>(fullUrl, id.ToString());
        }


    }
}
