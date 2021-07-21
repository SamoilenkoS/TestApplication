CREATE PROCEDURE [dbo].[GetAllUserRoles]
AS
    SELECT u.Id UserId,
    r.ROLE [Role]
FROM Users u
INNER JOIN UserRoles ur ON ur.UserId = u.Id
INNER JOIN Roles r ON ur.RoleId = r.Id

RETURN 0
