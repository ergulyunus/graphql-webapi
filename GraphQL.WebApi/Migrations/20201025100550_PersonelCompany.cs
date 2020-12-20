using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GraphQL.WebApi.Migrations
{
    public partial class PersonelCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "company",
                schema: "business",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creationDate = table.Column<DateTime>(nullable: true, defaultValueSql: "(now())"),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "personel",
                schema: "business",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creationDate = table.Column<DateTime>(nullable: true, defaultValueSql: "(now())"),
                    firtstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    birthday = table.Column<DateTime>(nullable: true),
                    eMail = table.Column<string>(nullable: true),
                    companyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personel", x => x.id);
                    table.ForeignKey(
                        name: "FK_personel_company_companyId",
                        column: x => x.companyId,
                        principalSchema: "business",
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_personel_companyId",
                schema: "business",
                table: "personel",
                column: "companyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "personel",
                schema: "business");

            migrationBuilder.DropTable(
                name: "company",
                schema: "business");
        }
    }
}
