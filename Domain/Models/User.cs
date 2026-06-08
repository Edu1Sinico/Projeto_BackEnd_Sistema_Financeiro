using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table("user")]
public class User
{
    public User(string name, string email, string password, DateOnly creationDate)
    {
        this.name = name;
        this.email = email;
        this.password = password;
        this.creationDate = creationDate;
    }

    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    [Column("name")]
    public string name { get; set; }

    [Column("email")]
    public string email { get; set; }

    [Column("password")]
    public string password { get; set; }

    [Column("creationDate")]
    public DateOnly creationDate { get; set; }

    public virtual ICollection<Account> accounts { get; set; } = new List<Account>();
    public virtual ICollection<Category> categories { get; set; } = new List<Category>();
    public virtual ICollection<Goal> goals { get; set; } = new List<Goal>();
}
