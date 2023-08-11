USE [Template]
GO
SET IDENTITY_INSERT [Gral].[tbAreas] ON 

INSERT [Gral].[tbAreas] ([are_Id], [are_Description], [are_Status], [are_IdUserCreate], [are_DateCreate], [are_IdUserModified], [are_DateModified]) VALUES (1, N'Administracion y Recursos Humanos', 1, 1, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [Gral].[tbAreas] ([are_Id], [are_Description], [are_Status], [are_IdUserCreate], [are_DateCreate], [are_IdUserModified], [are_DateModified]) VALUES (2, N'Ventas y Marketing', 1, 1, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [Gral].[tbAreas] ([are_Id], [are_Description], [are_Status], [are_IdUserCreate], [are_DateCreate], [are_IdUserModified], [are_DateModified]) VALUES (3, N'Financiera', 1, 1, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [Gral].[tbAreas] ([are_Id], [are_Description], [are_Status], [are_IdUserCreate], [are_DateCreate], [are_IdUserModified], [are_DateModified]) VALUES (4, N'Producción', 1, 1, CAST(N'2022-06-01T00:00:00.000' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [Gral].[tbAreas] OFF
GO
