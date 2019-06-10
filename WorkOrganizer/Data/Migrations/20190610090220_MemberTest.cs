using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkOrganizer.Data.Migrations
{
    public partial class MemberTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Project_ProjectId",
                table: "Member");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Member",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Project_ProjectId",
                table: "Member",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Project_ProjectId",
                table: "Member");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Member",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Project_ProjectId",
                table: "Member",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
