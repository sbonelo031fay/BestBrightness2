USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[GetAllReportsInvetory]    Script Date: 2024/08/01 21:30:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetAllReportsInvetory]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CAST(S.[Date] AS DATE) AS Date,
        DATEPART(HOUR, S.[Date]) AS HourOfDay, -- Extracts the hour part
        P.[Name] As ProductName,
        SUM(S.[Quantity] * S.[Price]) AS TotalSales,
        COUNT(S.[ProductId]) AS ProductsSold,
        STRING_AGG(SP.[FirstName], ', ') WITHIN GROUP (ORDER BY SP.[FirstName]) AS SalesByEmployee
    FROM 
        [dbo].[Sales] S
    INNER JOIN
        [dbo].[User] SP ON S.[SalespersonId] = SP.[EmailAddress]
    INNER JOIN
        [dbo].[Products] P ON S.[ProductId] = P.[Id] -- Join with the Product table
    GROUP BY 
        CAST(S.[Date] AS DATE),
        DATEPART(HOUR, S.[Date]),
        P.[Name]
    ORDER BY 
        Date,        -- Orders by the date
        HourOfDay,       -- Orders by hour within the date
        P.[Name]; -- Orders by product name within the hour
END
