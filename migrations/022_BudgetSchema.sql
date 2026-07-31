-- =============================================
-- ORBIT ERP - 022_BudgetSchema.sql
-- Budget Schema
-- Created: 2026
-- =============================================

USE OrbitERP;
GO

-- =============================================
-- 1. Budgets
--    Budget header per fiscal year
-- =============================================
CREATE TABLE Budgets (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    BudgetName          NVARCHAR(200)       NOT NULL,
    BudgetCode          NVARCHAR(50)        NOT NULL,
    FiscalYearId        UNIQUEIDENTIFIER    NOT NULL,
    BudgetType          NVARCHAR(30)        NOT NULL DEFAULT 'ANNUAL',   -- ANNUAL, QUARTERLY, MONTHLY, PROJECT
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'DRAFT',    -- DRAFT, SUBMITTED, APPROVED, ACTIVE, CLOSED, REJECTED
    TotalBudgetAmount   DECIMAL(18,2)       NOT NULL DEFAULT 0,
    TotalActualAmount   DECIMAL(18,2)       NOT NULL DEFAULT 0,
    TotalVariance       DECIMAL(18,2)       NOT NULL DEFAULT 0,          -- Budget - Actual
    Description         NVARCHAR(1000)      NULL,
    StartDate           DATE                NOT NULL,
    EndDate             DATE                NOT NULL,
    ApprovedBy          UNIQUEIDENTIFIER    NULL,
    ApprovedAt          DATETIME2           NULL,
    RejectedBy          UNIQUEIDENTIFIER    NULL,
    RejectedAt          DATETIME2           NULL,
    RejectionReason     NVARCHAR(500)       NULL,
    -- FK
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    BranchId            UNIQUEIDENTIFIER    NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_Budgets PRIMARY KEY (Id),
    CONSTRAINT UQ_Budgets_Code_Company UNIQUE (BudgetCode, CompanyId),
    CONSTRAINT FK_Budgets_FiscalYear FOREIGN KEY (FiscalYearId)
        REFERENCES FiscalYears(Id),
    CONSTRAINT FK_Budgets_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_Budgets_Branch FOREIGN KEY (BranchId)
        REFERENCES Branches(Id),
    CONSTRAINT FK_Budgets_ApprovedBy FOREIGN KEY (ApprovedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_Budgets_RejectedBy FOREIGN KEY (RejectedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_Budgets_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_Budgets_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_Budgets_Type CHECK (BudgetType IN (
        'ANNUAL', 'QUARTERLY', 'MONTHLY', 'PROJECT'
    )),
    CONSTRAINT CHK_Budgets_Status CHECK (Status IN (
        'DRAFT', 'SUBMITTED', 'APPROVED', 'ACTIVE', 'CLOSED', 'REJECTED'
    )),
    CONSTRAINT CHK_Budgets_Dates CHECK (EndDate > StartDate)
);
GO

-- =============================================
-- 2. BudgetLines
--    One line per GL account per period
-- =============================================
CREATE TABLE BudgetLines (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    BudgetId            UNIQUEIDENTIFIER    NOT NULL,
    AccountId           UNIQUEIDENTIFIER    NOT NULL,                    -- ChartOfAccounts
    AccountingPeriodId  UNIQUEIDENTIFIER    NULL,                        -- NULL = annual line, not period-specific
    LineDescription     NVARCHAR(500)       NULL,
    BudgetedAmount      DECIMAL(18,2)       NOT NULL DEFAULT 0,
    ActualAmount        DECIMAL(18,2)       NOT NULL DEFAULT 0,
    Variance            DECIMAL(18,2)       NOT NULL DEFAULT 0,          -- Budgeted - Actual
    VariancePercent     DECIMAL(8,2)        NOT NULL DEFAULT 0,
    Notes               NVARCHAR(500)       NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_BudgetLines PRIMARY KEY (Id),
    CONSTRAINT FK_BudgetLines_Budget FOREIGN KEY (BudgetId)
        REFERENCES Budgets(Id) ON DELETE CASCADE,
    CONSTRAINT FK_BudgetLines_Account FOREIGN KEY (AccountId)
        REFERENCES ChartOfAccounts(Id),
    CONSTRAINT FK_BudgetLines_Period FOREIGN KEY (AccountingPeriodId)
        REFERENCES AccountingPeriods(Id),
    CONSTRAINT CHK_BudgetLines_BudgetedAmount CHECK (BudgetedAmount >= 0),
    CONSTRAINT CHK_BudgetLines_ActualAmount CHECK (ActualAmount >= 0)
);
GO

-- =============================================
-- 3. BudgetRevisions
--    Track every change made to a budget
-- =============================================
CREATE TABLE BudgetRevisions (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    BudgetId            UNIQUEIDENTIFIER    NOT NULL,
    RevisionNumber      INT                 NOT NULL,
    RevisionDate        DATE                NOT NULL DEFAULT CAST(GETUTCDATE() AS DATE),
    PreviousAmount      DECIMAL(18,2)       NOT NULL DEFAULT 0,
    RevisedAmount       DECIMAL(18,2)       NOT NULL DEFAULT 0,
    Reason              NVARCHAR(500)       NOT NULL,
    ApprovedBy          UNIQUEIDENTIFIER    NULL,
    ApprovedAt          DATETIME2           NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_BudgetRevisions PRIMARY KEY (Id),
    CONSTRAINT FK_BudgetRevisions_Budget FOREIGN KEY (BudgetId)
        REFERENCES Budgets(Id) ON DELETE CASCADE,
    CONSTRAINT FK_BudgetRevisions_ApprovedBy FOREIGN KEY (ApprovedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_BudgetRevisions_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_BudgetRevisions_RevisionNo CHECK (RevisionNumber > 0)
);
GO

-- =============================================
-- 4. BudgetAlerts
--    Notify when spending crosses a threshold
-- =============================================
CREATE TABLE BudgetAlerts (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    BudgetId            UNIQUEIDENTIFIER    NOT NULL,
    BudgetLineId        UNIQUEIDENTIFIER    NULL,                        -- NULL = alert for whole budget
    AlertType           NVARCHAR(30)        NOT NULL DEFAULT 'THRESHOLD', -- THRESHOLD, OVERRUN, APPROACHING
    ThresholdPercent    DECIMAL(5,2)        NOT NULL DEFAULT 80,         -- Alert fires at this % of budget used
    CurrentPercent      DECIMAL(5,2)        NOT NULL DEFAULT 0,
    IsTriggered         BIT                 NOT NULL DEFAULT 0,
    TriggeredAt         DATETIME2           NULL,
    IsAcknowledged      BIT                 NOT NULL DEFAULT 0,
    AcknowledgedBy      UNIQUEIDENTIFIER    NULL,
    AcknowledgedAt      DATETIME2           NULL,
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_BudgetAlerts PRIMARY KEY (Id),
    CONSTRAINT FK_BudgetAlerts_Budget FOREIGN KEY (BudgetId)
        REFERENCES Budgets(Id) ON DELETE CASCADE,
    CONSTRAINT FK_BudgetAlerts_BudgetLine FOREIGN KEY (BudgetLineId)
        REFERENCES BudgetLines(Id),
    CONSTRAINT FK_BudgetAlerts_AcknowledgedBy FOREIGN KEY (AcknowledgedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_BudgetAlerts_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT CHK_BudgetAlerts_Type CHECK (AlertType IN (
        'THRESHOLD', 'OVERRUN', 'APPROACHING'
    )),
    CONSTRAINT CHK_BudgetAlerts_Threshold CHECK (
        ThresholdPercent BETWEEN 0 AND 200
    )
);
GO

-- =============================================
-- 5. INDEXES
-- =============================================

-- Budgets
CREATE INDEX IX_Budgets_FiscalYearId    ON Budgets(FiscalYearId);
CREATE INDEX IX_Budgets_CompanyId       ON Budgets(CompanyId);
CREATE INDEX IX_Budgets_BranchId        ON Budgets(BranchId);
CREATE INDEX IX_Budgets_Status          ON Budgets(Status);
CREATE INDEX IX_Budgets_BudgetType      ON Budgets(BudgetType);

-- BudgetLines
CREATE INDEX IX_BudgetLines_BudgetId    ON BudgetLines(BudgetId);
CREATE INDEX IX_BudgetLines_AccountId   ON BudgetLines(AccountId);
CREATE INDEX IX_BudgetLines_PeriodId    ON BudgetLines(AccountingPeriodId);

-- BudgetRevisions
CREATE INDEX IX_BudgetRevisions_BudgetId ON BudgetRevisions(BudgetId);

-- BudgetAlerts
CREATE INDEX IX_BudgetAlerts_BudgetId   ON BudgetAlerts(BudgetId);
CREATE INDEX IX_BudgetAlerts_CompanyId  ON BudgetAlerts(CompanyId);
CREATE INDEX IX_BudgetAlerts_Triggered  ON BudgetAlerts(IsTriggered);
GO

-- =============================================
-- 6. TRIGGER: Auto-update Budget totals from lines
-- =============================================
CREATE OR ALTER TRIGGER TR_BudgetLines_UpdateTotals
ON BudgetLines
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AffectedIds TABLE (BudgetId UNIQUEIDENTIFIER);

    INSERT INTO @AffectedIds SELECT BudgetId FROM inserted;
    INSERT INTO @AffectedIds SELECT BudgetId FROM deleted;

    UPDATE B
    SET
        TotalBudgetAmount = ISNULL(L.TotalBudget, 0),
        TotalActualAmount = ISNULL(L.TotalActual, 0),
        TotalVariance     = ISNULL(L.TotalBudget, 0) - ISNULL(L.TotalActual, 0),
        UpdatedAt         = GETUTCDATE()
    FROM Budgets B
    INNER JOIN @AffectedIds A ON B.Id = A.BudgetId
    LEFT JOIN (
        SELECT
            BudgetId,
            SUM(BudgetedAmount) AS TotalBudget,
            SUM(ActualAmount)   AS TotalActual
        FROM BudgetLines
        GROUP BY BudgetId
    ) L ON B.Id = L.BudgetId;
END;
GO

-- =============================================
-- 7. TRIGGER: Auto-update BudgetLine variance
-- =============================================
CREATE OR ALTER TRIGGER TR_BudgetLines_UpdateVariance
ON BudgetLines
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BL
    SET
        Variance        = BL.BudgetedAmount - BL.ActualAmount,
        VariancePercent = CASE
                            WHEN BL.BudgetedAmount = 0 THEN 0
                            ELSE ROUND(((BL.BudgetedAmount - BL.ActualAmount) / BL.BudgetedAmount) * 100, 2)
                          END,
        UpdatedAt       = GETUTCDATE()
    FROM BudgetLines BL
    INNER JOIN inserted I ON BL.Id = I.Id;
END;
GO

PRINT '022_BudgetSchema.sql executed successfully.';
PRINT 'Tables created: Budgets, BudgetLines, BudgetRevisions, BudgetAlerts';
PRINT 'Indexes: 12 indexes created';
PRINT 'Triggers: TR_BudgetLines_UpdateTotals, TR_BudgetLines_UpdateVariance';
GO
