using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace PortfolioApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfolioController : ControllerBase
{
    private readonly IConfiguration _config;

    public PortfolioController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("info")]
    public ActionResult<PersonalInfo> GetInfo()
    {
        return Ok(new PersonalInfo
        {
            Name = "Sedumedi Setshogo David",
            Title = "Software Developer",
            Bio = "I’m a versatile software developer with expertise in backend development, enterprise systems, and data-driven solutions. I work with technologies including C#, Java, Python, SQL, ILE RPG, DB2, and UiPath, building clean, efficient, and reliable software. I’m passionate about solving complex problems and delivering impactful technology solutions.",
            Location = "Tembisa, GP",
            Email = "davidsedumedi23@gmail.com",
            Github = "https://github.com/Mdeva23",
            LinkedIn = "https://www.linkedin.com/in/david-sedumedi-8331a3291"
        });
    }

    [HttpGet("projects")]
    public ActionResult<IEnumerable<Project>> GetProjects()
    {
        return Ok(new List<Project>
        {
            new Project
            {
                Id = 2,
                Title = "SSD PortfolioApp",
                Description = "A modern full-stack portfolio website built with ASP.NET Core 8 and vanilla JavaScript. Features dark/light mode, scroll animations, skill bars, project filtering, and a working contact form.",
                Tags = new[] { "C#", "ASP.NET", "ASP.NET Core", "JS", "HTML", "CSS" },
                GithubUrl = "https://github.com/Mdeva23/portfolio-website",
                LiveUrl = "https://ssd-portfolio-website.onrender.com/",
                Category = "Full-Stack",
                Year = 2026
            },
            new Project
            {
                Id = 3,
                Title = "RPG Order Entry System",
                Description = "Built an interactive order entry system on IBM i using RPGLE and embedded SQL, featuring subfile-based record navigation, order processing, and automated price and VAT calculations.",
                Tags = new[] { "RPGLE Free Format", "DB2", "Embedded SQL", "Subfiles", "Python" },
                GithubUrl = "",
                LiveUrl = "",
                Category = "Backend",
                Year = 2025
            },
            new Project
            {
                Id = 4,
                Title = "Excel RPA",
                Description = "Developed a robotic process automation solution using UiPath to streamline Excel based data entry and validation.",
                Tags = new[] { "UiPath", "RPA", "Excel", "Automation" },
                GithubUrl = "https://github.com/Mdeva23/CMPG-323-Project-4---38436272-",
                LiveUrl = "",
                Category = "Tools",
                Year = 2024
            }
        });
    }

    [HttpGet("skills")]
    public ActionResult<IEnumerable<Skill>> GetSkills()
    {
        return Ok(new List<Skill>
        {
            new Skill { Id = 1,  Name = "Java",            Proficiency = 75, Category = "Backend",   Icon = "☕" },
            new Skill { Id = 2,  Name = "C#",              Proficiency = 82, Category = "Backend",   Icon = "⚙️" },
            new Skill { Id = 3,  Name = "Python",          Proficiency = 70, Category = "Backend",   Icon = "🐍" },
            new Skill { Id = 4,  Name = "ILE RPG",         Proficiency = 85, Category = "Backend",   Icon = "💻" },
            new Skill { Id = 5,  Name = "ASP.NET Core",    Proficiency = 78, Category = "Backend",   Icon = "🔷" },
            new Skill { Id = 6,  Name = "JavaScript",      Proficiency = 70, Category = "Frontend",  Icon = "🟡" },
            new Skill { Id = 7,  Name = "HTML",            Proficiency = 78, Category = "Frontend",  Icon = "🌐" },
            new Skill { Id = 8,  Name = "CSS",             Proficiency = 75, Category = "Frontend",  Icon = "🎨" },
            new Skill { Id = 9,  Name = "SQL",             Proficiency = 85, Category = "Database",  Icon = "🗄️" },
            new Skill { Id = 10, Name = "DB2 (IBM i)",     Proficiency = 80, Category = "Database",  Icon = "📦" },
            new Skill { Id = 11, Name = "Microsoft Azure", Proficiency = 68, Category = "Cloud",     Icon = "☁️" },
            new Skill { Id = 12, Name = "Git & GitHub",    Proficiency = 69, Category = "Tools",     Icon = "🐙" },
            new Skill { Id = 13, Name = "UiPath (RPA)",    Proficiency = 75, Category = "Tools",     Icon = "🤖" }
        });
    }

    [HttpGet("experience")]
    public ActionResult<IEnumerable<Experience>> GetExperience()
    {
        return Ok(new List<Experience>
        {
            new Experience
            {
                Id = 1,
                Role = "Full Stack Junior RPG Developer",
                Company = "Nashua LTD",
                Period = "Dec 2025 — Present",
                Description = "Developed and optimized enterprise applications on IBM i (AS/400) using ILE RPG, DB2, and embedded SQL, delivering reliable and efficient backend solutions for business-critical operations.",
                Highlights = new[]
                {
                    "Built interactive subfile programs improving backend processing efficiency",
                    "Modernized legacy systems increasing reliability and maintainability of enterprise applications",
                    "Implemented paging, validation, and batch processing ensuring accurate and high performance business operations"
                },
                IsCurrent = true
            },
            new Experience
            {
                Id = 2,
                Role = "IBM I RPG Developer Intern",
                Company = "Momentum Metropolitan",
                Period = "Feb 2025 - Dec 2025",
                Description = "Gained hands-on experience with enterprise systems on IBM i (AS/400), learning ILE RPG, DB2, and embedded SQL in real world development environments.",
                Highlights = new[]
                {
                    "Gained hands-on experience solving real-world problems alongside experienced developers",
                    "Learned IBM i backend development by assisting in enterprise projects",
                    "Contributed to interactive programs supporting business-critical systems"
                },
                IsCurrent = false
            }
        });
    }

    [HttpPost("contact")]
    public async Task<ActionResult> SendMessage([FromBody] ContactMessage message)
    {
        if (string.IsNullOrWhiteSpace(message.Name) ||
            string.IsNullOrWhiteSpace(message.Email) ||
            string.IsNullOrWhiteSpace(message.Message))
        {
            return BadRequest(new { error = "Name, email, and message are required." });
        }

        try
        {
            // 🔹 Read secrets from environment variables (works locally too)
            var senderEmail = _config["EmailSettings:SenderEmail"];
            var senderPassword = _config["EmailSettings:SenderPassword"];
            var receiverEmail = _config["EmailSettings:ReceiverEmail"];

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(message.Name, senderEmail));
            email.To.Add(new MailboxAddress("Portfolio Owner", receiverEmail));
            email.Subject = $"Portfolio Contact: {message.Subject}";
            email.Body = new TextPart("plain")
            {
                Text =
                    $"From: {message.Name}\n" +
                    $"Email: {message.Email}\n\n" +
                    $"Message:\n{message.Message}"
            };

            using var smtp = new SmtpClient();
            // 🔹 Connect using StartTLS (works with Gmail App Password)
            await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(senderEmail, senderPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            return Ok(new
            {
                success = true,
                message = "Message received! I'll get back to you within 24 hours."
            });
        }
        catch (Exception ex)
        {
            // 🔹 Log for Render live debugging
            Console.WriteLine($"[Email Error] {ex.Message}");
            return StatusCode(500, new { error = "Failed to send email. Please check your credentials or SMTP settings." });
        }
    }
}