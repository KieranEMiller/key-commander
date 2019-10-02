USE [KeyCommander]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_KeySequence_SourceType]') AND parent_object_id = OBJECT_ID(N'[dbo].[KeySequence]'))
ALTER TABLE [dbo].[KeySequence] DROP CONSTRAINT [FK_KeySequence_SourceType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_KeySequence_Session]') AND parent_object_id = OBJECT_ID(N'[dbo].[KeySequence]'))
ALTER TABLE [dbo].[KeySequence] DROP CONSTRAINT [FK_KeySequence_Session]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_KeySequence_Created]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[KeySequence] DROP CONSTRAINT [DF_KeySequence_Created]
END
GO
/****** Object:  Table [dbo].[KeySequence]    Script Date: 10/2/2019 21:08:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KeySequence]') AND type in (N'U'))
DROP TABLE [dbo].[KeySequence]
GO
/****** Object:  Table [dbo].[KeySequence]    Script Date: 10/2/2019 21:08:52 ******/
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
