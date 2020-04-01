using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRockConcerts.Data.Migrations
{
    public partial class AddAlbumModelRemoveOrganiser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Concerts_Organisers_OrganiserId",
                table: "Concerts");

            migrationBuilder.DropTable(
                name: "Organisers");

            migrationBuilder.DropIndex(
                name: "IX_Concerts_OrganiserId",
                table: "Concerts");

            migrationBuilder.DropColumn(
                name: "OrganiserId",
                table: "Concerts");

            migrationBuilder.AddColumn<string>(
                name: "TicketUrl",
                table: "Concerts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CoverUrl = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_GroupId",
                table: "Albums",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_IsDeleted",
                table: "Albums",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropColumn(
                name: "TicketUrl",
                table: "Concerts");

            migrationBuilder.AddColumn<int>(
                name: "OrganiserId",
                table: "Concerts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Organisers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Concerts_OrganiserId",
                table: "Concerts",
                column: "OrganiserId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisers_IsDeleted",
                table: "Organisers",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Concerts_Organisers_OrganiserId",
                table: "Concerts",
                column: "OrganiserId",
                principalTable: "Organisers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
