namespace PortfolioApp.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string[] Tags { get; set; } = Array.Empty<string>();
    public string GithubUrl { get; set; } = string.Empty;
    public string LiveUrl { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Year { get; set; }
}

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Proficiency { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

public class Experience
{
    public int Id { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string[] Highlights { get; set; } = Array.Empty<string>();
    public bool IsCurrent { get; set; }
}

public class Education
{
    public int Id { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string[] Modules { get; set; } = Array.Empty<string>();
    public bool IsCurrent { get; set; }
}

public class Certificate
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string CredentialUrl { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

public class ContactMessage
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class PersonalInfo
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Github { get; set; } = string.Empty;
    public string LinkedIn { get; set; } = string.Empty;
    public string Twitter { get; set; } = string.Empty;
    public string[] Tags { get; set; } = Array.Empty<string>();
}