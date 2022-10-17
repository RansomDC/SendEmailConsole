# SendEmailConsole

This program was built to send emails from a C# .NET core 6.0 Console application. The main criteria were as follows:

1. The SendEmail Method needed to be in an external dll. (The SendEmailLibrary.dll can now be used in other applications with minimal adjustments)
2. Sender, recipient, subject, body, date, and status of send attempt must be stored in a database for future reference.
3. If the email fails to send it must be retried (up to three times).
4. Credentials needed to be stored in an appsetting file rather than hardcoded into the methods.
5. The user must not be interrupted or delayed while navigating the website simply because an email fails to send.

