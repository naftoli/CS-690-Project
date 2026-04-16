namespace Project.test;

using System;
using System.IO;
using Project;

public class ProjectTestMissingIngredientsFinder : IDisposable
{
    private string testRecipesFilePath;
    private string testIngredientsFilePath;

    public ProjectTestMissingIngredientsFinder()
    {
        // Create unique temp files for each test
        testRecipesFilePath = Path.Combine(Path.GetTempPath(), $"test_recipes_{Guid.NewGuid()}.txt");
        testIngredientsFilePath = Path.Combine(Path.GetTempPath(), $"test_ingredients_{Guid.NewGuid()}.txt");
    }

    public void Dispose()
    {
        // Clean up temp files after each test
        if (File.Exists(testRecipesFilePath))
        {
            File.Delete(testRecipesFilePath);
        }
        if (File.Exists(testIngredientsFilePath))
        {
            File.Delete(testIngredientsFilePath);
        }
    }

    [Fact]
    void TestFindWithMissingIngredients()
    {
        // Set up test data with one recipe and some ingredients
        File.WriteAllText(testIngredientsFilePath, "Ingredient1,1 cup\n");

        // create recipe
        var recipe = new Recipe(
            "Test Recipe",
            new List<Ingredient>
            {
                new Ingredient("Ingredient1", "1 cup"),
                new Ingredient("Ingredient2", "2 cups")
            },
            "Some instructions"
        );

        // Create a MissingIngredientsFinder instance and load test data
        MissingIngredientsFinder finder = new MissingIngredientsFinder(testIngredientsFilePath);
        List<Ingredient> missingIngredients = finder.Find(recipe);

        // Assert that the missing ingredient is correctly identified
        Assert.Single(missingIngredients);
        Assert.Equal("Ingredient2", missingIngredients[0].name);
    }
}