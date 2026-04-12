namespace Project.Test;

using System;
using System.IO;
using System.Collections.Generic;
using Project;

public class ProjectTestRecipes : IDisposable
{
    private string testFilePath;

    public ProjectTestRecipes()
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
    void TestAddRecipe()
    {
        Recipes recipes = new Recipes(testFilePath);
        string name = "Pasta";
        var ingredients = new List<Ingredient>
        {
            new Ingredient("Noodles", "400g"),
            new Ingredient("Tomato Sauce", "2 cups")
        };
        string instructions = "Boil and mix";
        
        bool result = recipes.AddRecipe(name, ingredients, instructions);
        
        Assert.True(result);
        Assert.Single(recipes.GetRecipeList());
        Assert.Equal(name, recipes.GetRecipeList()[0].Name);
        Assert.Equal(instructions, recipes.GetRecipeList()[0].Instructions);
        Assert.Equal(2, recipes.GetRecipeList()[0].Ingredients.Count);
    }

    [Fact]
    void TestAddMultipleRecipes()
    {
        Recipes recipes = new Recipes(testFilePath);
        
        bool result1 = recipes.AddRecipe("Pasta", new List<Ingredient> { new Ingredient("Noodles", "400g") }, "Boil and mix");
        bool result2 = recipes.AddRecipe("Pizza", new List<Ingredient> { new Ingredient("Dough", "500g") }, "Bake at 400F");
        bool result3 = recipes.AddRecipe("Salad", new List<Ingredient> { new Ingredient("Lettuce", "1 head") }, "Mix vegetables");
        
        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
        Assert.Equal(3, recipes.GetRecipeList().Count);
    }

    [Fact]
    void TestAddRecipeEmptyName()
    {
        Recipes recipes = new Recipes(testFilePath);
        
        bool result = recipes.AddRecipe("", new List<Ingredient>(), "Instructions");
        
        Assert.False(result);
        Assert.Empty(recipes.GetRecipeList());
    }

    [Fact]
    void TestAddRecipeEmptyInstructions()
    {
        Recipes recipes = new Recipes(testFilePath);
        
        bool result = recipes.AddRecipe("Pasta", new List<Ingredient>(), "");
        
        Assert.False(result);
        Assert.Empty(recipes.GetRecipeList());
    }

    [Fact]
    void TestAddRecipeWhitespaceName()
    {
        Recipes recipes = new Recipes(testFilePath);
        
        bool result = recipes.AddRecipe("   ", new List<Ingredient>(), "Instructions");
        
        Assert.False(result);
        Assert.Empty(recipes.GetRecipeList());
    }

    [Fact]
    void TestAddRecipeWhitespaceInstructions()
    {
        Recipes recipes = new Recipes(testFilePath);
        
        bool result = recipes.AddRecipe("Pasta", new List<Ingredient>(), "   ");
        
        Assert.False(result);
        Assert.Empty(recipes.GetRecipeList());
    }

    [Fact]
    void TestAddRecipeNoIngredients()
    {
        Recipes recipes = new Recipes(testFilePath);
        
        bool result = recipes.AddRecipe("Simple Recipe", new List<Ingredient>(), "Just instructions");
        
        Assert.True(result);
        Assert.Single(recipes.GetRecipeList());
        Assert.Empty(recipes.GetRecipeList()[0].Ingredients);
    }

    [Fact]
    void TestEditRecipe()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient> { new Ingredient("Noodles", "400g") }, "Boil");
        
        var newIngredients = new List<Ingredient> { new Ingredient("Penne", "500g") };
        bool result = recipes.EditRecipe(1, "Penne Pasta", newIngredients, "Boil and garnish");
        
        Assert.True(result);
        Assert.Equal("Penne Pasta", recipes.GetRecipeList()[0].Name);
        Assert.Equal("Boil and garnish", recipes.GetRecipeList()[0].Instructions);
        Assert.Single(recipes.GetRecipeList()[0].Ingredients);
        Assert.Equal("Penne", recipes.GetRecipeList()[0].Ingredients[0].name);
    }

    [Fact]
    void TestEditRecipeMultiple()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        recipes.AddRecipe("Pizza", new List<Ingredient>(), "Bake");
        recipes.AddRecipe("Salad", new List<Ingredient>(), "Mix");
        
        var newIngredients = new List<Ingredient> { new Ingredient("Cheese", "200g") };
        bool result = recipes.EditRecipe(2, "Pizza Fresh", newIngredients, "Bake at 500F");
        
        Assert.True(result);
        Assert.Equal("Pizza Fresh", recipes.GetRecipeList()[1].Name);
        // Others should be unchanged
        Assert.Equal("Pasta", recipes.GetRecipeList()[0].Name);
        Assert.Equal("Salad", recipes.GetRecipeList()[2].Name);
    }

    [Fact]
    void TestEditRecipeInvalidIndex()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        
        var newIngredients = new List<Ingredient>();
        bool result = recipes.EditRecipe(99, "New Name", newIngredients, "New instructions");
        
        Assert.False(result);
        Assert.Equal("Pasta", recipes.GetRecipeList()[0].Name);
    }

    [Fact]
    void TestEditRecipeZeroIndex()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        
        var newIngredients = new List<Ingredient>();
        bool result = recipes.EditRecipe(0, "New Name", newIngredients, "New instructions");
        
        Assert.False(result);
    }

    [Fact]
    void TestEditRecipeEmptyName()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        
        var newIngredients = new List<Ingredient>();
        bool result = recipes.EditRecipe(1, "", newIngredients, "New instructions");
        
        Assert.False(result);
        Assert.Equal("Pasta", recipes.GetRecipeList()[0].Name);
    }

    [Fact]
    void TestEditRecipeEmptyInstructions()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        
        var newIngredients = new List<Ingredient>();
        bool result = recipes.EditRecipe(1, "New Name", newIngredients, "");
        
        Assert.False(result);
        Assert.Equal("Pasta", recipes.GetRecipeList()[0].Name);
    }

    [Fact]
    void TestDeleteRecipe()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        
        bool result = recipes.DeleteRecipe(1);
        
        Assert.True(result);
        Assert.Empty(recipes.GetRecipeList());
    }

    [Fact]
    void TestDeleteRecipeMultiple()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        recipes.AddRecipe("Pizza", new List<Ingredient>(), "Bake");
        recipes.AddRecipe("Salad", new List<Ingredient>(), "Mix");
        
        bool result = recipes.DeleteRecipe(2);
        
        Assert.True(result);
        Assert.Equal(2, recipes.GetRecipeList().Count);
        Assert.Equal("Salad", recipes.GetRecipeList()[1].Name);
    }

    [Fact]
    void TestDeleteRecipeInvalidIndex()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        
        bool result = recipes.DeleteRecipe(99);
        
        Assert.False(result);
        Assert.Single(recipes.GetRecipeList());
    }

    [Fact]
    void TestDeleteRecipeZeroIndex()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        
        bool result = recipes.DeleteRecipe(0);
        
        Assert.False(result);
        Assert.Single(recipes.GetRecipeList());
    }

    [Fact]
    void TestGetRecipeList()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        recipes.AddRecipe("Pizza", new List<Ingredient>(), "Bake");
        
        var list = recipes.GetRecipeList();
        
        Assert.Equal(2, list.Count);
        Assert.Equal("Pasta", list[0].Name);
        Assert.Equal("Pizza", list[1].Name);
    }

    [Fact]
    void TestGetRecipeListEmpty()
    {
        Recipes recipes = new Recipes(testFilePath);
        
        var list = recipes.GetRecipeList();
        
        Assert.Empty(list);
    }

    [Fact]
    void TestSetRecipeList()
    {
        Recipes recipes = new Recipes(testFilePath);
        var newList = new List<Recipe>
        {
            new Recipe("Soup", new List<Ingredient>(), "Simmer"),
            new Recipe("Stew", new List<Ingredient>(), "Slow cook")
        };
        
        recipes.SetRecipeList(newList);
        
        var list = recipes.GetRecipeList();
        Assert.Equal(2, list.Count);
        Assert.Equal("Soup", list[0].Name);
        Assert.Equal("Stew", list[1].Name);
    }

    [Fact]
    void TestRecipeWithMultipleIngredients()
    {
        Recipes recipes = new Recipes(testFilePath);
        var ingredients = new List<Ingredient>
        {
            new Ingredient("Flour", "2 cups"),
            new Ingredient("Sugar", "1 cup"),
            new Ingredient("Butter", "1/2 cup"),
            new Ingredient("Eggs", "2"),
            new Ingredient("Vanilla", "1 tsp")
        };
        
        bool result = recipes.AddRecipe("Cake", ingredients, "Mix and bake");
        
        Assert.True(result);
        Assert.Equal(5, recipes.GetRecipeList()[0].Ingredients.Count);
    }

    [Fact]
    void TestListRecipes()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        recipes.AddRecipe("Pizza", new List<Ingredient>(), "Bake");
        
        var list = recipes.GetRecipeList();
        recipes.ListRecipes(list);
        
        Assert.Equal(2, list.Count);
    }

    [Fact]
    void TestListRecipesEmpty()
    {
        Recipes recipes = new Recipes(testFilePath);
        
        var list = recipes.GetRecipeList();
        recipes.ListRecipes(list);
        
        Assert.Empty(list);
    }

    [Fact]
    void TestRecipePersistence()
    {
        // Add recipes with first instance
        var recipes1 = new Recipes(testFilePath);
        recipes1.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        recipes1.AddRecipe("Pizza", new List<Ingredient>(), "Bake");
        
        // Load with new instance
        var recipes2 = new Recipes(testFilePath);
        var loadedList = recipes2.GetRecipeList();
        
        Assert.Equal(2, loadedList.Count);
        Assert.Equal("Pasta", loadedList[0].Name);
        Assert.Equal("Pizza", loadedList[1].Name);
    }

    [Fact]
    void TestRecipePersistenceWithIngredients()
    {
        // Add recipes with ingredients
        var recipes1 = new Recipes(testFilePath);
        var ingredients = new List<Ingredient>
        {
            new Ingredient("Noodles", "400g"),
            new Ingredient("Sauce", "2 cups")
        };
        recipes1.AddRecipe("Pasta", ingredients, "Boil and mix");
        
        // Load with new instance
        var recipes2 = new Recipes(testFilePath);
        var loadedList = recipes2.GetRecipeList();
        
        Assert.Single(loadedList);
        Assert.Equal("Pasta", loadedList[0].Name);
        Assert.Equal("Boil and mix", loadedList[0].Instructions);
        Assert.Equal(2, loadedList[0].Ingredients.Count);
        Assert.Equal("Noodles", loadedList[0].Ingredients[0].name);
        Assert.Equal("400g", loadedList[0].Ingredients[0].quantity);
        Assert.Equal("Sauce", loadedList[0].Ingredients[1].name);
        Assert.Equal("2 cups", loadedList[0].Ingredients[1].quantity);
    }

    [Fact]
    void TestEditAndDelete()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        recipes.AddRecipe("Pizza", new List<Ingredient>(), "Bake");
        recipes.AddRecipe("Salad", new List<Ingredient>(), "Mix");
        
        // Edit the first one
        recipes.EditRecipe(1, "Spaghetti", new List<Ingredient>(), "Boil with sauce");
        Assert.Equal("Spaghetti", recipes.GetRecipeList()[0].Name);
        
        // Delete the second one
        recipes.DeleteRecipe(2);
        Assert.Equal(2, recipes.GetRecipeList().Count);
        Assert.Equal("Spaghetti", recipes.GetRecipeList()[0].Name);
        Assert.Equal("Salad", recipes.GetRecipeList()[1].Name);
    }

    [Fact]
    void TestRecipeNameCaseSensitivity()
    {
        Recipes recipes = new Recipes(testFilePath);
        recipes.AddRecipe("Pasta", new List<Ingredient>(), "Boil");
        recipes.AddRecipe("PASTA", new List<Ingredient>(), "Boil");
        
        // Both should be added since names are case-sensitive
        Assert.Equal(2, recipes.GetRecipeList().Count);
        Assert.Equal("Pasta", recipes.GetRecipeList()[0].Name);
        Assert.Equal("PASTA", recipes.GetRecipeList()[1].Name);
    }

    [Fact]
    void TestRecipeToString()
    {
        var ingredients = new List<Ingredient>
        {
            new Ingredient("Noodles", "400g"),
            new Ingredient("Sauce", "2 cups")
        };
        var recipe = new Recipe("Pasta", ingredients, "Boil and mix");
        var result = recipe.ToString();
        
        Assert.Contains("Pasta", result);
        Assert.Contains("Noodles", result);
        Assert.Contains("Sauce", result);
        Assert.Contains("Boil and mix", result);
        Assert.Contains("[yellow]Name:[/]", result);
        Assert.Contains("[yellow]Ingredients:[/]", result);
        Assert.Contains("[yellow]Instructions:[/]", result);
    }

    [Fact]
    void TestRecipeToStringEmptyIngredients()
    {
        var recipe = new Recipe("Simple Recipe", new List<Ingredient>(), "Just instructions");
        var result = recipe.ToString();
        
        Assert.Contains("Simple Recipe", result);
        Assert.Contains("Just instructions", result);
        Assert.Contains("[yellow]Ingredients:[/]", result);
    }

    [Fact]
    void TestRecipeProperties()
    {
        var ingredients = new List<Ingredient> { new Ingredient("Flour", "2 cups") };
        var recipe = new Recipe("Cake", ingredients, "Bake at 350F");
        
        Assert.Equal("Cake", recipe.Name);
        Assert.Equal("Bake at 350F", recipe.Instructions);
        Assert.Single(recipe.Ingredients);
        Assert.Equal("Flour", recipe.Ingredients[0].name);
    }

    [Fact]
    void TestRecipeConstructor()
    {
        var ingredients = new List<Ingredient>
        {
            new Ingredient("Eggs", "2"),
            new Ingredient("Milk", "1 cup")
        };
        var recipe = new Recipe("Scrambled Eggs", ingredients, "Mix and cook");
        
        Assert.NotNull(recipe);
        Assert.Equal("Scrambled Eggs", recipe.Name);
        Assert.Equal(2, recipe.Ingredients.Count);
        Assert.Equal("Mix and cook", recipe.Instructions);
    }

    [Fact]
    void TestRecipesConstructorWithCustomFile()
    {
        var recipes = new Recipes(testFilePath);
        
        Assert.NotNull(recipes);
        Assert.Empty(recipes.GetRecipeList());
    }

    [Fact]
    void TestRecipesFilePersistence()
    {
        // Test that recipes are loaded from file correctly
        var recipes1 = new Recipes(testFilePath);
        recipes1.AddRecipe("Test Recipe", new List<Ingredient>(), "Test instructions");
        
        // Create new instance to test loading from file
        var recipes2 = new Recipes(testFilePath);
        var loadedList = recipes2.GetRecipeList();
        
        Assert.Single(loadedList);
        Assert.Equal("Test Recipe", loadedList[0].Name);
        Assert.Equal("Test instructions", loadedList[0].Instructions);
    }

    [Fact]
    void TestRecipesFileWithInvalidData()
    {
        // Create a file with invalid data (missing comma)
        File.WriteAllText(testFilePath, "InvalidLineWithoutComma\n");
        var recipes = new Recipes(testFilePath);
        
        // Should handle invalid data gracefully
        Assert.Empty(recipes.GetRecipeList());
    }

    [Fact]
    void TestRecipesFileWithIncompleteData()
    {
        // Create a file with incomplete data (only one field)
        File.WriteAllText(testFilePath, "OnlyOneField\n");
        var recipes = new Recipes(testFilePath);
        
        // Should handle incomplete data gracefully
        Assert.Empty(recipes.GetRecipeList());
    }
}
