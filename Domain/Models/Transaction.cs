namespace Domain.Models;

public class Transaction
{
    public Transaction(int id, string description, decimal ammount, TransactionType type, DateOnly transactionDate, int accountId, string category)
    {
        this.id = id;
        this.description = description;
        this.ammount = ammount;
        this.type = type;
        this.transactionDate = transactionDate;
        this.accountId = accountId;
        this.category = category;
    }

    public Transaction(string description, decimal ammount, TransactionType type, DateOnly transactionDate, int accountId, string category)
    {
        this.description = description;
        this.ammount = ammount;
        this.type = type;
        this.transactionDate = transactionDate;
        this.accountId = accountId;
        this.category = category;
    }

    public int id { get; set; }
    public string description{ get; set; }
    public decimal ammount{ get; set; }
    public TransactionType type { get; set; }
    public DateOnly transactionDate{ get; set; }
    public int accountId{ get; set; }
    public string category{ get; set; }
}