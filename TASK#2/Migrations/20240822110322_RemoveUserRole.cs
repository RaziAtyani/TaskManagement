using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TASK_2.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Registrations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
