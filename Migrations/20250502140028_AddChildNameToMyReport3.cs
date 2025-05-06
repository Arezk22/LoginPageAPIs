using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginPageAPIs.Migrations
{
    /// <inheritdoc />
    public partial class AddChildNameToMyReport3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LoginRequests_ChildId",
                table: "Reports");

            migrationBuilder.AlterColumn<int>(
                name: "ChildId",
                table: "Reports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LoginRequests_ChildId",
                table: "Reports",
                column: "ChildId",
                principalTable: "LoginRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LoginRequests_ChildId",
                table: "Reports");

            migrationBuilder.AlterColumn<int>(
                name: "ChildId",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LoginRequests_ChildId",
                table: "Reports",
                column: "ChildId",
                principalTable: "LoginRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
