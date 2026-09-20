# Data Model

## Core tables (referenced from source; schema itself lives only in the SQL Server database — no `.sql` script is included, see Known Limitations)

| Table (as referenced in SQL strings in source) | Role |
|---|---|
| `TracNghiem` / `ChiTietTracNghiem` | Question and its answer options, grouped by `TenModun` (subject/module) |
| `Degoc` / `ChitietDegoc` | Root exam template and its question composition |
| `DeThi` | Generated per-candidate exam instance, including its randomized structure (`KetCau_De`) |
| `Thisinh` | Candidate: identity, room, exam-progress state (`Tinhtrang`), submitted answers, score |
| `Kithi` | Exam session: room, subject, template, schedule, status |
| `Account` | Administrator accounts (username, encrypted password) |
| `Monthi` | Subject/module reference list |

## Custom exam-structure encoding

Rather than storing exam structure as normalized rows, the generated exam (`DeThi.KetCau_De` and the equivalent field for root templates) is serialized as a delimited string using custom separators (`#`, `;`) designed specifically for this project. `Globals.GetRandomNumbersString` builds this string when generating a candidate's randomized answer order; the exam-rendering and scoring code (`Globals.DisplayDethiOnRtf`, `FrmBaiThi.TinhDiemThi`) parses it back apart.

This is a functional but non-standard design choice: it works for this application's own read/write paths, but is not a schema-validated or portable format. It is documented here as-is, not modernized.

## Randomization

Two independent randomization steps exist:
1. **Question sampling**: for a given subject/module, a configured number of questions is drawn from the bank (random selection in SQL) when building a root exam template.
2. **Answer-option shuffling**: `Globals.GetRandomNumbers(count)` produces a random permutation of option positions, applied per candidate so that two candidates sitting the same root template see their answer options in a different order.

Both are deterministic-but-randomized (using .NET's `Random`/database-side randomization), not statistical or learned in any sense.

## Candidate identity and state

`Thisinh.Tinhtrang` (candidate status) acts as a simple state machine: not started → in progress → submitted. Login on the client side (`frmLogin.cs`) is by candidate ID (SBD) only, gated by this state rather than by a password.
