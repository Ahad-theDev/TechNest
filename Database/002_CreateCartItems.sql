USE TechNestDb;
GO

IF OBJECT_ID('dbo.CartItems', 'U') IS NOT NULL
    DROP TABLE dbo.CartItems;
GO

CREATE TABLE dbo.CartItems (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    UserId    NVARCHAR(450) NOT NULL,
    ProductId INT           NOT NULL,
    Quantity  INT           NOT NULL DEFAULT 1,
    CONSTRAINT FK_CartItems_Users
        FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers(Id) ON DELETE CASCADE,
    CONSTRAINT FK_CartItems_Products
        FOREIGN KEY (ProductId) REFERENCES dbo.Products(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_CartItems_User_Product UNIQUE (UserId, ProductId),
    CONSTRAINT CK_CartItems_Quantity CHECK (Quantity > 0)
);
GO