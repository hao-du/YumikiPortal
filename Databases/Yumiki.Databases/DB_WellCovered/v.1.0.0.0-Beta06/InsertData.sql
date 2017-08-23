USE [DB_WellCovered]
GO

-------------------------------------------------------------------------------------------------------------------------------------
ALTER TABLE [TB_Field]
	ADD IsSystemField BIT

ALTER TABLE [TB_Field]
	ADD [FieldOrder] INT

ALTER TABLE [TB_Field]
	ADD [IsDisplayable] BIT

ALTER TABLE [TB_Field]
	ADD [CanIndex] BIT

GO

UPDATE [TB_Field]
	SET IsSystemField = 0,
		IsDisplayable = 1,
		CanIndex = 1

GO

ALTER TABLE [TB_Field]
	ALTER COLUMN IsSystemField BIT NOT NULL

ALTER TABLE [TB_Field]
	ALTER COLUMN IsDisplayable BIT NOT NULL

ALTER TABLE [TB_Field]
	ALTER COLUMN CanIndex BIT NOT NULL

GO

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_Index](
	[ID] [uniqueidentifier] NOT NULL,
	[AppID] [uniqueidentifier] NULL,
	[ObjectID] [uniqueidentifier] NOT NULL,
	[LiveID] [uniqueidentifier] NOT NULL,
	[FullTextIndex] [ntext] NOT NULL,
	[DisplayContents] [ntext] NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Index] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-------------------------------------------------------------------------------------------------------------------------------------

/****** Object:  FullTextCatalog [FTS_WellCovered]    Script Date: 8/22/2017 9:57:02 PM ******/
CREATE FULLTEXT CATALOG [FTS_WellCovered]WITH ACCENT_SENSITIVITY = ON
GO
 
CREATE FULLTEXT INDEX ON [dbo].[TB_Index]  
(   
  [FullTextIndex] Language 1033     
)   
  KEY INDEX [PK_TB_Index] ON [FTS_WellCovered];   
GO 