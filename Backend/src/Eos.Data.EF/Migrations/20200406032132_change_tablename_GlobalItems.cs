using Microsoft.EntityFrameworkCore.Migrations;

namespace Eos.Data.EF.Migrations
{
    public partial class change_tablename_GlobalItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalItem_Items_ItemId",
                table: "GlobalItem");

            migrationBuilder.DropForeignKey(
                name: "FK_GlobalItem_Items_ParentId",
                table: "GlobalItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlobalItem",
                table: "GlobalItem");

            migrationBuilder.RenameTable(
                name: "GlobalItem",
                newName: "GlobalItems");

            migrationBuilder.RenameIndex(
                name: "IX_GlobalItem_ItemId",
                table: "GlobalItems",
                newName: "IX_GlobalItems_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlobalItems",
                table: "GlobalItems",
                columns: new[] { "ParentId", "ItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalItems_Items_ItemId",
                table: "GlobalItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalItems_Items_ParentId",
                table: "GlobalItems",
                column: "ParentId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalItems_Items_ItemId",
                table: "GlobalItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GlobalItems_Items_ParentId",
                table: "GlobalItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlobalItems",
                table: "GlobalItems");

            migrationBuilder.RenameTable(
                name: "GlobalItems",
                newName: "GlobalItem");

            migrationBuilder.RenameIndex(
                name: "IX_GlobalItems_ItemId",
                table: "GlobalItem",
                newName: "IX_GlobalItem_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlobalItem",
                table: "GlobalItem",
                columns: new[] { "ParentId", "ItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalItem_Items_ItemId",
                table: "GlobalItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalItem_Items_ParentId",
                table: "GlobalItem",
                column: "ParentId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
