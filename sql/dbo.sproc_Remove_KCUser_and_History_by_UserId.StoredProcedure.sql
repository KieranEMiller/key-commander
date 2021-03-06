USE [KeyCommander]
GO
/****** Object:  StoredProcedure [dbo].[sproc_Remove_KCUser_and_History_by_UserId]    Script Date: 12/10/2019 14:20:49 ******/
DROP PROCEDURE [dbo].[sproc_Remove_KCUser_and_History_by_UserId]
GO
/****** Object:  StoredProcedure [dbo].[sproc_Remove_KCUser_and_History_by_UserId]    Script Date: 12/10/2019 14:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sproc_Remove_KCUser_and_History_by_UserId]
	@UserID UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @UserKeySequences TABLE (
		KeySequenceId UNIQUEIDENTIFIER
	)
	INSERT INTO @UserKeySequences
	SELECT KeySEquenceId 
	FROM KeySequence
	WHERE
		SessionId in (
			SELECT SessionId 
			FROM [Session]
			WHERE
				[UserId] = @UserID
		)
		
	DELETE FROM AnalysisAccuracy
	WHERE
		KeySequenceAnalysisId IN (
			SELECT KeySequenceAnalysisId 
			FROM KeySequenceAnalysis
			WHERE
				KeySequenceId in (SELECT KeySequenceId FROM @UserKeySequences)
		)

	DELETE FROM AnalysisSpeed
	WHERE
		KeySequenceAnalysisId IN (
			SELECT KeySequenceAnalysisId 
			FROM KeySequenceAnalysis
			WHERE
				KeySequenceId in (SELECT KeySequenceId FROM @UserKeySequences)
		)

	DELETE FROM KeySequenceAnalysis
		WHERE
			KeySequenceId in (SELECT KeySequenceId FROM @UserKeySequences)
		
	DELETE FROM KeySequence
	WHERE 
		KeySequenceId in (SELECT KeySequenceId FROM @UserKeySequences)

	DELETE FROM [Session]
	WHERE
		UserId = @UserId

	DELETE FROM KCUserLogin
	WHERE
		UserId = @UserId

	DELETE FROM KCUser WHERE UserId = @UserID

END
GO
