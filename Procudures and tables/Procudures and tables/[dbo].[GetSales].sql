USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[GetSales]    Script Date: 2024/08/01 21:30:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[GetSales]
    @EmailAddress NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        DATEADD(MINUTE, DATEDIFF(MINUTE, 0, S.[Date]), 0) AS Date,
        P.[Name] As ProductName, -- Include the product name
        SUM(S.[Quantity] * S.[Price]) AS TotalSales,
        COUNT(S.[ProductId]) AS ProductsSold,
        STRING_AGG(SP.[FirstName], ', ') AS SalesByEmployee
    FROM 
        [dbo].[Sales] S
    INNER JOIN
        [dbo].[User] SP ON S.[SalespersonId] = SP.[EmailAddress]
    INNER JOIN
        [dbo].[Products] P ON S.[ProductId] = P.[Id] -- Join with the Product table
    WHERE
        SP.[EmailAddress] = @EmailAddress
    GROUP BY 
        DATEADD(MINUTE, DATEDIFF(MINUTE, 0, S.[Date]), 0),
        P.[Name] -- Group by product name
    ORDER BY 
        Date,
        P.[Name]; -- Order by date and product name
END
