using BLL.Interfaces;
using BLL.Models;

namespace TestProject;

public class TestUserService : IUserService
{
    private readonly User _user = new User("Test", "sample@gmail.com", BCrypt.Net.BCrypt.HashPassword("sample123"));

    public User GetUser(int id)
    {
        return _user;
    }

    public User GetUser(string email)
    {
        return _user;
    }

    public User SaveUser(User user)
    {
        return _user;
    }

    public User Login(string email, string password)
    {
        return _user;
    }
}