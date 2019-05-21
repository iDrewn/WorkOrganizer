using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkOrganizer.Data.Migrations
{
    public partial class JobProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Project_ProjectId1",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_ProjectId1",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Job");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Job",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_ProjectId",
                table: "Job",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Project_ProjectId",
                table: "Job",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Project_ProjectId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_ProjectId",
                table: "Job");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Job",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId1",
                table: "Job",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_ProjectId1",
                table: "Job",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Project_ProjectId1",
                table: "Job",
                column: "ProjectId1",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
