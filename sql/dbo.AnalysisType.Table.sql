USE [KeyCommander]
GO
/****** Object:  Table [dbo].[AnalysisType]    Script Date: 12/10/2019 14:20:49 ******/
DROP TABLE [dbo].[AnalysisType]
GO
/****** Object:  Table [dbo].[AnalysisType]    Script Date: 12/10/2019 14:20:50 ******/
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
