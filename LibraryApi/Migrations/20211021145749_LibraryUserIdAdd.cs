using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApi.Migrations
{
    public partial class LibraryUserIdAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_Employees_EmployeeId",
                table: "Libraries");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Libraries_LibraryId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Users_LibraryId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Libraries_EmployeeId",
                table: "Libraries");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Libraries",
                newName: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_CreatedById",
                table: "Libraries",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_Users_CreatedById",
                table: "Libraries",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_Users_CreatedById",
                table: "Libraries");

            migrationBuilder.DropIndex(
                name: "IX_Libraries_CreatedById",
                table: "Libraries");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Libraries",
                newName: "EmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "LibraryId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pesel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_LibraryId",
                table: "Users",
                column: "LibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_EmployeeId",
                table: "Libraries",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_Employees_EmployeeId",
                table: "Libraries",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Libraries_LibraryId",
                table: "Users",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
