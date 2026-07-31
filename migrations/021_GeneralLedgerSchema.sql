-- =============================================
-- ORBIT ERP - 021_GeneralLedgerSchema.sql
-- General Ledger Schema
-- Created: 2026
-- =============================================

USE OrbitERP;
GO

-- =============================================
-- 1. ChartOfAccounts
--    The master list of all GL accounts
-- =============================================
CREATE TABLE ChartOfAccounts (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    AccountCode         NVARCHAR(20)        NOT NULL,
    AccountName         NVARCHAR(200)       NOT NULL,
    AccountType         NVARCHAR(30)        NOT NULL,                    -- ASSET, LIABILITY, EQUITY, REVENUE, EXPENSE
    AccountSubType      NVARCHAR(50)        NULL,                        -- e.g. CURRENT_ASSET, FIXED_ASSET, CURRENT_LIABILITY
    NormalBalance       NVARCHAR(10)        NOT NULL DEFAULT 'DEBIT',    -- DEBIT, CREDIT
    ParentAccountId     UNIQUEIDENTIFIER    NULL,                        -- Self-referencing for account hierarchy
    Level               INT                 NOT NULL DEFAULT 1,          -- 1=Category, 2=Group, 3=Account
    IsHeader            BIT                 NOT NULL DEFAULT 0,          -- Header accounts cannot post transactions
    IsActive            BIT                 NOT NULL DEFAULT 1,
    IsSystemAccount     BIT                 NOT NULL DEFAULT 0,          -- System accounts cannot be deleted
    OpeningBalance      DECIMAL(18,2)       NOT NULL DEFAULT 0,
    CurrentBalance      DECIMAL(18,2)       NOT NULL DEFAULT 0,
    Description         NVARCHAR(500)       NULL,
    -- FK
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_ChartOfAccounts PRIMARY KEY (Id),
    CONSTRAINT UQ_ChartOfAccounts_Code_Company UNIQUE (AccountCode, CompanyId),
    CONSTRAINT FK_COA_ParentAccount FOREIGN KEY (ParentAccountId)
        REFERENCES ChartOfAccounts(Id),
    CONSTRAINT FK_COA_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_COA_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_COA_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_COA_AccountType CHECK (AccountType IN (
        'ASSET', 'LIABILITY', 'EQUITY', 'REVENUE', 'EXPENSE'
    )),
    CONSTRAINT CHK_COA_NormalBalance CHECK (NormalBalance IN ('DEBIT', 'CREDIT')),
    CONSTRAINT CHK_COA_Level CHECK (Level BETWEEN 1 AND 5)
);
GO

-- =============================================
-- 2. FiscalYears
-- =============================================
CREATE TABLE FiscalYears (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    FiscalYearName      NVARCHAR(50)        NOT NULL,                    -- e.g. FY-2026
    StartDate           DATE                NOT NULL,
    EndDate             DATE                NOT NULL,
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'OPEN',     -- OPEN, CLOSED, LOCKED
    IsCurrent           BIT                 NOT NULL DEFAULT 0,
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_FiscalYears PRIMARY KEY (Id),
    CONSTRAINT UQ_FiscalYears_Name_Company UNIQUE (FiscalYearName, CompanyId),
    CONSTRAINT FK_FiscalYears_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_FiscalYears_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_FiscalYears_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_FiscalYears_Status CHECK (Status IN ('OPEN', 'CLOSED', 'LOCKED')),
    CONSTRAINT CHK_FiscalYears_Dates CHECK (EndDate > StartDate)
);
GO

-- =============================================
-- 3. AccountingPeriods
-- =============================================
CREATE TABLE AccountingPeriods (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    FiscalYearId        UNIQUEIDENTIFIER    NOT NULL,
    PeriodName          NVARCHAR(50)        NOT NULL,                    -- e.g. January 2026
    PeriodNumber        INT                 NOT NULL,                    -- 1-12
    StartDate           DATE                NOT NULL,
    EndDate             DATE                NOT NULL,
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'OPEN',     -- OPEN, CLOSED, LOCKED
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_AccountingPeriods PRIMARY KEY (Id),
    CONSTRAINT FK_AccountingPeriods_FiscalYear FOREIGN KEY (FiscalYearId)
        REFERENCES FiscalYears(Id),
    CONSTRAINT FK_AccountingPeriods_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT CHK_AccountingPeriods_Status CHECK (Status IN ('OPEN', 'CLOSED', 'LOCKED')),
    CONSTRAINT CHK_AccountingPeriods_PeriodNo CHECK (PeriodNumber BETWEEN 1 AND 13),
    CONSTRAINT CHK_AccountingPeriods_Dates CHECK (EndDate > StartDate)
);
GO

-- =============================================
-- 4. JournalEntries
--    Header for each double-entry transaction
-- =============================================
CREATE TABLE JournalEntries (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    JournalNumber       NVARCHAR(50)        NOT NULL,
    EntryDate           DATE                NOT NULL DEFAULT CAST(GETUTCDATE() AS DATE),
    PostingDate         DATE                NULL,
    FiscalYearId        UNIQUEIDENTIFIER    NOT NULL,
    AccountingPeriodId  UNIQUEIDENTIFIER    NOT NULL,
    EntryType           NVARCHAR(30)        NOT NULL DEFAULT 'MANUAL',   -- MANUAL, SALES, PURCHASE, PAYMENT, RECEIPT, ADJUSTMENT, OPENING, CLOSING
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'DRAFT',    -- DRAFT, POSTED, REVERSED, CANCELLED
    Description         NVARCHAR(500)       NOT NULL,
    ReferenceType       NVARCHAR(50)        NULL,                        -- INVOICE, PURCHASE_ORDER, PAYMENT, etc.
    ReferenceId         UNIQUEIDENTIFIER    NULL,                        -- ID of the source document
    ReferenceNo         NVARCHAR(100)       NULL,
    TotalDebit          DECIMAL(18,2)       NOT NULL DEFAULT 0,
    TotalCredit         DECIMAL(18,2)       NOT NULL DEFAULT 0,
    IsBalanced          BIT                 NOT NULL DEFAULT 0,          -- Debit = Credit
    IsReversed          BIT                 NOT NULL DEFAULT 0,
    ReversedByJEId      UNIQUEIDENTIFIER    NULL,                        -- Points to reversal journal entry
    -- FK
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    BranchId            UNIQUEIDENTIFIER    NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,
    PostedBy            UNIQUEIDENTIFIER    NULL,
    PostedAt            DATETIME2           NULL,

    CONSTRAINT PK_JournalEntries PRIMARY KEY (Id),
    CONSTRAINT UQ_JournalEntries_JournalNumber UNIQUE (JournalNumber),
    CONSTRAINT FK_JE_FiscalYear FOREIGN KEY (FiscalYearId)
        REFERENCES FiscalYears(Id),
    CONSTRAINT FK_JE_AccountingPeriod FOREIGN KEY (AccountingPeriodId)
        REFERENCES AccountingPeriods(Id),
    CONSTRAINT FK_JE_ReversedBy FOREIGN KEY (ReversedByJEId)
        REFERENCES JournalEntries(Id),
    CONSTRAINT FK_JE_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_JE_Branch FOREIGN KEY (BranchId)
        REFERENCES Branches(Id),
    CONSTRAINT FK_JE_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_JE_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_JE_PostedBy FOREIGN KEY (PostedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_JE_EntryType CHECK (EntryType IN (
        'MANUAL', 'SALES', 'PURCHASE', 'PAYMENT', 'RECEIPT', 'ADJUSTMENT', 'OPENING', 'CLOSING'
    )),
    CONSTRAINT CHK_JE_Status CHECK (Status IN (
        'DRAFT', 'POSTED', 'REVERSED', 'CANCELLED'
    ))
);
GO

-- =============================================
-- 5. JournalEntryLines
--    Each debit/credit line of a journal entry
-- =============================================
CREATE TABLE JournalEntryLines (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    JournalEntryId      UNIQUEIDENTIFIER    NOT NULL,
    AccountId           UNIQUEIDENTIFIER    NOT NULL,                    -- ChartOfAccounts
    LineNumber          INT                 NOT NULL,
    Description         NVARCHAR(500)       NULL,
    DebitAmount         DECIMAL(18,2)       NOT NULL DEFAULT 0,
    CreditAmount        DECIMAL(18,2)       NOT NULL DEFAULT 0,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_JournalEntryLines PRIMARY KEY (Id),
    CONSTRAINT FK_JELines_JournalEntry FOREIGN KEY (JournalEntryId)
        REFERENCES JournalEntries(Id) ON DELETE CASCADE,
    CONSTRAINT FK_JELines_Account FOREIGN KEY (AccountId)
        REFERENCES ChartOfAccounts(Id),
    CONSTRAINT CHK_JELines_Amounts CHECK (
        DebitAmount >= 0 AND CreditAmount >= 0
        AND NOT (DebitAmount > 0 AND CreditAmount > 0)  -- a line cannot be both debit AND credit
    ),
    CONSTRAINT CHK_JELines_LineNumber CHECK (LineNumber > 0)
);
GO

-- =============================================
-- 6. INDEXES
-- =============================================

-- ChartOfAccounts
CREATE INDEX IX_COA_CompanyId          ON ChartOfAccounts(CompanyId);
CREATE INDEX IX_COA_AccountType        ON ChartOfAccounts(AccountType);
CREATE INDEX IX_COA_ParentAccountId    ON ChartOfAccounts(ParentAccountId);
CREATE INDEX IX_COA_IsActive           ON ChartOfAccounts(IsActive);

-- FiscalYears
CREATE INDEX IX_FiscalYears_CompanyId  ON FiscalYears(CompanyId);
CREATE INDEX IX_FiscalYears_IsCurrent  ON FiscalYears(IsCurrent);

-- AccountingPeriods
CREATE INDEX IX_AccPeriods_FiscalYearId ON AccountingPeriods(FiscalYearId);
CREATE INDEX IX_AccPeriods_CompanyId    ON AccountingPeriods(CompanyId);
CREATE INDEX IX_AccPeriods_Status       ON AccountingPeriods(Status);

-- JournalEntries
CREATE INDEX IX_JE_CompanyId           ON JournalEntries(CompanyId);
CREATE INDEX IX_JE_FiscalYearId        ON JournalEntries(FiscalYearId);
CREATE INDEX IX_JE_AccountingPeriodId  ON JournalEntries(AccountingPeriodId);
CREATE INDEX IX_JE_EntryDate           ON JournalEntries(EntryDate);
CREATE INDEX IX_JE_Status              ON JournalEntries(Status);
CREATE INDEX IX_JE_EntryType           ON JournalEntries(EntryType);
CREATE INDEX IX_JE_ReferenceId         ON JournalEntries(ReferenceId);

-- JournalEntryLines
CREATE INDEX IX_JELines_JournalEntryId ON JournalEntryLines(JournalEntryId);
CREATE INDEX IX_JELines_AccountId      ON JournalEntryLines(AccountId);
GO

-- =============================================
-- 7. TRIGGER: Auto-update JE totals and IsBalanced
-- =============================================
CREATE OR ALTER TRIGGER TR_JELines_UpdateTotals
ON JournalEntryLines
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AffectedIds TABLE (JEId UNIQUEIDENTIFIER);

    INSERT INTO @AffectedIds SELECT JournalEntryId FROM inserted;
    INSERT INTO @AffectedIds SELECT JournalEntryId FROM deleted;

    UPDATE JE
    SET
        TotalDebit  = ISNULL(L.TotalDr, 0),
        TotalCredit = ISNULL(L.TotalCr, 0),
        IsBalanced  = CASE
                        WHEN ISNULL(L.TotalDr, 0) = ISNULL(L.TotalCr, 0)
                             AND ISNULL(L.TotalDr, 0) > 0
                        THEN 1 ELSE 0
                      END,
        UpdatedAt   = GETUTCDATE()
    FROM JournalEntries JE
    INNER JOIN @AffectedIds A ON JE.Id = A.JEId
    LEFT JOIN (
        SELECT
            JournalEntryId,
            SUM(DebitAmount)  AS TotalDr,
            SUM(CreditAmount) AS TotalCr
        FROM JournalEntryLines
        GROUP BY JournalEntryId
    ) L ON JE.Id = L.JournalEntryId;
END;
GO

-- =============================================
-- 8. TRIGGER: Update COA CurrentBalance on JE post
-- =============================================
CREATE OR ALTER TRIGGER TR_JELines_UpdateAccountBalance
ON JournalEntryLines
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AffectedAccounts TABLE (AccountId UNIQUEIDENTIFIER);

    INSERT INTO @AffectedAccounts SELECT AccountId FROM inserted;
    INSERT INTO @AffectedAccounts SELECT AccountId FROM deleted;

    UPDATE COA
    SET
        CurrentBalance = COA.OpeningBalance +
            CASE COA.NormalBalance
                WHEN 'DEBIT'  THEN ISNULL(B.TotalDebit, 0)  - ISNULL(B.TotalCredit, 0)
                WHEN 'CREDIT' THEN ISNULL(B.TotalCredit, 0) - ISNULL(B.TotalDebit, 0)
                ELSE 0
            END,
        UpdatedAt = GETUTCDATE()
    FROM ChartOfAccounts COA
    INNER JOIN @AffectedAccounts A ON COA.Id = A.AccountId
    LEFT JOIN (
        SELECT
            JEL.AccountId,
            SUM(JEL.DebitAmount)  AS TotalDebit,
            SUM(JEL.CreditAmount) AS TotalCredit
        FROM JournalEntryLines JEL
        INNER JOIN JournalEntries JE ON JEL.JournalEntryId = JE.Id
        WHERE JE.Status = 'POSTED'
        GROUP BY JEL.AccountId
    ) B ON COA.Id = B.AccountId;
END;
GO

-- =============================================
-- 9. SEED: Standard Chart of Accounts
-- =============================================
DECLARE @CompanyId UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM Companies ORDER BY CreatedAt);

IF @CompanyId IS NOT NULL
BEGIN
    INSERT INTO ChartOfAccounts (AccountCode, AccountName, AccountType, NormalBalance, Level, IsHeader, IsSystemAccount, CompanyId)
    VALUES
    -- ASSETS
    ('1000', 'Assets',                      'ASSET',     'DEBIT',  1, 1, 1, @CompanyId),
    ('1100', 'Current Assets',              'ASSET',     'DEBIT',  2, 1, 1, @CompanyId),
    ('1110', 'Cash and Cash Equivalents',   'ASSET',     'DEBIT',  3, 0, 1, @CompanyId),
    ('1120', 'Accounts Receivable',         'ASSET',     'DEBIT',  3, 0, 1, @CompanyId),
    ('1130', 'Inventory',                   'ASSET',     'DEBIT',  3, 0, 1, @CompanyId),
    ('1140', 'Prepaid Expenses',            'ASSET',     'DEBIT',  3, 0, 0, @CompanyId),
    ('1200', 'Non-Current Assets',          'ASSET',     'DEBIT',  2, 1, 1, @CompanyId),
    ('1210', 'Property Plant & Equipment',  'ASSET',     'DEBIT',  3, 0, 0, @CompanyId),
    ('1220', 'Accumulated Depreciation',    'ASSET',     'CREDIT', 3, 0, 0, @CompanyId),
    -- LIABILITIES
    ('2000', 'Liabilities',                 'LIABILITY', 'CREDIT', 1, 1, 1, @CompanyId),
    ('2100', 'Current Liabilities',         'LIABILITY', 'CREDIT', 2, 1, 1, @CompanyId),
    ('2110', 'Accounts Payable',            'LIABILITY', 'CREDIT', 3, 0, 1, @CompanyId),
    ('2120', 'Accrued Liabilities',         'LIABILITY', 'CREDIT', 3, 0, 0, @CompanyId),
    ('2130', 'Tax Payable',                 'LIABILITY', 'CREDIT', 3, 0, 0, @CompanyId),
    ('2140', 'Salaries Payable',            'LIABILITY', 'CREDIT', 3, 0, 0, @CompanyId),
    ('2200', 'Non-Current Liabilities',     'LIABILITY', 'CREDIT', 2, 1, 0, @CompanyId),
    ('2210', 'Long-term Loans',             'LIABILITY', 'CREDIT', 3, 0, 0, @CompanyId),
    -- EQUITY
    ('3000', 'Equity',                      'EQUITY',   'CREDIT', 1, 1, 1, @CompanyId),
    ('3100', 'Share Capital',               'EQUITY',   'CREDIT', 2, 0, 1, @CompanyId),
    ('3200', 'Retained Earnings',           'EQUITY',   'CREDIT', 2, 0, 1, @CompanyId),
    ('3300', 'Current Year Profit/Loss',    'EQUITY',   'CREDIT', 2, 0, 1, @CompanyId),
    -- REVENUE
    ('4000', 'Revenue',                     'REVENUE',  'CREDIT', 1, 1, 1, @CompanyId),
    ('4100', 'Sales Revenue',               'REVENUE',  'CREDIT', 2, 0, 1, @CompanyId),
    ('4200', 'Service Revenue',             'REVENUE',  'CREDIT', 2, 0, 0, @CompanyId),
    ('4300', 'Other Income',                'REVENUE',  'CREDIT', 2, 0, 0, @CompanyId),
    -- EXPENSES
    ('5000', 'Expenses',                    'EXPENSE',  'DEBIT',  1, 1, 1, @CompanyId),
    ('5100', 'Cost of Goods Sold',          'EXPENSE',  'DEBIT',  2, 0, 1, @CompanyId),
    ('5200', 'Salaries & Wages',            'EXPENSE',  'DEBIT',  2, 0, 0, @CompanyId),
    ('5300', 'Rent Expense',                'EXPENSE',  'DEBIT',  2, 0, 0, @CompanyId),
    ('5400', 'Utilities Expense',           'EXPENSE',  'DEBIT',  2, 0, 0, @CompanyId),
    ('5500', 'Depreciation Expense',        'EXPENSE',  'DEBIT',  2, 0, 0, @CompanyId),
    ('5600', 'Tax Expense',                 'EXPENSE',  'DEBIT',  2, 0, 0, @CompanyId),
    ('5700', 'Bank Charges',                'EXPENSE',  'DEBIT',  2, 0, 0, @CompanyId),
    ('5800', 'Other Expenses',              'EXPENSE',  'DEBIT',  2, 0, 0, @CompanyId);

    PRINT 'Chart of Accounts seed data inserted successfully.';
END
ELSE
BEGIN
    PRINT 'WARNING: No company found — Chart of Accounts seed skipped.';
END
GO

PRINT '021_GeneralLedgerSchema.sql executed successfully.';
PRINT 'Tables created: ChartOfAccounts, FiscalYears, AccountingPeriods, JournalEntries, JournalEntryLines';
PRINT 'Indexes: 18 indexes created';
PRINT 'Triggers: TR_JELines_UpdateTotals, TR_JELines_UpdateAccountBalance';
PRINT 'Seed: Standard Chart of Accounts (33 accounts)';
GO
