USE [KeyCommander]
GO
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [FK_Session_KCUser]
GO
ALTER TABLE [dbo].[KeySequenceAnalysis] DROP CONSTRAINT [FK_KeySequenceAnalysis_KeySequence]
GO
ALTER TABLE [dbo].[KeySequenceAnalysis] DROP CONSTRAINT [FK_KeySequenceAnalysis_AnalysisType]
GO
ALTER TABLE [dbo].[KeySequence] DROP CONSTRAINT [FK_KeySequence_SourceType]
GO
ALTER TABLE [dbo].[KeySequence] DROP CONSTRAINT [FK_KeySequence_Session]
GO
ALTER TABLE [dbo].[KCUserLogin] DROP CONSTRAINT [FK_KCUserLogin_KCUser]
GO
ALTER TABLE [dbo].[AnalysisSpeed] DROP CONSTRAINT [FK_AnalysisSpeed_KeySequenceAnalysis]
GO
ALTER TABLE [dbo].[AnalysisSpeed] DROP CONSTRAINT [FK_AnalysisSpeed_AnalysisType]
GO
ALTER TABLE [dbo].[AnalysisAccuracy] DROP CONSTRAINT [FK_AnalysisAccuracy_KeySequenceAnalysis]
GO
ALTER TABLE [dbo].[AnalysisAccuracy] DROP CONSTRAINT [FK_AnalysisAccuracy_AnalysisType]
GO
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [DF_Session_CreateDate]
GO
ALTER TABLE [dbo].[KeySequenceAnalysis] DROP CONSTRAINT [DF_KeySequenceAnalysis_Created]
GO
ALTER TABLE [dbo].[KeySequence] DROP CONSTRAINT [DF_KeySequence_Created]
GO
ALTER TABLE [dbo].[KCUserLogin] DROP CONSTRAINT [DF_KCUserLogin_Created]
GO
ALTER TABLE [dbo].[KCUser] DROP CONSTRAINT [DF_KCUser_Created]
GO
/****** Object:  Table [dbo].[SourceType]    Script Date: 12/7/2019 12:31:22 ******/
DROP TABLE [dbo].[SourceType]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 12/7/2019 12:31:22 ******/
DROP TABLE [dbo].[Session]
GO
/****** Object:  Table [dbo].[KeySequenceAnalysis]    Script Date: 12/7/2019 12:31:22 ******/
DROP TABLE [dbo].[KeySequenceAnalysis]
GO
/****** Object:  Table [dbo].[KeySequence]    Script Date: 12/7/2019 12:31:22 ******/
DROP TABLE [dbo].[KeySequence]
GO
/****** Object:  Table [dbo].[KCUserLogin]    Script Date: 12/7/2019 12:31:22 ******/
DROP TABLE [dbo].[KCUserLogin]
GO
/****** Object:  Table [dbo].[KCUser]    Script Date: 12/7/2019 12:31:22 ******/
DROP TABLE [dbo].[KCUser]
GO
/****** Object:  Table [dbo].[AnalysisType]    Script Date: 12/7/2019 12:31:22 ******/
DROP TABLE [dbo].[AnalysisType]
GO
/****** Object:  Table [dbo].[AnalysisSpeed]    Script Date: 12/7/2019 12:31:22 ******/
DROP TABLE [dbo].[AnalysisSpeed]
GO
/****** Object:  Table [dbo].[AnalysisAccuracy]    Script Date: 12/7/2019 12:31:22 ******/
DROP TABLE [dbo].[AnalysisAccuracy]
GO
/****** Object:  User [keycdr]    Script Date: 12/7/2019 12:31:22 ******/
DROP USER [keycdr]
GO
/****** Object:  User [keycdr]    Script Date: 12/7/2019 12:31:22 ******/
CREATE USER [keycdr] FOR LOGIN [keycdr] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [keycdr]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [keycdr]
GO
/****** Object:  Table [dbo].[AnalysisAccuracy]    Script Date: 12/7/2019 12:31:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalysisAccuracy](
	[KeySequenceAnalysisId] [uniqueidentifier] NOT NULL,
	[AnalysisTypeId] [int] NOT NULL,
	[Accuracy] [decimal](5, 4) NOT NULL,
	[NumWords] [int] NOT NULL,
	[NumChars] [int] NOT NULL,
	[NumCorrectChars] [int] NOT NULL,
	[NumIncorrectChars] [int] NOT NULL,
	[NumExtraChars] [int] NOT NULL,
	[NumShortChars] [int] NOT NULL,
 CONSTRAINT [PK_AnalysisAccuracy] PRIMARY KEY CLUSTERED 
(
	[KeySequenceAnalysisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnalysisSpeed]    Script Date: 12/7/2019 12:31:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalysisSpeed](
	[KeySequenceAnalysisId] [uniqueidentifier] NOT NULL,
	[AnalysisTypeId] [int] NOT NULL,
	[TotalTimeInMilliSec] [decimal](12, 2) NOT NULL,
	[WordPerMin] [decimal](6, 2) NOT NULL,
	[CharsPerSec] [decimal](6, 2) NOT NULL,
 CONSTRAINT [PK_AnalysisSpeed] PRIMARY KEY CLUSTERED 
(
	[KeySequenceAnalysisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnalysisType]    Script Date: 12/7/2019 12:31:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalysisType](
	[AnalysisTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AnalysisType] PRIMARY KEY CLUSTERED 
(
	[AnalysisTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KCUser]    Script Date: 12/7/2019 12:31:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KCUser](
	[UserId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LoginName] [nvarchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[PasswordHash] [nvarchar](255) NULL,
	[PasswordSalt] [nvarchar](255) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [KCUser_LoginName_Unique] UNIQUE NONCLUSTERED 
(
	[LoginName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KCUserLogin]    Script Date: 12/7/2019 12:31:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KCUserLogin](
	[KCUserLoginId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[UserAgent] [nvarchar](512) NULL,
	[IpAddress] [nvarchar](15) NULL,
 CONSTRAINT [PK_KCUserLogin] PRIMARY KEY CLUSTERED 
(
	[KCUserLoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KeySequence]    Script Date: 12/7/2019 12:31:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeySequence](
	[KeySequenceId] [uniqueidentifier] NOT NULL,
	[SessionId] [uniqueidentifier] NOT NULL,
	[SourceTypeId] [int] NOT NULL,
	[SourceKey] [nvarchar](255) NOT NULL,
	[TextShown] [nvarchar](2048) NOT NULL,
	[TextEntered] [nvarchar](2048) NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_KeySequence] PRIMARY KEY CLUSTERED 
(
	[KeySequenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KeySequenceAnalysis]    Script Date: 12/7/2019 12:31:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeySequenceAnalysis](
	[KeySequenceAnalysisId] [uniqueidentifier] NOT NULL,
	[KeySequenceId] [uniqueidentifier] NOT NULL,
	[AnalysisTypeId] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_KeySequenceAnalysis] PRIMARY KEY CLUSTERED 
(
	[KeySequenceAnalysisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 12/7/2019 12:31:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[SessionId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SourceType]    Script Date: 12/7/2019 12:31:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceType](
	[SourceTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SourceType] PRIMARY KEY CLUSTERED 
(
	[SourceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[KCUser] ADD  CONSTRAINT [DF_KCUser_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[KCUserLogin] ADD  CONSTRAINT [DF_KCUserLogin_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[KeySequence] ADD  CONSTRAINT [DF_KeySequence_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[KeySequenceAnalysis] ADD  CONSTRAINT [DF_KeySequenceAnalysis_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_CreateDate]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[AnalysisAccuracy]  WITH CHECK ADD  CONSTRAINT [FK_AnalysisAccuracy_AnalysisType] FOREIGN KEY([AnalysisTypeId])
REFERENCES [dbo].[AnalysisType] ([AnalysisTypeId])
GO
ALTER TABLE [dbo].[AnalysisAccuracy] CHECK CONSTRAINT [FK_AnalysisAccuracy_AnalysisType]
GO
ALTER TABLE [dbo].[AnalysisAccuracy]  WITH CHECK ADD  CONSTRAINT [FK_AnalysisAccuracy_KeySequenceAnalysis] FOREIGN KEY([KeySequenceAnalysisId])
REFERENCES [dbo].[KeySequenceAnalysis] ([KeySequenceAnalysisId])
GO
ALTER TABLE [dbo].[AnalysisAccuracy] CHECK CONSTRAINT [FK_AnalysisAccuracy_KeySequenceAnalysis]
GO
ALTER TABLE [dbo].[AnalysisSpeed]  WITH CHECK ADD  CONSTRAINT [FK_AnalysisSpeed_AnalysisType] FOREIGN KEY([AnalysisTypeId])
REFERENCES [dbo].[AnalysisType] ([AnalysisTypeId])
GO
ALTER TABLE [dbo].[AnalysisSpeed] CHECK CONSTRAINT [FK_AnalysisSpeed_AnalysisType]
GO
ALTER TABLE [dbo].[AnalysisSpeed]  WITH CHECK ADD  CONSTRAINT [FK_AnalysisSpeed_KeySequenceAnalysis] FOREIGN KEY([KeySequenceAnalysisId])
REFERENCES [dbo].[KeySequenceAnalysis] ([KeySequenceAnalysisId])
GO
ALTER TABLE [dbo].[AnalysisSpeed] CHECK CONSTRAINT [FK_AnalysisSpeed_KeySequenceAnalysis]
GO
ALTER TABLE [dbo].[KCUserLogin]  WITH CHECK ADD  CONSTRAINT [FK_KCUserLogin_KCUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[KCUser] ([UserId])
GO
ALTER TABLE [dbo].[KCUserLogin] CHECK CONSTRAINT [FK_KCUserLogin_KCUser]
GO
ALTER TABLE [dbo].[KeySequence]  WITH CHECK ADD  CONSTRAINT [FK_KeySequence_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([SessionId])
GO
ALTER TABLE [dbo].[KeySequence] CHECK CONSTRAINT [FK_KeySequence_Session]
GO
ALTER TABLE [dbo].[KeySequence]  WITH CHECK ADD  CONSTRAINT [FK_KeySequence_SourceType] FOREIGN KEY([SourceTypeId])
REFERENCES [dbo].[SourceType] ([SourceTypeId])
GO
ALTER TABLE [dbo].[KeySequence] CHECK CONSTRAINT [FK_KeySequence_SourceType]
GO
ALTER TABLE [dbo].[KeySequenceAnalysis]  WITH CHECK ADD  CONSTRAINT [FK_KeySequenceAnalysis_AnalysisType] FOREIGN KEY([AnalysisTypeId])
REFERENCES [dbo].[AnalysisType] ([AnalysisTypeId])
GO
ALTER TABLE [dbo].[KeySequenceAnalysis] CHECK CONSTRAINT [FK_KeySequenceAnalysis_AnalysisType]
GO
ALTER TABLE [dbo].[KeySequenceAnalysis]  WITH CHECK ADD  CONSTRAINT [FK_KeySequenceAnalysis_KeySequence] FOREIGN KEY([KeySequenceId])
REFERENCES [dbo].[KeySequence] ([KeySequenceId])
GO
ALTER TABLE [dbo].[KeySequenceAnalysis] CHECK CONSTRAINT [FK_KeySequenceAnalysis_KeySequence]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_KCUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[KCUser] ([UserId])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_KCUser]
GO
