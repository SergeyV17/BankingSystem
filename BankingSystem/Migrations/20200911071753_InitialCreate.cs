using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingSystem.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Passport_FullName_LastName = table.Column<string>(nullable: true),
                    Passport_FullName_FirstName = table.Column<string>(nullable: true),
                    Passport_FullName_MiddleName = table.Column<string>(nullable: true),
                    Passport_Address = table.Column<string>(nullable: true),
                    Passport_SeriesAndNumber_Series = table.Column<string>(nullable: true),
                    Passport_SeriesAndNumber_Number = table.Column<string>(nullable: true),
                    Contact_PhoneNumber_Number = table.Column<string>(nullable: true),
                    Contact_Email = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Company_Name = table.Column<string>(nullable: true),
                    Company_Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountLockout = table.Column<bool>(nullable: false),
                    AmountOfReplenishmentPerDay = table.Column<decimal>(nullable: false),
                    DateOfLastReplenish = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardName = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    CardBalance = table.Column<decimal>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositNumber = table.Column<string>(nullable: true),
                    DepositBalance = table.Column<decimal>(nullable: false),
                    DateOfDepositOpen = table.Column<DateTime>(nullable: false),
                    DateOfDepositClose = table.Column<DateTime>(nullable: false),
                    DepositCapitalization = table.Column<bool>(nullable: false),
                    DepositRate = table.Column<decimal>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposits_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ClientId",
                table: "Accounts",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AccountId",
                table: "Cards",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_AccountId",
                table: "Deposits",
                column: "AccountId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
