﻿// <auto-generated />
using System;
using DynamicPricing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DynamicPrices.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240117183346_AddTabelStocElectronice")]
    partial class AddTabelStocElectronice
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

            modelBuilder.Entity("DynamicPrices.Models.Istoric_Preturi_Electronice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataModificare")
                        .HasColumnType("datetime");

                    b.Property<int>("IdProdus")
                        .HasColumnType("int");

                    b.Property<decimal>("PretNou")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("PretVechi")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdProdus");

                    b.ToTable("istoric_preturi_electronice");
                });

            modelBuilder.Entity("DynamicPrices.Models.Preturi_Electronice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataActualizare")
                        .HasColumnType("datetime");

                    b.Property<int>("IdProdus")
                        .HasColumnType("int");

                    b.Property<decimal>("PretCurent")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdProdus");

                    b.ToTable("preturi_electronice");
                });

            modelBuilder.Entity("DynamicPrices.Models.Produse_Electronice", b =>
                {
                    b.Property<int>("IdProdus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("CostProducere")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Descriere")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NumeProdus")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<decimal>("PretRecomandat")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("TipProdus")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("IdProdus");

                    b.ToTable("produse_electronice");
                });

            modelBuilder.Entity("DynamicPrices.Models.Stoc_Electronice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdProdus")
                        .HasColumnType("int");

                    b.Property<int>("InStoc")
                        .HasColumnType("int");

                    b.Property<int>("StocMaxim")
                        .HasColumnType("int");

                    b.Property<int>("StocMinim")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdProdus");

                    b.ToTable("stoc_electronice");
                });

            modelBuilder.Entity("DynamicPrices.Models.Istoric_Preturi_Electronice", b =>
                {
                    b.HasOne("DynamicPrices.Models.Produse_Electronice", "Produse_Electronice")
                        .WithMany()
                        .HasForeignKey("IdProdus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produse_Electronice");
                });

            modelBuilder.Entity("DynamicPrices.Models.Preturi_Electronice", b =>
                {
                    b.HasOne("DynamicPrices.Models.Produse_Electronice", "Produse_Electronice")
                        .WithMany()
                        .HasForeignKey("IdProdus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produse_Electronice");
                });

            modelBuilder.Entity("DynamicPrices.Models.Stoc_Electronice", b =>
                {
                    b.HasOne("DynamicPrices.Models.Produse_Electronice", "Produse_Electronice")
                        .WithMany()
                        .HasForeignKey("IdProdus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produse_Electronice");
                });
#pragma warning restore 612, 618
        }
    }
}
