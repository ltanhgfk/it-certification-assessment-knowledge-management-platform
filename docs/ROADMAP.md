# Roadmap / Future Research Directions

Everything in this document is **future work — nothing described here is implemented in the current codebase.** This distinction is kept explicit throughout, per this release's positioning as an honest research/portfolio artifact rather than a marketing document.

## Assessment science

- **Item-level analytics**: the current schema stores a candidate's final score but does not retain enough structure to compute per-question difficulty or discrimination indices across candidates. A future iteration could normalize submitted answers into a proper fact table (one row per candidate-question-answer) to enable this.
- **Item Response Theory (IRT)**: with item-level data available, a 1PL/2PL IRT model could estimate question difficulty and candidate ability jointly. Not implemented; would require both a schema change and a modeling component that does not exist today.
- **Computerized Adaptive Testing (CAT)**: selecting each candidate's next question based on a running ability estimate, rather than static random sampling from a fixed pool. This is a substantial architectural change, not a tweak to the existing generator.

## System architecture

- **Service-layer extraction**: business logic currently lives in WinForms code-behind. A service layer (or at minimum, a set of plain data-access/service classes separate from UI event handlers) would make future testing and reuse easier.
- **Configuration externalization**: connection strings and the password-encryption key should move out of source code and into environment-specific configuration/secrets management if this system were ever run against real data again.
- **Schema-validated exam structure**: replacing the current custom delimited-string encoding for exam structure with a validated, versioned format (e.g., JSON with a defined schema).

## Explicitly out of scope for this release

Per this release's audit instructions, none of the above was implemented, and no AI/ML component was added anywhere in this codebase as part of preparing this repository for publication.
