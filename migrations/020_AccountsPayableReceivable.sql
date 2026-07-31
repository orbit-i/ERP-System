-- =============================================
-- ORBIT ERP - 020_AccountsPayableReceivable.sql
-- Accounts Payable & Receivable Schema
-- Created: 2026
-- =============================================

USE OrbitERP;
GO

-- =============================================
-- 1. AccountsPayable
--    Tracks what the company OWES to vendors
-- =============================================
CREATE TABLE AccountsPayable (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    APNumber            NVARCHAR(50)        NOT NULL,
    VendorId            UNIQUEIDENTIFIER    NOT NULL,
    PurchaseOrderId     UNIQUEIDENTIFIER    NULL,
    GRNId               UNIQUEIDENTIFIER    NULL,
    TransactionType     NVARCHAR(30)        NOT NULL DEFAULT 'INVOICE',  -- INVOICE, CREDIT_NOTE, DEBIT_NOTE, ADVANCE
    TransactionDate     DATE                NOT NULL DEFAULT CAST(GETUTCDATE() AS DATE),
    DueDate             DATE                NOT NULL,
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'OPEN',     -- OPEN, PARTIAL, PAID, OVERDUE, CANCELLED, DISPUTED
    -- Amounts
    OriginalAmount      DECIMAL(18,2)       NOT NULL DEFAULT 0,
    PaidAmount          DECIMAL(18,2)       NOT NULL DEFAULT 0,
    BalanceDue          DECIMAL(18,2)       NOT NULL DEFAULT 0,
    -- Reference
    VendorInvoiceNo     NVARCHAR(100)       NULL,
    ReferenceNo         NVARCHAR(100)       NULL,
    Notes               NVARCHAR(1000)      NULL,
    -- FK
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    BranchId            UNIQUEIDENTIFIER    NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_AccountsPayable PRIMARY KEY (Id),
    CONSTRAINT UQ_AccountsPayable_APNumber UNIQUE (APNumber),
    CONSTRAINT FK_AP_Vendor FOREIGN KEY (VendorId)
        REFERENCES Vendors(Id),
    CONSTRAINT FK_AP_PurchaseOrder FOREIGN KEY (PurchaseOrderId)
        REFERENCES PurchaseOrders(Id),
    CONSTRAINT FK_AP_GRN FOREIGN KEY (GRNId)
        REFERENCES GoodsReceivingNotes(Id),
    CONSTRAINT FK_AP_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_AP_Branch FOREIGN KEY (BranchId)
        REFERENCES Branches(Id),
    CONSTRAINT FK_AP_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_AP_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_AP_TransactionType CHECK (TransactionType IN (
        'INVOICE', 'CREDIT_NOTE', 'DEBIT_NOTE', 'ADVANCE'
    )),
    CONSTRAINT CHK_AP_Status CHECK (Status IN (
        'OPEN', 'PARTIAL', 'PAID', 'OVERDUE', 'CANCELLED', 'DISPUTED'
    )),
    CONSTRAINT CHK_AP_Amounts CHECK (
        OriginalAmount >= 0 AND PaidAmount >= 0 AND BalanceDue >= 0
    )
);
GO

-- =============================================
-- 2. AccountsPayablePayments
--    Individual payment records against AP
-- =============================================
CREATE TABLE AccountsPayablePayments (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    APId                UNIQUEIDENTIFIER    NOT NULL,
    PaymentDate         DATE                NOT NULL DEFAULT CAST(GETUTCDATE() AS DATE),
    Amount              DECIMAL(18,2)       NOT NULL,
    PaymentMethod       NVARCHAR(50)        NOT NULL DEFAULT 'BANK_TRANSFER',
    ReferenceNo         NVARCHAR(100)       NULL,
    BankName            NVARCHAR(200)       NULL,
    ChequeNo            NVARCHAR(100)       NULL,
    Notes               NVARCHAR(500)       NULL,
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'COMPLETED', -- COMPLETED, PENDING, FAILED, REVERSED
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_AccountsPayablePayments PRIMARY KEY (Id),
    CONSTRAINT FK_APPayments_AP FOREIGN KEY (APId)
        REFERENCES AccountsPayable(Id) ON DELETE CASCADE,
    CONSTRAINT FK_APPayments_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_APPayments_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_APPayments_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_APPayments_Amount CHECK (Amount > 0),
    CONSTRAINT CHK_APPayments_Method CHECK (PaymentMethod IN (
        'CASH', 'BANK_TRANSFER', 'CHEQUE', 'CARD', 'ONLINE'
    )),
    CONSTRAINT CHK_APPayments_Status CHECK (Status IN (
        'COMPLETED', 'PENDING', 'FAILED', 'REVERSED'
    ))
);
GO

-- =============================================
-- 3. AccountsReceivable
--    Tracks what customers OWE the company
-- =============================================
CREATE TABLE AccountsReceivable (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    ARNumber            NVARCHAR(50)        NOT NULL,
    CustomerId          UNIQUEIDENTIFIER    NOT NULL,
    InvoiceId           UNIQUEIDENTIFIER    NULL,
    SalesOrderId        UNIQUEIDENTIFIER    NULL,
    TransactionType     NVARCHAR(30)        NOT NULL DEFAULT 'INVOICE',  -- INVOICE, CREDIT_NOTE, DEBIT_NOTE, ADVANCE
    TransactionDate     DATE                NOT NULL DEFAULT CAST(GETUTCDATE() AS DATE),
    DueDate             DATE                NOT NULL,
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'OPEN',     -- OPEN, PARTIAL, PAID, OVERDUE, CANCELLED, DISPUTED
    -- Amounts
    OriginalAmount      DECIMAL(18,2)       NOT NULL DEFAULT 0,
    PaidAmount          DECIMAL(18,2)       NOT NULL DEFAULT 0,
    BalanceDue          DECIMAL(18,2)       NOT NULL DEFAULT 0,
    -- Reference
    ReferenceNo         NVARCHAR(100)       NULL,
    Notes               NVARCHAR(1000)      NULL,
    -- FK
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    BranchId            UNIQUEIDENTIFIER    NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_AccountsReceivable PRIMARY KEY (Id),
    CONSTRAINT UQ_AccountsReceivable_ARNumber UNIQUE (ARNumber),
    CONSTRAINT FK_AR_Customer FOREIGN KEY (CustomerId)
        REFERENCES Customers(Id),
    CONSTRAINT FK_AR_Invoice FOREIGN KEY (InvoiceId)
        REFERENCES Invoices(Id),
    CONSTRAINT FK_AR_SalesOrder FOREIGN KEY (SalesOrderId)
        REFERENCES SalesOrders(Id),
    CONSTRAINT FK_AR_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_AR_Branch FOREIGN KEY (BranchId)
        REFERENCES Branches(Id),
    CONSTRAINT FK_AR_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_AR_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_AR_TransactionType CHECK (TransactionType IN (
        'INVOICE', 'CREDIT_NOTE', 'DEBIT_NOTE', 'ADVANCE'
    )),
    CONSTRAINT CHK_AR_Status CHECK (Status IN (
        'OPEN', 'PARTIAL', 'PAID', 'OVERDUE', 'CANCELLED', 'DISPUTED'
    )),
    CONSTRAINT CHK_AR_Amounts CHECK (
        OriginalAmount >= 0 AND PaidAmount >= 0 AND BalanceDue >= 0
    )
);
GO

-- =============================================
-- 4. AccountsReceivablePayments
--    Individual payment records against AR
-- =============================================
CREATE TABLE AccountsReceivablePayments (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    ARId                UNIQUEIDENTIFIER    NOT NULL,
    PaymentDate         DATE                NOT NULL DEFAULT CAST(GETUTCDATE() AS DATE),
    Amount              DECIMAL(18,2)       NOT NULL,
    PaymentMethod       NVARCHAR(50)        NOT NULL DEFAULT 'BANK_TRANSFER',
    ReferenceNo         NVARCHAR(100)       NULL,
    BankName            NVARCHAR(200)       NULL,
    ChequeNo            NVARCHAR(100)       NULL,
    Notes               NVARCHAR(500)       NULL,
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'COMPLETED', -- COMPLETED, PENDING, FAILED, REVERSED
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_AccountsReceivablePayments PRIMARY KEY (Id),
    CONSTRAINT FK_ARPayments_AR FOREIGN KEY (ARId)
        REFERENCES AccountsReceivable(Id) ON DELETE CASCADE,
    CONSTRAINT FK_ARPayments_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_ARPayments_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_ARPayments_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_ARPayments_Amount CHECK (Amount > 0),
    CONSTRAINT CHK_ARPayments_Method CHECK (PaymentMethod IN (
        'CASH', 'BANK_TRANSFER', 'CHEQUE', 'CARD', 'ONLINE'
    )),
    CONSTRAINT CHK_ARPayments_Status CHECK (Status IN (
        'COMPLETED', 'PENDING', 'FAILED', 'REVERSED'
    ))
);
GO

-- =============================================
-- 5. INDEXES
-- =============================================

-- AccountsPayable
CREATE INDEX IX_AP_VendorId            ON AccountsPayable(VendorId);
CREATE INDEX IX_AP_PurchaseOrderId     ON AccountsPayable(PurchaseOrderId);
CREATE INDEX IX_AP_CompanyId           ON AccountsPayable(CompanyId);
CREATE INDEX IX_AP_Status              ON AccountsPayable(Status);
CREATE INDEX IX_AP_DueDate             ON AccountsPayable(DueDate);
CREATE INDEX IX_AP_TransactionDate     ON AccountsPayable(TransactionDate);

-- AccountsPayablePayments
CREATE INDEX IX_APPayments_APId        ON AccountsPayablePayments(APId);
CREATE INDEX IX_APPayments_PaymentDate ON AccountsPayablePayments(PaymentDate);
CREATE INDEX IX_APPayments_CompanyId   ON AccountsPayablePayments(CompanyId);

-- AccountsReceivable
CREATE INDEX IX_AR_CustomerId          ON AccountsReceivable(CustomerId);
CREATE INDEX IX_AR_InvoiceId           ON AccountsReceivable(InvoiceId);
CREATE INDEX IX_AR_CompanyId           ON AccountsReceivable(CompanyId);
CREATE INDEX IX_AR_Status              ON AccountsReceivable(Status);
CREATE INDEX IX_AR_DueDate             ON AccountsReceivable(DueDate);
CREATE INDEX IX_AR_TransactionDate     ON AccountsReceivable(TransactionDate);

-- AccountsReceivablePayments
CREATE INDEX IX_ARPayments_ARId        ON AccountsReceivablePayments(ARId);
CREATE INDEX IX_ARPayments_PaymentDate ON AccountsReceivablePayments(PaymentDate);
CREATE INDEX IX_ARPayments_CompanyId   ON AccountsReceivablePayments(CompanyId);
GO

-- =============================================
-- 6. TRIGGER: Auto-update AP BalanceDue
-- =============================================
CREATE OR ALTER TRIGGER TR_APPayments_UpdateBalance
ON AccountsPayablePayments
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AffectedIds TABLE (APId UNIQUEIDENTIFIER);

    INSERT INTO @AffectedIds SELECT APId FROM inserted;
    INSERT INTO @AffectedIds SELECT APId FROM deleted;

    UPDATE AP
    SET
        PaidAmount  = ISNULL(P.TotalPaid, 0),
        BalanceDue  = AP.OriginalAmount - ISNULL(P.TotalPaid, 0),
        Status      = CASE
                        WHEN ISNULL(P.TotalPaid, 0) = 0                     THEN 'OPEN'
                        WHEN ISNULL(P.TotalPaid, 0) >= AP.OriginalAmount    THEN 'PAID'
                        ELSE 'PARTIAL'
                      END,
        UpdatedAt   = GETUTCDATE()
    FROM AccountsPayable AP
    INNER JOIN @AffectedIds A ON AP.Id = A.APId
    LEFT JOIN (
        SELECT APId, SUM(Amount) AS TotalPaid
        FROM AccountsPayablePayments
        WHERE Status = 'COMPLETED'
        GROUP BY APId
    ) P ON AP.Id = P.APId;
END;
GO

-- =============================================
-- 7. TRIGGER: Auto-update AR BalanceDue
-- =============================================
CREATE OR ALTER TRIGGER TR_ARPayments_UpdateBalance
ON AccountsReceivablePayments
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AffectedIds TABLE (ARId UNIQUEIDENTIFIER);

    INSERT INTO @AffectedIds SELECT ARId FROM inserted;
    INSERT INTO @AffectedIds SELECT ARId FROM deleted;

    UPDATE AR
    SET
        PaidAmount  = ISNULL(P.TotalPaid, 0),
        BalanceDue  = AR.OriginalAmount - ISNULL(P.TotalPaid, 0),
        Status      = CASE
                        WHEN ISNULL(P.TotalPaid, 0) = 0                     THEN 'OPEN'
                        WHEN ISNULL(P.TotalPaid, 0) >= AR.OriginalAmount    THEN 'PAID'
                        ELSE 'PARTIAL'
                      END,
        UpdatedAt   = GETUTCDATE()
    FROM AccountsReceivable AR
    INNER JOIN @AffectedIds A ON AR.Id = A.ARId
    LEFT JOIN (
        SELECT ARId, SUM(Amount) AS TotalPaid
        FROM AccountsReceivablePayments
        WHERE Status = 'COMPLETED'
        GROUP BY ARId
    ) P ON AR.Id = P.ARId;
END;
GO

PRINT '020_AccountsPayableReceivable.sql executed successfully.';
PRINT 'Tables created: AccountsPayable, AccountsPayablePayments, AccountsReceivable, AccountsReceivablePayments';
PRINT 'Indexes: 18 indexes created';
PRINT 'Triggers: TR_APPayments_UpdateBalance, TR_ARPayments_UpdateBalance';
GO
