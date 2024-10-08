USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[GetAllInvetory]    Script Date: 2024/08/01 21:29:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetAllInvetory]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id] As ProductId
          ,[Name]
          ,[StockLevel]
          ,[Price]
      FROM [dbo].[Products];
END
