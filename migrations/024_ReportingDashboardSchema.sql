-- =============================================
-- ORBIT ERP - 024_ReportingDashboardSchema.sql
-- Reporting & Dashboard Schema
-- Created: 2026
-- =============================================

USE OrbitERP;
GO

-- =============================================
-- 1. ReportTemplates
--    Saved report definitions
-- =============================================
CREATE TABLE ReportTemplates (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    ReportName          NVARCHAR(200)       NOT NULL,
    ReportCode          NVARCHAR(50)        NOT NULL,
    ReportCategory      NVARCHAR(50)        NOT NULL,                    -- SALES, PURCHASE, INVENTORY, HR, FINANCE, AI, CUSTOM
    ReportType          NVARCHAR(30)        NOT NULL DEFAULT 'TABULAR',  -- TABULAR, SUMMARY, CHART, PIVOT, MIXED
    Description         NVARCHAR(500)       NULL,
    ReportSQL           NVARCHAR(MAX)       NULL,                        -- Base SQL query
    FilterConfig        NVARCHAR(MAX)       NULL,                        -- JSON: filter field definitions
    ColumnConfig        NVARCHAR(MAX)       NULL,                        -- JSON: column definitions
    ChartConfig         NVARCHAR(MAX)       NULL,                        -- JSON: chart configuration
    IsSystem            BIT                 NOT NULL DEFAULT 0,          -- System reports cannot be deleted
    IsActive            BIT                 NOT NULL DEFAULT 1,
    IsPublic            BIT                 NOT NULL DEFAULT 0,          -- Visible to all users in company
    ExportFormats       NVARCHAR(100)       NOT NULL DEFAULT 'PDF,EXCEL', -- Supported export formats
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_ReportTemplates PRIMARY KEY (Id),
    CONSTRAINT UQ_ReportTemplates_Code_Company UNIQUE (ReportCode, CompanyId),
    CONSTRAINT FK_ReportTemplates_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_ReportTemplates_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_ReportTemplates_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_ReportTemplates_Category CHECK (ReportCategory IN (
        'SALES', 'PURCHASE', 'INVENTORY', 'HR', 'FINANCE', 'AI', 'CUSTOM'
    )),
    CONSTRAINT CHK_ReportTemplates_Type CHECK (ReportType IN (
        'TABULAR', 'SUMMARY', 'CHART', 'PIVOT', 'MIXED'
    ))
);
GO

-- =============================================
-- 2. ReportSchedules
--    Auto-run and email reports on a schedule
-- =============================================
CREATE TABLE ReportSchedules (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    ReportTemplateId    UNIQUEIDENTIFIER    NOT NULL,
    ScheduleName        NVARCHAR(200)       NOT NULL,
    Frequency           NVARCHAR(20)        NOT NULL DEFAULT 'DAILY',    -- DAILY, WEEKLY, MONTHLY, QUARTERLY
    DayOfWeek          INT                 NULL,                         -- 1=Mon..7=Sun (for WEEKLY)
    DayOfMonth         INT                 NULL,                         -- 1-31 (for MONTHLY)
    RunTime             TIME                NOT NULL DEFAULT '08:00:00',
    ExportFormat        NVARCHAR(10)        NOT NULL DEFAULT 'PDF',      -- PDF, EXCEL
    Recipients          NVARCHAR(2000)      NULL,                        -- Comma-separated emails
    IsActive            BIT                 NOT NULL DEFAULT 1,
    LastRunAt           DATETIME2           NULL,
    NextRunAt           DATETIME2           NULL,
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_ReportSchedules PRIMARY KEY (Id),
    CONSTRAINT FK_ReportSchedules_Template FOREIGN KEY (ReportTemplateId)
        REFERENCES ReportTemplates(Id) ON DELETE CASCADE,
    CONSTRAINT FK_ReportSchedules_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_ReportSchedules_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_ReportSchedules_Frequency CHECK (Frequency IN (
        'DAILY', 'WEEKLY', 'MONTHLY', 'QUARTERLY'
    )),
    CONSTRAINT CHK_ReportSchedules_Format CHECK (ExportFormat IN ('PDF', 'EXCEL'))
);
GO

-- =============================================
-- 3. ReportExecutionLogs
--    History of every report that was run
-- =============================================
CREATE TABLE ReportExecutionLogs (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    ReportTemplateId    UNIQUEIDENTIFIER    NOT NULL,
    ExecutedBy          UNIQUEIDENTIFIER    NULL,                        -- NULL = scheduled run
    ReportScheduleId    UNIQUEIDENTIFIER    NULL,
    ExecutedAt          DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    CompletedAt         DATETIME2           NULL,
    ExecutionTimeMs     INT                 NULL,
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'RUNNING',  -- RUNNING, COMPLETED, FAILED
    ExportFormat        NVARCHAR(10)        NULL,
    ResultRowCount            INT                 NULL,
    FilePath            NVARCHAR(500)       NULL,                        -- Path to generated file
    ErrorMessage        NVARCHAR(1000)      NULL,
    FilterParams        NVARCHAR(MAX)       NULL,                        -- JSON: filters used for this run
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT PK_ReportExecutionLogs PRIMARY KEY (Id),
    CONSTRAINT FK_ReportExecLogs_Template FOREIGN KEY (ReportTemplateId)
        REFERENCES ReportTemplates(Id),
    CONSTRAINT FK_ReportExecLogs_User FOREIGN KEY (ExecutedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_ReportExecLogs_Schedule FOREIGN KEY (ReportScheduleId)
        REFERENCES ReportSchedules(Id),
    CONSTRAINT FK_ReportExecLogs_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT CHK_ReportExecLogs_Status CHECK (Status IN (
        'RUNNING', 'COMPLETED', 'FAILED'
    ))
);
GO

-- =============================================
-- 4. Dashboards
--    User-configurable dashboards
-- =============================================
CREATE TABLE Dashboards (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    DashboardName       NVARCHAR(200)       NOT NULL,
    DashboardCode       NVARCHAR(50)        NOT NULL,
    IsDefault           BIT                 NOT NULL DEFAULT 0,          -- Default dashboard for role
    IsSystem            BIT                 NOT NULL DEFAULT 0,
    IsPublic            BIT                 NOT NULL DEFAULT 0,
    LayoutConfig        NVARCHAR(MAX)       NULL,                        -- JSON: grid layout config
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    OwnerId             UNIQUEIDENTIFIER    NULL,                        -- NULL = company-wide
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CreatedBy           UNIQUEIDENTIFIER    NULL,
    UpdatedBy           UNIQUEIDENTIFIER    NULL,

    CONSTRAINT PK_Dashboards PRIMARY KEY (Id),
    CONSTRAINT UQ_Dashboards_Code_Company UNIQUE (DashboardCode, CompanyId),
    CONSTRAINT FK_Dashboards_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_Dashboards_Owner FOREIGN KEY (OwnerId)
        REFERENCES Users(Id),
    CONSTRAINT FK_Dashboards_CreatedBy FOREIGN KEY (CreatedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_Dashboards_UpdatedBy FOREIGN KEY (UpdatedBy)
        REFERENCES Users(Id)
);
GO

-- =============================================
-- 5. DashboardWidgets
--    Individual widgets on a dashboard
-- =============================================
CREATE TABLE DashboardWidgets (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    DashboardId         UNIQUEIDENTIFIER    NOT NULL,
    WidgetName          NVARCHAR(200)       NOT NULL,
    WidgetType          NVARCHAR(30)        NOT NULL,                    -- KPI, BAR_CHART, LINE_CHART, PIE_CHART, TABLE, MAP, GAUGE
    DataSource          NVARCHAR(100)       NOT NULL,                    -- Which API/query feeds this widget
    RefreshInterval     INT                 NOT NULL DEFAULT 300,        -- Seconds between auto-refresh
    PositionX           INT                 NOT NULL DEFAULT 0,
    PositionY           INT                 NOT NULL DEFAULT 0,
    Width               INT                 NOT NULL DEFAULT 4,          -- Grid columns (1-12)
    Height              INT                 NOT NULL DEFAULT 3,          -- Grid rows
    Config              NVARCHAR(MAX)       NULL,                        -- JSON: widget-specific config
    FilterConfig        NVARCHAR(MAX)       NULL,                        -- JSON: widget filters
    IsVisible           BIT                 NOT NULL DEFAULT 1,
    SortOrder           INT                 NOT NULL DEFAULT 0,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_DashboardWidgets PRIMARY KEY (Id),
    CONSTRAINT FK_DashboardWidgets_Dashboard FOREIGN KEY (DashboardId)
        REFERENCES Dashboards(Id) ON DELETE CASCADE,
    CONSTRAINT CHK_DashboardWidgets_Type CHECK (WidgetType IN (
        'KPI', 'BAR_CHART', 'LINE_CHART', 'PIE_CHART', 'TABLE', 'MAP', 'GAUGE'
    )),
    CONSTRAINT CHK_DashboardWidgets_Width CHECK (Width BETWEEN 1 AND 12),
    CONSTRAINT CHK_DashboardWidgets_Height CHECK (Height BETWEEN 1 AND 20),
    CONSTRAINT CHK_DashboardWidgets_RefreshInterval CHECK (RefreshInterval >= 0)
);
GO

-- =============================================
-- 6. KPIMetrics
--    Stores computed KPI values over time
-- =============================================
CREATE TABLE KPIMetrics (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    MetricName          NVARCHAR(100)       NOT NULL,
    MetricCode          NVARCHAR(50)        NOT NULL,
    MetricCategory      NVARCHAR(50)        NOT NULL,                    -- SALES, PURCHASE, INVENTORY, HR, FINANCE
    MetricValue         DECIMAL(18,2)       NOT NULL DEFAULT 0,
    PreviousValue       DECIMAL(18,2)       NULL,
    TargetValue         DECIMAL(18,2)       NULL,
    ChangePercent       DECIMAL(8,2)        NULL,                        -- % change vs previous
    Unit                NVARCHAR(30)        NULL,                        -- PKR, COUNT, PERCENT, DAYS
    PeriodType          NVARCHAR(20)        NOT NULL DEFAULT 'DAILY',    -- DAILY, WEEKLY, MONTHLY, QUARTERLY, YEARLY
    PeriodDate          DATE                NOT NULL,                    -- The date this metric is for
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    BranchId            UNIQUEIDENTIFIER    NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT PK_KPIMetrics PRIMARY KEY (Id),
    CONSTRAINT UQ_KPIMetrics_Code_Period_Company UNIQUE (MetricCode, PeriodDate, CompanyId),
    CONSTRAINT FK_KPIMetrics_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_KPIMetrics_Branch FOREIGN KEY (BranchId)
        REFERENCES Branches(Id),
    CONSTRAINT CHK_KPIMetrics_Category CHECK (MetricCategory IN (
        'SALES', 'PURCHASE', 'INVENTORY', 'HR', 'FINANCE'
    )),
    CONSTRAINT CHK_KPIMetrics_PeriodType CHECK (PeriodType IN (
        'DAILY', 'WEEKLY', 'MONTHLY', 'QUARTERLY', 'YEARLY'
    ))
);
GO

-- =============================================
-- 7. UserDashboardPreferences
--    Per-user dashboard preferences
-- =============================================
CREATE TABLE UserDashboardPreferences (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    UserId              UNIQUEIDENTIFIER    NOT NULL,
    DashboardId         UNIQUEIDENTIFIER    NOT NULL,
    IsFavorite          BIT                 NOT NULL DEFAULT 0,
    IsDefault           BIT                 NOT NULL DEFAULT 0,
    CustomLayout        NVARCHAR(MAX)       NULL,                        -- User's personal layout overrides
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_UserDashboardPreferences PRIMARY KEY (Id),
    CONSTRAINT UQ_UserDashboardPref_User_Dashboard UNIQUE (UserId, DashboardId),
    CONSTRAINT FK_UserDashPref_User FOREIGN KEY (UserId)
        REFERENCES Users(Id) ON DELETE CASCADE,
    CONSTRAINT FK_UserDashPref_Dashboard FOREIGN KEY (DashboardId)
        REFERENCES Dashboards(Id) ON DELETE CASCADE
);
GO

-- =============================================
-- 8. INDEXES
-- =============================================

-- ReportTemplates
CREATE INDEX IX_ReportTemplates_CompanyId   ON ReportTemplates(CompanyId);
CREATE INDEX IX_ReportTemplates_Category    ON ReportTemplates(ReportCategory);
CREATE INDEX IX_ReportTemplates_IsActive    ON ReportTemplates(IsActive);

-- ReportSchedules
CREATE INDEX IX_ReportSchedules_TemplateId  ON ReportSchedules(ReportTemplateId);
CREATE INDEX IX_ReportSchedules_CompanyId   ON ReportSchedules(CompanyId);
CREATE INDEX IX_ReportSchedules_NextRunAt   ON ReportSchedules(NextRunAt);
CREATE INDEX IX_ReportSchedules_IsActive    ON ReportSchedules(IsActive);

-- ReportExecutionLogs
CREATE INDEX IX_ReportExecLogs_TemplateId   ON ReportExecutionLogs(ReportTemplateId);
CREATE INDEX IX_ReportExecLogs_CompanyId    ON ReportExecutionLogs(CompanyId);
CREATE INDEX IX_ReportExecLogs_ExecutedAt   ON ReportExecutionLogs(ExecutedAt);
CREATE INDEX IX_ReportExecLogs_Status       ON ReportExecutionLogs(Status);

-- Dashboards
CREATE INDEX IX_Dashboards_CompanyId        ON Dashboards(CompanyId);
CREATE INDEX IX_Dashboards_OwnerId          ON Dashboards(OwnerId);

-- DashboardWidgets
CREATE INDEX IX_DashboardWidgets_DashboardId ON DashboardWidgets(DashboardId);

-- KPIMetrics
CREATE INDEX IX_KPIMetrics_CompanyId        ON KPIMetrics(CompanyId);
CREATE INDEX IX_KPIMetrics_MetricCode       ON KPIMetrics(MetricCode);
CREATE INDEX IX_KPIMetrics_PeriodDate       ON KPIMetrics(PeriodDate);
CREATE INDEX IX_KPIMetrics_Category         ON KPIMetrics(MetricCategory);

-- UserDashboardPreferences
CREATE INDEX IX_UserDashPref_UserId         ON UserDashboardPreferences(UserId);
CREATE INDEX IX_UserDashPref_DashboardId    ON UserDashboardPreferences(DashboardId);
GO

-- =============================================
-- 9. SEED: Default System Dashboards
-- =============================================
DECLARE @CompanyId UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM Companies ORDER BY CreatedAt);

IF @CompanyId IS NOT NULL
BEGIN
    INSERT INTO Dashboards (DashboardName, DashboardCode, IsDefault, IsSystem, IsPublic, CompanyId)
    VALUES
    ('Main Dashboard',      'MAIN',         1, 1, 1, @CompanyId),
    ('Sales Dashboard',     'SALES',        0, 1, 1, @CompanyId),
    ('Finance Dashboard',   'FINANCE',      0, 1, 1, @CompanyId),
    ('HR Dashboard',        'HR',           0, 1, 1, @CompanyId),
    ('Inventory Dashboard', 'INVENTORY',    0, 1, 1, @CompanyId);

    PRINT 'Default dashboards seeded successfully.';
END
ELSE
BEGIN
    PRINT 'WARNING: No company found — dashboard seed skipped.';
END
GO

PRINT '024_ReportingDashboardSchema.sql executed successfully.';
PRINT 'Tables created: ReportTemplates, ReportSchedules, ReportExecutionLogs, Dashboards, DashboardWidgets, KPIMetrics, UserDashboardPreferences';
PRINT 'Indexes: 19 indexes created';
PRINT 'Seed: Default system dashboards';
GO
