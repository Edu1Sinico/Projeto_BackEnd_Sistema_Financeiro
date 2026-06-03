using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table("transaction_goal")]
public class TransactionGoal
{
    public TransactionGoal(int transactionId, int goalId, decimal contributedAmount)
    {
        this.transactionId = transactionId;
        this.goalId = goalId;
        this.contributedAmount = contributedAmount;
    }

    // FK → Transaction (parte da PK composta)
    [Key, Column("transactionId", Order = 0)]
    public int transactionId { get; set; }

    [ForeignKey(nameof(transactionId))]
    public Transaction transaction { get; set; } = null!;

    // FK → Goal (parte da PK composta)
    [Key, Column("goalId", Order = 1)]
    public int goalId { get; set; }

    [ForeignKey(nameof(goalId))]
    public Goal goal { get; set; } = null!;

    
    [Column("contributedAmount")]
    public decimal contributedAmount { get; set; }
}