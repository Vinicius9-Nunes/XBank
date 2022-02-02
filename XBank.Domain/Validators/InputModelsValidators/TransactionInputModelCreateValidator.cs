using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Enums.Transaction;
using XBank.Domain.Models.InputModel;

namespace XBank.Domain.Validators.InputModelsValidators
{
    public class TransactionInputModelCreateValidator : AbstractValidator<TransactionInputModelCreate>
    {
        public TransactionInputModelCreateValidator()
        {
            RuleFor(transaction => transaction.TransactionType)
                .NotEmpty()
                .WithMessage("O tipo de transação deve ser informado.");
            RuleFor(transaction => transaction.TransactionType)
                .IsInEnum()
                .WithMessage("O tipo de transação informado não é valido.");
            RuleFor(transaction => transaction.Amount)
                .NotNull()
                .NotEmpty()
                .WithMessage("Você deve informar uma valor para essa transação.");
        }
    }
}
