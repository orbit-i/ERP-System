USE OrbitERP;
GO

-- =============================================
-- TASK 4: Audit Log Schema
-- ORBIT ERP — Administration Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE AuditLogs (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    UserId          UNIQUEIDENTIFIER NULL,
    CompanyId       UNIQUEIDENTIFIER NULL,
    BranchId        UNIQUEIDENTIFIER NULL,
    Module          NVARCHAR(100)    NOT NULL,
    Action          NVARCHAR(100)    NOT NULL,
    EntityName      NVARCHAR(100)    NULL,
    EntityId        NVARCHAR(50)     NULL,
    OldValues       NVARCHAR(MAX)    NULL,
    NewValues       NVARCHAR(MAX)    NULL,
    IpAddress       NVARCHAR(50)     NULL,
    UserAgent       NVARCHAR(500)    NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_AuditLogs_User
        FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_AuditLogs_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_AuditLogs_Branch
        FOREIGN KEY (BranchId) REFERENCES Branches(Id)
);
GO

-- Indexes
CREATE INDEX IX_AuditLogs_UserId    ON AuditLogs(UserId);
CREATE INDEX IX_AuditLogs_CompanyId ON AuditLogs(CompanyId);
CREATE INDEX IX_AuditLogs_CreatedAt ON AuditLogs(CreatedAt);
CREATE INDEX IX_AuditLogs_Module    ON AuditLogs(Module);
GO

PRINT '004 — Audit Log schema created successfully.';
GO
