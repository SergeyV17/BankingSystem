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
                    CardName = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    CardBalance = table.Column<decimal>(nullable: true),
                    Card_Discriminator = table.Column<string>(nullable: true),
                    DepositNumber = table.Column<string>(nullable: true),
                    DepositBalance = table.Column<decimal>(nullable: true),
                    DateOfDepositOpen = table.Column<DateTime>(nullable: true),
                    DateOfDepositClose = table.Column<DateTime>(nullable: true),
                    DepositCapitalization = table.Column<bool>(nullable: true),
                    DepositRate = table.Column<double>(nullable: true),
                    Deposit_Discriminator = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ClientId",
                table: "Accounts",
                column: "ClientId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
