﻿DECLARE @AdministrationID UNIQUEIDENTIFIER = NEWID();  
INSERT INTO TB_Privilege
VALUES(@AdministrationID, 'Administration', '/Administration/', 0, NULL, 'Root of Administration Module', 1, GETDATE(), NULL)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'User', '/Administration/User', 1, @AdministrationID, 'User Page', 1, GETDATE(), NULL)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Group', '/Administration/Group', 1, @AdministrationID, 'Group Page', 1, GETDATE(), NULL)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Privilege', '/Administration/Privilege', 1, @AdministrationID, 'Privilege Page', 1, GETDATE(), NULL)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Contact Type', '/Administration/ContactType', 1, @AdministrationID, 'Contact Type Page', 1, GETDATE(), NULL)

-------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO [dbo].[TB_User]
VALUES (NEWID(), 'Administrator', 'rMjUHljPYXs41v7DsdvTHUPF5nnhzuQU81+85Oggr04=', 'Admin', 'Account', 'SE Asia Standard Time' ,'administrator account', 1, GETDATE(), NULL)

--v.1.0.0.0-beta3-----------------------------------------------------------------------------------------------------------------------------------

DECLARE @ExistedAdministrationPrivilegeID UNIQUEIDENTIFIER;
SELECT @ExistedAdministrationPrivilegeID = ID FROM TB_Privilege WHERE PrivilegeName = 'Administration'

DECLARE @SystemPrivilegeID UNIQUEIDENTIFIER = NEWID();
INSERT INTO TB_Privilege
VALUES(@SystemPrivilegeID, 'System Utilities', '/Administration/Utilities', 0, @ExistedAdministrationPrivilegeID, 'System Utilities', 1, GETDATE(), NULL)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Maintenance', '/Administration/Maintenance', 1, @SystemPrivilegeID, 'System Maintenance Utilities page', 1, GETDATE(), NULL)
