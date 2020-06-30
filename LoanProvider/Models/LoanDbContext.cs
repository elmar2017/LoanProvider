using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LoanProvider.Models
{
    public partial class LoanDbContext : DbContext
    {
        public LoanDbContext()
        {
        }

        public LoanDbContext(DbContextOptions<LoanDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Loan> Loan { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LoanDb;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientUniqueId);

                entity.Property(e => e.ClientUniqueId)
                    .HasColumnName("ClientUniqueID")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TelephoneNr).HasMaxLength(10);
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasKey(e => e.InvoiceNr)
                    .HasName("PK_Invoices_1");

                entity.Property(e => e.InvoiceNr).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.LoanId).HasColumnName("LoanID");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.LoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Loan");
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.ClientUniqueId)
                    .HasColumnName("ClientUniqueID")
                    .HasMaxLength(50);

                entity.Property(e => e.InterestRate).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.PayoutDate).HasColumnType("datetime");

                entity.HasOne(d => d.ClientUnique)
                    .WithMany(p => p.Loan)
                    .HasForeignKey(d => d.ClientUniqueId)
                    .HasConstraintName("FK_Loan_Client");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
