DELETE FROM [DB_MoneyTrace].[dbo].[TB_User]

INSERT INTO [DB_MoneyTrace].[dbo].[TB_User]
		([ID], [UserLoginName], [FirstName], [LastName], [Descriptions], [IsActive], [CreateDate], [LastUpdateDate])
SELECT [ID], [UserLoginName], [FirstName], [LastName], [Descriptions], [IsActive], [CreateDate], [LastUpdateDate]
FROM [DB_Administration].[dbo].[TB_User]

-------------------------------------------------------------------------------------------------------------------------------------

--Run these scripts in DB_Administrator when deploy
--DECLARE @MonneyTraceID UNIQUEIDENTIFIER = NEWID()

--INSERT INTO TB_Privilege
--VALUES(@MonneyTraceID, 'Money Trace', '/MoneyTrace/', 0, NULL, 'Root of Money Trace Module', 1, GETDATE(), NULL)

--INSERT INTO TB_Privilege
--VALUES(NEWID(), 'Currency', '/MoneyTrace/Currency', 1, @MonneyTraceID, 'Currency Page', 1, GETDATE(), NULL)

--INSERT INTO TB_Privilege
--VALUES(NEWID(), 'Trace', '/MoneyTrace/Trace', 1, @MonneyTraceID, 'Trace Page', 1, GETDATE(), NULL)

-------------------------------------------------------------------------------------------------------------------------------------
