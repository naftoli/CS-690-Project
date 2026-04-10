namespace Project.Test;

using System;
using System.IO;
using Project;

public class ProjectTestIngredients : IDisposable
{
    private string testFilePath;

    public ProjectTestIngredients()
    {
        // Create a unique temp file for each test
        testFilePath = Path.Combine(Path.GetTempPath(), $"test_ingredients_{Guid.NewGuid()}.txt");
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
    void TestIngredientsConstructorWithCustomFile()
    {
        var ingredients = new Ingredients(testFilePath);
        
        Assert.NotNull(ingredients);
        Assert.Empty(ingredients.GetIngredientList());
    }

        [Fact]
    void TestIngredientsFilePersistence()
    {
        // Test that ingredients are loaded from file correctly
        var ingredients1 = new Ingredients(testFilePath);
        ingredients1.AddIngredient("Test Ingredient", "1 cup");
        
        // Create new instance to test loading from file
        var ingredients2 = new Ingredients(testFilePath);
        var loadedList = ingredients2.GetIngredientList();
        
        Assert.Single(loadedList);
        Assert.Equal("Test Ingredient", loadedList[0].name);
        Assert.Equal("1 cup", loadedList[0].quantity);
    }

        [Fact]
    void TestIngredientsFileWithInvalidData()
    {
        // Create a file with invalid data (missing comma)
        File.WriteAllText(testFilePath, "InvalidLineWithoutComma\n");
        var ingredients = new Ingredients(testFilePath);
        
        // Should handle invalid data gracefully
        Assert.Empty(ingredients.GetIngredientList());
    }

        [Fact]
    void TestIngredientsFileWithIncompleteData()
    {
        // Create a file with incomplete data (only one field)
        File.WriteAllText(testFilePath, "OnlyOneField\n");
        var ingredients = new Ingredients(testFilePath);
        
        // Should handle incomplete data gracefully
        Assert.Empty(ingredients.GetIngredientList());
    }

    [Fact]
    void TestAddIngredient()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        string name = "Tomato";
        string quantity = "2 cups";
        
        bool result = ingredients.AddIngredient(name, quantity);
        
        Assert.True(result);
        Assert.Contains(ingredients.GetIngredientList(), i => i.name == name && i.quantity == quantity);
    }

        [Fact]
    void TestAddMultipleIngredients()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        
        bool result1 = ingredients.AddIngredient("Tomato", "2 cups");
        bool result2 = ingredients.AddIngredient("Lettuce", "1 head");
        bool result3 = ingredients.AddIngredient("Carrot", "3");
        
        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
        Assert.Equal(3, ingredients.GetIngredientList().Count);
    }

        [Fact]
    void TestAddIngredientValidation()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        
        // Test with valid data
        bool result = ingredients.AddIngredient("Onion", "2");
        Assert.True(result);
        Assert.Single(ingredients.GetIngredientList());
    }

        [Fact]
    void TestEditIngredient()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        ingredients.AddIngredient("Lettuce", "1 head");
        
        bool result = ingredients.EditIngredient(1, "Spinach", "2 cups");
        
        Assert.True(result);
        Assert.Equal("Spinach", ingredients.GetIngredientList()[0].name);
        Assert.Equal("2 cups", ingredients.GetIngredientList()[0].quantity);
    }

        [Fact]
    void TestEditIngredientMultiple()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        ingredients.AddIngredient("Tomato", "2");
        ingredients.AddIngredient("Lettuce", "1");
        ingredients.AddIngredient("Carrot", "3");
        
        bool result = ingredients.EditIngredient(2, "Spinach", "2 cups");
        
        Assert.True(result);
        Assert.Equal("Spinach", ingredients.GetIngredientList()[1].name);
        // First and third should be unchanged
        Assert.Equal("Tomato", ingredients.GetIngredientList()[0].name);
        Assert.Equal("Carrot", ingredients.GetIngredientList()[2].name);
    }

        [Fact]
    void TestEditIngredientInvalidIndex()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        ingredients.AddIngredient("Carrot", "3");
        
        bool result = ingredients.EditIngredient(99, "Potato", "2");
        
        Assert.False(result);
        // Original should be unchanged
        Assert.Equal("Carrot", ingredients.GetIngredientList()[0].name);
    }

        [Fact]
    void TestEditIngredientZeroIndex()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        ingredients.AddIngredient("Carrot", "3");
        
        bool result = ingredients.EditIngredient(0, "Potato", "2");
        
        Assert.False(result);
    }

        [Fact]
    void TestDeleteIngredient()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        ingredients.AddIngredient("Carrot", "3");
        
        bool result = ingredients.DeleteIngredient(1);
        
        Assert.True(result);
        Assert.Empty(ingredients.GetIngredientList());
    }

        [Fact]
    void TestDeleteIngredientMultiple()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        ingredients.AddIngredient("Tomato", "2");
        ingredients.AddIngredient("Lettuce", "1");
        ingredients.AddIngredient("Carrot", "3");
        
        bool result = ingredients.DeleteIngredient(2);
        
        Assert.True(result);
        Assert.Equal(2, ingredients.GetIngredientList().Count);
        Assert.Equal("Carrot", ingredients.GetIngredientList()[1].name);
    }

        [Fact]
    void TestDeleteIngredientInvalidIndex()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        ingredients.AddIngredient("Carrot", "3");
        
        bool result = ingredients.DeleteIngredient(99);
        
        Assert.False(result);
        Assert.Single(ingredients.GetIngredientList());
    }

        [Fact]
    void TestDeleteIngredientZeroIndex()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        ingredients.AddIngredient("Carrot", "3");
        
        bool result = ingredients.DeleteIngredient(0);
        
        Assert.False(result);
        Assert.Single(ingredients.GetIngredientList());
    }

        [Fact]
    void TestGetIngredientList()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        ingredients.AddIngredient("Tomato", "2");
        ingredients.AddIngredient("Lettuce", "1");
        
        var list = ingredients.GetIngredientList();
        
        Assert.Equal(2, list.Count);
        Assert.Equal("Tomato", list[0].name);
        Assert.Equal("Lettuce", list[1].name);
    }

        [Fact]
    void TestGetIngredientListEmpty()
    {
        Ingredients ingredients = new Ingredients(testFilePath);
        
        var list = ingredients.GetIngredientList();
        
        Assert.Empty(list);
    }

        [Fact]
    void TestIngredientToString()
    {
        var ingredient = new Ingredient("Tomato", "2 cups");
        var result = ingredient.ToString();
        
        Assert.Contains("Tomato", result);
        Assert.Contains("2 cups", result);
        Assert.Contains("[yellow]Name:[/]", result);
        Assert.Contains("[yellow]Quantity:[/]", result);
    }

        [Fact]
    void TestIngredientProperties()
    {
        var ingredient = new Ingredient("Salt", "1 tsp");
        
        Assert.Equal("Salt", ingredient.name);
        Assert.Equal("1 tsp", ingredient.quantity);
    }

        [Fact]
    void TestIngredientConstructor()
    {
        var ingredient = new Ingredient("Pepper", "1/2 cup");
        
        Assert.NotNull(ingredient);
        Assert.Equal("Pepper", ingredient.name);
        Assert.Equal("1/2 cup", ingredient.quantity);
    }
}