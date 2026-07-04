using Microsoft.EntityFrameworkCore;
using HRMS.API.Models;

namespace HRMS.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User {get; set;}
        public DbSet<EmployeeProfile> EmployeeProfile { get; set; } // Change to singular
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<AttendanceLog> AttendanceLogs { get; set; }
        public DbSet<Payslip> Payslips { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: Configure specific constraints here
            // Example: modelBuilder.Entity<EmployeeProfile>().HasIndex(e => e.UserId).IsUnique();
        }
    }
}