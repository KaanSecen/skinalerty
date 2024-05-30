namespace Domain.Entities;

public class User(int id, string name, string email, string password)
{
    public int Id { get; protected set; } = id;

    public string Name { get; protected set; } = name;

    public string Email { get; protected set; } = email;

    public string Password { get; protected set; } = password;
}