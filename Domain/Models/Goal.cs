using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table("goal")]
public class Goal
{
    public Goal(int id, string title, decimal totalAmount, decimal currentAmount, int userId)
    {
        this.id = id;
        this.title = title;
        this.totalAmount = totalAmount;
        this.currentAmount = currentAmount;
        this.userId = userId;
    }

    public Goal(string title, decimal totalAmount, decimal currentAmount, int userId)
    {
        this.title = title;
        this.totalAmount = totalAmount;
        this.currentAmount = currentAmount;
        this.userId = userId;
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

    // FK → User
    [Column("userId")]
    public int userId { get; set; }

    [ForeignKey(nameof(userId))]
    public User user { get; set; } = null!;

    // N:N → Transaction (via tabela ponte TransactionGoal)
    public virtual ICollection<TransactionGoal> transactionGoals { get; set; } = new List<TransactionGoal>();
}