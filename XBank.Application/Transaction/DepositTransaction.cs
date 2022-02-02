using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Application.Services.Core;
using XBank.Domain.Entities;
using XBank.Domain.Interfaces.Core;
using XBank.Domain.Models.DTOs;

namespace XBank.Application.Transaction
{
    public class DepositTransaction : BankTransaction
    {
        public DepositTransaction(IConfiguration configuration, IMapper mapper) : base(configuration, mapper) { }
        public async override Task<bool> MakeTransaction(TransactionEntity transaction)
        {
            ILocalRequestHttp<AccountUpdateBalanceDTO> localRequestHttp = new LocalRequestHttp<AccountUpdateBalanceDTO>();
            string baseUrl = UtilitiesLibrary.GetSectionFromSettings(_configuration, "EndPoints", "BaseEndPointAccount");
            string fullUrl = string.Concat(baseUrl, transaction.AccountEntityId, "/AddMoney?value=", transaction.Amount);
            AccountUpdateBalanceDTO accountUpdateBalanceDTO = await localRequestHttp.PostQueryString<AccountUpdateBalanceDTO>(fullUrl);
            throw new Exception();
        }
    }
}
