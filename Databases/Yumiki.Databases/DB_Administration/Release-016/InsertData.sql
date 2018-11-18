USE [DB_Administration]
GO

DECLARE @ID UNIQUEIDENTIFIER;
SELECT @ID = ID FROM TB_Privilege WHERE PrivilegeName = 'Money Trace' AND ParentPrivilegeID IS NULL

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Trace Template', '/MoneyTrace/TraceTemplate/Index', 1, @ID, 'Trace Template Setup Page', 1, GETDATE(), NULL, 3600)
