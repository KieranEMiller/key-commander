USE [KeyCommander]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_KCUser_Created]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[KCUser] DROP CONSTRAINT [DF_KCUser_Created]
END
GO
/****** Object:  Table [dbo].[KCUser]    Script Date: 10/2/2019 21:08:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KCUser]') AND type in (N'U'))
DROP TABLE [dbo].[KCUser]
GO
/****** Object:  Table [dbo].[KCUser]    Script Date: 10/2/2019 21:08:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KCUser](
	[UserId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LoginName] [nvarchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
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
ALTER TABLE [dbo].[KCUser] ADD  CONSTRAINT [DF_KCUser_Created]  DEFAULT (getdate()) FOR [Created]
GO
