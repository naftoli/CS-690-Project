namespace Project;

using System;
using Spectre.Console;

public class Program
{
    static void Main(string[] args)
    {
        RunApplication();
    }

    // Testable version that accepts a choice selector function
    public static void RunApplication(Func<string[], string>? choiceSelector = null)
    {
        while (true)
        {
            AnsiConsole.MarkupLine("[bold cyan]Welcome to the Recipe Manager![/]");
            string[] options = new string[] { "Login", "Register", "Exit" };

            string selection;
            if (choiceSelector != null)
            {
                selection = choiceSelector(options);
            }
            else
            {
                selection = MenuMaker.MenuHelper(options);
            }
            Console.WriteLine();

            switch (selection)
            {
                case "Login":
                    Console.WriteLine();
                    Login l = new Login();
                    if (l.Authenticate())
                    {
                        MenuMaker.MainMenu();
                    }
                    break;
                case "Register":
                    Console.WriteLine();
                    Login r = new Login();
                    r.Register();
                    break;
                case "Exit":
                    AnsiConsole.MarkupLine("[yellow]Exiting the application...[/]");
                    AnsiConsole.MarkupLine("[cyan]Bye![/]");
                    Console.WriteLine();
                    return;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid option. Please try again.[/]");
                    break;
            }
        }
    }
}
