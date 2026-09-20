# Public Release Notes

The final public-safety cleanup removed historical environment-specific and licensing artifacts that were not part of the assessment-engine functionality.

- Database credentials and machine-specific paths are sanitized.
- The historical TripleDES encryption key is no longer embedded in source; it is supplied through `MTTEST_ENCRYPTION_KEY`.
- The historical hardcoded `D:\` export path was replaced with a user-selected Save As dialog.
- The historical date-based trial/license gate and misleading error dialog were removed.
- The candidate roster sample and exam-import sample were removed because their real-vs-synthetic status could not be verified.

No AI/ML capability has been added or claimed.
