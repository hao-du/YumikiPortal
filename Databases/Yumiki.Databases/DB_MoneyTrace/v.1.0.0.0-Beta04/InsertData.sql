--v.1.0.0.0-beta4-----------------------------------------------------------------------------------------------------------------------------------

DECLARE @ExistedRootPrivilegeID UNIQUEIDENTIFIER;
SELECT @ExistedRootPrivilegeID = ID FROM TB_Privilege WHERE PrivilegeName = 'Money Trace'

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Bank Account', '/MoneyTrace/BankAccount/Index', 1, @ExistedRootPrivilegeID, 'Bank Account page', 1, GETDATE(), NULL)
