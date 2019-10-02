
USE [KeyCommander]
GO

INSERT INTO [dbo].[AnalysisType]
           ([AnalysisTypeId]
           ,[Name])
     VALUES
           (/*<AnalysisTypeId, int,>*/	1
           ,/*<Name, nvarchar(50),>*/	'Accuracy'
		   )
GO

INSERT INTO [dbo].[AnalysisType]
           ([AnalysisTypeId]
           ,[Name])
     VALUES
           (/*<AnalysisTypeId, int,>*/	2
           ,/*<Name, nvarchar(50),>*/	'Speed'
		   )
GO

USE [KeyCommander]
GO

INSERT INTO [dbo].[SourceType]
           ([SourceTypeId]
           ,[Name])
     VALUES
           (/*<SourceTypeId, int,>*/	1
           ,/*<Name, nvarchar(50),>*/	'Wikipedia'
		   )
GO

USE [KeyCommander]
GO

INSERT INTO [dbo].[KCUser]
           ([UserId]
           ,[Name]
           ,[LoginName])
     VALUES
           (/*<UserId, uniqueidentifier,>*/	NEWID()
           ,/*<Name, nvarchar(50),>*/		'kem'
           ,/*<LoginName, nvarchar(50),>*/	'kem'
		   )
GO