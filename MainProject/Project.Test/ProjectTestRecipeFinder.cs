namespace Project.Test;

using System;
using System.IO;
using Project;

public class ProjectTestRecipeFinder : IDisposable
{
    private string testFilePath;

    public ProjectTestRecipeFinder()
    {
        // Create a unique temp file for each test
        testFilePath = Path.Combine(Path.GetTempPath(), $"test_recipes_{Guid.NewGuid()}.txt");
    }

    public void Dispose()
    {
        // Clean up temp file after each test
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }

    [Fact]
    void TestFindRecipeByName()
    {
        // Arrange
        Recipes recipes = new Recipes(testFilePath);
        string recipeName = "Test Recipe";
        string instructions = "Test instructions";
        recipes.AddRecipe(recipeName, new List<Ingredient>(), instructions);

        RecipeFinder finder = new RecipeFinder(recipes);

        // Act
        var foundRecipe = finder.Find(recipeName);

        // Assert
        Assert.NotNull(foundRecipe);
        Assert.Equal(recipeName, foundRecipe.Name);
        Assert.Equal(instructions, foundRecipe.Instructions);
    }
}