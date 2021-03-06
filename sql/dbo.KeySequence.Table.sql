USE [KeyCommander]
GO
ALTER TABLE [dbo].[KeySequence] DROP CONSTRAINT [FK_KeySequence_SourceType]
GO
ALTER TABLE [dbo].[KeySequence] DROP CONSTRAINT [FK_KeySequence_Session]
GO
ALTER TABLE [dbo].[KeySequence] DROP CONSTRAINT [DF_KeySequence_Created]
GO
/****** Object:  Table [dbo].[KeySequence]    Script Date: 12/10/2019 14:20:49 ******/
DROP TABLE [dbo].[KeySequence]
GO
/****** Object:  Table [dbo].[KeySequence]    Script Date: 12/10/2019 14:20:50 ******/
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
ALTER TABLE [dbo].[KeySequence] ADD  CONSTRAINT [DF_KeySequence_Created]  DEFAULT (getdate()) FOR [Created]
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
