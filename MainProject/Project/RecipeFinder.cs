namespace Project;

using System;
using Spectre.Console;

public class RecipeFinder
{
    private Recipes r;

    public RecipeFinder()
    {
        // make sure the recipes file exists and is loaded
        this.r = new Recipes(); 
    }

    public RecipeFinder(Recipes recipes)
    {
        this.r = recipes;
    }
    
    public Recipe? Find()
    {
        var name = AnsiConsole.Ask<string>("Enter the name of the recipe:");
        return Find(name);
    }

    public Recipe? Find(string name)
    {
        var recipe = r.FindRecipeByName(name);
        if (recipe != null)
        {
            AnsiConsole.MarkupLine($"[bold cyan]Recipe Found![/]\n");
            return recipe;
        }
        else
        {
            AnsiConsole.MarkupLine($"[yellow]No recipe found with the name '{name}'.[/]\n");
            return null;
        }
    }
}