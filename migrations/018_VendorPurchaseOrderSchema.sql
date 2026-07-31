-- =============================================
-- ORBIT ERP - 018_VendorPurchaseOrderSchema.sql
-- Vendor & Purchase Order Schema
-- Created: 2026
-- =============================================

USE OrbitERP;
GO

-- =============================================
-- 1. Vendors
-- =============================================
CREATE TABLE Vendors (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    VendorCode          NVARCHAR(50)        NOT NULL,
    CompanyName         NVARCHAR(200)       NOT NULL,
    ContactPerson       NVARCHAR(150)       NULL,
    Email               NVARCHAR(200)       NULL,
    Phone               NVARCHAR(50)        NULL,
    AlternatePhone      NVARCHAR(50)        NULL,
    Website             NVARCHAR(200)       NULL,
    TaxNumber           NVARCHAR(100)       NULL,
    RegistrationNo      NVARCHAR(100)       NULL,
    VendorType          NVARCHAR(50)        NOT NULL DEFAULT 'SUPPLIER',  -- SUPPLIER, MANUFACTURER, DISTRIBUTOR, SERVICE
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'ACTIVE',    -- ACTIVE, INACTIVE, BLACKLISTED
    CreditLimit         DECIMAL(18,2)       NOT NULL DEFAULT 0,
    PaymentTerms        NVARCHAR(100)       NULL,                          -- Net 30, Net 60, etc.
    Currency            NVARCHAR(10)        NOT NULL DEFAULT 'PKR',
    -- Address
    Address             NVARCHAR(500)       NULL,
    City                NVARCHAR(100)       NULL,
    State               NVARCHAR(100)       NULL,
    Country             NVARCHAR(100)       NULL DEFAULT 'Pakistan',
    PostalCode          NVARCHAR(20)        NULL,
    -- Notes
    Notes               NVARCHAR(1000)      NULL,
    -- FK
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    BranchId            UNIQUEIDENTIFIER    NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_Vendors PRIMARY KEY (Id),
    CONSTRAINT UQ_Vendors_VendorCode UNIQUE (VendorCode),
    CONSTRAINT FK_Vendors_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_Vendors_Branch FOREIGN KEY (BranchId)
        REFERENCES Branches(Id),
    CONSTRAINT FK_Vendors_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_Vendors_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_Vendors_Type CHECK (VendorType IN ('SUPPLIER', 'MANUFACTURER', 'DISTRIBUTOR', 'SERVICE')),
    CONSTRAINT CHK_Vendors_Status CHECK (Status IN ('ACTIVE', 'INACTIVE', 'BLACKLISTED')),
    CONSTRAINT CHK_Vendors_CreditLimit CHECK (CreditLimit >= 0)
);
GO

-- =============================================
-- 2. VendorContacts
-- =============================================
CREATE TABLE VendorContacts (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    VendorId            UNIQUEIDENTIFIER    NOT NULL,
    FullName            NVARCHAR(150)       NOT NULL,
    Designation         NVARCHAR(100)       NULL,
    Email               NVARCHAR(200)       NULL,
    Phone               NVARCHAR(50)        NULL,
    IsPrimary           BIT                 NOT NULL DEFAULT 0,
    Notes               NVARCHAR(500)       NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_VendorContacts PRIMARY KEY (Id),
    CONSTRAINT FK_VendorContacts_Vendor FOREIGN KEY (VendorId)
        REFERENCES Vendors(Id) ON DELETE CASCADE
);
GO

-- =============================================
-- 3. VendorBankAccounts
-- =============================================
CREATE TABLE VendorBankAccounts (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    VendorId            UNIQUEIDENTIFIER    NOT NULL,
    BankName            NVARCHAR(200)       NOT NULL,
    AccountTitle        NVARCHAR(200)       NOT NULL,
    AccountNumber       NVARCHAR(100)       NOT NULL,
    IBAN                NVARCHAR(50)        NULL,
    BranchCode          NVARCHAR(50)        NULL,
    BranchName          NVARCHAR(200)       NULL,
    IsDefault           BIT                 NOT NULL DEFAULT 0,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_VendorBankAccounts PRIMARY KEY (Id),
    CONSTRAINT FK_VendorBankAccounts_Vendor FOREIGN KEY (VendorId)
        REFERENCES Vendors(Id) ON DELETE CASCADE
);
GO

-- =============================================
-- 4. PurchaseOrders
-- =============================================
CREATE TABLE PurchaseOrders (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    PONumber            NVARCHAR(50)        NOT NULL,
    VendorId            UNIQUEIDENTIFIER    NOT NULL,
    OrderDate           DATE                NOT NULL DEFAULT CAST(GETUTCDATE() AS DATE),
    ExpectedDelivery    DATE                NULL,
    ActualDelivery      DATE                NULL,
    Status              NVARCHAR(30)        NOT NULL DEFAULT 'DRAFT',    -- DRAFT, SENT, CONFIRMED, PARTIALLY_RECEIVED, RECEIVED, CANCELLED, CLOSED
    PaymentStatus       NVARCHAR(20)        NOT NULL DEFAULT 'UNPAID',   -- UNPAID, PARTIAL, PAID
    -- Financial
    SubTotal            DECIMAL(18,2)       NOT NULL DEFAULT 0,
    DiscountAmount      DECIMAL(18,2)       NOT NULL DEFAULT 0,
    TaxAmount           DECIMAL(18,2)       NOT NULL DEFAULT 0,
    ShippingCost        DECIMAL(18,2)       NOT NULL DEFAULT 0,
    TotalAmount         DECIMAL(18,2)       NOT NULL DEFAULT 0,
    PaidAmount          DECIMAL(18,2)       NOT NULL DEFAULT 0,
    BalanceDue          DECIMAL(18,2)       NOT NULL DEFAULT 0,
    -- Details
    ShippingAddress     NVARCHAR(500)       NULL,
    PaymentTerms        NVARCHAR(100)       NULL,
    Notes               NVARCHAR(1000)      NULL,
    TermsConditions     NVARCHAR(2000)      NULL,
    ReferenceNo         NVARCHAR(100)       NULL,
    -- FK
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    BranchId            UNIQUEIDENTIFIER    NULL,
    ApprovedBy          UNIQUEIDENTIFIER    NULL,
    ApprovedAt          DATETIME2           NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_PurchaseOrders PRIMARY KEY (Id),
    CONSTRAINT UQ_PurchaseOrders_PONumber UNIQUE (PONumber),
    CONSTRAINT FK_PurchaseOrders_Vendor FOREIGN KEY (VendorId)
        REFERENCES Vendors(Id),
    CONSTRAINT FK_PurchaseOrders_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_PurchaseOrders_Branch FOREIGN KEY (BranchId)
        REFERENCES Branches(Id),
    CONSTRAINT FK_PurchaseOrders_ApprovedBy FOREIGN KEY (ApprovedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_PurchaseOrders_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_PurchaseOrders_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_PurchaseOrders_Status CHECK (Status IN (
        'DRAFT', 'SENT', 'CONFIRMED', 'PARTIALLY_RECEIVED', 'RECEIVED', 'CANCELLED', 'CLOSED'
    )),
    CONSTRAINT CHK_PurchaseOrders_PaymentStatus CHECK (PaymentStatus IN ('UNPAID', 'PARTIAL', 'PAID')),
    CONSTRAINT CHK_PurchaseOrders_Amounts CHECK (TotalAmount >= 0 AND PaidAmount >= 0 AND BalanceDue >= 0)
);
GO

-- =============================================
-- 5. PurchaseOrderLines
-- =============================================
CREATE TABLE PurchaseOrderLines (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    PurchaseOrderId     UNIQUEIDENTIFIER    NOT NULL,
    ProductId           UNIQUEIDENTIFIER    NULL,                         -- NULL allows non-product/service lines
    LineNumber          INT                 NOT NULL,
    Description         NVARCHAR(500)       NOT NULL,
    Quantity            DECIMAL(18,4)       NOT NULL DEFAULT 1,
    ReceivedQuantity    DECIMAL(18,4)       NOT NULL DEFAULT 0,
    UnitPrice           DECIMAL(18,2)       NOT NULL DEFAULT 0,
    DiscountPercent     DECIMAL(5,2)        NOT NULL DEFAULT 0,
    DiscountAmount      DECIMAL(18,2)       NOT NULL DEFAULT 0,
    TaxPercent          DECIMAL(5,2)        NOT NULL DEFAULT 0,
    TaxAmount           DECIMAL(18,2)       NOT NULL DEFAULT 0,
    LineTotal           DECIMAL(18,2)       NOT NULL DEFAULT 0,
    UnitOfMeasure       NVARCHAR(50)        NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_PurchaseOrderLines PRIMARY KEY (Id),
    CONSTRAINT FK_PurchaseOrderLines_PO FOREIGN KEY (PurchaseOrderId)
        REFERENCES PurchaseOrders(Id) ON DELETE CASCADE,
    CONSTRAINT FK_PurchaseOrderLines_Product FOREIGN KEY (ProductId)
        REFERENCES Products(Id),
    CONSTRAINT CHK_PurchaseOrderLines_Quantity CHECK (Quantity > 0),
    CONSTRAINT CHK_PurchaseOrderLines_ReceivedQty CHECK (ReceivedQuantity >= 0),
    CONSTRAINT CHK_PurchaseOrderLines_UnitPrice CHECK (UnitPrice >= 0),
    CONSTRAINT CHK_PurchaseOrderLines_LineNumber CHECK (LineNumber > 0)
);
GO

-- =============================================
-- 6. VendorPayments
-- =============================================
CREATE TABLE VendorPayments (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    VendorId            UNIQUEIDENTIFIER    NOT NULL,
    PurchaseOrderId     UNIQUEIDENTIFIER    NULL,                         -- Payment may cover multiple POs
    PaymentDate         DATE                NOT NULL DEFAULT CAST(GETUTCDATE() AS DATE),
    Amount              DECIMAL(18,2)       NOT NULL,
    PaymentMethod       NVARCHAR(50)        NOT NULL DEFAULT 'BANK_TRANSFER', -- CASH, BANK_TRANSFER, CHEQUE, CARD, ONLINE
    ReferenceNo         NVARCHAR(100)       NULL,
    ChequeNo            NVARCHAR(100)       NULL,
    BankName            NVARCHAR(200)       NULL,
    Notes               NVARCHAR(500)       NULL,
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'COMPLETED', -- COMPLETED, PENDING, FAILED, REVERSED
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_VendorPayments PRIMARY KEY (Id),
    CONSTRAINT FK_VendorPayments_Vendor FOREIGN KEY (VendorId)
        REFERENCES Vendors(Id),
    CONSTRAINT FK_VendorPayments_PO FOREIGN KEY (PurchaseOrderId)
        REFERENCES PurchaseOrders(Id),
    CONSTRAINT FK_VendorPayments_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_VendorPayments_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_VendorPayments_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_VendorPayments_Amount CHECK (Amount > 0),
    CONSTRAINT CHK_VendorPayments_Method CHECK (PaymentMethod IN (
        'CASH', 'BANK_TRANSFER', 'CHEQUE', 'CARD', 'ONLINE'
    )),
    CONSTRAINT CHK_VendorPayments_Status CHECK (Status IN (
        'COMPLETED', 'PENDING', 'FAILED', 'REVERSED'
    ))
);
GO

-- =============================================
-- 7. INDEXES
-- =============================================

-- Vendors
CREATE INDEX IX_Vendors_CompanyId       ON Vendors(CompanyId);
CREATE INDEX IX_Vendors_Status          ON Vendors(Status);
CREATE INDEX IX_Vendors_VendorType      ON Vendors(VendorType);

-- VendorContacts
CREATE INDEX IX_VendorContacts_VendorId ON VendorContacts(VendorId);

-- VendorBankAccounts
CREATE INDEX IX_VendorBankAccounts_VendorId ON VendorBankAccounts(VendorId);

-- PurchaseOrders
CREATE INDEX IX_PurchaseOrders_VendorId     ON PurchaseOrders(VendorId);
CREATE INDEX IX_PurchaseOrders_CompanyId    ON PurchaseOrders(CompanyId);
CREATE INDEX IX_PurchaseOrders_BranchId     ON PurchaseOrders(BranchId);
CREATE INDEX IX_PurchaseOrders_Status       ON PurchaseOrders(Status);
CREATE INDEX IX_PurchaseOrders_OrderDate    ON PurchaseOrders(OrderDate);
CREATE INDEX IX_PurchaseOrders_PaymentStatus ON PurchaseOrders(PaymentStatus);

-- PurchaseOrderLines
CREATE INDEX IX_PurchaseOrderLines_POId     ON PurchaseOrderLines(PurchaseOrderId);
CREATE INDEX IX_PurchaseOrderLines_ProductId ON PurchaseOrderLines(ProductId);

-- VendorPayments
CREATE INDEX IX_VendorPayments_VendorId     ON VendorPayments(VendorId);
CREATE INDEX IX_VendorPayments_POId         ON VendorPayments(PurchaseOrderId);
CREATE INDEX IX_VendorPayments_CompanyId    ON VendorPayments(CompanyId);
CREATE INDEX IX_VendorPayments_Date         ON VendorPayments(PaymentDate);
GO

-- =============================================
-- 8. TRIGGER: Auto-update PO totals from lines
-- =============================================
CREATE OR ALTER TRIGGER TR_PurchaseOrderLines_UpdateTotals
ON PurchaseOrderLines
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AffectedIds TABLE (POId UNIQUEIDENTIFIER);

    INSERT INTO @AffectedIds SELECT PurchaseOrderId FROM inserted;
    INSERT INTO @AffectedIds SELECT PurchaseOrderId FROM deleted;

    UPDATE PO
    SET
        SubTotal       = ISNULL(L.SubTotal, 0),
        TaxAmount      = ISNULL(L.TaxTotal, 0),
        DiscountAmount = ISNULL(L.DiscountTotal, 0),
        TotalAmount    = ISNULL(L.SubTotal, 0) + ISNULL(L.TaxTotal, 0)
                         - ISNULL(L.DiscountTotal, 0) + PO.ShippingCost,
        BalanceDue     = (ISNULL(L.SubTotal, 0) + ISNULL(L.TaxTotal, 0)
                         - ISNULL(L.DiscountTotal, 0) + PO.ShippingCost) - PO.PaidAmount,
        UpdatedAt      = GETUTCDATE()
    FROM PurchaseOrders PO
    INNER JOIN @AffectedIds A ON PO.Id = A.POId
    LEFT JOIN (
        SELECT
            PurchaseOrderId,
            SUM(Quantity * UnitPrice)   AS SubTotal,
            SUM(TaxAmount)              AS TaxTotal,
            SUM(DiscountAmount)         AS DiscountTotal
        FROM PurchaseOrderLines
        GROUP BY PurchaseOrderId
    ) L ON PO.Id = L.PurchaseOrderId;
END;
GO

-- =============================================
-- 9. TRIGGER: Auto-update PO BalanceDue on payment
-- =============================================
CREATE OR ALTER TRIGGER TR_VendorPayments_UpdateBalance
ON VendorPayments
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AffectedIds TABLE (POId UNIQUEIDENTIFIER);

    INSERT INTO @AffectedIds
    SELECT PurchaseOrderId FROM inserted WHERE PurchaseOrderId IS NOT NULL;
    INSERT INTO @AffectedIds
    SELECT PurchaseOrderId FROM deleted  WHERE PurchaseOrderId IS NOT NULL;

    UPDATE PO
    SET
        PaidAmount    = ISNULL(P.TotalPaid, 0),
        BalanceDue    = PO.TotalAmount - ISNULL(P.TotalPaid, 0),
        PaymentStatus = CASE
                            WHEN ISNULL(P.TotalPaid, 0) = 0               THEN 'UNPAID'
                            WHEN ISNULL(P.TotalPaid, 0) >= PO.TotalAmount THEN 'PAID'
                            ELSE 'PARTIAL'
                        END,
        UpdatedAt     = GETUTCDATE()
    FROM PurchaseOrders PO
    INNER JOIN @AffectedIds A ON PO.Id = A.POId
    LEFT JOIN (
        SELECT PurchaseOrderId, SUM(Amount) AS TotalPaid
        FROM VendorPayments
        WHERE Status = 'COMPLETED'
        GROUP BY PurchaseOrderId
    ) P ON PO.Id = P.PurchaseOrderId;
END;
GO

PRINT '018_VendorPurchaseOrderSchema.sql executed successfully.';
PRINT 'Tables created: Vendors, VendorContacts, VendorBankAccounts, PurchaseOrders, PurchaseOrderLines, VendorPayments';
PRINT 'Indexes: 17 indexes created';
PRINT 'Triggers: TR_PurchaseOrderLines_UpdateTotals, TR_VendorPayments_UpdateBalance';
GO
