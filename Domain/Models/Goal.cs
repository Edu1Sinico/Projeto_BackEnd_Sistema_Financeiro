namespace Domain.Models;

public class Goal
{
    public Goal(int id, string title, decimal totalAmmount, decimal currentAmmount)
    {
        this.id = id;
        this.title = title;
        this.totalAmmount = totalAmmount;
        this.currentAmmount = currentAmmount;
    }

    public Goal(string title, decimal totalAmmount, decimal currentAmmount)
    {
        this.title = title;
        this.totalAmmount = totalAmmount;
        this.currentAmmount = currentAmmount;
    }

    public int id { get; set; }
    public string title { get; set; }
    public decimal totalAmmount { get; set; }
    public decimal currentAmmount{ get; set; }
}