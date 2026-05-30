namespace Domain.Models;

public class Account
{
    public Account(int id, string name, decimal balance, AccountType type, int userId)
    {
        this.id = id;
        this.name = name;
        this.balance = balance;
        this.type = type;
        this.userId = userId;
    }

    public Account(string name, decimal balance, AccountType type, int userId)
    {
        this.name = name;
        this.balance = balance;
        this.type = type;
        this.userId = userId;
    }

    public int id { get; set; }
    public string name { get; set; }
    public decimal balance { get; set; }
    public AccountType type { get; set; }
    public int userId { get; set; }
}