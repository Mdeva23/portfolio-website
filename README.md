# {SSD} Portfolio — Setshogo David Sedumedi

**Live site:** [ssd-portfolio-website.onrender.com](https://ssd-portfolio-website.onrender.com)

---
##  Features

-  **Dark / Light mode** — persisted in localStorage with smooth CSS transitions
-  **REST API** — 7 endpoints serving all portfolio data as JSON
-  **Animated UI** — scroll reveal, skill bars, typewriter terminal, custom cursor
-  **Project filtering** — filter by category without page reload
-  **Working contact form** — sends real emails via EmailJS (browser-based, no SMTP)
-  **Education section** — degree, institution, modules displayed as chips
-  **Experience timeline** — animated vertical timeline with current role indicator
-  **Certificates section** — with credential links

---
##  Tech Stack

| Layer | Technology |
|---|---|
| Backend | C# 12 / .NET 8 / ASP.NET Core Web API |
| Frontend | HTML5, CSS3, JavaScript |
| Email | EmailJS (browser-based, no SMTP) |
| Fonts | Syne, DM Mono, DM Sans (Google Fonts) |
| Hosting | Render (free tier) |
| Server | Kestrel (built-in .NET) |

---
##  Project Structure
```
PortfolioApp/
├── Controllers/
│   └── PortfolioController.cs   # All 7 API endpoints
├── Models/
│   └── Portfolio.cs             # Data models
├── wwwroot/
│   ├── index.html               # Single-page app shell
│   ├── David.jpeg               # Profile photo
│   ├── Setshogo_David_Sedumedi_Resume.pdf
│   ├── favicon_ssd_code.png
│   ├── css/
│   │   └── styles.css           # Full stylesheet (dark/light theme)
│   └── js/
│       └── app.js               # All frontend logic
├── Program.cs                   # App startup and middleware
├── appsettings.json             # App configuration
└── PortfolioApp.csproj
```

---
##  API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/portfolio/info` | Personal info, bio, social links |
| GET | `/api/portfolio/projects` | All projects with tags and links |
| GET | `/api/portfolio/skills` | Skills with proficiency percentages |
| GET | `/api/portfolio/experience` | Work history timeline |
| GET | `/api/portfolio/education` | Degree, institution, modules |
| GET | `/api/portfolio/certificates` | Certificates with credential links |
| POST | `/api/portfolio/contact` | Contact form (logs to console) |

---
##  Getting Started
### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)

### Run locally

```bash
# Clone the repo
git clone https://github.com/Mdeva23/portfolio-website.git
cd portfolio-website

# Run
dotnet run
```

Open **http://localhost:5000** in your browser.

---
##  Customisation

All content lives in `Controllers/PortfolioController.cs`. Update the methods below to reflect your own details:

| Method | What to update |
|---|---|
| `GetInfo()` | Name, bio, location, email, social links, tags |
| `GetProjects()` | Project title, description, tech tags, GitHub/live URLs |
| `GetSkills()` | Skill name, proficiency (0–100), category, icon |
| `GetExperience()` | Role, company, period, highlights |
| `GetEducation()` | Degree, institution, modules |
| `GetCertificates()` | Title, issuer, year, credential URL |

---
##  Contact Form (EmailJS)

The contact form sends emails directly from the browser using [EmailJS](https://emailjs.com).

To configure your own credentials, update these values in `wwwroot/js/app.js`:

```javascript
const EMAILJS_PUBLIC_KEY  = 'your_public_key';
const EMAILJS_SERVICE_ID  = 'your_service_id';
const EMAILJS_TEMPLATE_ID = 'your_template_id';
```

---
##  Deployment (Render)

### 1. Add a Dockerfile to the project root

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "PortfolioApp.dll"]
```

### 2. Push to GitHub

```bash
git add .
git commit -m "deploy"
git push
```

### 3. Deploy on Render
1. Go to [render.com](https://render.com) and sign in with GitHub
2. Click **New → Web Service → Connect your repo**
3. Set build command: `dotnet publish -c Release -o out`
4. Set start command: `dotnet out/PortfolioApp.dll`
5. Or use the Dockerfile — Render auto-detects it
6. Your site goes live at `your-app.onrender.com`

Every `git push` triggers an automatic redeploy.

---

##  Local Development Tips
```bash
# Run with hot reload
dotnet watch run

# Build only
dotnet build
```

---

## Author

**Setshogo David Sedumedi**

- GitHub: [@Mdeva23](https://github.com/Mdeva23)
- LinkedIn: [david-sedumedi-8331a3291](https://www.linkedin.com/in/david-sedumedi-8331a3291)
- Email: davidsedumedi23@gmail.com
