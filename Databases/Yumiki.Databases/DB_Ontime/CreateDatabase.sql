USE [DB_Ontime]
GO
/****** Object:  Table [dbo].[TB_History]    Script Date: 5/9/2018 9:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_History](
	[ID] [uniqueidentifier] NOT NULL,
	[PhaseID] [uniqueidentifier] NULL,
	[TaskID] [uniqueidentifier] NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_History] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_Phase]    Script Date: 5/9/2018 9:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_Phase](
	[ID] [uniqueidentifier] NOT NULL,
	[PhaseName] [nvarchar](50) NOT NULL,
	[EstimatedStartDate] [date] NULL,
	[EstimatedEndDate] [date] NULL,
	[ActualStartDate] [date] NULL,
	[ActualEndDate] [date] NULL,
	[ReleaseVersion] [varchar](50) NULL,
	[Status] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Phase] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_PhaseAssignment]    Script Date: 5/9/2018 9:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_PhaseAssignment](
	[ID] [uniqueidentifier] NOT NULL,
	[PhaseID] [uniqueidentifier] NULL,
	[UserID] [uniqueidentifier] NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_PhaseAssignment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_Project]    Script Date: 5/9/2018 9:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_Project](
	[ID] [uniqueidentifier] NOT NULL,
	[ProjectName] [nvarchar](50) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Prefix] [varchar](4) NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Project] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_ProjectAssignment]    Script Date: 5/9/2018 9:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_ProjectAssignment](
	[ID] [uniqueidentifier] NOT NULL,
	[ProjectID] [uniqueidentifier] NULL,
	[UserID] [uniqueidentifier] NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_ProjectAssignment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_Task]    Script Date: 5/9/2018 9:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_Task](
	[ID] [uniqueidentifier] NOT NULL,
	[TaskName] [nvarchar](50) NOT NULL,
	[ProjectID] [uniqueidentifier] NOT NULL,
	[PhaseID] [uniqueidentifier] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Status] [int] NOT NULL,
	[TaskDescriptions] [ntext] NOT NULL,
	[AssignedUserID] [uniqueidentifier] NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Task] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_User]    Script Date: 5/9/2018 9:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_User](
	[ID] [uniqueidentifier] NOT NULL,
	[UserLoginName] [varchar](20) NOT NULL,
	[FirstName] [nvarchar](15) NULL,
	[LastName] [nvarchar](15) NULL,
	[TimeZone] [varchar](100) NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_History]  WITH CHECK ADD  CONSTRAINT [FK_TB_History_TB_Phase] FOREIGN KEY([PhaseID])
REFERENCES [dbo].[TB_Phase] ([ID])
GO
ALTER TABLE [dbo].[TB_History] CHECK CONSTRAINT [FK_TB_History_TB_Phase]
GO
ALTER TABLE [dbo].[TB_History]  WITH CHECK ADD  CONSTRAINT [FK_TB_History_TB_Task] FOREIGN KEY([TaskID])
REFERENCES [dbo].[TB_Task] ([ID])
GO
ALTER TABLE [dbo].[TB_History] CHECK CONSTRAINT [FK_TB_History_TB_Task]
GO
ALTER TABLE [dbo].[TB_Phase]  WITH CHECK ADD  CONSTRAINT [FK_TB_Phase_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])
GO
ALTER TABLE [dbo].[TB_Phase] CHECK CONSTRAINT [FK_TB_Phase_TB_User]
GO
ALTER TABLE [dbo].[TB_PhaseAssignment]  WITH CHECK ADD  CONSTRAINT [FK_TB_PhaseAssignment_TB_Phase] FOREIGN KEY([PhaseID])
REFERENCES [dbo].[TB_Phase] ([ID])
GO
ALTER TABLE [dbo].[TB_PhaseAssignment] CHECK CONSTRAINT [FK_TB_PhaseAssignment_TB_Phase]
GO
ALTER TABLE [dbo].[TB_PhaseAssignment]  WITH CHECK ADD  CONSTRAINT [FK_TB_PhaseAssignment_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])
GO
ALTER TABLE [dbo].[TB_PhaseAssignment] CHECK CONSTRAINT [FK_TB_PhaseAssignment_TB_User]
GO
ALTER TABLE [dbo].[TB_Project]  WITH CHECK ADD  CONSTRAINT [FK_TB_Project_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])
GO
ALTER TABLE [dbo].[TB_Project] CHECK CONSTRAINT [FK_TB_Project_TB_User]
GO
ALTER TABLE [dbo].[TB_ProjectAssignment]  WITH CHECK ADD  CONSTRAINT [FK_TB_ProjectAssignment_TB_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[TB_Project] ([ID])
GO
ALTER TABLE [dbo].[TB_ProjectAssignment] CHECK CONSTRAINT [FK_TB_ProjectAssignment_TB_Project]
GO
ALTER TABLE [dbo].[TB_ProjectAssignment]  WITH CHECK ADD  CONSTRAINT [FK_TB_ProjectAssignment_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])
GO
ALTER TABLE [dbo].[TB_ProjectAssignment] CHECK CONSTRAINT [FK_TB_ProjectAssignment_TB_User]
GO
ALTER TABLE [dbo].[TB_Task]  WITH CHECK ADD  CONSTRAINT [FK_TB_Task_TB_Phase] FOREIGN KEY([PhaseID])
REFERENCES [dbo].[TB_Phase] ([ID])
GO
ALTER TABLE [dbo].[TB_Task] CHECK CONSTRAINT [FK_TB_Task_TB_Phase]
GO
ALTER TABLE [dbo].[TB_Task]  WITH CHECK ADD  CONSTRAINT [FK_TB_Task_TB_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[TB_Project] ([ID])
GO
ALTER TABLE [dbo].[TB_Task] CHECK CONSTRAINT [FK_TB_Task_TB_Project]
GO
ALTER TABLE [dbo].[TB_Task]  WITH CHECK ADD  CONSTRAINT [FK_TB_Task_TB_User] FOREIGN KEY([AssignedUserID])
REFERENCES [dbo].[TB_User] ([ID])
GO
ALTER TABLE [dbo].[TB_Task] CHECK CONSTRAINT [FK_TB_Task_TB_User]
GO
ALTER TABLE [dbo].[TB_Task]  WITH CHECK ADD  CONSTRAINT [FK_TB_Task_TB_User1] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])
GO
ALTER TABLE [dbo].[TB_Task] CHECK CONSTRAINT [FK_TB_Task_TB_User1]
GO

/****** Object:  Table [dbo].[TB_Comment]    Script Date: 5/18/2018 9:47:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_Comment](
	[ID] [uniqueidentifier] NOT NULL,
	[Comment] [ntext] NOT NULL,
	[TaskID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Descriptions] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TB_Comment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[TB_Comment]  WITH CHECK ADD  CONSTRAINT [FK_TB_Comment_TB_Task] FOREIGN KEY([TaskID])
REFERENCES [dbo].[TB_Task] ([ID])
GO

ALTER TABLE [dbo].[TB_Comment] CHECK CONSTRAINT [FK_TB_Comment_TB_Task]
GO

ALTER TABLE [dbo].[TB_Comment]  WITH CHECK ADD  CONSTRAINT [FK_TB_Comment_TB_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[TB_User] ([ID])
GO

ALTER TABLE [dbo].[TB_Comment] CHECK CONSTRAINT [FK_TB_Comment_TB_User]
GO
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------

ALTER TABLE [dbo].[TB_Task]
ADD [Priority] INT  NULL
GO
UPDATE [dbo].[TB_Task]
SET [Priority] = 1
GO
ALTER TABLE [dbo].[TB_Task]
ALTER COLUMN [Priority] INT  NOT NULL
GO

ALTER TABLE [dbo].[TB_Task]
ADD [TaskNumber] [int] IDENTITY(1,1) NOT NULL
GO
