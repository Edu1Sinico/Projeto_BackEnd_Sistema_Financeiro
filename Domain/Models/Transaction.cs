using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table("transaction")]
public class Transaction
{
    public Transaction(string description, decimal amount, TransactionType type, DateOnly transactionDate, int accountId, int categoryId)
    {
        this.description = description;
        this.amount = amount;
        this.type = type;
        this.transactionDate = transactionDate;
        this.accountId = accountId;
        this.categoryId = categoryId;
        
    }


    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    [Column("description")]
    public string description { get; set; }

    [Column("amount")]
    public decimal amount { get; set; }

    [Column("type")]
    public TransactionType type { get; set; }

    [Column("transactionDate")]
    public DateOnly transactionDate { get; set; }

    [Column("accountId")]
    public int accountId { get; set; }

    [ForeignKey(nameof(accountId))]
    public Account account { get; set; } = null!;

    [Column("categoryId")]
    public int categoryId { get; set; }

    [ForeignKey(nameof(categoryId))]
    public Category category { get; set; } = null!;
    // N:N → Goal (via tabela ponte TransactionGoal)
    public virtual ICollection<TransactionGoal> transactionGoals { get; set; } = new List<TransactionGoal>();
}