USE DutShop
GO

CREATE PROCEDURE  SimulateBatches
AS 
BEGIN

DECLARE @randomTotalMade int = ABS(CHECKSUM(NEWID()) % 25)
-- Expires after 16 hours.
DECLARE @randomAddHours int = ABS(CHECKSUM(NEWID()) % 25)
DECLARE @completion datetime = DATEADD(HOUR,@randomAddHours,GETDATE())

-- Get random ID.
DECLARE @randomKVP TABLE 
(
	ID int, 
	nameOf nvarchar(50)
) 
INSERT INTO @randomKVP 
	SELECT ID, nameOf
	FROM BatchesInProgress 
	Where ID = (SELECT TOP 1 ID FROM BatchesInProgress ORDER BY NEWID())

DECLARE @ID int = (SELECT TOP 1 ID FROM @randomKVP)
DECLARE @nameOf nvarchar(50) = (SELECT TOP 1 nameOf FROM @randomKVP)

PRINT @ID
PRINT @nameOf
PRINT @randomTotalMade
PRINT @randomAddHours

UPDATE dbo.BatchesInProgress
SET total = @randomTotalMade, completion = @completion
WHERE ID = @ID

END