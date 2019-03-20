USE [DB_MoneyTrace]
GO

/****** Object:  Table [dbo].[TB_TraceTemplate]    Script Date: 11/18/2018 10:51:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_TraceTemplate](
	[ID] [uniqueidentifier] NOT NULL,
	[TemplateName] [nvarchar](50) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[TransferredUserID] [uniqueidentifier] NULL,
	[Tags] [ntext] NULL,
	[CurrencyID] [uniqueidentifier] NOT NULL,
	[TransactionType] [int] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_TraceTemplate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[TB_TraceTemplate]  WITH CHECK ADD  CONSTRAINT [FK_TB_TraceTemplate_TB_Currency] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[TB_Currency] ([ID])
GO

ALTER TABLE [dbo].[TB_TraceTemplate] CHECK CONSTRAINT [FK_TB_TraceTemplate_TB_Currency]
GO

ALTER TABLE [dbo].[TB_TraceTemplate]  WITH CHECK ADD  CONSTRAINT [FK_TB_TraceTemplate_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])
GO

ALTER TABLE [dbo].[TB_TraceTemplate] CHECK CONSTRAINT [FK_TB_TraceTemplate_TB_User]
GO

ALTER TABLE [dbo].[TB_TraceTemplate]  WITH CHECK ADD  CONSTRAINT [FK_TB_TraceTemplate_TB_User1] FOREIGN KEY([TransferredUserID])
REFERENCES [dbo].[TB_User] ([ID])
GO

ALTER TABLE [dbo].[TB_TraceTemplate] CHECK CONSTRAINT [FK_TB_TraceTemplate_TB_User1]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store keywords such as "Shopping, Deposit ABC Bank, Buy stuff" for filter purpose. Keywords are splitted by '','' and save in TB_Tag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_TraceTemplate', @level2type=N'COLUMN',@level2name=N'Tags'
GO

-------------------------------------------------------------------------------------------------------------------------------------
--Fix NULL VALUE FOR E_BANKING TYPE
UPDATE TA
SET TA.BankAccountID = TB.BankAccountID
FROM TB_Trace TA
	INNER JOIN TB_Trace TB ON TA.GroupTokenID = TB.GroupTokenID AND TA.ID <> TB.ID
WHERE TA.BankAccountID IS NULL AND TA.BankID IS NOT NULL

UPDATE TA
SET TA.GroupTokenID = TB.GroupTokenID
FROM TB_Trace TA
	INNER JOIN TB_Trace TB ON TA.BankAccountID = TB.BankAccountID AND TA.ID <> TB.ID AND TA.BankID = TB.BankID AND TB.TransactionType = 2 AND TB.TraceDate = TA.TraceDate
WHERE TA.GroupTokenID IS NULL AND TA.BankID IS NOT NULL 

