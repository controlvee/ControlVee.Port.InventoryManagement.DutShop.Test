USE [DutShop]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[SimulateBatches]

SELECT	'Return Value' = @return_value

GO
