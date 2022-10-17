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

            

            bool writeAnother = true;
            while (writeAnother == true)
            {

                Console.WriteLine("Please enter the email address of the person you want to email:");
                string recipient = Console.ReadLine();

                Console.WriteLine("Please enter your email address:");
                string sender = Console.ReadLine();

                Console.WriteLine("Please enter the email's subject");
                string subject = Console.ReadLine();

                Console.WriteLine("Please enter the message you would like to send");
                string body = Console.ReadLine();

                Console.WriteLine("Sending your email!");




                // This calls the Email instance's SendEmail() method asynchronously
                _ = emailSVC.SendEmail(sender, recipient, subject, body);

                Console.WriteLine("Would you like to write another email? Y/N");
                string response = Console.ReadLine();
                if (response != "Y" && response != "N")
                {
                    Console.WriteLine("Please enter only Y or N");
                }
                else if(response == "N")
                {
                    writeAnother = false;
                }

            }








            Console.ReadLine();
        }


        //Added NuGet package:   Microsoft.Extensions.Hosting
        //Added NuGet package:   Microsoft.EntityFrameworkCore.SqlServer
        //Added NuGet package:   Microsoft.EntityFrameworkCore.Design
        //Added NuGet package:   Microsoft.EntityFrameworkCore
        //Added NuGet package:   


    }


}