using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SendEmailConsole.Migrations
{
    public partial class addSuccessfulsend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "sendSuccessful",
                table: "Emails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sendSuccessful",
                table: "Emails");
        }
    }
}
