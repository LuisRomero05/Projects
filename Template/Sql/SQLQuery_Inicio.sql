--creacion de Base de Datos
CREATE DATABASE [Template]   
GO 
USE [Template]
GO	
ALTER DATABASE [Template] SET ENABLE_BROKER
GO 
--creacion de esquema
CREATE SCHEMA [Gral]
GO

--creacion de la tabla
CREATE TABLE [Gral].tbAreas(
    [are_Id]              INT IDENTITY(1,1),--id
    [are_Description]     NVARCHAR(MAX),--nombre
    [are_Status]          BIT				NOT NULL,--ESTADO DE LA AREA Activado/desactivado.
	--auditoria
    [are_IdUserCreate]    INT				NULL,--usuario que lo crea
    [are_DateCreate]      DATETIME			NOT NULL,--fecha de creacion
    [are_IdUserModified]  INT				NULL,--usuario que lo modifica
    [are_DateModified]    DATETIME			NULL,--fecha de la modificacion
    CONSTRAINT PK_Gral_tbAreas_are_Id PRIMARY KEY(are_Id)
);