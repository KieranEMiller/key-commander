USE [KeyCommander]
GO
ALTER TABLE [dbo].[KeySequenceAnalysis] DROP CONSTRAINT [FK_KeySequenceAnalysis_KeySequence]
GO
ALTER TABLE [dbo].[KeySequenceAnalysis] DROP CONSTRAINT [FK_KeySequenceAnalysis_AnalysisType]
GO
ALTER TABLE [dbo].[KeySequenceAnalysis] DROP CONSTRAINT [DF_KeySequenceAnalysis_Created]
GO
/****** Object:  Table [dbo].[KeySequenceAnalysis]    Script Date: 12/10/2019 14:20:49 ******/
DROP TABLE [dbo].[KeySequenceAnalysis]
GO
/****** Object:  Table [dbo].[KeySequenceAnalysis]    Script Date: 12/10/2019 14:20:50 ******/
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
