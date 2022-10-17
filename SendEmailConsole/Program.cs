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



            //User Interface for sending emails
            bool writeAnother = true;
            while (writeAnother == true)
            {

                Console.WriteLine("Please enter the email address of the person you want to email:");
                string recipient = Console.ReadLine();

                Console.WriteLine("Please enter your email address:");
                string sender = Console.ReadLine();

                Console.WriteLine("Please enter the email's subject");
                string subject = Console.ReadLine();
                if (subject == "") { subject = "<No Subject>"; }

                Console.WriteLine("Please enter the message you would like to send");
                string body = Console.ReadLine();
                if (body == "") { body = "<No body>"; }

                Console.WriteLine("Sending your email!");

                for(int i = 1; i < 3; i++)
                {
                    try
                    {
                        // This calls the Email instance's SendEmail() method asynchronously
                        _ = emailSVC.SendEmail(sender, recipient, subject, body);
                        _ = writeToDB(sender, recipient, subject, body, true);
                        break;
                    }
                    catch (Exception ex)
                    {

                        if(i == 3)
                        {
                            _ = writeToDB(sender, recipient, subject, body, false);
                            Console.WriteLine(ex);
                            Console.WriteLine("Your email failed to send after 3 retries. A log has been made. Please contact your system administrator if the problem continues.");
                        }
                    }
                }

                //Get user response of whether to continue writing emails or to exit program.
                while(writeAnother == true)
                {
                    Console.WriteLine("Would you like to write another email? Y/N");
                    string response = Console.ReadLine();
                    if (response != "Y" && response != "N")
                    {
                        Console.WriteLine("Please enter only Y or N");
                    }
                    else if (response == "N")
                    {
                        writeAnother = false;
                    }
                    else
                    {
                        break;
                    }
                }
                

            }

            // A method to write the email data to the database asynchronously.
            static async Task writeToDB(string sender, string recipient, string subject, string body, bool sendSuccessful)
            {

                using (var context = new EmailContext())
                {
                    var em = new EmailEntity()
                    {
                        senderAddress = sender,
                        recipeintAddress = recipient,
                        subject = subject,
                        sendSuccessful = sendSuccessful,
                        sendTime = DateTime.Now,
                        body = body
                    };

                    context.Emails.Add(em);
                    await context.SaveChangesAsync();
                }
            }

        }


        //Added NuGet package:   Microsoft.Extensions.Hosting
        //Added NuGet package:   Microsoft.EntityFrameworkCore.SqlServer
        //Added NuGet package:   Microsoft.EntityFrameworkCore.Design
        //Added NuGet package:   Microsoft.EntityFrameworkCore
        //Added NuGet package:   


    }


}