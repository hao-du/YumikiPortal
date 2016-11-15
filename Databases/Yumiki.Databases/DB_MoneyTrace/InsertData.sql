DELETE FROM [DB_MoneyTrace].[dbo].[TB_User]

INSERT INTO [DB_MoneyTrace].[dbo].[TB_User]
		([ID], [UserLoginName], [FirstName], [LastName], [Descriptions], [IsActive], [CreateDate], [LastUpdateDate])
SELECT [ID], [UserLoginName], [FirstName], [LastName], [Descriptions], [IsActive], [CreateDate], [LastUpdateDate]
FROM [DB_Administration].[dbo].[TB_User]

-------------------------------------------------------------------------------------------------------------------------------------
