USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[GetReportsByDateRange]    Script Date: 2024/08/01 21:30:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetReportsByDateRange]
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        S.[Date],
        SUM(S.[Quantity] * S.[Price]) AS TotalSales,
        COUNT(S.[ProductId]) AS ProductsSold,
        STRING_AGG(SP.[FirstName], ', ') AS SalesByEmployee
    FROM 
        [dbo].[Sales] S
    INNER JOIN
        [dbo].[User] SP ON S.[SalespersonId] = SP.[EmailAddress]
    WHERE 
        S.[Date] BETWEEN @StartDate AND @EndDate
    GROUP BY 
        S.[Date]
    ORDER BY 
        S.[Date];
END
