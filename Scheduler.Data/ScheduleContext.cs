using System;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using Scheduler.Domain;

namespace Scheduler.Data
{
    public class ScheduleContext : DbContext
    {

        public void Migrate()
        {
            this.Database.Migrate();
        }


        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleNote> ScheduleNotes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<TeamCategory> TeamCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TeamUser> TeamUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Warranty> Warranties { get; set; }
        public DbSet<WarrantyNote> WarrantyNotes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "SchedulerDatabase.db");

            optionsBuilder.UseSqlite($"Data Source={path}");
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
             .Where(e => e.State == EntityState.Added ||
                         e.State == EntityState.Modified))
            {
                entry.Property("ChangeDate").CurrentValue = DateTime.UtcNow;
            }
            return base.SaveChanges();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region set disinct keys

            modelBuilder.Entity<User>().HasKey(x => x.UserId);
            modelBuilder.Entity<UserDetail>().HasKey(x => x.UserDetailId);
            modelBuilder.Entity<Team>().HasKey(x => x.TeamId);
            modelBuilder.Entity<Location>().HasKey(x => x.LocationId);
            modelBuilder.Entity<TeamUser>().HasKey(x => x.TeamUserId);
            //modelBuilder.Entity<TeamUser>().HasKey(x => new { x.TeamId, x.UserId });
            modelBuilder.Entity<TeamCategory>().HasKey(x => x.TeamCategoryId);
            //modelBuilder.Entity<TeamCategory>().HasKey(x => new { x.TeamId, x.CategoryId });
            modelBuilder.Entity<Category>().HasKey(x => x.CategoryId);
            modelBuilder.Entity<Schedule>().HasKey(x => x.ScheduleId);
            modelBuilder.Entity<ScheduleNote>().HasKey(x => x.ScheduleNoteId);
            modelBuilder.Entity<Warranty>().HasKey(x => x.WarrantyId);
            modelBuilder.Entity<WarrantyNote>().HasKey(x => x.WarrantyNoteId);

            #endregion


            #region set indexes

            modelBuilder.Entity<User>().HasIndex(x => x.UserId);
            modelBuilder.Entity<User>().HasIndex(x => x.UserName);
            modelBuilder.Entity<UserDetail>().HasIndex(x => x.UserDetailId);
            modelBuilder.Entity<Team>().HasIndex(x => x.TeamId);
            modelBuilder.Entity<Team>().HasIndex(x => x.LocationId);
            modelBuilder.Entity<Location>().HasIndex(x => x.LocationId);
            modelBuilder.Entity<TeamUser>().HasIndex(x => x.TeamUserId);
            modelBuilder.Entity<TeamUser>().HasIndex(x => new { x.TeamId, x.UserId });
            modelBuilder.Entity<TeamCategory>().HasIndex(x => x.TeamCategoryId);
            modelBuilder.Entity<TeamCategory>().HasIndex(x => new { x.TeamId, x.CategoryId });
            modelBuilder.Entity<Category>().HasIndex(x => x.CategoryId);
            modelBuilder.Entity<Schedule>().HasIndex(x => x.ScheduleId);
            modelBuilder.Entity<ScheduleNote>().HasIndex(x => x.ScheduleNoteId);
            modelBuilder.Entity<UserDetail>().HasIndex(x => x.UserId);
            modelBuilder.Entity<Schedule>().HasIndex(x => new { x.UserId, x.StartDate, x.EndDate });
            modelBuilder.Entity<Schedule>().HasIndex(x => new { x.TeamId, x.StartDate, x.EndDate });
            modelBuilder.Entity<ScheduleNote>().HasIndex(x => x.ScheduleId);
            modelBuilder.Entity<Warranty>().HasIndex(x => x.WarrantyId);
            modelBuilder.Entity<Warranty>().HasIndex(x => new { x.UserId, x.StartDate, x.EndDate });
            modelBuilder.Entity<Warranty>().HasIndex(x => new { x.TeamId, x.StartDate, x.EndDate });
            modelBuilder.Entity<WarrantyNote>().HasIndex(x => x.WarrantyNoteId);
            modelBuilder.Entity<WarrantyNote>().HasIndex(x => x.WarrantyId);

            #endregion

            #region set default values

           
            #endregion

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Ignore("IsDirty");
            }
        }

    }
}
