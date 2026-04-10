namespace Project;

using System;
using System.IO;
using System.Collections.Generic;
using Spectre.Console;

public class Login
{
    private string filePath;

    public string getFilePath() {
        return filePath;
    }

    public Login()
    {
        filePath = "users.txt";
        EnsureFileExists(filePath);
    }

    public Login(string customFilePath)
    {
        filePath = customFilePath;
        EnsureFileExists(filePath);
    }

    private void EnsureFileExists(string path)
    {
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }
    }

    // Check if user already exists
    public bool UserExists(string username)
    {
        if (!File.Exists(filePath))
            return false;
            
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2 && parts[0] == username)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Get all users (for testing)
    public List<(string username, string password)> GetAllUsers()
    {
        var users = new List<(string, string)>();
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        users.Add((parts[0], parts[1]));
                    }
                }
            }
        }
        return users;
    }

    public bool RegisterUser(string username, string password)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        // Check if user already exists
        if (UserExists(username))
        {
            return false;
        }

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"{username},{password}");
        }
        return true;
    }

    // Create an interactive wrapper method
    public bool Register()
    {
        var username = AnsiConsole.Ask<string>("Enter your username:");
        var password = AnsiConsole.Ask<string>("Enter your password:");
        
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            AnsiConsole.MarkupLine("[red]Username and password cannot be empty.[/]");
            Console.WriteLine();
            return false;
        }

        if (UserExists(username))
        {
            AnsiConsole.MarkupLine("[red]Username already exists. Please choose a different one.[/]");
            Console.WriteLine();
            return false;
        }

        var registered = RegisterUser(username, password);
        if (registered) {
            AnsiConsole.MarkupLine("[green]Registration successful![/]");
            Console.WriteLine();
            return true;
        } else {
            AnsiConsole.MarkupLine("[red]Registration failed. Please try again.[/]");
            Console.WriteLine();
            return false;
        }
    }

    public bool AuthenticateUser(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        if (!File.Exists(filePath))
        {
            return false;
        }

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2 && parts[0] == username && parts[1] == password)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool Authenticate()
    {
        var username = AnsiConsole.Ask<string>("Enter your username:");
        var password = AnsiConsole.Ask<string>("Enter your password:");
        
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            AnsiConsole.MarkupLine("[red]Username and password cannot be empty.[/]");
            Console.WriteLine();
            return false;
        }

        var authenticated = AuthenticateUser(username, password);
        if (authenticated) {
            AnsiConsole.MarkupLine("[green]Login successful![/]");
            Console.WriteLine();
            return true;
        } else {
            AnsiConsole.MarkupLine("[red]Invalid username or password.[/]");
            AnsiConsole.MarkupLine("[yellow]Please register first.[/]");
            Console.WriteLine();
            return false;
        }
    }
}