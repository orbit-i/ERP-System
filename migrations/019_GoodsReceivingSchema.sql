-- =============================================
-- ORBIT ERP - 019_GoodsReceivingSchema.sql
-- Goods Receiving Schema
-- Created: 2026
-- =============================================

USE OrbitERP;
GO

-- =============================================
-- 1. GoodsReceivingNotes (GRN)
-- =============================================
CREATE TABLE GoodsReceivingNotes (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    GRNNumber           NVARCHAR(50)        NOT NULL,
    PurchaseOrderId     UNIQUEIDENTIFIER    NULL,                        -- GRN may arrive without a PO
    VendorId            UNIQUEIDENTIFIER    NOT NULL,
    WarehouseId         UNIQUEIDENTIFIER    NOT NULL,
    ReceivedDate        DATE                NOT NULL DEFAULT CAST(GETUTCDATE() AS DATE),
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'DRAFT',   -- DRAFT, CONFIRMED, POSTED, RETURNED, CANCELLED
    DeliveryNote        NVARCHAR(100)       NULL,                        -- Vendor's delivery note number
    InvoiceReference    NVARCHAR(100)       NULL,                        -- Vendor invoice ref
    QualityStatus       NVARCHAR(20)        NOT NULL DEFAULT 'PENDING', -- PENDING, PASSED, FAILED, PARTIAL
    TotalQuantity       DECIMAL(18,4)       NOT NULL DEFAULT 0,
    TotalValue          DECIMAL(18,2)       NOT NULL DEFAULT 0,
    Notes               NVARCHAR(1000)      NULL,
    -- FK
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    BranchId            UNIQUEIDENTIFIER    NULL,
    ReceivedBy          UNIQUEIDENTIFIER    NULL,
    InspectedBy         UNIQUEIDENTIFIER    NULL,
    InspectedAt         DATETIME2           NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_GoodsReceivingNotes PRIMARY KEY (Id),
    CONSTRAINT UQ_GoodsReceivingNotes_GRNNumber UNIQUE (GRNNumber),
    CONSTRAINT FK_GRN_PurchaseOrder FOREIGN KEY (PurchaseOrderId)
        REFERENCES PurchaseOrders(Id),
    CONSTRAINT FK_GRN_Vendor FOREIGN KEY (VendorId)
        REFERENCES Vendors(Id),
    CONSTRAINT FK_GRN_Warehouse FOREIGN KEY (WarehouseId)
        REFERENCES Warehouses(Id),
    CONSTRAINT FK_GRN_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_GRN_Branch FOREIGN KEY (BranchId)
        REFERENCES Branches(Id),
    CONSTRAINT FK_GRN_ReceivedBy FOREIGN KEY (ReceivedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_GRN_InspectedBy FOREIGN KEY (InspectedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_GRN_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_GRN_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_GRN_Status CHECK (Status IN (
        'DRAFT', 'CONFIRMED', 'POSTED', 'RETURNED', 'CANCELLED'
    )),
    CONSTRAINT CHK_GRN_QualityStatus CHECK (QualityStatus IN (
        'PENDING', 'PASSED', 'FAILED', 'PARTIAL'
    ))
);
GO

-- =============================================
-- 2. GoodsReceivingLines
-- =============================================
CREATE TABLE GoodsReceivingLines (
    Id                      UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    GRNId                   UNIQUEIDENTIFIER    NOT NULL,
    PurchaseOrderLineId     UNIQUEIDENTIFIER    NULL,                    -- Link back to PO line if applicable
    ProductId               UNIQUEIDENTIFIER    NOT NULL,
    StorageLocationId       UNIQUEIDENTIFIER    NULL,                    -- Where in the warehouse it was stored
    LineNumber              INT                 NOT NULL,
    Description             NVARCHAR(500)       NULL,
    OrderedQuantity         DECIMAL(18,4)       NOT NULL DEFAULT 0,      -- From PO
    ReceivedQuantity        DECIMAL(18,4)       NOT NULL DEFAULT 0,      -- Actually received
    AcceptedQuantity        DECIMAL(18,4)       NOT NULL DEFAULT 0,      -- Passed quality check
    RejectedQuantity        DECIMAL(18,4)       NOT NULL DEFAULT 0,      -- Failed quality check
    UnitPrice               DECIMAL(18,2)       NOT NULL DEFAULT 0,
    LineTotal               DECIMAL(18,2)       NOT NULL DEFAULT 0,
    UnitOfMeasure           NVARCHAR(50)        NULL,
    BatchNumber             NVARCHAR(100)       NULL,
    SerialNumber            NVARCHAR(100)       NULL,
    ExpiryDate              DATE                NULL,
    QualityStatus           NVARCHAR(20)        NOT NULL DEFAULT 'PENDING', -- PENDING, PASSED, FAILED
    QualityNotes            NVARCHAR(500)       NULL,
    -- Audit
    CreatedAt               DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt               DATETIME2           NULL,

    CONSTRAINT PK_GoodsReceivingLines PRIMARY KEY (Id),
    CONSTRAINT FK_GRNLines_GRN FOREIGN KEY (GRNId)
        REFERENCES GoodsReceivingNotes(Id) ON DELETE CASCADE,
    CONSTRAINT FK_GRNLines_POLine FOREIGN KEY (PurchaseOrderLineId)
        REFERENCES PurchaseOrderLines(Id),
    CONSTRAINT FK_GRNLines_Product FOREIGN KEY (ProductId)
        REFERENCES Products(Id),
    CONSTRAINT FK_GRNLines_StorageLocation FOREIGN KEY (StorageLocationId)
        REFERENCES StorageLocations(Id),
    CONSTRAINT CHK_GRNLines_ReceivedQty CHECK (ReceivedQuantity >= 0),
    CONSTRAINT CHK_GRNLines_AcceptedQty CHECK (AcceptedQuantity >= 0),
    CONSTRAINT CHK_GRNLines_RejectedQty CHECK (RejectedQuantity >= 0),
    CONSTRAINT CHK_GRNLines_UnitPrice CHECK (UnitPrice >= 0),
    CONSTRAINT CHK_GRNLines_LineNumber CHECK (LineNumber > 0),
    CONSTRAINT CHK_GRNLines_QualityStatus CHECK (QualityStatus IN ('PENDING', 'PASSED', 'FAILED'))
);
GO

-- =============================================
-- 3. GoodsReturnNotes (Return to Vendor)
-- =============================================
CREATE TABLE GoodsReturnNotes (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    ReturnNumber        NVARCHAR(50)        NOT NULL,
    GRNId               UNIQUEIDENTIFIER    NOT NULL,                    -- Original GRN
    VendorId            UNIQUEIDENTIFIER    NOT NULL,
    ReturnDate          DATE                NOT NULL DEFAULT CAST(GETUTCDATE() AS DATE),
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'DRAFT',   -- DRAFT, CONFIRMED, SHIPPED, COMPLETED, CANCELLED
    Reason              NVARCHAR(50)        NOT NULL DEFAULT 'DEFECTIVE', -- DEFECTIVE, WRONG_ITEM, EXCESS, DAMAGED, EXPIRED
    TotalQuantity       DECIMAL(18,4)       NOT NULL DEFAULT 0,
    TotalValue          DECIMAL(18,2)       NOT NULL DEFAULT 0,
    Notes               NVARCHAR(1000)      NULL,
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_GoodsReturnNotes PRIMARY KEY (Id),
    CONSTRAINT UQ_GoodsReturnNotes_ReturnNumber UNIQUE (ReturnNumber),
    CONSTRAINT FK_GRReturn_GRN FOREIGN KEY (GRNId)
        REFERENCES GoodsReceivingNotes(Id),
    CONSTRAINT FK_GRReturn_Vendor FOREIGN KEY (VendorId)
        REFERENCES Vendors(Id),
    CONSTRAINT FK_GRReturn_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_GRReturn_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_GRReturn_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_GRReturn_Status CHECK (Status IN (
        'DRAFT', 'CONFIRMED', 'SHIPPED', 'COMPLETED', 'CANCELLED'
    )),
    CONSTRAINT CHK_GRReturn_Reason CHECK (Reason IN (
        'DEFECTIVE', 'WRONG_ITEM', 'EXCESS', 'DAMAGED', 'EXPIRED'
    ))
);
GO

-- =============================================
-- 4. GoodsReturnLines
-- =============================================
CREATE TABLE GoodsReturnLines (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    ReturnNoteId        UNIQUEIDENTIFIER    NOT NULL,
    GRNLineId           UNIQUEIDENTIFIER    NOT NULL,                    -- Original GRN line
    ProductId           UNIQUEIDENTIFIER    NOT NULL,
    LineNumber          INT                 NOT NULL,
    Quantity            DECIMAL(18,4)       NOT NULL,
    UnitPrice           DECIMAL(18,2)       NOT NULL DEFAULT 0,
    LineTotal           DECIMAL(18,2)       NOT NULL DEFAULT 0,
    Reason              NVARCHAR(500)       NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_GoodsReturnLines PRIMARY KEY (Id),
    CONSTRAINT FK_GRReturnLines_ReturnNote FOREIGN KEY (ReturnNoteId)
        REFERENCES GoodsReturnNotes(Id) ON DELETE CASCADE,
    CONSTRAINT FK_GRReturnLines_GRNLine FOREIGN KEY (GRNLineId)
        REFERENCES GoodsReceivingLines(Id),
    CONSTRAINT FK_GRReturnLines_Product FOREIGN KEY (ProductId)
        REFERENCES Products(Id),
    CONSTRAINT CHK_GRReturnLines_Quantity CHECK (Quantity > 0),
    CONSTRAINT CHK_GRReturnLines_UnitPrice CHECK (UnitPrice >= 0),
    CONSTRAINT CHK_GRReturnLines_LineNumber CHECK (LineNumber > 0)
);
GO

-- =============================================
-- 5. INDEXES
-- =============================================

-- GoodsReceivingNotes
CREATE INDEX IX_GRN_PurchaseOrderId    ON GoodsReceivingNotes(PurchaseOrderId);
CREATE INDEX IX_GRN_VendorId           ON GoodsReceivingNotes(VendorId);
CREATE INDEX IX_GRN_WarehouseId        ON GoodsReceivingNotes(WarehouseId);
CREATE INDEX IX_GRN_CompanyId          ON GoodsReceivingNotes(CompanyId);
CREATE INDEX IX_GRN_Status             ON GoodsReceivingNotes(Status);
CREATE INDEX IX_GRN_ReceivedDate       ON GoodsReceivingNotes(ReceivedDate);

-- GoodsReceivingLines
CREATE INDEX IX_GRNLines_GRNId         ON GoodsReceivingLines(GRNId);
CREATE INDEX IX_GRNLines_ProductId     ON GoodsReceivingLines(ProductId);
CREATE INDEX IX_GRNLines_POLineId      ON GoodsReceivingLines(PurchaseOrderLineId);
CREATE INDEX IX_GRNLines_StorageLocId  ON GoodsReceivingLines(StorageLocationId);

-- GoodsReturnNotes
CREATE INDEX IX_GRReturn_GRNId         ON GoodsReturnNotes(GRNId);
CREATE INDEX IX_GRReturn_VendorId      ON GoodsReturnNotes(VendorId);
CREATE INDEX IX_GRReturn_Status        ON GoodsReturnNotes(Status);

-- GoodsReturnLines
CREATE INDEX IX_GRReturnLines_ReturnId ON GoodsReturnLines(ReturnNoteId);
CREATE INDEX IX_GRReturnLines_ProductId ON GoodsReturnLines(ProductId);
GO

-- =============================================
-- 6. TRIGGER: Auto-update GRN totals from lines
-- =============================================
CREATE OR ALTER TRIGGER TR_GRNLines_UpdateTotals
ON GoodsReceivingLines
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AffectedIds TABLE (GRNId UNIQUEIDENTIFIER);

    INSERT INTO @AffectedIds SELECT GRNId FROM inserted;
    INSERT INTO @AffectedIds SELECT GRNId FROM deleted;

    UPDATE G
    SET
        TotalQuantity   = ISNULL(L.TotalQty, 0),
        TotalValue      = ISNULL(L.TotalVal, 0),
        UpdatedAt       = GETUTCDATE()
    FROM GoodsReceivingNotes G
    INNER JOIN @AffectedIds A ON G.Id = A.GRNId
    LEFT JOIN (
        SELECT
            GRNId,
            SUM(AcceptedQuantity)           AS TotalQty,
            SUM(AcceptedQuantity * UnitPrice) AS TotalVal
        FROM GoodsReceivingLines
        GROUP BY GRNId
    ) L ON G.Id = L.GRNId;
END;
GO

-- =============================================
-- 7. TRIGGER: Auto-update PO ReceivedQuantity
--    when GRN lines are posted
-- =============================================
CREATE OR ALTER TRIGGER TR_GRNLines_UpdatePOReceivedQty
ON GoodsReceivingLines
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Update ReceivedQuantity on PurchaseOrderLines
    UPDATE POL
    SET
        ReceivedQuantity = ISNULL(G.TotalReceived, 0),
        UpdatedAt        = GETUTCDATE()
    FROM PurchaseOrderLines POL
    INNER JOIN (
        SELECT
            PurchaseOrderLineId,
            SUM(AcceptedQuantity) AS TotalReceived
        FROM GoodsReceivingLines
        WHERE PurchaseOrderLineId IS NOT NULL
        GROUP BY PurchaseOrderLineId
    ) G ON POL.Id = G.PurchaseOrderLineId;
END;
GO

PRINT '019_GoodsReceivingSchema.sql executed successfully.';
PRINT 'Tables created: GoodsReceivingNotes, GoodsReceivingLines, GoodsReturnNotes, GoodsReturnLines';
PRINT 'Indexes: 15 indexes created';
PRINT 'Triggers: TR_GRNLines_UpdateTotals, TR_GRNLines_UpdatePOReceivedQty';
GO
