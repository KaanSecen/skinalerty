using BLL.Interfaces;
using BLL.Models;

namespace BLL.Controllers;

public class UserLogic
{
    private IUserService _userService;

    public UserLogic(IUserService userService)
    {
        _userService = userService;
    }

    public User GetUser(int id)
    {
        return _userService.GetUser(id);
    }

    public ValidationResult<User> SaveUser(User user)
    {
        ValidationResult<User> userValidationResult = new ValidationResult<User>
        {
            Result = user
        };

        if(user.IsPasswordLengthValid())
        {
            user.HashPassword();
            if (!CheckIfUserEmailExists(user.Email).IsSuccess)
            {
                _userService.SaveUser(user);
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
        _userService = userServices;
    }

    public ValidationResult<User> Login(string email, string password)
    {
        ValidationResult<User> userValidationResult = new ValidationResult<User>();

        var user = CheckIfUserEmailExists(email);

        if (user.IsSuccess)
        {
            if (user.Result != null && user.Result.VerifyPassword(password))
            {
                _userService.Login(email, password);
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
        var databaseUser = _userService.GetUser(email);

        if (databaseUser != null)
        {
            userValidationResult.Message = "Email already exists!";
            userValidationResult.Result = databaseUser;
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