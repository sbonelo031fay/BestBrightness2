USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[RecordSales]    Script Date: 2024/08/01 21:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RecordSales]
    @SalespersonId NVARCHAR(255),
    @DateOfSale DATETIME,
    @ProductDetails NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Variables for processing products
    DECLARE @ProductId UNIQUEIDENTIFIER;
    DECLARE @Quantity INT;
    DECLARE @ProductPrice DECIMAL(18, 2);
    DECLARE @StockLevel INT;
    DECLARE @Price DECIMAL(18, 2);
    DECLARE @ProductName NVARCHAR(255);
    DECLARE @Detail NVARCHAR(255);
    DECLARE @SeparatorIndex INT;
    DECLARE @ProductIdStr NVARCHAR(36);

	IF EXISTS (SELECT 1 FROM [dbo].[ProductDetailType])
	BEGIN
    -- If the table is not empty, delete all rows
    DELETE FROM [dbo].[ProductDetailType];
    PRINT 'All rows deleted from [dbo].[ProductDetailType].';
END
    -- Split the comma-separated string and iterate over each product detail
    DECLARE details_cursor CURSOR FOR
    SELECT VALUE AS Detail
    FROM STRING_SPLIT(@ProductDetails, ',')
    WHERE VALUE <> '';

    OPEN details_cursor;
    FETCH NEXT FROM details_cursor INTO @Detail;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Extract ProductId and Quantity from the detail
        SET @SeparatorIndex = CHARINDEX(':', @Detail);
        IF @SeparatorIndex = 0 OR @SeparatorIndex = LEN(@Detail)
        BEGIN
            RAISERROR ('Invalid product detail format: "%s". Expected format: ProductId:Quantity', 16, 1, @Detail);
            FETCH NEXT FROM details_cursor INTO @Detail;
            CONTINUE;
        END

        -- Extracting ProductId and Quantity
        SET @ProductIdStr = LTRIM(RTRIM(LEFT(@Detail, @SeparatorIndex - 1)));
        SET @Quantity = TRY_CAST(LTRIM(RTRIM(SUBSTRING(@Detail, @SeparatorIndex + 1, LEN(@Detail) - @SeparatorIndex))) AS INT);
        
        -- Convert ProductId from string to UNIQUEIDENTIFIER
        SET @ProductId = TRY_CAST(@ProductIdStr AS UNIQUEIDENTIFIER);

        -- Debugging output for ProductId and Quantity
        PRINT 'ProductId: ' + ISNULL(@ProductIdStr, 'NULL') + ', Quantity: ' + ISNULL(CAST(@Quantity AS NVARCHAR), 'NULL');

        -- Check if the ProductId and Quantity are valid
        IF @ProductId IS NULL OR @Quantity IS NULL OR @Quantity <= 0
        BEGIN
            RAISERROR ('Invalid product detail: "%s". ProductId or Quantity is not valid.', 16, 1, @Detail);
            FETCH NEXT FROM details_cursor INTO @Detail;
            CONTINUE;
        END

        -- Get ProductName, ProductPrice, and StockLevel for the current product
        SELECT 
            @ProductName = [Name],
            @ProductPrice = [Price],
            @StockLevel = StockLevel
        FROM [dbo].[Products]
        WHERE [Id] = @ProductId;

        -- Check if the product exists
        IF @ProductName IS NULL
        BEGIN
            RAISERROR ('Product with ID "%s" does not exist.', 16, 1, @ProductIdStr);
            FETCH NEXT FROM details_cursor INTO @Detail;
            CONTINUE;
        END

        -- Check if the stock level is sufficient
        IF @StockLevel < @Quantity
        BEGIN
            RAISERROR ('Insufficient stock level for product ID "%s".', 16, 1, @ProductIdStr);
            FETCH NEXT FROM details_cursor INTO @Detail;
            CONTINUE;
        END

        -- Calculate the total price
        SET @Price = @Quantity * @ProductPrice;

        -- Insert into Sales table
        INSERT INTO [dbo].[Sales] (Id, ProductId, Quantity, Price, Date, SalespersonId)
        VALUES (NEWID(), @ProductId, @Quantity, @Price, @DateOfSale, @SalespersonId);

        -- Update the Products table to decrement the stock level
        UPDATE [dbo].[Products]
        SET StockLevel = StockLevel - @Quantity
        WHERE Id = @ProductId;

        -- Insert into ProductDetailType table
        INSERT INTO [dbo].[ProductDetailType] (ProductId, Quantity)
        VALUES (@ProductId, @Quantity);

        FETCH NEXT FROM details_cursor INTO @Detail;
    END

    CLOSE details_cursor;
    DEALLOCATE details_cursor;
END
