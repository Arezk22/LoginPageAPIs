using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginPageAPIs.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LoginRequests_ReportId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "ReportId",
                table: "Reports",
                newName: "LoginRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_ReportId",
                table: "Reports",
                newName: "IX_Reports_LoginRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LoginRequests_LoginRequestId",
                table: "Reports",
                column: "LoginRequestId",
                principalTable: "LoginRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LoginRequests_LoginRequestId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "LoginRequestId",
                table: "Reports",
                newName: "ReportId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_LoginRequestId",
                table: "Reports",
                newName: "IX_Reports_ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LoginRequests_ReportId",
                table: "Reports",
                column: "ReportId",
                principalTable: "LoginRequests",
                principalColumn: "Id");
        }
    }
}
