IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES 
					WHERE TABLE_NAME = N'Pets')
	AND EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS 
					WHERE COLUMN_NAME = N'Gender')
	AND EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS 
					WHERE COLUMN_NAME = N'GenderType')
BEGIN
	BEGIN TRANSACTION 
	UPDATE Pets
		SET GenderType = 
			CASE 
				WHEN Gender = 'M' THEN 1 
			ELSE 0 
		END;
	COMMIT TRANSACTION
END