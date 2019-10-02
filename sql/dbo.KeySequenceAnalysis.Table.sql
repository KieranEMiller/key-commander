USE [KeyCommander]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_KeySequenceAnalysis_KeySequence]') AND parent_object_id = OBJECT_ID(N'[dbo].[KeySequenceAnalysis]'))
ALTER TABLE [dbo].[KeySequenceAnalysis] DROP CONSTRAINT [FK_KeySequenceAnalysis_KeySequence]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_KeySequenceAnalysis_AnalysisType]') AND parent_object_id = OBJECT_ID(N'[dbo].[KeySequenceAnalysis]'))
ALTER TABLE [dbo].[KeySequenceAnalysis] DROP CONSTRAINT [FK_KeySequenceAnalysis_AnalysisType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_KeySequenceAnalysis_Created]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[KeySequenceAnalysis] DROP CONSTRAINT [DF_KeySequenceAnalysis_Created]
END
GO
/****** Object:  Table [dbo].[KeySequenceAnalysis]    Script Date: 10/2/2019 21:08:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KeySequenceAnalysis]') AND type in (N'U'))
DROP TABLE [dbo].[KeySequenceAnalysis]
GO
/****** Object:  Table [dbo].[KeySequenceAnalysis]    Script Date: 10/2/2019 21:08:52 ******/
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
ALTER TABLE [dbo].[KeySequenceAnalysis] ADD  CONSTRAINT [DF_KeySequenceAnalysis_Created]  DEFAULT (getdate()) FOR [Created]
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
