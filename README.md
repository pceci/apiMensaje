Pasos para crear un proyecto desde cero

## dotnet command

Ejemplo el proyecto apiMensaje
En el directorio donde se va a encontrar el proyecto, hay que ejecutar:

### `dotnet new apiMensaje`

--

para agregar archivo de git ignore para proyecto core net

### `dotnet new gitignore`

--

para agregar la conexión a ef core, sin necesidad de crear la tabla

### `dotnet add package Microsoft.EntityFrameworkCore.SqlServer`

--

se necesita esto para crear las clases BD a partir clases C#

### `dotnet add package Microsoft.EntityFrameworkCore.Design`

--

luego hacer: crear clases en la carpeta Migrations

### `dotnet ef migrations add InitialCreate`

luego hacer: correr esas clases para armar las tablas en la BD

### `dotnet ef database update`

--

para agregar seguridad a través de toquen Jwt

### `dotnet add package System.IdentityModel.Tokens.Jwt`
