CREATE TABLE [dbo].[TB_Attachment](
	[ID] [uniqueidentifier] NOT NULL,
	[AttachmentName] [nvarchar](50) NOT NULL,
	[AttachmentPath] [ntext] NOT NULL,
	[LiveRecordID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Attachment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-------------------------------------------------------------------------------------------------------------------------------------

ALTER TABLE [TB_Field]
	ADD HasAttachments BIT
GO

UPDATE [TB_Field]
	SET HasAttachments = 0
GO

ALTER TABLE [TB_Field]
	ALTER COLUMN HasAttachments BIT NOT NULL
GO