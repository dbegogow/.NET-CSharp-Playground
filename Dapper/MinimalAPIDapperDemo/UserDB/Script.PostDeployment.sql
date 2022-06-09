IF NOT EXISTS (SELECT 1 FROM dbo.[User])
BEGIN
	INSERT INTO dbo.[User] (FirstName, LastName)
	VALUES ('Dzhulio', 'Begogov'),
		   ('Ivana', 'Tagareva'),
		   ('Ivaylo', 'Kirilov'),
		   ('Nikolay', 'Kamenov')
END