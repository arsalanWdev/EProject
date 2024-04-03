using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EProject.Migrations
{
    /// <inheritdoc />
    public partial class DBSug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.CreateTable(
                name: "suggests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminResponse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suggests", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "suggests");

            migrationBuilder.CreateTable(
                name: "usersuggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminReply = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersuggestions", x => x.Id);
                });
        }
    }
}
