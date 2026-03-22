using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Models;

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
            Bio = "I'm a software developer whose journey into tech has been driven by curiosity, persistence, and a passion for building real solutions. From starting with the fundamentals to now working as an RPG Developer, I’ve consistently grown by applying my skills in practical environments and solving real-world problems.\n\nI have gained hands-on experience developing backend and enterprise systems on IBM i using ILE RPG and DB2, while also building modern applications with C#, Java, ASP.NET Core, and JavaScript. Along the way, I’ve worked on full-stack projects and automation solutions, continuously improving my ability to write clean, efficient, and reliable code.\n\nI’m particularly motivated by the challenge of learning new technologies, improving existing systems, and bridging the gap between modern development and enterprise platforms. As an early-career developer, I’m eager to keep building, growing, and contributing to impactful software solutions.",
            Location = "Tembisa, GP",
            Email = "davidsedumedi23@gmail.com",
            Github = "https://github.com/Mdeva23",
            LinkedIn = "https://www.linkedin.com/in/david-sedumedi-8331a3291",
            Tags = new[] { "C#", ".NET", "ILE RPG", "Java", "Python", "SQL", "DB2" }
        });
    }

    [HttpGet("projects")]
    public ActionResult<IEnumerable<Project>> GetProjects()
    {
        return Ok(new List<Project>
        {
            new Project
            {
                Id = 1,
                Title = "SSD PortfolioApp",
                Description = "A modern full-stack portfolio website built with ASP.NET Core 8 and vanilla JavaScript. Features dark/light mode, scroll animations, skill bars, project filtering, and a working contact form.",
                Tags = new[] { "C#", "ASP.NET Core", "JS", "HTML", "CSS" },
                GithubUrl = "https://github.com/Mdeva23/portfolio-website",
                LiveUrl = "https://ssd-portfolio-website.onrender.com/",
                Category = "Full-Stack",
                Year = 2026
            },
            new Project
            {
                Id = 2,
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
                Id = 3,
                Title = "Excel RPA",
                Description = "Developed a robotic process automation solution using UiPath to streamline Excel based data entry and validation. Used SQL for data validation and built error handling to ensure smooth operation.",
                Tags = new[] { "UiPath", "RPA", "Excel", "SQL", "Automation" },
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
            new Skill { Id = 1,  Name = "C#",              Proficiency = 82, Category = "Backend",   Icon = "⚙️" },
            new Skill { Id = 2,  Name = "Java",            Proficiency = 75, Category = "Backend",   Icon = "☕" },
            new Skill { Id = 3,  Name = "Python",          Proficiency = 70, Category = "Backend",   Icon = "🐍" },
            new Skill { Id = 4,  Name = "ILE RPG",         Proficiency = 85, Category = "Backend",   Icon = "🖥️" },
            new Skill { Id = 5,  Name = "ASP.NET Core",    Proficiency = 78, Category = "Backend",   Icon = "🔷" },
            new Skill { Id = 6,  Name = "JavaScript",      Proficiency = 70, Category = "Frontend",  Icon = "🟡" },
            new Skill { Id = 7,  Name = "HTML",            Proficiency = 78, Category = "Frontend",  Icon = "🌐" },
            new Skill { Id = 8,  Name = "CSS",             Proficiency = 75, Category = "Frontend",  Icon = "🎨" },
            new Skill { Id = 9,  Name = "SQL",             Proficiency = 85, Category = "Database",  Icon = "🗄️" },
            new Skill { Id = 10, Name = "DB2 (IBM i)",     Proficiency = 80, Category = "Database",  Icon = "📦" },
            new Skill { Id = 11, Name = "Microsoft Azure", Proficiency = 68, Category = "Cloud",     Icon = "☁️" },
            new Skill { Id = 12, Name = "UiPath (RPA)",    Proficiency = 75, Category = "Tools",     Icon = "🤖" },
            new Skill { Id = 13, Name = "Git & GitHub",    Proficiency = 69, Category = "Tools",     Icon = "🐙" }
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
                Period = "Feb 2025 — Dec 2025",
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

    [HttpGet("education")]
    public ActionResult<IEnumerable<Education>> GetEducation()
    {
        return Ok(new List<Education>
        {
            new Education
            {
                Id = 1,
                Degree = "BSc in Information Technology",
                Institution = "North-West University",
                Location = "Vanderbijlpark, Vereeniging",
                Period = "Feb 2022 — Nov 2024",
                Description = "Completed a comprehensive degree in Information Technology with a focus on software development, enterprise systems, and databases. Graduated with hands-on project experience across multiple programming languages and platforms.",
                Modules = new[]
                {
                    "Systems Analysis & Design",
                    "Object-Oriented Programming (C# & Java)",
                    "Data Structures & Algorithms",
                    "Databases & SQL",
                    "Computer Networks & Security",
                    "Cloud Computing (Microsoft Azure)"
                },
                IsCurrent = false
            }
        });
    }

    [HttpGet("certificates")]
    public ActionResult<IEnumerable<Certificate>> GetCertificates()
    {
        return Ok(new List<Certificate>
        {
            new Certificate
            {
                Id = 1,
                Title = "RPG ILE Language",
                Issuer = "Momentum Metropolitan Life Limited",
                Year = "2025",
                CredentialUrl = "",
                Icon = "🖥️"
            },
            new Certificate
            {
                Id = 2,
                Title = "Artificial Intelligence Fundamentals",
                Issuer = "IBM SkillsBuild",
                Year = "2025",
                CredentialUrl = "https://www.credly.com/badges/f312839d-9ff6-408b-b7f7-086879bf76af",
                Icon = "🧠"
            }
        });
    }

    [HttpPost("contact")]
    public ActionResult SendMessage([FromBody] ContactMessage message)
    {
        if (string.IsNullOrWhiteSpace(message.Name) ||
            string.IsNullOrWhiteSpace(message.Email) ||
            string.IsNullOrWhiteSpace(message.Message))
        {
            return BadRequest(new { error = "Name, email, and message are required." });
        }

        // Email is handled by EmailJS on the frontend
        Console.WriteLine($"[Contact] From: {message.Name} <{message.Email}> — {message.Subject}");

        return Ok(new { success = true, message = "Message received! I'll get back to you within 24 hours." });
    }
}