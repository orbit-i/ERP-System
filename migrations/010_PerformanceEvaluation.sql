USE OrbitERP;
GO

-- =============================================
-- TASK 10: Performance Evaluation Schema
-- ORBIT ERP — Human Resource Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE PerformanceTemplates (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    Name            NVARCHAR(200)    NOT NULL,
    Description     NVARCHAR(500)    NULL,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_PerformanceTemplates_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id)
);
GO

CREATE TABLE PerformanceCriteria (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    TemplateId      UNIQUEIDENTIFIER NOT NULL,
    Name            NVARCHAR(200)    NOT NULL,
    Description     NVARCHAR(500)    NULL,
    MaxScore        DECIMAL(5,2)     NOT NULL DEFAULT 10,
    Weight          DECIMAL(5,2)     NOT NULL DEFAULT 1,
    SortOrder       INT              NOT NULL DEFAULT 0,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_PerformanceCriteria_Template
        FOREIGN KEY (TemplateId) REFERENCES PerformanceTemplates(Id)
);
GO

CREATE TABLE PerformanceReviews (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    EmployeeId      UNIQUEIDENTIFIER NOT NULL,
    TemplateId      UNIQUEIDENTIFIER NOT NULL,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    ReviewPeriod    NVARCHAR(100)    NOT NULL,   -- e.g. Q1-2024, Annual-2024
    ReviewDate      DATE             NOT NULL,
    ReviewedById    UNIQUEIDENTIFIER NULL,
    TotalScore      DECIMAL(5,2)     NOT NULL DEFAULT 0,
    MaxScore        DECIMAL(5,2)     NOT NULL DEFAULT 0,
    ScorePercent    DECIMAL(5,2)     NOT NULL DEFAULT 0,
    Rating          NVARCHAR(50)     NULL,
                    -- EXCELLENT | GOOD | AVERAGE | BELOW_AVERAGE | POOR
    Comments        NVARCHAR(1000)   NULL,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'DRAFT',
                    -- DRAFT | SUBMITTED | APPROVED | ACKNOWLEDGED
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT FK_PerformanceReviews_Employee
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
    CONSTRAINT FK_PerformanceReviews_Template
        FOREIGN KEY (TemplateId) REFERENCES PerformanceTemplates(Id),
    CONSTRAINT FK_PerformanceReviews_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_PerformanceReviews_ReviewedBy
        FOREIGN KEY (ReviewedById) REFERENCES Users(Id)
);
GO

CREATE TABLE PerformanceReviewDetails (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    ReviewId        UNIQUEIDENTIFIER NOT NULL,
    CriteriaId      UNIQUEIDENTIFIER NOT NULL,
    Score           DECIMAL(5,2)     NOT NULL DEFAULT 0,
    Comments        NVARCHAR(500)    NULL,

    CONSTRAINT FK_PRD_Review
        FOREIGN KEY (ReviewId) REFERENCES PerformanceReviews(Id),
    CONSTRAINT FK_PRD_Criteria
        FOREIGN KEY (CriteriaId) REFERENCES PerformanceCriteria(Id)
);
GO

CREATE TABLE Goals (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    EmployeeId      UNIQUEIDENTIFIER NOT NULL,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    Title           NVARCHAR(200)    NOT NULL,
    Description     NVARCHAR(1000)   NULL,
    TargetDate      DATE             NULL,
    CompletionDate  DATE             NULL,
    Progress        INT              NOT NULL DEFAULT 0,  -- 0-100%
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'IN_PROGRESS',
                    -- IN_PROGRESS | COMPLETED | CANCELLED | ON_HOLD
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,
    CreatedBy       UNIQUEIDENTIFIER NULL,

    CONSTRAINT FK_Goals_Employee
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
    CONSTRAINT FK_Goals_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_Goals_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);
GO

-- Indexes
CREATE INDEX IX_PerformanceReviews_Employee ON PerformanceReviews(EmployeeId);
CREATE INDEX IX_PerformanceReviews_Status   ON PerformanceReviews(Status);
CREATE INDEX IX_Goals_EmployeeId            ON Goals(EmployeeId);
CREATE INDEX IX_Goals_Status                ON Goals(Status);
GO

PRINT '010 — Performance Evaluation schema created successfully.';
GO
