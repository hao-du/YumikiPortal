ALTER TABLE [dbo].[TB_Field]
DROP COLUMN [HasAttachments]
GO

ALTER TABLE [dbo].[TB_Field]
ADD [DataSortByOrder] INT
GO

-------------------------------------------------------------------------------------------------------------------------------------