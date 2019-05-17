using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankApp.Data.Migrations
{
    public partial class CreateIdentitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //    migrationBuilder.CreateTable(
            //        name: "Accounts",
            //        columns: table => new
            //        {
            //            AccountId = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //            Frequency = table.Column<string>(maxLength: 50, nullable: false),
            //            Created = table.Column<DateTime>(type: "date", nullable: false),
            //            Balance = table.Column<decimal>(type: "decimal(13, 2)", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_account", x => x.AccountId);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Customers",
            //        columns: table => new
            //        {
            //            CustomerId = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //            Gender = table.Column<string>(maxLength: 6, nullable: false),
            //            Givenname = table.Column<string>(maxLength: 100, nullable: false),
            //            Surname = table.Column<string>(maxLength: 100, nullable: false),
            //            Streetaddress = table.Column<string>(maxLength: 100, nullable: false),
            //            City = table.Column<string>(maxLength: 100, nullable: false),
            //            Zipcode = table.Column<string>(maxLength: 15, nullable: false),
            //            Country = table.Column<string>(maxLength: 100, nullable: false),
            //            CountryCode = table.Column<string>(maxLength: 2, nullable: false),
            //            Birthday = table.Column<DateTime>(type: "date", nullable: true),
            //            NationalId = table.Column<string>(maxLength: 20, nullable: true),
            //            Telephonecountrycode = table.Column<string>(maxLength: 10, nullable: true),
            //            Telephonenumber = table.Column<string>(maxLength: 25, nullable: true),
            //            Emailaddress = table.Column<string>(maxLength: 100, nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Customers", x => x.CustomerId);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Loans",
            //        columns: table => new
            //        {
            //            LoanId = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //            AccountId = table.Column<int>(nullable: false),
            //            Date = table.Column<DateTime>(type: "date", nullable: false),
            //            Amount = table.Column<decimal>(type: "decimal(13, 2)", nullable: false),
            //            Duration = table.Column<int>(nullable: false),
            //            Payments = table.Column<decimal>(type: "decimal(13, 2)", nullable: false),
            //            Status = table.Column<string>(maxLength: 50, nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_loan", x => x.LoanId);
            //            table.ForeignKey(
            //                name: "FK_Loans_Accounts",
            //                column: x => x.AccountId,
            //                principalTable: "Accounts",
            //                principalColumn: "AccountId",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "PermenentOrder",
            //        columns: table => new
            //        {
            //            OrderId = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //            AccountId = table.Column<int>(nullable: false),
            //            BankTo = table.Column<string>(maxLength: 50, nullable: false),
            //            AccountTo = table.Column<string>(maxLength: 50, nullable: false),
            //            Amount = table.Column<decimal>(type: "decimal(13, 2)", nullable: true),
            //            Symbol = table.Column<string>(maxLength: 50, nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_PermenentOrder", x => x.OrderId);
            //            table.ForeignKey(
            //                name: "FK_PermenentOrder_Accounts",
            //                column: x => x.AccountId,
            //                principalTable: "Accounts",
            //                principalColumn: "AccountId",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Transactions",
            //        columns: table => new
            //        {
            //            TransactionId = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //            AccountId = table.Column<int>(nullable: false),
            //            Date = table.Column<DateTime>(type: "date", nullable: false),
            //            Type = table.Column<string>(maxLength: 50, nullable: false),
            //            Operation = table.Column<string>(maxLength: 50, nullable: false),
            //            Amount = table.Column<decimal>(type: "decimal(13, 2)", nullable: false),
            //            Balance = table.Column<decimal>(type: "decimal(13, 2)", nullable: false),
            //            Symbol = table.Column<string>(maxLength: 50, nullable: true),
            //            Bank = table.Column<string>(maxLength: 50, nullable: true),
            //            Account = table.Column<string>(maxLength: 50, nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_trans2", x => x.TransactionId);
            //            table.ForeignKey(
            //                name: "FK_Transactions_Accounts",
            //                column: x => x.AccountId,
            //                principalTable: "Accounts",
            //                principalColumn: "AccountId",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Dispositions",
            //        columns: table => new
            //        {
            //            DispositionId = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //            CustomerId = table.Column<int>(nullable: false),
            //            AccountId = table.Column<int>(nullable: false),
            //            Type = table.Column<string>(maxLength: 50, nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_disposition", x => x.DispositionId);
            //            table.ForeignKey(
            //                name: "FK_Dispositions_Accounts",
            //                column: x => x.AccountId,
            //                principalTable: "Accounts",
            //                principalColumn: "AccountId",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Dispositions_Customers",
            //                column: x => x.CustomerId,
            //                principalTable: "Customers",
            //                principalColumn: "CustomerId",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Cards",
            //        columns: table => new
            //        {
            //            CardId = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //            DispositionId = table.Column<int>(nullable: false),
            //            Type = table.Column<string>(maxLength: 50, nullable: false),
            //            Issued = table.Column<DateTime>(type: "date", nullable: false),
            //            CCType = table.Column<string>(maxLength: 50, nullable: false),
            //            CCNumber = table.Column<string>(maxLength: 50, nullable: false),
            //            CVV2 = table.Column<string>(maxLength: 10, nullable: false),
            //            ExpM = table.Column<int>(nullable: false),
            //            ExpY = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Cards", x => x.CardId);
            //            table.ForeignKey(
            //                name: "FK_Cards_Dispositions",
            //                column: x => x.DispositionId,
            //                principalTable: "Dispositions",
            //                principalColumn: "DispositionId",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Cards_DispositionId",
            //        table: "Cards",
            //        column: "DispositionId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Dispositions_AccountId",
            //        table: "Dispositions",
            //        column: "AccountId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Dispositions_CustomerId",
            //        table: "Dispositions",
            //        column: "CustomerId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Loans_AccountId",
            //        table: "Loans",
            //        column: "AccountId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PermenentOrder_AccountId",
            //        table: "PermenentOrder",
            //        column: "AccountId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Transactions_AccountId",
            //        table: "Transactions",
            //        column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "PermenentOrder");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Dispositions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
