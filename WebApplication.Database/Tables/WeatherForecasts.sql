CREATE TABLE [dbo].[WeatherForecasts]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Date] DATETIME NOT NULL,
    [Temperature] INT NOT NULL,
    [Summary] NVARCHAR(100) NOT NULL
)
