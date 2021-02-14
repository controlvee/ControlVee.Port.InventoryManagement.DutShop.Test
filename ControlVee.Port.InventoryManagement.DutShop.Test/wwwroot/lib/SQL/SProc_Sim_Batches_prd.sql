USE [DutShop]
GO
/****** Object:  StoredProcedure [dbo].[SimulateBatches]    Script Date: 2/13/2021 4:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE  [dbo].[SimulateBatches]
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

INSERT INTO dbo.OnHandInventory (ID, nameOf, total, completion, expire)
VALUES (@ID, @nameOf, @randomTotalMade, @completion, DATEADD(HOUR,16,@completion))

END