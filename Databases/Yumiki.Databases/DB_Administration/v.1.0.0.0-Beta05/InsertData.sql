USE [DB_Administration]
GO

DECLARE @AdministrationID UNIQUEIDENTIFIER = NEWID();  
INSERT INTO TB_Privilege
VALUES(@AdministrationID, 'Well Covered', '/WellCovered/', 0, NULL, 'Root of Well Covered Module', 1, GETDATE(), NULL)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Application', '/WellCovered/App/List', 1, @AdministrationID, 'Application Page', 1, GETDATE(), NULL)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Object', '/WellCovered/Object/List', 1, @AdministrationID, 'Object Page', 1, GETDATE(), NULL)
