# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build and Run Commands

```bash
# Build the solution
dotnet build RuneKeeperNotes.sln

# Run the application
dotnet run --project RuneKeeperNotes

# Run with hot reload (development)
dotnet watch run --project RuneKeeperNotes

# Restore dependencies
dotnet restore
```

## Server Endpoints

- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Docker: ports 8080/8081

## Architecture

This is an ASP.NET Core 9.0 Web API for note management with optional Elder Futhark rune tagging.

### Project Structure

- **Controllers/**: REST API endpoints (`NotesController.cs` handles `/notes` routes)
- **Models/**: Data models (`Note` record with Id, Title, Content, optional RuneName)
- **Repositories/**: Data access layer using repository pattern
  - `INoteRepository`: Interface for note operations
  - `FileNoteRepository`: JSON file-based implementation (stores in `Data/notes.json`)
- **RuneData.cs**: Static lookup tables for 24 Elder Futhark runes with name/symbol validation

### Key Patterns

- **Dependency Injection**: `INoteRepository` registered as Singleton in `Program.cs`
- **Repository Pattern**: Abstracts data storage, currently file-based but designed for future DB migration
- **Record Types**: `Note` uses C# record for immutable value semantics

### API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /notes | Get all notes |
| GET | /notes/{id} | Get note by ID |
| POST | /notes | Create note (validates RuneName if provided) |
| PUT | /notes/{id} | Update note |
| DELETE | /notes/{id} | Delete note |
| GET | /notes/validate/{rune} | Validate rune name or symbol |

### Rune System

Notes can optionally be tagged with a `RuneName` from the Elder Futhark alphabet. Valid rune names: Fehu, Uruz, Thurisaz, Ansuz, Raidho, Kenaz, Gebo, Wunjo, Hagalaz, Nauthiz, Isa, Jera, Eiwaz, Perthro, Algiz, Sowilo, Tiwaz, Berkano, Ehwaz, Mannaz, Laguz, Ingwaz, Dagaz, Othala.

Runes can be looked up by name (e.g., "Fehu") or Unicode symbol (e.g., "ᚠ").
