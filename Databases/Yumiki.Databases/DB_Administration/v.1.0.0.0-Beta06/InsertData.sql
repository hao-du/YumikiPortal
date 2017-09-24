USE [DB_Administration]
GO

DECLARE @ExistedRootPrivilegeID UNIQUEIDENTIFIER;
SELECT @ExistedRootPrivilegeID = ID FROM TB_Privilege WHERE PrivilegeName = 'Well Covered'

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Search', '/WellCovered/Live/Search', 1, @ExistedRootPrivilegeID, 'Live Full Text Search page like Google', 1, GETDATE(), NULL)