USE [DutShop]
GO
/****** Object:  StoredProcedure [dbo].[GetOnHandInventory]    Script Date: 2/13/2021 5:38:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE  [dbo].[GetOnHandInventory]
AS 
BEGIN

SELECT nameOf, SUM(total)
FROM OnHandInventory
GROUP BY nameOf

END