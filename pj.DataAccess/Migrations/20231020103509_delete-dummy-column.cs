using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pj.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class deletedummycolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price3",
                table: "OrderDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price3",
                table: "OrderDetail",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
