using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table("account")]
public class Account
{
    public Account(string name, decimal balance, AccountType type, int userId)
    {
        this.name = name;
        this.balance = balance;
        this.type = type;
        this.userId = userId;
    }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    [Column("name")]
    public string name { get; set; }

    [Column("balance")]
    public decimal balance { get; set; }

    [Column("type")]
    public AccountType type { get; set; }

    // FK → User
    [Column("userId")]
    public int userId { get; set; }

    [ForeignKey(nameof(userId))]
    public User user { get; set; } = null!;

    public virtual ICollection<Transaction> transactions { get; set; } = new List<Transaction>();
}
