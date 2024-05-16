﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using h2dYatırım.DataAccess;

#nullable disable

namespace h2dYatirim.Infrastructure.Migrations
{
    [DbContext(typeof(h2dYatirimDBContext))]
    [Migration("20240516212918_mig")]
    partial class mig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("h2dYatirim.Domain.Entity.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("AmountInAccount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("AssetValue")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("CryptoAccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("InvestmentAccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CryptoAccountId");

                    b.HasIndex("InvestmentAccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("h2dYatirim.Domain.Entity.InvestmentAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("PortfolioValue")
                        .HasColumnType("numeric");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("InvestmentAccounts");
                });

            modelBuilder.Entity("h2dYatırım.Entities.AccountMovement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<string>("AssetId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("AccountMovements");
                });

            modelBuilder.Entity("h2dYatırım.Entities.CryptoAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("WalletValue")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("CryptoAccounts");
                });

            modelBuilder.Entity("h2dYatırım.Entities.Portfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<decimal>("CurrentValue")
                        .HasColumnType("numeric");

                    b.Property<Guid>("InvestmentAccountId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("ReceivedValue")
                        .HasColumnType("numeric");

                    b.Property<string>("ShareCertificateId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("ValueChange")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("h2dYatırım.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IdentificationNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("h2dYatırım.Entities.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<Guid>("CryptoAccountId")
                        .HasColumnType("uuid");

                    b.Property<string>("CryptoId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("CurrentValue")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ReceivedValue")
                        .HasColumnType("numeric");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("ValueChange")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("h2dYatirim.Domain.Entity.Account", b =>
                {
                    b.HasOne("h2dYatırım.Entities.CryptoAccount", "CryptoAccount")
                        .WithMany()
                        .HasForeignKey("CryptoAccountId");

                    b.HasOne("h2dYatirim.Domain.Entity.InvestmentAccount", "InvestmentAccount")
                        .WithMany()
                        .HasForeignKey("InvestmentAccountId");

                    b.Navigation("CryptoAccount");

                    b.Navigation("InvestmentAccount");
                });
#pragma warning restore 612, 618
        }
    }
}