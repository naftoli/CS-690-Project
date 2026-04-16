namespace Project;

using Spectre.Console;

public class MissingIngredientsFinder
{
    private List<Recipe> recipes;
    private List<Ingredient> ingredients;

    public MissingIngredientsFinder()
    {
        this.recipes = LoadRecipes();
        this.ingredients = LoadIngredients();
    }

    public MissingIngredientsFinder(string ingredientFile)
    {
        this.recipes = new List<Recipe>();
        this.ingredients = LoadIngredients(ingredientFile);
    }

    public void Find()
    {
        Console.WriteLine();
        // check recipes 
        if (this.recipes.Count == 0)
        {
            Console.WriteLine("No recipes available to find missing ingredients.");
            return;
        }

        // check ingredients
        if (this.ingredients.Count == 0)
        {
            Console.WriteLine("No ingredients available to find missing ingredients.");
            return;
        }

        Recipe recipe = GetRecipeSelection();  
        List<Ingredient> missingIngredients = FindMissingIngredients(recipe); 
        PrintMissingIngedients(missingIngredients);
    }

    public List<Ingredient> Find(Recipe recipe)
    {
        return FindMissingIngredients(recipe); 
    }

    public List<Recipe> LoadRecipes(string filePath = "recipes.txt") 
    {
        Recipes r = new Recipes(filePath);
        return r.GetRecipeList();
    }

    public List<Ingredient> LoadIngredients(string filePath = "ingredients.txt") 
    {
        Ingredients i = new Ingredients(filePath);
        return i.GetIngredientList();
    }

    public Recipe GetRecipeSelection()
    {
        AnsiConsole.MarkupLine("[bold cyan]Choose a recipe to edit:[/]");
        for (int i = 0; i < this.recipes.Count; i++)
        {
            AnsiConsole.MarkupLine($"[yellow]{i + 1}.[/] Name: {this.recipes[i].Name}, Instructions: {this.recipes[i].Instructions}");
        }
        var choice = AnsiConsole.Ask<int>("Enter the number of the recipe to edit:");
        return this.recipes[choice - 1];
    }

    public List<Ingredient> FindMissingIngredients(Recipe recipe)
    {
        List<Ingredient> missingIngredients = new List<Ingredient>();

        foreach (var ingredient in recipe.Ingredients)
        {
            if (!this.ingredients.Any(i => i.name.Equals(ingredient.name, StringComparison.OrdinalIgnoreCase)))
            {
                missingIngredients.Add(ingredient);
            }
        }

        return missingIngredients;
    }

    public void PrintMissingIngedients(List<Ingredient> missingIngredients)
    {
        if (missingIngredients.Count == 0)
        {
            AnsiConsole.MarkupLine("[green]You have all the ingredients for this recipe![/]");
            return;
        }

        AnsiConsole.MarkupLine("[bold red]Missing Ingredients:[/]");
        foreach (var ingredient in missingIngredients)
        {
            AnsiConsole.MarkupLine(ingredient.ToString()+"\n");
        }
    }
}