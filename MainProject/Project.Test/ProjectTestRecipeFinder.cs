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
    public void FindRecipeByName_ExistingRecipe_ReturnsRecipe()
    {
        // Arrange
        string recipeName = "Test Pancakes";
        string recipeInstructions = "Mix ingredients and cook on griddle.";
        string recipeLine = $"{recipeName},Flour,2 cups,Eggs,2,Milk,1 cup,{recipeInstructions}";
        File.WriteAllText(testFilePath, recipeLine);
        Recipes recipes = new Recipes(testFilePath);
        RecipeFinder finder = new RecipeFinder(recipes);

        // Act
        var r = new Recipes(testFilePath);
        var result = r.FindByName(recipeName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(recipeName, result.Name);
        Assert.Equal(recipeInstructions, result.Instructions);
    }

    [Fact]
    public void FindRecipeByName_NonExistingRecipe_ReturnsNull()
    {
        // Arrange
        string recipeName = "Non-Existent Recipe";
        File.WriteAllText(testFilePath, "Some Other Recipe,Ingredient,1,Instructions");
        Recipes recipes = new Recipes(testFilePath);
        RecipeFinder finder = new RecipeFinder(recipes);
        // Act
        var r = new Recipes(testFilePath);
        var result = r.FindByName(recipeName);
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void FindRecipeByTerm_ExistingTerm_ReturnsRecipe()
    {
        // Arrange
        string recipeName = "Test Pancakes";
        string recipeInstructions = "Mix ingredients and cook on griddle.";
        string recipeLine = $"{recipeName},Flour,2 cups,Eggs,2,Milk,1 cup,{recipeInstructions}";
        File.WriteAllText(testFilePath, recipeLine);
        Recipes recipes = new Recipes(testFilePath);
        RecipeFinder finder = new RecipeFinder(recipes);
        // Act
        var r = new Recipes(testFilePath);
        var result = r.FindByTerm("Pancakes");
        // Assert
        Assert.NotNull(result);
        Assert.Equal(recipeName, result.Name);
    }

    [Fact]
    public void FindRecipeByTerm_NonExistingTerm_ReturnsNull()
    {
        // Arrange
        string recipeName = "Test Pancakes";
        string recipeInstructions = "Mix ingredients and cook on griddle.";
        string recipeLine = $"{recipeName},Flour,2 cups,Eggs,2,Milk,1 cup,{recipeInstructions}";
        File.WriteAllText(testFilePath, recipeLine);
        Recipes recipes = new Recipes(testFilePath);
        RecipeFinder finder = new RecipeFinder(recipes);
        // Act
        var r = new Recipes(testFilePath);
        var result = r.FindByTerm("Waffles");
        // Assert
        Assert.Null(result);
    }
}