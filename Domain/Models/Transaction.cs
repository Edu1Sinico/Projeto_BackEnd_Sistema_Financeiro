using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table("transaction")]
public class Transaction
{
    public Transaction(int id, string description, decimal amount, TransactionType type, DateOnly transactionDate, int accountId, string category)
    {
        this.id = id;
        this.description = description;
        this.amount = amount;
        this.type = type;
        this.transactionDate = transactionDate;
        this.accountId = accountId;
        this.category = category;
    }

    public Transaction(string description, decimal amount, TransactionType type, DateOnly transactionDate, int accountId, string category)
    {
        this.description = description;
        this.amount = amount;
        this.type = type;
        this.transactionDate = transactionDate;
        this.accountId = accountId;
        this.category = category;
    }

    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("description")]
    public string description { get; set; }

    [Column("amount")]
    public decimal amount { get; set; }

    [Column("type")]
    public TransactionType type { get; set; }

    [Column("transactionDate")]
    public DateOnly transactionDate { get; set; }

    [Column("categoryId")]
    public string category { get; set; }

    // FK → Account
    [Column("accountId")]
    public int accountId { get; set; }

    [ForeignKey(nameof(accountId))]
    public Account account { get; set; } = null!;

    // N:N → Goal (via tabela ponte TransactionGoal)
    public virtual ICollection<TransactionGoal> transactionGoals { get; set; } = new List<TransactionGoal>();
}