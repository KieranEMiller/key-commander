USE [KeyCommander]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AnalysisAccuracy_KeySequenceAnalysis]') AND parent_object_id = OBJECT_ID(N'[dbo].[AnalysisAccuracy]'))
ALTER TABLE [dbo].[AnalysisAccuracy] DROP CONSTRAINT [FK_AnalysisAccuracy_KeySequenceAnalysis]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AnalysisAccuracy_AnalysisType]') AND parent_object_id = OBJECT_ID(N'[dbo].[AnalysisAccuracy]'))
ALTER TABLE [dbo].[AnalysisAccuracy] DROP CONSTRAINT [FK_AnalysisAccuracy_AnalysisType]
GO
/****** Object:  Table [dbo].[AnalysisAccuracy]    Script Date: 10/2/2019 21:08:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AnalysisAccuracy]') AND type in (N'U'))
DROP TABLE [dbo].[AnalysisAccuracy]
GO
/****** Object:  Table [dbo].[AnalysisAccuracy]    Script Date: 10/2/2019 21:08:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalysisAccuracy](
	[AnalysisAccuracyId] [uniqueidentifier] NOT NULL,
	[AnalysisTypeId] [int] NOT NULL,
	[KeySequenceAnalysisId] [uniqueidentifier] NOT NULL,
	[Accuracy] [decimal](4, 4) NOT NULL,
	[NumWords] [int] NOT NULL,
	[NumChars] [int] NOT NULL,
	[NumCorrectChars] [int] NOT NULL,
	[NumIncorrectChars] [int] NOT NULL,
	[NumExtraChars] [int] NOT NULL,
	[NumShortChars] [int] NOT NULL,
 CONSTRAINT [PK_AnalysisAccuracy] PRIMARY KEY CLUSTERED 
(
	[AnalysisAccuracyId] ASC
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
