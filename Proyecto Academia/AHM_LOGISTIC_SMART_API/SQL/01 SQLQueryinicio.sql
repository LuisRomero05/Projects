--========================================================
----IMPORTANTE
----1. CREAR LA BASE DE DATOS Y NO DESCOMENTARLO.
----2. EJECUTAR EL USE.
----3  EJECUTAR EL ALTER DATABASE Y NO DESCOMENTARLO.
----4. EJECUTAR TODO EL SCRIPTS.
--========================================================

/*CONFIGURACI�N INICIAL DEL SISTEMA*/
/*Sección INICIO*/

CREATE DATABASE [LOGISTIC_SMART_AHM]   
GO 
USE [LOGISTIC_SMART_AHM]
GO	
ALTER DATABASE [LOGISTIC_SMART_AHM] SET ENABLE_BROKER
GO 

--===============ESQUEMAS================
/*Sección #1*/
CREATE SCHEMA [Gral]
GO
CREATE SCHEMA [Acce]
GO
CREATE SCHEMA [Vent]
GO
CREATE SCHEMA [Publ]
GO
CREATE SCHEMA [Clte]
GO
CREATE SCHEMA [Logs]

--===============TABLAS==================
/*Sección #2*/
CREATE TABLE [Acce].tbComponents(
	[com_Id]            INT IDENTITY(1,1),
	[com_Description]   NVARCHAR(150)		NOT NULL,
	[com_Status]		BIT					NOT NULL,	 

	CONSTRAINT  PK_Acce_tbComponentes_comp_Id  PRIMARY KEY(com_Id)
);
GO


/*Sección #3*/
CREATE TABLE [Acce].tbModules(
	[mod_Id]           INT IDENTITY(1,1),
	[com_Id]           INT					NOT NULL,
	[mod_Description]  NVARCHAR(50)			NOT NULL,
	[mod_Status]	   BIT

	CONSTRAINT PK_Acce_tbModules_mod_Id				  PRIMARY KEY(mod_Id),
	CONSTRAINT FK_Acce_tbComponents_tbModules_comp_Id FOREIGN KEY (com_Id) REFERENCES [Acce].tbComponents (com_Id)
);
GO

/*Sección #4*/
CREATE TABLE [Acce].tbModuleItems
(
	[mit_Id]                 INT IDENTITY(1,1),
	[mod_Id]                  INT				NOT NULL,
	[mit_Descripction]       NVARCHAR(150)		NOT NULL,
	[mit_Status]			 BIT

	CONSTRAINT PK_Acce_tbModulosPantallas_modpt_Id		PRIMARY KEY(mit_Id),
	CONSTRAINT FK_Acce_tbModulos_tbModuleItems_mod_Id FOREIGN KEY (mod_Id) REFERENCES [Acce].tbModules (mod_Id)
);
GO

/*Sección #5*/
CREATE TABLE [Acce].tbRoles
(
	[rol_Id]              INT IDENTITY(1,1),
	[rol_Description]     NVARCHAR(50)		NOT NULL,
	[rol_Status]		  BIT				NOT NULL,
	[rol_IdUserCreate]    INT				 NULL,
	[rol_DateCreate]      DATETIME			 NULL,
	[rol_IdUserModified]  INT				NULL,
	[rol_DateModified]    DATETIME			NULL,
	

    CONSTRAINT  PK_Acce_tbRoles_rol_Id			PRIMARY KEY(rol_Id),
	CONSTRAINT  UQ_Acce_tbRoles_rol_Description UNIQUE (rol_Description)
);
GO

/*Sección #6*/
CREATE TABLE [Acce].tbRoleModuleItems(
	[rmi_Id]				INT IDENTITY(1,1),
	[mit_Id]				INT			NOT NULL,
	[rol_Id]				INT			NOT NULL,

	CONSTRAINT  PK_Acce_tbRolModulesScreen_rmi_Id					PRIMARY KEY (rmi_Id  ),
	CONSTRAINT  FK_Acce_tbModuleItems_tbRolModulesScreen_mit_Id  FOREIGN KEY (mit_Id) REFERENCES Acce.tbModuleItems (mit_Id),
	CONSTRAINT  FK_Acce_tbRoles_tbRolModulesScreen_rol_Id			FOREIGN KEY (rol_Id) REFERENCES [Acce].tbRoles (rol_Id)
);
GO


/*Sección #7*/
CREATE TABLE [Gral].tbCountries
(
	[cou_Id]              INT  IDENTITY(1,1),
	[cou_Description]     NVARCHAR(100)		NOT NULL, 
	[cou_Status]          BIT               NOT NULL,
	[cou_IdUserCreate]    INT				 NULL,
	[cou_DateCreate]      DATETIME			 NULL,
	[cou_IdUserModified]  INT				NULL,
	[cou_DateModified]    DATETIME			NULL,	
	CONSTRAINT  PK_Gral_tbPais_depto_Id			 PRIMARY KEY (cou_Id),
);
GO


/*Sección #8*/
CREATE TABLE [Gral].tbDepartments
(
	[dep_Id]              INT  IDENTITY(1,1),
	[dep_Code]            VARCHAR(2)		NOT NULL,
	[dep_Description]     NVARCHAR(100)		NOT NULL,
	[cou_Id]			  INT				NOT NULL,
	[dep_Status]          BIT               NOT NULL,
	[dep_IdUserCreate]    INT				 NULL,
	[dep_DateCreate]      DATETIME			 NULL,
	[dep_IdUserModified]  INT				NULL,
	[dep_DateModified]    DATETIME			NULL,		
	CONSTRAINT  PK_Gral_tbDepartments_depto_Id			 PRIMARY KEY (dep_Id),
	CONSTRAINT  FK_Gral_tbDepartments_tbPais_pai_Id		 FOREIGN KEY(cou_Id) REFERENCES Gral.tbCountries(cou_Id)
);
GO

/*Sección #9*/

CREATE TABLE [Gral].tbMunicipalities
(
	[mun_Id]              INT IDENTITY(1,1),
	[mun_Code]            VARCHAR(4)		NOT NULL,
	[mun_Description]     NVARCHAR(100)		NOT NULL,
	[dep_Id]              INT				NOT NULL,
	[mun_Status]          BIT         		NOT NULL,
	[mun_IdUserCreate]    INT				 NULL,
	[mun_DateCreate]      DATETIME			 NULL,
	[mun_IdUserModified]  INT				NULL,
	[mun_DateModified]    DATETIME			NULL,	
	CONSTRAINT  PK_Gral_tbMunicipalities_mun_Id				  PRIMARY KEY(mun_Id, dep_Id),
	CONSTRAINT  FK_Gral_tbDepartments_tbMunicipalities_dep_Id FOREIGN KEY(dep_Id) REFERENCES [Gral].tbDepartments(dep_Id)
);
GO

/*Sección #10*/
CREATE TABLE [Gral].tbPersons(
	[per_Id]              INT IDENTITY(1,1),
	[per_Identidad]       VARCHAR(13)		NOT NULL,
	[per_Firstname]       NVARCHAR(20)		NOT NULL,
	[per_Secondname]      NVARCHAR(20)		NULL,
	[per_LastNames]       NVARCHAR(20)		NOT NULL,	
	[per_BirthDate]       DATE				NOT NULL,
	[per_Sex]             CHAR(1)			NULL,
	[per_Email]           NVARCHAR(100)		NOT NULL,
	[per_Phone]           NVARCHAR(30)		NOT NULL,
	[per_Direccion]       NVARCHAR(200)		NOT NULL,
	[dep_Id]              INT				NOT NULL,
	[mun_Id]              INT				NOT NULL,
	[per_Esciv]			  CHAR				NOT NULL,
	[per_Status]          BIT   			NOT NULL,
	[per_IdUserCreate]    INT    			NULL,
	[per_DateCreate]      DATETIME			NULL,
	[per_IdUserModified]  INT				NULL,
	[per_DateModified]    DATETIME			NULL,
	
	CONSTRAINT PK_Gral_tbPersons_per_Id					PRIMARY KEY (per_Id),
	CONSTRAINT CHK_Gral_tbPersons_per_Esciv				CHECK(per_Esciv IN ('S', 'C', 'D', 'V', 'U')),
	CONSTRAINT FK_Gral_tbMunicipalities_tbPersons_mun_Id  FOREIGN KEY (mun_Id, dep_Id) REFERENCES [Gral].tbMunicipalities(mun_Id, dep_Id),
	CONSTRAINT FK_Gral_tbDepartments_tbPersons_dep_Id  FOREIGN KEY (dep_Id) REFERENCES [Gral].[tbDepartments](dep_Id),
	CONSTRAINT UQ_Gral_tbPersons_per_Identidad			UNIQUE (per_Identidad),
	CONSTRAINT CK_Gral_tbPersonas_per_Sex				CHECK (per_Sex IN ('M','F'))
);
GO

/*Sección #11*/
CREATE TABLE [Acce].tbUsers(
    [usu_Id]              INT IDENTITY(1,1),
    [usu_UserName]        VARCHAR(20)     NOT NULL,
    [usu_Password]        NVARCHAR(MAX)   NOT NULL,
	[usu_PasswordSalt]    NVARCHAR(MAX)	  NOT NULL,
	[usu_Profile_picture] NVARCHAR(MAX)	  NULL,
	[usu_Temporal_Password] BIT         NULL,
    [rol_Id]			  INT             NULL,
    [usu_Status]          BIT             NOT NULL,
	[Per_Id]    INT						  NOT NULL,
    [usu_IdUserCreate]    INT			  NULL,
    [usu_DateCreate]      DATETIME        NULL,
    [usu_IdUserModified]  INT             NULL,
    [usu_DateModified]    DATETIME        NULL,
    CONSTRAINT PK_Acce_tbUsers_usu_Id					  PRIMARY KEY(usu_Id),
    CONSTRAINT UQ_Acce_tbUsers_usu_UserName				  UNIQUE (usu_UserName),
	CONSTRAINT FK_Acce_tbUsers_tbrol_rol_Id				  FOREIGN KEY (rol_Id)				REFERENCES [Acce].tbRoles(rol_Id),
    CONSTRAINT FK_Acce_tbUsers_tbUsers_usu_IdUserModified FOREIGN KEY (usu_IdUserModified)  REFERENCES [Acce].tbUsers(usu_Id),
    CONSTRAINT FK_Acce_tbPersons_tbUsers_per_Id		      FOREIGN KEY (Per_Id)	REFERENCES [Gral].tbPersons (per_Id),
)
GO


/*Sección #12*/
CREATE TABLE [Gral].tbAreas(
    [are_Id]              INT IDENTITY(1,1),
    [are_Description]     NVARCHAR(MAX),
    [are_Status]          BIT				NOT NULL,
    [are_IdUserCreate]    INT				NULL,
    [are_DateCreate]      DATETIME			NOT NULL,
    [are_IdUserModified]  INT				NULL,
    [are_DateModified]    DATETIME			NULL,
    CONSTRAINT PK_Gral_tbAreas_are_Id PRIMARY KEY(are_Id)
);

/*Sección #13*/
CREATE TABLE [Gral].tbOccupations(
	occ_Id				INT IDENTITY(1,1),
	occ_Description		NVARCHAR(100),
	occ_Status			BIT         NOT NULL,
	occ_IdUserCreate	INT			NOT NULL,
	occ_DateCreate		DATETIME	NOT NULL,
	occ_IdUserModified	INT			NULL,
	occ_DateModified	DATETIME	NULL
	CONSTRAINT PK_tbOccupations_occ_Id PRIMARY KEY(occ_Id)
)
GO


/*Sección #14*/
CREATE TABLE [Gral].tbEmployees(
	[emp_Id]			  INT IDENTITY(1,1),
	[per_Id]			  INT		NOT NULL,
	[are_Id]			  INT		NOT NULL,
	[occ_Id]			  INT		NOT NULL,
	[emp_Status]          BIT       NOT NULL,
	[emp_IdUserCreate]    INT       NOT NULL,
	[emp_DateCreate]      DATETIME  NOT NULL,
	[emp_IdUserModified]  INT       NULL,
	[emp_DateModified]    DATETIME  NULL,
	CONSTRAINT PK_tbEmployees_emp_Id PRIMARY KEY(emp_Id),
	CONSTRAINT FK_tbEmployees_tbPersons_per_Id FOREIGN KEY(per_Id) REFERENCES [Gral].tbPersons(per_Id),
	CONSTRAINT FK_tbEmployees_tbOccupations_occ_Id FOREIGN KEY(occ_Id) REFERENCES [Gral].tbOccupations(occ_Id),
	CONSTRAINT FK_tbEmployees_tbAreas_are_Id FOREIGN KEY(are_Id) REFERENCES [Gral].tbAreas(are_Id)
)
GO



--================EJECUTAR ESTOS INSERTS DESPUES DE LA EJECUCI�N DE LAS PRIMERAS TABLAS================
/*Sección #15*/
INSERT INTO Gral.tbCountries
VALUES('Honduras',1, NULL,GETDATE(),NULL,NULL)
GO

INSERT INTO Gral.tbDepartments
VALUES('01','Atlantida',1,1, NULL,GETDATE(),NULL,NULL)
GO
INSERT INTO Gral.tbMunicipalities
VALUES('0101','La Ceiba', 1, 1, NULL,GETDATE(),NULL,NULL)
GO
INSERT INTO Gral.tbPersons
VALUES
('0607199301183',
'Magdaly',
'Gissele',
'Zuniga',
'1993/11/25',
'F',
'magdaly.zuniga03@hotmail.com',
'+504 98574566',
'Time Square,New York',
1,
1,
'C',
1,
1,
GETDATE(),
NULL,
NULL)
GO

INSERT INTO Acce.tbRoles
VALUES('Administrador',1,null,GETDATE(),NULL,NULL)
GO

INSERT INTO Acce.tbUsers
(
	[usu_UserName],
	[usu_Password],
	[usu_PasswordSalt],
	[usu_Profile_picture],
	[rol_Id],
	[Per_Id],
	[usu_Status],
	[usu_IdUserCreate], 
	[usu_DateCreate],    
	[usu_IdUserModified],
	[usu_DateModified]		
	)
	VALUES(
	'Administrator',
	'4B51BDDC1FCAE7564561A60A22EEF05C4398EB6A4CB5C7D52C45D60B54901A00',
	'50703163-7da2-4a49-819c-4696ccaf47cf',
	NULL,
	1,
	1,
	1,
	1,
	GETDATE(),
	NULL,
	NULL
)
GO

--================EJECUTAR LOS SIGUIENTES ALTERS DESPUES DE LA EJECUCI�N DE LOS INSERTS================
/*Sección #16*/

ALTER TABLE [Acce].tbRoles
ADD CONSTRAINT FK_Acce_tbUsers_tbRoles_rol_IdUserCreate
FOREIGN KEY (rol_IdUserCreate) REFERENCES [Acce].tbUsers (usu_Id)

ALTER TABLE [Acce].tbRoles
ADD CONSTRAINT FK_Acce_tbUsers_tbRoles_rol_IdUserModified
FOREIGN KEY (rol_IdUserModified) REFERENCES [Acce].tbUsers (usu_Id)

ALTER TABLE [Gral].tbDepartments
ADD CONSTRAINT FK_Gral_tbUsers_tbDepartments_depto_IdUserCreate 
FOREIGN KEY (dep_IdUserCreate) REFERENCES [Acce].tbUsers (usu_Id)

ALTER TABLE [Gral].tbDepartments
ADD CONSTRAINT FK_Gral_tbUsers_tbDepartments_depto_IdUserModified 
FOREIGN KEY (dep_IdUserModified) REFERENCES [Acce].tbUsers (usu_Id)

ALTER TABLE [Gral].tbMunicipalities
ADD CONSTRAINT  FK_Gral_tbUsers_tbMunicipalities_mpio_IdUserCreate 
FOREIGN KEY (mun_IdUserCreate) REFERENCES [Acce].tbUsers (usu_Id)

ALTER TABLE [Gral].tbMunicipalities
ADD CONSTRAINT  FK_Gral_tbUsers_tbMunicipalities_mpio_IdUserModified 
FOREIGN KEY (mun_IdUserModified) REFERENCES [Acce].tbUsers (usu_Id)

ALTER TABLE [Gral].tbPersons
ADD CONSTRAINT  FK_Acce_tbUsers_tbPersons_pers_IdUserModified 
FOREIGN KEY (per_IdUserModified) REFERENCES [Acce].tbUsers (usu_Id)
GO


/*Sección #17*/
CREATE TABLE Clte.tbPriorities
(
pry_Id					INT IDENTITY(1,1),
pry_Descripcion			NVARCHAR(100)	NOT NULL,
pry_Status				BIT				NOT NULL,
pry_IdUserCreate		INT				NOT NULL,
pry_DateCreate			DATETIME		NOT NULL,
pry_IdUserModified		INT				NULL,
pry_DateModified		DATETIME		NULL
CONSTRAINT PK_Clte_tbPriorities_pry_Id PRIMARY KEY(pry_Id)
)



/*Sección #18*/
CREATE TABLE Clte.tbTypeChannels
(
tyCh_Id				INT IDENTITY(1,1),
tyCh_Description	NVARCHAR(100)	NOT NULL,
tyCh_Status			BIT				NOT NULL,
tyCh_IdUserCreate	INT			    NOT NULL,
tyCh_DateCreate		DATETIME		NOT NULL,
tyCh_IdUserModified INT				NULL,
tyCh_DateModified	DATETIME		NULL
CONSTRAINT PK_Clte_tbTypeChannels_tyCh_Id PRIMARY KEY(tyCh_Id)
)


/*Sección #19*/
CREATE TABLE Clte.tbCustomers
(
cus_Id                INT IDENTITY(1,1),
cus_AssignedUser    INT						NOT NULL,
tyCh_Id                INT					NOT NULL,
cus_Name            NVARCHAR(200)			NOT NULL,
cus_RTN                NVARCHAR(14)			NOT NULL,
cus_Address            NVARCHAR(200)		NOT NULL,
dep_Id                INT					NOT NULL,
mun_Id                INT					NOT NULL,
cus_Email            NVARCHAR(100)			NOT NULL,
cus_Receive_email    BIT					NOT NULL,
cus_Phone            NVARCHAR(30)			NOT NULL,
cus_AnotherPhone    NVARCHAR(30)			NULL,
cus_Status            BIT					NOT NULL,
cus_Active            BIT					NOT NULL,
cus_IdUserCreate    INT						NOT NULL,
cus_DateCreate        DATETIME				NOT NULL,
cus_IdUserModified    INT					NULL,
cus_DateModified    DATETIME				NULL
CONSTRAINT PK_Clte_tbCustomers_cus_Id                                PRIMARY KEY(cus_Id)
CONSTRAINT FK_Clte_tbCustomers_tyCh_Id_Clte_tbTypeChannels_tyCh_Id    FOREIGN KEY (tyCh_Id) REFERENCES Clte.tbTypeChannels (tyCh_Id),
CONSTRAINT FK_Clte_tbCustomers_Gral_tbMunicipalities_mun_Id            FOREIGN KEY (mun_Id, dep_Id)  REFERENCES [Gral].tbMunicipalities(mun_Id, dep_Id),
CONSTRAINT FK_Clte_tbCustomers_Acce_tbUsers_usu_Id                    FOREIGN KEY (cus_AssignedUser)    REFERENCES Acce.tbUsers(usu_Id)
)

/*Sección #20*/
CREATE TABLE Clte.tbMeetings
(
met_Id				INT IDENTITY(1,1),
met_Title			NVARCHAR(100) NOT NULL,
met_MeetingLink		NVARCHAR(MAX) NOT NULL,
cus_Id				INT			  NOT NULL,
met_Date			DATE		  NOT NULL,
met_StartTime		NVARCHAR(12)  NOT NULL,
met_EndTime			NVARCHAR(12)  NOT NULL,
met_Status			BIT			  NOT NULL,
met_IdUserCreate	INT			  NOT NULL,
met_DateCreate		DATETIME	  NOT NULL,
met_IdUserModified  INT			  NULL,
met_DateModified	DATETIME	  NULL
CONSTRAINT PK_Clte_tbMeetings_met_Id PRIMARY KEY(met_Id),
CONSTRAINT PK_Clte_tbMeetings__tbCustomers_cus_Id FOREIGN KEY(cus_Id) REFERENCES  Clte.tbCustomers(cus_Id)
)

/*Sección #21*/
CREATE TABLE Clte.tbCustomerNotes
(
cun_Id				INT IDENTITY(1,1),
cun_Descripcion		NVARCHAR(MAX)	NOT NULL,
cun_ExpirationDate	DATE			NOT NULL,
pry_Id				INT				NOT NULL,
cus_Id				INT				NOT NULL,
cun_Status			BIT				NOT NULL,
cun_IdUserCreate	INT				NOT NULL,
cun_DateCreate		DATETIME		NOT NULL,
cun_IdUserModified	INT				NULL,
cun_DateModified	DATETIME		NULL,
CONSTRAINT PK_Clte_tbCustomerNotes_cun_Id							PRIMARY KEY(cun_Id),
CONSTRAINT FK_Clte_tbCustomerNotes_pry_Id_Clte_tbPriorities_pry_Id  FOREIGN KEY (pry_Id) REFERENCES Clte.tbPriorities (pry_Id),
CONSTRAINT FK_Clte_tbtbCustomerNotes_tbCustomers_cus_Id				FOREIGN KEY (cus_Id) REFERENCES Clte.tbCustomers (cus_Id)
)

/*Sección #22*/
CREATE TABLE Clte.tbContacts
(
cont_Id				INT IDENTITY(1,1),
cont_Name			NVARCHAR(100) NOT NULL,
cont_LastName		NVARCHAR(100) NOT NULL,
cont_Email			NVARCHAR(100) NULL,
cont_Phone			NVARCHAR(100) NOT NULL,
occ_Id				INT			  NOT NULL,
cus_Id				INT			  NOT NULL,
cont_Status			BIT			  NOT NULL,
cont_IdUserCreate	INT			  NOT NULL,
cont_DateCreate		DATETIME	  NOT NULL,
cont_IdUserModified  INT		  NULL,
cont_DateModified	DATETIME	  NULL
CONSTRAINT PK_Clte_tbContacts_cont_Id				PRIMARY KEY(cont_Id)
CONSTRAINT FK_Clte_tbContacts_tbOccupations_occ_Id  FOREIGN KEY(occ_Id) REFERENCES [Gral].tbOccupations(occ_Id),
CONSTRAINT FK_Clte_tbContacts_tbCustomers_cus_Id	FOREIGN KEY(cus_Id) REFERENCES [Clte].tbCustomers(cus_Id)
)


/*Sección #23*/
CREATE TABLE Clte.tbCallType
(
 cati_Id INT IDENTITY(1,1)			NOT NULL,
 cati_Description NVARCHAR(100)		NOT NULL,
 cati_Status BIT					NOT NULL
 CONSTRAINT PK_CallType_cati_Id PRIMARY KEY(cati_Id)
)


/*Sección #24*/
CREATE TABLE Clte.tbCallBusiness
(
 cabu_Id INT IDENTITY(1,1),
 cabu_Description NVARCHAR(100)	NOT NULL,
 cacabu_Status BIT				NOT NULL
 CONSTRAINT PK_CallBusiness_cabu_Id PRIMARY KEY(cabu_Id)
)


/*Sección #25*/
CREATE TABLE Clte.tbCallResult
(
 caru_Id INT IDENTITY(1,1),
 caru_Description NVARCHAR(100)		NOT NULL,
 caru_Status BIT					NOT NULL
 CONSTRAINT PK_CallResult_caru_Id PRIMARY KEY(caru_Id)
)

/*Sección #26*/
CREATE TABLE Clte.tbCustomerCalls
(
cca_Id				INT IDENTITY(1,1),
cca_CallType		INT			    NOT NULL,
cca_Business		INT         	NOT NULL,
cca_Date			DATE			NOT NULL,
cca_StartTime		NVARCHAR(12)	NOT NULL,
cca_EndTime			NVARCHAR(12)	NOT NULL,
cca_Result			INT				NOT NULL,
cus_Id				INT				NOT NULL,
cca_Status			BIT				NOT NULL,
cca_IdUserCreate	INT				NOT NULL,
cca_DateCreate		DATETIME		NOT NULL,
cca_IdUserModified	INT				NULL,
cca_DateModified	DATETIME		NULL
CONSTRAINT PK_Clte_tbCustomerCalls_cca_Id							PRIMARY KEY (cca_Id)
CONSTRAINT FK_Clte_tbCustomerCalls_cus_Id_Clte_tbCustomer_cus_Id	FOREIGN KEY (cus_Id)       REFERENCES Clte.tbCustomers (cus_Id),
CONSTRAINT FK_Clte_tbCustomerCalls_CallType_cati_Id					FOREIGN KEY (cca_CallType) REFERENCES Clte.tbCallType(cati_Id),
CONSTRAINT FK_Clte_tbCustomerCalls_CallBusiness_cca_Business		FOREIGN KEY (cca_Business) REFERENCES Clte.tbCallBusiness (cabu_Id),
CONSTRAINT FK_Clte_tbCustomerCalls_CallResult_cca_Result			FOREIGN KEY (cca_Result)   REFERENCES Clte.tbCallResult (caru_Id)

)

/*Sección #27*/
CREATE TABLE Clte.tbMeetingsDetails
(
mde_Id				INT IDENTITY(1,1),
met_Id				INT			NOT NULL,
cus_Id				INT			NULL,
emp_Id				INT			NULL,
cont_Id				INT			NULL,
mde_Status			BIT			NOT NULL,
mde_IdUserCreate	INT			NOT NULL,
mde_DateCreate		DATETIME	NOT NULL,
mde_IdUserModified	INT			NULL,
mde_DateModified	DATETIME	NULL
CONSTRAINT PK_Clte_tbMeetingsDetails_mde_Id						PRIMARY KEY(mde_Id),
CONSTRAINT FK_Clte_tbMeetingsDetails_met_Id_Clte_tbMeetings_met_Id	FOREIGN KEY (met_Id) REFERENCES Clte.tbMeetings (met_Id),
CONSTRAINT FK_Clte_tbMeetingsDetails_cus_Id_Clte_tbCustomers_cus_Id FOREIGN KEY (cus_Id) REFERENCES Clte.tbCustomers (cus_Id),
CONSTRAINT FK_Clte_tbMeetingsDetails_emp_Id_Gral_tbEmployees_emp_Id FOREIGN KEY (emp_Id) REFERENCES Gral.tbEmployees (emp_Id),
CONSTRAINT FK_Clte_tbMeetingsDetails_cont_Id_Clte_tbContacts_cont_Id FOREIGN KEY (cont_Id) REFERENCES Clte.tbContacts (cont_Id)
)


/*Sección #28*/
CREATE TABLE Vent.tbMarketingHeaders--s
(
mhe_Id				INT IDENTITY(1,1),
mhe_Description		NVARCHAR(100),
mhe_Status			BIT			NOT NULL,
mhe_IdUserCreate	INT			NOT NULL,
mhe_DateCreate		DATETIME	NOT NULL,
mhe_IdUserModified	INT			NULL,
mhe_DateModified	DATETIME	NULL
CONSTRAINT PK_Vent_tbMarketingHeaders_mhe_Id PRIMARY KEY(mhe_Id)
)


/*Sección #29*/
CREATE TABLE Vent.tbMarketingDetails
(
mde_Id				INT IDENTITY(1,1),
mhe_Id				INT		 NOT NULL,
cus_Id				INT		 NOT NULL,	
mde_Status			BIT		 NOT NULL,
mde_IdUserCreate	INT		 NOT NULL,
mde_DateCreate		DATETIME NOT NULL,
mde_IdUserModified	INT		 NULL,
mde_DateModified	DATETIME NULL
CONSTRAINT PK_Vent_tbMarketingDetails_mde_Id									PRIMARY KEY(mde_Id)
CONSTRAINT FK_Vent_tbMarketingDetails_mhe_Id_Vent_tbMarketingHeaders_mhe_Id	FOREIGN KEY (mhe_Id) REFERENCES Vent.tbMarketingHeaders (mhe_Id),
CONSTRAINT FK_Vent_tbMarketingDetails_cus_Id_Clte_tbCustomers_cus_Id			FOREIGN KEY (cus_Id)  REFERENCES Clte.tbCustomers (cus_Id)
)


/*Sección #30*/
CREATE TABLE Gral.tbStates
(
sta_Id				INT IDENTITY(1,1),
sta_Description		NVARCHAR(100) NULL,
sta_Status			BIT NOT NULL,
sta_IdUserCreate	INT NOT NULL,
sta_DateCreate		DATETIME NOT NULL,
sta_IdUserModified	INT NULL,
sta_DateModified	DATETIME NULL
CONSTRAINT PK_Gral_tbStates_sta_Id PRIMARY KEY(sta_Id)
)
GO


/*Sección #31*/
CREATE TABLE Vent.tbUnits 
(
uni_Id				INT IDENTITY(1,1),
uni_Description		NVARCHAR(100)	NOT NULL,
uni_Abrevitation	NVARCHAR(4)		NOT NULL,
uni_Status			BIT				NOT NULL,
uni_IdUserCreate	INT				NOT NULL,
uni_DateCreate		DATETIME		NOT NULL,
uni_IdUserModified	INT				NULL,
uni_DateModified	DATETIME		NULL
CONSTRAINT PK_Vent_tbUnits_uni_Id PRIMARY KEY (uni_Id)
)
GO


/*Sección #32*/
CREATE TABLE Vent.tbCategories 
(
cat_Id				INT IDENTITY (1,1),
cat_Description		NVARCHAR (300) NOT NULL,
cat_Status			BIT			NOT NULL,
cat_IdUserCreate	INT			NOT NULL,
cat_DateCreate		DATETIME	NOT NULL,
cat_IdUserModified	INT			NULL,
cat_DateModified	DATETIME	NULL
CONSTRAINT PK_Vent_tbCategories_cat_Id PRIMARY KEY(cat_Id)
)
GO


/*Sección #33*/
CREATE TABLE Vent.tbSubCategories
(
scat_Id				INT IDENTITY(1,1),
scat_Description	NVARCHAR(100) NOT NULL,
cat_Id				INT			  NOT NULL,
scat_Status			BIT			  NOT NULL,
scat_IdUserCreate	INT			  NOT NULL,
scat_DateCreate		DATETIME	  NOT NULL,
scat_IdUserModified INT			  NULL,
scat_DateModified	DATETIME      NULL
CONSTRAINT PK_Vent_tbSubCategories_scat_Id							PRIMARY KEY (scat_Id),
CONSTRAINT FK_Vent_tbSubCategories_cat_Id_Vent_tbCategories_cat_Id  FOREIGN KEY (cat_Id) REFERENCES Vent.tbCategories(cat_Id)
)
GO
 
/*Sección #34*/
CREATE TABLE Vent.tbProducts 
(
pro_Id				INT IDENTITY(1,1),
pro_Description		NVARCHAR(200)	NOT NULL,
pro_PurchasePrice	NUMERIC(8,2)	NOT NULL,
pro_SalesPrice		NUMERIC(8,2)	NOT NULL,
pro_Stock			INT				NULL,
pro_ISV				NUMERIC			NOT NULL,
uni_Id				INT				NOT NULL,
scat_Id				INT				NOT NULL,
pro_Status			BIT				NOT NULL,
pro_IdUserCreate	INT				NOT NULL,
pro_DateCreate		DATETIME		NOT NULL,
pro_IdUserModified	INT				NULL,
pro_DateModified	DATETIME		NULL
CONSTRAINT PK_Vent_tbProducts_pro_Id PRIMARY KEY(pro_Id)
CONSTRAINT FK_Vent_tbProducts_uni_Id_Vent_tbUnits_uni_Id			FOREIGN KEY(uni_Id)	 REFERENCES Vent.tbUnits(uni_Id),
CONSTRAINT FK_Vent_tbProducts_scat_Id_Vent_tbSubCategories_scat_Id	FOREIGN KEY(scat_Id) REFERENCES Vent.tbSubCategories(scat_Id)
)
GO

/*Sección #35*/
CREATE TABLE Vent.tbCotizations
(
cot_Id				INT IDENTITY(1,1),
cus_Id				INT			NOT NULL,
cot_DateValidUntil	DATETIME	NOT NULL,
sta_Id				INT			NULL,
cot_Status			BIT			NOT NULL,
cot_IdUserCreate	INT			NOT NULL,
cot_DateCreate		DATETIME	NOT NULL,
cot_IdUserModified	INT			NULL,
cot_DateModified	DATETIME	NULL,
CONSTRAINT PK_Vent_tbCotizations_cot_Id								PRIMARY KEY (cot_Id),
CONSTRAINT FK_Vent_tbCotizations_cus_Id_Clte_tbCustomers_cus_Id FOREIGN KEY (cus_Id)	REFERENCES Clte.tbCustomers(cus_Id),
CONSTRAINT FK_Vent_tbCotizations_sta_Id_Gral_tbStates_sta_Id			FOREIGN KEY (sta_Id)	REFERENCES Gral.tbStates(sta_Id)
)
GO


/*Sección #36*/
CREATE TABLE Vent.tbCotizationsDetail 
(
code_Id				INT IDENTITY(1,1),
code_Cantidad		INT				NOT NULL,
pro_Id				INT				NOT NULL,
code_TotalPrice		DECIMAL(12,5)   NOT NULL,
cot_Id				INT				NOT NULL,
code_Status			BIT				NOT NULL,
code_IdUserCreate	INT				NOT NULL,
code_DateCreate		DATETIME		NOT NULL,
code_IdUserModified INT				NULL,
code_DateModified	DATETIME		NULL
CONSTRAINT PK_Vent_tbCotizationsDetail_code_Id							PRIMARY KEY (code_Id),
CONSTRAINT FK_Vent_tbCotizationsDetail_cot_Id_Vent_tbCotizations_cot_Id FOREIGN KEY(cot_Id)		REFERENCES Vent.tbCotizations(cot_Id)
)
GO



/*Sección #37*/
CREATE TABLE Vent.tbSaleOrders
(
sor_Id				INT IDENTITY(1,1),
cus_Id				INT			NOT NULL,
cot_Id				INT			NULL,
sta_Id				INT			NULL,
sor_Status			BIT			NOT NULL,
sor_IdUserCreate	INT			NOT NULL,
sor_DateCreate		DATETIME	NOT NULL,
sor_IdUserModified	INT			NULL,
sor_DateModified	DATETIME	NULL
CONSTRAINT PK_Vent_tbSaleOrders_sor_Id								PRIMARY KEY(sor_Id),
CONSTRAINT FK_Vent_tbSaleOrders_cus_Id_Clte_tbCustomers_cus_Id		FOREIGN KEY (cus_Id) REFERENCES Clte.tbCustomers(cus_Id),
CONSTRAINT FK_Vent_tbSaleOrders_cot_Id_Vent_tbCotizations_cot_Id	FOREIGN KEY (cot_Id) REFERENCES Vent.tbCotizations(cot_Id),
CONSTRAINT FK_Vent_tbSaleOrders_sta_Id_Vent_tbStates_sta_Id			FOREIGN KEY (sta_Id) REFERENCES Gral.tbStates  (sta_Id)
)
GO


/*Sección #38*/
CREATE TABLE Vent.tbOrderDetails 
(
ode_Id				INT IDENTITY(1,1),
ode_Amount			INT				NOT NULL,
pro_Id				INT				NULL,
ode_TotalPrice		DECIMAL(12,5)	NOT NULL,
sor_Id				INT				NULL,
ode_Status			BIT				NOT NULL,
ode_IdUserCreate	INT				NOT NULL,
ode_DateCreate		DATETIME		NOT NULL,
ode_IdUserModified	INT				NULL,
ode_DateModified	DATETIME		NULL
CONSTRAINT PK_Vent_tbOrderDetails_ode_Id						PRIMARY KEY (ode_Id),
CONSTRAINT FK_Vent_tbOrderDetails_pro_Id_Vent_tbProducts_pro_Id FOREIGN KEY(pro_Id) REFERENCES Vent.tbProducts(pro_Id),
CONSTRAINT FK_Vent_tbOrderDetails_sor_Id_tbSaleOrders			FOREIGN KEY(sor_Id) REFERENCES Vent.tbSaleOrders(sor_Id)
)
GO


/*Sección #39*/
CREATE TABLE Vent.tbCampaign
( 
cam_Id				INT IDENTITY(1,1),
cam_Nombre			NVARCHAR(100)	NOT NULL,
cam_Descripcion		NVARCHAR(MAX)	NULL,
cam_Html			NVARCHAR(MAX)	NOT NULL,
cam_Status			BIT				NOT NULL,
cam_IdUserCreate	INT				NOT NULL,
cam_DateCreate		DATETIME		NOT NULL,
CONSTRAINT PK_tbCampaign_cam_Id PRIMARY KEY(cam_Id)
)
GO

/*Sección #40*/
CREATE TABLE  Vent.tbCampaignDetails 
(
cde_Id				INT IDENTITY(1,1) NOT NULL,
cus_Id				INT				  NOT NULL,
cam_Id				INT				  NOT NULL,
CONSTRAINT PK_tbCampaignDetails_cde_Id PRIMARY KEY(cde_Id),
CONSTRAINT FK_tbCampaignDetails_tbCampaign_cde_Id FOREIGN KEY(cam_Id) REFERENCES Vent.tbCampaign (cam_Id),
CONSTRAINT FK_tbCampaignDetails_tbCustomers_cus_Id FOREIGN KEY(cus_Id) REFERENCES Clte.tbCustomers(cus_Id),
)
GO

/*Sección #41*/
CREATE TABLE Clte.tbCustomersFile
(
cfi_Id				INT IDENTITY(1,1),
cfi_fileRoute		NVARCHAR(MAX),
cus_Id				INT,
cfi_Status			BIT				NOT NULL,
cfi_IdUserCreate	INT				NOT NULL,
cfi_DateCreate		DATETIME		NOT NULL,
cfi_IdUserModified	INT				NULL,
cfi_DateModified	DATETIME		NULL,
CONSTRAINT PK_Clte_tbCustomersFile_cfi_Id							PRIMARY KEY(cfi_Id),
CONSTRAINT FK_Clte_tbCustomersFile_tbCustomers_cus_Id				FOREIGN KEY (cus_Id) REFERENCES Clte.tbCustomers (cus_Id)
)
GO

/*Sección #42*/
CREATE TABLE Logs.tbEventLogType(
	etype_Id INT NOT NULL,
	etype_Name NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_tbEventLogType_etype_Id PRIMARY KEY(etype_Id)
)

/*Sección #43*/
CREATE TABLE Logs.tbEventLog(
	event_Id INT IDENTITY(1,1) NOT NULL,
	event_Eventtype INT NOT NULL,
	event_User INT NULL,
	event_Details NVARCHAR(MAX) NOT NULL,
	event_IpAddress NVARCHAR(100) NOT NULL,
	event_UserAgent NVARCHAR(MAX) NOT NULL,
	event_PreviousState NVARCHAR(MAX) NULL,
	event_NewState NVARCHAR(MAX) NULL,
	event_CreationDate DATETIME NOT NULL,
	CONSTRAINT PK_tbEventLog_event_Id PRIMARY KEY(event_Id),
	CONSTRAINT FK_tbEventLog_event_Eventtype FOREIGN KEY(event_Eventtype) REFERENCES Logs.tbEventLogType (etype_Id),
)

--SET IDENTITY_INSERT [Clte].[tbCallBusiness] ON 

--INSERT [Clte].[tbCallBusiness] ([cabu_Id], [cabu_Description], [cacabu_Status]) VALUES (1, N'Posible Negocio', 1)
--INSERT [Clte].[tbCallBusiness] ([cabu_Id], [cabu_Description], [cacabu_Status]) VALUES (2, N'Administrativo', 1)
--INSERT [Clte].[tbCallBusiness] ([cabu_Id], [cabu_Description], [cacabu_Status]) VALUES (3, N'Negociación', 1)
--INSERT [Clte].[tbCallBusiness] ([cabu_Id], [cabu_Description], [cacabu_Status]) VALUES (4, N'Demostración', 1)
--INSERT [Clte].[tbCallBusiness] ([cabu_Id], [cabu_Description], [cacabu_Status]) VALUES (5, N'Proyecto', 1)
--INSERT [Clte].[tbCallBusiness] ([cabu_Id], [cabu_Description], [cacabu_Status]) VALUES (6, N'Desk', NULL)
--SET IDENTITY_INSERT [Clte].[tbCallBusiness] OFF
--GO
--SET IDENTITY_INSERT [Clte].[tbCallResult] ON 

--INSERT [Clte].[tbCallResult] ([caru_Id], [caru_Description], [caru_Status]) VALUES (1, N'Realizada', 1)
--INSERT [Clte].[tbCallResult] ([caru_Id], [caru_Description], [caru_Status]) VALUES (2, N'Rechazada', 1)
--INSERT [Clte].[tbCallResult] ([caru_Id], [caru_Description], [caru_Status]) VALUES (3, N'Sin respuesta/Ocupado', 1)
--INSERT [Clte].[tbCallResult] ([caru_Id], [caru_Description], [caru_Status]) VALUES (4, N'Solícito llamar de nuevo', 1)
--INSERT [Clte].[tbCallResult] ([caru_Id], [caru_Description], [caru_Status]) VALUES (5, N'Número no valido', 1)
--SET IDENTITY_INSERT [Clte].[tbCallResult] OFF
--GO

--SET IDENTITY_INSERT [Clte].[tbCallType] ON 

--INSERT [Clte].[tbCallType] ([cati_Id], [cati_Description], [cati_Status]) VALUES (1, N'Saliente', 1)
--INSERT [Clte].[tbCallType] ([cati_Id], [cati_Description], [cati_Status]) VALUES (2, N'Entrante', 1)
--SET IDENTITY_INSERT [Clte].[tbCallType] OFF
--GO

--SET IDENTITY_INSERT [Clte].[tbTypeChannels] ON 

--INSERT [Clte].[tbTypeChannels] ([tyCh_Id], [tyCh_Description], [tyCh_Status], [tyCh_IdUserCreate], [tyCh_DateCreate], [tyCh_IdUserModified], [tyCh_DateModified]) VALUES (1, N'Correo Electronico', 1, 1, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Clte].[tbTypeChannels] ([tyCh_Id], [tyCh_Description], [tyCh_Status], [tyCh_IdUserCreate], [tyCh_DateCreate], [tyCh_IdUserModified], [tyCh_DateModified]) VALUES (2, N'WhatsApp', 1, 1, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Clte].[tbTypeChannels] ([tyCh_Id], [tyCh_Description], [tyCh_Status], [tyCh_IdUserCreate], [tyCh_DateCreate], [tyCh_IdUserModified], [tyCh_DateModified]) VALUES (3, N'Facebook', 1, 1, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Clte].[tbTypeChannels] ([tyCh_Id], [tyCh_Description], [tyCh_Status], [tyCh_IdUserCreate], [tyCh_DateCreate], [tyCh_IdUserModified], [tyCh_DateModified]) VALUES (4, N'Llamada Telefonica', 1, 1, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--SET IDENTITY_INSERT [Clte].[tbTypeChannels] OFF
--GO

--SET IDENTITY_INSERT [Clte].[tbPriorities] ON 

--INSERT [Clte].[tbPriorities] ([pry_Id], [pry_Descripcion], [pry_Status], [pry_IdUserCreate], [pry_DateCreate], [pry_IdUserModified], [pry_DateModified]) VALUES (1, N'Alta', 1, 1, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Clte].[tbPriorities] ([pry_Id], [pry_Descripcion], [pry_Status], [pry_IdUserCreate], [pry_DateCreate], [pry_IdUserModified], [pry_DateModified]) VALUES (2, N'Media', 1, 1, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Clte].[tbPriorities] ([pry_Id], [pry_Descripcion], [pry_Status], [pry_IdUserCreate], [pry_DateCreate], [pry_IdUserModified], [pry_DateModified]) VALUES (3, N'Baja', 1, 1, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--SET IDENTITY_INSERT [Clte].[tbPriorities] OFF
--GO

--SET IDENTITY_INSERT [Acce].[tbComponents] ON 

--INSERT [Acce].[tbComponents] ([com_Id], [com_Description], [com_Status]) VALUES (1, N'Contabilidad', 1)
--INSERT [Acce].[tbComponents] ([com_Id], [com_Description], [com_Status]) VALUES (2, N'Distribucion', 1)
--INSERT [Acce].[tbComponents] ([com_Id], [com_Description], [com_Status]) VALUES (3, N'Produccion', 1)
--INSERT [Acce].[tbComponents] ([com_Id], [com_Description], [com_Status]) VALUES (4, N'Mercadeo', 1)
--INSERT [Acce].[tbComponents] ([com_Id], [com_Description], [com_Status]) VALUES (5, N'Recursos Humanos', 1)
--SET IDENTITY_INSERT [Acce].[tbComponents] OFF
--GO


--SET IDENTITY_INSERT [Acce].[tbModules] ON 

--INSERT [Acce].[tbModules] ([mod_Id], [com_Id], [mod_Description], [mod_Status]) VALUES (1, 5, N'Servicio al cliente', 1)
--INSERT [Acce].[tbModules] ([mod_Id], [com_Id], [mod_Description], [mod_Status]) VALUES (2, 4, N'Ventas', 1)
--INSERT [Acce].[tbModules] ([mod_Id], [com_Id], [mod_Description], [mod_Status]) VALUES (3, 4, N'Marketing', 1)
--INSERT [Acce].[tbModules] ([mod_Id], [com_Id], [mod_Description], [mod_Status]) VALUES (5, 1, N'Informes,estadisticas y cuadros de mando', 1)
--INSERT [Acce].[tbModules] ([mod_Id], [com_Id], [mod_Description], [mod_Status]) VALUES (6, 2, N'Social', 1)
--SET IDENTITY_INSERT [Acce].[tbModules] OFF
--GO

--SET IDENTITY_INSERT [Gral].[tbDepartments] ON 


--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (2, N'02', N'Colon',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (3, N'03', N'Comayagua',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (4, N'04', N'Copan',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (5, N'05', N'Cortes',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (6, N'06', N'Choluteca',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (7, N'07', N'El Paraiso',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (8, N'08', N'Francisco Morazan',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (9, N'09', N'Gracias a Dios',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (10, N'10', N'Intibuca',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (11, N'11', N'Islas de la bahia',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (12, N'12', N'La paz',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (13, N'13', N'Lempira',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (14, N'14', N'Ocotepeque',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (15, N'15', N'Olancho',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (16, N'16', N'Santa Barbara',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (17, N'17', N'Valle',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbDepartments] ([dep_Id], [dep_Code], [dep_Description],[cou_Id], [dep_Status], [dep_IdUserCreate], [dep_DateCreate], [dep_IdUserModified], [dep_DateModified]) VALUES (18, N'18', N'Yoro',1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--SET IDENTITY_INSERT [Gral].[tbDepartments] OFF
--GO

--SET IDENTITY_INSERT [Gral].[tbMunicipalities] ON 

----insert de Atlantidad/1
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (2, N'0102', N'El Porvenir', 1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (3, N'0103', N'Esparta ', 1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (4, N'0104', N'Jutiapa', 1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (5, N'0105', N'La Masica', 1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (6, N'0106', N'San Francisco', 1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (7, N'0107', N'Tela', 1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (8, N'0108', N'Arizona', 1, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)


----insert de Colon/2
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (9, N'0201', N'Trujillo ', 2, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (10, N'0202', N'Balfate ', 2, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (11, N'0203', N'Iriona ', 2, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (12, N'0204', N'Limón', 2, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (13, N'0205', N'Sabá', 2, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (14, N'0206', N'Santa Fe', 2, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (15, N'0207', N'Santa Rosa de Aguán', 2, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (16, N'0208', N'Sonaguera', 2, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (17, N'0209', N'Tocoa', 2, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (18, N'0210', N'Bonito Oriental', 2, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)


----insert de Comayagua/3
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (19, N'0301', N'Comayagua', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (20, N'0302', N'Ajuterique  ', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (21, N'0303', N'El Rosario ', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (22, N'0304', N'Esquías ', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (23, N'0305', N'Humuya ', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (24, N'0306', N'La Libertad ', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (25, N'0307', N'Lamaní ', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (26, N'0308', N'La Trinidad', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (27, N'0309', N'Lejamani ', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (28, N'0310', N'Meambar', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (29, N'0311', N'Minas de Oro', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (30, N'0312', N'Ojos de Agua', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (31, N'0313', N'San Jerónimo', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (32, N'0314', N'San José de Comayagua', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (33, N'0315', N'San José del Potrero', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (34, N'0316', N'San Luis ', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (35, N'0317', N'San Sebastián', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (36, N'0318', N'Siguatepeque ', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (37, N'0319', N'Villa de San Antonio', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (38, N'0320', N'Las Lajas ', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (39, N'0321', N'Taulabé ', 3, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

----insert de copan/4

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (40, N'0401', N'STA. Rosa de copan', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (41, N'0402', N'Cabañas ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (42, N'0403', N'Concepción  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (43, N'0404', N'Copán Ruinas  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (44, N'0405', N'Corquín ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (45, N'0406', N'Cucuyagua  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (46, N'0407', N'Dolores ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (47, N'0408', N'Dulce Nombre  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (48, N'0409', N'El Paraíso ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (49, N'0410', N'Florida  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (50, N'0411', N'La Jigua  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (51, N'0412', N'La Unión  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (52, N'0413', N'Nueva Arcadia  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (53, N'0414', N'San Águstin ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (54, N'0415', N'San Antonio  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (55, N'0416', N'San Jerónimo  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (56, N'0417', N'San José ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (57, N'0418', N'San Juan de Opoa  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (58, N'0419', N'San Nicolás ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (59, N'0420', N'San Pedro  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (60, N'0421', N'Santa Rita  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (61, N'0422', N'Trinidad de Copán  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (62, N'0423', N'Veracruz  ', 4, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

----insert de Cortes/5

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (63, N'0501', N'San pedro sula', 5, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (64, N'0502', N'Choloma', 5, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (65, N'0503', N'Omoa ', 5, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (66, N'0504', N'Pimienta', 5, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (67, N'0505', N'Potrerillos ', 5, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (68, N'0506', N'Puerto Cortés ', 5, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (69, N'0507', N'San Antonio de Cortés', 5, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (70, N'0508', N'San Francisco de Yojoa ', 5, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (71, N'0509', N'San Manuel', 5, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (72, N'0510', N'Santa Cruz de Yojoa', 5, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (73, N'0511', N'Villanueva ', 5, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (74, N'0512', N'La Lima', 5, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

----insert de Choluteca/6
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (75, N'0601', N'Choluteca ', 6, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (76, N'0602', N'Apacilagua  ', 6, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (77, N'0603', N'Concepción de María  ', 6, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

----insert de El Paraíso/7
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (78, N'0701', N'Yuscarán ', 7, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (79, N'0702', N'Alauca  ', 7, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (80, N'0703', N'Danlí  ', 7, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
----insert de Francisco Morazan/8
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (81, N'0801', N'Distrito Central  ', 8, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (82, N'0802', N'Alubarén ', 8, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (83, N'0803', N'Cedros  ', 8, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
----insert de Gracias a Dios/9
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (84, N'0901', N'Puerto Lempira  ', 9, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (85, N'0902', N'Brus Laguna  ', 9, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (86, N'0903', N'Ahuas  ', 9, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
----insert de Intibuca/10
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (87, N'1001', N'La Esperanza ', 10, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (88, N'1002', N'Camasca ', 10, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (89, N'1003', N'Colomoncagua ', 10, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
----insert de Islas de La Bahia/11
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (90, N'1101', N'Roatán  ', 11, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (91, N'1102', N'Guanaja  ', 11, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (92, N'1103', N'José Santos Guardiola  ', 11, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)


----insert de La Paz/12
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (93, N'1201', N'La Paz  ', 12, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (94, N'1202', N'Aguanqueterique  ', 12, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (95, N'1203', N'Cabañas  ', 12, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
----insert de Lempira/13
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (96, N'1301', N'Gracias  ', 13, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (97, N'1302', N'Belén ', 13, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (98, N'1303', N'Candelaria ', 13, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
----insert de Ocotepeque/14
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (99, N'1401', N'Nueva Ocotepeque  ', 14, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (100, N'1402', N'Belén Gualcho  ', 14, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (101, N'1403', N'Concepción  ', 14, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
----insert de Olancho/15
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (102, N'1501', N'Juticalpa  ', 15, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (103, N'1502', N'Campamento ', 15, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (104, N'1503', N'Catacamas  ', 15, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
----insert de Santa Barbara/16
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (105, N'1601', N'Santa Barbara ', 16, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (106, N'1602', N'Arada  ', 16, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (107, N'1603', N'Atima  ', 16, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
----insert de Valle/17
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (108, N'1701', N'Nacaome ', 17, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (109, N'1702', N'Alianza  ', 17, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (110, N'1703', N'Amapala ', 17, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
----insert de Yoro/18
--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (111, N'1801', N'Yoro ', 18, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (112, N'1802', N'Arenal ', 18, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--INSERT [Gral].[tbMunicipalities] ([mun_Id], [mun_Code], [mun_Description], [dep_Id], [mun_Status], [mun_IdUserCreate], [mun_DateCreate], [mun_IdUserModified], [mun_DateModified]) VALUES (113, N'1803', N'El Negrito  ', 18, 1, NULL, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)

--SET IDENTITY_INSERT [Gral].[tbMunicipalities] OFF
--GO
