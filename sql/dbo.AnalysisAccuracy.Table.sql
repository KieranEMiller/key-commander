USE [KeyCommander]
GO
ALTER TABLE [dbo].[AnalysisAccuracy] DROP CONSTRAINT [FK_AnalysisAccuracy_KeySequenceAnalysis]
GO
ALTER TABLE [dbo].[AnalysisAccuracy] DROP CONSTRAINT [FK_AnalysisAccuracy_AnalysisType]
GO
/****** Object:  Table [dbo].[AnalysisAccuracy]    Script Date: 12/10/2019 14:20:49 ******/
DROP TABLE [dbo].[AnalysisAccuracy]
GO
/****** Object:  Table [dbo].[AnalysisAccuracy]    Script Date: 12/10/2019 14:20:50 ******/
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
