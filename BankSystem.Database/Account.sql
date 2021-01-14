CREATE TABLE [dbo].[Account]
(
	[IBANNumber] NCHAR(18) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [IsActive] INT NOT NULL, 
    [Balance] DECIMAL NOT NULL, 
    [CreatedDate] DATETIME NOT NULL
)
