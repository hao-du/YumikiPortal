INSERT INTO [DB_Master].[dbo].[TB_User]
		([ID], [UserLoginName], [CurrentPassword], [FirstName], [LastName], [Descriptions], [IsActive], [CreateDate], [LastUpdateDate])
SELECT [ID], [UserLoginName], [CurrentPassword], [FirstName], [LastName], [Descriptions], [IsActive], [CreateDate], [LastUpdateDate]
FROM [DB_Administration].[dbo].[TB_User]
