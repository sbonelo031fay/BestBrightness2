USE [BestBrightness]
GO
/****** Object:  StoredProcedure [dbo].[GetUserRole]    Script Date: 2024/08/01 21:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[GetUserRole]
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
