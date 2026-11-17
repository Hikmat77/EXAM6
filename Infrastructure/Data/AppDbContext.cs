using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
    public DbSet<Position> Positions => Set<Position>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.Department)
            .WithMany()
            .HasForeignKey(employee => employee.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.Position)
            .WithMany()
            .HasForeignKey(employee => employee.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LeaveRequest>()
            .HasOne(leaveRequest => leaveRequest.Employee)
            .WithMany()
            .HasForeignKey(leaveRequest => leaveRequest.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>().Property(employee => employee.Salary).HasPrecision(18, 2);
        modelBuilder.Entity<Position>().Property(position => position.MinSalary).HasPrecision(18, 2);
        modelBuilder.Entity<Position>().Property(position => position.MaxSalary).HasPrecision(18, 2);
    }
}
