using Microsoft.EntityFrameworkCore;
using Focus_App.Models;

namespace Focus_App.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<PomodoroSession> PomodoroSessions { get; set; }
        public DbSet<FocusRoom> FocusRooms { get; set; }
        public DbSet<RoomParticipant> RoomParticipants { get; set; }
        public DbSet<FocusInsight> FocusInsights { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<TaskItem>().ToTable("Tasks");
            modelBuilder.Entity<PomodoroSession>().ToTable("PomodoroSessions");
            modelBuilder.Entity<FocusRoom>().ToTable("FocusRooms");
            modelBuilder.Entity<RoomParticipant>().ToTable("RoomParticipants");
            modelBuilder.Entity<FocusInsight>().ToTable("FocusInsights");
        }
    }
}