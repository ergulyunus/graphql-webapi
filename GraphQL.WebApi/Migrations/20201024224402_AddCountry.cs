using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GraphQL.WebApi.Migrations
{
    public partial class AddCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "countryId",
                schema: "business",
                table: "city",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "country",
                schema: "business",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creationDate = table.Column<DateTime>(nullable: true, defaultValueSql: "(now())"),
                    name = table.Column<string>(nullable: true),
                    continent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_city_countryId",
                schema: "business",
                table: "city",
                column: "countryId");

            migrationBuilder.AddForeignKey(
                name: "FK_city_country_countryId",
                schema: "business",
                table: "city",
                column: "countryId",
                principalSchema: "business",
                principalTable: "country",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_city_country_countryId",
                schema: "business",
                table: "city");

            migrationBuilder.DropTable(
                name: "country",
                schema: "business");

            migrationBuilder.DropIndex(
                name: "IX_city_countryId",
                schema: "business",
                table: "city");

            migrationBuilder.DropColumn(
                name: "countryId",
                schema: "business",
                table: "city");
        }
    }
}
