using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pj.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addsessionid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentSessionId",
                table: "OrderHeads",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentSessionId",
                table: "OrderHeads");
        }
    }
}
