USE [KeyCommander]
GO
/****** Object:  User [keycdr]    Script Date: 12/10/2019 14:20:49 ******/
DROP USER [keycdr]
GO
/****** Object:  User [keycdr]    Script Date: 12/10/2019 14:20:49 ******/
CREATE USER [keycdr] FOR LOGIN [keycdr] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [keycdr]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [keycdr]
GO
