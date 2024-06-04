IF EXISTS ( SELECT * FROM   sysobjects WHERE  id = object_id(N'[dbo].[StpSaveErrorLogs]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[StpSaveErrorLogs]
END

GO

CREATE PROCEDURE [dbo].[StpSaveErrorLogs] 
	@Logid int,
	@ErrorDescription nvarchar(Max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
		BEGIN TRY

				INSERT INTO [dbo].[tnxErrorLog](LogId,
												StackMsg,
												CreatedBy,
												CreatedAt,
												LastModifiedBy,
												LastModifiedAt)
										VALUES(	@Logid,
												@ErrorDescription,
												'Admin',
												GETDATE(),
												'Admin',
												GETDATE());
 
			COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
			SELECT 
					ERROR_NUMBER() As ErrorNumber,
					ERROR_STATE() As ErrorState,
					ERROR_SEVERITY() As ErrorSeverity,
					ERROR_PROCEDURE() As ErrorProcedure,
					ERROR_LINE() As ErrorLine,
					ERROR_MESSAGE() ErrorMessage;

				IF @@TRANCOUNT > 0
				BEGIN
					ROLLBACK TRANSACTION;
				END
		END CATCH


END
GO