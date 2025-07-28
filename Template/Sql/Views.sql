USE [Template]
GO	

CREATE VIEW Gral.View_Areas_List
--========================================================
--Author:       Luis Romero
--Create date:  27/07/2022
--Description:  Selecciona General
--========================================================
AS 
	SELECT are.are_Id,
		   are.are_Description,
	CASE   are.are_Status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END  'Status',
		   are.are_IdUserCreate,
		  -- us.usu_UserName  AS usu_UserNameCreate,
		   are.are_DateCreate,
		   are.are_IdUserModified,
		   --usm.usu_UserName AS usu_UserNameModified,
		   are.are_DateModified
	FROM   [Gral].[tbAreas] AS are
	--LEFT JOIN Acce.tbUsers AS us ON are.are_IdUserCreate   = us.usu_Id
	--LEFT JOIN Acce.tbUsers AS usm ON are.are_IdUserModified = usm.usu_Id
	WHERE are.are_Status = 1
GO