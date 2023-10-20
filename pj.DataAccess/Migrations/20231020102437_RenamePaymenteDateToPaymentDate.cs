using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pj.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenamePaymenteDateToPaymentDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymenteDate",
                table: "OrderHeads",
                newName: "PaymentDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "OrderHeads",
                newName: "PaymenteDate");
        }
    }
}
