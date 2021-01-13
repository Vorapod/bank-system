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

--Initial data into table customer
INSERT INTO [dbo].[Customer]([UserName],[Password],[CreateDate])
     VALUES ('UserOne' ,'111',GetDate()),
	 ('UserTwo' ,'222',GetDate())
