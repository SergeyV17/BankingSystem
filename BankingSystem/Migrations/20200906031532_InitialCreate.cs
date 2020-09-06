using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingSystem.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardName = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    CardBalance = table.Column<decimal>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    DepositId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositNumber = table.Column<string>(nullable: true),
                    DepositBalance = table.Column<decimal>(nullable: false),
                    DateOfDepositOpen = table.Column<DateTime>(nullable: false),
                    DateOfDepositClose = table.Column<DateTime>(nullable: false),
                    DepositCapitalization = table.Column<bool>(nullable: false),
                    DepositRate = table.Column<decimal>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.DepositId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(nullable: true),
                    DepositId = table.Column<int>(nullable: true),
                    AccountLockout = table.Column<bool>(nullable: false),
                    AmountOfReplenishmentPerDay = table.Column<decimal>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Deposits_DepositId",
                        column: x => x.DepositId,
                        principalTable: "Deposits",
                        principalColumn: "DepositId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Passport_FullName_LastName = table.Column<string>(nullable: true),
                    Passport_FullName_FirstName = table.Column<string>(nullable: true),
                    Passport_FullName_MiddleName = table.Column<string>(nullable: true),
                    Passport_Address = table.Column<string>(nullable: true),
                    Passport_SeriesAndNumber_Series = table.Column<string>(nullable: true),
                    Passport_SeriesAndNumber_Number = table.Column<string>(nullable: true),
                    Contact_PhoneNumber_Number = table.Column<string>(nullable: true),
                    Contact_Email = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Company_Name = table.Column<string>(nullable: true),
                    Company_Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CardId",
                table: "Accounts",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_DepositId",
                table: "Accounts",
                column: "DepositId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AccountId",
                table: "Clients",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Deposits");
        }
    }
}
