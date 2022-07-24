using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DebitiNBE_API.Models.DB
{
    public partial class DebitiContext : DbContext
    {
        public DebitiContext()
        {
        }

        public DebitiContext(DbContextOptions<DebitiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Friendlist> Friendlist { get; set; }
        public virtual DbSet<Paymentrequest> Paymentrequest { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;user id=root;password=toor;persistsecurityinfo=True;database=debiti");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friendlist>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.UserId1 })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_user_has_user_user_idx");

                entity.HasIndex(e => e.UserId1)
                    .HasName("fk_user_has_user_user1_idx");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FriendlistUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_has_user_user");

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.FriendlistUserId1Navigation)
                    .HasForeignKey(d => d.UserId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_has_user_user1");
            });

            modelBuilder.Entity<Paymentrequest>(entity =>
            {
                entity.HasIndex(e => e.RequestId)
                    .HasName("fk_PaymentRequest_Request1_idx");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.Paymentrequest)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PaymentRequest_Request1");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasIndex(e => e.IdMandante)
                    .HasName("fk_Request_user1_idx");

                entity.HasIndex(e => e.IdRicevente)
                    .HasName("fk_Request_user2_idx");

                entity.HasOne(d => d.IdMandanteNavigation)
                    .WithMany(p => p.RequestIdMandanteNavigation)
                    .HasForeignKey(d => d.IdMandante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Request_user1");

                entity.HasOne(d => d.IdRiceventeNavigation)
                    .WithMany(p => p.RequestIdRiceventeNavigation)
                    .HasForeignKey(d => d.IdRicevente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Request_user2");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("Email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("Username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Lastname).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
