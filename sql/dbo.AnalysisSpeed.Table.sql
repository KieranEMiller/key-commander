USE [KeyCommander]
GO
/****** Object:  Table [dbo].[AnalysisSpeed]    Script Date: 11/14/2019 22:37:44 ******/
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
