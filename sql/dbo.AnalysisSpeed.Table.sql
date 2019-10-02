USE [KeyCommander]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AnalysisSpeed_KeySequenceAnalysis]') AND parent_object_id = OBJECT_ID(N'[dbo].[AnalysisSpeed]'))
ALTER TABLE [dbo].[AnalysisSpeed] DROP CONSTRAINT [FK_AnalysisSpeed_KeySequenceAnalysis]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AnalysisSpeed_AnalysisType]') AND parent_object_id = OBJECT_ID(N'[dbo].[AnalysisSpeed]'))
ALTER TABLE [dbo].[AnalysisSpeed] DROP CONSTRAINT [FK_AnalysisSpeed_AnalysisType]
GO
/****** Object:  Table [dbo].[AnalysisSpeed]    Script Date: 10/2/2019 21:08:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AnalysisSpeed]') AND type in (N'U'))
DROP TABLE [dbo].[AnalysisSpeed]
GO
/****** Object:  Table [dbo].[AnalysisSpeed]    Script Date: 10/2/2019 21:08:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalysisSpeed](
	[AnalysisSpeedId] [uniqueidentifier] NOT NULL,
	[AnalysisTypeId] [int] NOT NULL,
	[KeySequenceAnalysisId] [uniqueidentifier] NOT NULL,
	[TotalTimeInMilliSec] [decimal](12, 2) NOT NULL,
	[WordPerMin] [decimal](6, 2) NOT NULL,
	[CharsPerSec] [decimal](6, 2) NOT NULL,
 CONSTRAINT [PK_AnalysisSpeed] PRIMARY KEY CLUSTERED 
(
	[AnalysisSpeedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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
