const API = '/api/portfolio';

/* ============================================================
   BOOT
   ============================================================ */
document.addEventListener('DOMContentLoaded', () => {
  initTheme();
  initCursor();
  initNav();
  initMobileMenu();
  initReveal();
  initTerminal();
  loadAll();
});

/* ============================================================
   THEME (dark / light, persisted in localStorage)
   ============================================================ */
function initTheme() {
  const saved = localStorage.getItem('portfolio-theme') || 'dark';
  applyTheme(saved);
  document.getElementById('themeToggle').addEventListener('click', () => {
    const current = document.documentElement.getAttribute('data-theme');
    applyTheme(current === 'dark' ? 'light' : 'dark');
  });
}
function applyTheme(theme) {
  document.documentElement.setAttribute('data-theme', theme);
  localStorage.setItem('portfolio-theme', theme);
}

/* ============================================================
   CUSTOM CURSOR
   ============================================================ */
function initCursor() {
  if (window.innerWidth < 768) return;
  const dot   = document.getElementById('cursor');
  const trail = document.getElementById('cursorTrail');
  let tx = 0, ty = 0, cx = 0, cy = 0;

  document.addEventListener('mousemove', e => {
    tx = e.clientX; ty = e.clientY;
    dot.style.left = tx + 'px';
    dot.style.top  = ty + 'px';
  });

  (function animate() {
    cx += (tx - cx) * 0.12;
    cy += (ty - cy) * 0.12;
    trail.style.left = cx + 'px';
    trail.style.top  = cy + 'px';
    requestAnimationFrame(animate);
  })();

  document.querySelectorAll('a, button, .project-card, .skill-category-card').forEach(el => {
    el.addEventListener('mouseenter', () => document.body.classList.add('cursor-active'));
    el.addEventListener('mouseleave', () => document.body.classList.remove('cursor-active'));
  });
}

/* ============================================================
   NAVIGATION
   ============================================================ */
function initNav() {
  const nav = document.getElementById('nav');

  window.addEventListener('scroll', () => {
    nav.classList.toggle('scrolled', window.scrollY > 30);
    highlightActiveSection();
  }, { passive: true });

  document.querySelectorAll('a[href^="#"]').forEach(link => {
    link.addEventListener('click', e => {
      const id = link.getAttribute('href');
      const target = document.querySelector(id);
      if (!target) return;
      e.preventDefault();
      target.scrollIntoView({ behavior: 'smooth' });
      document.getElementById('mobileMenu').classList.remove('open');
    });
  });
}

function highlightActiveSection() {
  const sections = ['home','about','projects','skills','experience','contact'];
  const mid = window.scrollY + window.innerHeight / 3;
  sections.forEach(id => {
    const el = document.getElementById(id);
    if (!el) return;
    if (mid >= el.offsetTop && mid < el.offsetTop + el.offsetHeight) {
      document.querySelectorAll('.nav-link').forEach(l => l.classList.remove('active'));
      const a = document.querySelector(`.nav-link[data-section="${id}"]`);
      if (a) a.classList.add('active');
    }
  });
}

/* ============================================================
   MOBILE MENU
   ============================================================ */
function initMobileMenu() {
  document.getElementById('mobileMenuBtn').addEventListener('click', () => {
    document.getElementById('mobileMenu').classList.toggle('open');
  });
}

/* ============================================================
   SCROLL REVEAL
   ============================================================ */
function initReveal() {
  const obs = new IntersectionObserver((entries) => {
    entries.forEach((entry, i) => {
      if (entry.isIntersecting) {
        setTimeout(() => entry.target.classList.add('visible'), i * 80);
        obs.unobserve(entry.target);
      }
    });
  }, { threshold: 0.1, rootMargin: '0px 0px -40px 0px' });

  document.querySelectorAll('.reveal').forEach(el => obs.observe(el));
}

/* ============================================================
   TERMINAL TYPEWRITER
   ============================================================ */
function initTerminal() {
  const steps = [
    { delay: 700,  id: 'to1' },
    { delay: 1300, id: 'tl2' },
    { delay: 1900, id: 'to2' },
    { delay: 2500, id: 'tl3' },
    { delay: 3100, id: 'to3' },
    { delay: 3700, id: 'tl4' },
    { delay: 4300, id: 'to5' },
  ];
  steps.forEach(({ delay, id }) =>
    setTimeout(() => document.getElementById(id)?.classList.remove('t-hidden'), delay)
  );
}

/* ============================================================
   DATA LOADING
   ============================================================ */
async function loadAll() {
  await Promise.allSettled([
    loadInfo(),
    loadProjects(),
    loadSkills(),
    loadExperience(),
  ]);
  setupContactForm();
}

/* ---------- Info ---------- */
async function loadInfo() {
  const d = await apiFetch('info');
  if (!d) return;

  setText('heroDesc',        d.bio);
  setText('aboutBio',        d.bio);
  setText('aboutLocation',   d.location);
  setLink('aboutEmail',      d.email, `mailto:${d.email}`);
  setLink('contactEmail',    d.email, `mailto:${d.email}`);
  setText('contactLocation', d.location);
  document.title = `${d.name} — ${d.title}`;

  const tagsEl = document.getElementById('heroTags');
  if (tagsEl && d.tags)
    tagsEl.innerHTML = d.tags.map(t => `<span class="hero-tag">${sanitize(t)}</span>`).join('');

  const socials = document.getElementById('socialLinks');
  if (socials) {
    const items = [
      { label: '⌥ GitHub',   url: d.github   },
      { label: '⊞ LinkedIn', url: d.linkedIn  },
    ].filter(x => x.url);
    socials.innerHTML = items.map(x =>
      `<a href="${sanitize(x.url)}" target="_blank" rel="noopener" class="social-link">${x.label}</a>`
    ).join('');
  }
}

/* ---------- Projects ---------- */
async function loadProjects() {
  const projects = await apiFetch('projects') ?? [];
  renderProjects(projects);

  document.getElementById('filterBar')?.addEventListener('click', e => {
    const btn = e.target.closest('.filter-btn');
    if (!btn) return;
    document.querySelectorAll('.filter-btn').forEach(b => b.classList.remove('active'));
    btn.classList.add('active');
    const filter = btn.dataset.filter;
    document.querySelectorAll('.project-card').forEach(card => {
      card.classList.toggle('filtered', filter !== 'all' && card.dataset.category !== filter);
    });
  });
}

function renderProjects(projects) {
  const grid = document.getElementById('projectsGrid');
  if (!grid) return;
  if (!projects.length) { grid.innerHTML = '<p class="loading-state">No projects found.</p>'; return; }

  grid.innerHTML = projects.map(p => `
    <div class="project-card reveal" data-category="${sanitize(p.category)}">
      <div class="project-top">
        <span class="project-cat">${sanitize(p.category)}</span>
        <span class="project-year">${p.year}</span>
      </div>
      <h3 class="project-title">${sanitize(p.title)}</h3>
      <p class="project-desc">${sanitize(p.description)}</p>
      <div class="project-tags">${(p.tags||[]).map(t => `<span class="project-tag">${sanitize(t)}</span>`).join('')}</div>
      <div class="project-links">
        ${p.githubUrl ? `<a href="${sanitize(p.githubUrl)}" target="_blank" rel="noopener" class="project-link">⌥ GitHub</a>` : ''}
        ${p.liveUrl   ? `<a href="${sanitize(p.liveUrl)}"   target="_blank" rel="noopener" class="project-link">↗ Live</a>`   : ''}
      </div>
    </div>
  `).join('');

  observeCards('.project-card.reveal');
}

/* ---------- Skills ---------- */
async function loadSkills() {
  const skills = await apiFetch('skills') ?? [];
  if (!skills.length) return;

  const categories = [...new Set(skills.map(s => s.category))];
  const catMeta = {
    Backend:  { icon: '⚙️', desc: 'Server-side architecture, APIs, and data engineering.' },
    Frontend: { icon: '🎨', desc: 'Crafting responsive, accessible user interfaces.' },
    Database: { icon: '🗄️', desc: 'Relational and NoSQL data storage & querying.' },
    DevOps:   { icon: '☁️', desc: 'CI/CD, containerisation, and cloud infrastructure.' },
  };

  const catsEl = document.getElementById('skillsCategories');
  if (!catsEl) return;

  catsEl.innerHTML = categories.map((cat, i) => {
    const meta = catMeta[cat] || { icon: '🔧', desc: '' };
    const count = skills.filter(s => s.category === cat).length;
    return `
      <div class="skill-category-card ${i === 0 ? 'active' : ''}" data-cat="${sanitize(cat)}">
        <div class="scc-top">
          <span class="scc-icon">${meta.icon}</span>
          <span class="scc-name">${sanitize(cat)}</span>
          <span class="scc-count">${count}</span>
        </div>
        <p class="scc-desc">${meta.desc}</p>
      </div>`;
  }).join('');

  renderSkillBars(categories[0], skills);

  catsEl.addEventListener('click', e => {
    const card = e.target.closest('.skill-category-card');
    if (!card) return;
    document.querySelectorAll('.skill-category-card').forEach(c => c.classList.remove('active'));
    card.classList.add('active');
    renderSkillBars(card.dataset.cat, skills);
  });
}

function renderSkillBars(category, skills) {
  const barsEl = document.getElementById('skillsBars');
  if (!barsEl) return;
  const filtered = skills.filter(s => s.category === category);

  barsEl.innerHTML = filtered.map(s => `
    <div class="skill-bar-item">
      <div class="skill-bar-top">
        <span class="skill-name"><span>${s.icon}</span>${sanitize(s.name)}</span>
        <span class="skill-pct">${s.proficiency}%</span>
      </div>
      <div class="skill-track">
        <div class="skill-fill" data-pct="${s.proficiency}"></div>
      </div>
    </div>
  `).join('');

  requestAnimationFrame(() => requestAnimationFrame(() => {
    barsEl.querySelectorAll('.skill-fill').forEach(f => {
      f.style.width = f.dataset.pct + '%';
    });
  }));
}

/* ---------- Experience ---------- */
async function loadExperience() {
  const exp = await apiFetch('experience') ?? [];
  const timeline = document.getElementById('timeline');
  if (!timeline) return;
  if (!exp.length) { timeline.innerHTML = '<p class="loading-state">No experience found.</p>'; return; }

  timeline.innerHTML = exp.map(e => `
    <div class="timeline-item ${e.isCurrent ? 'current' : ''}">
      <div class="timeline-dot"></div>
      <div class="timeline-period">${sanitize(e.period)}</div>
      <h3 class="timeline-role">${sanitize(e.role)}</h3>
      <div class="timeline-company">${sanitize(e.company)}</div>
      <p class="timeline-desc">${sanitize(e.description)}</p>
      <ul class="timeline-highlights">
        ${(e.highlights||[]).map(h => `<li>${sanitize(h)}</li>`).join('')}
      </ul>
    </div>
  `).join('');

  const obs = new IntersectionObserver((entries) => {
    entries.forEach((entry, i) => {
      if (entry.isIntersecting) {
        setTimeout(() => entry.target.classList.add('visible'), i * 150);
        obs.unobserve(entry.target);
      }
    });
  }, { threshold: 0.15 });
  timeline.querySelectorAll('.timeline-item').forEach(el => obs.observe(el));
}

/* ============================================================
   CONTACT FORM
   ============================================================ */
function setupContactForm() {
  const form     = document.getElementById('contactForm');
  const btn      = document.getElementById('submitBtn');
  const feedback = document.getElementById('formFeedback');
  if (!form) return;

  form.addEventListener('submit', async e => {
    e.preventDefault();

    const btnText    = btn.querySelector('.btn-text');
    const btnArrow   = btn.querySelector('.btn-arrow');
    const btnLoading = btn.querySelector('.btn-loading');

    btn.disabled = true;
    btnText.classList.add('t-hidden');
    btnArrow.classList.add('t-hidden');
    btnLoading.classList.remove('t-hidden');
    feedback.className = 'form-feedback t-hidden';
    feedback.textContent = '';

    const payload = {
      name:    form.querySelector('[name="name"]').value.trim(),
      email:   form.querySelector('[name="email"]').value.trim(),
      subject: form.querySelector('[name="subject"]').value.trim(),
      message: form.querySelector('[name="message"]').value.trim(),
    };

    try {
      const [res] = await Promise.all([
        fetch(`${API}/contact`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(payload),
        }),
        new Promise(resolve => setTimeout(resolve, 800))
      ]);

      let data;
      try { data = await res.json(); } catch { data = {}; }

      if (res.ok) {
        feedback.textContent = data.message || 'Message sent successfully!';
        feedback.className   = 'form-feedback success';
        form.reset();
      } else {
        feedback.textContent = data.error || 'Something went wrong. Please try again.';
        feedback.className   = 'form-feedback error';
      }

    } catch (err) {
      console.error(err);
      feedback.textContent = 'Network error — please try again.';
      feedback.className   = 'form-feedback error';
    }

    btn.disabled = false;
    btnText.classList.remove('t-hidden');
    btnArrow.classList.remove('t-hidden');
    btnLoading.classList.add('t-hidden');

    if (feedback.className.includes('success')) {
      feedback.style.transition = 'opacity 0.5s';
      setTimeout(() => feedback.style.opacity = '0', 15000);
      setTimeout(() => {
        feedback.className     = 'form-feedback t-hidden';
        feedback.style.opacity = '1';
      }, 15500);
    }
  });
}

/* ============================================================
   HELPERS
   ============================================================ */
async function apiFetch(endpoint) {
  try {
    const res = await fetch(`${API}/${endpoint}`);
    if (!res.ok) throw new Error(`HTTP ${res.status}`);
    return await res.json();
  } catch (err) {
    console.error(`[API] ${endpoint} failed:`, err);
    return null;
  }
}

function setText(id, val) {
  const el = document.getElementById(id);
  if (el) el.textContent = val;
}

function setLink(id, text, href) {
  const el = document.getElementById(id);
  if (!el) return;
  el.textContent = text;
  el.href = href;
}

function sanitize(str) {
  return String(str)
    .replace(/&/g,'&amp;')
    .replace(/</g,'&lt;')
    .replace(/>/g,'&gt;')
    .replace(/"/g,'&quot;');
}

function observeCards(selector) {
  const obs = new IntersectionObserver((entries) => {
    entries.forEach((entry, i) => {
      if (entry.isIntersecting) {
        setTimeout(() => entry.target.classList.add('visible'), i * 80);
        obs.unobserve(entry.target);
      }
    });
  }, { threshold: 0.08 });
  document.querySelectorAll(selector).forEach(el => obs.observe(el));
}