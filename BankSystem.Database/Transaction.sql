CREATE TABLE [dbo].[Transaction]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SenderIBANNumber] NCHAR(18) NOT NULL, 
    [ReceiverIBANNumber] NCHAR(18) NOT NULL, 
    [Type] INT NOT NULL, 
    [Amount] DECIMAL(18, 2) NOT NULL DEFAULT 0.00, 
    [Fee] DECIMAL(18, 2) NOT NULL DEFAULT 0.00, 
    [OutStandingBalance] DECIMAL(18, 2) NOT NULL DEFAULT 0.00, 
    [CreatedDate] DATETIME NOT NULL, 
    CONSTRAINT [FK_Transaction_Account] FOREIGN KEY (SenderIBANNumber) REFERENCES [dbo].[Account](IBANNumber)

)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1=Debit, 2=Credit',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Transaction',
    @level2type = N'COLUMN',
    @level2name = N'Type'