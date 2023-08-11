USE [LOGISTIC_SMART_AHM]
GO	
--==================PROCEDIMIENTOS ALMACENADOS==================



/*Sección #42*/
CREATE PROCEDURE Acce.UDP_tbComponents_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Procedimiento que inserta nuevos
--				componentes al sistema.
--========================================================
		@com_Description  NVARCHAR(50),		
		@com_Status		  BIT					
AS
    BEGIN
        INSERT INTO Acce.tbComponents
        (
			[com_Description],
			[com_Status]
		)
        VALUES
        (
			@com_Description,
			@com_Status	
		)
		SELECT SCOPE_IDENTITY() AS [com_Id];
END
GO

/*Sección #44*/
CREATE PROCEDURE Acce.UDP_tbComponents_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Procedimiento que modifica la tabla
--				componentes al sistema.
--========================================================
		@com_Id				INT,
		@com_Description	NVARCHAR(50),		
		@com_Status			BIT					
AS
    BEGIN
         UPDATE Acce.tbComponents
         SET      [com_Description]   = @com_Description,
				  [com_Status]        = @com_Status
               
         WHERE	 com_Id = @com_Id
END
GO

/*Sección #45*/
CREATE PROCEDURE Acce.UDP_tbComponents_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Procedimiento que elimina datos de la
--				tabla componentes al sistema.
--========================================================
		@com_Id				INT		
AS
    BEGIN
        UPDATE [Acce].[tbComponents]
        SET    [com_Status] = 0
        WHERE  com_Id = @com_Id
END
GO




/*Sección #46*/
CREATE PROCEDURE Acce.UDP_tbModules_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Procedimiento que inserta nuevos
--				modulos al sistema.
--========================================================
		@mod_Description  NVARCHAR(50),	
		@com_Id			  INT,
		@mod_Status		  BIT					
AS
    BEGIN
        INSERT INTO [Acce].[tbModules]
        (
			[com_Id],
			[mod_Description],
			[mod_Status]
		)
        VALUES
        (
			@com_Id	,
			@mod_Description,
			@mod_Status		
		)
		SELECT SCOPE_IDENTITY() AS [mod_Id];
END
GO


/*Sección #47*/
CREATE PROCEDURE Acce.UDP_tbModules_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Procedimiento que modifica la tabla
--				modulos del sistema.
--========================================================
		@mod_Id		  INT,
		@mod_Description  NVARCHAR(50),	
		@com_Id			  INT,
		@mod_Status		  BIT				
AS
    BEGIN
         UPDATE [Acce].[tbModules]
        SET      
				 [com_Id]           = @mod_Description,
				 [mod_Description]	= @com_Id,			
				 [mod_Status]	    = @mod_Status		

               
        WHERE  @mod_Id = @mod_Id
END
GO


/*Sección #48*/
CREATE PROCEDURE Acce.UDP_tbModules_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Procedimiento que elimina datos de la
--				tabla modulos del sistema.
--========================================================
		@mod_Id				INT				
AS
    BEGIN
        UPDATE [Acce].[tbModules]
        SET    [mod_Status] = 0
        WHERE  mod_Id = @mod_Id
END
GO





/*Sección #49*/
CREATE PROCEDURE Acce.UDP_tbRoles_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que inserta registros
--              de la tabla de roles.
--========================================================
	@Description NVARCHAR(100),
	@IdUserCreate INT	
AS
	BEGIN
	INSERT INTO [Acce].[tbRoles]
           ([rol_Description]
           ,[rol_Status]
           ,[rol_IdUserCreate]
           ,[rol_DateCreate]
           ,[rol_IdUsermodified]
           ,[rol_DateModified])
     VALUES
           (@Description,
		   1,
		   @IdUserCreate,
		   GETDATE(),
		   null,
		   null)
		   SELECT @@IDENTITY
	END

GO


/*Sección #50*/
CREATE PROCEDURE Acce.UDP_tbRoles_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que modifica registros
--              de la tabla de roles.
--========================================================
@rol_Id INT,
@rol_Description NVARCHAR(100),
@rol_IdUserModified INT
AS
    BEGIN
    UPDATE [Acce].[tbRoles]
    SET [rol_Description] = @rol_Description,
       [rol_IdUserModified] = @rol_IdUserModified,
       [rol_DateModified] = GETDATE()
   WHERE rol_Id = @rol_Id
		
	SELECT @@IDENTITY
 END

GO


/*Sección #51*/
CREATE PROCEDURE Acce.UDP_tbRoles_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina registros
--              de la tabla roles.
--========================================================
	@rol_Id					INT,
	@rol_IdUserModified		INT
AS
	BEGIN
		UPDATE Acce.tbRoles
		SET rol_Status = 0, rol_IdUserModified = @rol_IdUserModified
		WHERE rol_Id = @rol_Id
END

GO


/*Sección #52*/
CREATE PROCEDURE Gral.UDP_tbCountries_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento con el cual se insertan 
--              datos en la tabla Paises.
--========================================================
	@cou_Description		 NVARCHAR(100),
	@cou_IdUserCreate		 INT
AS
BEGIN
	INSERT [Gral].tbCountries
	(
		[cou_Description],
		cou_Status, 
		cou_IdUserCreate,
		cou_DateCreate
	)
	VALUES 
	(
		@cou_Description,
		1,
		@cou_IdUserCreate, 
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
END
GO

/*Sección #53*/
CREATE PROCEDURE Gral.UDP_tbCountries_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que actualiza los datos
--              de un registro en la tabla Paises.
--========================================================
	@cou_Id				INT,  
	@cou_Description	NVARCHAR(100),
	@cou_IdUserModified	INT
AS
BEGIN
	UPDATE  [Gral].tbCountries
	SET 	cou_Description  	= @cou_Description,
			cou_Status			=		1, 
			cou_IdUserModified  = @cou_IdUserModified,
			cou_DateModified	= GETDATE()
	WHERE	cou_Id		        = @cou_Id
END
 GO  


 /*Sección #54*/
CREATE PROCEDURE Gral.UDP_tbCountries_DELETE
 --========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina registros
--              de la tabla Paises segun el id.
--========================================================
	@cou_Id	            INT,
	@cou_IdUserModified	INT
AS
BEGIN
	UPDATE	[Gral].tbCountries
	SET		cou_Status			 = 0,
			cou_IdUserModified   = @cou_IdUserModified,
			cou_DateModified	 = GETDATE()
	WHERE	cou_Id			     = @cou_Id
END
GO	


/*Sección #55*/
CREATE PROCEDURE Gral.UDP_tbDepartamentos_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento con el cual se insertan 
--              datos en la tabla departamentos.
--========================================================
	@dep_Code			 VARCHAR(2), 
	@dep_Description	 NVARCHAR(100),
	@cou_Id				 INT,
	@dep_UsuarioCrea	 INT
AS
BEGIN
	INSERT [Gral].[tbDepartments]
	(
		[dep_Code],
		[dep_Description],
		cou_Id,
		dep_Status, 
		dep_IdUserCreate,
		dep_DateCreate
	)
	VALUES 
	(
		@dep_Code, 
		@dep_Description,
		@cou_Id,
		1,
		@dep_UsuarioCrea, 
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
END
GO

/*Sección #56*/
CREATE PROCEDURE Gral.UDP_tbDepartamentos_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que actualiza los datos
--              de un registro en la tabla departamentos.
--========================================================
	@dep_Id				INT, 
	@dep_Code			VARCHAR(2), 
	@dep_Description	NVARCHAR(100),
	@cou_Id				 INT,
	@dep_IdUserModified	INT
AS
BEGIN
	UPDATE  [Gral].[tbDepartments]
	SET 	[dep_Code]			= @dep_Code,
			[dep_Description]	= @dep_Description,
			cou_Id				= @cou_Id	,
			dep_Status	=		1, 
			dep_IdUserModified  = @dep_IdUserModified,
			dep_DateModified	= GETDATE()
	WHERE	[dep_Id]		    = @dep_Id
END
 GO  


 /*Sección #57*/
CREATE PROCEDURE Gral.UDP_tbDepartamentos_DELETE
 --========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina registros
--              de la tabla departamentos segun el id.
--========================================================
	@dep_Id	            INT,
	@dep_IdUserModified	INT
AS
BEGIN
	UPDATE	[Gral].[tbDepartments]
	SET		dep_Status			 = 0,
			[dep_IdUserModified] = @dep_IdUserModified,
			[dep_DateModified]	 = GETDATE()
	WHERE	[dep_Id]			 = @dep_Id
END
GO	


/*Sección #58*/
CREATE PROCEDURE Gral.UDP_tbMunicipalities_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento con el cual se insertan 
--              datos en la tabla municipios.
--========================================================
	@mun_Code        	VARCHAR(4),
	@mun_Description	NVARCHAR(100),
	@dep_Id				INT,
	@mun_UsuarioCrea	INT
AS
BEGIN
	INSERT INTO [Gral].[tbMunicipalities]	
	( 
	   [mun_Code],
	   [mun_Description], 
	   [dep_Id],
	   mun_Status,
	   mun_IdUserCreate, 
       mun_DateCreate
	 )

	VALUES 
	(

	    @mun_Code,
		@mun_Description,
		@dep_Id,  
		1,
		@mun_UsuarioCrea,  
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
END
GO


/*Sección #59*/
CREATE PROCEDURE Gral.UDP_tbMunicipalities_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que  actualiza los datos
--              de un registro en la tabla municipios.
--========================================================
	@mun_Id				INT,
	@mun_Code			VARCHAR(4),
	@mun_Descripcion	NVARCHAR(100),
	@dep_Id				INT,
	@mun_UsuarioModifica INT
AS
BEGIN
	UPDATE	[Gral].[tbMunicipalities]
	SET		[mun_Code] = @mun_Code,
			[mun_Description] = @mun_Descripcion,
			[dep_Id] = @dep_Id,
			mun_IdUserModified = @mun_UsuarioModifica,
			mun_DateModified = GETDATE()
	WHERE	[mun_Id] = @mun_Id
END
GO


/*Sección #60*/
CREATE PROCEDURE Gral.UDP_tbMunicipalities_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina registros
--              de la tabla segun el id.
--========================================================
	@mun_Id INT,
	@mun_IdUserModified	INT
AS
BEGIN
	UPDATE	[Gral].[tbMunicipalities]
	SET		mun_Status =			0,
			mun_IdUserModified	= 	@mun_IdUserModified,
			mun_DateModified = GETDATE()
	WHERE	[mun_Id] =				@mun_Id
END
GO



/*Sección #61*/
CREATE PROCEDURE Clte.UDP_Areas_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
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


/*Sección #62*/
CREATE PROCEDURE Clte.UDP_Areas_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina registros
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

/*Sección #63*/
CREATE PROCEDURE Clte.UDP_Areas_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
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


/*Sección #64*/
CREATE PROCEDURE Gral.UDP_tbOccupations_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento con el cual se insertan 
--              datos en la tabla occupation.
--========================================================
	@occ_Description 	VARCHAR(100),
	@occ_IdUserCreate	INT
AS
BEGIN
	INSERT INTO Gral.tbOccupations	
	(  
		occ_Description,
		occ_Status,
		occ_IdUserCreate,
		occ_DateCreate
	 )

	VALUES 
	(
	    @occ_Description,  
		1,
		@occ_IdUserCreate,  
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
END
GO


/*Sección #65*/
CREATE PROCEDURE Gral.UDP_tbOccupations_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que  actualiza los datos
--              de un registro en la tabla occupations.
--========================================================
	@occ_Id				INT,
	@occ_Description 	VARCHAR(100),
	@occ_IdUserModified	INT
AS
BEGIN
	UPDATE	[Gral].[tbOccupations]
	SET		occ_Description = @occ_Description,
			occ_IdUserModified = @occ_IdUserModified,
			occ_DateModified = GETDATE()
	WHERE	[occ_Id] = @occ_Id
END
GO


/*Sección #66*/
CREATE PROCEDURE Gral.UDP_tbOccupations_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina registros
--              de la tabla segun el id.
--========================================================
	@occ_Id INT,
	@occ_IdUserModified INT
AS
BEGIN
	UPDATE	[Gral].[tbOccupations]
	SET		occ_Status = 0,
			occ_IdUserModified = @occ_IdUserModified,
			occ_DateModified  = GETDATE()
	WHERE	[occ_Id] = @occ_Id
END
GO


/*Sección #67*/
CREATE OR ALTER PROCEDURE Acce.UDP_tbUsers_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que Inserta registros
--              a la tabla Users.
--========================================================
    @usu_UserName			 VARCHAR(100),
	@usu_Password			 NVARCHAR(100),
	@usu_PasswordSalt		 NVARCHAR(MAX),
	@usu_Profile_picture	 NVARCHAR(MAX),
	@rol_Id					 INT,
	@Per_Id					 INT,
	@usu_IdUserCreate		 INT
AS
BEGIN
	INSERT INTO Acce.tbUsers
	VALUES(@usu_UserName,
	@usu_Password,
	@usu_PasswordSalt,
	@usu_Profile_picture,
	0,
	@rol_Id,
	1,
	@Per_Id,
	@usu_IdUserCreate,
	GETDATE(),
	NULL,
	NULL)

	SELECT @@IDENTITY
END

GO

/*Sección #68*/
CREATE OR ALTER PROCEDURE Acce.UDP_tbUsers_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que modifica registros
--              a la tabla Users.
--========================================================
    @usu_Id                    INT,
    @usu_UserName            VARCHAR(20),
    @usu_Password			 NVARCHAR(100),
	@usu_PasswordSalt		 NVARCHAR(MAX),
	@usu_Profile_picture	 NVARCHAR(MAX),
    @rol_Id                    INT,
    @per_Id                    INT,
    @usu_IdUserModified        INT
AS
    BEGIN
        UPDATE Acce.tbUsers
        SET usu_UserName	=  @usu_UserName,
		usu_Password		=  @usu_Password,
		usu_PasswordSalt	=  @usu_PasswordSalt,
		usu_Profile_picture =  @usu_Profile_picture,
		usu_Temporal_Password = 0,
		rol_Id              =  @rol_Id,
		per_Id              =  @per_Id,
		usu_IdUserModified  = @usu_IdUserModified,
		usu_DateModified    = GETDATE()
        WHERE usu_Id        = @usu_Id
		
	SELECT @@IDENTITY
    END

GO


/*Sección #69*/
CREATE PROCEDURE Acce.UDP_tbUsers_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina registros
--              a la tabla Users.
--========================================================
	@usu_Id					INT,
	@usu_IdUserModified		INT
AS
	BEGIN
		UPDATE Acce.tbUsers
		SET usu_Status = 0,
			usu_IdUserModified = @usu_IdUserModified,
			usu_DateModified = GETDATE()
		WHERE usu_Id = @usu_Id
	END

GO


/*Sección #70*/
CREATE PROCEDURE Acce.UPD_tbUsers_LOGIN
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento valida el pasword y
--              usuari que entra al sistema.
--========================================================
	@usu_UserName      VARCHAR(100),
	@usu_Password      NVARCHAR(100)
AS
BEGIN
	SELECT 
	Acce.tbUsers.usu_Password,
	Acce.tbUsers.usu_PasswordSalt
	FROM Acce.tbUsers
	WHERE Acce.tbUsers.usu_UserName = @usu_UserName
END

GO


/*Sección #71*/
CREATE PROCEDURE Vent.UDP_tbCategories_REGISTER
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que registra
--              en la tabla categorias.
--========================================================
@cat_Description NVARCHAR(100),
@cat_IdUserCreate INT
AS
	BEGIN
     INSERT INTO [Vent].[tbCategories]
           ([cat_Description]
           ,[cat_Status]
           ,[cat_IdUserCreate]
           ,[cat_DateCreate]
           ,[cat_IdUserModified]
           ,[cat_DateModified])
     VALUES
           (@cat_Description,1,@cat_IdUserCreate,GETDATE(),NULL,NULL)
		    SELECT @@IDENTITY
END

GO

/*Sección #72*/
CREATE PROCEDURE Vent.UDP_tbCategories_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que modifica
--              en la tabla categorias.
--========================================================
@cat_Description NVARCHAR(100),
@cat_IdUserModified INT,
@cat_Id INT
AS
    BEGIN
       UPDATE [Vent].[tbCategories]
       SET 
	   [cat_Description] = @cat_Description,
       [cat_IdUserModified] = @cat_IdUserModified,
       [cat_DateModified] = GETDATE()
      WHERE cat_Id = @cat_Id
		
	SELECT @@IDENTITY
    END

GO


/*Sección #73*/
CREATE PROCEDURE Vent.UDP_tbCategories_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina
--              en la tabla categorias.
--========================================================
	@cat_Id					INT,
	@cat_IdUserModified		INT
AS
	BEGIN
		UPDATE Vent.tbCategories
		SET cat_Status = 0,
			cat_IdUserModified = @cat_IdUserModified,
			cat_DateModified = GETDATE()
		WHERE cat_Id = @cat_Id
	END

GO



/*Sección #74*/
CREATE PROCEDURE Clte.UDP_tbCustomers_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que inserta
--              en la tabla customers.
--========================================================
@cus_AssignedUser        INT,
@tyCh_Id                INT,
@cus_Name                NVARCHAR(200),
@cus_RTN                NVARCHAR(14),
@cus_Address            NVARCHAR(200),
@dep_Id                    INT,
@mun_Id                    INT,
@cus_Email                NVARCHAR(100),
@cus_receive_email        BIT,
@cus_Phone                NVARCHAR(30),
@cus_AnotherPhone        NVARCHAR(30),
@cus_IdUserCreate        INT
AS
    BEGIN
        INSERT INTO Clte.tbCustomers
        VALUES (
        @cus_AssignedUser,
        @tyCh_Id,
        @cus_Name,
        @cus_RTN,
        @cus_Address,
        @dep_Id,
        @mun_Id,
        @cus_Email,
        @cus_receive_email,
        @cus_Phone,
        @cus_AnotherPhone,
        1,
        1,
        @cus_IdUserCreate,
        GETDATE(),
        NULL,
        NULL)

    SELECT @@IDENTITY 
    END
	Go

/*Sección #75*/
CREATE PROCEDURE Clte.UDP_tbCustomers_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que modifica
--              en la tabla customers.
--========================================================
@cus_Id                    INT,
@cus_AssignedUser        INT,
@tyCh_Id                INT,
@cus_Name                NVARCHAR(200),
@cus_RTN                NVARCHAR(14),
@cus_Address            NVARCHAR(200),
@dep_Id                    INT,
@mun_Id                    INT,
@cus_Email                NVARCHAR(100),
@cus_receive_email        BIT,
@cus_Active             BIT,
@cus_Phone                NVARCHAR(30),
@cus_AnotherPhone        NVARCHAR(30),
@cus_IdUserModified        INT
AS
    BEGIN
        UPDATE Clte.tbCustomers
        SET cus_AssignedUser    = @cus_AssignedUser, 
        tyCh_Id                    = @tyCh_Id, 
        cus_Name                = @cus_Name, 
        cus_RTN                    = @cus_RTN, 
        cus_Address                = @cus_Address, 
        dep_Id                    = @dep_Id,
        mun_Id                    = @mun_Id,
        cus_Email                = @cus_Email, 
        cus_receive_email        = @cus_receive_email,
        cus_Phone                = @cus_Phone, 
        cus_AnotherPhone        = @cus_AnotherPhone, 
        cus_Status                = 1, 
        cus_Active                = @cus_Active,
        cus_IdUserModified        = @cus_IdUserModified, 
        cus_DateModified        = GETDATE()
        WHERE cus_Id = @cus_Id
    END
	Go

/*Sección #76*/
CREATE PROCEDURE Clte.UDP_tbCustomers_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina
--              en la tabla customers.
--========================================================
@cus_Id					INT,
@cus_IdUserModified		INT
AS
	BEGIN
		UPDATE Clte.tbCustomers
		SET cus_Status = 0, 
		cus_IdUserModified = @cus_IdUserModified,
		cus_DateModified = GETDATE()
		WHERE cus_Id = @cus_Id
	END

GO



/*Sección #77*/
CREATE PROCEDURE Vent.UDP_tbSubCategories_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que inserta registros
--              en la tabla SubCategories.
--========================================================
	@scat_Description						NVARCHAR(100),
	@cat_Id									INT,
	@scat_IdUserCreate						INT
AS
	BEGIN
		INSERT INTO Vent.tbSubCategories
		VALUES (@scat_Description,@cat_Id,1,@scat_IdUserCreate,GETDATE(), NULL,NULL)
	END
	SELECT @@IDENTITY

GO


/*Sección #78*/
CREATE PROCEDURE Vent.UDP_tbSubCategories_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que modifica los registros
--              de la tabla SubCategories.
--========================================================
	@scat_Id								INT,
	@scat_Description						NVARCHAR(100),
	@cat_Id									INT,
	@scat_IdUserModified					INT
AS
	BEGIN
		UPDATE Vent.tbSubCategories
		SET scat_Description	= @scat_Description, 
			cat_Id				= @cat_Id, 
			scat_IdUserModified = @scat_IdUserModified, 
			scat_DateModified	= GETDATE()
		WHERE scat_Id			= @scat_Id
	END

GO


/*Sección #79*/
CREATE PROCEDURE Vent.UDP_tbSubCategories_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina un registros
--              en la tabla SubCategories.
--========================================================
	@scat_Id								INT,
	@scat_IdUserModified					INT
AS
	BEGIN
		UPDATE Vent.tbSubCategories
		SET scat_Status			= 0, 
			scat_IdUserModified = @scat_IdUserModified, 
			scat_DateModified	= GETDATE()
		WHERE scat_Id			= @scat_Id
	END

GO





/*Sección #80*/
CREATE PROCEDURE Clte.UDP_tbContacts_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que inserta un registros
--              en la tabla Contacts.
--========================================================
	@cont_Name					NVARCHAR(100),
	@cont_LastName				NVARCHAR(100),
	@cont_Email					NVARCHAR(100),
	@cont_Phone					NVARCHAR(100),
	@occ_Id						INT,
	@cus_Id						INT,
	@cont_IdUserCreate			INT
AS
	BEGIN
		INSERT INTO Clte.tbContacts
		VALUES (@cont_Name, @cont_LastName, @cont_Email, @cont_Phone, @occ_Id,@cus_Id, 1, @cont_IdUserCreate, GETDATE(), NULL, NULL)
	END
	SELECT @@IDENTITY
GO


/*Sección #81*/
CREATE PROCEDURE Clte.UDP_tbContacts_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que modifica los registros
--              de la tabla Contacts.
--========================================================
	@cont_Id					INT,
	@cont_Name					NVARCHAR(100),
	@cont_LastName				NVARCHAR(100),
	@cont_Email					NVARCHAR(100),
	@cont_Phone					NVARCHAR(100),
	@occ_Id						INT,
	@cus_Id						INT,
	@cont_IdUserModified		INT
AS
	BEGIN
		UPDATE Clte.tbContacts
		SET cont_Name			= @cont_Name, 
			cont_LastName		= @cont_LastName, 
			cont_Email			= @cont_Email, 
			cont_Phone			= @cont_Phone, 
			occ_Id				= @occ_Id, 
			cus_Id				= @cus_Id,
			cont_IdUserModified = @cont_IdUserModified, 
			cont_DateModified	= GETDATE()
		WHERE cont_Id = @cont_Id
	END

GO


/*Sección #82*/
CREATE PROCEDURE Clte.UDP_tbContacts_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina un registros
--              en la tabla Contacts.
--========================================================
	@cont_Id					INT,
	@cont_IdUserModified		INT
AS
	BEGIN
		UPDATE Clte.tbContacts
		SET cont_Status = 0, 
		cont_IdUserModified = @cont_IdUserModified, 
		cont_DateModified = GETDATE()
		WHERE cont_Id = @cont_Id
	END

GO



/*Sección #83*/
CREATE PROCEDURE Gral.UDP_tbEmployees_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que inserta un registros
--              a la tabla Employees.
--========================================================
	@per_Id						INT,
	@are_Id						INT,
	@occ_Id						INT,
	@emp_IdUserCreate			INT
AS
	BEGIN
		INSERT INTO Gral.tbEmployees
		VALUES (@per_Id, @are_Id, @occ_Id, 1,@emp_IdUserCreate, GETDATE(), NULL, NULL)
	END
	SELECT @@IDENTITY

GO


/*Sección #84*/
CREATE PROCEDURE Gral.UDP_tbEmployees_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que modifica un registro
--              a la tabla Employees.
--========================================================
	@emp_Id						INT,
	@per_Id						INT,
	@are_Id						INT,
	@occ_Id						INT,
	@emp_IdUserModified			INT
AS
	BEGIN
		UPDATE Gral.tbEmployees
		SET per_Id			= @per_Id, 
		are_Id				= @are_Id, 
		occ_Id				= @occ_Id, 
		emp_IdUserModified	= @emp_IdUserModified, 
		emp_DateModified	= GETDATE()
		WHERE emp_Id = @emp_Id
	END

GO


/*Sección #85*/
CREATE PROCEDURE Gral.UDP_tbEmployees_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina un registros
--              a la tabla Employees.
--========================================================
	@emp_Id						INT,
	@emp_IdUserModified			INT
AS
	BEGIN
		UPDATE Gral.tbEmployees
		SET emp_Status		= 0, 
		emp_IdUserModified	= @emp_IdUserModified, 
		emp_DateModified	= GETDATE()
		WHERE emp_Id = @emp_Id
	END
GO


/*Sección #86*/
CREATE PROCEDURE Vent.UDP_tbProducts_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que inserta un registros
--              a la tabla Products.
--========================================================
    @pro_Description NVARCHAR(100),
    @pro_PurchasePrice NUMERIC(8,2),
    @pro_SalesPrice NUMERIC(8,2),
    @pro_Stock INT,
    @pro_ISV NUMERIC(18,0),
    @uni_Id INT,
    @scat_Id INT,
    @pro_IdUserCreate INT
AS
    BEGIN

     INSERT INTO Vent.tbProducts
           ([pro_Description]
           ,[pro_PurchasePrice]
           ,[pro_SalesPrice]
           ,[pro_Stock]
           ,[pro_ISV]
           ,[uni_Id]
           ,[scat_Id]
           ,[pro_Status]
           ,[pro_IdUserCreate]
           ,[pro_DateCreate]
           ,[pro_IdUserModified]
           ,[pro_DateModified])
     VALUES
           (@pro_Description,@pro_PurchasePrice,@pro_SalesPrice,@pro_Stock,@pro_ISV,@uni_Id,@scat_Id,1,@pro_IdUserCreate,GETDATE(),Null,Null)

    SELECT @@IDENTITY
    END
GO


/*Sección #87*/
CREATE PROCEDURE Vent.UDP_tbProducts_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que modifica un registros
--              a la tabla Products.
--========================================================
    @pro_Id INT,
    @pro_Description NVARCHAR(100),
    @pro_PurchasePrice NUMERIC(8,2),
    @pro_SalesPrice NUMERIC(8,2),
    @pro_Stock INT,
    @pro_ISV NUMERIC(18,0),
    @uni_Id INT,
    @scat_Id INT,
    @pro_IdUserModified INT
AS
    BEGIN
    UPDATE [Vent].[tbProducts]
   SET [pro_Description] = @pro_Description,
       [pro_PurchasePrice] = @pro_PurchasePrice,
       [pro_SalesPrice] = @pro_SalesPrice,
       [pro_Stock] = @pro_Stock,
       [pro_ISV] = @pro_ISV,
       [uni_Id] = @uni_Id,
       [scat_Id] = @scat_Id,
       [pro_IdUserModified] = @pro_IdUserModified,
       [pro_DateModified] = GETDATE()
 WHERE pro_Id = @pro_Id
END
GO

/*Sección #88*/
CREATE PROCEDURE Vent.UDP_tbProducts_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina un registros
--              a la tabla Products.
--========================================================
	@pro_Id						INT,
	@pro_IdUserModified			INT
AS
	BEGIN
		UPDATE [Vent].[tbProducts]
		SET pro_Status		= 0, 
		pro_IdUserModified	= @pro_IdUserModified, 
		pro_DateModified	= GETDATE()
		WHERE pro_Id = @pro_Id
	END
GO



/*Sección #89*/
CREATE PROCEDURE Vent.UDP_tbPersons_INSERT
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que inserta un registros
--              a la tabla Persons.
--========================================================
    @per_Identidad		 NVARCHAR(13)		,
    @per_Firstname		 NVARCHAR(20)		,
    @per_Secondname		 NVARCHAR(20)		,
    @per_LastNames		 NVARCHAR(20)		,
    @per_BirthDate		 DATE				,
    @per_Sex			 CHAR(1)			,
    @per_Email			 NVARCHAR(100)		,
	@per_Phone			 NVARCHAR(30)		,
	@per_Direccion		 NVARCHAR(200)		,
	@dep_Id				 INT				,
	@mun_Id				 INT				,
	@per_Esciv			 CHAR				,
    @per_IdUserCreate    INT
AS
    BEGIN

     INSERT INTO [Gral].[tbPersons]
           (per_Identidad	
           ,per_Firstname	
           ,per_Secondname	
           ,per_LastNames	
           ,per_BirthDate	
           ,per_Sex		
           ,per_Email		
           ,per_Phone		
           ,per_Direccion	
           ,dep_Id			
           ,mun_Id			
           ,per_Esciv		
		   ,per_Status		
		   ,per_IdUserCreate
		   ,per_DateCreate)
     VALUES
           ( @per_Identidad	,	
			 @per_Firstname	,	
			 @per_Secondname,		
			 @per_LastNames	,	
			 @per_BirthDate	,	
			 @per_Sex		,	
			 @per_Email		,	
			 @per_Phone		,	
			 @per_Direccion	,	
			 @dep_Id		,		
			 @mun_Id		,		
			 @per_Esciv		,	
			 1				,	
			 @per_IdUserCreate,
		     GETDATE()
		   )

    SELECT @@IDENTITY
    END
GO


/*Sección #90*/
CREATE PROCEDURE Vent.UDP_tbPersons_UPDATE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que modifica un registros
--              a la tabla Persons.
--========================================================
    @per_Id				 INT                ,
    @per_Identidad		 NVARCHAR(13)		,
    @per_Firstname		 NVARCHAR(20)		,
    @per_Secondname		 NVARCHAR(20)		,
    @per_LastNames		 NVARCHAR(20)		,
    @per_BirthDate		 DATE				,
    @per_Sex			 CHAR(1)			,
    @per_Email			 NVARCHAR(100)		,
	@per_Phone			 NVARCHAR(30)		,
	@per_Direccion		 NVARCHAR(200)		,
	@dep_Id				 INT				,
	@mun_Id				 INT				,
	@per_Esciv			 CHAR				,
    @per_IdUserModified  INT
AS
    BEGIN
    UPDATE [Gral].[tbPersons]
   SET  [per_Identidad]  =	   @per_Identidad	,	
		[per_Firstname]  =	   @per_Firstname	,	
		[per_Secondname] =	   @per_Secondname	,	
		[per_LastNames]  =	   @per_LastNames	,	
		[per_BirthDate]  =	   @per_BirthDate	,	
		[per_Sex]        =	   @per_Sex			,
		[per_Email]      =	   @per_Email		,	
		[per_Phone]      =	   @per_Phone		,	
		[per_Direccion]  =	   @per_Direccion	,	
		[dep_Id]         =	   @dep_Id			,	
		[mun_Id]         =	   @mun_Id			,	
		[per_Esciv]		 =	   @per_Esciv		,	
		[per_Status]     =	   1				,	
        [per_IdUserModified] = @per_IdUserModified,
        [per_DateModified] =   GETDATE()
 WHERE  per_Id = @per_Id
END
GO

/*Sección #91*/
CREATE PROCEDURE Vent.UDP_tbPersons_DELETE
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Procedimiento que elimina un registros
--              a la tabla Personas.
--========================================================
	@per_Id						INT,
	@per_IdUserModified			INT
AS
	BEGIN
		UPDATE [Gral].[tbPersons]
		SET per_Status		= 0, 
		per_IdUserModified	= @per_IdUserModified, 
		per_DateModified	= GETDATE()
		WHERE per_Id = @per_Id
	END
GO



/*Sección #92*/
CREATE PROCEDURE Vent.View_tbCotizations_Details
--========================================================
--Author:       Angel Rapalo
--Create date:  29/05/2022
--Description:  Selecciona una la cotizaciones.
--========================================================
	@cot_Id						INT
AS
        SELECT coti.cot_Id,
			   coti.cus_Id,
			   cus.cus_Name,
			   coti.cot_DateValidUntil,
			   coti.sta_Id,
			   cotd.pro_Id,
			   prd.pro_Description,
			   cotd.code_Cantidad,
			   cotd.code_TotalPrice,
		CASE   coti.cot_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
			   coti.cot_IdUserCreate,
			   us.usu_UserName AS usu_UserNameCreate,
			   coti.cot_DateCreate,
			   coti.cot_IdUserModified,
			   usm.usu_UserName AS usu_UserNameModified,
			   coti.cot_DateModified
        FROM   [Vent].[tbCotizations]  AS  coti
		INNER JOIN Vent.tbCotizationsDetail AS cotd ON cotd.cot_Id = coti.cot_Id
		INNER JOIN Clte.tbCustomers AS cus ON cus.cus_Id=coti.cus_Id
		INNER JOIN [Gral].[tbStates] AS sta ON sta.sta_Id=coti.sta_Id
		INNER JOIN Acce.tbUsers AS us ON coti.cot_IdUserCreate     = us.usu_Id
		INNER JOIN Vent.tbProducts AS prd ON prd.pro_Id = cotd.pro_Id
		LEFT  JOIN Acce.tbUsers AS usm ON coti.cot_IdUserModified  = usm.usu_Id
		WHERE coti.cot_Status = 1 AND coti.cot_Id = @cot_Id
GO


/*Sección #93*/
CREATE PROCEDURE Vent.UDP_tbCotizations_DELETE
--========================================================
--Author:       Angel Rapalo
--Create date:  02/06/2022
--Description:  Elimina Cotizaciones.
--========================================================
	@cot_Id						INT,
	@cot_IdUserModified			INT
AS
	BEGIN
		BEGIN TRAN
			BEGIN TRY
				UPDATE Vent.tbCotizations
				SET cot_Status = 0, cot_IdUserModified = @cot_IdUserModified, cot_DateModified = GETDATE()
				WHERE cot_Id = @cot_Id

				UPDATE Vent.tbCotizationsDetail
				SET code_Status = 0, code_IdUserModified = @cot_IdUserModified, code_DateModified = GETDATE()
				WHERE cot_Id = @cot_Id
		COMMIT TRAN
			END TRY
			BEGIN CATCH
				SELECT ERROR_MESSAGE()
				ROLLBACK
			END CATCH
	END
GO


/*Sección #94*/
CREATE PROCEDURE Vent.UDP_tbSaleOrders_Details
--========================================================
--Author:       Angel Rapalo
--Create date:  04/06/2022
--Description:  Selecciona una orden de venta.
--========================================================
	@sor_Id						INT
AS
        SELECT	sor.sor_Id,
				sor.cus_Id,
				cus.cus_Name,
				sor.cot_Id,
				coti.cot_Status,
				sor.sta_Id,
				sta.sta_Description,
				ord.ode_Amount,
				ord.pro_Id,
				prd.pro_Description,
				ord.ode_TotalPrice,
		CASE	sor.sor_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END 'Status',
				sor.sor_IdUserCreate,
				usc.usu_UserName AS usu_UserNameCreate,
				sor.sor_DateCreate,
				sor.sor_IdUserModified,
				usm.usu_UserName AS usu_UserNameModified,
				sor.sor_DateModified
        FROM Vent.tbSaleOrders AS sor
		INNER JOIN Vent.tbOrderDetails AS ord ON ord.sor_Id = sor.sor_Id
		INNER JOIN Clte.tbCustomers AS cus ON cus.cus_Id = sor.cus_Id
		INNER JOIN Vent.tbCotizations AS coti ON coti.cot_Id = sor.cot_Id
		INNER JOIN Gral.tbStates AS sta ON sta.sta_Id = sor.sor_Id
		INNER JOIN Vent.tbProducts AS prd ON prd.pro_Id = ord.pro_Id
		INNER JOIN Acce.tbUsers AS usc ON usc.usu_Id = sor.sor_IdUserCreate
		LEFT JOIN Acce.tbUsers AS usm ON usm.usu_Id = sor.sor_IdUserModified
		WHERE sor.sor_Status = 1 AND sor.sor_Id = @sor_Id
GO

/*Sección #95*/
CREATE PROCEDURE Vent.UDP_tbOrders_DELETE
--========================================================
--Author:       Angel Rapalo
--Create date:  02/06/2022
--Description:  Elimina Ordenes.
--========================================================
	@sor_Id						INT,
	@sor_IdUserModified			INT
AS
	BEGIN
		BEGIN TRAN
			BEGIN TRY
				UPDATE Vent.tbSaleOrders
				SET sor_Status = 0, sor_IdUserModified = @sor_IdUserModified, sor_DateModified = GETDATE()
				WHERE sor_Id = @sor_Id

				UPDATE Vent.tbOrderDetails
				SET ode_Status = 0, ode_IdUserModified = @sor_IdUserModified, ode_DateModified = GETDATE()
				WHERE sor_Id = @sor_Id
		COMMIT TRAN
			END TRY
			BEGIN CATCH
				SELECT ERROR_MESSAGE()
				ROLLBACK
			END CATCH
	END

GO

/*Sección #96*/
CREATE PROCEDURE Clte.UDP_tbCustomerCalls_INSERT
--========================================================
--Author:       Angel Rapalo
--Create date:  08/06/2022
--Description:  Inserta una nueva llamada de cliente.
--========================================================
@cca_CallType		INT, 
@cca_Business		INT, 
@cca_Date			DATE,
@cca_StartTime	    NVARCHAR(12), 
@cca_EndTime		NVARCHAR(12),
@cca_Result			INT,
@cus_Id				INT,
@cca_IdUserCreate	INT
AS
	BEGIN
		INSERT INTO Clte.tbCustomerCalls
		VALUES(@cca_CallType,@cca_Business,@cca_Date,@cca_StartTime, @cca_EndTime,@cca_Result,@cus_Id,1,@cca_IdUserCreate,GETDATE(),NULL,NULL)
		SELECT @@IDENTITY
	END

GO



/*Sección #97*/
CREATE PROCEDURE Clte.UDP_tbCustomerCalls_UPDATE
--========================================================
--Author:       Angel Rapalo
--Create date:  08/06/2022
--Description:  Actualiza una llamada de cliente.
--========================================================
@cca_Id				INT,
@cca_CallType		INT, 
@cca_Business		INT, 
@cca_Date			DATE,
@cca_StartTime		NVARCHAR(12), 
@cca_EndTime     	NVARCHAR(12),
@cca_Result			INT,
@cus_Id				INT,
@cca_IdUserModified	INT 
AS
	BEGIN
		UPDATE Clte.tbCustomerCalls
		SET cca_CallType = @cca_CallType,cca_Business = @cca_Business,cca_Date=@cca_Date,cca_StartTime=@cca_StartTime,cca_EndTime=@cca_EndTime,cca_Result=@cca_Result,cus_Id = @cus_Id,cca_IdUserModified = @cca_IdUserModified,cca_DateModified = GETDATE()
		WHERE cca_Id = @cca_Id
	END

GO

/*Sección #98*/
CREATE PROCEDURE Clte.UDP_tbCustomerCalls_DELETE
--========================================================
--Author:       Angel Rapalo
--Create date:  08/06/2022
--Description:  Elimina una llamada de cliente.
--========================================================
@cca_Id					INT,
@cca_IdUserModified		INT 
AS
	BEGIN
		UPDATE Clte.tbCustomerCalls
		SET cca_Status = 0,cca_IdUserModified = @cca_IdUserModified,cca_DateModified = GETDATE()
		WHERE cca_Id = @cca_Id
	END

GO

/*Sección #99*/
CREATE PROCEDURE Clte.UDP_tbCustomerNotes_INSERT
--========================================================
--Author:       Angel Rapalo
--Create date:  08/06/2022
--Description:  Inserta una nueva nota de cliente.
--========================================================
@cun_Descripcion		NVARCHAR(MAX), 
@cun_ExpirationDate		DATE, 
@pry_Id					INT, 
@cus_Id					INT,
@cun_IdUserCreate		INT
AS
	BEGIN
		INSERT INTO Clte.tbCustomerNotes
		VALUES(@cun_Descripcion,@cun_ExpirationDate,@pry_Id,@cus_Id,1,@cun_IdUserCreate,GETDATE(),NULL,NULL)
		SELECT @@IDENTITY 
	END

GO

/*Sección #100*/
CREATE PROCEDURE Clte.UDP_tbCustomerNotes_UPDATE
--========================================================
--Author:       Angel Rapalo
--Create date:  08/06/2022
--Description:  Actualiza una nota de cliente.
--========================================================
@cun_Id					INT,
@cun_Descripcion		NVARCHAR(MAX), 
@cun_ExpirationDate		DATE, 
@pry_Id					INT,  
@cus_Id					INT,
@cun_IdUserModified		INT
AS
	BEGIN
		UPDATE Clte.tbCustomerNotes
		SET cun_Descripcion = @cun_Descripcion,cun_ExpirationDate = @cun_ExpirationDate,pry_Id = @pry_Id,cus_Id = @cus_Id,cun_IdUserModified = @cun_IdUserModified,cun_DateModified = GETDATE()
		WHERE cun_Id = @cun_Id
	END

GO

/*Sección #101*/
CREATE PROCEDURE Clte.UDP_tbCustomerNotes_DELETE
--========================================================
--Author:       Angel Rapalo
--Create date:  08/06/2022
--Description:  Actualiza una nota de cliente.
--========================================================
@cun_Id					INT,
@cun_IdUserModified		INT
AS
	BEGIN
		UPDATE Clte.tbCustomerNotes
		SET cun_Status = 0,cun_IdUserModified = @cun_IdUserModified,cun_DateModified = GETDATE()
		WHERE cun_Id = @cun_Id
	END
GO

/*Sección #102*/
CREATE PROCEDURE Vent.UDP_tbCotizationsDetail_DELETE
--========================================================
--Author:       Benkay Ham
--Create date:  09/06/2022
--Description:  Procedimiento que elimina un registro de cotizacion
--========================================================
	@pro_Id							INT,
	@cot_Id						INT,
	@code_IdUserModified			INT
AS
	BEGIN
		UPDATE Vent.tbCotizationsDetail
		SET code_Status		= 0, 
		code_IdUserModified	= @code_IdUserModified, 
		code_DateModified	= GETDATE()
		WHERE cot_Id = @cot_Id AND pro_Id = @pro_Id
	END
GO

/*Sección #103*/
CREATE PROCEDURE Vent.UDP_tbCampaign_INSERT
--========================================================
--Author:       Benkay Ham
--Create date:  11/06/2022
--Description:  Inserta un nuevo detalle de campaña.
--========================================================
@cam_Nombre NVARCHAR(100),
@cam_Descripcion NVARCHAR(MAX),
@cam_Html NVARCHAR(MAX),
@cam_IdUserCreate INT
AS
	BEGIN
		INSERT INTO Vent.tbCampaign
		VALUES(@cam_Nombre,@cam_Descripcion,@cam_Html,1,@cam_IdUserCreate,GETDATE())
		SELECT @@IDENTITY 
	END

GO

/*Sección #104*/
CREATE PROCEDURE Vent.UDP_tbCampaign_DELETE
--========================================================
--Author:       Benkay Ham
--Create date:  14/06/2022
--Description:  Elimina una campaña.
--========================================================
@cam_Id INT
AS
	BEGIN
	  DELETE FROM [Vent].[tbCampaign]
      WHERE cam_Id = @cam_Id
	END

GO

/*Sección #105*/
CREATE PROCEDURE [Vent].[UDP_tbOrdersDetail_DELETE]
--========================================================
--Author:       Mauricio Escalante
--Create date:  09/06/2022
--Description:  Procedimiento que elimina un registro de orden de venta
--========================================================
	@pro_Id						INT,
	@sor_Id						INT,
	@ode_IdUserModified			INT
AS
	BEGIN
		UPDATE Vent.tbOrderDetails
		SET ode_Status		= 0, 
		ode_IdUserModified	= @ode_IdUserModified, 
		ode_DateModified	= GETDATE()
		WHERE sor_Id = @sor_Id AND pro_Id = @pro_Id
END
GO

/*Sección #106*/
CREATE PROCEDURE [Gral].[UDP_Dashboard_Metrics]
--========================================================
--Author:       Benkay Ham
--Create date:  14/06/2022
--Description:  Muestra las metricas generales
--========================================================

AS
	BEGIN
DECLARE @LastCampaign INT
SET	@LastCampaign=(SELECT TOP 1 (cam_Id) 
        FROM Vent.tbCampaign
        ORDER BY cam_Id Desc)

	  SELECT COUNT(cus_Id) AS Customers 
	  FROM Clte.tbCustomers 
	  WHERE cus_Status=1

	  SELECT COUNT(cot_Id) AS Cotizations 
	  FROM Vent.tbCotizations AS Coti 
	  WHERE Coti.cot_Status=1
	  AND coti.cot_DateCreate >= DATEADD(day,-30,GETDATE()) 
      AND coti.cot_DateCreate <= getdate()
	  
	  SELECT COUNT(sor_Id) AS Sales 
	  FROM Vent.tbSaleOrders AS Sale 
	  WHERE sor_Status=1
	  AND Sale.sor_DateCreate >= DATEADD(day,-30,GETDATE()) 
      AND Sale.sor_DateCreate <= getdate()
	  
	  SELECT COUNT(cde_Id) AS Campaigns 
	  FROM Vent.tbCampaignDetails 
	  WHERE cam_Id = @LastCampaign
	END
GO


/*Sección #107*/
CREATE PROCEDURE [Clte].[UDP_tbCustomersFile_INSERT]
--========================================================
--Author:       Angel Rapalo
--Create date:  16/06/2022
--Description:  Registra un la ruta de un archivo
--========================================================
	@cfi_fileRoute				NVARCHAR(MAX),
	@cus_Id						INT,
	@cfi_IdUserCreate			INT
AS
	BEGIN
		INSERT INTO Clte.tbCustomersFile
		VALUES(@cfi_fileRoute, @cus_Id, 1, @cfi_IdUserCreate, GETDATE(), NULL, NULL)
		SELECT @@IDENTITY
	END
GO


/*Sección #108*/
CREATE PROCEDURE [Clte].[UDP_tbCustomersFile_UPDATE]
--========================================================
--Author:       Angel Rapalo
--Create date:  16/06/2022
--Description:  Actualiza un archivo
--========================================================
	@cfi_Id						INT,
	@cfi_fileRoute				NVARCHAR(MAX),
	@cus_Id						INT,
	@cfi_IdUserModified			INT
AS
	BEGIN
		UPDATE Clte.tbCustomersFile
		SET cfi_fileRoute = @cfi_fileRoute, cus_Id = @cus_Id, cfi_IdUserModified = @cfi_IdUserModified, cfi_DateModified = GETDATE()
		WHERE cfi_Id = @cfi_Id
	END
GO


/*Sección #109*/
CREATE PROCEDURE [Clte].[UDP_tbCustomersFile_DELETE]
--========================================================
--Author:       Angel Rapalo
--Create date:  16/06/2022
--Description:  Hace el borrado logico a un archivo
--========================================================
	@cfi_Id						INT,
	@cfi_IdUserModified			INT
AS
	BEGIN
		UPDATE Clte.tbCustomersFile
		SET cfi_Status = 0, cfi_IdUserModified = @cfi_IdUserModified, cfi_DateModified = GETDATE()
		WHERE cfi_Id = @cfi_Id
	END
GO


/*Sección #110*/
CREATE PROCEDURE [Gral].[UDP_Dashboard_Metrics_Filter]
--========================================================
--Author:       Benkay Ham
--Create date:  16/06/2022
--Description:  Muestra las metricas generales filtradas por el usuario
--========================================================
@Id INT
AS
	BEGIN
	DECLARE @LastCampaign INT
    SET	@LastCampaign=(SELECT TOP 1 (cam_Id) 
        FROM Vent.tbCampaign
        ORDER BY cam_Id Desc)

	  SELECT COUNT(cus_Id) AS Customers 
	  FROM Clte.tbCustomers 
	  WHERE cus_Status=1 
	  AND cus_AssignedUser=@Id
	
      SELECT COUNT(cot_Id) as Cotizations
      FROM    Vent.tbCotizations AS coti
	  INNER JOIN  Clte.tbCustomers AS custo ON coti.cus_Id=custo.cus_Id
	  WHERE coti.cot_DateCreate >= DATEADD(day,-30,GETDATE()) 
      AND   coti.cot_DateCreate <= getdate()
	  AND custo.cus_AssignedUser=@Id
	  
	  SELECT COUNT(sor_Id) as Sales
      FROM    Vent.tbSaleOrders AS sales
	  INNER JOIN  Clte.tbCustomers AS custo ON sales.cus_Id =custo.cus_Id
	  WHERE sales.sor_DateCreate >= DATEADD(day,-30,GETDATE()) 
      AND sales.sor_DateCreate <= getdate()
	  AND custo.cus_AssignedUser=@Id

	  SELECT COUNT(cde_Id) as Campaigns
	  FROM Vent.View_tbCampaignDetails_List
	  WHERE cam_Id=@LastCampaign

	END
GO


/*Sección #111*/
CREATE PROCEDURE [Vent].[UDP_Dashboard_Cotizations_Filter]
--========================================================
--Author:       Benkay Ham
--Create date:  18/06/2022
--Description:  Muestra las ultiamas 5 cotizaciones filtradas por el usuario
--========================================================
@Id INT
AS
	BEGIN
	
      SELECT TOP 5 coti.*
      FROM    Vent.View_tbCotizations_List AS coti
	  INNER JOIN  Clte.tbCustomers AS custo ON coti.cus_Id=custo.cus_Id
	  WHERE coti.cot_DateCreate >= DATEADD(day,-30,GETDATE()) 
      and   coti.cot_DateCreate <= getdate()
	  and custo.cus_AssignedUser=1
	  ORDER BY cot_DateCreate Desc
	  
	END
GO


/*Sección #112*/
CREATE PROCEDURE [Clte].[UDP_tbMeetings_DELETE]
--========================================================
--Author:       Angel Teruel
--Create date:  19/06/2022
--Description:  Hace el borrado logico a una reunion
--========================================================
	@met_Id						INT,
	@met_IdUserModified			INT
AS
	BEGIN
		UPDATE [Clte].[tbMeetings]
		SET met_Status = 0, met_IdUserModified = @met_IdUserModified, met_DateModified = GETDATE()
		WHERE met_Id = @met_Id
	END
GO

/*Sección #113*/
CREATE PROCEDURE [Clte].[UDP_tbMeetingsDetails_DELETE]
--========================================================
--Author:       Benkay Ham
--Create date:  21/06/2022
--Description:  Hace el borrado logico a una reunion
--========================================================
	@mde_Id						INT,
	@mde_IdUserModified			INT
AS
	BEGIN
		UPDATE [Clte].[tbMeetingsDetails]
		SET mde_Status = 0, mde_IdUserModified = @mde_IdUserModified, mde_DateModified = GETDATE()
		WHERE mde_Id = @mde_Id
	END
GO


/*Sección #114*/
CREATE PROCEDURE Vent.UDP_TopCustomerOrders_Metrics
--========================================================
--Author:       Benkay Ham
--Create date:  21/06/2022
--Description:  Muestra los 3 clientes con mas ordenes de venta
--========================================================
AS
	BEGIN
		SELECT TOP 3 COUNT(Sale.cus_Id) as Cantidad ,Custo.cus_Name as Nombre
		FROM Vent.tbSaleOrders as Sale
		INNER JOIN Clte.tbCustomers as Custo on Custo.cus_Id = Sale.cus_Id
		WHERE Sale.sor_DateCreate >= DATEADD(day,-30,GETDATE()) 
        and   Sale.sor_DateCreate <= getdate()
		GROUP BY Custo.cus_Name
		ORDER BY Cantidad Desc
	END
GO


/*Sección #115*/
CREATE PROCEDURE Vent.UDP_Top10Products_Metrics
--========================================================
--Author:       Benkay Ham
--Create date:  20/06/2022
--Description:  Muestra los 10 productos mas vendidos
--========================================================
AS
	BEGIN
		SELECT TOP 10 Pro.pro_Description as Producto ,SUM(ode_Amount) as Cantidad
		FROM vent.tbOrderDetails ord
		INNER JOIN vent.tbProducts as Pro on ord.pro_Id = Pro.pro_Id
		WHERE ord.ode_DateCreate >= DATEADD(day,-30,GETDATE()) 
        and   ord.ode_DateCreate <= getdate()
        GROUP BY pro.pro_Description
		ORDER BY Cantidad Desc
	END
GO


/*Sección #116*/
CREATE PROCEDURE Vent.UDP_Dashboard_Sales_Filter
--========================================================
--Author:       Benkay Ham
--Create date:  18/06/2022
--Description:  Muestra las ultiamas 5 ventas filtradas por el usuario
--========================================================
@Id INT
AS
	BEGIN
	
      SELECT TOP 5 sale.*
      FROM    Vent.View_tbSaleOrders_List AS sale
	  INNER JOIN  Clte.tbCustomers AS custo ON sale.cus_Id=custo.cus_Id
	  WHERE Sale.sor_DateCreate >= DATEADD(day,-30,GETDATE()) 
      and   Sale.sor_DateCreate <= getdate()
	  and custo.cus_AssignedUser=@Id
      ORDER BY sor_DateCreate Desc
	  
	END
GO


/*Sección #117*/
CREATE OR ALTER PROCEDURE Vent.UDP_LastMontCampaigns_Metrics
--========================================================
--Author:       Benkay Ham
--Create date:  21/06/2022
--Description:  Muestra los 3 clientes con mas ordenes de venta
--========================================================
AS
	BEGIN
		SELECT COUNT(camD.cde_Id) as Cantidad,Cam.cam_Id,Cam.cam_Nombre as Nombre,Cam.cam_Descripcion as Descripcion,CASE cam.cam_Status WHEN 1 THEN 'Sin enviar' WHEN 0 THEN 'Enviada' END  'Status',Cam.cam_DateCreate as Fechacrea
		FROM Vent.tbCampaignDetails as camD
		INNER JOIN Vent.tbCampaign as Cam on CamD.cam_Id = Cam.cam_Id
		WHERE cam.cam_DateCreate >= DATEADD(day,-30,GETDATE())
		AND cam.cam_DateCreate <= GETDATE()
		GROUP BY cam.cam_Id,cam.cam_Nombre,cam.cam_Descripcion,cam.cam_Status,cam.cam_DateCreate
		ORDER BY Cantidad Desc
	END
GO


/*Sección #118*/
CREATE OR ALTER PROCEDURE Vent.UDP_LastMontCampaigns_DateFilter
--========================================================
--Author:       Benkay Ham
--Create date:  21/06/2022
--Description:  Muestra los 3 clientes con mas ordenes de venta
--========================================================
@fechainicio DATETIME,
@fechatermina DATETIME
AS
	BEGIN
		SELECT COUNT(camD.cde_Id) as Cantidad,Cam.cam_Id,Cam.cam_Nombre as Nombre,Cam.cam_Descripcion as Descripcion,CASE cam.cam_Status WHEN 1 THEN 'Sin enviar' WHEN 0 THEN 'Enviada' END  'Status',Cam.cam_DateCreate as Fechacrea
		FROM Vent.tbCampaign as Cam 
		LEFT JOIN  Vent.tbCampaignDetails as camD on camD.cam_Id = Cam.cam_Id

		WHERE cam.cam_DateCreate BETWEEN @fechainicio  AND DATEADD(DAY, 1, @fechatermina) 	  
		GROUP BY cam.cam_Id,cam.cam_Nombre,cam.cam_Descripcion,cam.cam_Status,cam.cam_DateCreate
		ORDER BY Cantidad Desc
				    
	END
GO




/*Sección #119*/

CREATE OR ALTER PROCEDURE [Vent].[UDP_Reports_Orders_Dynamic]
--========================================================
--Author:       Benkay Ham
--Create date:  22/06/2022
--Description:  Genera y filtra los datos de una orden para un reporte, en base al Id de un cliente.
--========================================================
@Id INT,
@fechainicio DATETIME,
@fechatermina DATETIME
AS
BEGIN
 IF (@Id=0 AND @fechainicio='1950-01-01' AND @fechatermina ='1950-01-02')
	SELECT	*	   
		FROM Vent.View_tbSaleOrders_List as Sale
		INNER JOIN Vent.tbOrderDetails as SaleD ON Sale.sor_Id = SaleD.sor_Id
		INNER JOIN Vent.tbProducts AS prd ON prd.pro_Id =SaleD.pro_Id
		ORDER BY Sale.sor_Id desc
    ELSE IF (@Id=0)
	SELECT	*	   
	FROM   Vent.View_tbSaleOrders_List AS Sale
	INNER JOIN Vent.tbOrderDetails as SaleD ON Sale.sor_Id = SaleD.sor_Id
	INNER JOIN Vent.tbProducts AS prd ON prd.pro_Id =SaleD.pro_Id
	AND Sale.sor_DateCreate BETWEEN   @fechainicio  AND DATEADD(DAY, 1, @fechatermina) 
	ORDER BY Sale.sor_Id desc
	ELSE IF (@fechainicio='1950-01-01' AND @fechatermina='1950-01-02')
	SELECT	*	   
	FROM   Vent.View_tbSaleOrders_List AS Sale
	INNER JOIN Vent.tbOrderDetails as SaleD ON Sale.sor_Id = SaleD.sor_Id
	INNER JOIN Vent.tbProducts AS prd ON prd.pro_Id =SaleD.pro_Id
	AND Sale.cus_Id = @Id
	ORDER BY Sale.sor_Id desc
	ELSE IF (@Id!=0 AND @fechainicio!='1950-01-01' AND @fechatermina !='1950-01-02')
	SELECT	*	   
	FROM   Vent.View_tbSaleOrders_List AS Sale
	INNER JOIN Vent.tbOrderDetails as SaleD ON Sale.sor_Id = SaleD.sor_Id
	INNER JOIN Vent.tbProducts AS prd ON prd.pro_Id =SaleD.pro_Id
	AND Sale.cus_Id = @Id
	AND Sale.sor_DateCreate BETWEEN  @fechainicio AND DATEADD(DAY, 1, @fechatermina) 
	ORDER BY Sale.cus_Id desc
END
GO


/*Sección #120*/
CREATE OR ALTER PROCEDURE [Vent].[UDP_LastMontCampaigns_DateFilter]
--========================================================
--Author:       Ismael
--Create date:  21/06/2022
--Description:  Genera y filtra los datos de campañas
--		para los reportes por un rango de fecha 
--========================================================
@fechainicio DATETIME,
@fechatermina DATETIME
AS
    BEGIN
        SELECT COUNT(camD.cde_Id) as Cantidad,Cam.cam_Id,Cam.cam_Nombre as Nombre,Cam.cam_Descripcion as Descripcion,CASE cam.cam_Status WHEN 1 THEN 'Sin enviar' WHEN 0 THEN 'Enviada' END  'Status',Cam.cam_DateCreate as Fechacrea
        FROM Vent.tbCampaign as Cam 
        LEFT JOIN  Vent.tbCampaignDetails as camD on camD.cam_Id = Cam.cam_Id

        WHERE cam.cam_DateCreate BETWEEN @fechainicio  AND DATEADD(DAY, 1, @fechatermina) 
        GROUP BY cam.cam_Id,cam.cam_Nombre,cam.cam_Descripcion,cam.cam_Status,cam.cam_DateCreate
        ORDER BY Cantidad Desc

    END
GO


/*Sección #121*/
CREATE or ALTER PROCEDURE [Vent].[UDP_Reports_Cotizations_Dynamic]
--========================================================
--Author:       Gloria Medina
--Create date:  22/06/2022
--Description:  Este procedimiento trae la información 
--              necesaria para el reporte dinámico de
--              cotizaciones.
--========================================================
@Id INT,
@fechainicio DATETIME,
@fechatermina DATETIME
AS
BEGIN
    IF (@Id=0 AND @fechainicio='1950-01-01' AND @fechatermina ='1950-01-02')
    SELECT    *
    FROM   Vent.View_tbCotizations_List AS coti
    INNER JOIN Vent.tbCotizationsDetail AS cotd ON cotd.cot_Id = coti.cot_Id
    INNER JOIN Vent.tbProducts AS prd ON prd.pro_Id = cotd.pro_Id
    ORDER BY coti.cot_Id desc
    ELSE IF (@Id=0)
    SELECT    *
    FROM   Vent.View_tbCotizations_List AS coti
    INNER JOIN Vent.tbCotizationsDetail AS cotd ON cotd.cot_Id = coti.cot_Id
    INNER JOIN Vent.tbProducts AS prd ON prd.pro_Id = cotd.pro_Id
    AND coti.cot_DateCreate BETWEEN  @fechainicio  AND DATEADD(DAY, 1, @fechatermina) 
    ORDER BY coti.cot_Id desc
    ELSE IF (@fechainicio='1950-01-01' AND @fechatermina='1950-01-02')
    SELECT    *
    FROM   Vent.View_tbCotizations_List AS coti
    INNER JOIN Vent.tbCotizationsDetail AS cotd ON cotd.cot_Id = coti.cot_Id
    INNER JOIN Vent.tbProducts AS prd ON prd.pro_Id = cotd.pro_Id
    AND coti.cus_Id = @Id
    ORDER BY coti.cot_Id desc
    ELSE IF (@Id!=0 AND @fechainicio!='1950-01-01' AND @fechatermina !='1950-01-02')
    SELECT    *
    FROM   Vent.View_tbCotizations_List AS coti
    INNER JOIN Vent.tbCotizationsDetail AS cotd ON cotd.cot_Id = coti.cot_Id
    INNER JOIN Vent.tbProducts AS prd ON prd.pro_Id = cotd.pro_Id
    AND coti.cus_Id = @Id
    AND coti.cot_DateCreate BETWEEN  @fechainicio  AND DATEADD(DAY, 1, @fechatermina) 
    ORDER BY coti.cot_Id desc
END
GO


/*Sección #122*/
CREATE    PROCEDURE [Vent].[UDP_Reports_CotizationsDetail]
@cot_Id						INT
--========================================================
--Author:       Gloria Medina
--Create date:  22/06/2022
--Description: 	Este procedimiento permite traer los 
--		detalles de las cotizaciones por el
--		medio del Id.
--========================================================
AS
BEGIN
	SELECT		   coti.cot_Id AS [Id],
				   coti.cot_DateCreate AS [FechaCreacion],
				   coti.cot_DateValidUntil AS [ValidoHasta],
				   cust.cus_Id, 
				   cust.cus_Name,
				   cust.cus_Email,
				   cust.cus_RTN,
				   muni.mun_Description,
				   cust.cus_Phone,
				   stat.sta_Id,
				   stat.sta_Description,
				   prd.pro_Id AS [IdProduct],
				   prd.pro_Description AS [DescripcionProduct],
				   cotd.code_Cantidad AS [QuantityProducto],
				   prd.pro_SalesPrice AS [UnitPrice],
				   prd.pro_SalesPrice * cotd.code_Cantidad AS [LineTotal]
	FROM   [Vent].[tbCotizations] AS coti
	INNER JOIN Clte.tbCustomers AS cust ON coti.cus_Id = cust.cus_Id
	INNER JOIN Gral.tbMunicipalities AS muni ON cust.mun_Id = muni.mun_Id
	INNER JOIN Vent.tbCotizationsDetail AS cotd ON cotd.cot_Id = coti.cot_Id
	INNER JOIN Vent.tbProducts AS prd ON prd.pro_Id = cotd.pro_Id
	INNER JOIN Gral.tbStates AS stat ON stat.sta_Id = coti.sta_Id
	WHERE coti.cot_Status = 1 AND coti.cot_Id = @cot_Id
END
GO

CREATE OR ALTER PROCEDURE [Clte].[UDP_tbCustomers_last_30]
AS
BEGIN
SELECT *, CASE Cus.cus_receive_email WHEN 1 THEN 'Permitido' WHEN 0 THEN 'Denegado' END  'Promocion'  FROM [Clte].[View_tbCustomers_List] as Cus
WHERE DATEDIFF(day,cus_DateCreate,GETDATE()) < 31 AND Status = 'Activo'
END

GO


/*Sección #123*/
CREATE OR ALTER PROCEDURE [Clte].[UDP_Reports_Customers_Dynamic]
--========================================================
--Author:       Fernando E. Rios
--Create date:  22/06/2022
--Description:  Este procedimiento trae la información 
--              necesaria para el reporte de Customers
--========================================================
@Id INT,
@fechainicio DATETIME,
@fechatermina DATETIME
AS
BEGIN
    IF (@Id=0 AND @fechainicio='1950-01-01' AND @fechatermina ='1950-01-02')
    SELECT    *
    FROM   [Clte].[View_tbCustomers_List] AS cust
    ORDER BY cust.cus_Id desc
    ELSE IF (@Id=0)
    SELECT    *
    FROM   [Clte].[View_tbCustomers_List] AS cust Where
    cust.cus_DateCreate BETWEEN  @fechainicio  AND DATEADD(DAY, 1, @fechatermina) 
    ORDER BY cust.cus_Id desc
    ELSE IF (@fechainicio='1950-01-01' AND @fechatermina='1950-01-02')
    SELECT    *
    FROM   [Clte].[View_tbCustomers_List] AS cust
    WHERE cust.dep_Id  = @Id
    ORDER BY cust.cus_Id desc
    ELSE IF (@Id!=0 AND @fechainicio!='1950-01-01' AND @fechatermina !='1950-01-02')
    SELECT    *
    FROM   [Clte].[View_tbCustomers_List] AS cust
    WHERE
    cust.dep_Id  = @Id
    AND cust.cus_DateCreate BETWEEN @fechainicio  AND DATEADD(DAY, 1, @fechatermina) 
    ORDER BY cust.cus_Id desc
END
GO 


/*Sección #124*/

CREATE   PROCEDURE [Vent].[UDP_Reports_Details_ByOrder]
--========================================================
--Author:       Benkay Ham
--Create date:  22/06/2022
--Description:  Genera y filtra por el Id de la orden, los detalles de esa orden.
--========================================================
@Id INT
AS
BEGIN
SELECT * FROM Vent.tbOrderDetails Details
INNER JOIN Vent.View_tbSaleOrders_List as Sales on Sales.sor_Id=Details.sor_Id
INNER JOIN Vent.tbProducts as Pro on Details.pro_Id = Pro.pro_Id
WHERE Sales.sor_Id=@Id
AND Sales.Status='Activo'
END
GO


/*Sección #125*/
CREATE OR ALTER PROCEDURE Acce.UDP_tbRoleModuleItems_INSERT
--========================================================
--Author:       Jireh Aguilar
--Create date:  07/07/2022
--Description:  Procedimiento que inserta un nuevo 
--              registro a la tabla tbRoleModuleItems.
--========================================================
	  @mit_Id   INT,
	  @rol_Id   INT
 AS
 BEGIN
		INSERT INTO [Acce].[tbRoleModuleItems]
		([mit_Id]
		,[rol_Id])
		VALUES  
		(@mit_Id
		 ,@rol_Id)
        SELECT @@IDENTITY      
 END
GO


/*Sección #126*/
CREATE OR ALTER PROCEDURE Acce.UDP_tbRoleModuleItems_DELETE
--========================================================
--Author:       Jireh Aguilar
--Create date:  07/07/2022
--Description:  Procedimiento que elimina un 
--              registro a la tabla tbRoleModuleItems.
--========================================================
    @rol_Id    INT
AS
BEGIN
    DELETE 
    FROM	[Acce].[tbRoleModuleItems]
    WHERE   [rol_Id] = @rol_Id
END
GO

CREATE PROCEDURE Vent.UDP_tbProducts_Stock_UPDATE
--========================================================
--Author:       Mauricio Escalante
--Create date:  05/07/2022
--Description:  Procedimiento que modifica el stock
--              de la tabla Products.
--========================================================
    @pro_Id INT,
    @pro_Stock INT,
    @pro_IdUserModified INT
AS
    BEGIN
    UPDATE [Vent].[tbProducts]
   SET [pro_Stock] = @pro_Stock,  
       [pro_IdUserModified] = @pro_IdUserModified,
       [pro_DateModified] = GETDATE()
 WHERE pro_Id = @pro_Id
END
GO

