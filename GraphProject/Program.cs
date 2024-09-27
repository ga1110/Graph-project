using System;
using Handlers;

public class Program
{
    public static void Main(string[] args)
    {
        // Создаем объект для управления меню
        var menuHandler = new MenuHandler();
        menuHandler.Start();

    }
}
