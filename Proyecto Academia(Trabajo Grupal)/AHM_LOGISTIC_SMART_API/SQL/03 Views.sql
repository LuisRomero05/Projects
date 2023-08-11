USE [LOGISTIC_SMART_AHM]
GO
--==================VISTAS==================

/*Sección #127*/
CREATE VIEW Acce.View_tbComponents_List
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  selecciona los 
--              componentes del sistema.
--========================================================
AS
        SELECT  [Comp].[com_Id],            
                [Comp].[com_Description], 
		CASE	[Comp].[com_Status] WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status'
        FROM    [Acce].[tbComponents]		AS [Comp]
		WHERE   [Comp].[com_Status] = 1
GO



/*Sección #128*/
CREATE VIEW Acce.View_tbModules_List
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Selecciona todos los registros de los 
--              m�dulos.
--========================================================
AS
        SELECT     [Mods].[mod_Id],
				   [Mods].[mod_Description],
                   [Comp].[com_Id],
                   [Comp].[com_Description],
		CASE	   [Mods].[mod_Status] WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status'		   
        FROM       [Acce].[tbModules]    AS [Mods]
        LEFT JOIN [Acce].[tbComponents] AS [Comp]
        ON         [Mods].[com_Id] = [Comp].[com_Id]
		WHERE      [Mods].[mod_Status] = 1
GO



/*Sección #129*/
CREATE VIEW Acce.View_tbModulosPantallas_List
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Selecciona todos los registros de las
--              pantallas.
--========================================================
AS
        SELECT  [Modite].mit_Id,
				[Modite].mit_Descripction,
                [Mod].[mod_Id],
                [Mod].[mod_Description],
        CASE    [Modite].[mit_Status] WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status'
        FROM    [Acce].[tbModuleItems] Modite LEFT JOIN [Acce].[tbModules] [Mod]
        ON      [Modite].[mod_Id]     = [Mod].[mod_Id]
		WHERE   [Modite].[mit_Status] = 1
GO



/*Sección #130*/
CREATE VIEW Acce.View_tbRoles_List
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Selecciona todos los roles activos.
--========================================================
AS

        SELECT  
                [roles].[rol_Id],                
                [roles].[rol_Description],
        CASE    [roles].[rol_Status] WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
				[roles].[rol_IdUserCreate],
				us.usu_UserName     AS usu_UserNameCreate,
				[roles].[rol_DateCreate],
				[roles].rol_IdUserModified,
				usm.usu_UserName AS usu_UserNameModified,
				[roles].rol_DateModified
        FROM    [Acce].[tbRoles]    AS roles 
		LEFT JOIN [Acce].[tbUsers] AS us ON roles.rol_IdUserCreate    = us.usu_Id
		LEFT JOIN [Acce].[tbUsers] AS usm ON roles.rol_IdUserModified  = usm.usu_Id
		WHERE   [roles].[rol_Status] = 1
GO



/*Sección #131*/
CREATE VIEW Acce.View_tbRoleModuleItems_List
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Selecciona todos los roles por modulos.
--========================================================
AS
        SELECT  
                rmi.rmi_Id,
				rol.rol_Description,
				mi.mit_Descripction
        FROM    [Acce].[tbRoleModuleItems] AS rmi 
		LEFT JOIN  [Acce].[tbRoles] AS rol ON rol.rol_Id=rmi.rol_Id
		LEFT JOIN  [Acce].[tbModuleItems] AS mi ON mi.mit_Id=rmi.mit_Id
GO


/*Sección #132*/
CREATE VIEW Gral.View_tbCountries_List
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Selecciona todos los Departamenntos.
--========================================================
AS
        SELECT  cou.cou_Id,
				cou.cou_Description,
		CASE    cou.cou_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
				cou.cou_IdUserCreate,
				us.usu_UserName AS usu_UserNameCreate,
				cou.cou_DateCreate,
				cou.cou_IdUserModified,
				usm.usu_UserName AS usu_UserNameModified,
				cou.cou_DateModified
        FROM    [Gral].[tbCountries] AS cou
		LEFT JOIN [Acce].[tbUsers] AS us ON cou.cou_IdUserCreate   = us.usu_Id
		LEFT JOIN [Acce].[tbUsers] AS usm ON cou.cou_IdUserModified = usm.usu_Id
		WHERE cou.cou_Status = 1
GO



/*Sección #133*/
CREATE VIEW Gral.View_tbDepartments_List
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Selecciona todos los Departamenntos.
--========================================================
AS
        SELECT  
                dep.dep_Id,
				dep.dep_Code,
				dep.dep_Description,
				cou.cou_Id,
				cou.cou_Description,
		CASE    dep.dep_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
				dep.dep_IdUserCreate,
				us.usu_UserName AS usu_UserNameCreate,
				dep.dep_DateCreate,
				dep.dep_IdUserModified,
				usm.usu_UserName AS usu_UserNameModified,
				dep.dep_DateModified
        FROM   [Gral].[tbDepartments]  AS dep 
		LEFT JOIN [Gral].[tbCountries] AS cou ON cou.cou_Id=dep.cou_Id
		LEFT JOIN [Acce].[tbUsers] AS us ON dep.dep_IdUserCreate   = us.usu_Id
		LEFT JOIN [Acce].[tbUsers] AS usm ON dep.dep_IdUserModified = usm.usu_Id
		WHERE dep.dep_Status = 1
GO



/*Sección #134*/
CREATE VIEW Gral.View_tbMunicipalities_List
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Selecciona todos los Municipios activos.
--========================================================
AS
        SELECT  mun.[mun_Id],
				mun.[mun_Code],
				mun.[mun_Description],
				mun.[dep_Id],
				dep.dep_Description,
		CASE    mun.[mun_Status] WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
				mun.[mun_IdUserCreate],
				us.usu_UserName AS usu_UserNameCreate,
                mun.mun_DateCreate,
				mun.mun_IdUserModified,
				usm.usu_UserName AS usu_UserNameModified,
				mun.mun_DateModified
        FROM   [Gral].[tbMunicipalities]  AS mun 
		LEFT JOIN [Acce].[tbUsers] AS us ON mun.mun_IdUserCreate   = us.usu_Id
		LEFT JOIN [Acce].[tbUsers] AS usm ON mun.mun_IdUserModified = usm.usu_Id
		LEFT JOIN [Gral].[tbDepartments] AS dep ON dep.dep_Id = mun.dep_Id
		WHERE mun.[mun_Status] = 1
GO

/*Sección #135*/
CREATE VIEW Gral.View_Areas_List
--========================================================
--Author:       Angel Teruel
--Create date:  26/05/2022
--Description:  Selecciona todos las areas activas.
--========================================================
AS 
	SELECT are.are_Id,
		   are.are_Description,
	CASE   are.are_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
		   are.are_IdUserCreate,
		   us.usu_UserName  AS usu_UserNameCreate,
		   are.are_DateCreate,
		   are.are_IdUserModified,
		   usm.usu_UserName AS usu_UserNameModified,
		   are.are_DateModified
	FROM   [Gral].[tbAreas] AS are
	LEFT JOIN Acce.tbUsers AS us ON are.are_IdUserCreate   = us.usu_Id
	LEFT JOIN Acce.tbUsers AS usm ON are.are_IdUserModified = usm.usu_Id
	WHERE are.are_Status = 1
GO

/*Sección #136*/
CREATE VIEW Gral.View_tbOccupations_List
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Selecciona todas las ocupaciones.
--========================================================
AS
        SELECT occ_Id,
			   occ_Description,
		CASE   occ_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
			   occ_IdUserCreate,
			   us.usu_UserName AS usu_UserNameCreate,
			   occ.occ_DateCreate,
			   occ.occ_IdUserModified,
			   usm.usu_UserName AS usu_UserNameModified,
			   occ.occ_DateModified
        FROM   [Gral].[tbOccupations]  AS  occ
		LEFT JOIN [Acce].[tbUsers] AS us ON occ.occ_IdUserCreate   = us.usu_Id
		LEFT JOIN [Acce].[tbUsers] AS usm ON occ.occ_IdUserModified = usm.usu_Id
		WHERE  occ_Status = 1
GO

/*Sección #137*/
CREATE VIEW Acce.View_tbUsers_List
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Muestra todos los usuarios registrados.
--========================================================
AS
		SELECT  usu.usu_Id, 
				usu.usu_UserName, 
				usu.usu_Password,
				usu.usu_Profile_picture,
				rol.rol_Id,
				rol.rol_Description,
				usu.Per_Id,
				usu.usu_Temporal_Password,
				per.per_Firstname +' '+ per.per_Secondname AS [per_PerName],
		CASE	usu.usu_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
				usu.usu_Status,
				usu.usu_IdUserCreate,
				us.usu_UserName AS usu_UserNameCreate,
				usu.usu_DateCreate,
				usu.usu_IdUserModified,
				usm.usu_UserName AS usu_UserNameModified,
				usu.usu_DateModified
		FROM Acce.tbUsers AS usu 
		LEFT JOIN Acce.tbRoles AS rol ON usu.rol_Id = rol.rol_Id
		LEFT JOIN Gral.tbPersons AS per ON per.per_Id = usu.per_Id
		LEFT JOIN [Acce].[tbUsers] AS us ON usu.usu_IdUserCreate      = us.usu_Id
		LEFT  JOIN [Acce].[tbUsers] AS usm ON usu.usu_IdUserModified   = usm.usu_Id
		WHERE usu.usu_Status = 1
GO

/*Sección #138*/
CREATE VIEW Vent.View_tbCategories_List
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Muestra el listado de la tabla Categorias.
--========================================================
AS
	SELECT cat.cat_Id,
		   cat.cat_Description,
	CASE   cat.cat_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
		   cat.cat_IdUserCreate,
		   us.usu_UserName AS usu_UserNameCreate,
		   cat.cat_DateCreate,
		   cat.cat_IdUserModified,
		   usm.usu_UserName AS usu_UserNameModified,
		   cat.cat_DateModified
	FROM [Vent].[tbCategories] AS cat 
	LEFT JOIN Acce.tbUsers AS us ON cat.cat_IdUserCreate    = us.usu_Id
	LEFT  JOIN Acce.tbUsers AS usm ON cat.cat_IdUserModified = usm.usu_Id
	WHERE cat.cat_Status = 1

GO

/*Sección #139*/
CREATE VIEW [Clte].[View_tbCustomers_List]
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  muestra el listado de la tabla customers.
--========================================================
AS
		SELECT	cus.cus_Id					AS cus_Id,
				cus.cus_Name				AS cus_Name,
				cus.cus_AssignedUser		AS cus_AssignedUser,
				cus.tyCh_Id					AS tyCh_Id,
				typ.tyCh_Description		AS tyCh_Description,
				cus.cus_RTN					AS cus_RTN,
				cus.cus_Address				AS cus_Address,
				cus.dep_Id					AS dep_Id,
				dep.dep_Description			AS dep_Description,
				cus.mun_Id					AS mun_Id,
				mun.mun_Description			AS mun_Description,
				cus.cus_Email				AS cus_Email,
				cus.cus_receive_email		AS cus_receive_email,
				cus_Phone					AS cus_Phone,
				cus_AnotherPhone			AS cus_AnotherPhone,
		CASE	cus_Status					   WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
		CASE	cus_Active 					WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Estado',
				cus_IdUserCreate			AS cus_IdUserCreate,
				usu.usu_UserName            AS usu_UserNameCreate,
				cus_DateCreate				AS cus_DateCreate,
				cus_IdUserModified			AS cus_IdUserModified,
				usm.usu_UserName            AS usu_UserNameModified,
				cus_DateModified			AS cus_DateModified
		FROM	Clte.tbCustomers AS cus	
				LEFT JOIN Gral.tbMunicipalities		AS mun	ON mun.mun_Id	= cus.mun_Id
				LEFT JOIN Gral.tbDepartments		AS dep	ON cus.dep_Id	= dep.dep_Id
				LEFT JOIN Clte.tbTypeChannels		AS typ  ON typ.tyCh_Id	= cus.tyCh_Id
				LEFT JOIN Acce.tbUsers				AS usu	ON cus.cus_IdUserCreate   = usu.usu_Id
				LEFT JOIN Acce.tbUsers				AS usm	ON cus.cus_IdUserModified = usm.usu_Id
		WHERE   cus_Status = 1
GO


/*Sección #140*/
CREATE VIEW Clte.View_tbCustomers_tbEmployees_List
--========================================================
--Author:       Angel Rapalo
--Create date:  29/05/2022
--Description:  muestra el listado de la tabla customers, empleados y contactos.
--========================================================
AS
		SELECT	CAST(cus.cus_Id + .0 AS NVARCHAR)											AS Id,
				cus.cus_Name + ' - Cliente'													AS [Name],
		CASE	cus_Status																	WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status'
		FROM	Clte.tbCustomers AS cus	
		WHERE   cus_Status = 1
		UNION 
		SELECT	CAST(emp.emp_Id + .1 AS NVARCHAR),			
				per.per_Firstname + ' ' + per.per_LastNames + ' - ' + occ.occ_Description,
		CASE	emp.emp_Status																WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END
		FROM Gral.tbEmployees AS emp LEFT JOIN Gral.tbPersons								AS per ON per.per_Id = emp.per_Id
									 LEFT JOIN Gral.tbOccupations							AS occ ON emp.occ_Id = occ.occ_Id
		UNION
		SELECT	CAST(cont.cont_Id + .2 AS NVARCHAR),
				cont.cont_Name + ' - Contacto',
		CASE	cont.cont_Status															WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END
		FROM Clte.tbContacts AS cont
		WHERE cont_Status = 1
GO

/*Sección #141*/
CREATE VIEW Vent.View_tbSubCategories_List
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  muestra los registros de la SubCategories.
--========================================================
AS
		SELECT	sbc.scat_Id             AS [scat_Id],
				sbc.scat_Description    AS [scat_Description],
				cat.cat_Id				AS [cat_Id],
				cat.cat_Description     AS [cat_Description],
		CASE	sbc.scat_Status         WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
				sbc.scat_IdUserCreate   AS [scat_IdUserCreate],
				us.usu_UserName         AS [usu_UserNameCreate],
				sbc.scat_DateCreate     AS [scat_DateCreate],
				sbc.scat_IdUserModified AS [scat_IdUserModified],
				usm.usu_UserName        AS [usu_UserNameModified],
				sbc.scat_DateModified   AS [scat_DateModified]
		FROM Vent.tbSubCategories       AS sbc	  LEFT JOIN Vent.tbCategories AS cat ON cat.cat_Id         = sbc.cat_Id
											      LEFT JOIN Acce.tbUsers AS us ON sbc.scat_IdUserCreate    = us.usu_Id 
											      LEFT  JOIN Acce.tbUsers AS usm ON sbc.scat_IdUserModified = usm.usu_Id 
		WHERE sbc.scat_Status = 1
GO

/*Sección #142*/
CREATE VIEW Clte.View_tbContacts_List
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  muestra los registros de la tabla Contacts.
--========================================================
AS
		SELECT	cont.cont_Id              AS [cont_Id],
				cont.cont_Name,
				cont.cont_LastName,
				cont.cont_Email           AS [cont_Email],
				cont.cont_Phone           AS [cont_Phone],
				occ.occ_Id                AS [occ_Id],
				occ.occ_Description       AS [occ_Description],
				cus.cus_Id                AS [cus_Id],
				cus.cus_Name              AS [cus_Name], 
		CASE	cont.cont_Status          WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
				cont.cont_IdUserCreate    AS [cont_IdUserCreate],
				us.usu_UserName           AS [usu_UserNameCreate],
				cont.cont_DateCreate      AS [cont_DateCreate],
				cont.cont_IdUserModified  AS [cont_IdUserModified],
				usm.usu_UserName          AS [usu_UserNameModified],
				cont.cont_DateModified    AS [cont_DateModified]
		FROM Clte.tbContacts AS cont 
		LEFT JOIN Gral.tbOccupations AS occ ON occ.occ_Id = cont.occ_Id
		LEFT JOIN Clte.tbCustomers AS cus ON cus.cus_Id = cont.cus_Id
		LEFT JOIN Acce.tbUsers AS us ON cont.cont_IdUserCreate       = us.usu_Id
		LEFT  JOIN Acce.tbUsers AS usm ON cont.cont_IdUserModified    = usm.usu_Id
		WHERE cont.cont_Status = 1
GO

/*Sección #143*/
CREATE VIEW Gral.View_tbEmployees_List
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  muestra los registros de la tabla Employees.
--========================================================
AS
		SELECT	emp.emp_Id AS [emp_Id],
				per.per_Id AS [per_Id],
				per.per_Firstname AS [per_Firstname],
				per.per_LastNames AS [per_LastNames],
				are.are_Id AS [are_Id],
				are.are_Description AS [are_Description],
				occ.occ_Id AS [occ_Id],
				occ.occ_Description AS [occ_Description],
		CASE	emp.emp_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
				emp.emp_IdUserCreate AS [emp_IdUserCreate],
				US.usu_UserName AS usu_UserNameCreate,
				emp.emp_DateCreate AS [emp_DateCreate],
				emp.emp_IdUserModified AS [emp_IdUserModified],
				usm.usu_UserName AS usu_UserNameModified,
				emp.emp_DateModified AS [emp_DateModified]
		FROM Gral.tbEmployees AS emp	LEFT JOIN Gral.tbPersons AS per ON per.per_Id = emp.per_Id
										LEFT JOIN Gral.tbAreas AS are ON are.are_Id = emp.are_Id
										LEFT JOIN Gral.tbOccupations AS occ ON occ.occ_Id = emp.occ_Id
										LEFT JOIN Acce.tbUsers AS us ON emp.emp_IdUserCreate          = us.usu_Id
										LEFT  JOIN Acce.tbUsers AS usm ON emp.emp_IdUserModified       = usm.usu_Id
	    WHERE emp.emp_Status = 1
									
GO

/*Sección #144*/
CREATE VIEW Vent.View_tbProducts_List
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  muestra los registros de la tabla Products.
--========================================================
AS
	SELECT  pro.pro_Id,
		    pro.pro_Description,
			pro.pro_PurchasePrice,
			pro.pro_SalesPrice,
			pro.pro_Stock,
			pro.pro_ISV,
			pro.uni_Id,
			uni.uni_Description,
			subc.scat_Description,
			pro.scat_Id,
	CASE	pro.pro_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
			pro.pro_IdUserCreate,
			us.usu_UserName AS usu_UserNameCreate,
			pro.pro_DateCreate,
			pro.pro_IdUserModified,
			usm.usu_UserName AS usu_UserNameModified,
			pro.pro_DateModified
		
	FROM [Vent].[tbProducts] AS pro
	LEFT JOIN [Vent].[tbUnits] AS uni ON uni.uni_Id=pro.uni_Id
	LEFT JOIN [Vent].[tbSubCategories] AS subc ON subc.scat_Id=pro.scat_Id
	LEFT JOIN Acce.tbUsers AS us ON pro.pro_IdUserCreate      = us.usu_Id
	LEFT  JOIN Acce.tbUsers AS usm ON pro.pro_IdUserModified   = usm.usu_Id
	WHERE pro.pro_Status = 1
GO

/*Sección #145*/
CREATE VIEW Vent.View_tbPersons_List
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  muestra los registros de la tabla Persons.
--========================================================
AS
	SELECT  per.[per_Id],
			per.[per_Identidad],
			per.[per_Firstname],
			per.[per_Secondname],
			per.[per_LastNames],
			per.[per_BirthDate],
			per.[per_Sex],
			per.[per_Email],
			per.[per_Phone],
			per.[per_Direccion],
			per.[dep_Id],
			dep.dep_Description,
			per.[mun_Id],
			mun.mun_Description,
			per.[per_Esciv],
	CASE	per.[per_Status] WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
			per.[per_IdUserCreate],
			us.usu_UserName AS usu_UserNameCreate,
			per.[per_DateCreate],
			per.per_IdUserModified,
			usm.usu_UserName AS usu_UserNameModified,
			per.per_DateModified
		
	FROM [Gral].[tbPersons] AS per
	LEFT JOIN [Gral].[tbDepartments] AS dep ON per.dep_Id=dep.dep_Id
	LEFT JOIN [Gral].[tbMunicipalities] AS mun ON mun.mun_Id=per.mun_Id
	LEFT JOIN Acce.tbUsers AS us ON per.per_IdUserCreate      = us.usu_Id
	LEFT  JOIN Acce.tbUsers AS usm ON per.per_IdUserModified   = usm.usu_Id
	WHERE per.[per_Status] = 1
GO

/*Sección #146*/
CREATE VIEW Vent.View_tbCotizations_List
--========================================================
--Author:       Angel Teruel
--Create date:  29/05/2022
--Description:  Selecciona todas la cotizaciones.
--========================================================
AS
        SELECT coti.cot_Id,
			   coti.cus_Id,
			   cus.cus_Name,
			   cus.cus_Email,
			   cus.cus_Phone,
			   cus.cus_RTN,
			   cus.mun_Id,
			   mun.mun_Description,
			   coti.cot_DateValidUntil,
			   coti.sta_Id,
			   sta.sta_Description,
		CASE   coti.cot_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
			   coti.cot_IdUserCreate,
			   us.usu_UserName AS usu_UserNameCreate,
			   coti.cot_DateCreate,
			   coti.cot_IdUserModified,
			   usm.usu_UserName AS usu_UserNameModified,
			   coti.cot_DateModified
        FROM   [Vent].[tbCotizations]  AS  coti
		LEFT JOIN Clte.tbCustomers AS cus ON cus.cus_Id			= coti.cus_Id
		LEFT JOIN [Gral].[tbStates] AS sta ON sta.sta_Id			= coti.sta_Id
		LEFT JOIN Gral.tbMunicipalities AS mun ON mun.mun_Id		= cus.mun_Id
		LEFT JOIN Acce.tbUsers AS us ON coti.cot_IdUserCreate		= us.usu_Id
		LEFT  JOIN Acce.tbUsers AS usm ON coti.cot_IdUserModified	= usm.usu_Id
		WHERE coti.cot_Status = 1
GO



/*Sección #147*/
CREATE VIEW Vent.View_tbSaleOrders_List
--========================================================
--Author:       Angel Rapalo
--Create date:  04/06/2022
--Description:  Selecciona el listado de ordenes de venta.
--========================================================
AS
        SELECT	sor.sor_Id,
				sor.cus_Id,
			    cus.cus_Name,
			    cus.cus_Email,
			    cus.cus_Phone,
			    cus.cus_RTN,
			    cus.mun_Id,
			    mun.mun_Description,
				sor.cot_Id,
         CASE   coti.cot_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Coti_Status',
				sor.sta_Id,
				sta.sta_Description,
		CASE    sor.sor_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
				sor.sor_IdUserCreate,
				usc.usu_UserName AS usu_UserNameCreate,
				sor.sor_DateCreate,
				sor.sor_IdUserModified,
				usm.usu_UserName AS usu_UserNameModified,
				sor.sor_DateModified
        FROM Vent.tbSaleOrders AS sor
		LEFT JOIN Clte.tbCustomers AS cus ON cus.cus_Id = sor.cus_Id
		LEFT JOIN Vent.tbCotizations AS coti ON coti.cot_Id = sor.cot_Id
		LEFT JOIN Gral.tbStates AS sta ON sta.sta_Id = sor.sta_Id
		LEFT JOIN Acce.tbUsers AS usc ON usc.usu_Id = sor.sor_IdUserCreate
		LEFT JOIN Gral.tbMunicipalities AS mun ON cus.mun_Id = mun.mun_Id
		LEFT JOIN Acce.tbUsers AS usm ON usm.usu_Id = sor.sor_IdUserModified
		WHERE   sor.sor_Status = 1
GO

/*Sección #148*/
CREATE VIEW Vent.View_tbUnits_List
--========================================================
--Author:       Angel Teruel
--Create date:  04/06/2022
--Description:  Selecciona el listado de Unidades.
--========================================================
AS
        SELECT	uni_Id,
				uni_Description,
				uni_Abrevitation,
		CASE    uni_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
				uni_IdUserCreate,
				us.usu_UserName AS usu_UserNameCreate,
				uni_DateCreate,
				uni_IdUserModified,
				usm.usu_UserName AS usu_UserNameModified,
				uni_DateModified
        FROM [Vent].[tbUnits] AS uni
		LEFT JOIN Acce.tbUsers AS us ON uni.uni_IdUserCreate     = us.usu_Id
		LEFT  JOIN Acce.tbUsers AS usm ON uni.uni_IdUserModified  = usm.usu_Id
		WHERE uni_Status = 1
GO

/*Sección #149*/
CREATE VIEW Acce.View_Screens_List
--========================================================
--Author:       Benkay Ham
--Create date:  06/06/2022
--Description:  Selecciona el listado de las pantallas del Rol.
--========================================================
AS
        SELECT	modu.mit_Id,
		        modu.mod_Id,
				modu.mit_Descripction,
		CASE    modu.mit_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status'
        FROM [Acce].[tbModuleItems] AS modu
		LEFT JOIN Acce.tbModules AS modules ON modu.mod_Id  = modules.mod_Id
		WHERE mit_Status = 1
GO

/*Sección #150*/
CREATE VIEW Acce.View_ScreensPerRole_List
--========================================================
--Author:       Benkay Ham
--Create date:  06/06/2022
--Description:  Selecciona el listado de las pantallas del Rol.
--========================================================
AS
        SELECT	modu.*,
		        rol.rol_Description,
				moduItm.mit_Descripction
        FROM [Acce].[tbRoleModuleItems] AS modu
		LEFT JOIN Acce.tbRoles AS rol ON rol.rol_Id  = modu.rol_Id
		LEFT JOIN Acce.tbModuleItems AS moduItm ON moduItm.mit_Id = modu.mit_Id
GO

/*Sección #151*/
CREATE VIEW Acce.View_tbSalesDetails_List
--========================================================
--Author:       Benkay Ham
--Create date:  06/06/2022
--Description:  Selecciona el listado de las pantallas del Rol.
--========================================================
AS
        SELECT	Sale.*,
		        OrderD.ode_Id,
				OrderD.ode_Amount,
				OrderD.pro_Id,
				OrderD.ode_TotalPrice,
				OrderD.ode_Status,
				OrderD.ode_IdUserCreate,
				OrderD.ode_DateCreate,
				OrderD.ode_IdUserModified,
				OrderD.ode_DateModified
        FROM Vent.tbSaleOrders AS Sale
		LEFT JOIN Vent.tbOrderDetails AS OrderD ON OrderD.sor_Id  = Sale.sor_Id

GO

/*Sección #152*/
CREATE VIEW Acce.View_tbCotizationsDetails_List
--========================================================
--Author:       Benkay Ham
--Create date:  06/06/2022
--Description:  Selecciona el listado de los detallesde la cotizacion.
--========================================================
AS
        SELECT	Coti.*,
		        CotiD.code_Id,
				CotiD.code_Cantidad,
				CotiD.pro_Id,
				CotiD.code_TotalPrice,
				CotiD.code_Status,
				CotiD.code_IdUserCreate,
				CotiD.code_DateCreate,
				CotiD.code_IdUserModified,
				CotiD.code_DateModified
        FROM Vent.tbCotizations AS Coti
		LEFT JOIN Vent.tbCotizationsDetail AS CotiD ON Coti.cot_Id  = CotiD.cot_Id

GO

/*Sección #153*/
CREATE VIEW Clte.View_tbCustomerCalls
--========================================================
--Author:       Angel Rapalo
--Create date:  08/06/2022
--Description:  Selecciona el listado de las llamadas de cliente.
--========================================================
AS
	SELECT	cca.cca_Id				AS cca_Id,
			cca.cca_CallType		AS cca_CallType,
			cat.cati_Description	AS cati_Description,
			cca.cca_Business		AS cca_Business,
			cab.cabu_Description	AS cabu_Description,
			cca.cca_Date			AS cca_Date,
			cca.cca_StartTime		AS cca_StartTime,	
			cca.cca_EndTime		    AS cca_EndTime,		
			cca.cca_Result			AS cca_Result,
			car.caru_Description	AS caru_Description,
			cca.cus_Id				AS cus_Id,
			cus.cus_Name			AS cus_Name,
	CASE    cca.cca_Status			WHEN 1 THEN 'Completado' WHEN 0 THEN 'Incompletado' END  'Status',
			cca.cca_IdUserCreate	AS cca_IdUserCreate,
			usc.usu_UserName		AS cca_UserNameCreate,
			cca.cca_DateCreate		AS cca_DateCreate
	FROM Clte.tbCustomerCalls		AS cca
	LEFT JOIN Clte.tbCustomers		AS cus ON cus.cus_Id = cca.cus_Id
	LEFT JOIN Clte.tbCallType		AS cat ON cca.cca_CallType=cat.cati_Id
	LEFT JOIN Clte.tbCallBusiness	AS cab ON cca.cca_Business = cab.cabu_Id
	LEFT JOIN Clte.tbCallResult	AS car ON cca.cca_Result = car.caru_Id
	LEFT JOIN Acce.tbUsers			AS usc ON usc.usu_Id = cca.cca_IdUserCreate
	WHERE cca.cca_Status = 1
GO


/*Sección #154*/
CREATE VIEW Clte.View_tbMeetings_List
--========================================================
--Author:       Benkay Ham
--Create date:  14/06/2022
--Description:  Selecciona el listado de las reuniones del cliente
--========================================================
AS
       SELECT met.met_Id,
			  met.met_Title,
			  met.met_MeetingLink,
			  met.cus_Id,
			  cus.cus_Name,
			  met.met_Date,
			  met.met_StartTime,
			  met.met_EndTime,
		CASE  met.met_Status WHEN 1 THEN 'Completado' WHEN 0 THEN 'Incompletado' END  'Status',
			  met.met_IdUserCreate,
			  met.met_DateCreate
	   FROM Clte.tbMeetings AS met
	   LEFT JOIN Clte.tbCustomers	AS cus ON cus.cus_Id = met.cus_Id
	   LEFT JOIN Acce.tbUsers		AS usc ON usc.usu_Id = met.met_IdUserCreate
	   WHERE [met_Status] = 1
GO


/*Sección #155*/
CREATE VIEW Clte.View_tbCustomerNotes
--========================================================
--Author:       Angel Rapalo
--Create date:  08/06/2022
--Description:  Selecciona el listado de las notas de cliente.
--========================================================
AS
	SELECT	cun.cun_Id				AS cun_Id,
			cun.cun_Descripcion		AS cun_Descripcion,
			cun.cun_ExpirationDate	AS cun_ExpirationDate,
			cun.pry_Id				AS pry_Id,
			pry.pry_Descripcion		AS pry_Descripcion,
			cun.cus_Id				AS cus_Id,
			cus.cus_Name			AS cus_Name,
	CASE	cun.cun_Status			WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
			cun.cun_IdUserCreate	AS cun_IdUserCreate,
			usc.usu_UserName		AS cun_UserNameCreate,
			cun.cun_DateCreate		AS cun_DateCreate,
			cun.cun_IdUserModified	AS cun_IdUserModified,
			usm.usu_UserName		AS cun_UserNameModified,
			cun.cun_DateModified	AS cun_DateModified
	FROM Clte.tbCustomerNotes		AS cun
	LEFT JOIN Clte.tbPriorities	AS pry ON pry.pry_Id = cun.pry_Id
	LEFT JOIN Clte.tbCustomers		AS cus ON cus.cus_Id = cun.cus_Id
	LEFT JOIN Acce.tbUsers			AS usc ON usc.usu_Id = cun.cun_IdUserCreate
	LEFT JOIN Acce.tbUsers			AS usm ON usm.usu_Id = cun.cun_IdUserModified
	WHERE cun_Status = 1
GO


/*Sección #156*/
CREATE VIEW Vent.View_tbPriorities_List
--========================================================
--Author:       Angel Rapalo
--Create date:  04/06/2022
--Description:  Selecciona el listado de Prioridades.
--========================================================
AS
        SELECT	pry_Id,
				pry_Descripcion,
		CASE    pry_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
				pry_IdUserCreate,
				usc.usu_UserName AS pry_UserNameCreate,
				pry_DateCreate,
				pry_IdUserModified,
				usm.usu_UserName AS pry_UserNameModified,
				pry_DateModified
        FROM Clte.tbPriorities AS pry
		LEFT JOIN Acce.tbUsers AS usc ON pry.pry_IdUserCreate     = usc.usu_Id
		LEFT  JOIN Acce.tbUsers AS usm ON pry.pry_IdUserModified  = usm.usu_Id
		WHERE pry_Status = 1
GO

/*Sección #157*/
CREATE VIEW Vent.View_tbCampaign_List
--========================================================
--Author:       Benkay Ham
--Create date:  13/06/2022
--Description:  Selecciona el listado de Campañas
--========================================================
AS
        SELECT cam.cam_Id,cam.cam_Nombre,cam.cam_Descripcion,cam.cam_Html,cam.cam_IdUserCreate,cam.cam_DateCreate,CASE cam_Status WHEN 1 THEN 'Sin enviar' WHEN 0 THEN 'Enviada' END  'Status',users.usu_UserName
        FROM Vent.tbCampaign AS cam
		LEFT JOIN Acce.tbUsers AS users ON users.usu_Id = cam.cam_IdUserCreate

GO

/*Sección #158*/
CREATE VIEW Vent.View_tbCampaignDetails_List
--========================================================
--Author:       Benkay Ham
--Create date:  13/06/2022
--Description:  Selecciona el listado de Campa�as
--========================================================
AS
       SELECT Detail.cde_Id,Detail.cus_Id,Detail.cam_Id,Cam.cam_Nombre,Cam.cam_Descripcion,Cam.cam_Html,Cam.cam_Status,Cam.cam_IdUserCreate,Cus.cus_Name,Cus.cus_Email,Cus.cus_Phone,Cus.cus_IdUserCreate
	   FROM Vent.tbCampaignDetails Detail
	   LEFT JOIN Vent.tbCampaign AS Cam ON Cam.cam_Id = Detail.cam_Id 
	   LEFT JOIN Clte.tbCustomers AS Cus ON Detail.cus_Id = Cus.cus_Id
GO

/*Sección #159*/
CREATE VIEW Clte.View_tbCallType_List
--========================================================
--Author:       Angel Teruel
--Create date:  14/06/2022
--Description:  Selecciona el listado de los tipos de llamadas
--========================================================
AS
       SELECT [cati_Id],
[cati_Description],
[cati_Status]
	   FROM [Clte].[tbCallType]

GO


/*Sección #160*/
CREATE VIEW Clte.View_tbCallBusiness_List
--========================================================
--Author:       Angel Teruel
--Create date:  14/06/2022
--Description:  Selecciona el listado de los bisness de llamadas
--========================================================
AS
       SELECT [cabu_Id],
[cabu_Description],
[cacabu_Status]
	   FROM [Clte].[tbCallBusiness]

GO

/*Sección #161*/
CREATE VIEW Clte.View_tbCallResult_List
--========================================================
--Author:       Angel Teruel
--Create date:  14/06/2022
--Description:  Selecciona el listado de los resultados de llamadas
--========================================================
AS
       SELECT [caru_Id],
			  [caru_Description],
			  [caru_Status]
	   FROM [Clte].[tbCallResult]

GO


/*Sección #162*/
CREATE VIEW Clte.View_tbCustomersFile_List
--========================================================
--Author:       Angel Rapalo
--Create date:  08/06/2022
--Description:  Selecciona el listado de los archivos de cliente.
--========================================================
AS
	SELECT	cfi.cfi_Id,
			cfi.cfi_fileRoute,
			cfi.cus_Id,
			cus.cus_Name,
	CASE	cfi.cfi_Status			WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
			cfi.cfi_DateCreate
	FROM Clte.tbCustomersFile		AS cfi
	LEFT JOIN Clte.tbCustomers		AS cus ON cus.cus_Id = cfi.cus_Id
	WHERE cfi_Status = 1
GO


/*Sección #163*/
CREATE VIEW Acce.View_UserPermits_SELECT
--========================================================
--Author:       Mauricio Escalante
--Create date:  07/07/2022
--Description:  Vista que muestra las 
--              pantallas filtradas por el rol del usuario.
--========================================================   
AS
    SELECT  T2.usu_UserName,
			T3.mit_Descripction

    FROM [Acce].[tbRoleModuleItems] T1 INNER JOIN [Acce].[tbUsers] T2
    ON T1.rol_Id = T2.rol_Id INNER JOIN [Acce].[tbModuleItems] T3
    ON T1.mit_Id = T3.mit_Id
GO