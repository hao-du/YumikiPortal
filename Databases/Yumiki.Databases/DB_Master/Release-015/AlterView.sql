USE [DB_Master]
GO
ALTER VIEW VW_Privilege AS
SELECT DISTINCT P.ID, P.PrivilegeName, P.PagePath, P.IsDisplayable, P.ParentPrivilegeID, P.PrivilegeOrder, U.ID AS UserID
FROM [DB_Administration].[dbo].[TB_Privilege] P
	INNER JOIN [DB_Administration].[dbo].[TB_GroupPrivilege] GP ON P.ID = GP.PrivilegeID
	INNER JOIN [DB_Administration].[dbo].[TB_Group] G ON GP.GroupID = G.ID
	INNER JOIN [DB_Administration].[dbo].[TB_UserGroup] UG ON UG.GroupID = G.ID
	INNER JOIN [DB_Administration].[dbo].[TB_User] U ON U.ID = UG.UserID
WHERE P.IsActive = 1 AND G.IsActive = 1 AND U.IsActive = 1
GO