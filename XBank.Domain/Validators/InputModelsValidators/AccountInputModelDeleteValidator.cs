using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Models.InputModel;

namespace XBank.Domain.Validators.InputModelsValidators
{
    public class AccountInputModelDeleteValidator : AbstractValidator<AccountInputModelDelete>
    {
        public AccountInputModelDeleteValidator()
        {
            RuleFor(account => account.AccountStatus)
                .NotEmpty()
                .NotNull()
                .IsInEnum()
                .WithMessage("O status da conta deve ser informado.");

            RuleFor(account => account.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("O id da conta deve ser informado.");
        }
    }
}
