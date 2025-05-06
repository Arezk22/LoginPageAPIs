using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginPageAPIs.Migrations
{
    /// <inheritdoc />
    public partial class AddChildNameToMyReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExerciseName",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "ChildId",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChildName",
                table: "Reports",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ChildId",
                table: "Reports",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportId",
                table: "Reports",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LoginRequests_ChildId",
                table: "Reports",
                column: "ChildId",
                principalTable: "LoginRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LoginRequests_ReportId",
                table: "Reports",
                column: "ReportId",
                principalTable: "LoginRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LoginRequests_ChildId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LoginRequests_ReportId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ChildId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ReportId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ChildName",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Reports");

            migrationBuilder.AlterColumn<string>(
                name: "ExerciseName",
                table: "Reports",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
