IF EXISTS ( SELECT * FROM   sysobjects WHERE  id = object_id(N'[dbo].[StpSaveServiceLog]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[StpSaveServiceLog]
END

GO

CREATE PROCEDURE [dbo].[StpSaveServiceLog]
    @LogId int = NULL OUTPUT,
	@SourceId int,
	@ProductId int,
	@PlainReq nvarchar(MAX),
	@PlainRes nvarchar(MAX),
	@EncrytedReq nvarchar(max),
	@EncrytedRes nvarchar(max),
	@Header nvarchar(max),
    @createdBy varchar(50),
	@CreatedAt DateTime,
	@LastModifiedBy varchar(50),
	@LastModifiedAt DateTime
AS
BEGIN
	DECLARE @result int
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
		BEGIN TRY
			
			IF(@LogId = NULL)
			BEGIN

				INSERT INTO [dbo].[txnSeviceLog](SourceId,
												ProductId,
												PlainReq,
												EncrytedReq,
												Header,
												createdBy,
												CreatedAt,
												LastModifiedBy,
												LastModifiedAt) 
										Values(	@SourceId,
												@ProductId,
												@PlainReq,
												@EncrytedReq,
												@Header,
												'Admin',
												GETDATE(),
												'Admin', 
												GETDATE())

				SET @LogId = SCOPE_IDENTITY()
			END
			ELSE
			BEGIN
				UPDATE [dbo].[txnSeviceLog] SET 
						PlainRes=@PlainRes,
						EncrytedRes = @EncrytedRes,
						LastModifiedBy=@LastModifiedBy,
						LastModifiedAt=GETDATE() 
				WHERE	LogId=@LogId
			END

			
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