#  Sistema de Gestión de Clientes

Aplicación web para administrar clientes, desarrollada con Angular y .NET (Visual Basic).

---

##  Tecnologías

- Angular
- .NET (Visual Basic)
- SQL Server
---

## Requisitos

Instalar previamente:

- Node.js
- Visual Studio
- SQL Server

---

##  Instalación

###  1. Clonar repositorio
```bash
git clone https://github.com/TU_USUARIO/TU_REPO.git

2. Abrir en Visual Studio

Abrir el archivo:

TuProyecto.sln
3. Configurar base de datos

En:

appsettings.json

Cambiar:

"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=TU_BD;User Id=usuario;Password=1234;"
}


4. Restaurar dependencias

Visual Studio normalmente lo hace solo, pero si no:

Build → Restore NuGet Packages
5. Migraciones


Update-Database
6. Ejecutar
F5

o

dotnet run



