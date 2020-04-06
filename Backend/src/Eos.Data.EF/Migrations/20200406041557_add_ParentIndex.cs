using Microsoft.EntityFrameworkCore.Migrations;

namespace Eos.Data.EF.Migrations
{
    public partial class add_ParentIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentIndex",
                table: "GlobalItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentIndex",
                table: "GlobalItems");
        }
    }
}
