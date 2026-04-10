namespace Project;

using System;
using System.IO;
using System.Collections.Generic;
using Spectre.Console;

public class Ingredients
{
    string filePath;
    List<Ingredient> IngredientList = new List<Ingredient>();

    public Ingredients()
    {
        filePath = "ingredients.txt";
        LoadIngredientsFromFile(filePath);
    }

    public Ingredients(string customFilePath)
    {
        filePath = customFilePath;
        LoadIngredientsFromFile(filePath);
    }

    private void LoadIngredientsFromFile(string path)
    {
        // load ingredients from file
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
                        Ingredient i = new Ingredient(details[0], details[1]);
                        IngredientList.Add(i);
                    }
                    // Skip invalid lines
                }
            }
        } else {
            File.Create(path).Close();
        }
    }

    // Getter for testing purposes
    public List<Ingredient> GetIngredientList()
    {
        return IngredientList;
    }

    // Setter for testing purposes
    public void SetIngredientList(List<Ingredient> ingredients)
    {
        IngredientList = ingredients;
    }

    public bool ManageIngredients()
    {
        while (true)
        {
            AnsiConsole.MarkupLine("[bold cyan]Manage Ingredients:[/]");
            string selection = MenuMaker.MenuHelper(new string[] { "Add Ingredient", "List Ingredients", "Edit Ingredient", "Delete Ingredient", "Back to Main Menu" });
            switch (selection)
            {
                case "Add Ingredient":
                    AddIngredientUI();
                    break;
                case "List Ingredients":
                    ListIngredientsUI();
                    break;
                case "Edit Ingredient":
                    EditIngredientUI();
                    break;
                case "Delete Ingredient":
                    DeleteIngredientUI();
                    break;
                case "Back to Main Menu":
                    return true;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid option. Please try again.[/]");
                    break;
            }
        }
    }

    // UI wrapper for AddIngredient - prompts user for input
    public bool AddIngredientUI()
    {
        var name = AnsiConsole.Ask<string>("Enter the ingredient name:");
        var quantity = AnsiConsole.Ask<string>("Enter the ingredient quantity:");
        bool result = AddIngredient(name, quantity);
        if (result)
        {
            AnsiConsole.MarkupLine("[green]Ingredient added successfully![/]");
            Console.WriteLine();
        }
        return result;
    }

    // Testable version - business logic without console input
    public bool AddIngredient(string name, string quantity)
    {
        Ingredient i = new Ingredient(name, quantity);
        IngredientList.Add(i);
        SaveIngredientsToFile();
        return true;
    }

    // UI wrapper for ListIngredients
    public void ListIngredientsUI()
    {
        ListIngredients(IngredientList);
        Console.WriteLine();
    }

    // Testable version - returns formatted list without console output
    public void ListIngredients()
    {
        ListIngredients(IngredientList);
    }

    // Core logic version that works with any list
    public void ListIngredients(List<Ingredient> ingredients)
    {
        if (ingredients.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No ingredients found.[/]");
            return;
        }

        AnsiConsole.MarkupLine("[bold cyan]Ingredients List:[/]");
        foreach (var ingredient in ingredients)
        {
            AnsiConsole.MarkupLine(ingredient.ToString());
        }
    }

    // UI wrapper for EditIngredient - prompts user for selection and input
    public bool EditIngredientUI()
    {
        if (IngredientList.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No ingredients to edit.[/]");
            Console.WriteLine();
            return false;
        }

        AnsiConsole.MarkupLine("[bold cyan]Choose an ingredient to edit:[/]");
        for (int i = 0; i < IngredientList.Count; i++)
        {
            AnsiConsole.MarkupLine(IngredientList[i].ToString());
        }

        int choice = AnsiConsole.Ask<int>("Enter the number of the ingredient to edit:");
        var newName = AnsiConsole.Ask<string>("Enter new name:");
        var newQty = AnsiConsole.Ask<string>("Enter new quantity:");
        
        bool result = EditIngredient(choice, newName, newQty);
        if (result)
        {
            AnsiConsole.MarkupLine("[green]Ingredient updated successfully![/]");
            Console.WriteLine();
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Invalid choice. Please try again.[/]");
            Console.WriteLine();
        }
        return result;
    }

    // Testable version - takes ingredient index and new values as parameters
    public bool EditIngredient(int ingredientIndex, string newName, string newQuantity)
    {
        if (ingredientIndex > 0 && ingredientIndex <= IngredientList.Count)
        {
            IngredientList[ingredientIndex - 1] = new Ingredient(newName, newQuantity);
            SaveIngredientsToFile();
            return true;
        }
        return false;
    }

    // UI wrapper for DeleteIngredient - prompts user for selection
    public bool DeleteIngredientUI()
    {
        if (IngredientList.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No ingredients to delete.[/]");
            Console.WriteLine();
            return false;
        }

        AnsiConsole.MarkupLine("[bold cyan]Choose an ingredient to delete:[/]");
        for (int i = 0; i < IngredientList.Count; i++)
        {
            AnsiConsole.MarkupLine($"[yellow]{i + 1}.[/] {IngredientList[i].ToString()}");
        }

        int choice = AnsiConsole.Ask<int>("Enter the number of the ingredient to delete:");
        bool result = DeleteIngredient(choice);
        if (result)
        {
            AnsiConsole.MarkupLine("[green]Ingredient deleted successfully![/]");
            Console.WriteLine();
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Invalid choice. Please try again.[/]");
            Console.WriteLine();
        }
        return result;
    }

    // Testable version - takes ingredient index as parameter
    public bool DeleteIngredient(int ingredientIndex)
    {
        if (ingredientIndex > 0 && ingredientIndex <= IngredientList.Count)
        {
            IngredientList.RemoveAt(ingredientIndex - 1);
            SaveIngredientsToFile();
            return true;
        }
        return false;
    }

    // Helper method to save ingredients to file
    private void SaveIngredientsToFile()
    {
        File.WriteAllLines(filePath, IngredientList.Select(x => $"{x.name},{x.quantity}"));
    }
}