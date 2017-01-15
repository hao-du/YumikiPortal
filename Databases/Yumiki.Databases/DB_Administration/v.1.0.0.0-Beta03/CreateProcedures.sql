CREATE PROCEDURE PROC_SyncUser AS
BEGIN
	------SYNC MASTER
	--UPDATE EXISTING RECORDS
	UPDATE M
	SET 
		M.UserLoginName = A.UserLoginName,
		M.TimeZone = A.TimeZone,
		M.LastUpdateDate = A.LastUpdateDate,
		M.LastName = A.LastName,
		M.IsActive = A.IsActive,
		M.FirstName = A.FirstName,
		M.Descriptions = A.Descriptions,
		M.CurrentPassword = A.CurrentPassword
	FROM DB_Master.dbo.TB_User M
		INNER JOIN TB_User A ON M.ID = A.ID
	WHERE (A.LastUpdateDate IS NOT NULL AND M.LastUpdateDate IS NULL)
		OR A.LastUpdateDate <> M.LastUpdateDate

	--INSERT NEW RECORDS
	INSERT INTO DB_Master.dbo.TB_User
		(ID, UserLoginName, CurrentPassword, FirstName, LastName, TimeZone, Descriptions, IsActive, CreateDate, LastUpdateDate)
	SELECT A.ID, A.UserLoginName, A.CurrentPassword, A.FirstName, A.LastName, A.TimeZone, A.Descriptions, A.IsActive, A.CreateDate, A.LastUpdateDate
	FROM TB_User A
		LEFT JOIN DB_Master.dbo.TB_User M ON M.ID = A.ID
	WHERE M.ID IS NULL

	------SYNC MONEY TRACE
	--UPDATE EXISTING RECORDS
	UPDATE M
	SET 
		M.UserLoginName = A.UserLoginName,
		M.TimeZone = A.TimeZone,
		M.LastUpdateDate = A.LastUpdateDate,
		M.LastName = A.LastName,
		M.IsActive = A.IsActive,
		M.FirstName = A.FirstName,
		M.Descriptions = A.Descriptions
	FROM DB_MoneyTrace.dbo.TB_User M
		INNER JOIN TB_User A ON M.ID = A.ID
	WHERE (A.LastUpdateDate IS NOT NULL AND M.LastUpdateDate IS NULL)
		OR A.LastUpdateDate <> M.LastUpdateDate

	--INSERT NEW RECORDS
	INSERT INTO DB_MoneyTrace.dbo.TB_User
		(ID, UserLoginName, FirstName, LastName, TimeZone, Descriptions, IsActive, CreateDate, LastUpdateDate)
	SELECT A.ID, A.UserLoginName, A.FirstName, A.LastName, A.TimeZone, A.Descriptions, A.IsActive, A.CreateDate, A.LastUpdateDate
	FROM TB_User A
		LEFT JOIN DB_MoneyTrace.dbo.TB_User M ON M.ID = A.ID
	WHERE M.ID IS NULL
END

