USE [DB_WellCovered]
GO

ALTER PROCEDURE PROC_PerformSearch
	@keywords NVARCHAR(255),
	@userID UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @keywords = N'"' + @keywords + '*"'

    CREATE TABLE #TempTable (
		ID UNIQUEIDENTIFIER,
		AppID UNIQUEIDENTIFIER,
		ObjectID UNIQUEIDENTIFIER,
		LiveID UNIQUEIDENTIFIER,
		FullTextIndex NTEXT,
		DisplayContents NTEXT,
		UserID UNIQUEIDENTIFIER,
		Descriptions NVARCHAR(255),
		IsActive BIT,
		CreateDate DATETIME,
		LastUpdateDate DATETIME,
		[Rank] INT
	)

	INSERT INTO #TempTable
	SELECT T.ID, T.AppID, T.ObjectID, T.LiveID, T.FullTextIndex
		, T.DisplayContents, T.UserID, T.Descriptions
		, T.IsActive, T.CreateDate, T.LastUpdateDate, I.[Rank] + 900000000
	FROM CONTAINSTABLE(TB_Index, FullTextIndex, @keywords) I
		INNER JOIN TB_Index T ON I.[Key] = T.ID AND T.UserID = @userID

	INSERT INTO #TempTable
	SELECT T.ID, T.AppID, T.ObjectID, T.LiveID, T.FullTextIndex
		, T.DisplayContents, T.UserID, T.Descriptions
		, T.IsActive, T.CreateDate, T.LastUpdateDate, I.[Rank]
	FROM FREETEXTTABLE(TB_Index, FullTextIndex, @keywords) I
		INNER JOIN TB_Index T ON I.[Key] = T.ID AND T.UserID = @userID
		LEFT JOIN #TempTable TEMP ON T.ID = TEMP.ID
	WHERE TEMP.ID IS NULL

	SELECT T.ID, T.AppID, T.ObjectID, T.LiveID, T.FullTextIndex, T.DisplayContents
	FROM #TempTable T
	ORDER BY [Rank] DESC

	DROP TABLE #TempTable
END
GO
