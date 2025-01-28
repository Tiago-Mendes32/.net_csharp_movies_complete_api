# MoviesAPI

MoviesAPI is a RESTful API designed to manage a collection of movies, supporting operations like adding, retrieving, updating, and deleting movie records. This API is built with .NET 7 and employs Entity Framework Core for database interactions. Swagger is integrated for API documentation and testing.

## Features
- **Add Movies**: Create a new movie record with details like title, director, genre, and release date.
- **Retrieve Movies**: Fetch all movies with pagination or retrieve a specific movie by its ID.
- **Update Movies**: Update an entire movie record or partially update specific fields.
- **Delete Movies**: Remove a movie from the database.
- **API Documentation**: Interactive API documentation with Swagger UI.

---

## Technologies Used

### Framework and Language
- **.NET 7**: Provides the foundation for building scalable, high-performance APIs.

### Database and ORM
- **MySQL**: Relational database for storing movie data.
- **Entity Framework Core**: ORM for database interactions with support for migrations and LINQ queries.

### API Documentation
- **Swagger / Swashbuckle**: Generates interactive API documentation and UI.

### Serialization
- **Newtonsoft.Json**: Advanced JSON serialization for PATCH requests and complex JSON structures.

### Dependency Injection and Mapping
- **AutoMapper**: Simplifies object-to-object mapping for DTOs and models.

## Project Structure
- **Controllers**: Contains the `MovieController` for handling HTTP requests.
- **Data**: Includes the database context (`MovieContext`) and configuration.
- **Dtos**: Defines data transfer objects (DTOs) for mapping between models and API inputs/outputs.
- **Models**: Contains the `Movie` model representing the database entity.

---

## Contributing
Feel free to fork this repository and submit pull requests. Suggestions and bug reports are welcome!

---

## License
This project is licensed under the [MIT License](LICENSE).

---

## Contact
For further inquiries or feedback, please contact:
- **Name**: Tiago
- **Email**: tiago.mendes.goes@gmail.com

