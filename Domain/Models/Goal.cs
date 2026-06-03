using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table("goal")]
public class Goal
{
    public Goal(int id, string title, decimal totalAmmount, decimal currentAmmount, int userId)
    {
        this.id = id;
        this.title = title;
        this.totalAmmount = totalAmmount;
        this.currentAmmount = currentAmmount;
        this.userId = userId;
    }

    public Goal(string title, decimal totalAmmount, decimal currentAmmount, int userId)
    {
        this.title = title;
        this.totalAmmount = totalAmmount;
        this.currentAmmount = currentAmmount;
        this.userId = userId;
    }

    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("title")]
    public string title { get; set; }

    [Column("totalAmmount")]
    public decimal totalAmmount { get; set; }

    [Column("currentAmmount")]
    public decimal currentAmmount { get; set; }

    // FK → User
    [Column("userId")]
    public int userId { get; set; }

    [ForeignKey(nameof(userId))]
    public User user { get; set; } = null!;

    // N:N → Transaction (via tabela ponte TransactionGoal)
    public virtual ICollection<TransactionGoal> transactionGoals { get; set; } = new List<TransactionGoal>();
}