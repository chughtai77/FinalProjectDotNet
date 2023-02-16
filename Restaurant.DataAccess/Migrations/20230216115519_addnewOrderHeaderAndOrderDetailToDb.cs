using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantWeb.Migrations
{
    public partial class addnewOrderHeaderAndOrderDetailToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "OrderHeader");
        }
    }
}
