/*Sección #1*/
CREATE PROCEDURE UDP_Areas_REGISTER
--========================================================
--Author:       Luis Romero
--Create date:  26/07/2022
--Description:  Procedimiento que elimina registros
--              de la tabla segun el id.
--========================================================
    @are_Description     NVARCHAR(100),
    @are_IdUserCreate    INT
AS
BEGIN
INSERT INTO [Gral].[tbAreas]
           ([are_Description]
           ,[are_Status]
           ,[are_IdUserCreate]
           ,[are_DateCreate]
           ,[are_IdUserModified]
           ,[are_DateModified])
     VALUES
             (@are_Description,1,@are_IdUserCreate,GETDATE(),NULL,NULL)

    SELECT @@IDENTITY
END
GO

/*Sección #2*/
CREATE PROCEDURE UDP_Areas_UPDATE
--========================================================
--Author:       Luis Romero
--Create date:  26/07/2022
--Description:  Procedimiento que actualiza registros
--              de la tabla segun el id.
--========================================================
    @Id                  INT,
    @are_Description     NVARCHAR(100),
    @are_IdUserModified      INT

AS
BEGIN
    UPDATE [Gral].[tbAreas]
    SET
            [are_Description]=@are_Description
           ,[are_IdUserModified]=@are_IdUserModified
           ,[are_DateModified]=GETDATE()
    WHERE are_Id=@Id

    SELECT @@IDENTITY
END
GO

/*Sección #3*/
CREATE PROCEDURE UDP_Areas_DELETE
--========================================================
--Author:       Luis Romero
--Create date:  26/07/2022
--Description:  Procedimiento que elimina registros
--              de la tabla segun el id.
--========================================================
    @Id						 INT,
	@are_IdUserModified	 INT
AS
BEGIN
	  UPDATE [Gral].[tbAreas]
	  SET	are_Status = 0,
			are_IdUserModified=@are_IdUserModified,
			are_DateModified=GETDATE()
      WHERE are_Id=@Id
			
    SELECT @@IDENTITY
END
GO

