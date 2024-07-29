using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global.CarManagement.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialEstruct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_BRAND",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_BRAND", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_PHOTO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BASE64 = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PHOTO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_CAR",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "varchar(200)", nullable: false),
                    DETAILS = table.Column<string>(type: "varchar(max)", nullable: false),
                    PRICE = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    DT_CREATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DT_UPDATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    PHOTO_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BRAND_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CAR", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TB_CAR_TB_BRAND_BRAND_ID",
                        column: x => x.BRAND_ID,
                        principalTable: "TB_BRAND",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_CAR_TB_PHOTO_PHOTO_ID",
                        column: x => x.PHOTO_ID,
                        principalTable: "TB_PHOTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CAR_BRAND_ID",
                table: "TB_CAR",
                column: "BRAND_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CAR_PHOTO_ID",
                table: "TB_CAR",
                column: "PHOTO_ID");


            migrationBuilder.Sql(@"
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('3849fd79-0666-43fe-b390-ce2ef4cbcba7', 'Brand 1');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('4b6350d5-5f9c-4925-ba4b-703a14763a81', 'Brand 2');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('dea39053-3104-4e23-8675-fe361ffb733d', 'Brand 3');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('8bbc8343-f18e-4872-9038-8449d205ed1b', 'Brand 4');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('94536ead-466a-43ef-9146-a12683c50464', 'Brand 5');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('f7dcacc4-bd09-4c96-81cb-263bb4f058c2', 'Brand 6');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('7e8f2363-e812-4e19-8612-448ae2e109cf', 'Brand 7');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('be75e2cb-e129-4537-81c3-22a2f92ea7ef', 'Brand 8');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('bf314b5b-2e81-439d-aab2-11bfbd8f0f59', 'Brand 9');
                INSERT INTO TB_BRAND (ID, NAME) VALUES ('2534ac19-952b-4335-9797-81ca22220f35', 'Brand 10');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CAR");

            migrationBuilder.DropTable(
                name: "TB_BRAND");

            migrationBuilder.DropTable(
                name: "TB_PHOTO");
        }
    }
}
