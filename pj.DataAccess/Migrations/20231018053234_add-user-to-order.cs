using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pj.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addusertoorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "OrderHeads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderHeads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "OrderHeads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "OrderHeads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "OrderHeads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "OrderHeads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "OrderHeads");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderHeads");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "OrderHeads");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "OrderHeads");

            migrationBuilder.DropColumn(
                name: "State",
                table: "OrderHeads");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "OrderHeads");
        }
    }
}
