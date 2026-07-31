USE OrbitERP;
GO

-- =============================================
-- TASK 13: Stock Movement Schema
-- ORBIT ERP — Inventory Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE StockMovementTypes (
    Id          UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Code        NVARCHAR(50)     NOT NULL,
    Name        NVARCHAR(100)    NOT NULL,
    Direction   NVARCHAR(10)     NOT NULL,   -- IN | OUT | BOTH
    IsActive    BIT              NOT NULL DEFAULT 1,

    CONSTRAINT UQ_StockMovementTypes_Code UNIQUE (Code)
);
GO

CREATE TABLE StockMovements (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    ReferenceNo     NVARCHAR(50)     NOT NULL,
    MovementTypeId  UNIQUEIDENTIFIER NOT NULL,
    FromWarehouseId UNIQUEIDENTIFIER NULL,
    ToWarehouseId   UNIQUEIDENTIFIER NULL,
    MovementDate    DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    Notes           NVARCHAR(500)    NULL,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'DRAFT',
                    -- DRAFT | CONFIRMED | CANCELLED
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    CreatedBy       UNIQUEIDENTIFIER NULL,
    ConfirmedBy     UNIQUEIDENTIFIER NULL,
    ConfirmedAt     DATETIME2        NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT UQ_StockMovements_RefNo UNIQUE (ReferenceNo),
    CONSTRAINT FK_StockMovements_Type
        FOREIGN KEY (MovementTypeId) REFERENCES StockMovementTypes(Id),
    CONSTRAINT FK_StockMovements_FromWarehouse
        FOREIGN KEY (FromWarehouseId) REFERENCES Warehouses(Id),
    CONSTRAINT FK_StockMovements_ToWarehouse
        FOREIGN KEY (ToWarehouseId) REFERENCES Warehouses(Id),
    CONSTRAINT FK_StockMovements_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_StockMovements_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    CONSTRAINT FK_StockMovements_ConfirmedBy
        FOREIGN KEY (ConfirmedBy) REFERENCES Users(Id)
);
GO

CREATE TABLE StockMovementLines (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    MovementId      UNIQUEIDENTIFIER NOT NULL,
    ProductId       UNIQUEIDENTIFIER NOT NULL,
    VariantId       UNIQUEIDENTIFIER NULL,
    FromLocationId  UNIQUEIDENTIFIER NULL,
    ToLocationId    UNIQUEIDENTIFIER NULL,
    Quantity        DECIMAL(18,4)    NOT NULL,
    UomId           UNIQUEIDENTIFIER NOT NULL,
    CostPrice       DECIMAL(18,4)    NOT NULL DEFAULT 0,
    BatchNo         NVARCHAR(100)    NULL,
    SerialNo        NVARCHAR(100)    NULL,
    ExpiryDate      DATE             NULL,
    Notes           NVARCHAR(500)    NULL,

    CONSTRAINT FK_SML_Movement
        FOREIGN KEY (MovementId) REFERENCES StockMovements(Id),
    CONSTRAINT FK_SML_Product
        FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT FK_SML_Variant
        FOREIGN KEY (VariantId) REFERENCES ProductVariants(Id),
    CONSTRAINT FK_SML_FromLocation
        FOREIGN KEY (FromLocationId) REFERENCES StorageLocations(Id),
    CONSTRAINT FK_SML_ToLocation
        FOREIGN KEY (ToLocationId) REFERENCES StorageLocations(Id),
    CONSTRAINT FK_SML_Uom
        FOREIGN KEY (UomId) REFERENCES UnitsOfMeasure(Id)
);
GO

INSERT INTO StockMovementTypes (Code, Name, Direction) VALUES
    ('RECEIPT',    'Stock Receipt',    'IN'),
    ('ISSUE',      'Stock Issue',      'OUT'),
    ('TRANSFER',   'Stock Transfer',   'BOTH'),
    ('ADJUSTMENT', 'Stock Adjustment', 'BOTH'),
    ('RETURN',     'Stock Return',     'IN');
GO

CREATE INDEX IX_StockMovements_Date     ON StockMovements(MovementDate);
CREATE INDEX IX_StockMovements_Status   ON StockMovements(Status);
CREATE INDEX IX_StockMovementLines_Prod ON StockMovementLines(ProductId);
CREATE INDEX IX_StockMovementLines_Move ON StockMovementLines(MovementId);
GO

PRINT '013 — Stock Movement schema created successfully.';
GO
