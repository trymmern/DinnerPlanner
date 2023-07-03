using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DinnerPlanner.Migrations
{
    /// <inheritdoc />
    public partial class AddDinnerPlanDishTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_DinnerPlans_DinnerPlanId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_DinnerPlanId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "DinnerPlanId",
                table: "Dishes");

            migrationBuilder.CreateTable(
                name: "DinnerPlanDish",
                columns: table => new
                {
                    DinnerPlansId = table.Column<int>(type: "int", nullable: false),
                    DishesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DinnerPlanDish", x => new { x.DinnerPlansId, x.DishesId });
                    table.ForeignKey(
                        name: "FK_DinnerPlanDish_DinnerPlans_DinnerPlansId",
                        column: x => x.DinnerPlansId,
                        principalTable: "DinnerPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DinnerPlanDish_Dishes_DishesId",
                        column: x => x.DishesId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DinnerPlanDishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DinnerPlanId = table.Column<int>(type: "int", nullable: false),
                    DishId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DinnerPlanDishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DinnerPlanDishes_DinnerPlans_DinnerPlanId",
                        column: x => x.DinnerPlanId,
                        principalTable: "DinnerPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DinnerPlanDishes_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DinnerPlanDish_DishesId",
                table: "DinnerPlanDish",
                column: "DishesId");

            migrationBuilder.CreateIndex(
                name: "IX_DinnerPlanDishes_DinnerPlanId",
                table: "DinnerPlanDishes",
                column: "DinnerPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DinnerPlanDishes_DishId",
                table: "DinnerPlanDishes",
                column: "DishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DinnerPlanDish");

            migrationBuilder.DropTable(
                name: "DinnerPlanDishes");

            migrationBuilder.AddColumn<int>(
                name: "DinnerPlanId",
                table: "Dishes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_DinnerPlanId",
                table: "Dishes",
                column: "DinnerPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_DinnerPlans_DinnerPlanId",
                table: "Dishes",
                column: "DinnerPlanId",
                principalTable: "DinnerPlans",
                principalColumn: "Id");
        }
    }
}
