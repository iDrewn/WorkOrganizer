using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkOrganizer.Data.Migrations
{
    public partial class AddprojectId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Project_ProjectId",
                table: "Job");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Job",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Project_ProjectId",
                table: "Job",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Project_ProjectId",
                table: "Job");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Job",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Project_ProjectId",
                table: "Job",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
