using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table("category")]
public class Category
{
    public Category(string name, TransactionType type, int userId)
    {
        this.name = name;
        this.type = type;
        this.userId = userId;
    }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    [Column("name")]
    public string name { get; set; }

    [Column("type")]
    public TransactionType type { get; set; }

    [Column("userId")]
    public int userId { get; set; }

    [ForeignKey(nameof(userId))]
    public User user { get; set; } = null!;

    public virtual ICollection<Transaction> transactions { get; set; } = new List<Transaction>();
}
