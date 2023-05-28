using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublishingHouse.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CauntryHandBooks",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauntryHandBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenderHandBooks",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderHandBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypesHandBooks",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypesHandBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublishingHouseHandBooks",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishingHouseHandBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityHandBooks",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityHandBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityHandBooks_CauntryHandBooks_CountryId",
                        column: x => x.CountryId,
                        principalTable: "CauntryHandBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Annotation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ProductTypeId = table.Column<short>(type: "smallint", nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfPages = table.Column<short>(type: "smallint", nullable: true),
                    Address = table.Column<string>(type: "varchar(max)", maxLength: 500, nullable: false),
                    PublishingHouseId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypesHandBooks_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypesHandBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_PublishingHouseHandBooks_PublishingHouseId",
                        column: x => x.PublishingHouseId,
                        principalTable: "PublishingHouseHandBooks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonalIDnumber = table.Column<string>(name: "Personal ID number", type: "nvarchar(11)", maxLength: 11, nullable: false),
                    DateOfBirth = table.Column<DateTime>(name: "Date Of Birth", type: "datetime2", nullable: false),
                    Phonenumber = table.Column<string>(name: "Phone number", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenderId = table.Column<short>(type: "smallint", nullable: true),
                    CountryId = table.Column<short>(type: "smallint", nullable: true),
                    CityId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_CauntryHandBooks_CountryId",
                        column: x => x.CountryId,
                        principalTable: "CauntryHandBooks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Authors_CityHandBooks_CityId",
                        column: x => x.CityId,
                        principalTable: "CityHandBooks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Authors_GenderHandBooks_GenderId",
                        column: x => x.GenderId,
                        principalTable: "GenderHandBooks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuthorProducts",
                columns: table => new
                {
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorProducts", x => new { x.AuthorId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_AuthorProducts_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorProducts_ProductId",
                table: "AuthorProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CityId",
                table: "Authors",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CountryId",
                table: "Authors",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_GenderId",
                table: "Authors",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_CityHandBooks_CountryId",
                table: "CityHandBooks",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PublishingHouseId",
                table: "Products",
                column: "PublishingHouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorProducts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "CityHandBooks");

            migrationBuilder.DropTable(
                name: "GenderHandBooks");

            migrationBuilder.DropTable(
                name: "ProductTypesHandBooks");

            migrationBuilder.DropTable(
                name: "PublishingHouseHandBooks");

            migrationBuilder.DropTable(
                name: "CauntryHandBooks");
        }
    }
}
