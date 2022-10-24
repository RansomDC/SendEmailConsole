using System;
using System.Net.Mail;
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

            //==================================
            //  DI and appsettings.json access
            //==================================

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



            //=================================
            //   User Interface begins here
            //=================================
            //Greeting
            Console.WriteLine("Hello, and welcome to the email sender. Please enter the information of the email you would like to send,\nif you are unhappy with your email you can choose not to send it at the end of the process");

            // The following loop continues as long as the user wants to keep sending emails. writeAnother has the opportunity to change based on user input.
            bool writeAnother = true;
            while (writeAnother == true)
            {

                //We will use this message object to validate the input of our users.
                var testMessage = new MailMessage();

                Console.WriteLine("\nPlease enter the email address of the person you want to email:");
                string recipient;
                do
                {
                    try
                    {
                        recipient = Console.ReadLine();
                        testMessage.To.Add(recipient);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please be sure that your input is in the format \"example@email.com\"");
                        recipient = "";
                    }
                } while (recipient == "");

                
                Console.WriteLine("Please enter your email address:");
                string sender;
                // Validates the input received from the above
                do
                {
                    try
                    {
                        sender = Console.ReadLine();
                        testMessage.From = new MailAddress(sender);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please be sure that your input is in the format \"example@email.com\"");
                        sender = "";
                    }
                } while (sender == "");

                //Get email subject from user, provide default value if none is given
                Console.WriteLine("Please enter the email's subject");
                string subject = Console.ReadLine();
                if (subject == "") { subject = "<No Subject>"; }

                //Get email body from user, provide default value if none is given
                Console.WriteLine("Please enter the message you would like to send");
                string body = Console.ReadLine();
                if (body == "") { body = "<No body>"; }


                // Checks to see if the user wants to send the compiled email
                Console.WriteLine("\nYour email reads:\n" +
                    $"To: {recipient}\n" +
                    $"From: {sender}\n" +
                    $"Subject: {subject}\n" +
                    $"Message: {body}");
                Console.WriteLine("Would you like to send your email?  Y/N");
                string response = Console.ReadLine();
                do
                {
                    if (response != "Y" && response != "N")
                    {
                        Console.WriteLine("Please enter only Y or N");
                        response = Console.ReadLine();
                    }
                    else if (response == "Y")
                    {
                        //Calls the async method to send the email. This allows this method to be called without waiting for any response from the method.
                        _ = emailSVC.SendEmail(sender, recipient, subject, body);
                    }
                } while (response != "Y" && response != "N");


                    //Get user response of whether to continue writing emails or to exit program.
                    while (writeAnother == true)
                {
                    Console.WriteLine("Would you like to write another email? Y/N");
                    string more = Console.ReadLine();
                    if (more != "Y" && more != "N")
                    {
                        Console.WriteLine("Please enter only Y or N");
                    }
                    else if (more == "N")
                    {
                        writeAnother = false;
                    }
                    else
                    {
                        break;
                    }

                }

            }
            
        }
 
    }


}