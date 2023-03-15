﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using rendszerfejlesztes_beadando.Data;

#nullable disable

namespace rendszerfejlesztes_beadando.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230315153628_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("rendszerfejlesztes_beadando.Models.Alkatresz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Alkatresz_megnevezes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ar")
                        .HasColumnType("int");

                    b.Property<int>("MaxTarolasRekeszenkent")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Alkatresz");
                });

            modelBuilder.Entity("rendszerfejlesztes_beadando.Models.Megrendelo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Adoszam")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nev")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefonszam")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Megrendelo");
                });

            modelBuilder.Entity("rendszerfejlesztes_beadando.Models.Naplo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjektID")
                        .HasColumnType("int");

                    b.Property<int>("StatuszID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProjektID");

                    b.HasIndex("StatuszID");

                    b.ToTable("Naplo");
                });

            modelBuilder.Entity("rendszerfejlesztes_beadando.Models.Projekt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Helyszin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Leiras")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MegrendeloId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MegrendeloId");

                    b.ToTable("Projekt");
                });

            modelBuilder.Entity("rendszerfejlesztes_beadando.Models.Raktar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlkatreszId")
                        .HasColumnType("int");

                    b.Property<int>("Darabszam")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlkatreszId");

                    b.ToTable("Raktar");
                });

            modelBuilder.Entity("rendszerfejlesztes_beadando.Models.Statusz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Statusz_megnevezes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statusz");
                });

            modelBuilder.Entity("rendszerfejlesztes_beadando.Models.Naplo", b =>
                {
                    b.HasOne("rendszerfejlesztes_beadando.Models.Projekt", "Projekt")
                        .WithMany()
                        .HasForeignKey("ProjektID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("rendszerfejlesztes_beadando.Models.Statusz", "Statusz")
                        .WithMany()
                        .HasForeignKey("StatuszID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Projekt");

                    b.Navigation("Statusz");
                });

            modelBuilder.Entity("rendszerfejlesztes_beadando.Models.Projekt", b =>
                {
                    b.HasOne("rendszerfejlesztes_beadando.Models.Megrendelo", "Megrendelo")
                        .WithMany()
                        .HasForeignKey("MegrendeloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Megrendelo");
                });

            modelBuilder.Entity("rendszerfejlesztes_beadando.Models.Raktar", b =>
                {
                    b.HasOne("rendszerfejlesztes_beadando.Models.Alkatresz", "Alkatresz")
                        .WithMany()
                        .HasForeignKey("AlkatreszId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alkatresz");
                });
#pragma warning restore 612, 618
        }
    }
}