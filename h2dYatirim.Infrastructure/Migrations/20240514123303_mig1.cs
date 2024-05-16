using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace h2dYatirim.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountMovements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CryptoAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    CryptoId = table.Column<string>(type: "text", nullable: false),
                    TransactionType = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountMovements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CryptoAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AmountInAccount = table.Column<decimal>(type: "numeric", nullable: false),
                    WalletValue = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AmountInAccount = table.Column<decimal>(type: "numeric", nullable: false),
                    ProtfolioValue = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CryptoAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    CryptoId = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    ValueChange = table.Column<decimal>(type: "numeric", nullable: false),
                    ReceivedValue = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrentValue = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CryptoAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    CryptoId = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    ValueChange = table.Column<decimal>(type: "numeric", nullable: false),
                    ReceivedValue = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrentValue = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountMovements");

            migrationBuilder.DropTable(
                name: "CryptoAccounts");

            migrationBuilder.DropTable(
                name: "InvestmentAccounts");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Wallets");
        }
    }
}
