using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EMPLOYEEID = table.Column<string>(name: "EMPLOYEE_ID", type: "nvarchar(9)", maxLength: 9, nullable: false),
                    EMPLOYEENAME = table.Column<string>(name: "EMPLOYEE_NAME", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EMPLOYEEROLE = table.Column<string>(name: "EMPLOYEE_ROLE", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MANAGERNAME = table.Column<string>(name: "MANAGER_NAME", type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__CBA14F48C246D7C3", x => x.EMPLOYEEID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
