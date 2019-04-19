USE [DB_Administration]
GO

DECLARE @ShopperID UNIQUEIDENTIFIER = NEWID();  
INSERT INTO TB_Privilege
VALUES(@ShopperID, 'Shopper', '/Shopper/', 0, NULL, 'Root of Shopper Module', 1, GETDATE(), NULL, 5000)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Product', '/Shopper/Product/Index', 1, @ShopperID, 'Product Setup Page', 1, GETDATE(), NULL, 5100)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Receipt', '/Shopper/Receipt/Index', 1, @ShopperID, 'Purchase Receipt Page', 1, GETDATE(), NULL, 5200)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Additional Fee', '/Shopper/AdditionalFee/Index', 1, @ShopperID, 'Additional Fee Page', 1, GETDATE(), NULL, 5300)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Invoice', '/Shopper/Invoice/Index', 1, @ShopperID, 'Invoice Page', 1, GETDATE(), NULL, 5400)

INSERT INTO TB_Privilege
VALUES(NEWID(), 'Fee Type', '/Shopper/FeeType/Index', 1, @ShopperID, 'Fee Type Setup Page', 1, GETDATE(), NULL, 5500)