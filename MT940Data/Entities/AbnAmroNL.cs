using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MT940Data.Entities
{
    public partial class AbnAmroNL : DbContext
    {
        /*
        public AbnAmroNL()
        {
        }
        */

        public AbnAmroNL(DbContextOptions<AbnAmroNL> options)
            : base(options)
        {
        }

        public virtual DbSet<BalanceType> BalanceType { get; set; }
        public virtual DbSet<MarkType> MarkType { get; set; }
        public virtual DbSet<Statement> Statement { get; set; }
        public virtual DbSet<StatementBalance> StatementBalance { get; set; }
        public virtual DbSet<StatementInformation> StatementInformation { get; set; }
        public virtual DbSet<StatementLine> StatementLine { get; set; }
        public virtual DbSet<StatementLineInformation> StatementLineInformation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BalanceType>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<MarkType>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Statement>(entity =>
            {
                entity.Property(e => e.AccountIdentification).IsUnicode(false);

                entity.Property(e => e.ChangedBy).HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.RelatedReference).IsUnicode(false);

                entity.Property(e => e.TransactionReferenceNumber).IsUnicode(false);
            });

            modelBuilder.Entity<StatementBalance>(entity =>
            {
                entity.Property(e => e.Currency).IsUnicode(false);

                entity.HasOne(d => d.MarkNavigation)
                    .WithMany(p => p.StatementBalance)
                    .HasForeignKey(d => d.Mark)
                    .HasConstraintName("FK_StatementBalance_MarkType");

                entity.HasOne(d => d.Statement)
                    .WithMany(p => p.StatementBalance)
                    .HasForeignKey(d => d.StatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementBalance_Statement");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.StatementBalance)
                    .HasForeignKey(d => d.Type)
                    .HasConstraintName("FK_StatementBalance_BalanceType");
            });

            modelBuilder.Entity<StatementInformation>(entity =>
            {
                entity.Property(e => e.AccountNumberOfPayer).IsUnicode(false);

                entity.Property(e => e.BankCodeOfPayer).IsUnicode(false);

                entity.Property(e => e.CompensationAmount).IsUnicode(false);

                entity.Property(e => e.CreditorReference).IsUnicode(false);

                entity.Property(e => e.CreditorsReferenceParty).IsUnicode(false);

                entity.Property(e => e.CustomerReference).IsUnicode(false);

                entity.Property(e => e.EndToEndReference).IsUnicode(false);

                entity.Property(e => e.JournalNumber).IsUnicode(false);

                entity.Property(e => e.MandateReference).IsUnicode(false);

                entity.Property(e => e.NameOfPayer).IsUnicode(false);

                entity.Property(e => e.OriginalAmount).IsUnicode(false);

                entity.Property(e => e.OriginatorsIdentificationCode).IsUnicode(false);

                entity.Property(e => e.PayersReferenceParty).IsUnicode(false);

                entity.Property(e => e.PostingText).IsUnicode(false);

                entity.Property(e => e.SepaRemittanceInformation).IsUnicode(false);

                entity.Property(e => e.UnstructuredRemittanceInformation).IsUnicode(false);

                entity.HasOne(d => d.Statement)
                    .WithMany(p => p.StatementInformation)
                    .HasForeignKey(d => d.StatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementBalance_StatementInformation");
            });

            modelBuilder.Entity<StatementLine>(entity =>
            {
                entity.Property(e => e.BankReference).IsUnicode(false);

                entity.Property(e => e.CustomerReference).IsUnicode(false);

                entity.Property(e => e.FundsCode).IsUnicode(false);

                entity.Property(e => e.SupplementaryDetails).IsUnicode(false);

                entity.Property(e => e.TransactionTypeIdCode).IsUnicode(false);

                entity.HasOne(d => d.Statement)
                    .WithMany(p => p.StatementLine)
                    .HasForeignKey(d => d.StatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementLine_Statement");
            });

            modelBuilder.Entity<StatementLineInformation>(entity =>
            {
                entity.Property(e => e.AccountNumberOfPayer).IsUnicode(false);

                entity.Property(e => e.BankCodeOfPayer).IsUnicode(false);

                entity.Property(e => e.CompensationAmount).IsUnicode(false);

                entity.Property(e => e.CreditorReference).IsUnicode(false);

                entity.Property(e => e.CreditorsReferenceParty).IsUnicode(false);

                entity.Property(e => e.CustomerReference).IsUnicode(false);

                entity.Property(e => e.EndToEndReference).IsUnicode(false);

                entity.Property(e => e.JournalNumber).IsUnicode(false);

                entity.Property(e => e.MandateReference).IsUnicode(false);

                entity.Property(e => e.NameOfPayer).IsUnicode(false);

                entity.Property(e => e.OriginalAmount).IsUnicode(false);

                entity.Property(e => e.OriginatorsIdentificationCode).IsUnicode(false);

                entity.Property(e => e.PayersReferenceParty).IsUnicode(false);

                entity.Property(e => e.PostingText).IsUnicode(false);

                entity.Property(e => e.SepaRemittanceInformation).IsUnicode(false);

                entity.Property(e => e.UnstructuredRemittanceInformation).IsUnicode(false);

                entity.HasOne(d => d.StatementLine)
                    .WithMany(p => p.StatementLineInformation)
                    .HasForeignKey(d => d.StatementLineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementBalance_StatementLineInformation");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
