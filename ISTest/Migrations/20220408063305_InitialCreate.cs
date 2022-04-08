using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTest.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beverages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(2,2)", precision: 2, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beverages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendingMachines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendingMachines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeverageToVendingMachines",
                columns: table => new
                {
                    BeverageId = table.Column<int>(type: "int", nullable: false),
                    VendingMachineId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeverageToVendingMachines", x => new { x.BeverageId, x.VendingMachineId });
                    table.ForeignKey(
                        name: "FK_BeverageToVendingMachines_Beverages_BeverageId",
                        column: x => x.BeverageId,
                        principalTable: "Beverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeverageToVendingMachines_VendingMachines_VendingMachineId",
                        column: x => x.VendingMachineId,
                        principalTable: "VendingMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinToVendingMachines",
                columns: table => new
                {
                    CoinId = table.Column<int>(type: "int", nullable: false),
                    VendingMachineId = table.Column<int>(type: "int", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinToVendingMachines", x => new { x.CoinId, x.VendingMachineId });
                    table.ForeignKey(
                        name: "FK_CoinToVendingMachines_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinToVendingMachines_VendingMachines_VendingMachineId",
                        column: x => x.VendingMachineId,
                        principalTable: "VendingMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Beverages",
                columns: new[] { "Id", "Name", "Price", "Volume" },
                values: new object[,]
                {
                    { 1, "Coca-Cola", 20m, 0.33m },
                    { 2, "Fanta", 17m, 0.33m },
                    { 3, "Sprite", 15m, 0.33m },
                    { 4, "Merinda", 22m, 0.33m },
                    { 5, "BonAqua", 23m, 0.5m }
                });

            migrationBuilder.InsertData(
                table: "Coins",
                columns: new[] { "Id", "ImagePath", "Value" },
                values: new object[,]
                {
                    { 1, "97(16)_1R_R.gif", 1m },
                    { 2, "97(16)_2R_R.gif", 2m },
                    { 3, "97(16)_5R_R.gif", 5m },
                    { 4, "97(16)_10R_R.gif", 10m }
                });

            migrationBuilder.InsertData(
                table: "VendingMachines",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Автомат на Уральской" });

            migrationBuilder.InsertData(
                table: "BeverageToVendingMachines",
                columns: new[] { "BeverageId", "VendingMachineId", "Number" },
                values: new object[,]
                {
                    { 1, 1, 10 },
                    { 2, 1, 10 },
                    { 3, 1, 10 },
                    { 4, 1, 10 }
                });

            migrationBuilder.InsertData(
                table: "CoinToVendingMachines",
                columns: new[] { "CoinId", "VendingMachineId", "Amount", "Disabled" },
                values: new object[,]
                {
                    { 1, 1, 100, false },
                    { 2, 1, 100, false },
                    { 3, 1, 100, false },
                    { 4, 1, 100, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeverageToVendingMachines_VendingMachineId",
                table: "BeverageToVendingMachines",
                column: "VendingMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinToVendingMachines_VendingMachineId",
                table: "CoinToVendingMachines",
                column: "VendingMachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeverageToVendingMachines");

            migrationBuilder.DropTable(
                name: "CoinToVendingMachines");

            migrationBuilder.DropTable(
                name: "Beverages");

            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "VendingMachines");
        }
    }
}
