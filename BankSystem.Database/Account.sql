CREATE TABLE [dbo].[Account]
(
	[IBANNumber] NCHAR(18) NOT NULL PRIMARY KEY, 
    [CustomerId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [IsActive] INT NOT NULL, 
    [Balance] DECIMAL NOT NULL, 
    [CreatedDate] DATETIME NULL, 
    CONSTRAINT [FK_Account_Customer] FOREIGN KEY (CustomerId) REFERENCES [dbo].[Customer](Id)
)
