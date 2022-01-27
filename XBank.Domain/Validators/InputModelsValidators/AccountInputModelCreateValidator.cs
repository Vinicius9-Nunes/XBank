using FluentValidation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Models.InputModel;

namespace XBank.Domain.Validators.InputModelsValidators
{
    public class AccountInputModelCreateValidator : AbstractValidator<AccountInputModelCreate>
    {
        private readonly IConfiguration _configuration;
        private const string MAX_DUE_DATE_PARAMETER = "MAX_DUE_DATE";
        private int _maxDueDate;

        public AccountInputModelCreateValidator(IConfiguration configuration)
        {
            _configuration = configuration;
            GetMaxDueDateFromSettings();

            RuleFor(account => account.DueDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("A Data de vencimento deve ser informada.");
            RuleFor(account => account.DueDate)
                .Custom((list, context) =>
                {
                    if (!GetMaxDueDateFromSettings())
                        context.AddFailure("Não foi possivel recuperar o dia maximo de vencimento.");
                });
            RuleFor(account => account.DueDate)
                .LessThanOrEqualTo(_maxDueDate)
                .WithMessage($"A Data de vencimento deve ser menor ou igual a {_maxDueDate}.");

            RuleFor(account => account.HolderName)
                .NotEmpty()
                .NotNull()
                .WithMessage("O nome do titular deve ser informado.");
            RuleFor(account => account.HolderName)
                .Must(holderName => holderName.Length > 3)
                .WithMessage("O nome deve ser maior que 3 caracteres.");

            RuleFor(account => account.HolderCpf)
                .NotNull()
                .NotEmpty()
                .WithMessage("O CPF do titular deve ser informado.");
            RuleFor(account => account.HolderCpf)
                .Must(cpf => cpf.RemoveCpfLetters().Length == 11)
                .WithMessage("O CPF do titular deve ter 11 digitos.");
            RuleFor(account => account.HolderCpf)
                .Custom((list, context) =>
                {
                    list.RemoveCpfLetters();
                    if (!list.IsValidCPF())
                        context.AddFailure("Não foi possivel recuperar o dia maximo de vencimento.");
                });
        }

        private bool GetMaxDueDateFromSettings()
        {
            _maxDueDate = new int();
            return int.TryParse(_configuration.GetSection(MAX_DUE_DATE_PARAMETER).Value, out _maxDueDate);
        }
    }
}
