USE [DB_Administration]
GO

ALTER TABLE [dbo].[TB_Privilege]
	ADD [PrivilegeOrder] INT NULL
GO
UPDATE [dbo].[TB_Privilege]
SET [PrivilegeOrder] = 0
GO
ALTER TABLE [dbo].[TB_Privilege]
  ALTER COLUMN [PrivilegeOrder] INT NOT NULL