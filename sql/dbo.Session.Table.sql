USE [KeyCommander]
GO
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [FK_Session_KCUser]
GO
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [DF_Session_CreateDate]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 12/10/2019 14:20:49 ******/
DROP TABLE [dbo].[Session]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 12/10/2019 14:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[SessionId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_CreateDate]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_KCUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[KCUser] ([UserId])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_KCUser]
GO
