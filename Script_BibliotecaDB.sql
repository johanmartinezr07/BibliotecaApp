CREATE DATABASE BibliotecaDB;
GO

USE BibliotecaDB;
GO

-- Eliminar tablas si ya existen para evitar conflictos
IF OBJECT_ID('dbo.Prestamos', 'U') IS NOT NULL DROP TABLE dbo.Prestamos;
IF OBJECT_ID('dbo.Libros', 'U') IS NOT NULL DROP TABLE dbo.Libros;
IF OBJECT_ID('dbo.Usuarios', 'U') IS NOT NULL DROP TABLE dbo.Usuarios;
IF OBJECT_ID('dbo.Editoriales', 'U') IS NOT NULL DROP TABLE dbo.Editoriales;
IF OBJECT_ID('dbo.Autores', 'U') IS NOT NULL DROP TABLE dbo.Autores;
GO

CREATE TABLE Autores (
    IdAutor INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Editoriales (
    IdEditorial INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Pais VARCHAR(100) NULL
);
GO

CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Documento VARCHAR(20) NOT NULL UNIQUE,
    Telefono VARCHAR(20) NULL,
    Correo VARCHAR(100) NULL
);
GO

CREATE TABLE Libros (
    IdLibro INT IDENTITY(1,1) PRIMARY KEY,
    Titulo VARCHAR(150) NOT NULL,
    IdAutor INT NOT NULL,
    IdEditorial INT NOT NULL,
    AnioPublicacion INT NOT NULL DEFAULT 2024,
    Stock INT NOT NULL DEFAULT 10,
    FOREIGN KEY (IdAutor) REFERENCES Autores(IdAutor),
    FOREIGN KEY (IdEditorial) REFERENCES Editoriales(IdEditorial)
);
GO

CREATE TABLE Prestamos (
    IdPrestamo INT IDENTITY(1,1) PRIMARY KEY,
    IdLibro INT NOT NULL,
    IdUsuario INT NOT NULL,
    FechaPrestamo DATETIME NOT NULL DEFAULT GETDATE(),
    FechaDevolucion DATETIME NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Prestado',
    FOREIGN KEY (IdLibro) REFERENCES Libros(IdLibro),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
);
GO