-- =============================================
-- ORBIT ERP - 017_InvoiceSchema.sql
-- Invoice Schema
-- Created: 2026
-- =============================================

USE OrbitERP;
GO

-- =============================================
-- 1. Invoices
-- =============================================
CREATE TABLE Invoices (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    InvoiceNo           NVARCHAR(50)        NOT NULL,
    InvoiceType         NVARCHAR(20)        NOT NULL DEFAULT 'SALES',    -- SALES, CREDIT_NOTE, DEBIT_NOTE
    InvoiceDate         DATE                NOT NULL,
    DueDate             DATE                NOT NULL,
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'DRAFT',    -- DRAFT, SENT, PAID, PARTIAL, OVERDUE, CANCELLED, VOID
    PaymentStatus       NVARCHAR(20)        NOT NULL DEFAULT 'UNPAID',   -- UNPAID, PARTIAL, PAID
    -- FK References
    CustomerId          UNIQUEIDENTIFIER    NOT NULL,
    SalesOrderId        UNIQUEIDENTIFIER    NULL,                        -- Optional: invoice may not come from a sales order
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    BranchId            UNIQUEIDENTIFIER    NULL,
    -- Financial Fields
    SubTotal            DECIMAL(18,2)       NOT NULL DEFAULT 0,
    DiscountAmount      DECIMAL(18,2)       NOT NULL DEFAULT 0,
    TaxAmount           DECIMAL(18,2)       NOT NULL DEFAULT 0,
    ShippingCost        DECIMAL(18,2)       NOT NULL DEFAULT 0,
    TotalAmount         DECIMAL(18,2)       NOT NULL DEFAULT 0,
    PaidAmount          DECIMAL(18,2)       NOT NULL DEFAULT 0,
    BalanceDue          DECIMAL(18,2)       NOT NULL DEFAULT 0,
    -- Payment Info
    PaymentTerms        NVARCHAR(100)       NULL,                        -- e.g. Net 30, Net 60
    PaymentMethod       NVARCHAR(50)        NULL,                        -- CASH, BANK_TRANSFER, CHEQUE, CARD
    -- Address & Notes
    BillingAddress      NVARCHAR(500)       NULL,
    ShippingAddress     NVARCHAR(500)       NULL,
    Notes               NVARCHAR(1000)      NULL,
    TermsConditions     NVARCHAR(2000)      NULL,
    -- Reference
    ReferenceNo         NVARCHAR(100)       NULL,                        -- PO number or external ref
    -- Dates
    SentAt              DATETIME2           NULL,
    PaidAt              DATETIME2           NULL,
    CancelledAt         DATETIME2           NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_Invoices PRIMARY KEY (Id),
    CONSTRAINT UQ_Invoices_InvoiceNo UNIQUE (InvoiceNo),
    CONSTRAINT FK_Invoices_Customer FOREIGN KEY (CustomerId)
        REFERENCES Customers(Id),
    CONSTRAINT FK_Invoices_SalesOrder FOREIGN KEY (SalesOrderId)
        REFERENCES SalesOrders(Id),
    CONSTRAINT FK_Invoices_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_Invoices_Branch FOREIGN KEY (BranchId)
        REFERENCES Branches(Id),
    CONSTRAINT FK_Invoices_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_Invoices_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_Invoices_Type CHECK (InvoiceType IN ('SALES', 'CREDIT_NOTE', 'DEBIT_NOTE')),
    CONSTRAINT CHK_Invoices_Status CHECK (Status IN ('DRAFT', 'SENT', 'PAID', 'PARTIAL', 'OVERDUE', 'CANCELLED', 'VOID')),
    CONSTRAINT CHK_Invoices_PaymentStatus CHECK (PaymentStatus IN ('UNPAID', 'PARTIAL', 'PAID')),
    CONSTRAINT CHK_Invoices_Amounts CHECK (TotalAmount >= 0 AND PaidAmount >= 0 AND BalanceDue >= 0)
);
GO

-- =============================================
-- 2. InvoiceLines
-- =============================================
CREATE TABLE InvoiceLines (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    InvoiceId           UNIQUEIDENTIFIER    NOT NULL,
    ProductId           UNIQUEIDENTIFIER    NULL,                        -- NULL allows service/manual lines
    LineNumber          INT                 NOT NULL,
    Description         NVARCHAR(500)       NOT NULL,
    Quantity            DECIMAL(18,4)       NOT NULL DEFAULT 1,
    UnitPrice           DECIMAL(18,2)       NOT NULL DEFAULT 0,
    DiscountPercent     DECIMAL(5,2)        NOT NULL DEFAULT 0,
    DiscountAmount      DECIMAL(18,2)       NOT NULL DEFAULT 0,
    TaxPercent          DECIMAL(5,2)        NOT NULL DEFAULT 0,
    TaxAmount           DECIMAL(18,2)       NOT NULL DEFAULT 0,
    LineTotal           DECIMAL(18,2)       NOT NULL DEFAULT 0,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_InvoiceLines PRIMARY KEY (Id),
    CONSTRAINT FK_InvoiceLines_Invoice FOREIGN KEY (InvoiceId)
        REFERENCES Invoices(Id) ON DELETE CASCADE,
    CONSTRAINT FK_InvoiceLines_Product FOREIGN KEY (ProductId)
        REFERENCES Products(Id),
    CONSTRAINT CHK_InvoiceLines_Quantity CHECK (Quantity > 0),
    CONSTRAINT CHK_InvoiceLines_UnitPrice CHECK (UnitPrice >= 0)
);
GO

-- =============================================
-- 3. InvoicePayments
-- =============================================
CREATE TABLE InvoicePayments (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    InvoiceId           UNIQUEIDENTIFIER    NOT NULL,
    PaymentDate         DATE                NOT NULL,
    Amount              DECIMAL(18,2)       NOT NULL,
    PaymentMethod       NVARCHAR(50)        NOT NULL,                    -- CASH, BANK_TRANSFER, CHEQUE, CARD
    ReferenceNo         NVARCHAR(100)       NULL,                        -- Cheque no, transaction ID, etc.
    Notes               NVARCHAR(500)       NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_InvoicePayments PRIMARY KEY (Id),
    CONSTRAINT FK_InvoicePayments_Invoice FOREIGN KEY (InvoiceId)
        REFERENCES Invoices(Id) ON DELETE CASCADE,
    CONSTRAINT FK_InvoicePayments_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_InvoicePayments_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_InvoicePayments_Amount CHECK (Amount > 0),
    CONSTRAINT CHK_InvoicePayments_Method CHECK (PaymentMethod IN ('CASH', 'BANK_TRANSFER', 'CHEQUE', 'CARD', 'ONLINE'))
);
GO

-- =============================================
-- 4. INDEXES
-- =============================================

-- Invoices
CREATE INDEX IX_Invoices_CustomerId      ON Invoices(CustomerId);
CREATE INDEX IX_Invoices_SalesOrderId    ON Invoices(SalesOrderId);
CREATE INDEX IX_Invoices_CompanyId       ON Invoices(CompanyId);
CREATE INDEX IX_Invoices_BranchId        ON Invoices(BranchId);
CREATE INDEX IX_Invoices_Status          ON Invoices(Status);
CREATE INDEX IX_Invoices_PaymentStatus   ON Invoices(PaymentStatus);
CREATE INDEX IX_Invoices_InvoiceDate     ON Invoices(InvoiceDate);
CREATE INDEX IX_Invoices_DueDate         ON Invoices(DueDate);

-- InvoiceLines
CREATE INDEX IX_InvoiceLines_InvoiceId   ON InvoiceLines(InvoiceId);
CREATE INDEX IX_InvoiceLines_ProductId   ON InvoiceLines(ProductId);

-- InvoicePayments
CREATE INDEX IX_InvoicePayments_InvoiceId ON InvoicePayments(InvoiceId);
CREATE INDEX IX_InvoicePayments_Date      ON InvoicePayments(PaymentDate);
GO

-- =============================================
-- 5. TRIGGER: Auto-update BalanceDue on Invoices
--    when a payment is inserted/updated/deleted
-- =============================================
CREATE OR ALTER TRIGGER TR_InvoicePayments_UpdateBalance
ON InvoicePayments
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Collect all affected InvoiceIds
    DECLARE @AffectedIds TABLE (InvoiceId UNIQUEIDENTIFIER);

    INSERT INTO @AffectedIds SELECT InvoiceId FROM inserted;
    INSERT INTO @AffectedIds SELECT InvoiceId FROM deleted;

    -- Recalculate PaidAmount and BalanceDue
    UPDATE I
    SET
        PaidAmount    = ISNULL(P.TotalPaid, 0),
        BalanceDue    = I.TotalAmount - ISNULL(P.TotalPaid, 0),
        PaymentStatus = CASE
                            WHEN ISNULL(P.TotalPaid, 0) = 0                 THEN 'UNPAID'
                            WHEN ISNULL(P.TotalPaid, 0) >= I.TotalAmount    THEN 'PAID'
                            ELSE 'PARTIAL'
                        END,
        UpdatedAt     = GETUTCDATE()
    FROM Invoices I
    INNER JOIN @AffectedIds A ON I.Id = A.InvoiceId
    LEFT JOIN (
        SELECT InvoiceId, SUM(Amount) AS TotalPaid
        FROM InvoicePayments
        GROUP BY InvoiceId
    ) P ON I.Id = P.InvoiceId;
END;
GO

-- =============================================
-- 6. TRIGGER: Auto-update SubTotal/TotalAmount
--    on Invoices when lines change
-- =============================================
CREATE OR ALTER TRIGGER TR_InvoiceLines_UpdateTotals
ON InvoiceLines
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AffectedIds TABLE (InvoiceId UNIQUEIDENTIFIER);

    INSERT INTO @AffectedIds SELECT InvoiceId FROM inserted;
    INSERT INTO @AffectedIds SELECT InvoiceId FROM deleted;

    UPDATE I
    SET
        SubTotal      = ISNULL(L.SubTotal, 0),
        TaxAmount     = ISNULL(L.TaxTotal, 0),
        DiscountAmount= ISNULL(L.DiscountTotal, 0),
        TotalAmount   = ISNULL(L.SubTotal, 0) + ISNULL(L.TaxTotal, 0) - ISNULL(L.DiscountTotal, 0) + I.ShippingCost,
        BalanceDue    = (ISNULL(L.SubTotal, 0) + ISNULL(L.TaxTotal, 0) - ISNULL(L.DiscountTotal, 0) + I.ShippingCost) - I.PaidAmount,
        UpdatedAt     = GETUTCDATE()
    FROM Invoices I
    INNER JOIN @AffectedIds A ON I.Id = A.InvoiceId
    LEFT JOIN (
        SELECT
            InvoiceId,
            SUM(Quantity * UnitPrice)   AS SubTotal,
            SUM(TaxAmount)              AS TaxTotal,
            SUM(DiscountAmount)         AS DiscountTotal
        FROM InvoiceLines
        GROUP BY InvoiceId
    ) L ON I.Id = L.InvoiceId;
END;
GO

PRINT '017_InvoiceSchema.sql executed successfully.';
PRINT 'Tables created: Invoices, InvoiceLines, InvoicePayments';
PRINT 'Indexes: 11 indexes created';
PRINT 'Triggers: TR_InvoicePayments_UpdateBalance, TR_InvoiceLines_UpdateTotals';
GO
