USE [Laboratory]
GO
/****** Object:  Table [dbo].[Appliance]    Script Date: 25.08.2020 19:20:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appliance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Barcode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Appliance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Auth]    Script Date: 25.08.2020 19:20:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auth](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
 CONSTRAINT [PK_Auth] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 25.08.2020 19:20:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TC] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](250) NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lab]    Script Date: 25.08.2020 19:20:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lab](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[MaxApplianceCapacity] [int] NOT NULL,
	[CurrentApplianceCapacity] [int] NOT NULL,
 CONSTRAINT [PK_Lab] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Move]    Script Date: 25.08.2020 19:20:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Move](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplianceId] [int] NOT NULL,
	[LabId] [int] NOT NULL,
	[EntranceDate] [datetime] NULL,
	[ExitDate] [datetime] NULL,
 CONSTRAINT [PK_Move] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 25.08.2020 19:20:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileDetail]    Script Date: 25.08.2020 19:20:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfileId] [int] NOT NULL,
	[AuthId] [int] NOT NULL,
 CONSTRAINT [PK_ProfileDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileEmployee]    Script Date: 25.08.2020 19:20:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileEmployee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfileId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_ProfileEmployee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Appliance] ON 

INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (1, N'Appliance1', N'Barcode1')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (2, N'Appliance2', N'Barcode2')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (3, N'Appliance3', N'Barcode3')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (4, N'Appliance4', N'Barcode4')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (5, N'Appliance5', N'Barcode5')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (6, N'Appliance6', N'Barcode6')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (7, N'Serial Record Appliance Test 1', N'Barcode 1')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (8, N'Serial Record Appliance Test 1', N'Barcode 1')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (9, N'Serial Record Appliance Test 1', N'Barcode 1')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (10, N'Serial Record Appliance Test 2', N'Barcode 2')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (11, N'Serial Record Appliance Test 1', N'Barcode 1')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (12, N'Serial Record Appliance Test 2', N'Barcode 2')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (13, N'Serial Record Appliance Test 3', N'Barcode 3')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (14, N'Serial Record Appliance Test 4', N'Barcode 4')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (15, N'Serial Record Appliance Test 5', N'Barcode 5')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (16, N'Serial Record Appliance Test 6', N'Barcode 6')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (17, N'Serial Record Appliance Test 7', N'Barcode 7')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (18, N'Serial Record Appliance Test 8', N'Barcode 8')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (19, N'Serial Record Appliance Test 9', N'Barcode 9')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (20, N'Serial Record Appliance Test 10', N'Barcode 10')
INSERT [dbo].[Appliance] ([Id], [Name], [Barcode]) VALUES (21, N'Serial Record Appliance Test 11', N'Barcode 11')
SET IDENTITY_INSERT [dbo].[Appliance] OFF
GO
SET IDENTITY_INSERT [dbo].[Auth] ON 

INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (1, N'PAGE_AUTH_LIST', N'Yetki Listeleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (2, N'PAGE_AUTH_ADD', N'Yetki Ekleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (3, N'PAGE_AUTH_EDIT', N'Yetki Düzenleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (4, N'PAGE_AUTH_DISPLAY', N'Yetki Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (5, N'PAGE_AUTH_DELETE', N'Yetki Silme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (6, N'PAGE_APPLIANCE_LIST', N'Cihaz Listeleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (7, N'PAGE_APPLIANCE_ADD', N'Cihaz Ekleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (8, N'PAGE_APPLIANCE_EDIT', N'Cihaz Düzenleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (9, N'PAGE_APPLIANCE_DISPLAY', N'Cihaz Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (10, N'PAGE_EMPLOYEE_LIST', N'Kullanıcı Listeleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (11, N'PAGE_EMPLOYEE_ADD', N'Kullanıcı Ekleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (12, N'PAGE_EMPLOYEE_EDIT', N'Kullanıcı Düzenleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (13, N'PAGE_EMPLOYEE_DISPLAY', N'Kullanıcı Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (14, N'PAGE_LAB_LIST', N'Lab Listeleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (15, N'PAGE_LAB_ADD', N'Lab Ekleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (16, N'PAGE_LAB_EDIT', N'Lab Düzenleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (17, N'PAGE_LAB_DISPLAY', N'Lab Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (18, N'PAGE_MOVE_LIST', N'Cihaz Hareketi Listeleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (19, N'PAGE_MOVE_ADD', N'Cihaz Hareketei Ekleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (20, N'PAGE_MOVE_EDIT', N'Cihaz Hareketi Düzenleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (21, N'PAGE_MOVE_DISPLAY', N'Cihaz Hareketi Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (22, N'PAGE_PROFILE_LIST', N'Profil Listeleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (23, N'PAGE_PROFILE_ADD', N'Profil Ekleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (24, N'PAGE_PROFILE_EDIT', N'Profil Düzenleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (25, N'PAGE_PROFILE_DISPLAY', N'Profil Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (26, N'PAGE_PROFILE_DELETE', N'Profil Silme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (27, N'PAGE_PROFILEDETAIL_BATCHEDIT', N'Profil Detaylarını Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (28, N'PAGE_PROFILEEMPLOYEE_BATCHEDIT', N'Profil Kullanıcılarını Görüntüleme', 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Auth] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [TC], [Name], [LastName], [Phone], [Address], [UserName], [Password]) VALUES (1, N'11166699988', N'Talha', N'Erdogan', N'5557779968', N'A Sk. B Cad. C Mah. D / E', N'admin', N'1')
INSERT [dbo].[Employee] ([Id], [TC], [Name], [LastName], [Phone], [Address], [UserName], [Password]) VALUES (3, N'99988866652', N'Talha', N' Erdogan(Lab)', N'5559684499', N'E Sk. D Cad.  C Mah. B / A', N'labUser', N'1')
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Lab] ON 

INSERT [dbo].[Lab] ([Id], [Name], [MaxApplianceCapacity], [CurrentApplianceCapacity]) VALUES (1, N'Lab 1 ', 1, 1)
INSERT [dbo].[Lab] ([Id], [Name], [MaxApplianceCapacity], [CurrentApplianceCapacity]) VALUES (2, N'Lab 2', 2, 0)
INSERT [dbo].[Lab] ([Id], [Name], [MaxApplianceCapacity], [CurrentApplianceCapacity]) VALUES (3, N'Lab 3', 3, 0)
INSERT [dbo].[Lab] ([Id], [Name], [MaxApplianceCapacity], [CurrentApplianceCapacity]) VALUES (4, N'Lab 4', 4, 0)
INSERT [dbo].[Lab] ([Id], [Name], [MaxApplianceCapacity], [CurrentApplianceCapacity]) VALUES (5, N'Lab 5', 5, 0)
INSERT [dbo].[Lab] ([Id], [Name], [MaxApplianceCapacity], [CurrentApplianceCapacity]) VALUES (6, N'Lab 6', 6, 0)
INSERT [dbo].[Lab] ([Id], [Name], [MaxApplianceCapacity], [CurrentApplianceCapacity]) VALUES (7, N'Lab 7', 7, 0)
INSERT [dbo].[Lab] ([Id], [Name], [MaxApplianceCapacity], [CurrentApplianceCapacity]) VALUES (8, N'Lab 8', 8, 0)
INSERT [dbo].[Lab] ([Id], [Name], [MaxApplianceCapacity], [CurrentApplianceCapacity]) VALUES (9, N'Lab 9', 9, 0)
INSERT [dbo].[Lab] ([Id], [Name], [MaxApplianceCapacity], [CurrentApplianceCapacity]) VALUES (10, N'Lab 10', 10, 0)
INSERT [dbo].[Lab] ([Id], [Name], [MaxApplianceCapacity], [CurrentApplianceCapacity]) VALUES (11, N'Lab 11', 11, 0)
SET IDENTITY_INSERT [dbo].[Lab] OFF
GO
SET IDENTITY_INSERT [dbo].[Move] ON 

INSERT [dbo].[Move] ([Id], [ApplianceId], [LabId], [EntranceDate], [ExitDate]) VALUES (1, 7, 10, CAST(N'2020-08-25T19:09:04.700' AS DateTime), CAST(N'2020-08-25T19:09:14.400' AS DateTime))
INSERT [dbo].[Move] ([Id], [ApplianceId], [LabId], [EntranceDate], [ExitDate]) VALUES (2, 1, 1, CAST(N'2020-08-25T19:10:11.007' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Move] OFF
GO
SET IDENTITY_INSERT [dbo].[Profile] ON 

INSERT [dbo].[Profile] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (1, N'SYSTEMADMIN', N'Sistem Admin Profili', 0, NULL, NULL)
INSERT [dbo].[Profile] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (2, N'LABUSERPROFILE', N'Lab Kullanıcısı', 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Profile] OFF
GO
SET IDENTITY_INSERT [dbo].[ProfileDetail] ON 

INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (1, 1, 12)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (2, 1, 11)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (3, 1, 13)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (4, 1, 10)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (5, 1, 27)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (6, 1, 24)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (7, 1, 23)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (8, 1, 25)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (9, 1, 28)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (10, 1, 22)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (11, 1, 26)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (12, 1, 3)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (13, 1, 2)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (14, 1, 4)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (15, 1, 1)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (16, 1, 5)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (17, 2, 8)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (18, 2, 7)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (19, 2, 9)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (20, 2, 19)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (21, 2, 20)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (22, 2, 21)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (23, 2, 18)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (24, 2, 6)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (25, 2, 16)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (26, 2, 15)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (27, 2, 17)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (28, 2, 14)
SET IDENTITY_INSERT [dbo].[ProfileDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[ProfileEmployee] ON 

INSERT [dbo].[ProfileEmployee] ([Id], [ProfileId], [EmployeeId]) VALUES (1, 2, 3)
INSERT [dbo].[ProfileEmployee] ([Id], [ProfileId], [EmployeeId]) VALUES (2, 1, 1)
SET IDENTITY_INSERT [dbo].[ProfileEmployee] OFF
GO
