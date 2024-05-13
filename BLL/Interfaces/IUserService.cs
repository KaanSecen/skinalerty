using BLL.Models;

namespace BLL.Interfaces;

public interface IUserService
{
    User GetUser(int id);
    User? GetUser(string email);

    User SaveUser(User user);
    User Login(string email, string password);
}