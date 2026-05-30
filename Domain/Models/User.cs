namespace Domain.Models;

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

    public int id  { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public DateOnly creationDate { get; set; }
}