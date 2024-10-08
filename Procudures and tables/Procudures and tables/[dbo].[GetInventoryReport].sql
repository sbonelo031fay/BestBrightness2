USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[GetInventoryReport]    Script Date: 2024/08/01 21:30:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetInventoryReport]
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH SalesAggregated AS (
        SELECT 
            ProductId,
            SUM(Quantity) AS TotalSold
        FROM 
            [dbo].[Sales]
        GROUP BY 
            ProductId
    )
    SELECT 
        P.[Name],
        ISNULL(P.[StockLevel], 0) - ISNULL(SA.TotalSold, 0) AS StockLevel,
        P.[RestockThreshold],
        CASE 
            WHEN ISNULL(P.[StockLevel], 0) - ISNULL(SA.TotalSold, 0) < P.[RestockThreshold] 
            THEN 'Yes' 
            ELSE 'No' 
        END AS NeedsRestocking
    FROM 
        [dbo].[Products] P
    LEFT JOIN 
        SalesAggregated SA ON P.[Id] = SA.ProductId
    ORDER BY 
        P.[Name];
END
