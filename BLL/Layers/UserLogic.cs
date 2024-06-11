using BLL.Interfaces;
using BLL.Models;

namespace BLL.Layers;

public class UserLogic(IUserService userService)
{
    public User GetUser(int id)
    {
        return userService.GetUser(id);
    }

    public ValidationResult<User> SaveUser(User user)
    {
        ValidationResult<User> userValidationResult = new ValidationResult<User>
        {
            Result = [user]
        };

        if (!user.IsEmailFormatValid())
        {
            userValidationResult.IsSuccess = false;
            userValidationResult.Message = "Email format is not valid!";
            return userValidationResult;
        }

        if(user.IsPasswordLengthValid())
        {
            user.HashPassword();
            if (!CheckIfUserEmailExists(user.Email).IsSuccess)
            {
                userValidationResult.Result = [userService.SaveUser(user)];
                userValidationResult.IsSuccess = true;
                userValidationResult.Message = "User created successfully!";
                return userValidationResult;
            }
            userValidationResult.IsSuccess = false;
            userValidationResult.Message = "User already exists!";

            return userValidationResult;
        }

        userValidationResult.Message = "Password must be at least 8 characters long!";
        userValidationResult.IsSuccess = false;

        return userValidationResult;
    }

    public void SetUserService(IUserService userServices)
    {
        userService = userServices;
    }

    public ValidationResult<User> UpdateUser(User user)
    {
        ValidationResult<User> userValidationResult = new ValidationResult<User>
        {
            Result = [user]
        };

        if (user.IsPasswordLengthValid())
        {
            user.HashPassword();
            userService.UpdateUser(user);
            userValidationResult.IsSuccess = true;
            userValidationResult.Message = "User updated successfully!";
            return userValidationResult;
        }

        return userValidationResult;
    }

    public ValidationResult<User> Login(string email, string password)
    {
        ValidationResult<User> userValidationResult = new ValidationResult<User>();

        var user = CheckIfUserEmailExists(email);

        if (user.IsSuccess)
        {
            if (user.Result != null && user.Result.First().VerifyPassword(password))
            {
                userService.Login(email, password);
                userValidationResult.Message = "User logged in successfully!";
                userValidationResult.IsSuccess = true;
                userValidationResult.Result = user.Result;
                return userValidationResult;
            }

            userValidationResult.Message = "Password is incorrect!";
            userValidationResult.IsSuccess = false;
            return userValidationResult;
        }

        userValidationResult.Message = "User does not exist!";
        userValidationResult.IsSuccess = false;
        return userValidationResult;
    }

    public ValidationResult<User> CheckIfUserEmailExists(string email)
    {
        ValidationResult<User> userValidationResult = new ValidationResult<User>();
        var databaseUser = userService.GetUser(email);

        if (databaseUser != null)
        {
            userValidationResult.Message = "Email already exists!";
            userValidationResult.Result = [databaseUser];
            userValidationResult.IsSuccess =  true;
        }
        else
        {
            userValidationResult.Message = "Email does not exist!";
            userValidationResult.IsSuccess =  false;
        }

        return userValidationResult;
    }
}