CREATE PROCEDURE [dbo].[spUser_Delete]
	@Id int
AS
BEGIN
	DELETE
	FROM dbo.[User]
	WHERE Id = @Id;
END
