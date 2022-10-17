using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SendEmailLibrary;


namespace SendEmailConsole
{
    internal class Program
    {

        public static void Main(string[] args)
        {

            //CreateDefaultBuilder() Creates a host object which loads IConfiguration for appsettings.json
            //configureservices enables the use of dependency injection using the services that are declared within the method call
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IEmail, Email>();
                })
                .Build();

            // This creates an Email object instance using the interface from the service declared above
            var emailSVC = ActivatorUtilities.CreateInstance<Email>(host.Services);


            //bool writeAnother = true;
            //while (writeAnother == true){

            //    Console.WriteLine("Please enter the email address of the person you want to email:");
            //    string recipient = Console.ReadLine();

            //    Console.WriteLine("Please enter your email address:");
            //    string sender = Console.ReadLine();

            //    Console.WriteLine("Please enter the email's subject");
            //    string subject = Console.ReadLine();

            //    Console.WriteLine("Please enter the message you would like to send");
            //    string body = Console.ReadLine();



            //}



            // This calls the email instance's SendEmail() method asynchronously
            _ = emailSVC.SendEmail("ransomcadorette@gmail.com", "cadorette.test@gmail.com", "Test Subject", "Test Body");


            Console.WriteLine("sent");

            Console.ReadLine();
        }


        //Added NuGet package Microsoft.Extensions.Hosting


    }


}