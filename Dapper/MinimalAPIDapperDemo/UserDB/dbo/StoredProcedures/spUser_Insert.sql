CREATE PROCEDURE [dbo].[spUser_Insert]
	@FirstName nvarchar(50),
	@LastName nvarchar(50)
AS
BEGIN
	INSERT INTO dbo.[User] (FirstName, LastName)
	VALUES (@FirstName, @LastName);
END
