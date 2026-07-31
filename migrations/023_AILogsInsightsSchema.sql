-- =============================================
-- ORBIT ERP - 023_AILogsInsightsSchema.sql
-- AI Logs & Insights Schema
-- Created: 2026
-- =============================================

USE OrbitERP;
GO

-- =============================================
-- 1. AIConversations
--    Tracks each chat session with AI Assistant
-- =============================================
CREATE TABLE AIConversations (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    SessionId           NVARCHAR(100)       NOT NULL,                    -- Unique session identifier
    UserId              UNIQUEIDENTIFIER    NOT NULL,
    Title               NVARCHAR(200)       NULL,                        -- Auto-generated or user-defined
    AIModel             NVARCHAR(100)       NOT NULL DEFAULT 'gpt-4',    -- gpt-4, gemini-pro, etc.
    Module              NVARCHAR(50)        NULL,                        -- Which ERP module context
    TotalMessages       INT                 NOT NULL DEFAULT 0,
    TotalTokensUsed     INT                 NOT NULL DEFAULT 0,
    IsActive            BIT                 NOT NULL DEFAULT 1,
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    LastMessageAt       DATETIME2           NULL,

    CONSTRAINT PK_AIConversations PRIMARY KEY (Id),
    CONSTRAINT UQ_AIConversations_SessionId UNIQUE (SessionId),
    CONSTRAINT FK_AIConversations_User FOREIGN KEY (UserId)
        REFERENCES Users(Id),
    CONSTRAINT FK_AIConversations_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id)
);
GO

-- =============================================
-- 2. AIMessages
--    Individual messages within a conversation
-- =============================================
CREATE TABLE AIMessages (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    ConversationId      UNIQUEIDENTIFIER    NOT NULL,
    Role                NVARCHAR(20)        NOT NULL DEFAULT 'USER',     -- USER, ASSISTANT, SYSTEM
    Content             NVARCHAR(MAX)       NOT NULL,
    TokensUsed          INT                 NOT NULL DEFAULT 0,
    ResponseTimeMs      INT                 NULL,                        -- How long AI took to respond (ms)
    IsError             BIT                 NOT NULL DEFAULT 0,
    ErrorMessage        NVARCHAR(500)       NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT PK_AIMessages PRIMARY KEY (Id),
    CONSTRAINT FK_AIMessages_Conversation FOREIGN KEY (ConversationId)
        REFERENCES AIConversations(Id) ON DELETE CASCADE,
    CONSTRAINT CHK_AIMessages_Role CHECK (Role IN ('USER', 'ASSISTANT', 'SYSTEM'))
);
GO

-- =============================================
-- 3. AIInsights
--    AI-generated business insights and predictions
-- =============================================
CREATE TABLE AIInsights (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    InsightType         NVARCHAR(50)        NOT NULL,                    -- SALES_PREDICTION, INVENTORY_FORECAST, FINANCIAL_INSIGHT, KPI_SUMMARY, RISK_ALERT
    Title               NVARCHAR(200)       NOT NULL,
    Summary             NVARCHAR(1000)      NOT NULL,
    DetailedContent     NVARCHAR(MAX)       NULL,                        -- Full AI response / analysis
    AIModel             NVARCHAR(100)       NOT NULL DEFAULT 'gpt-4',
    Confidence          DECIMAL(5,2)        NULL,                        -- 0-100 confidence score
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'ACTIVE',   -- ACTIVE, DISMISSED, ACTIONED, EXPIRED
    Priority            NVARCHAR(10)        NOT NULL DEFAULT 'MEDIUM',   -- LOW, MEDIUM, HIGH, CRITICAL
    -- Context
    Module              NVARCHAR(50)        NULL,                        -- ERP module this insight relates to
    ReferenceType       NVARCHAR(50)        NULL,                        -- PRODUCT, CUSTOMER, VENDOR, etc.
    ReferenceId         UNIQUEIDENTIFIER    NULL,                        -- ID of related entity
    -- Validity
    ValidFrom           DATE                NULL,
    ValidUntil          DATE                NULL,
    -- FK
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    GeneratedBy         UNIQUEIDENTIFIER    NULL,                        -- User who triggered generation
    DismissedBy         UNIQUEIDENTIFIER    NULL,
    DismissedAt         DATETIME2           NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_AIInsights PRIMARY KEY (Id),
    CONSTRAINT FK_AIInsights_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT FK_AIInsights_GeneratedBy FOREIGN KEY (GeneratedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_AIInsights_DismissedBy FOREIGN KEY (DismissedBy)
        REFERENCES Users(Id),
    CONSTRAINT CHK_AIInsights_Type CHECK (InsightType IN (
        'SALES_PREDICTION', 'INVENTORY_FORECAST', 'FINANCIAL_INSIGHT',
        'KPI_SUMMARY', 'RISK_ALERT', 'REPORT_SUMMARY', 'ANOMALY_DETECTION'
    )),
    CONSTRAINT CHK_AIInsights_Status CHECK (Status IN (
        'ACTIVE', 'DISMISSED', 'ACTIONED', 'EXPIRED'
    )),
    CONSTRAINT CHK_AIInsights_Priority CHECK (Priority IN (
        'LOW', 'MEDIUM', 'HIGH', 'CRITICAL'
    )),
    CONSTRAINT CHK_AIInsights_Confidence CHECK (
        Confidence IS NULL OR Confidence BETWEEN 0 AND 100
    )
);
GO

-- =============================================
-- 4. AIReportRequests
--    Tracks AI-generated report requests
-- =============================================
CREATE TABLE AIReportRequests (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    ReportType          NVARCHAR(50)        NOT NULL,                    -- SALES, INVENTORY, FINANCIAL, HR, CUSTOM
    RequestedBy         UNIQUEIDENTIFIER    NOT NULL,
    Prompt              NVARCHAR(2000)      NOT NULL,                    -- User's natural language request
    GeneratedSQL        NVARCHAR(MAX)       NULL,                        -- SQL generated by AI
    GeneratedReport     NVARCHAR(MAX)       NULL,                        -- Report content / JSON
    Status              NVARCHAR(20)        NOT NULL DEFAULT 'PENDING',  -- PENDING, PROCESSING, COMPLETED, FAILED
    AIModel             NVARCHAR(100)       NOT NULL DEFAULT 'gpt-4',
    TokensUsed          INT                 NOT NULL DEFAULT 0,
    ProcessingTimeMs    INT                 NULL,
    ErrorMessage        NVARCHAR(1000)      NULL,
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,
    CompletedAt         DATETIME2           NULL,

    CONSTRAINT PK_AIReportRequests PRIMARY KEY (Id),
    CONSTRAINT FK_AIReportRequests_User FOREIGN KEY (RequestedBy)
        REFERENCES Users(Id),
    CONSTRAINT FK_AIReportRequests_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT CHK_AIReportRequests_Status CHECK (Status IN (
        'PENDING', 'PROCESSING', 'COMPLETED', 'FAILED'
    )),
    CONSTRAINT CHK_AIReportRequests_Type CHECK (ReportType IN (
        'SALES', 'INVENTORY', 'FINANCIAL', 'HR', 'CUSTOM'
    ))
);
GO

-- =============================================
-- 5. AIPredictions
--    Stores AI model predictions (sales, stock, etc.)
-- =============================================
CREATE TABLE AIPredictions (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    PredictionType      NVARCHAR(50)        NOT NULL,                    -- SALES_FORECAST, STOCK_DEPLETION, CASH_FLOW, DEMAND
    EntityType          NVARCHAR(50)        NULL,                        -- PRODUCT, CUSTOMER, CATEGORY
    EntityId            UNIQUEIDENTIFIER    NULL,                        -- ID of the entity being predicted
    PredictionDate      DATE                NOT NULL,                    -- Date this prediction is FOR
    PredictedValue      DECIMAL(18,2)       NOT NULL,
    ActualValue         DECIMAL(18,2)       NULL,                        -- Filled in later for accuracy tracking
    Accuracy            DECIMAL(5,2)        NULL,                        -- Calculated after actual is known
    Confidence          DECIMAL(5,2)        NULL,
    AIModel             NVARCHAR(100)       NOT NULL DEFAULT 'gpt-4',
    ModelVersion        NVARCHAR(50)        NULL,
    InputData           NVARCHAR(MAX)       NULL,                        -- JSON of input features
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2           NULL,

    CONSTRAINT PK_AIPredictions PRIMARY KEY (Id),
    CONSTRAINT FK_AIPredictions_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT CHK_AIPredictions_Type CHECK (PredictionType IN (
        'SALES_FORECAST', 'STOCK_DEPLETION', 'CASH_FLOW', 'DEMAND'
    )),
    CONSTRAINT CHK_AIPredictions_Confidence CHECK (
        Confidence IS NULL OR Confidence BETWEEN 0 AND 100
    ),
    CONSTRAINT CHK_AIPredictions_Accuracy CHECK (
        Accuracy IS NULL OR Accuracy BETWEEN 0 AND 100
    )
);
GO

-- =============================================
-- 6. AIUsageLogs
--    Tracks API usage and costs per company
-- =============================================
CREATE TABLE AIUsageLogs (
    Id                  UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    UserId              UNIQUEIDENTIFIER    NOT NULL,
    AIModel             NVARCHAR(100)       NOT NULL,
    FeatureType         NVARCHAR(50)        NOT NULL,                    -- ASSISTANT, REPORT, PREDICTION, INSIGHT
    TokensInput         INT                 NOT NULL DEFAULT 0,
    TokensOutput        INT                 NOT NULL DEFAULT 0,
    TotalTokens         INT                 NOT NULL DEFAULT 0,
    EstimatedCostUSD    DECIMAL(10,6)       NOT NULL DEFAULT 0,
    ResponseTimeMs      INT                 NULL,
    IsSuccess           BIT                 NOT NULL DEFAULT 1,
    ErrorCode           NVARCHAR(50)        NULL,
    CompanyId           UNIQUEIDENTIFIER    NOT NULL,
    -- Audit
    CreatedAt           DATETIME2           NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT PK_AIUsageLogs PRIMARY KEY (Id),
    CONSTRAINT FK_AIUsageLogs_User FOREIGN KEY (UserId)
        REFERENCES Users(Id),
    CONSTRAINT FK_AIUsageLogs_Company FOREIGN KEY (CompanyId)
        REFERENCES Companies(Id),
    CONSTRAINT CHK_AIUsageLogs_FeatureType CHECK (FeatureType IN (
        'ASSISTANT', 'REPORT', 'PREDICTION', 'INSIGHT'
    ))
);
GO

-- =============================================
-- 7. INDEXES
-- =============================================

-- AIConversations
CREATE INDEX IX_AIConversations_UserId      ON AIConversations(UserId);
CREATE INDEX IX_AIConversations_CompanyId   ON AIConversations(CompanyId);
CREATE INDEX IX_AIConversations_CreatedAt   ON AIConversations(CreatedAt);

-- AIMessages
CREATE INDEX IX_AIMessages_ConversationId   ON AIMessages(ConversationId);
CREATE INDEX IX_AIMessages_CreatedAt        ON AIMessages(CreatedAt);

-- AIInsights
CREATE INDEX IX_AIInsights_CompanyId        ON AIInsights(CompanyId);
CREATE INDEX IX_AIInsights_InsightType      ON AIInsights(InsightType);
CREATE INDEX IX_AIInsights_Status           ON AIInsights(Status);
CREATE INDEX IX_AIInsights_Priority         ON AIInsights(Priority);
CREATE INDEX IX_AIInsights_ValidUntil       ON AIInsights(ValidUntil);

-- AIReportRequests
CREATE INDEX IX_AIReportRequests_UserId     ON AIReportRequests(RequestedBy);
CREATE INDEX IX_AIReportRequests_CompanyId  ON AIReportRequests(CompanyId);
CREATE INDEX IX_AIReportRequests_Status     ON AIReportRequests(Status);

-- AIPredictions
CREATE INDEX IX_AIPredictions_CompanyId     ON AIPredictions(CompanyId);
CREATE INDEX IX_AIPredictions_Type          ON AIPredictions(PredictionType);
CREATE INDEX IX_AIPredictions_Date          ON AIPredictions(PredictionDate);
CREATE INDEX IX_AIPredictions_EntityId      ON AIPredictions(EntityId);

-- AIUsageLogs
CREATE INDEX IX_AIUsageLogs_UserId          ON AIUsageLogs(UserId);
CREATE INDEX IX_AIUsageLogs_CompanyId       ON AIUsageLogs(CompanyId);
CREATE INDEX IX_AIUsageLogs_CreatedAt       ON AIUsageLogs(CreatedAt);
GO

-- =============================================
-- 8. TRIGGER: Auto-update conversation stats
-- =============================================
CREATE OR ALTER TRIGGER TR_AIMessages_UpdateConversation
ON AIMessages
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE C
    SET
        TotalMessages   = TotalMessages + 1,
        TotalTokensUsed = TotalTokensUsed + ISNULL(I.TokensUsed, 0),
        LastMessageAt   = GETUTCDATE(),
        UpdatedAt       = GETUTCDATE()
    FROM AIConversations C
    INNER JOIN inserted I ON C.Id = I.ConversationId;
END;
GO

PRINT '023_AILogsInsightsSchema.sql executed successfully.';
PRINT 'Tables created: AIConversations, AIMessages, AIInsights, AIReportRequests, AIPredictions, AIUsageLogs';
PRINT 'Indexes: 19 indexes created';
PRINT 'Triggers: TR_AIMessages_UpdateConversation';
GO
