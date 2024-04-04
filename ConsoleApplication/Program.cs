using BusinessLogic.User;
using DataLogic.Services;

namespace skinalerty_console_app;

class Program
{
    static void Main(string[] args)
    {
        var userService = new UserService();
        User.SetUserService(userService);

        // Get user by id
        var user = new User();

        var user1 = user.GetUser(1);

        Console.WriteLine(user1.Email);

    }
}