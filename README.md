# TodoAPI - RESTful API for Managing Notes

TodoAPI is a RESTful API built using ASP.NET Core to manage a collection of notes. It includes functionalities to create, retrieve, update, and delete notes.

## Features

### Endpoints

#### Notes Controller
The `NotesController` manages the CRUD operations for the `Note` model.

- **GET `/notes`**: Retrieves all notes or filters notes based on completion status.
- **GET `/notes/{id}`**: Retrieves a specific note by its ID.
- **GET `/remaining`**: Retrieves the count of remaining incomplete notes.
- **PUT `/notes/{id}`**: Updates a specific note.
- **POST `/notes`**: Creates a new note.
- **POST `/clear-completed`**: Deletes all completed notes.
- **DELETE `/notes/{id}`**: Deletes a specific note.

### Database Initialization

The `DbInitializer` class initializes the database with sample note data if it's empty.

### Models

#### Note
The `Note` model represents a single note with the following properties:
- `ID`: Unique identifier for the note.
- `Text`: The text content of the note.
- `IsDone`: Indicates whether the note is completed or not.

## Project Structure

### Startup.cs
The `Startup` class configures services, middleware, and the HTTP request pipeline.

### Program.cs
The `Program` class contains the application entry point and database initialization logic.

### Data
Contains the `TodoAPIContext` for database connectivity and the `DbInitializer` to seed initial data.

### Controllers
The `NotesController` manages HTTP requests and handles note-related operations.

### Models
Contains the `Note` model class representing the structure of a note.

## Dependencies

- **Entity Framework Core**: Used for ORM and database management.
- **Swagger**: Integrated for API documentation and testing.
- **Microsoft.Extensions.DependencyInjection**: Dependency injection for services.
- **Microsoft.Extensions.Logging**: Logging framework for the application.

## Usage

### Running the Application
Ensure you have the necessary configurations set in `appsettings.json` and run the application.

### Testing Endpoints
Explore and test the available endpoints using Swagger UI.

## Deployment
The API can be hosted on various platforms, and configurations can be adjusted as per deployment requirements.
