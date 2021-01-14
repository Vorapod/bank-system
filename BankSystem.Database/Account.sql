﻿CREATE TABLE [dbo].[Account]
(
	[IBANNumber] NCHAR(18) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [Balance] DECIMAL(18, 2) NOT NULL DEFAULT 0.00, 
    [CreatedDate] DATETIME NOT NULL
)
