# Product Service

Microservicio de **Cacha-el-precio.com** encargado de almacenar, consultar y exponer los productos recopilados por el servicio de scraping.

El proyecto se encuentra en desarrollo y utiliza ASP.NET Core, Entity Framework Core y SQLite. La API incluye versionado por encabezado y documentación OpenAPI mediante Scalar.

## Tecnologías

- .NET 10 y ASP.NET Core
- Entity Framework Core 10
- SQLite
- ASP.NET API Versioning
- OpenAPI y Scalar

## Requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0)
- Git
- La herramienta `dotnet-ef` en la misma versión principal que Entity Framework Core

Puedes comprobar la instalación con:

```bash
dotnet --version
git --version
dotnet ef --version
```

Si `dotnet ef` no está disponible:

```bash
dotnet tool install --global dotnet-ef --version 10.0.11
```

Si ya está instalado, puedes actualizarlo con:

```bash
dotnet tool update --global dotnet-ef --version 10.0.11
```

## Instalación

```bash
git clone https://github.com/OrionTheProgrammer/product-service.git
cd product-service
dotnet restore
dotnet ef database update
dotnet run --launch-profile http
```

La aplicación utiliza por defecto:

```text
http://localhost:8081
```

Cuando el entorno es `Development`, la especificación OpenAPI se publica en `/openapi/v1.json` y Scalar en `/scalar/v1`.

## Configuración de la base de datos

La conexión se encuentra en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Sqlite": "Data Source=product.db"
  }
}
```

Para utilizar otra ubicación sin modificar el archivo:

```bash
ConnectionStrings__Sqlite="Data Source=/ruta/product.db" dotnet run
```

### Comandos de migraciones

Crear una migración:

```bash
dotnet ef migrations add NombreDeLaMigracion
```

Aplicar las migraciones pendientes:

```bash
dotnet ef database update
```

Listar las migraciones:

```bash
dotnet ef migrations list
```

Eliminar la última migración que todavía no haya sido aplicada:

```bash
dotnet ef migrations remove
```

Comprobar que el modelo y la última migración estén sincronizados:

```bash
dotnet ef migrations has-pending-model-changes
```

## Modelo de producto

Cada producto contiene:

- nombre y marca;
- categoría normalizada junto con su valor original;
- precio entero;
- disponibilidad para las tallas `XS`, `S`, `M`, `L`, `XL` y `XXL`.

Las tallas se modelan como un objeto dependiente de `ProductEntity` y se almacenan en columnas booleanas de la tabla `Products`.

Ejemplo del cuerpo esperado para crear un producto:

```json
{
  "name": "Zapatilla urbana",
  "brand": "Ejemplo",
  "category": "zapatillas",
  "price": 59990,
  "sizes": {
    "xs": false,
    "s": true,
    "m": true,
    "l": true,
    "xl": false,
    "xxl": false
  }
}
```

La versión de la API se solicita mediante el encabezado:

```http
Version: 1.0
```

## Estructura del proyecto

```text
Controllers/   Endpoints HTTP
Data/          DbContext y configuración de Entity Framework Core
Exceptions/    Excepciones del dominio
Migrations/    Historial y snapshot de la base de datos
Models/        Entidades, DTOs, objetos de dominio y mappers
Repository/    Contratos y acceso a SQLite
Service/       Casos de uso y transformación de respuestas
```

## Verificación durante el desarrollo

```bash
dotnet build
dotnet format --verify-no-changes
dotnet ef dbcontext info
dotnet ef migrations has-pending-model-changes
```

## Estado actual

El modelo de Entity Framework Core y la migración inicial son válidos. Antes de considerar completo el CRUD quedan pendientes:

- corregir y diferenciar las rutas de consulta por ID, categoría, precio y talla;
- exponer en el controlador la búsqueda por talla;
- completar la eliminación efectiva del producto;
- corregir la actualización para persistir los nuevos valores;
- incorporar pruebas automatizadas.
