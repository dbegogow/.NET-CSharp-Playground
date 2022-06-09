CREATE PROCEDURE [dbo].[spUser_Get]
	@Id int
AS
BEGIN
	SELECT *
	FROM dbo.[User]
	WHERE Id = @Id;
END
