USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 2024/08/01 21:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[GetUser]
@email varchar(50)
AS
BEGIN
SET NOCOUNT ON;

SELECT ISNULL(UR.[role], '') AS Role
      
  FROM [User] U
  INNER JOIN [UserRoles] URS ON U.Id = URS.[UserId]
  INNER JOIN [UserRole] UR ON URS.[RoleId] = UR.[Id]
  WHERE U.EmailAddress = @email;
END
