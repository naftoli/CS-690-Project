namespace Project;
using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine();
            // Console.WriteLine("Welcome to the Recipe Manager!");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine();
                    Login();
                    break;
                case "2":
                    Console.WriteLine();
                    Register();
                    break;
                case "3":
                    Console.WriteLine("Bye!");
                    Console.WriteLine();
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void Login()
    {
        try
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (File.Exists("users.txt"))
            {
                string[] users = File.ReadAllLines("users.txt");
                foreach (string user in users)
                {
                    string[] credentials = user.Split(',');
                    if (credentials[0] == username && credentials[1] == password)
                    {
                        Console.WriteLine("Login successful!");
                        Console.WriteLine();
                        MainMenu();

                        return;
                    }
                }
            }
            Console.WriteLine("Invalid username or password.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void Register()
    {
        try
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            File.AppendAllText("users.txt", $"{username},{password}" + Environment.NewLine);
            Console.WriteLine("Registration successful!");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void MainMenu() 
    {
        while (true)
        {
            Console.WriteLine("Welcome to the Main Menu!");
            // Additional functionality can be added here
            // choose from following options:
            // 1. Manage Ingredients
            // 2. Manage Recipes
            // 3. Find Recipe
            // 4. Logout
            Console.WriteLine("1. Manage Ingredients");
            Console.WriteLine("2. Manage Recipes");
            Console.WriteLine("3. Find Recipe");
            Console.WriteLine("4. Logout");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    Console.WriteLine();
                    ManageIngredients();
                    break;
                case "2":
                    Console.WriteLine();
                    Console.WriteLine("Manage Recipes coming soon..." + Environment.NewLine);
                    // Implement recipe management functionality here
                    break;
                case "3":
                    Console.WriteLine();
                    Console.WriteLine("Find Recipe coming soon..." + Environment.NewLine);
                    // Implement recipe finding functionality here
                    break;
                case "4":
                    Console.WriteLine();
                    Console.WriteLine("Logging out...");
                    return;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Invalid option. Please try again." + Environment.NewLine);
                    break;
            }
        }
    }

    static void ManageIngredients()
    {
        // add / list ingredients
        while (true)        
        {
            Console.WriteLine("Manage Ingredients:");
            Console.WriteLine("1. Add Ingredient");
            Console.WriteLine("2. Edit Ingredient");
            Console.WriteLine("3. Delete Ingredient");
            Console.WriteLine("4. List Ingredients");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            
            switch (choice)            
            {
                case "1":
                    // add ingredient and qty
                    Console.WriteLine();
                    Console.WriteLine("Ingredient Name: ");
                    string ingredientName = Console.ReadLine();
                    Console.WriteLine("Ingredient Quantity: ");
                    string ingredientQty = Console.ReadLine();
                    // save ingredient to file 
                    File.AppendAllText("ingredients.txt", $"{ingredientName},{ingredientQty}" + Environment.NewLine);
                    Console.WriteLine("Ingredient added successfully!" + Environment.NewLine);
                    break;
                case "2":
                    Console.WriteLine();
                    Console.WriteLine("Choose from the list of ingredients to edit:");
                    if (File.Exists("ingredients.txt"))                    
                    {
                        string[] ingredients = File.ReadAllLines("ingredients.txt");
                        if (ingredients.Length == 0)                        
                        {
                            Console.WriteLine("No ingredients found." + Environment.NewLine);
                            break;
                        }
                        for (int i = 0; i < ingredients.Length; i++)                        
                        {
                            string[] details = ingredients[i].Split(',');
                            Console.WriteLine($"{i + 1}. Name: {details[0]}, Quantity: {details[1]}");
                        }
                        Console.Write("Enter the number of the ingredient to edit: ");
                        int editChoice = int.Parse(Console.ReadLine());
                        if (editChoice > 0 && editChoice <= ingredients.Length)                        
                        {
                            Console.Write("Enter new name: ");
                            string newName = Console.ReadLine();
                            Console.Write("Enter new quantity: ");
                            string newQty = Console.ReadLine();
                            ingredients[editChoice - 1] = $"{newName},{newQty}";
                            File.WriteAllLines("ingredients.txt", ingredients);
                            Console.WriteLine("Ingredient updated successfully!" + Environment.NewLine);
                        } else {
                            Console.WriteLine("Invalid choice." + Environment.NewLine);
                        }
                    } else {
                        Console.WriteLine("No ingredients found." + Environment.NewLine);
                    }
                    break;
                case "3":
                    Console.WriteLine();
                    Console.WriteLine("Choose from the list of ingredients to delete:");
                    if (File.Exists("ingredients.txt"))                    {
                        string[] ingredients = File.ReadAllLines("ingredients.txt");
                        if (ingredients.Length == 0)                        
                        {
                            Console.WriteLine("No ingredients found." + Environment.NewLine);
                            break;
                        }
                        for (int i = 0; i < ingredients.Length; i++)                        {
                            string[] details = ingredients[i].Split(',');
                            Console.WriteLine($"{i + 1}. Name: {details[0]}, Quantity: {details[1]}");
                        }
                        Console.Write("Enter the number of the ingredient to delete: ");
                        int deleteChoice = int.Parse(Console.ReadLine());
                        if (deleteChoice > 0 && deleteChoice <= ingredients.Length)                        
                        {
                            List<string> ingredientList = new List<string>(ingredients);
                            ingredientList.RemoveAt(deleteChoice - 1);
                            File.WriteAllLines("ingredients.txt", ingredientList);
                            Console.WriteLine("Ingredient deleted successfully!" + Environment.NewLine);
                        } else {
                            Console.WriteLine("Invalid choice." + Environment.NewLine);
                        }
                    } else {
                        Console.WriteLine("No ingredients found." + Environment.NewLine);
                    }
                    break;
                case "4":
                    Console.WriteLine();
                    Console.WriteLine("List of Ingredients:");
                    if (File.Exists("ingredients.txt"))                    
                    {
                        string[] ingredients = File.ReadAllLines("ingredients.txt");
                        if (ingredients.Length == 0)                        
                        {
                            Console.WriteLine("No ingredients found." + Environment.NewLine);
                            break;
                        }
                        foreach (string ingredient in ingredients)                        
                        {
                            string[] details = ingredient.Split(',');
                            Console.WriteLine($"Name: {details[0]}, Quantity: {details[1]}");
                        }
                    } else {
                        Console.WriteLine("No ingredients found." + Environment.NewLine);
                    }
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Invalid option. Please try again." + Environment.NewLine);
                    break;
            }
        }
    }
}
