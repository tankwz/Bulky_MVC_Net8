using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace pj.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class companydummydata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "PostalCode", "State", "StressAddress" },
                values: new object[,]
                {
                    { 1, "Colalombua", "SFX Coop", "012312312", "AAA123", "Texa", "3/2 abc xyz" },
                    { 2, "Xiaxia", "KMS Coop", "0111111233", "BBB345", "Texa", "532 ax" },
                    { 3, "That city", "ABC Coop", "00123233", "123BBB", "That state", "6/3 351s as" },
                    { 4, "AAAA", "Last Coop", "1551251252", "15325", "BBBBBB", "3/2 abc xyz" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
