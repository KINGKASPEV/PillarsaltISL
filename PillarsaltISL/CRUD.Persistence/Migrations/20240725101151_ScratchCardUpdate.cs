using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ScratchCardUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPurchased",
                table: "ScratchCards",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPurchased",
                table: "ScratchCards");
        }
    }
}
