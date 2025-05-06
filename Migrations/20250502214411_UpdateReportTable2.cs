using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginPageAPIs.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LoginRequests_ChildId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LoginRequests_LoginRequestId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_LoginRequestId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "LoginRequestId",
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
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LoginRequestId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_LoginRequestId",
                table: "Reports",
                column: "LoginRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LoginRequests_ChildId",
                table: "Reports",
                column: "ChildId",
                principalTable: "LoginRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LoginRequests_LoginRequestId",
                table: "Reports",
                column: "LoginRequestId",
                principalTable: "LoginRequests",
                principalColumn: "Id");
        }
    }
}
