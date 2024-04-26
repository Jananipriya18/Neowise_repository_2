﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RideShare.Models;

#nullable disable

namespace RideShare.Migrations
{
    [DbContext(typeof(RideSharingDbContext))]
    [Migration("20240422160600_ini")]
    partial class ini
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RideShare.Models.Commuter", b =>
                {
                    b.Property<int>("CommuterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommuterID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RideID")
                        .HasColumnType("int");

                    b.HasKey("CommuterID");

                    b.HasIndex("RideID");

                    b.ToTable("Commuters");
                });

            modelBuilder.Entity("RideShare.Models.Ride", b =>
                {
                    b.Property<int>("RideID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RideID"), 1L, 1);

                    b.Property<DateTime>("DateOfDeparture")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartureLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaximumCapacity")
                        .HasColumnType("int");

                    b.HasKey("RideID");

                    b.ToTable("Rides");

                    b.HasData(
                        new
                        {
                            RideID = 1,
                            DateOfDeparture = new DateTime(2024, 4, 22, 21, 36, 0, 209, DateTimeKind.Local).AddTicks(6082),
                            DepartureLocation = "Location1",
                            Destination = "Destination1",
                            MaximumCapacity = 4
                        },
                        new
                        {
                            RideID = 2,
                            DateOfDeparture = new DateTime(2024, 4, 22, 21, 36, 0, 209, DateTimeKind.Local).AddTicks(6107),
                            DepartureLocation = "Location2",
                            Destination = "Destination2",
                            MaximumCapacity = 3
                        },
                        new
                        {
                            RideID = 3,
                            DateOfDeparture = new DateTime(2024, 4, 22, 21, 36, 0, 209, DateTimeKind.Local).AddTicks(6110),
                            DepartureLocation = "Location3",
                            Destination = "Destination3",
                            MaximumCapacity = 2
                        },
                        new
                        {
                            RideID = 4,
                            DateOfDeparture = new DateTime(2024, 4, 22, 21, 36, 0, 209, DateTimeKind.Local).AddTicks(6113),
                            DepartureLocation = "Location4",
                            Destination = "Destination4",
                            MaximumCapacity = 4
                        },
                        new
                        {
                            RideID = 5,
                            DateOfDeparture = new DateTime(2024, 4, 22, 21, 36, 0, 209, DateTimeKind.Local).AddTicks(6115),
                            DepartureLocation = "Location5",
                            Destination = "Destination5",
                            MaximumCapacity = 3
                        });
                });

            modelBuilder.Entity("RideShare.Models.Commuter", b =>
                {
                    b.HasOne("RideShare.Models.Ride", "Ride")
                        .WithMany("Commuters")
                        .HasForeignKey("RideID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ride");
                });

            modelBuilder.Entity("RideShare.Models.Ride", b =>
                {
                    b.Navigation("Commuters");
                });
#pragma warning restore 612, 618
        }
    }
}