USE [KeyCommander]
GO
/****** Object:  User [keycdr]    Script Date: 11/14/2019 22:37:43 ******/
CREATE USER [keycdr] FOR LOGIN [keycdr] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [keycdr]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [keycdr]
GO
