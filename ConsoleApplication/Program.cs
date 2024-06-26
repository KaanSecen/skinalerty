﻿using System.Timers;
using BLL.Layers;
using BLL.Models;
using DAL.Services;
using SteamApi;
using Spectre.Console;
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
        // var timer = new Timer(1000);
        //
        // timer.Elapsed += TickFunction;
        //
        // timer.Start();
        //
        // await Task.Delay(-1);

        var userController = new UserLogic(new UserService());
        var notificationController = new NotificationLogic(new NotificationService());

        var user = new User(1, "Kaan Secen", "kaansecen03@gmail.com", "sample12345");

        // var test = userController.SaveUser(user);

        // Console.WriteLine(test.Message);

        // var test2 = userController.Login(Console.ReadLine(), Console.ReadLine());

        // Console.WriteLine(test2.Message);


        // Console.WriteLine("User created with id: " + user.Id);
        //
        // Console.WriteLine("User name: " + user.Name);
        // Console.WriteLine("User email: " + user.Email);
        // Console.WriteLine("User password: " + user.Password);
        //
        // var test3 = notificationController.GetAllNotificationsFromUser(77);
        //
        // var test4 = notificationController.SaveNotification(new Notification(2, 1, 1, 1, 1, true));
        //
        // var result = userController.CheckIfUserEmailExists(user.Email);
        //
        // Console.WriteLine(result.Message);

        var itemLogic = new ItemLogic(new ItemService());
        var steamService = new SteamHttpClientService(itemLogic);

        var savedItems = await steamService.FetchAndSaveItems();

        foreach (var itemSaved in savedItems)
        {
            AnsiConsole.Status()
                .Start(itemSaved.Message!, ctx =>
                {
                    // Simulate some work
                    AnsiConsole.MarkupLine("Doing some work...");
                    Thread.Sleep(1000);

                    // Update the status and spinner
                    ctx.Status(itemSaved.Message!);
                    ctx.Spinner(Spinner.Known.Star);
                    ctx.SpinnerStyle(Style.Parse("green"));

                    // Simulate some work
                    AnsiConsole.MarkupLine("Doing some more work...");
                    Thread.Sleep(2000);
                });
        }

    }
    void TickFunction(object? sender, ElapsedEventArgs args)
    {
        Console.WriteLine("Test");
    }
}