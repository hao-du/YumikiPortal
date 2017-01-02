TRUNCATE TABLE [DB_Master].[dbo].[TB_User]

INSERT INTO [DB_Master].[dbo].[TB_User]
		([ID], [UserLoginName], [CurrentPassword], [FirstName], [LastName], [TimeZone], [Descriptions], [IsActive], [CreateDate], [LastUpdateDate])
SELECT [ID], [UserLoginName], [CurrentPassword], [FirstName], [LastName], [TimeZone], [Descriptions], [IsActive], [CreateDate], [LastUpdateDate]
FROM [DB_Administration].[dbo].[TB_User]
