USE DutShop
GO

CREATE PROCEDURE  GetOnHandInventory
AS 
BEGIN

SELECT nameOf, SUM(total)
FROM OnHandInventory
GROUP BY nameOf

END