using System;
using Microsoft.EntityFrameworkCore.Migrations;
using AKKTN_Pr00.Data;

#nullable disable

namespace AKKTN_Pr00.Migrations
{
    /// <inheritdoc />
    public partial class project00Migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admintbls",
                columns: table => new
                {
                    adminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    cell = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    adminpass = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admintbls", x => x.adminID);
                });

            migrationBuilder.CreateTable(
                name: "assignedTasks",
                columns: table => new
                {
                    assignedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    TaskID = table.Column<int>(type: "int", nullable: false),
                    memberID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignedTasks", x => x.assignedID);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    ClientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    CIPCRegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IncomeTaxNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    VAT = table.Column<bool>(type: "bit", nullable: false),
                    VATPeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayeeNumber = table.Column<bool>(type: "bit", nullable: false),
                    PayeeReferenceNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EMP501 = table.Column<bool>(type: "bit", nullable: false),
                    UIF = table.Column<bool>(type: "bit", nullable: false),
                    UIFNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    WCC = table.Column<bool>(type: "bit", nullable: false),
                    WCCNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Payroll = table.Column<bool>(type: "bit", nullable: false),
                    MonthlyCashbook = table.Column<bool>(type: "bit", nullable: false),
                    FinancialStatements = table.Column<bool>(type: "bit", nullable: false),
                    IncomeTaxReturn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    companypass = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactName1 = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Email1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cell1 = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    ContactName2 = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Email2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Cell2 = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "companiesTeam",
                columns: table => new
                {
                    memberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cell = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companiesTeam", x => x.memberID);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    TaskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    AssignTaskDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reminders = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.TaskID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admintbls");

            migrationBuilder.DropTable(
                name: "assignedTasks");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.DropTable(
                name: "companiesTeam");

            migrationBuilder.DropTable(
                name: "tasks");
        }
    }
}
