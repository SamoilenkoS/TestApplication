/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/
DECLARE @adminId UNIQUEIDENTIFIER = NEWID()
INSERT INTO Users
VALUES (@adminId, 'Admin', 'Alexandr', 'Brulov', '1970-03-02', 'StrongPass');

DECLARE @forecasterId UNIQUEIDENTIFIER = NEWID()
INSERT INTO Users
VALUES (@forecasterId, 'Forecaster', 'Vasya', 'Pupkin', '1990-05-04', 'StrongPass');

DECLARE @adminForecasterId UNIQUEIDENTIFIER = NEWID()
INSERT INTO Users
VALUES (@adminForecasterId, 'AdminForecaster', 'Izya', 'Fomin', '1995-12-03', 'StrongPass');


DECLARE @adminRoleId UNIQUEIDENTIFIER = NEWID()
INSERT INTO Roles
VALUES (@adminRoleId, 'Forecaster')

DECLARE @forecasterRoleId UNIQUEIDENTIFIER = NEWID()
INSERT INTO Roles
VALUES (@forecasterRoleId, 'Administrator')


INSERT INTO UserRoles
VALUES (@adminId, @adminRoleId)

INSERT INTO UserRoles
VALUES (@forecasterId, @forecasterRoleId)

INSERT INTO UserRoles
VALUES (@adminForecasterId, @adminRoleId)

INSERT INTO UserRoles
VALUES (@adminForecasterId, @forecasterRoleId)