CREATE TABLE [dbo].[TB_Queue](
	[ID] [uniqueidentifier] NOT NULL,
	[QueueType] [int] NOT NULL,
	[Value1] [nvarchar](255) NULL,
	[Value2] [nvarchar](255) NULL,
	[Value3] [nvarchar](255) NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Queue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_Queue]  WITH CHECK ADD  CONSTRAINT [FK_TB_Queue_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_Queue] CHECK CONSTRAINT [FK_TB_Queue_TB_User]

-------------------------------------------------------------------------------------------------------------------------------------