USE OrbitERP;
GO

-- =============================================
-- TASK 11: Product & Category Schema
-- ORBIT ERP — Inventory Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE UnitsOfMeasure (
    Id          UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Name        NVARCHAR(100)    NOT NULL,
    Code        NVARCHAR(20)     NOT NULL,
    IsActive    BIT              NOT NULL DEFAULT 1,
    CreatedAt   DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT UQ_UnitsOfMeasure_Code UNIQUE (Code)
);
GO

CREATE TABLE ProductCategories (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Name            NVARCHAR(150)    NOT NULL,
    ParentId        UNIQUEIDENTIFIER NULL,
    Description     NVARCHAR(500)    NULL,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,
    CreatedBy       UNIQUEIDENTIFIER NULL,
    UpdatedBy       UNIQUEIDENTIFIER NULL,

    CONSTRAINT FK_ProductCategories_Parent
        FOREIGN KEY (ParentId) REFERENCES ProductCategories(Id),
    CONSTRAINT FK_ProductCategories_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    CONSTRAINT FK_ProductCategories_UpdatedBy
        FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
);
GO

CREATE TABLE Products (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Code            NVARCHAR(50)     NOT NULL,
    Name            NVARCHAR(200)    NOT NULL,
    CategoryId      UNIQUEIDENTIFIER NOT NULL,
    UomId           UNIQUEIDENTIFIER NOT NULL,
    Description     NVARCHAR(1000)   NULL,
    ProductType     NVARCHAR(50)     NOT NULL DEFAULT 'Storable',
                    -- Storable | Consumable | Service
    CostPrice       DECIMAL(18,4)    NOT NULL DEFAULT 0,
    SalePrice       DECIMAL(18,4)    NOT NULL DEFAULT 0,
    ReorderLevel    DECIMAL(18,4)    NOT NULL DEFAULT 0,
    ReorderQty      DECIMAL(18,4)    NOT NULL DEFAULT 0,
    ImageUrl        NVARCHAR(500)    NULL,
    IsSerialized    BIT              NOT NULL DEFAULT 0,
    IsBatchTracked  BIT              NOT NULL DEFAULT 0,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,
    CreatedBy       UNIQUEIDENTIFIER NULL,
    UpdatedBy       UNIQUEIDENTIFIER NULL,

    CONSTRAINT UQ_Products_Code UNIQUE (Code),
    CONSTRAINT FK_Products_Category
        FOREIGN KEY (CategoryId) REFERENCES ProductCategories(Id),
    CONSTRAINT FK_Products_Uom
        FOREIGN KEY (UomId) REFERENCES UnitsOfMeasure(Id),
    CONSTRAINT FK_Products_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    CONSTRAINT FK_Products_UpdatedBy
        FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
);
GO

CREATE TABLE ProductVariants (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    ProductId       UNIQUEIDENTIFIER NOT NULL,
    VariantName     NVARCHAR(200)    NOT NULL,
    SKU             NVARCHAR(100)    NOT NULL,
    CostPrice       DECIMAL(18,4)    NOT NULL DEFAULT 0,
    SalePrice       DECIMAL(18,4)    NOT NULL DEFAULT 0,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT UQ_ProductVariants_SKU UNIQUE (SKU),
    CONSTRAINT FK_ProductVariants_Product
        FOREIGN KEY (ProductId) REFERENCES Products(Id)
);
GO

CREATE TABLE ProductAttributes (
    Id          UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Name        NVARCHAR(100)    NOT NULL,
    IsActive    BIT              NOT NULL DEFAULT 1
);
GO

CREATE TABLE ProductAttributeValues (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    ProductId       UNIQUEIDENTIFIER NOT NULL,
    AttributeId     UNIQUEIDENTIFIER NOT NULL,
    Value           NVARCHAR(200)    NOT NULL,

    CONSTRAINT FK_PAV_Product
        FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT FK_PAV_Attribute
        FOREIGN KEY (AttributeId) REFERENCES ProductAttributes(Id)
);
GO

-- Indexes
CREATE INDEX IX_Products_CategoryId  ON Products(CategoryId);
CREATE INDEX IX_Products_Code        ON Products(Code);
CREATE INDEX IX_Products_Name        ON Products(Name);
CREATE INDEX IX_Products_IsActive    ON Products(IsActive);
CREATE INDEX IX_ProductVariants_SKU  ON ProductVariants(SKU);
GO

PRINT '011 — Product & Category schema created successfully.';
GO
