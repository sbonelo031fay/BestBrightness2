USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[InsertProductDetails]    Script Date: 2024/08/01 21:32:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[InsertProductDetails]
    @SaleId UNIQUEIDENTIFIER, -- The ID of the sale record
    @ProductDetails NVARCHAR(MAX) -- Comma-separated product details in the format 'ProductId:Quantity'
AS
BEGIN
    SET NOCOUNT ON;

    -- Create a temporary table to hold product details
    CREATE TABLE #TempProductDetails
    (
        ProductId UNIQUEIDENTIFIER,
        Quantity INT
    );

    -- Parse the product details and insert into the temporary table
    ;WITH ParsedDetails AS
    (
        SELECT 
            CAST(SUBSTRING(value, 1, CHARINDEX(':', value) - 1) AS UNIQUEIDENTIFIER) AS ProductId,
            CAST(SUBSTRING(value, CHARINDEX(':', value) + 1, LEN(value)) AS INT) AS Quantity
        FROM STRING_SPLIT(@ProductDetails, ',')
    )
    INSERT INTO #TempProductDetails (ProductId, Quantity)
    SELECT ProductId, Quantity FROM ParsedDetails;

    -- Insert each product detail into the ProductDetailType table
    INSERT INTO [dbo].[ProductDetailType] (ProductId, Quantity)
    SELECT ProductId, Quantity FROM #TempProductDetails;

    -- Clean up the temporary table
    DROP TABLE #TempProductDetails;
END
