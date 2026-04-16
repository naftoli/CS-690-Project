namespace Project;

using Spectre.Console;

public class MenuMaker
{
    public static string MenuHelper(string[] options)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option:")
                .AddChoices(options));
        return selection;
    }

    // Testable version that accepts a pre-selected choice
    public static string MenuHelper(string[] options, string selectedChoice)
    {
        // Validate that the selected choice is in the options
        if (Array.Exists(options, option => option == selectedChoice))
        {
            return selectedChoice;
        }
        throw new ArgumentException("Selected choice is not in the available options");
    }

    public static void MainMenu()
    {
        MainMenuLogic();
    }

    // Testable version that accepts a choice selector function
    public static void MainMenu(Func<string[], string> choiceSelector)
    {
        MainMenuLogic(choiceSelector);
    }

    private static void MainMenuLogic(Func<string[], string>? choiceSelector = null)
    {
        while (true)
        {
            AnsiConsole.MarkupLine("[bold cyan]Welcome to the Main Menu![/]");
            var options = new string[] { "Manage Ingredients", "Manage Recipes", "Find Recipe", "Find Missing Ingredients for Recipe", "Logout" };

            string choice;
            if (choiceSelector != null)
            {
                choice = choiceSelector(options);
            }
            else
            {
                choice = MenuHelper(options);
            }

            switch (choice)
            {
                case "Manage Ingredients":
                    Console.WriteLine();
                    Ingredients ing = new Ingredients();
                    ing.ManageIngredients();
                    break;
                case "Manage Recipes":
                    Console.WriteLine();
                    Recipes rec = new Recipes();
                    rec.ManageRecipes();
                    break;
                case "Find Recipe":
                    Console.WriteLine();
                    RecipeFinder rf = new RecipeFinder();
                    rf.Find();
                    break;
                case "Find Missing Ingredients for Recipe":
                    Console.WriteLine();
                    MissingIngredientsFinder finder = new MissingIngredientsFinder();
                    finder.Find();
                    break;
                case "Logout":
                    Console.WriteLine();
                    Console.WriteLine("Logging out...");
                    return;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid option. Please try again.[/]");
                    break;
            }
        }
    }
}