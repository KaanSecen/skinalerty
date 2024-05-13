using BLL.Interfaces;
using BLL.Models;
using MySql.Data.MySqlClient;

namespace DAL.Services;

public class UserService : IUserService
{
    public User GetUser(int id)
    {
        var parameters = new[]
        {
            new MySqlParameter("@id", id)
        };
        var data = Logic.ExecuteQuery("SELECT * FROM skinalerty_users WHERE id = @id", parameters);
        if (data.Count == 0) return null!;

        var user = new User(
            (string)data[0]["name"],
            (string)data[0]["email"],
            (string)data[0]["password"],
            (int)data[0]["id"]
        );
        return user;
    }

    public User GetUser(string email)
    {
        var parameters = new[]
        {
            new MySqlParameter("@email", email)
        };
        var data = Logic.ExecuteQuery("SELECT * FROM skinalerty_users WHERE email = @email", parameters);
        if (data.Count == 0) return null!;

        var user = new User(
            (string)data[0]["name"],
            (string)data[0]["email"],
            (string)data[0]["password"],
            (int)data[0]["id"]
        );

        return user;
    }

    public User SaveUser(User user)
    {
        var parameters = new[]
        {
            new MySqlParameter("@name", user.Name),
            new MySqlParameter("@email", user.Email),
            new MySqlParameter("@password", user.Password)
        };

        var data = Logic.ExecuteQuery("INSERT INTO skinalerty_users (name, email, password) VALUES (@name, @email, @password)", parameters);
        if (data.Count == 0) return null!;

        user = new User(
            user.Name,
            user.Email,
            user.Password,
            (int)data[0]["LastInsertedId"]
        );
        return user;
    }

    public User Login(string email, string password)
    {
        var parameters = new[]
        {
            new MySqlParameter("@email", email),
            new MySqlParameter("@password", password)
        };
        var data = Logic.ExecuteQuery("SELECT * FROM skinalerty_users WHERE email = @email AND password = @password", parameters);
        if (data.Count == 0) return null!;

        var user = new User(
            (string)data[0]["name"],
            (string)data[0]["email"],
            (string)data[0]["password"],
            (int)data[0]["id"]
        );

        return user;
    }
}