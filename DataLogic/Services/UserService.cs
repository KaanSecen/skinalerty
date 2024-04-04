using BusinessLogic.User;
using MySql.Data.MySqlClient;

namespace DataLogic.Services;

public class UserService : BusinessLogic.Interfaces.IUser
{
    public User GetUser(int id)
    {
        var parameters = new[]
        {
            new MySqlParameter("@id", id),
        };
        var data = Logic.ExecuteQuery("SELECT * FROM skinalerty_users WHERE id = @id", parameters);
        User user = new User()
        {
            Id = (int)data[0]["id"],
            Name = (string)data[0]["name"],
            Email = (string)data[0]["email"],
            Password = (string)data[0]["password"]
        };
        return user;
    }
}