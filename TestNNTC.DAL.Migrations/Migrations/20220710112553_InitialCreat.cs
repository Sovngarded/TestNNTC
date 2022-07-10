using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestNNTC.DAL.Migrations.Migrations
{
    public partial class InitialCreat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogueList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogueList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogueDataProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Cost = table.Column<int>(type: "integer", nullable: false),
                    CatalogueDataEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogueDataProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogueDataProduct_CatalogueList_CatalogueDataEntityId",
                        column: x => x.CatalogueDataEntityId,
                        principalTable: "CatalogueList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogueDataProduct_CatalogueDataEntityId",
                table: "CatalogueDataProduct",
                column: "CatalogueDataEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogueDataProduct");

            migrationBuilder.DropTable(
                name: "CatalogueList");
        }
    }
}
