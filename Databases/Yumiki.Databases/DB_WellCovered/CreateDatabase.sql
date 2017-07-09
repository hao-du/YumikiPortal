USE [DB_WellCovered]
GO
-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_User](
	[ID] [uniqueidentifier] NOT NULL,
	[UserLoginName] [varchar](20) NOT NULL,
	[FirstName] [nvarchar](15) NULL,
	[LastName] [nvarchar](15) NULL,
	[TimeZone] [varchar](100) NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_App](
	[ID] [uniqueidentifier] NOT NULL,
	[AppName] [nvarchar](50) NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[IsShareable] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_App] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TB_App]  WITH CHECK ADD  CONSTRAINT [FK_TB_User_TB_App] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])
GO

ALTER TABLE [dbo].[TB_App] CHECK CONSTRAINT [FK_TB_User_TB_App]
GO

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_Object](
	[ID] [uniqueidentifier] NOT NULL,
	[ObjectName] [nvarchar](50) NOT NULL,
	[DisplayName] [nvarchar](50) NOT NULL,
	[ApiName] [varchar](50) NOT NULL,
	[AppID] [uniqueidentifier] NULL,
	[Descriptions] [nvarchar](255) NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Object] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TB_Object]  WITH CHECK ADD  CONSTRAINT [FK_TB_Object_TB_Object] FOREIGN KEY([AppID])
REFERENCES [dbo].[TB_App] ([ID])
GO

ALTER TABLE [dbo].[TB_Object] CHECK CONSTRAINT [FK_TB_Object_TB_Object]
GO

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_Field](
	[ID] [uniqueidentifier] NOT NULL,
	[FieldName] [nvarchar](50) NOT NULL,
	[DisplayName] [nvarchar](50) NOT NULL,
	[ApiName] [varchar](50) NOT NULL,
	[ObjectID] [uniqueidentifier] NOT NULL,
	[FieldType] [int] NOT NULL,
	[FieldLength] [int] NULL,
	[IsRequired] [bit] NOT NULL,
	[Datasource] [ntext] NULL,
	[Descriptions] [nvarchar](255) NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Field] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[TB_Field]  WITH CHECK ADD  CONSTRAINT [FK_TB_Field_TB_Object] FOREIGN KEY([ObjectID])
REFERENCES [dbo].[TB_Object] ([ID])
GO

ALTER TABLE [dbo].[TB_Field] CHECK CONSTRAINT [FK_TB_Field_TB_Object]
GO

ALTER TABLE [dbo].[TB_Field]  WITH CHECK ADD  CONSTRAINT [FK_TB_Field_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])
GO

ALTER TABLE [dbo].[TB_Field] CHECK CONSTRAINT [FK_TB_Field_TB_User]
GO
