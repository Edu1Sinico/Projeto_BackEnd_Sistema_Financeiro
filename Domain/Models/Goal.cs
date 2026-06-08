using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table("goal")]
public class Goal
{
    public Goal(string title, decimal totalAmount, decimal currentAmount, int userId, DateOnly? deadline = null)
    {
        this.title = title;
        this.totalAmount = totalAmount;
        this.currentAmount = currentAmount;
        this.userId = userId;
        this.deadline = deadline;
        this.completed = currentAmount >= totalAmount;
    }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    [Column("title")]
    public string title { get; set; }

    [Column("totalAmount")]
    public decimal totalAmount { get; set; }

    [Column("currentAmount")]
    public decimal currentAmount { get; set; }

    [Column("deadline")]
    public DateOnly? deadline { get; set; }

    [Column("completed")]
    public bool completed { get; set; }

    [NotMapped]
    public decimal progressPercentage => totalAmount <= 0 ? 0 : Math.Min(100, currentAmount / totalAmount * 100);

    [Column("userId")]
    public int userId { get; set; }

    [ForeignKey(nameof(userId))]
    public User user { get; set; } = null!;

    // N:N → Transaction (via tabela ponte TransactionGoal)
    public virtual ICollection<TransactionGoal> transactionGoals { get; set; } = new List<TransactionGoal>();
}
