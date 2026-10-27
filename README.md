# Web Design Labs: Jobs Site, Timer App, Timer on Vue + ASP.NET Core

Three web labs, each a step up in tooling: a static Bootstrap site, a vanilla-JS single-page timer with login, and the same timer idea rebuilt on a Vue 2 + ASP.NET Core template.

Year: 2021 · Stack: HTML/CSS/Bootstrap, jQuery, ES modules, Web Workers, Vue 2 + Vuetify, ASP.NET Core 3.1, NHibernate, MediatR · Context: university coursework (KPI), web design course; lab 3 is built on a company project scaffold (`Elfo.Round` / `@round` packages)

## What's inside

- `lab1/` - a multi-page Bootstrap site about Steve Jobs: `index.html` (header, "Facts about myself", blog, "Get in Touch"), `about.html`, `mail.html`, `profile.html`, shared `header.html` / `footer.html`, custom `styles/styles.css`, smooth-scroll navigation in `js/main.js` (jQuery). `main.png`, `login.png`, `profile.png` are screenshots.
- `lab2/` - "Timer", a client-only app in vanilla JavaScript: register and log in (`signup.html`, `index.html`, `js/controller/register.js`, `login.js`), a work timer with start / pause / stop and an activity title (`timer.html`), a session list rendered from `localStorage`, and a profile page. Code is split MVC-style into `js/model`, `js/view`, `js/controller`.
- `lab3/` - the timer again as a full-stack app. `lab3/UI/` is a Vue 2 + Vuetify + TypeScript front end (the timer page is `UI/src/views/pdfGallery/PdfGallery.vue`). `lab3/Backend/` is an ASP.NET Core 3.1 solution (`LAB3.sln`) with `Api`, `Domain`, `Infrastructure` (NHibernate mappings) and test projects; the timer endpoints are in `Api/PdfGallery/TimerController.cs`.

## Interesting bits

- `lab2/js/controller/taskForWebWorker.js`: per-day totals of tracked time are computed in a Web Worker (`timer.js` posts the session list and a list of days and gets back formatted `h:mm:ss` sums), keeping the UI thread free.
- `lab2/js/model/Timer.js`: the timer stores wall-clock start and end `Date` values and derives duration from them, not from counted ticks.
- `lab3/Backend/Api/PdfGallery/TimerController.cs`: `saveTime` / `getTimer` keep timer records in an injected in-memory list; the surrounding user / group management (CQRS commands and queries under `Api/SystemManagement/`, domain tests in `Test/Domain.Test`) comes from the scaffold, not from the lab.
- `lab3/UI/src/views/pdfGallery/`: the folder name "pdfGallery" is a leftover of the scaffold's sample feature, which was reused for the timer view.

## Run

Verified here: `lab1/` and `lab2/` are static and are served fine with a local server (HTTP 200 for the pages and scripts; not clicked through in a browser):

```
cd lab2 && python3 -m http.server 8000     # open http://localhost:8000/index.html
```

`lab2` must be served from its own root (it uses absolute links such as `/profile.html`). `lab1` can be opened directly or served the same way.

`lab3` was not re-tested: it needs the proprietary `Elfo.Round.*` NuGet packages, the `@round/*` npm packages and SQL Server, which are not public. Based on the project files: `dotnet run` in `lab3/Backend/Api`, `npm install && npm run serve` in `lab3/UI`.

## Reports

Lab reports are Google Docs, linked from `lab1/README.md`, `lab2/README.md` and `lab3/Readme.md`.

## Note

Written in 2021, before AI coding assistants. Kept as-is for history.
