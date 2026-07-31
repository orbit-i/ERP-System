-- =============================================
-- ORBIT ERP - 026_BackupMigrationStrategy.sql
-- Backup & Migration Strategy
-- Created: 2026
-- Environment: SQL Server 2022 in Docker
-- Container: orbit-sqlserver
-- =============================================

USE OrbitERP;
GO

-- =============================================
-- SECTION 1: BACKUP HISTORY TABLE
-- Tracks every backup taken
-- =============================================
CREATE TABLE BackupHistory (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    BackupType          NVARCHAR(20)        NOT NULL,                    -- FULL, DIFFERENTIAL, LOG
    BackupFile          NVARCHAR(500)       NOT NULL,
    BackupSizeBytes     BIGINT              NULL,
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'COMPLETED', -- COMPLETED, FAILED
    StartedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    CompletedAt         DATETIME2           NULL,
    DurationSeconds     INT                 NULL,
    Notes               NVARCHAR(500)       NULL,
    CreatedBy           NVARCHAR(100)       NOT NULL DEFAULT 'SYSTEM',

    CONSTRAINT PK_BackupHistory PRIMARY KEY (Id),
    CONSTRAINT CHK_BackupHistory_Type CHECK (BackupType IN ('FULL', 'DIFFERENTIAL', 'LOG')),
    CONSTRAINT CHK_BackupHistory_Status CHECK (Status IN ('COMPLETED', 'FAILED'))
);
GO

-- =============================================
-- SECTION 2: MIGRATION HISTORY TABLE
-- Tracks which migration scripts have been run
-- =============================================
CREATE TABLE MigrationHistory (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    ScriptNumber        INT                 NOT NULL,
    ScriptName          NVARCHAR(200)       NOT NULL,
    ScriptFile          NVARCHAR(200)       NOT NULL,
    AppliedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    ExecutionTimeMs     INT                 NULL,
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'APPLIED',  -- APPLIED, FAILED, ROLLED_BACK
    AppliedBy           NVARCHAR(100)       NOT NULL DEFAULT 'DBA',
    Notes               NVARCHAR(500)       NULL,
    Checksum            NVARCHAR(64)        NULL,                        -- SHA256 of script content

    CONSTRAINT PK_MigrationHistory PRIMARY KEY (Id),
    CONSTRAINT UQ_MigrationHistory_ScriptNumber UNIQUE (ScriptNumber),
    CONSTRAINT CHK_MigrationHistory_Status CHECK (Status IN (
        'APPLIED', 'FAILED', 'ROLLED_BACK'
    ))
);
GO

-- =============================================
-- SECTION 3: SEED MIGRATION HISTORY
-- Record all 26 scripts as already applied
-- =============================================
INSERT INTO MigrationHistory (ScriptNumber, ScriptName, ScriptFile, Status, AppliedBy)
VALUES
(1,  'Database Setup',                  '001_DatabaseSetup.sql',                'APPLIED', 'Chandar'),
(2,  'Users Roles Permissions',         '002_UsersRolesPermissions.sql',         'APPLIED', 'Chandar'),
(3,  'Company Branch',                  '003_CompanyBranch.sql',                 'APPLIED', 'Chandar'),
(4,  'Audit Log',                       '004_AuditLog.sql',                      'APPLIED', 'Chandar'),
(5,  'Employee Schema',                 '005_EmployeeSchema.sql',                'APPLIED', 'Chandar'),
(6,  'Attendance Schema',               '006_AttendanceSchema.sql',              'APPLIED', 'Chandar'),
(7,  'Leave Schema',                    '007_LeaveSchema.sql',                   'APPLIED', 'Chandar'),
(8,  'Payroll Schema',                  '008_PayrollSchema.sql',                 'APPLIED', 'Chandar'),
(9,  'Recruitment Schema',              '009_RecruitmentSchema.sql',             'APPLIED', 'Chandar'),
(10, 'Performance Evaluation',          '010_PerformanceEvaluation.sql',         'APPLIED', 'Chandar'),
(11, 'Product Category Schema',         '011_ProductCategorySchema.sql',         'APPLIED', 'Chandar'),
(12, 'Warehouse Schema',                '012_WarehouseSchema.sql',               'APPLIED', 'Chandar'),
(13, 'Stock Movement Schema',           '013_StockMovementSchema.sql',           'APPLIED', 'Chandar'),
(14, 'Barcode Schema',                  '014_BarcodeSchema.sql',                 'APPLIED', 'Chandar'),
(15, 'Customer CRM Schema',             '015_CustomerCRMSchema.sql',             'APPLIED', 'Chandar'),
(16, 'Sales Order Quotation Schema',    '016_SalesOrderQuotationSchema.sql',     'APPLIED', 'Chandar'),
(17, 'Invoice Schema',                  '017_InvoiceSchema.sql',                 'APPLIED', 'Chandar'),
(18, 'Vendor Purchase Order Schema',    '018_VendorPurchaseOrderSchema.sql',     'APPLIED', 'Chandar'),
(19, 'Goods Receiving Schema',          '019_GoodsReceivingSchema.sql',          'APPLIED', 'Chandar'),
(20, 'Accounts Payable Receivable',     '020_AccountsPayableReceivable.sql',     'APPLIED', 'Chandar'),
(21, 'General Ledger Schema',           '021_GeneralLedgerSchema.sql',           'APPLIED', 'Chandar'),
(22, 'Budget Schema',                   '022_BudgetSchema.sql',                  'APPLIED', 'Chandar'),
(23, 'AI Logs Insights Schema',         '023_AILogsInsightsSchema.sql',          'APPLIED', 'Chandar'),
(24, 'Reporting Dashboard Schema',      '024_ReportingDashboardSchema.sql',      'APPLIED', 'Chandar'),
(25, 'Indexing Optimization',           '025_IndexingOptimization.sql',          'APPLIED', 'Chandar'),
(26, 'Backup Migration Strategy',       '026_BackupMigrationStrategy.sql',       'APPLIED', 'Chandar');
GO

-- =============================================
-- SECTION 4: STORED PROCEDURE — TAKE BACKUP
-- Run this manually or on schedule
-- =============================================
CREATE OR ALTER PROCEDURE SP_TakeBackup
    @BackupType     NVARCHAR(20)    = 'FULL',
    @BackupPath     NVARCHAR(500)   = '/var/opt/mssql/backups/'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @FileName       NVARCHAR(500);
    DECLARE @FullPath       NVARCHAR(500);
    DECLARE @StartTime      DATETIME2 = GETUTCDATE();
    DECLARE @BackupSQL      NVARCHAR(1000);
    DECLARE @HistoryId      UNIQUEIDENTIFIER = NEWID();
    DECLARE @Timestamp      NVARCHAR(20) = FORMAT(GETUTCDATE(), 'yyyyMMdd_HHmmss');

    -- Build filename
    SET @FileName = 'OrbitERP_' + @BackupType + '_' + @Timestamp + '.bak';
    SET @FullPath = @BackupPath + @FileName;

    -- Log start
    INSERT INTO BackupHistory (Id, BackupType, BackupFile, Status, StartedAt, CreatedBy)
    VALUES (@HistoryId, @BackupType, @FullPath, 'COMPLETED', @StartTime, SUSER_NAME());

    -- Build and execute backup SQL
    IF @BackupType = 'FULL'
        SET @BackupSQL = 'BACKUP DATABASE OrbitERP TO DISK = ''' + @FullPath +
                         ''' WITH FORMAT, INIT, COMPRESSION, STATS = 10';
    ELSE IF @BackupType = 'DIFFERENTIAL'
        SET @BackupSQL = 'BACKUP DATABASE OrbitERP TO DISK = ''' + @FullPath +
                         ''' WITH DIFFERENTIAL, COMPRESSION, STATS = 10';
    ELSE IF @BackupType = 'LOG'
        SET @BackupSQL = 'BACKUP LOG OrbitERP TO DISK = ''' + @FullPath +
                         ''' WITH COMPRESSION, STATS = 10';

    EXEC sp_executesql @BackupSQL;

    -- Update history with completion
    UPDATE BackupHistory
    SET
        CompletedAt     = GETUTCDATE(),
        DurationSeconds = DATEDIFF(SECOND, @StartTime, GETUTCDATE()),
        Notes           = 'Backup completed successfully'
    WHERE Id = @HistoryId;

    PRINT 'Backup completed: ' + @FullPath;
END;
GO

-- =============================================
-- SECTION 5: VIEW — Migration Status
-- =============================================
CREATE OR ALTER VIEW VW_MigrationStatus AS
SELECT
    ScriptNumber,
    ScriptName,
    ScriptFile,
    Status,
    AppliedBy,
    AppliedAt,
    ExecutionTimeMs,
    Notes
FROM MigrationHistory
ORDER BY ScriptNumber
OFFSET 0 ROWS;
GO

-- =============================================
-- SECTION 6: VIEW — Backup History
-- =============================================
CREATE OR ALTER VIEW VW_BackupHistory AS
SELECT
    BackupType,
    BackupFile,
    BackupSizeBytes,
    Status,
    StartedAt,
    CompletedAt,
    DurationSeconds,
    CreatedBy
FROM BackupHistory
ORDER BY StartedAt DESC
OFFSET 0 ROWS;
GO

-- =============================================
-- SECTION 7: RECORD FINAL BACKUP
-- =============================================
INSERT INTO BackupHistory (BackupType, BackupFile, Status, StartedAt, CompletedAt, Notes, CreatedBy)
VALUES (
    'FULL',
    '/var/opt/mssql/OrbitERP_final.bak',
    'COMPLETED',
    GETUTCDATE(),
    GETUTCDATE(),
    'Final backup after all 26 migration scripts completed',
    'Chandar'
);
GO

PRINT '026_BackupMigrationStrategy.sql executed successfully.';
PRINT 'Tables created: BackupHistory, MigrationHistory';
PRINT 'Seed: All 26 migration scripts recorded';
PRINT 'Stored Procedure: SP_TakeBackup';
PRINT 'Views: VW_MigrationStatus, VW_BackupHistory';
GO
