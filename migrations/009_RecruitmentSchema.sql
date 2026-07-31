USE OrbitERP;
GO

-- =============================================
-- TASK 9: Recruitment Schema
-- ORBIT ERP — Human Resource Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE JobPostings (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    BranchId        UNIQUEIDENTIFIER NULL,
    DepartmentId    UNIQUEIDENTIFIER NULL,
    DesignationId   UNIQUEIDENTIFIER NULL,
    Title           NVARCHAR(200)    NOT NULL,
    Description     NVARCHAR(MAX)    NULL,
    Requirements    NVARCHAR(MAX)    NULL,
    JobType         NVARCHAR(50)     NOT NULL DEFAULT 'FULL_TIME',
                    -- FULL_TIME | PART_TIME | CONTRACT | INTERN
    Location        NVARCHAR(200)    NULL,
    SalaryMin       DECIMAL(18,2)    NULL,
    SalaryMax       DECIMAL(18,2)    NULL,
    Vacancies       INT              NOT NULL DEFAULT 1,
    PostedDate      DATE             NOT NULL,
    ClosingDate     DATE             NULL,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'DRAFT',
                    -- DRAFT | PUBLISHED | CLOSED | CANCELLED
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,
    CreatedBy       UNIQUEIDENTIFIER NULL,

    CONSTRAINT FK_JobPostings_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_JobPostings_Branch
        FOREIGN KEY (BranchId) REFERENCES Branches(Id),
    CONSTRAINT FK_JobPostings_Department
        FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
    CONSTRAINT FK_JobPostings_Designation
        FOREIGN KEY (DesignationId) REFERENCES Designations(Id),
    CONSTRAINT FK_JobPostings_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);
GO

CREATE TABLE JobApplications (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    JobPostingId    UNIQUEIDENTIFIER NOT NULL,
    FirstName       NVARCHAR(100)    NOT NULL,
    LastName        NVARCHAR(100)    NOT NULL,
    Email           NVARCHAR(200)    NOT NULL,
    Phone           NVARCHAR(50)     NULL,
    ResumeUrl       NVARCHAR(500)    NULL,
    CoverLetter     NVARCHAR(MAX)    NULL,
    CurrentSalary   DECIMAL(18,2)    NULL,
    ExpectedSalary  DECIMAL(18,2)    NULL,
    NoticePeriod    INT              NULL,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'APPLIED',
                    -- APPLIED | SCREENING | INTERVIEW | OFFER | HIRED | REJECTED
    Notes           NVARCHAR(1000)   NULL,
    AppliedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT FK_JobApplications_Posting
        FOREIGN KEY (JobPostingId) REFERENCES JobPostings(Id)
);
GO

CREATE TABLE Interviews (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    ApplicationId   UNIQUEIDENTIFIER NOT NULL,
    InterviewType   NVARCHAR(50)     NOT NULL DEFAULT 'IN_PERSON',
                    -- IN_PERSON | PHONE | VIDEO | TECHNICAL
    ScheduledAt     DATETIME2        NOT NULL,
    Location        NVARCHAR(200)    NULL,
    MeetingLink     NVARCHAR(500)    NULL,
    InterviewerId   UNIQUEIDENTIFIER NULL,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'SCHEDULED',
                    -- SCHEDULED | COMPLETED | CANCELLED | NO_SHOW
    Feedback        NVARCHAR(MAX)    NULL,
    Rating          INT              NULL,  -- 1-5
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT FK_Interviews_Application
        FOREIGN KEY (ApplicationId) REFERENCES JobApplications(Id),
    CONSTRAINT FK_Interviews_Interviewer
        FOREIGN KEY (InterviewerId) REFERENCES Users(Id)
);
GO

-- Indexes
CREATE INDEX IX_JobPostings_Status      ON JobPostings(Status);
CREATE INDEX IX_JobPostings_CompanyId   ON JobPostings(CompanyId);
CREATE INDEX IX_JobApplications_Posting ON JobApplications(JobPostingId);
CREATE INDEX IX_JobApplications_Status  ON JobApplications(Status);
CREATE INDEX IX_Interviews_Application  ON Interviews(ApplicationId);
GO

PRINT '009 — Recruitment schema created successfully.';
GO
