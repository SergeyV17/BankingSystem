﻿// <auto-generated />
using System;
using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankingSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BankingSystem.Models.Implementations.Accounts.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AccountLockout")
                        .HasColumnType("bit");

                    b.Property<decimal>("AmountOfReplenishmentPerDay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int?>("DepositId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.HasIndex("DepositId");

                    b.ToTable("Accounts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Account");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.BankServices.CardService.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CardBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CardName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CardId");

                    b.ToTable("Cards");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Card");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.BankServices.DepositService.Deposit", b =>
                {
                    b.Property<int>("DepositId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfDepositClose")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfDepositOpen")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DepositBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("DepositCapitalization")
                        .HasColumnType("bit");

                    b.Property<string>("DepositNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DepositRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepositId");

                    b.ToTable("Deposits");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Deposit");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.Clients.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Client");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.Accounts.RegularAccount", b =>
                {
                    b.HasBaseType("BankingSystem.Models.Implementations.Accounts.Account");

                    b.HasDiscriminator().HasValue("RegularAccount");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.Accounts.VIPAccount", b =>
                {
                    b.HasBaseType("BankingSystem.Models.Implementations.Accounts.Account");

                    b.HasDiscriminator().HasValue("VIPAccount");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.BankServices.CardService.VisaBlack", b =>
                {
                    b.HasBaseType("BankingSystem.Models.Implementations.BankServices.CardService.Card");

                    b.HasDiscriminator().HasValue("VisaBlack");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.BankServices.CardService.VisaClassic", b =>
                {
                    b.HasBaseType("BankingSystem.Models.Implementations.BankServices.CardService.Card");

                    b.HasDiscriminator().HasValue("VisaClassic");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.BankServices.CardService.VisaCorporate", b =>
                {
                    b.HasBaseType("BankingSystem.Models.Implementations.BankServices.CardService.Card");

                    b.HasDiscriminator().HasValue("VisaCorporate");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.BankServices.CardService.VisaPlatinum", b =>
                {
                    b.HasBaseType("BankingSystem.Models.Implementations.BankServices.CardService.Card");

                    b.HasDiscriminator().HasValue("VisaPlatinum");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.BankServices.DepositService.DefaultDeposit", b =>
                {
                    b.HasBaseType("BankingSystem.Models.Implementations.BankServices.DepositService.Deposit");

                    b.HasDiscriminator().HasValue("DefaultDeposit");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.Clients.Entity", b =>
                {
                    b.HasBaseType("BankingSystem.Models.Implementations.Clients.Client");

                    b.HasDiscriminator().HasValue("Entity");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.Clients.Individual", b =>
                {
                    b.HasBaseType("BankingSystem.Models.Implementations.Clients.Client");

                    b.HasDiscriminator().HasValue("Individual");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.Accounts.Account", b =>
                {
                    b.HasOne("BankingSystem.Models.Implementations.BankServices.CardService.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");

                    b.HasOne("BankingSystem.Models.Implementations.Clients.Client", "Client")
                        .WithOne("Account")
                        .HasForeignKey("BankingSystem.Models.Implementations.Accounts.Account", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankingSystem.Models.Implementations.BankServices.DepositService.Deposit", "Deposit")
                        .WithMany()
                        .HasForeignKey("DepositId");
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.Clients.Client", b =>
                {
                    b.OwnsOne("BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData.Contact", "Contact", b1 =>
                        {
                            b1.Property<int>("ClientId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ClientId");

                            b1.ToTable("Clients");

                            b1.WithOwner()
                                .HasForeignKey("ClientId");

                            b1.OwnsOne("BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData.PhoneNumber", "PhoneNumber", b2 =>
                                {
                                    b2.Property<int>("ContactClientId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int")
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<string>("Number")
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("ContactClientId");

                                    b2.ToTable("Clients");

                                    b2.WithOwner()
                                        .HasForeignKey("ContactClientId");
                                });
                        });

                    b.OwnsOne("BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData.Passport", "Passport", b1 =>
                        {
                            b1.Property<int>("ClientId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Address")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ClientId");

                            b1.ToTable("Clients");

                            b1.WithOwner()
                                .HasForeignKey("ClientId");

                            b1.OwnsOne("BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData.FullName", "FullName", b2 =>
                                {
                                    b2.Property<int>("PassportClientId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int")
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<string>("FirstName")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("LastName")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("MiddleName")
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("PassportClientId");

                                    b2.ToTable("Clients");

                                    b2.WithOwner()
                                        .HasForeignKey("PassportClientId");
                                });

                            b1.OwnsOne("BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData.SeriesAndNumber", "SeriesAndNumber", b2 =>
                                {
                                    b2.Property<int>("PassportClientId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int")
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<string>("Number")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Series")
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("PassportClientId");

                                    b2.ToTable("Clients");

                                    b2.WithOwner()
                                        .HasForeignKey("PassportClientId");
                                });
                        });
                });

            modelBuilder.Entity("BankingSystem.Models.Implementations.Clients.Entity", b =>
                {
                    b.OwnsOne("BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData.Company", "Company", b1 =>
                        {
                            b1.Property<int>("EntityId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Website")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("EntityId");

                            b1.ToTable("Clients");

                            b1.WithOwner()
                                .HasForeignKey("EntityId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
