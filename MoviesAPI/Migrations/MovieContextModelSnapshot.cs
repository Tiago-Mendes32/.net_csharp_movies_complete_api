﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesAPI.Data;

#nullable disable

namespace MoviesAPI.Migrations;

[DbContext(typeof(MovieContext))]
partial class MovieContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "6.0.10")
            .HasAnnotation("Relational:MaxIdentifierLength", 64);

        modelBuilder.Entity("MoviesAPI.Models.Movie", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<int>("Duration")
                    .HasColumnType("int");

                b.Property<string>("Gender")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("varchar(20)");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");

                b.HasKey("Id");

                b.ToTable("Movies");
            });
#pragma warning restore 612, 618
    }
}
