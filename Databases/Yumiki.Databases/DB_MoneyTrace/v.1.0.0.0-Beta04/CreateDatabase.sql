CREATE TABLE [dbo].[TB_BankAccount](
	[ID] [uniqueidentifier] NOT NULL,
	[AccountNumber] [nvarchar](50) NULL,
	[DepositDate] [datetime] NULL,
	[WithdrawDate] [datetime] NULL,
	[Interest] [decimal](18, 2) NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[BankID] [uniqueidentifier] NOT NULL,
	[CurrencyID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_BankAccount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_TB_BankAccount_TB_Bank] FOREIGN KEY([BankID])
REFERENCES [dbo].[TB_Bank] ([ID])

ALTER TABLE [dbo].[TB_BankAccount] CHECK CONSTRAINT [FK_TB_BankAccount_TB_Bank]

ALTER TABLE [dbo].[TB_BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_TB_BankAccount_TB_Currency] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[TB_Currency] ([ID])

ALTER TABLE [dbo].[TB_BankAccount] CHECK CONSTRAINT [FK_TB_BankAccount_TB_Currency]

ALTER TABLE [dbo].[TB_BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_TB_BankAccount_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_BankAccount] CHECK CONSTRAINT [FK_TB_BankAccount_TB_User]

-------------------------------------------------------------------------------------------------------------------------------------
ALTER TABLE [dbo].[TB_Trace]
	ADD BankAccountID UNIQUEIDENTIFIER
	
ALTER TABLE [dbo].[TB_Trace]  WITH CHECK ADD CONSTRAINT [FK_TB_Trace_TB_BankAccount] FOREIGN KEY([BankAccountID])
REFERENCES [dbo].[TB_BankAccount] ([ID])

ALTER TABLE [dbo].[TB_Trace] CHECK CONSTRAINT [FK_TB_Trace_TB_BankAccount]

-------------------------------------------------------------------------------------------------------------------------------------
DELETE FROM [dbo].[TB_BankAccount]

ALTER TABLE [dbo].[TB_BankAccount]
	ADD Amount DECIMAL(18, 2) NOT NULL

