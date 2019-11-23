USE [KeyCommander]
GO

ALTER TABLE [dbo].[KCUserLogin] DROP CONSTRAINT [FK_KCUserLogin_KCUser]
GO

ALTER TABLE [dbo].[KCUserLogin] DROP CONSTRAINT [DF_KCUserLogin_Created]
GO

/****** Object:  Table [dbo].[KCUserLogin]    Script Date: 11/23/2019 14:28:53 ******/
DROP TABLE [dbo].[KCUserLogin]
GO

/****** Object:  Table [dbo].[KCUserLogin]    Script Date: 11/23/2019 14:28:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[KCUserLogin](
	[KCUserLoginId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[UserAgent] [nvarchar](512) NULL,
	[IpAddress] [nvarchar](15) NULL,
 CONSTRAINT [PK_KCUserLogin] PRIMARY KEY CLUSTERED 
(
	[KCUserLoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[KCUserLogin] ADD  CONSTRAINT [DF_KCUserLogin_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [dbo].[KCUserLogin]  WITH CHECK ADD  CONSTRAINT [FK_KCUserLogin_KCUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[KCUser] ([UserId])
GO

ALTER TABLE [dbo].[KCUserLogin] CHECK CONSTRAINT [FK_KCUserLogin_KCUser]
GO

