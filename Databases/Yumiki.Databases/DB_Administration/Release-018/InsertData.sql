--INSERT INTO DB_Administration-----------------------------------------------------------------------------------------------------------------------------------
USE [DB_Administration]
GO

DECLARE @ShopperID UNIQUEIDENTIFIER;
SELECT @ShopperID = ID FROM TB_Privilege WHERE PrivilegeName = 'Shopper'

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Product Offset', '/Shopper/ProductQuantityOffset/Index', 1, @ShopperID, 'Product Quantity Offset Setup Page', 1, GETDATE(), NULL, 5450)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Revenue Report', '/Shopper/Report/Index', 1, @ShopperID, 'Revenue Report Setup Page', 1, GETDATE(), NULL, 5600)

