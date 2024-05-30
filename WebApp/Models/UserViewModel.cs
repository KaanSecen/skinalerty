using BLL.Models;

namespace WebApp.Models;

public class UserViewModel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public static User ConvertToUser(UserViewModel userViewModel)
    {
        return new User (
            userViewModel.Id,
            userViewModel.Name,
            userViewModel.Email,
            userViewModel.Password
        );
    }

    public static UserViewModel ConvertToView(User user)
    {
        return new UserViewModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };
    }
}