USE [DB_WellCovered]
GO

-------------------------------------------------------------------------------------------------------------------------------------
ALTER TABLE [TB_Field]
	ADD IsSystemField BIT

ALTER TABLE [TB_Field]
	ADD [FieldOrder] INT

ALTER TABLE [TB_Field]
	ADD [IsDisplayable] BIT

ALTER TABLE [TB_Field]
	ADD [CanIndex] BIT

ALTER TABLE [TB_Field]
	ADD [Analyzer] INT

GO

UPDATE [TB_Field]
	SET IsSystemField = 0,
		IsDisplayable = 1,
		CanIndex = 1

GO

ALTER TABLE [TB_Field]
	ALTER COLUMN IsSystemField BIT NOT NULL

ALTER TABLE [TB_Field]
	ALTER COLUMN IsDisplayable BIT NOT NULL

ALTER TABLE [TB_Field]
	ALTER COLUMN CanIndex BIT NOT NULL

GO
