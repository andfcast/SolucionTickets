using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using TicketManager.Domain.Entities;

namespace TicketManager.Infrastructure.Persistence
{
    public class TicketDbContext : DbContext
    {        
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options) {             
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Configure SQLite to use a local file
            //options.UseSqlite($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TicketDB.db")}");
            options.UseSqlite($"Data Source=DB\\TicketDB.db");
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(a => a.Category)
                .WithMany()
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(a => a.Status)
                .WithMany()
                .HasForeignKey(a => a.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TicketComment>()
                .HasOne(a => a.Ticket)
                .WithMany()
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TicketStatus>().HasData(
               new TicketStatus { Id = 1, Description = "Nuevo" },
               new TicketStatus { Id = 2, Description = "En proceso" },
               new TicketStatus { Id = 3, Description = "Solucionado" },
               new TicketStatus { Id = 4, Description = "Cerrado" }
            );

            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Description = "Falla" },
               new Category { Id = 2, Description = "Informativo" },
               new Category { Id = 3, Description = "Configuración" },
               new Category { Id = 4, Description = "Otro" }
            );

            modelBuilder.Entity<User>().HasData(
               new User { Id = 1, FullName = "Andrés Castañeda", UserName = "acastaneda" },
               new User { Id = 2, FullName = "Felipe Díaz", UserName = "fdiaz" }               
            );

        }

    }
}
