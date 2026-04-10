namespace Project;

public class Ingredient
{
    public string name { get; set; }
    public string quantity { get; set; }

    public Ingredient(string name, string quantity)
    {
        this.name = name;
        this.quantity = quantity;
    }

    public override string ToString()
    {
        return $"[yellow]Name:[/] {name}, [yellow]Quantity:[/] {quantity}";
    }
}