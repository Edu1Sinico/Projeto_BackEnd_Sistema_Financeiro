using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table("user")]
public class User
{
    public User(int id, string name, string email, string password, DateOnly creationDate)
    {
        this.id = id;
        this.name = name;
        this.email = email;
        this.password = password;
        this.creationDate = creationDate;
    }

    public User(string name, string email, string password, DateOnly creationDate)
    {
        this.name = name;
        this.email = email;
        this.password = password;
        this.creationDate = creationDate;
    }

    [Key]
    [Column("id")]
    public int id { get; set; }

    [Column("name")]
    public string name { get; set; }

    [Column("email")]
    public string email { get; set; }

    [Column("password")]
    public string password { get; set; }

    [Column("creationDate")]
    public DateOnly creationDate { get; set; }

    // 1:N — um usuário possui várias contas
    public virtual ICollection<Account> accounts { get; set; } = new List<Account>();

    // 1:N — um usuário possui várias metas
    public virtual ICollection<Goal> goals { get; set; } = new List<Goal>();
}