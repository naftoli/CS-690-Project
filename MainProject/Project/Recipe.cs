namespace Project;

using System.Collections.Generic;
using Spectre.Console;

public class Recipe
{
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public string Instructions { get; set; }

    public Recipe(string name, List<Ingredient> ingredients, string instructions)
    {
        Name = name;
        Ingredients = ingredients;
        Instructions = instructions;
    }

    public override string ToString()
    {
        string ingredientList = string.Join(", ", Ingredients.Select(i => i.ToString()));
        return $"[yellow]Name:[/] {Name},\n[yellow]Ingredients:[/] {ingredientList},\n[yellow]Instructions:[/] {Instructions}";
    }
}