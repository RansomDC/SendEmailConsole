# SendEmailConsole

This application was build using C# and .NET core 6.0 in Visual Studio 2022

### NuGet Packages included in this project:
* Microsoft.EntityFrameworkCore 6.0.1
* Microsoft.EntityFrameworkCore.Design 6.0.1
* Microsoft.EntityFrameworkCore.SqlServer 6.0.1
* Microsoft.Extensions.Hosting 6.0.1


## Explanation
The console application was built to send emails. The main criteria were as follows:

1. The SendEmail Method needed to be in an external dll. (The SendEmailLibrary.dll can now be used in other applications with minimal adjustments)
2. Sender, recipient, subject, body, date, and status of send attempt must be stored in a database for future reference.
3. If the email fails to send it must be retried (up to three times).
4. Credentials needed to be stored in an appsetting file rather than hardcoded into the methods.
5. The user must not be interrupted or delayed while navigating the website simply because an email fails to send.


## Directions for downloading and running this application
1. Download the code (e.g SendEmailConsole.zip)
2. Start the program in Visual Studio 2022
3. Enter the credentials for the SMTP you want to use in the appsettings.json file. This should include: 
  * An smtp server (e.g. smtp-relay.sendinblue.com)
  * A port (e.g. 587)
  * A username for your smtp service (e.g. myemail@example.com)
  * A password for your smp service
4. Make migrations for the database being sure to select the SendEmailLibrary and then create the database in the Package Manager Console with:
  * add-migration * Insert migration name *
  * update-database
5. Build the SendEmailLibrary
6. Build the SendEmailConsole
7. Run the program!
