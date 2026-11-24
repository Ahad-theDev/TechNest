USE TechNestDb;
GO

IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL
    DROP TABLE dbo.Products;
GO

CREATE TABLE dbo.Products (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Name        NVARCHAR(100)  NOT NULL,
    Description NVARCHAR(500)  NOT NULL DEFAULT '',
    Price       DECIMAL(10,2)  NOT NULL,
    Stock       INT            NOT NULL DEFAULT 0,
    CategoryId  INT            NOT NULL,
    CONSTRAINT FK_Products_Categories
        FOREIGN KEY (CategoryId) REFERENCES dbo.Categories(Id)
);
GO

INSERT INTO Products (Name, Description, Price, Stock, CategoryId)
SELECT 'USB-C Fast Charger 20W', 'Compact 20W fast charger for phones and tablets.', 1800, 50, Id
FROM Categories WHERE Name = 'Chargers and Cables';

INSERT INTO Products (Name, Description, Price, Stock, CategoryId)
SELECT 'Braided Charging Cable 1m', 'Durable braided USB-C cable.', 600, 120, Id
FROM Categories WHERE Name = 'Chargers and Cables';

INSERT INTO Products (Name, Description, Price, Stock, CategoryId)
SELECT '10000mAh Power Bank', 'Slim power bank with dual USB output.', 3500, 30, Id
FROM Categories WHERE Name = 'Chargers and Cables';

INSERT INTO Products (Name, Description, Price, Stock, CategoryId)
SELECT 'Wireless Earbuds', 'Bluetooth earbuds with a charging case.', 4500, 25, Id
FROM Categories WHERE Name = 'Audio';

INSERT INTO Products (Name, Description, Price, Stock, CategoryId)
SELECT 'Bluetooth Speaker', 'Portable speaker with deep bass.', 5500, 15, Id
FROM Categories WHERE Name = 'Audio';

INSERT INTO Products (Name, Description, Price, Stock, CategoryId)
SELECT 'Shockproof Phone Case', 'Clear case with reinforced corners.', 900, 80, Id
FROM Categories WHERE Name = 'Cases and Protection';
GO