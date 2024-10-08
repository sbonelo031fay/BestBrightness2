USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[GetAllReportsInventory]    Script Date: 2024/08/01 21:29:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetAllReportsInventory]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        FORMAT(S.[Date], 'yyyy-MM-dd HH:mm') AS SalesMinute,
        SUM(S.[Quantity] * S.[Price]) AS TotalSales,
        COUNT(DISTINCT S.[ProductId]) AS ProductsSold,
        STRING_AGG(SP.[FirstName], ', ') WITHIN GROUP (ORDER BY SP.[FirstName]) AS SalesByEmployee
    FROM 
        [dbo].[Sales] S
    INNER JOIN
        [dbo].[User] SP ON S.[SalespersonId] = SP.[EmailAddress]
    GROUP BY 
        FORMAT(S.[Date], 'yyyy-MM-dd HH:mm')
    ORDER BY 
        FORMAT(S.[Date], 'yyyy-MM-dd HH:mm');
END
