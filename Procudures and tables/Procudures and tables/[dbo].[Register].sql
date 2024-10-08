USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[Register]    Script Date: 2024/08/01 21:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   PROCEDURE [dbo].[Register]
    @Title VARCHAR(10),
    @FirstName VARCHAR(25),
    @LastName VARCHAR(25),
    @EmailAddress VARCHAR(100),
    @ContactNumber VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the email already exists
    IF EXISTS (SELECT 1 FROM [dbo].[User] WHERE EmailAddress = @EmailAddress)
    BEGIN
        RAISERROR('Email address already exists.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        -- Start transaction
        BEGIN TRANSACTION;

        DECLARE @UserId UNIQUEIDENTIFIER;
        SET @UserId = NEWID();

        -- Insert into User table
        INSERT INTO [dbo].[User]
               ([Id]
               ,[Title]
               ,[FirstName]
               ,[LastName]
               ,[EmailAddress]
               ,[ContactNumber]
               ,[IsActive])
         VALUES
               (@UserId
               ,@Title
               ,@FirstName
               ,@LastName
               ,@EmailAddress
               ,@ContactNumber
               ,1);

        -- Insert into UserRoles table
        INSERT INTO [dbo].[UserRoles]
                   ([Id]
                   ,[UserId]
                   ,[RoleId])
             VALUES
                   (NEWID()
                   ,@UserId
                   ,2); -- Assuming '2' is a valid RoleId

        -- Commit transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback transaction if an error occurs
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Rethrow the error
        THROW;
    END CATCH
END
