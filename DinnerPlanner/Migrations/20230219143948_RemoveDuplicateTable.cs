using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DinnerPlanner.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDuplicateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DinnerPlanPersons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DinnerPlanPersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DinnerPlanId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DinnerPlanPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DinnerPlanPersons_DinnerPlans_DinnerPlanId",
                        column: x => x.DinnerPlanId,
                        principalTable: "DinnerPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DinnerPlanPersons_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DinnerPlanPersons_DinnerPlanId",
                table: "DinnerPlanPersons",
                column: "DinnerPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DinnerPlanPersons_PersonId",
                table: "DinnerPlanPersons",
                column: "PersonId");
        }
    }
}
