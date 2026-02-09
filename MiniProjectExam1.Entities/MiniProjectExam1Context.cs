using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MiniProjectExam1.Entities;

public partial class MiniProjectExam1Context : DbContext
{
    public MiniProjectExam1Context()
    {
    }

    public MiniProjectExam1Context(DbContextOptions<MiniProjectExam1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<BookedTicket> BookedTickets { get; set; }

    public virtual DbSet<BookedTicketDetail> BookedTicketDetails { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=MiniProjectExam1;Trusted_Connection=True;Encrypt=False");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookedTicket>(entity =>
        {
            entity.HasKey(e => e.BookedTicketId).HasName("PK__BookedTi__9110472FD53CA50E");

            entity.Property(e => e.BookedTicketId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BookingDate).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<BookedTicketDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookedTi__3214EC07059AEAB8");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CodeTicket)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.BookedTicket).WithMany(p => p.BookedTicketDetails)
                .HasForeignKey(d => d.BookedTicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookedTic__Booke__5165187F");

            entity.HasOne(d => d.CodeTicketNavigation).WithMany(p => p.BookedTicketDetails)
                .HasForeignKey(d => d.CodeTicket)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookedTic__CodeT__52593CB8");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.CodeTicket).HasName("PK__Tickets__E7BD581B19ACB906");

            entity.Property(e => e.CodeTicket)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CategoryTicket)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NameTicket)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
