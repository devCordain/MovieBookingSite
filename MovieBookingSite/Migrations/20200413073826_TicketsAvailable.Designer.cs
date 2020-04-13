﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieBookingSite.Data;

namespace MovieBookingSite.Migrations
{
    [DbContext(typeof(CinemaContext))]
    [Migration("20200413073826_TicketsAvailable")]
    partial class TicketsAvailable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MovieBookingSite.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieBookingSite.Salon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rows")
                        .HasColumnType("int");

                    b.Property<int>("SeatsPerRow")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Salons");
                });

            modelBuilder.Entity("MovieBookingSite.Showing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<int?>("SalonId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ShowTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TicketPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("SalonId");

                    b.ToTable("Showings");
                });

            modelBuilder.Entity("MovieBookingSite.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<int>("Row")
                        .HasColumnType("int");

                    b.Property<int>("SeatOnRow")
                        .HasColumnType("int");

                    b.Property<int?>("ShowingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShowingId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("MovieBookingSite.Showing", b =>
                {
                    b.HasOne("MovieBookingSite.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.HasOne("MovieBookingSite.Salon", "Salon")
                        .WithMany()
                        .HasForeignKey("SalonId");
                });

            modelBuilder.Entity("MovieBookingSite.Ticket", b =>
                {
                    b.HasOne("MovieBookingSite.Showing", null)
                        .WithMany("Tickets")
                        .HasForeignKey("ShowingId");
                });
#pragma warning restore 612, 618
        }
    }
}