USE [DataDoctor]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Licence] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contact]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactId] [int] IDENTITY(1,1) NOT NULL,
	[ContactNumber] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[PersonId] [int] NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Disease]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disease](
	[Disease_Id] [int] NOT NULL,
	[Disease_Name] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Disease_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Doc_Specialization]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doc_Specialization](
	[Doctor_Id] [nvarchar](128) NOT NULL,
	[Field] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK__Doc_Spec__B17A1725EB355201] PRIMARY KEY CLUSTERED 
(
	[Doctor_Id] ASC,
	[Field] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DoctorDegree]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorDegree](
	[Doctor_Id] [nvarchar](128) NOT NULL,
	[Degree] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__DoctorDe__E59B532F8F07ECC2] PRIMARY KEY CLUSTERED 
(
	[Doctor_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Medication]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medication](
	[Pres_Id] [int] NOT NULL,
	[Med_Id] [int] NOT NULL,
	[timings] [text] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Pres_Id] ASC,
	[Med_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Medicin]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicin](
	[Med_Id] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Potency] [float] NOT NULL,
	[Company] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__Medicin__B58A7BF56B20116E] PRIMARY KEY CLUSTERED 
(
	[Med_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MedicinRank]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicinRank](
	[Med_Id] [int] NOT NULL,
	[Ranking] [int] NOT NULL,
	[Doctor_Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK__MedicinR__B58A7BF5A5F3928C] PRIMARY KEY CLUSTERED 
(
	[Med_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pat_Die]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pat_Die](
	[Patient_Id] [int] NOT NULL,
	[Disease_Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Patient_Id] ASC,
	[Disease_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pat_Doc]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pat_Doc](
	[Patient_Id] [int] NOT NULL,
	[Doctor_Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK__Pat_Doc__2FF13E4B0ED0B974] PRIMARY KEY CLUSTERED 
(
	[Patient_Id] ASC,
	[Doctor_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pat_Medicin]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pat_Medicin](
	[Patient_Id] [int] NOT NULL,
	[Med_Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Patient_Id] ASC,
	[Med_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Patient]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Patient](
	[Patient_Id] [int] IDENTITY(1,1) NOT NULL,
	[S_Date] [datetime] NOT NULL,
	[E_Date] [datetime] NOT NULL,
	[Area] [varchar](max) NULL,
	[Remarks] [text] NULL,
	[Gender] [nvarchar](20) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Patient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Prescription]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prescription](
	[Pres_Id] [int] IDENTITY(1,1) NOT NULL,
	[patient_Id] [int] NOT NULL,
	[St_Date] [datetime] NOT NULL,
	[E_Date] [datetime] NULL,
	[Results] [text] NULL,
	[Doctor_Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK__Prescrip__702BC7EBA73D40FE] PRIMARY KEY CLUSTERED 
(
	[Pres_Id] ASC,
	[patient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Symptoms]    Script Date: 5/4/2019 8:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Symptoms](
	[Disease_Id] [int] NOT NULL,
	[symptom] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Disease_Id] ASC,
	[symptom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Doc_Specialization]  WITH CHECK ADD  CONSTRAINT [FK_Doc_Specialization_AspNetUsers] FOREIGN KEY([Doctor_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Doc_Specialization] CHECK CONSTRAINT [FK_Doc_Specialization_AspNetUsers]
GO
ALTER TABLE [dbo].[DoctorDegree]  WITH CHECK ADD  CONSTRAINT [FK_DoctorDegree_AspNetUsers] FOREIGN KEY([Doctor_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[DoctorDegree] CHECK CONSTRAINT [FK_DoctorDegree_AspNetUsers]
GO
ALTER TABLE [dbo].[DoctorDegree]  WITH CHECK ADD  CONSTRAINT [FK_DoctorDegree_Doctor] FOREIGN KEY([Doctor_Id])
REFERENCES [dbo].[Doctor] ([Doctor_Id])
GO
ALTER TABLE [dbo].[DoctorDegree] CHECK CONSTRAINT [FK_DoctorDegree_Doctor]
GO
ALTER TABLE [dbo].[Medication]  WITH CHECK ADD  CONSTRAINT [FK_Medication_Medicin] FOREIGN KEY([Med_Id])
REFERENCES [dbo].[Medicin] ([Med_Id])
GO
ALTER TABLE [dbo].[Medication] CHECK CONSTRAINT [FK_Medication_Medicin]
GO
ALTER TABLE [dbo].[Medication]  WITH CHECK ADD  CONSTRAINT [FK_Medication_Medicin1] FOREIGN KEY([Med_Id])
REFERENCES [dbo].[Medicin] ([Med_Id])
GO
ALTER TABLE [dbo].[Medication] CHECK CONSTRAINT [FK_Medication_Medicin1]
GO
ALTER TABLE [dbo].[MedicinRank]  WITH CHECK ADD  CONSTRAINT [FK_MedicinRank_AspNetUsers] FOREIGN KEY([Doctor_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[MedicinRank] CHECK CONSTRAINT [FK_MedicinRank_AspNetUsers]
GO
ALTER TABLE [dbo].[MedicinRank]  WITH CHECK ADD  CONSTRAINT [FK_MedicinRank_Medicin] FOREIGN KEY([Med_Id])
REFERENCES [dbo].[Medicin] ([Med_Id])
GO
ALTER TABLE [dbo].[MedicinRank] CHECK CONSTRAINT [FK_MedicinRank_Medicin]
GO
ALTER TABLE [dbo].[Pat_Die]  WITH CHECK ADD  CONSTRAINT [FK_Pat_Die_Disease1] FOREIGN KEY([Disease_Id])
REFERENCES [dbo].[Disease] ([Disease_Id])
GO
ALTER TABLE [dbo].[Pat_Die] CHECK CONSTRAINT [FK_Pat_Die_Disease1]
GO
ALTER TABLE [dbo].[Pat_Die]  WITH CHECK ADD  CONSTRAINT [FK_Pat_Die_Patient1] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Pat_Die] CHECK CONSTRAINT [FK_Pat_Die_Patient1]
GO
ALTER TABLE [dbo].[Pat_Doc]  WITH CHECK ADD  CONSTRAINT [FK_Pat_Doc_AspNetUsers] FOREIGN KEY([Doctor_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Pat_Doc] CHECK CONSTRAINT [FK_Pat_Doc_AspNetUsers]
GO
ALTER TABLE [dbo].[Pat_Doc]  WITH CHECK ADD  CONSTRAINT [FK_Pat_Doc_Patient] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Pat_Doc] CHECK CONSTRAINT [FK_Pat_Doc_Patient]
GO
ALTER TABLE [dbo].[Pat_Medicin]  WITH CHECK ADD  CONSTRAINT [FK_Pat_Medicin_Medicin] FOREIGN KEY([Med_Id])
REFERENCES [dbo].[Medicin] ([Med_Id])
GO
ALTER TABLE [dbo].[Pat_Medicin] CHECK CONSTRAINT [FK_Pat_Medicin_Medicin]
GO
ALTER TABLE [dbo].[Pat_Medicin]  WITH CHECK ADD  CONSTRAINT [FK_Pat_Medicin_Patient] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Pat_Medicin] CHECK CONSTRAINT [FK_Pat_Medicin_Patient]
GO
ALTER TABLE [dbo].[Prescription]  WITH CHECK ADD  CONSTRAINT [FK_Prescription_AspNetUsers] FOREIGN KEY([Doctor_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Prescription] CHECK CONSTRAINT [FK_Prescription_AspNetUsers]
GO
ALTER TABLE [dbo].[Prescription]  WITH CHECK ADD  CONSTRAINT [FK_Prescription_Patient] FOREIGN KEY([patient_Id])
REFERENCES [dbo].[Patient] ([Patient_Id])
GO
ALTER TABLE [dbo].[Prescription] CHECK CONSTRAINT [FK_Prescription_Patient]
GO
ALTER TABLE [dbo].[Symptoms]  WITH CHECK ADD FOREIGN KEY([Disease_Id])
REFERENCES [dbo].[Disease] ([Disease_Id])
GO
