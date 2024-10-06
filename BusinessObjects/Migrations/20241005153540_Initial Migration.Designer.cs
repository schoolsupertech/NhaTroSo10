﻿// <auto-generated />
using System;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObjects.Migrations
{
    [DbContext(typeof(MotelManagement2024DbContext))]
    [Migration("20241005153540_Initial Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObjects.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .HasColumnType("int")
                        .HasColumnName("invoice_id");

                    b.Property<int>("CapacityOfElectric")
                        .HasColumnType("int")
                        .HasColumnName("capacity_of_electric");

                    b.Property<int>("CapacityOfWater")
                        .HasColumnType("int")
                        .HasColumnName("capacity_of_water");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime")
                        .HasColumnName("invoice_date");

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasColumnName("price");

                    b.Property<decimal?>("PriceOfElectric")
                        .HasColumnType("money")
                        .HasColumnName("price_of_electric");

                    b.Property<decimal>("PriceOfServices")
                        .HasColumnType("money")
                        .HasColumnName("price_of_services");

                    b.Property<decimal?>("PriceOfWater")
                        .HasColumnType("money")
                        .HasColumnName("price_of_water");

                    b.Property<int>("RoomId")
                        .HasColumnType("int")
                        .HasColumnName("room_id");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("money")
                        .HasColumnName("total_price");

                    b.HasKey("InvoiceId");

                    b.HasIndex("RoomId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("BusinessObjects.Models.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("member_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"));

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_of_birth");

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_of_issue");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("full_name");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasDefaultValue("FEMALE")
                        .HasColumnName("gender");

                    b.Property<string>("IdentificationCard")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("identification_card");

                    b.Property<string>("PermanentAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("permanent_address");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("phone_number");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int")
                        .HasColumnName("room_id");

                    b.Property<string>("TemporaryAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("temporary_address");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_date");

                    b.HasKey("MemberId");

                    b.HasIndex("RoomId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("BusinessObjects.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("int")
                        .HasColumnName("room_id");

                    b.Property<decimal>("CostOfElectric")
                        .HasColumnType("money")
                        .HasColumnName("cost_of_electric");

                    b.Property<decimal>("CostOfServices")
                        .HasColumnType("money")
                        .HasColumnName("cost_of_services");

                    b.Property<decimal>("CostOfSharedRoom")
                        .HasColumnType("money")
                        .HasColumnName("cost_of_shared_room");

                    b.Property<decimal>("CostOfWater")
                        .HasColumnType("money")
                        .HasColumnName("cost_of_water");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("MaxQuantity")
                        .HasColumnType("int")
                        .HasColumnName("max_quantity");

                    b.Property<DateOnly>("Payday")
                        .HasColumnType("date")
                        .HasColumnName("payday");

                    b.Property<decimal>("PriceOfRoom")
                        .HasColumnType("money")
                        .HasColumnName("price_of_room");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("room_name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at");

                    b.HasKey("RoomId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("BusinessObjects.Models.Invoice", b =>
                {
                    b.HasOne("BusinessObjects.Models.Room", "Room")
                        .WithMany("Invoices")
                        .HasForeignKey("RoomId")
                        .IsRequired()
                        .HasConstraintName("FK_Invoices_Rooms");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("BusinessObjects.Models.Member", b =>
                {
                    b.HasOne("BusinessObjects.Models.Room", "Room")
                        .WithMany("Members")
                        .HasForeignKey("RoomId")
                        .HasConstraintName("FK_Members_Rooms");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("BusinessObjects.Models.Room", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
