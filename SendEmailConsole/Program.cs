using System;
using SendEmailLibrary;

namespace SendEmailConsole
{
    internal class Program
    {

        static void Main(string[] args)
        {

            _ = Email.SendEmail("david.r.cadorette@gmail.com", "cadorette.test@gmail.com", "Test Subject", "Test Body");

            Console.WriteLine("sent");

            Console.ReadLine();
        }


    }
}