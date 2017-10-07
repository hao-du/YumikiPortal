--INSERT INTO DB_Administration-----------------------------------------------------------------------------------------------------------------------------------
USE DB_Administration

DECLARE @ExistedRootPrivilegeID UNIQUEIDENTIFIER;
SELECT @ExistedRootPrivilegeID = ID FROM TB_Privilege WHERE PrivilegeName = 'Money Trace'

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Report', '/MoneyTrace/Report/Index', 1, @ExistedRootPrivilegeID, 'Report page', 1, GETDATE(), NULL)
