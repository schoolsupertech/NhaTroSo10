using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Models;

public partial class MotelManagement2024DbContext : DbContext
{
    public MotelManagement2024DbContext()
    {
    }

    public MotelManagement2024DbContext(DbContextOptions<MotelManagement2024DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //=> optionsBuilder.UseSqlServer(GetConnectionString());
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var dbPwd = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
        var connectionString = $"Server=host.docker.internal;Database={dbName};Uid=sa;Pwd={dbPwd};TrustServerCertificate=true;";
        optionsBuilder.UseSqlServer(connectionString);
    }

    //private static string GetConnectionString()
    //{
    //    IConfiguration config = new ConfigurationBuilder()
    //        .SetBasePath(Directory.GetCurrentDirectory())
    //        .AddJsonFile("appsettings.json", true, true)
    //        .Build();
    //    return config.GetConnectionString("DefaultConnection")
    //        ?? throw new Exception("Connection String 'DefaultConnection' not found in appsettings.json");
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.Property(e => e.InvoiceId)
                .ValueGeneratedNever()
                .HasColumnName("invoice_id");
            entity.Property(e => e.CapacityOfElectric).HasColumnName("capacity_of_electric");
            entity.Property(e => e.CapacityOfWater).HasColumnName("capacity_of_water");
            entity.Property(e => e.InvoiceDate)
                .HasColumnType("datetime")
                .HasColumnName("invoice_date");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.PriceOfElectric)
                .HasColumnType("money")
                .HasColumnName("price_of_electric");
            entity.Property(e => e.PriceOfServices)
                .HasColumnType("money")
                .HasColumnName("price_of_services");
            entity.Property(e => e.PriceOfWater)
                .HasColumnType("money")
                .HasColumnName("price_of_water");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("money")
                .HasColumnName("total_price");

            entity.HasOne(d => d.Room).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Rooms");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DateOfIssue).HasColumnName("date_of_issue");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(200)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("FEMALE")
                .HasColumnName("gender");
            entity.Property(e => e.IdentificationCard)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identification_card");
            entity.Property(e => e.PermanentAddress).HasColumnName("permanent_address");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.TemporaryAddress).HasColumnName("temporary_address");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Room).WithMany(p => p.Members)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_Members_Rooms");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(e => e.RoomId)
                .ValueGeneratedNever()
                .HasColumnName("room_id");
            entity.Property(e => e.CostOfElectric)
                .HasColumnType("money")
                .HasColumnName("cost_of_electric");
            entity.Property(e => e.CostOfServices)
                .HasColumnType("money")
                .HasColumnName("cost_of_services");
            entity.Property(e => e.CostOfSharedRoom)
                .HasColumnType("money")
                .HasColumnName("cost_of_shared_room");
            entity.Property(e => e.CostOfWater)
                .HasColumnType("money")
                .HasColumnName("cost_of_water");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.MaxQuantity).HasColumnName("max_quantity");
            entity.Property(e => e.Payday).HasColumnName("payday");
            entity.Property(e => e.PriceOfRoom)
                .HasColumnType("money")
                .HasColumnName("price_of_room");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.RoomName)
                .HasMaxLength(100)
                .HasColumnName("room_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
