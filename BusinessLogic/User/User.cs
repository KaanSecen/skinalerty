using BusinessLogic.Interfaces;

namespace BusinessLogic.User;

public class User
{
    public int Id;
    public string Name;
    public string Email;
    public string Password;

    private static IUser _userService = null!;

    public static void SetUserService(IUser userServices)
    {
        _userService = userServices;
    }

    public User GetUser(int id)
    {
        return _userService.GetUser(id);
    }
}