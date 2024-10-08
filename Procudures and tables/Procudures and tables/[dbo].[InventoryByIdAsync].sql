USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[InventoryByIdAsync]    Script Date: 2024/08/01 21:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Corrected and properly defining the stored procedure
ALTER PROCEDURE [dbo].[InventoryByIdAsync]
    @ProductId UNIQUEIDENTIFIER -- Define the input parameter
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id] As ProductId,
           [Name],
           [StockLevel],
           [Price]
      FROM [dbo].[Products]
      WHERE [Id] = @ProductId; 
END
