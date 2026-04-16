namespace Project;

using System;
using System.IO;
using System.Collections.Generic;
using Spectre.Console;

public class Recipes
{
    string filePath = "recipes.txt";
    List<Recipe> RecipeList = new List<Recipe>();

    public Recipes()
    {
        filePath = "recipes.txt";
        LoadRecipesFromFile(filePath);
    }

    public Recipes(string customFilePath)
    {
        filePath = customFilePath;
        LoadRecipesFromFile(filePath);
    }

    private void LoadRecipesFromFile(string path)
    {
        // load recipes from file
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] details = line.Split(',');
                    if (details.Length >= 2)
                    {
                        string name = details[0];
                        string instructions = details[details.Length - 1];
                        List<Ingredient> ingredients = new List<Ingredient>();
                        for (int i = 1; i < details.Length - 1; i += 2)
                        {
                            ingredients.Add(new Ingredient(details[i], details[i + 1]));
                        }
                        Recipe r = new Recipe(name, ingredients, instructions);
                        RecipeList.Add(r);
                    }
                }
            }
        } else {
            File.Create(path).Close();
        }
    }

    // Getter for testing purposes
    public List<Recipe> GetRecipeList()
    {
        return RecipeList;
    }

    // Setter for testing purposes
    public void SetRecipeList(List<Recipe> recipes)
    {
        RecipeList = recipes;
    }

    public bool ManageRecipes()
    {
        while (true)
        {
            AnsiConsole.MarkupLine("[bold cyan]Manage Recipes:[/]");
            string selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select an option:")
                    .AddChoices(new string[] { "Add Recipe", "List Recipes", "Edit Recipe", "Delete Recipe", "Back to Main Menu" }));

            switch (selection)
            {
                case "Add Recipe":
                    AddRecipeUI();
                    break;
                case "List Recipes":
                    ListRecipesUI();
                    break;
                case "Edit Recipe":
                    EditRecipeUI();
                    break;
                case "Delete Recipe":
                    DeleteRecipeUI();
                    break;
                case "Back to Main Menu":
                    return true;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid option. Please try again.[/]");
                    break;
            }
        }
    }

    // UI wrapper for AddRecipe - prompts user for input
    public bool AddRecipeUI()
    {
        var name = AnsiConsole.Ask<string>("Enter the recipe name:");
        List<Ingredient> ingredients = new List<Ingredient>();
        while (true)
        {
            var addIngredient = AnsiConsole.Confirm("Do you want to add an ingredient?");
            if (!addIngredient) break;

            var ingredientName = AnsiConsole.Ask<string>("Enter the ingredient name:");
            var ingredientQuantity = AnsiConsole.Ask<string>("Enter the ingredient quantity:");
            Ingredient i = new Ingredient(ingredientName.Trim(), ingredientQuantity.Trim());
            ingredients.Add(i);
        }
        var instructions = AnsiConsole.Ask<string>("Enter the recipe instructions:");
        
        bool result = AddRecipe(name.Trim(), ingredients, instructions.Trim());
        if (result)
        {
            AnsiConsole.MarkupLine("[green]Recipe added successfully![/]");
            Console.WriteLine();
        }
        return result;
    }

    // Testable version - business logic without console input
    public bool AddRecipe(string name, List<Ingredient> ingredients, string instructions)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(instructions))
        {
            return false;
        }

        Recipe r = new Recipe(name, ingredients, instructions);
        RecipeList.Add(r);
        SaveRecipesToFile();
        return true;
    }

    // UI wrapper for ListRecipes
    public void ListRecipesUI()
    {
        ListRecipes(RecipeList);
        Console.WriteLine();
    }

    // Testable version
    public void ListRecipes()
    {
        ListRecipes(RecipeList);
    }

    // Core logic version that works with any list
    public void ListRecipes(List<Recipe> recipes)
    {
        if (recipes.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No recipes found.[/]");
            return;
        }
        
        AnsiConsole.MarkupLine("[bold cyan]Recipes List:[/]");
        foreach (var recipe in recipes)
        {
            AnsiConsole.MarkupLine(recipe.ToString());
        }
    }

    // UI wrapper for EditRecipe - prompts user for selection and input
    public bool EditRecipeUI()
    {
        if (RecipeList.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No recipes found to edit.[/]");
            Console.WriteLine();
            return false;
        }

        AnsiConsole.MarkupLine("[bold cyan]Choose a recipe to edit:[/]");
        for (int i = 0; i < RecipeList.Count; i++)
        {
            AnsiConsole.MarkupLine($"[yellow]{i + 1}.[/] Name: {RecipeList[i].Name}, Instructions: {RecipeList[i].Instructions}");
        }

        var choice = AnsiConsole.Ask<int>("Enter the number of the recipe to edit:");
        var newName = AnsiConsole.Ask<string>("Enter new name:");
        List<Ingredient> newIngredients = new List<Ingredient>();
        while (true)
        {
            var addIngredient = AnsiConsole.Confirm("Do you want to add an ingredient?");
            if (!addIngredient) break;

            var ingredientName = AnsiConsole.Ask<string>("Enter the ingredient name:");
            var ingredientQuantity = AnsiConsole.Ask<string>("Enter the ingredient quantity:");
            Ingredient i = new Ingredient(ingredientName.Trim(), ingredientQuantity.Trim());
            newIngredients.Add(i);
        }
        var newInstructions = AnsiConsole.Ask<string>("Enter new instructions:");
        
        bool result = EditRecipe(choice, newName.Trim(), newIngredients, newInstructions.Trim());
        if (result)
        {
            AnsiConsole.MarkupLine("[green]Recipe updated successfully![/]");
            Console.WriteLine();
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Recipe not found.[/]");
            Console.WriteLine();
        }
        return result;
    }

    // Testable version - takes recipe index and new values as parameters
    public bool EditRecipe(int recipeIndex, string newName, List<Ingredient> newIngredients, string newInstructions)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newInstructions))
        {
            return false;
        }

        if (recipeIndex > 0 && recipeIndex <= RecipeList.Count)
        {
            RecipeList[recipeIndex - 1] = new Recipe(newName, newIngredients, newInstructions);
            SaveRecipesToFile();
            return true;
        }
        return false;
    }

    // UI wrapper for DeleteRecipe - prompts user for selection
    public bool DeleteRecipeUI()
    {
        if (RecipeList.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No recipes found to delete.[/]");
            Console.WriteLine();
            return false;
        }

        AnsiConsole.MarkupLine("[bold cyan]Choose a recipe to delete:[/]");
        for (int i = 0; i < RecipeList.Count; i++)        
        {
            AnsiConsole.MarkupLine($"[yellow]{i + 1}.[/] Name: {RecipeList[i].Name}, Instructions: {RecipeList[i].Instructions}");
        }

        var choice = AnsiConsole.Ask<int>("Enter the number of the recipe to delete:");
        bool result = DeleteRecipe(choice);
        if (result)
        {
            AnsiConsole.MarkupLine("[green]Recipe deleted successfully![/]");
            Console.WriteLine();
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Recipe not found.[/]");
            Console.WriteLine();
        }
        return result;
    }

    // Testable version - takes recipe index as parameter
    public bool DeleteRecipe(int recipeIndex)
    {
        if (recipeIndex > 0 && recipeIndex <= RecipeList.Count)
        {
            RecipeList.RemoveAt(recipeIndex - 1);
            SaveRecipesToFile();
            return true;
        }
        return false;
    }

    // Helper method to save recipes to file
    private void SaveRecipesToFile()
    {
        File.WriteAllLines(filePath, RecipeList.Select(x => $"{x.Name},{string.Join(",", x.Ingredients.SelectMany(i => new[] { i.name, i.quantity }))},{x.Instructions}"));
    }

    // Helper method to find a recipe by name
    public Recipe? FindByName(string name)
    {
        for (int i = 0; i < RecipeList.Count; i++)
        {
            if (RecipeList[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return RecipeList[i];
            }
        }
        return null;
    }

    public Recipe? FindByTerm(string term)
    {
        foreach (var recipe in RecipeList)
        {
            if (recipe.Name.Contains(term, StringComparison.OrdinalIgnoreCase) || 
                recipe.Instructions.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                recipe.Ingredients.Any(i => i.name.Contains(term, StringComparison.OrdinalIgnoreCase)))
            {
                return recipe;
            }
        }
        return null;
    }
}