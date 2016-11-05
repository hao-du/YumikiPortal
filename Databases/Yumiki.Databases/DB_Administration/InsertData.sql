DECLARE @AdministrationID UNIQUEIDENTIFIER = NEWID();  
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