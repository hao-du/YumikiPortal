USE [DB_Administration]
GO

DECLARE @OnTimeID UNIQUEIDENTIFIER = NEWID();  
INSERT INTO TB_Privilege
VALUES(@OnTimeID, 'Ontime', '/Ontime/', 0, NULL, 'Root of Ontime Tracking Module', 1, GETDATE(), NULL)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Project', '/Ontime/Project/Index', 1, @OnTimeID, 'Project Setup Page', 1, GETDATE(), NULL)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Phase', '/Ontime/Phase/Index', 1, @OnTimeID, 'Phase Setup Page', 1, GETDATE(), NULL)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Task', '/Ontime/Task/Index', 1, @OnTimeID, 'Task Dashboard and Management Page', 1, GETDATE(), NULL)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'User Assignment', '/Ontime/UserAssignment/Index', 1, @OnTimeID, 'Phase and Project Assignments for Users Page', 1, GETDATE(), NULL)
