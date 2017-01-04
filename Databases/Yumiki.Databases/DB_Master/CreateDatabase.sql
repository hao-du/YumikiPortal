CREATE TABLE [dbo].[TB_User](
	[ID] [uniqueidentifier] NOT NULL,
	[UserLoginName] [varchar](20) NOT NULL,
	[CurrentPassword] [varchar](255) NOT NULL,
	[FirstName] [nvarchar](15) NULL,
	[LastName] [nvarchar](15) NULL,
	[TimeZone] [varchar](100) NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

-------------------------------------------------------------------------------------------------------------------------------------
--RUN THIS WHEN DEPLOYING
--CREATE VIEW VW_Privilege AS
--SELECT DISTINCT P.ID, P.PrivilegeName, P.PagePath, P.IsDisplayable, P.ParentPrivilegeID, U.ID AS UserID
--FROM [DB_Administration].[dbo].[TB_Privilege] P
--	INNER JOIN [DB_Administration].[dbo].[TB_GroupPrivilege] GP ON P.ID = GP.PrivilegeID
--	INNER JOIN [DB_Administration].[dbo].[TB_Group] G ON GP.GroupID = G.ID
--	INNER JOIN [DB_Administration].[dbo].[TB_UserGroup] UG ON UG.GroupID = G.ID
--	INNER JOIN [DB_Administration].[dbo].[TB_User] U ON U.ID = UG.UserID
--WHERE P.IsActive = 1 AND G.IsActive = 1 AND U.IsActive = 1

-------------------------------------------------------------------------------------------------------------------------------------