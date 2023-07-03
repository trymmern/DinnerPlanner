using System.Text.Json.Nodes;
using DinnerPlanner.Data;
using DinnerPlanner.Data.Models;

namespace DinnerPlanner.TestData;

public class TestDataGenerator
{
    private readonly DinnerPlannerContext _db;
    
    public TestDataGenerator(DinnerPlannerContext db)
    {
        _db = db;
    }

    public void AddTestData()
    {
        AddCategories();
        AddDishes();
    }

    private void AddCategories()
    {
        _db.DishCategories.AddRange(new List<DishCategory>
        {
            new() { Id = 1, Value = "Aperitif" },
            new() { Id = 2, Value = "Main course"},
            new() { Id = 3, Value = "Desert" }
        });
    }
    
    private void AddDishes()
    {
        _db.Dishes.AddRange(new List<Dish>
        {
            new()
            {
                Id = 1,
                Name = "BLT Sandwich",
                Category = _db.DishCategories.First(c => c.Value == "Aperitif"),
                Ingredients = "{{ \"Tomato\", \"1 whole\" }, { \"Bacon\", \"2 strips\" }, { \"Lettuce\", \"2 leaves\" }, { \"Bread\", \"2 slices\" }}"
            },
            new()
            {
                Id = 2,
                Name = "Cereal",
                Category = _db.DishCategories.First(c => c.Value == "Main course"),
                Ingredients = "{ { \"Cereal\", \"300g\" }, { \"Milk\", \"300ml\" } }"
            }
        });
    }
}