# Data Privacy and Public Release

This repository is a sanitized public/research release of a legacy IT certification assessment system.

## Removed from the public release

- Production SQL Server credentials and machine-specific server names
- Historical hardcoded password-encryption key
- Developer-machine absolute paths
- Historical license/trial enforcement code and its misleading error dialog
- Real or unverified candidate/exam sample files
- IDE/build artifacts and stale project backups
- Compiled installer/build output

## Configuration

The application uses a placeholder SQL Server connection string in the public source. A local deployment must provide its own database configuration.

The historical password-encryption helper now reads `MTTEST_ENCRYPTION_KEY` from the process environment. The key is deliberately not stored in source control.

## Sample data policy

The historical candidate-roster spreadsheet and exam-import Word document were removed because their real-vs-synthetic status could not be verified and their contents were not appropriate to publish as demonstration data.

No production candidate records or real certification examination datasets are intentionally included in this release.

## Legacy security limitations

The application retains legacy cryptographic behavior for compatibility with its historical account model: TripleDES in ECB mode with an MD5-derived key. This is documented as historical behavior, not recommended modern password storage.

The project also uses direct ADO.NET database access and does not provide a modern secrets-management layer. These are documented limitations of the historical system.

## Public-release principle

The repository preserves software architecture and engineering logic while excluding operational credentials, private data, and environment-specific artifacts.
