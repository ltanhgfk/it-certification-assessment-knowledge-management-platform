# Architecture

## Verified structure

```
MTTestServer (WinForms, namespace ThiTracNghemUDCNTT)   MTTestClient (WinForms)
             │                                                      │
             └──────────────────── ADO.NET (SqlConnection) ─────────┘
                                        │
                                  SQL Server ("MTTest" database)
```

Both applications hold their own `static class Database` with a hardcoded `ConnectionString` field and helper methods (`GetConnection`, `Fill`, `ExecuteScalar`, `ExecuteNonQuery`) built directly on `System.Data.SqlClient`. There is no ORM, no repository abstraction, and no service layer — controllers here are WinForms code-behind classes calling `Database.*` helpers directly from UI event handlers.

## What this is not

- **Not distributed / not microservice**: the two applications never talk to each other directly. They cooperate only by reading and writing the same SQL Server tables. There is no message queue, no RPC, no HTTP API.
- **Not AI/ML**: exam generation uses `ORDER BY NEWID()`-style random sampling and a Fisher–Yates-style shuffle for answer-option order (see `Globals.GetRandomNumbers` equivalent logic referenced from `formDethi.cs`). No statistical or learned model is involved anywhere.

## Question bank ingestion path

`Globals.cs` (present in both projects, with the server's copy being the one actually used for authoring) implements `BuilderCode`, which:
1. Opens a `.docx` file via `Microsoft.Office.Interop.Word`.
2. Walks paragraphs looking for a small custom markup: `<mod>` marks a subject/module header, `<11>` / `<00>` mark question text, `<**>` marks the correct answer among the options that follow.
3. Extracts inline images from the document, converts them to Base64, and embeds them in the stored question text using `##BegImg##...##EndImg##` markers.
4. Writes the parsed questions/options into the database.

This is a real, if unconventional, content-ingestion pipeline — it is not a generic document parser, but a fixed, project-specific format the developer designed to make the Word-authoring workflow possible.

## Exam generation path

`formDethi.cs`:
1. For a given subject/module, samples a configured number of questions from the bank (random selection via SQL).
2. Assembles the sample into a reusable "root" exam template.
3. For each candidate, generates an individual exam instance from the root template, independently randomizing the order of answer options.
4. Persists the generated structure as a delimited string (see `docs/DATA_MODEL.md`) rather than as normalized rows.

## Scoring path

`FrmBaiThi.TinhDiemThi()` (client): parses the candidate's submitted answer string and the generated exam's answer key (both delimited strings), compares them position-by-position, and computes `10 * correct / total`, rounded to one decimal place. There is no per-question weighting, no partial credit, and no time-based scoring.

## Build/runtime dependencies not managed by NuGet

Two binaries are referenced via a `bin\Debug\...` hint path rather than a package reference:
- `Microsoft.Office.Interop.Word.dll`
- `RTFLib.dll`

These were present as vendored files in the original project's `bin/Debug` folder. Because this release removes `bin/`/`obj/` as build output (see `.gitignore`), building this project from a clean checkout requires manually sourcing these two binaries and placing them where the `HintPath` expects them (or updating the reference to a locally available copy). This is documented rather than silently worked around.
