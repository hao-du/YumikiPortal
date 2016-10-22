CREATE TABLE [dbo].[TB_User](
	[ID] [uniqueidentifier] NOT NULL,
	[UserLoginName] [varchar](20) NOT NULL,
	[CurrentPassword] [varchar](50) NOT NULL,
	[FirstName] [nvarchar](15) NULL,
	[LastName] [nvarchar](15) NULL,
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

CREATE TABLE [dbo].[TB_Group](
	[ID] [uniqueidentifier] NOT NULL,
	[GroupName] [nvarchar](20) NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_Privilege](
	[ID] [uniqueidentifier] NOT NULL,
	[PrivilegeName] [nvarchar](20) NOT NULL,
	[PagePath] [varchar](256) NOT NULL,
	[IsDisplayable] [bit] NOT NULL,
	[ParentPrivilegeID] [uniqueidentifier] NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Privilege] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_Privilege]  WITH CHECK ADD  CONSTRAINT [FK_TB_Privilege_TB_Privilege1] FOREIGN KEY([ParentPrivilegeID])
REFERENCES [dbo].[TB_Privilege] ([ID])

ALTER TABLE [dbo].[TB_Privilege] CHECK CONSTRAINT [FK_TB_Privilege_TB_Privilege1]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_GroupPrivilege](
	[GroupID] [uniqueidentifier] NOT NULL,
	[PrivilegeID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TB_GroupPrivilege] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC,
	[PrivilegeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_GroupPrivilege]  WITH CHECK ADD  CONSTRAINT [FK_TB_GroupPrivilege_TB_Group] FOREIGN KEY([GroupID])
REFERENCES [dbo].[TB_Group] ([ID])

ALTER TABLE [dbo].[TB_GroupPrivilege] CHECK CONSTRAINT [FK_TB_GroupPrivilege_TB_Group]

ALTER TABLE [dbo].[TB_GroupPrivilege]  WITH CHECK ADD  CONSTRAINT [FK_TB_GroupPrivilege_TB_Privilege] FOREIGN KEY([PrivilegeID])
REFERENCES [dbo].[TB_Privilege] ([ID])

ALTER TABLE [dbo].[TB_GroupPrivilege] CHECK CONSTRAINT [FK_TB_GroupPrivilege_TB_Privilege]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_UserAddress](
	[ID] [uniqueidentifier] NOT NULL,
	[UserAddress] [nvarchar](256) NULL,
	[AddressType] [int] NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_UserAddress] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_UserAddress]  WITH CHECK ADD  CONSTRAINT [FK_TB_UserAddress_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_UserAddress] CHECK CONSTRAINT [FK_TB_UserAddress_TB_User]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_UserGroup](
	[UserID] [uniqueidentifier] NOT NULL,
	[GroupID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TB_UserGroup] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_TB_UserGroup_TB_Group] FOREIGN KEY([GroupID])
REFERENCES [dbo].[TB_Group] ([ID])

ALTER TABLE [dbo].[TB_UserGroup] CHECK CONSTRAINT [FK_TB_UserGroup_TB_Group]

ALTER TABLE [dbo].[TB_UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_TB_UserGroup_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_UserGroup] CHECK CONSTRAINT [FK_TB_UserGroup_TB_User]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_PasswordHistory](
	[ID] [uniqueidentifier] NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_PasswordHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_PasswordHistory]  WITH CHECK ADD  CONSTRAINT [FK_TB_PasswordHistory_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_PasswordHistory] CHECK CONSTRAINT [FK_TB_PasswordHistory_TB_User]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_ContactType](
	[ID] [uniqueidentifier] NOT NULL,
	[ContactTypeName] [nvarchar](20) NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_ContactType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

-------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[TB_UserAddress](
	[ID] [uniqueidentifier] NOT NULL,
	[UserAddress] [nvarchar](256) NOT NULL,
	[UserContactTypeID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_UserAddress] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[TB_UserAddress]  WITH CHECK ADD  CONSTRAINT [FK_TB_UserAddress_TB_ContactType] FOREIGN KEY([UserContactTypeID])
REFERENCES [dbo].[TB_ContactType] ([ID])

ALTER TABLE [dbo].[TB_UserAddress] CHECK CONSTRAINT [FK_TB_UserAddress_TB_ContactType]

ALTER TABLE [dbo].[TB_UserAddress]  WITH CHECK ADD  CONSTRAINT [FK_TB_UserAddress_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_UserAddress] CHECK CONSTRAINT [FK_TB_UserAddress_TB_User]

-------------------------------------------------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------------------------------------