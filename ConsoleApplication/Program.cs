using System.Timers;
using BLL.Controllers;
using BLL.Models;
using DAL.Services;
using Timer = System.Timers.Timer;

namespace skinalerty_console_app;

internal class Program
{
    private static void Main()
    {
        new Program().Test().GetAwaiter().GetResult();
    }

    private async Task Test()
    {
        var timer = new Timer(1000);

        timer.Elapsed += TickFunction;

        timer.Start();

        await Task.Delay(-1);

        // var userController = new UserLogic(new UserService());
        //
        // // var user = new User("Kaan Secen", "kaansecen07@gmail.com", "sample12345");
        // // //
        // // var test = userController.SaveUser(user);
        // //
        // // Console.WriteLine(test.Message);
        //
        // var test2 = userController.Login(Console.ReadLine(), Console.ReadLine());
        //
        // Console.WriteLine(test2.Message);
        //
        //
        //
        // // Console.WriteLine("User created with id: " + user.Id);
        // //
        // // Console.WriteLine("User name: " + user.Name);
        // // Console.WriteLine("User email: " + user.Email);
        // // Console.WriteLine("User password: " + user.Password);
        //
        // // var result = userController.CheckIfUserEmailExists(user.Email);
        // //
        // // Console.WriteLine(result.Message);

    }
    void TickFunction(object? sender, ElapsedEventArgs args)
    {
        Console.WriteLine("Test");
    }
}