USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[UpdateInventoryAsync]    Script Date: 2024/08/01 21:33:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create or alter the stored procedure for updating product details
ALTER   PROCEDURE [dbo].[UpdateInventoryAsync]
    @ProductId UNIQUEIDENTIFIER, 
    @Name NVARCHAR(255),         
    @StockLevel INT,             
    @Price DECIMAL(18, 2)        
AS
BEGIN
    SET NOCOUNT ON;

    -- Update the product details in the Products table
    UPDATE [dbo].[Products]
    SET [Name] = @Name,
        [StockLevel] = @StockLevel,
        [Price] = @Price
    WHERE [Id] = @ProductId;
END
