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

CREATE TABLE [dbo].[TB_Tag](
	[ID] [uniqueidentifier] NOT NULL,
	[TagName] [nvarchar](50) NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[UserID] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_Tag]  WITH CHECK ADD  CONSTRAINT [FK_TB_Category_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_Tag] CHECK CONSTRAINT [FK_TB_Category_TB_User]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_Currency](
	[ID] [uniqueidentifier] NOT NULL,
	[CurrencyName] [nvarchar](50) NOT NULL,
	[CurrencyShortName] [nvarchar](10) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsShareable] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Currency] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_Currency]  WITH CHECK ADD  CONSTRAINT [FK_TB_Currency_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_Currency] CHECK CONSTRAINT [FK_TB_Currency_TB_User]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_Trace](
	[ID] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TraceDate] [datetime] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Tags] [ntext] NULL,
	[CurrencyID] [uniqueidentifier] NOT NULL,
	[TransactionType] [int] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[GroupTokenID] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Trace] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[TB_Trace]  WITH CHECK ADD  CONSTRAINT [FK_TB_Trace_TB_Currency] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[TB_Currency] ([ID])

ALTER TABLE [dbo].[TB_Trace] CHECK CONSTRAINT [FK_TB_Trace_TB_Currency]

ALTER TABLE [dbo].[TB_Trace]  WITH CHECK ADD  CONSTRAINT [FK_TB_Trace_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_Trace] CHECK CONSTRAINT [FK_TB_Trace_TB_User]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store keywords such as "Shopping, Deposit ABC Bank, Buy stuff" for filter purpose. Keywords are splitted by '','' and save in TB_Tag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Trace', @level2type=N'COLUMN',@level2name=N'Tags'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'In case of Banking, generating 2 records: Deposite to bank account and withdraw from personal amount.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Trace', @level2type=N'COLUMN',@level2name=N'GroupTokenID'

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_Bank](
	[ID] [uniqueidentifier] NOT NULL,
	[BankName] [nvarchar](50) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsShareable] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Bank] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

-------------------------------------------------------------------------------------------------------------------------------------

ALTER TABLE [dbo].[TB_Trace]
ADD BankID UniqueIdentifier NOT NULL

ALTER TABLE [dbo].[TB_Trace]  WITH CHECK ADD  CONSTRAINT [FK_TB_Trace_TB_Bank] FOREIGN KEY([BankID])
REFERENCES [dbo].[TB_Bank] ([ID])

ALTER TABLE [dbo].[TB_Trace] CHECK CONSTRAINT [FK_TB_Trace_TB_Bank]

-------------------------------------------------------------------------------------------------------------------------------------