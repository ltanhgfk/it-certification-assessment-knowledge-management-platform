# IT Certification Assessment & Knowledge Management Platform

A C# / Windows Forms desktop system for managing an IT-certification question bank, generating randomized per-candidate exams, running proctored exam sessions, and scoring results — published here as a research/portfolio artifact.

This is a legacy two-application system (an administrator/proctor app and a candidate exam-taking app) built directly against a shared SQL Server database. It is not maintained as a commercial product, is not being modernized as part of this release, and — stated explicitly — **contains no machine learning or AI of any kind**. Where a future direction is mentioned, it is clearly future work, not an existing capability.

## Overview

Certification centers running IT skills exams need to: maintain a bank of questions organized by subject, generate exam papers from that bank, hand out a different (but equivalent) exam to each candidate to reduce copying, run the exam session, and score it automatically. This system implements that workflow as two cooperating Windows Forms applications sharing one SQL Server database.

## System Purpose

- **MTTestServer** (`ThiTracNghemUDCNTT`): the administrator/proctor application — manages the question bank, generates exam templates and per-candidate exam variants, manages candidates and exam sessions, and prints reports.
- **MTTestClient**: the candidate-facing application — a candidate logs in with their candidate ID, takes the randomized exam, and submits it for automatic scoring.

## Main Components (verified against source)

| Component | File(s) | Responsibility |
|---|---|---|
| Question bank import (Word) | `Globals.cs` (`BuilderCode`, both projects) | Parses questions/answers/images out of a Word document using a lightweight custom markup (`<mod>`, `<11>`, `<00>`, `<**>`) via `Microsoft.Office.Interop.Word` |
| Exam template & variant generation | `formDethi.cs` | Samples questions from the bank per subject/module, builds a reusable exam template, and generates per-candidate exam instances with independently randomized answer-option order |
| Candidate/session management | `formThisinh.cs` | Candidate roster (including bulk import from Excel via OLE DB), room/session assignment, exam-progress state |
| Exam taking (client) | `FrmBaiThi.cs`, `FrmBaiThiOutside.cs` | Renders the candidate's exam, collects answers, submits for scoring |
| Scoring | `FrmBaiThi.cs` (`TinhDiemThi`) | Parses submitted answers against the generated exam key and computes a 0–10 score |
| Admin/account management | `formAccount.cs`, `Login.cs`, `LoginDialog.cs` | Administrator accounts, password-based login |
| Reporting | `crpForm*.cs`, `CrystalReportIn*.cs` | Crystal Reports-based printouts: score lists, candidate answer sheets, symbol sheets |

## System Workflow

```
1. Question authoring: questions are written in a Word document using the
   custom markup, then imported into the database via BuilderCode (Globals.cs).
2. Exam template generation (formDethi.cs): questions are sampled per subject/
   module from the bank and assembled into a reusable "root" exam.
3. Per-candidate exam generation: from the root exam, individual exam
   instances are generated with independently randomized answer-option order
   per candidate.
4. Candidate/session setup (formThisinh.cs): candidates are registered
   (individually or via Excel bulk import) and assigned to a room/session.
5. Exam taking (MTTestClient): a candidate logs in by candidate ID (no
   password — access is gated by session/exam state) and takes their exam.
6. Scoring (FrmBaiThi.TinhDiemThi): submitted answers are parsed and compared
   against the generated exam key; a 0–10 score is computed.
7. Reporting (MTTestServer): score lists and candidate answer sheets are
   printed via Crystal Reports.
```

## Architecture

```
MTTestServer (WinForms, admin/proctor)      MTTestClient (WinForms, candidate)
            │                                            │
            └──────────────── direct ADO.NET ────────────┘
                                   │
                             SQL Server
                          (shared database)
```

This is a **two-tier, shared-database architecture** — both applications connect independently and directly to the same SQL Server database via hand-written ADO.NET (`SqlConnection`/`SqlCommand`), using a static `Database` helper class in each project. There is:
- no network protocol between the two applications beyond the database connection itself,
- no service/API layer,
- no distributed or microservice design of any kind.

This is stated explicitly so the architecture is not mistaken for something more decoupled than it is. Full detail in [`docs/ARCHITECTURE.md`](docs/ARCHITECTURE.md).

## Roles

Two roles exist in the system, not a general-purpose role framework:
- **Administrator/proctor** (`MTTestServer`): username + password login (`Login.cs`), password encrypted with a symmetric cipher (see [Known Limitations](#known-limitations)).
- **Candidate** (`MTTestClient`): identified by candidate ID (SBD) only, gated by the candidate's exam-session state (not-started / in-progress / submitted) rather than a password.

## Data Model (core concepts)

| Concept | Role |
|---|---|
| Question bank | Questions grouped by subject/module, each with multiple answer options and one marked correct |
| Root exam template | A reusable composition of sampled questions for a subject/module |
| Per-candidate exam instance | Generated from a root template, with independently randomized answer-option order |
| Candidate | Identity, room/session assignment, exam-progress state, submitted answers, score |
| Exam session | Room, subject, template, schedule, status |

Full detail, including the custom text-encoding scheme used for exam structure, in [`docs/DATA_MODEL.md`](docs/DATA_MODEL.md).

## What this system is — and is not

Stated plainly, per this release's positioning:

- ✅ It is an **assessment engine and knowledge-management system**: a structured question bank, a template/variant generation algorithm, session/candidate management, and automated scoring.
- ❌ It is **not an AI system**: there is no machine learning, no statistical model, no learning algorithm anywhere in the codebase — only deterministic sampling and shuffling.
- ❌ It is **not a distributed or microservice architecture**: it is a two-tier desktop system sharing one database, with no service layer.
- ❌ It does **not** implement adaptive testing or Item Response Theory. These are listed only as possible future research directions (see below), never as existing capabilities.

## Known Limitations

- No transactional integrity around multi-step operations (e.g., generating an exam template performs multiple sequential inserts without an explicit database transaction).
- No role-based access control beyond the single administrator account type — no granular permissions.
- Exam structure is serialized into a custom delimited string format rather than normalized rows or a schema-validated format.
- Administrator password encryption uses the historical symmetric-cipher implementation, but its key is no longer embedded in source. The public release reads `MTTEST_ENCRYPTION_KEY` from the process environment. This is documented as legacy behavior, not a modern password-storage recommendation.
- No automated tests exist.
- The exam-export feature (`formDethi.cs`, "Export to Word") uses a user-selected save location through `SaveFileDialog`.

## Future Research Directions (not implemented — explicitly future work)

- **Item Response Theory / psychometric calibration**: modeling per-question difficulty and discrimination from historical results, which the schema does not currently capture at the granularity needed.
- **Adaptive/computerized adaptive testing (CAT)**: selecting each candidate's next question based on their running ability estimate, instead of static random sampling.
- **Item-level analytics**: difficulty/discrimination indices computed from aggregated historical answer data.
- **Service-layer refactor**: extracting business logic out of WinForms code-behind and introducing a proper service/API boundary between the two applications and the database.

None of the above exists in the current codebase; they are documented here only as directions a future iteration could take.

## Repository Structure

```
.
├── README.md
├── .gitignore
├── docs/
│   ├── ARCHITECTURE.md
│   ├── DATA_MODEL.md
│   ├── DATA_PRIVACY.md
│   └── ROADMAP.md
├── MTTestServer/            (admin/proctor application, namespace ThiTracNghemUDCNTT)
│   ├── formDethi.cs         (exam template & per-candidate variant generation)
│   ├── formThisinh.cs       (candidate/session management)
│   ├── formAccount.cs, Login.cs, LoginDialog*.cs  (admin accounts/auth)
│   ├── Globals.cs           (Word-based question bank import, RTF rendering helpers)
│   ├── Database.cs          (ADO.NET data access + password encryption helper)
│   ├── crpForm*.cs, CrystalReportIn*.cs, ds*.cs  (Crystal Reports printouts)
│   ├── app.config.example   (sanitized connection-string template)
│   └── MTTestServer.csproj
└── MTTestClient/
    └── MTTestClient/
        ├── FrmBaiThi.cs, FrmBaiThiOutside.cs  (exam taking + scoring)
        ├── frmLogin.cs      (candidate login by ID)
        ├── Globals.cs       (shared parsing/rendering helpers)
        ├── Database.cs      (ADO.NET data access)
        └── MTTestClient.csproj
```

## Technology Stack

C# · Windows Forms · .NET Framework 4.0 · ADO.NET (raw `SqlCommand`/`SqlDataAdapter`, no ORM) · SQL Server · Crystal Reports for Visual Studio · Microsoft Office Interop (Word import, Excel candidate import via OLE DB) · a small vendored RTF-building library (`RTFLib.dll`).

## Configuration

Both applications previously contained hardcoded, real SQL Server credentials and a machine-specific server name; these have been replaced with placeholders as part of this public release (see `docs/DATA_PRIVACY.md`). Before running locally:
- **Server**: edit the `ConnectionString` placeholder in `MTTestServer/Database.cs`, and/or copy `MTTestServer/app.config.example` to `app.config` and fill in your own SQL Server details (note: the `app.config` connection string is not currently read by any code path — the application uses the hardcoded field in `Database.cs` — this is documented as-is, see `docs/DATA_PRIVACY.md`).
- **Client**: edit the `ConnectionString` placeholder in `MTTestClient/MTTestClient/Database.cs`.

## Build Notes

Both projects target **.NET Framework 4.0**. Building requires Visual Studio with the .NET Framework 4.0 targeting pack, and two dependencies that are **not** restorable via NuGet:

- `Microsoft.Office.Interop.Word.dll` — referenced via a `bin\Debug\...` hint path in both `.csproj` files. This is a real build-time dependency (from a Microsoft Office / PIA install), not a regenerable build artifact; it must be placed manually before building, since this release removed `bin/`/`obj/` as build output.
- `RTFLib.dll` — same situation: vendored directly under `bin\Debug\...` rather than distributed via NuGet.
- `Microsoft.VisualBasic.PowerPacks.Vs` (server only) — this one **is** NuGet-restorable via `packages.config`.

**Build was not verified as part of this release.** The environment used to prepare this repository had no Windows/MSBuild/.NET Framework tooling available, and Crystal Reports for Visual Studio (a separate, non-NuGet installable component) is required to build the server project. See [`docs/ARCHITECTURE.md`](docs/ARCHITECTURE.md) for what was and wasn't checked.

## Public Release / Data Policy

Real database credentials, machine-specific server names, unverified candidate/exam sample files, developer-machine artifacts, and the historical license/trial gate were removed from the public release.
