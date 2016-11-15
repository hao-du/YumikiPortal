CREATE TABLE [dbo].[TB_User](
	[ID] [uniqueidentifier] NOT NULL,
	[UserLoginName] [varchar](20) NOT NULL,
	[FirstName] [nvarchar](15) NULL,
	[LastName] [nvarchar](15) NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_Category](
	[ID] [uniqueidentifier] NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[UserID] [uniqueidentifier] NULL,
	[TransactionTypeID] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_Category]  WITH CHECK ADD  CONSTRAINT [FK_TB_Category_TB_TransactionType] FOREIGN KEY([TransactionTypeID])
REFERENCES [dbo].[TB_TransactionType] ([ID])

ALTER TABLE [dbo].[TB_Category] CHECK CONSTRAINT [FK_TB_Category_TB_TransactionType]

ALTER TABLE [dbo].[TB_Category]  WITH CHECK ADD  CONSTRAINT [FK_TB_Category_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_Category] CHECK CONSTRAINT [FK_TB_Category_TB_User]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_Currency](
	[ID] [uniqueidentifier] NOT NULL,
	[CurrencyName] [nvarchar](50) NOT NULL,
	[CurrencyShortName] [nvarchar](10) NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Currency] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_Currency]  WITH CHECK ADD  CONSTRAINT [FK_TB_Currency_TB_Currency] FOREIGN KEY([ID])
REFERENCES [dbo].[TB_Currency] ([ID])

ALTER TABLE [dbo].[TB_Currency] CHECK CONSTRAINT [FK_TB_Currency_TB_Currency]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_TransactionType](
	[ID] [uniqueidentifier] NOT NULL,
	[TransactionTypeName] [nvarchar](20) NOT NULL,
	[IsIncome] [bit] NOT NULL,
	[IsTransfer] [bit] NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_TransactionType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_TransactionType]  WITH CHECK ADD  CONSTRAINT [FK_TB_TransactionType_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_TransactionType] CHECK CONSTRAINT [FK_TB_TransactionType_TB_User]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_Trace](
	[ID] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TraceDate] [datetime] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CategoryID] [uniqueidentifier] NOT NULL,
	[CurrencyID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Trace] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_Trace]  WITH CHECK ADD  CONSTRAINT [FK_TB_Trace_TB_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[TB_Category] ([ID])

ALTER TABLE [dbo].[TB_Trace] CHECK CONSTRAINT [FK_TB_Trace_TB_Category]

ALTER TABLE [dbo].[TB_Trace]  WITH CHECK ADD  CONSTRAINT [FK_TB_Trace_TB_Currency] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[TB_Currency] ([ID])

ALTER TABLE [dbo].[TB_Trace] CHECK CONSTRAINT [FK_TB_Trace_TB_Currency]

ALTER TABLE [dbo].[TB_Trace]  WITH CHECK ADD  CONSTRAINT [FK_TB_Trace_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_Trace] CHECK CONSTRAINT [FK_TB_Trace_TB_User]