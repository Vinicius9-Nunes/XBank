using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBank.Domain.Entities;

namespace XBank.Repository.Persistence
{
    public class EntityConfiguration
    {
        public void AccountConfigure(ModelBuilder builder)
        {
            builder.Entity<AccountEntity>(entity =>
            {
                entity.HasKey(account => account.Id);
                entity.Property(account => account.Id).ValueGeneratedOnAdd();
                entity.Property(account => account.HolderName).HasMaxLength(100).IsRequired();
                entity.Property(account => account.HolderCpf).HasMaxLength(11).IsRequired();
                entity.HasIndex(account => account.HolderCpf).IsUnique();
                entity.Property(account => account.DueDate).IsRequired();
                entity.Property(account => account.Balance).IsRequired();
                entity.Property(account => account.AccountStatus).IsRequired();
                entity.Property(account => account.CreatAt).IsRequired();
                
            });
        }

        public void TransactionConfigure(ModelBuilder builder)
        {
            builder.Entity<TransactionEntity>(entity =>
            {
                entity.HasKey(trans => trans.Id);
                entity.Property(trans => trans.Id).ValueGeneratedOnAdd();
                entity.Property(trans => trans.Description).HasMaxLength(70);
                entity.Property(trans => trans.Amount).IsRequired();
                entity.Property(trans => trans.TransactionDate).IsRequired();
                entity.Property(trans => trans.TransactionType).IsRequired();
                entity.Property(trans => trans.CreatAt).IsRequired();
            });
        }
    }
}
