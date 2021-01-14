CREATE TABLE [dbo].[Account]
(
	[IBANNumber] NCHAR(18) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [Balance] DECIMAL NOT NULL, 
    [CreatedDate] DATETIME NOT NULL
)
