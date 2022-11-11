using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementWebAPI.Models;

public partial class EmployeeManagementSystemDbContext : DbContext
{
    public EmployeeManagementSystemDbContext()
    {
    }

    public EmployeeManagementSystemDbContext(DbContextOptions<EmployeeManagementSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=NETANEL\\SQLEXPRESS; Database=EmployeeManagementSystemDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__CBA14F48C246D7C3");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(9)
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .HasColumnName("EMPLOYEE_NAME");
            entity.Property(e => e.EmployeeRole)
                .HasMaxLength(50)
                .HasColumnName("EMPLOYEE_ROLE");
            entity.Property(e => e.ManagerName)
                .HasMaxLength(50)
                .HasColumnName("MANAGER_NAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
