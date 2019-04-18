USE [DB_Shopper] 

GO 

/****** Object:  Table [dbo].[TB_AdditionalFee]    Script Date: 4/18/2019 12:48:55 PM ******/ 

SET ANSI_NULLS ON 

GO 

SET QUOTED_IDENTIFIER ON 

GO 

CREATE TABLE [dbo].[TB_AdditionalFee]( 

    [ID] [uniqueidentifier] NOT NULL, 

    [FeeTypeID] [uniqueidentifier] NOT NULL, 

    [Amount] [decimal](18, 2) NOT NULL, 

    [FeeIssueDate] [datetime] NOT NULL, 

    [UserID] [uniqueidentifier] NOT NULL, 

    [Descriptions] [nvarchar](255) NULL, 

    [IsActive] [bit] NOT NULL, 

    [CreateDate] [datetime] NOT NULL, 

    [LastUpdateDate] [datetime] NULL, 

CONSTRAINT [PK_TB_AdditionalFee] PRIMARY KEY CLUSTERED  

( 

    [ID] ASC 

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 

) ON [PRIMARY] 

GO 

/****** Object:  Table [dbo].[TB_FeeType]    Script Date: 4/18/2019 12:48:55 PM ******/ 

SET ANSI_NULLS ON 

GO 

SET QUOTED_IDENTIFIER ON 

GO 

CREATE TABLE [dbo].[TB_FeeType]( 

    [ID] [uniqueidentifier] NOT NULL, 

    [FeeTypeName] [nvarchar](255) NOT NULL, 

    [ShowInReceipt] [bit] NOT NULL, 

    [ShowInInvoice] [bit] NOT NULL, 

    [ShowInAdditionalFee] [bit] NOT NULL, 

    [UserID] [uniqueidentifier] NOT NULL, 

    [Descriptions] [nvarchar](255) NULL, 

    [IsActive] [bit] NOT NULL, 

    [CreateDate] [datetime] NOT NULL, 

    [LastUpdateDate] [datetime] NULL, 

CONSTRAINT [PK_TB_FeeType] PRIMARY KEY CLUSTERED  

( 

    [ID] ASC 

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 

) ON [PRIMARY] 

GO 

/****** Object:  Table [dbo].[TB_Invoice]    Script Date: 4/18/2019 12:48:55 PM ******/ 

SET ANSI_NULLS ON 

GO 

SET QUOTED_IDENTIFIER ON 

GO 

CREATE TABLE [dbo].[TB_Invoice]( 

    [ID] [uniqueidentifier] NOT NULL, 

    [InvoiceNumber] [varchar](50) NOT NULL, 

    [CustomerName] [nvarchar](50) NOT NULL, 

    [CustomerAddress] [nvarchar](255) NOT NULL, 

    [CustomerPhone] [varchar](20) NOT NULL, 

    [CustomerEmail] [varchar](20) NULL, 

    [CustomerNote] [ntext] NULL, 

    [TotalAmount] [decimal](18, 2) NOT NULL, 

    [InvoiceDate] [datetime] NOT NULL, 

    [Status] [int] NOT NULL, 

    [UserID] [uniqueidentifier] NOT NULL, 

    [Descriptions] [nvarchar](255) NULL, 

    [IsActive] [bit] NOT NULL, 

    [CreateDate] [datetime] NOT NULL, 

    [LastUpdateDate] [datetime] NULL, 

CONSTRAINT [PK_TB_Invoice] PRIMARY KEY CLUSTERED  

( 

    [ID] ASC 

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] 

GO 

/****** Object:  Table [dbo].[TB_InvoiceDetail]    Script Date: 4/18/2019 12:48:55 PM ******/ 

SET ANSI_NULLS ON 

GO 

SET QUOTED_IDENTIFIER ON 

GO 

CREATE TABLE [dbo].[TB_InvoiceDetail]( 

    [ID] [uniqueidentifier] NOT NULL, 

    [InvoiceID] [uniqueidentifier] NOT NULL, 

    [ProductID] [uniqueidentifier] NOT NULL, 

    [OriginalPrice] [decimal](18, 2) NOT NULL, 

    [UnitPrice] [decimal](18, 2) NOT NULL, 

    [Quantity] [int] NOT NULL, 

    [Amount] [decimal](18, 2) NOT NULL, 

    [UserID] [uniqueidentifier] NOT NULL, 

    [Descriptions] [nvarchar](255) NULL, 

    [IsActive] [bit] NOT NULL, 

    [CreateDate] [datetime] NOT NULL, 

    [LastUpdateDate] [datetime] NULL, 

CONSTRAINT [PK_TB_InvoiceDetail] PRIMARY KEY CLUSTERED  

( 

    [ID] ASC 

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 

) ON [PRIMARY] 

GO 

/****** Object:  Table [dbo].[TB_InvoiceExtraFee]    Script Date: 4/18/2019 12:48:55 PM ******/ 

SET ANSI_NULLS ON 

GO 

SET QUOTED_IDENTIFIER ON 

GO 

CREATE TABLE [dbo].[TB_InvoiceExtraFee]( 

    [ID] [uniqueidentifier] NOT NULL, 

    [InvoiceID] [uniqueidentifier] NOT NULL, 

    [FeeTypeID] [uniqueidentifier] NOT NULL, 

    [Amount] [decimal](18, 2) NOT NULL, 

    [UserID] [uniqueidentifier] NOT NULL, 

    [Descriptions] [nvarchar](255) NULL, 

    [IsActive] [bit] NOT NULL, 

    [CreateDate] [datetime] NOT NULL, 

    [LastUpdateDate] [datetime] NULL, 

CONSTRAINT [PK_TB_InvoiceExtraFee] PRIMARY KEY CLUSTERED  

( 

    [ID] ASC 

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 

) ON [PRIMARY] 

GO 

/****** Object:  Table [dbo].[TB_Product]    Script Date: 4/18/2019 12:48:55 PM ******/ 

SET ANSI_NULLS ON 

GO 

SET QUOTED_IDENTIFIER ON 

GO 

CREATE TABLE [dbo].[TB_Product]( 

    [ID] [uniqueidentifier] NOT NULL, 

    [ProductName] [nvarchar](255) NOT NULL, 

    [ProductCode] [nvarchar](50) NOT NULL, 

    [Price] [decimal](18, 2) NOT NULL, 

    [FeaturedImage] [nvarchar](2000) NOT NULL, 

    [SourceUrl] [nvarchar](2000) NULL, 

    [Keywords] [ntext] NULL, 

    [UserID] [uniqueidentifier] NOT NULL, 

    [Descriptions] [nvarchar](255) NULL, 

    [IsActive] [bit] NOT NULL, 

    [CreateDate] [datetime] NOT NULL, 

    [LastUpdateDate] [datetime] NULL, 

CONSTRAINT [PK_TB_Product] PRIMARY KEY CLUSTERED  

( 

    [ID] ASC 

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 

) ON [PRIMARY] 

GO 

/****** Object:  Table [dbo].[TB_Receipt]    Script Date: 4/18/2019 12:48:55 PM ******/ 

SET ANSI_NULLS ON 

GO 

SET QUOTED_IDENTIFIER ON 

GO 

CREATE TABLE [dbo].[TB_Receipt]( 

    [ID] [uniqueidentifier] NOT NULL, 

    [ExternalReceiptID] [nvarchar](50) NOT NULL, 

    [ExternalReceiptUrl] [nvarchar](2000) NULL, 

    [TotalAmount] [decimal](18, 2) NOT NULL, 

    [ReceiptDate] [datetime] NOT NULL, 

    [Status] [int] NOT NULL, 

    [UserID] [uniqueidentifier] NOT NULL, 

    [Descriptions] [nvarchar](255) NULL, 

    [IsActive] [bit] NOT NULL, 

    [CreateDate] [datetime] NOT NULL, 

    [LastUpdateDate] [datetime] NULL, 

CONSTRAINT [PK_TB_Receipt] PRIMARY KEY CLUSTERED  

( 

    [ID] ASC 

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 

) ON [PRIMARY] 

GO 

/****** Object:  Table [dbo].[TB_ReceiptDetail]    Script Date: 4/18/2019 12:48:55 PM ******/ 

SET ANSI_NULLS ON 

GO 

SET QUOTED_IDENTIFIER ON 

GO 

CREATE TABLE [dbo].[TB_ReceiptDetail]( 

    [ID] [uniqueidentifier] NOT NULL, 

    [ReceiptID] [uniqueidentifier] NOT NULL, 

    [ProductID] [uniqueidentifier] NOT NULL, 

    [UnitPrice] [decimal](18, 2) NOT NULL, 

    [Quantity] [int] NOT NULL, 

    [Amount] [decimal](18, 2) NOT NULL, 

    [UserID] [uniqueidentifier] NOT NULL, 

    [Descriptions] [nvarchar](255) NULL, 

    [IsActive] [bit] NOT NULL, 

    [CreateDate] [datetime] NOT NULL, 

    [LastUpdateDate] [datetime] NULL, 

CONSTRAINT [PK_TB_ReceiptDetail] PRIMARY KEY CLUSTERED  

( 

    [ID] ASC 

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 

) ON [PRIMARY] 

GO 

/****** Object:  Table [dbo].[TB_ReceiptExtraFee]    Script Date: 4/18/2019 12:48:55 PM ******/ 

SET ANSI_NULLS ON 

GO 

SET QUOTED_IDENTIFIER ON 

GO 

CREATE TABLE [dbo].[TB_ReceiptExtraFee]( 

    [ID] [uniqueidentifier] NOT NULL, 

    [ReceiptID] [uniqueidentifier] NOT NULL, 

    [FeeTypeID] [uniqueidentifier] NOT NULL, 

    [Amount] [decimal](18, 2) NOT NULL, 

    [UserID] [uniqueidentifier] NOT NULL, 

    [Descriptions] [nvarchar](255) NULL, 

    [IsActive] [bit] NOT NULL, 

    [CreateDate] [datetime] NOT NULL, 

    [LastUpdateDate] [datetime] NULL, 

CONSTRAINT [PK_TB_ReceiptExtraFee] PRIMARY KEY CLUSTERED  

( 

    [ID] ASC 

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 

) ON [PRIMARY] 

GO 

/****** Object:  Table [dbo].[TB_Stock]    Script Date: 4/18/2019 12:48:55 PM ******/ 

SET ANSI_NULLS ON 

GO 

SET QUOTED_IDENTIFIER ON 

GO 

CREATE TABLE [dbo].[TB_Stock]( 

    [ID] [uniqueidentifier] NOT NULL, 

    [InvoiceDetailID] [uniqueidentifier] NULL, 

    [ReceiptDetailID] [uniqueidentifier] NULL, 

    [Quantity] [int] NOT NULL, 

    [UserID] [uniqueidentifier] NOT NULL, 

    [Descriptions] [nvarchar](255) NULL, 

    [IsActive] [bit] NOT NULL, 

    [CreateDate] [datetime] NOT NULL, 

    [LastUpdateDate] [datetime] NULL, 

CONSTRAINT [PK_TB_Stock] PRIMARY KEY CLUSTERED  

( 

    [ID] ASC 

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 

) ON [PRIMARY] 

GO 

/****** Object:  Table [dbo].[TB_User]    Script Date: 4/18/2019 12:48:55 PM ******/ 

SET ANSI_NULLS ON 

GO 

SET QUOTED_IDENTIFIER ON 

GO 

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

ALTER TABLE [dbo].[TB_AdditionalFee]  WITH CHECK ADD  CONSTRAINT [FK_TB_AdditionalFee_TB_FeeType] FOREIGN KEY([FeeTypeID]) 

REFERENCES [dbo].[TB_FeeType] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_AdditionalFee] CHECK CONSTRAINT [FK_TB_AdditionalFee_TB_FeeType] 

GO 

ALTER TABLE [dbo].[TB_AdditionalFee]  WITH CHECK ADD  CONSTRAINT [FK_TB_AdditionalFee_TB_User] FOREIGN KEY([UserID]) 

REFERENCES [dbo].[TB_User] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_AdditionalFee] CHECK CONSTRAINT [FK_TB_AdditionalFee_TB_User] 

GO 

ALTER TABLE [dbo].[TB_FeeType]  WITH CHECK ADD  CONSTRAINT [FK_TB_FeeType_TB_User] FOREIGN KEY([UserID]) 

REFERENCES [dbo].[TB_User] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_FeeType] CHECK CONSTRAINT [FK_TB_FeeType_TB_User] 

GO 

ALTER TABLE [dbo].[TB_Invoice]  WITH CHECK ADD  CONSTRAINT [FK_TB_Invoice_TB_User] FOREIGN KEY([UserID]) 

REFERENCES [dbo].[TB_User] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_Invoice] CHECK CONSTRAINT [FK_TB_Invoice_TB_User] 

GO 

ALTER TABLE [dbo].[TB_InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_TB_InvoiceDetail_TB_Invoice] FOREIGN KEY([InvoiceID]) 

REFERENCES [dbo].[TB_Invoice] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_InvoiceDetail] CHECK CONSTRAINT [FK_TB_InvoiceDetail_TB_Invoice] 

GO 

ALTER TABLE [dbo].[TB_InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_TB_InvoiceDetail_TB_Product] FOREIGN KEY([ProductID]) 

REFERENCES [dbo].[TB_Product] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_InvoiceDetail] CHECK CONSTRAINT [FK_TB_InvoiceDetail_TB_Product] 

GO 

ALTER TABLE [dbo].[TB_InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_TB_InvoiceDetail_TB_User] FOREIGN KEY([UserID]) 

REFERENCES [dbo].[TB_User] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_InvoiceDetail] CHECK CONSTRAINT [FK_TB_InvoiceDetail_TB_User] 

GO 

ALTER TABLE [dbo].[TB_InvoiceExtraFee]  WITH CHECK ADD  CONSTRAINT [FK_TB_InvoiceExtraFee_TB_FeeType] FOREIGN KEY([FeeTypeID]) 

REFERENCES [dbo].[TB_FeeType] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_InvoiceExtraFee] CHECK CONSTRAINT [FK_TB_InvoiceExtraFee_TB_FeeType] 

GO 

ALTER TABLE [dbo].[TB_InvoiceExtraFee]  WITH CHECK ADD  CONSTRAINT [FK_TB_InvoiceExtraFee_TB_Invoice] FOREIGN KEY([InvoiceID]) 

REFERENCES [dbo].[TB_Invoice] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_InvoiceExtraFee] CHECK CONSTRAINT [FK_TB_InvoiceExtraFee_TB_Invoice] 

GO 

ALTER TABLE [dbo].[TB_InvoiceExtraFee]  WITH CHECK ADD  CONSTRAINT [FK_TB_InvoiceExtraFee_TB_User] FOREIGN KEY([UserID]) 

REFERENCES [dbo].[TB_User] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_InvoiceExtraFee] CHECK CONSTRAINT [FK_TB_InvoiceExtraFee_TB_User] 

GO 

ALTER TABLE [dbo].[TB_Product]  WITH CHECK ADD  CONSTRAINT [FK_TB_Product_TB_User] FOREIGN KEY([UserID]) 

REFERENCES [dbo].[TB_User] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_Product] CHECK CONSTRAINT [FK_TB_Product_TB_User] 

GO 

ALTER TABLE [dbo].[TB_Receipt]  WITH CHECK ADD  CONSTRAINT [FK_TB_Receipt_TB_User] FOREIGN KEY([UserID]) 

REFERENCES [dbo].[TB_User] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_Receipt] CHECK CONSTRAINT [FK_TB_Receipt_TB_User] 

GO 

ALTER TABLE [dbo].[TB_ReceiptDetail]  WITH CHECK ADD  CONSTRAINT [FK_TB_ReceiptDetail_TB_Product] FOREIGN KEY([ProductID]) 

REFERENCES [dbo].[TB_Product] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_ReceiptDetail] CHECK CONSTRAINT [FK_TB_ReceiptDetail_TB_Product] 

GO 

ALTER TABLE [dbo].[TB_ReceiptDetail]  WITH CHECK ADD  CONSTRAINT [FK_TB_ReceiptDetail_TB_Receipt] FOREIGN KEY([ReceiptID]) 

REFERENCES [dbo].[TB_Receipt] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_ReceiptDetail] CHECK CONSTRAINT [FK_TB_ReceiptDetail_TB_Receipt] 

GO 

ALTER TABLE [dbo].[TB_ReceiptDetail]  WITH CHECK ADD  CONSTRAINT [FK_TB_ReceiptDetail_TB_User] FOREIGN KEY([UserID]) 

REFERENCES [dbo].[TB_User] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_ReceiptDetail] CHECK CONSTRAINT [FK_TB_ReceiptDetail_TB_User] 

GO 

ALTER TABLE [dbo].[TB_ReceiptExtraFee]  WITH CHECK ADD  CONSTRAINT [FK_TB_ReceiptExtraFee_TB_FeeType] FOREIGN KEY([FeeTypeID]) 

REFERENCES [dbo].[TB_FeeType] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_ReceiptExtraFee] CHECK CONSTRAINT [FK_TB_ReceiptExtraFee_TB_FeeType] 

GO 

ALTER TABLE [dbo].[TB_ReceiptExtraFee]  WITH CHECK ADD  CONSTRAINT [FK_TB_ReceiptExtraFee_TB_Receipt] FOREIGN KEY([ReceiptID]) 

REFERENCES [dbo].[TB_Receipt] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_ReceiptExtraFee] CHECK CONSTRAINT [FK_TB_ReceiptExtraFee_TB_Receipt] 

GO 

ALTER TABLE [dbo].[TB_ReceiptExtraFee]  WITH CHECK ADD  CONSTRAINT [FK_TB_ReceiptExtraFee_TB_User] FOREIGN KEY([UserID]) 

REFERENCES [dbo].[TB_User] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_ReceiptExtraFee] CHECK CONSTRAINT [FK_TB_ReceiptExtraFee_TB_User] 

GO 

ALTER TABLE [dbo].[TB_Stock]  WITH CHECK ADD  CONSTRAINT [FK_TB_Stock_TB_InvoiceDetail] FOREIGN KEY([InvoiceDetailID]) 

REFERENCES [dbo].[TB_InvoiceDetail] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_Stock] CHECK CONSTRAINT [FK_TB_Stock_TB_InvoiceDetail] 

GO 

ALTER TABLE [dbo].[TB_Stock]  WITH CHECK ADD  CONSTRAINT [FK_TB_Stock_TB_ReceiptDetail] FOREIGN KEY([ReceiptDetailID]) 

REFERENCES [dbo].[TB_ReceiptDetail] ([ID]) 

GO 

ALTER TABLE [dbo].[TB_Stock] CHECK CONSTRAINT [FK_TB_Stock_TB_ReceiptDetail] 

GO 

  

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'This is the Receipt ID from External System' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Receipt', @level2type=N'COLUMN',@level2name=N'ExternalReceiptID' 

GO 