USE [DB_Shopper]
GO

/****** Object:  Table [dbo].[TB_ProductQuantityOffset]    Script Date: 8/20/2019 1:13:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_ProductQuantityOffset](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductID] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_ProductQuantityOffset] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TB_ProductQuantityOffset]  WITH CHECK ADD  CONSTRAINT [FK_TB_ProductQuantityOffset_TB_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[TB_Product] ([ID])
GO

ALTER TABLE [dbo].[TB_ProductQuantityOffset] CHECK CONSTRAINT [FK_TB_ProductQuantityOffset_TB_Product]
GO

ALTER TABLE [dbo].[TB_ProductQuantityOffset]  WITH CHECK ADD  CONSTRAINT [FK_TB_ProductQuantityOffset_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])
GO

ALTER TABLE [dbo].[TB_ProductQuantityOffset] CHECK CONSTRAINT [FK_TB_ProductQuantityOffset_TB_User]
GO

ALTER TABLE [dbo].[TB_Stock]
	ADD ProductQuantityOffsetID UNIQUEIDENTIFIER NULL
GO

ALTER TABLE [dbo].[TB_Stock]  WITH CHECK ADD  CONSTRAINT [FK_TB_Stock_TB_ProductQuantityOffset] FOREIGN KEY([ProductQuantityOffsetID])
REFERENCES [dbo].[TB_ProductQuantityOffset] ([ID])
GO

ALTER TABLE [dbo].[TB_Stock] CHECK CONSTRAINT [FK_TB_Stock_TB_ProductQuantityOffset]
GO