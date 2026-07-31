USE OrbitERP;
GO

-- =============================================
-- TASK 14: Barcode Schema
-- ORBIT ERP — Inventory Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE Barcodes (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    ProductId       UNIQUEIDENTIFIER NOT NULL,
    VariantId       UNIQUEIDENTIFIER NULL,
    BarcodeValue    NVARCHAR(200)    NOT NULL,
    BarcodeType     NVARCHAR(50)     NOT NULL DEFAULT 'EAN13',
                    -- EAN13 | EAN8 | UPC | CODE128 | QR | DATAMATRIX
    IsDefault       BIT              NOT NULL DEFAULT 0,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy       UNIQUEIDENTIFIER NULL,

    CONSTRAINT UQ_Barcodes_Value UNIQUE (BarcodeValue),
    CONSTRAINT FK_Barcodes_Product
        FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT FK_Barcodes_Variant
        FOREIGN KEY (VariantId) REFERENCES ProductVariants(Id),
    CONSTRAINT FK_Barcodes_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);
GO

CREATE TABLE SerialNumbers (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    ProductId       UNIQUEIDENTIFIER NOT NULL,
    VariantId       UNIQUEIDENTIFIER NULL,
    WarehouseId     UNIQUEIDENTIFIER NOT NULL,
    SerialNo        NVARCHAR(100)    NOT NULL,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'AVAILABLE',
                    -- AVAILABLE | RESERVED | SOLD | RETURNED | SCRAPPED
    PurchaseDate    DATE             NULL,
    ExpiryDate      DATE             NULL,
    Notes           NVARCHAR(500)    NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT UQ_SerialNumbers_Serial UNIQUE (SerialNo),
    CONSTRAINT FK_SerialNumbers_Product
        FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT FK_SerialNumbers_Variant
        FOREIGN KEY (VariantId) REFERENCES ProductVariants(Id),
    CONSTRAINT FK_SerialNumbers_Warehouse
        FOREIGN KEY (WarehouseId) REFERENCES Warehouses(Id)
);
GO

CREATE TABLE BatchNumbers (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    ProductId       UNIQUEIDENTIFIER NOT NULL,
    VariantId       UNIQUEIDENTIFIER NULL,
    WarehouseId     UNIQUEIDENTIFIER NOT NULL,
    BatchNo         NVARCHAR(100)    NOT NULL,
    Quantity        DECIMAL(18,4)    NOT NULL DEFAULT 0,
    ManufactureDate DATE             NULL,
    ExpiryDate      DATE             NULL,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'ACTIVE',
                    -- ACTIVE | EXPIRED | QUARANTINE | CONSUMED
    Notes           NVARCHAR(500)    NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT UQ_BatchNumbers UNIQUE (BatchNo, ProductId, WarehouseId),
    CONSTRAINT FK_BatchNumbers_Product
        FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT FK_BatchNumbers_Variant
        FOREIGN KEY (VariantId) REFERENCES ProductVariants(Id),
    CONSTRAINT FK_BatchNumbers_Warehouse
        FOREIGN KEY (WarehouseId) REFERENCES Warehouses(Id)
);
GO

CREATE INDEX IX_Barcodes_ProductId      ON Barcodes(ProductId);
CREATE INDEX IX_Barcodes_Value          ON Barcodes(BarcodeValue);
CREATE INDEX IX_SerialNumbers_ProductId ON SerialNumbers(ProductId);
CREATE INDEX IX_SerialNumbers_Status    ON SerialNumbers(Status);
CREATE INDEX IX_BatchNumbers_ProductId  ON BatchNumbers(ProductId);
CREATE INDEX IX_BatchNumbers_Expiry     ON BatchNumbers(ExpiryDate);
GO

PRINT '014 — Barcode schema created successfully.';
GO
