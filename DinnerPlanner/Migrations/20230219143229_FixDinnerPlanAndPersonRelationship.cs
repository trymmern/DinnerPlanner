using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DinnerPlanner.Migrations
{
    /// <inheritdoc />
    public partial class FixDinnerPlanAndPersonRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DinnerPlans_Persons_OrganizerId",
                table: "DinnerPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_DinnerPlans_DinnerPlanId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_DinnerPlanId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_DinnerPlans_OrganizerId",
                table: "DinnerPlans");

            migrationBuilder.DropColumn(
                name: "DinnerPlanId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "DinnerPlans");

            migrationBuilder.CreateTable(
                name: "DinnerPlanPerson",
                columns: table => new
                {
                    DinnerPlansId = table.Column<int>(type: "int", nullable: false),
                    PersonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DinnerPlanPerson", x => new { x.DinnerPlansId, x.PersonsId });
                    table.ForeignKey(
                        name: "FK_DinnerPlanPerson_DinnerPlans_DinnerPlansId",
                        column: x => x.DinnerPlansId,
                        principalTable: "DinnerPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DinnerPlanPerson_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_DinnerPlanPerson_PersonsId",
                table: "DinnerPlanPerson",
                column: "PersonsId");

            migrationBuilder.CreateIndex(
                name: "IX_DinnerPlanPersons_DinnerPlanId",
                table: "DinnerPlanPersons",
                column: "DinnerPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DinnerPlanPersons_PersonId",
                table: "DinnerPlanPersons",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DinnerPlanPerson");

            migrationBuilder.DropTable(
                name: "DinnerPlanPersons");

            migrationBuilder.AddColumn<int>(
                name: "DinnerPlanId",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                table: "DinnerPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DinnerPlanId",
                table: "Persons",
                column: "DinnerPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DinnerPlans_OrganizerId",
                table: "DinnerPlans",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DinnerPlans_Persons_OrganizerId",
                table: "DinnerPlans",
                column: "OrganizerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_DinnerPlans_DinnerPlanId",
                table: "Persons",
                column: "DinnerPlanId",
                principalTable: "DinnerPlans",
                principalColumn: "Id");
        }
    }
}
