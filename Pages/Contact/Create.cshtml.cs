using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using MailKit.Net.Smtp;
using Salto.Data;
using Salto.Models;

namespace Salto.Pages.Contact
{
    public class CreateModel : PageModel
    {
        private readonly Salto.Data.ApplicationDbContext _context;

        public CreateModel(Salto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Salto.Models.Contact Contact { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Load environment variables from .env file
            LoadEnvironmentVariables();

            // Retrieve environment variables
            var smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER");
            var smtpPort = Environment.GetEnvironmentVariable("SMTP_PORT");
            var emailUsername = Environment.GetEnvironmentVariable("EMAIL_USERNAME");
            var emailPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

            // Send email notification
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Timewise Website", "TimewiseWebsite@gmail.com"));
            message.To.Add(new MailboxAddress("Timewise", "supportTimewise@mellbrand.com"));
            message.Subject = "Message from Timewise Website";

            var body = $"Name: {Contact.FirstName} {Contact.LastName}<br>Organization: {Contact.Organization}<br>Email: {Contact.Email}<br>Phone: {Contact.Phone}<br>Message: {Contact.Message}<br>Order: {Contact.VersionDemoMeeting}";
            message.Body = new TextPart("html") { Text = body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServer, Convert.ToInt32(smtpPort), false);
                await client.AuthenticateAsync(emailUsername, emailPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            // Add contact form info to the database
            _context.Contact.Add(Contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private void LoadEnvironmentVariables()
        {
            var envFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".env");

            if (System.IO.File.Exists(envFilePath))  // Fully qualify the File class
            {
                var lines = System.IO.File.ReadAllLines(envFilePath);  // Fully qualify the File class
                foreach (var line in lines)
                {
                    var parts = line.Split("=", StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        var key = parts[0].Trim();
                        var value = parts[1].Trim();
                        Environment.SetEnvironmentVariable(key, value);
                    }
                }
            }
        }

    }
}


//Local Testing: Set the environment variables in your local development environment.
//You can use a tool like DotNetEnv to manage environment variables in development.
//Refer to .env in the wwwrooot
//remember to add .env to gitignore file
//Production Deployment: Configure the environment variables in your server environment where the application is hosted. Consult your hosting provider's documentation on how to set environment variables for your deployed application.
