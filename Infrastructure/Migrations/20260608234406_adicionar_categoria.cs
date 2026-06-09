using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adicionar_categoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "category",
                table: "transaction",
                newName: "categoryId");

            migrationBuilder.AddColumn<bool>(
                name: "completed",
                table: "goal",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "deadline",
                table: "goal",
                type: "date",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_category_user_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transaction_categoryId",
                table: "transaction",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_category_userId_name_type",
                table: "category",
                columns: new[] { "userId", "name", "type" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_transaction_category_categoryId",
                table: "transaction",
                column: "categoryId",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transaction_category_categoryId",
                table: "transaction");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropIndex(
                name: "IX_transaction_categoryId",
                table: "transaction");

            migrationBuilder.DropColumn(
                name: "completed",
                table: "goal");

            migrationBuilder.DropColumn(
                name: "deadline",
                table: "goal");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "transaction",
                newName: "category");
        }
    }
}
