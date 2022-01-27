using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Application.Services;
using XBank.Domain.Interfaces;
using XBank.Domain.Models.InputModel;
using XBank.Domain.Validators.InputModelsValidators;

namespace XBank.CrossCutting.Configuration
{
    public static class ConfigureService
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IValidator<TransactionInputModelCreate>, TransactionInputModelCreateValidator>();
            services.AddTransient<IValidator<AccountInputModelCreate>, AccountInputModelCreateValidator>();
            services.AddTransient<IValidator<AccountInputModelUpdate>, AccountInputModelUpdateValidator>();
        }
    }
}
