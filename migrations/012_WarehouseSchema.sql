USE OrbitERP;
GO

-- =============================================
-- TASK 12: Warehouse Schema
-- ORBIT ERP — Inventory Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE Warehouses (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    BranchId        UNIQUEIDENTIFIER NULL,
    Name            NVARCHAR(200)    NOT NULL,
    Code            NVARCHAR(50)     NOT NULL,
    AddressLine1    NVARCHAR(200)    NULL,
    City            NVARCHAR(100)    NULL,
    Country         NVARCHAR(100)    NULL,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,
    CreatedBy       UNIQUEIDENTIFIER NULL,

    CONSTRAINT UQ_Warehouses_Code UNIQUE (Code, CompanyId),
    CONSTRAINT FK_Warehouses_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_Warehouses_Branch
        FOREIGN KEY (BranchId) REFERENCES Branches(Id),
    CONSTRAINT FK_Warehouses_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);
GO

CREATE TABLE StorageLocations (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    WarehouseId     UNIQUEIDENTIFIER NOT NULL,
    Name            NVARCHAR(100)    NOT NULL,
    Code            NVARCHAR(50)     NOT NULL,
    LocationType    NVARCHAR(50)     NOT NULL DEFAULT 'BIN',
                    -- ZONE | AISLE | RACK | BIN | SHELF
    ParentId        UNIQUEIDENTIFIER NULL,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT UQ_StorageLocations_Code UNIQUE (Code, WarehouseId),
    CONSTRAINT FK_StorageLocations_Warehouse
        FOREIGN KEY (WarehouseId) REFERENCES Warehouses(Id),
    CONSTRAINT FK_StorageLocations_Parent
        FOREIGN KEY (ParentId) REFERENCES StorageLocations(Id)
);
GO

CREATE TABLE StockLevels (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    WarehouseId     UNIQUEIDENTIFIER NOT NULL,
    LocationId      UNIQUEIDENTIFIER NULL,
    ProductId       UNIQUEIDENTIFIER NOT NULL,
    VariantId       UNIQUEIDENTIFIER NULL,
    Quantity        DECIMAL(18,4)    NOT NULL DEFAULT 0,
    ReservedQty     DECIMAL(18,4)    NOT NULL DEFAULT 0,
    AvailableQty    AS (Quantity - ReservedQty),
    UpdatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT UQ_StockLevels UNIQUE (WarehouseId, ProductId, VariantId, LocationId),
    CONSTRAINT FK_StockLevels_Warehouse
        FOREIGN KEY (WarehouseId) REFERENCES Warehouses(Id),
    CONSTRAINT FK_StockLevels_Location
        FOREIGN KEY (LocationId) REFERENCES StorageLocations(Id),
    CONSTRAINT FK_StockLevels_Product
        FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT FK_StockLevels_Variant
        FOREIGN KEY (VariantId) REFERENCES ProductVariants(Id)
);
GO

CREATE INDEX IX_Warehouses_CompanyId    ON Warehouses(CompanyId);
CREATE INDEX IX_StockLevels_ProductId   ON StockLevels(ProductId);
CREATE INDEX IX_StockLevels_WarehouseId ON StockLevels(WarehouseId);
GO

PRINT '012 — Warehouse schema created successfully.';
GO
