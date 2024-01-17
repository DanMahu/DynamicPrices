﻿// <auto-generated />
using DynamicPricing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DynamicPrices.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240117182523_AddTabelClienti")]
    partial class AddTabelClienti
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DynamicPrices.Models.Clienti", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nume")
                        .IsRequired()
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Prenume")
                        .IsRequired()
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("IdClient");

                    b.ToTable("clienti");
                });
#pragma warning restore 612, 618
        }
    }
}
