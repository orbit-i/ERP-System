USE OrbitERP;
GO

-- =============================================
-- TASK 7: Leave Schema
-- ORBIT ERP — Human Resource Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE LeaveTypes (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    Name            NVARCHAR(100)    NOT NULL,
    Description     NVARCHAR(255)    NULL,
    MaxDaysPerYear  INT              NOT NULL DEFAULT 0,
    IsPaid          BIT              NOT NULL DEFAULT 1,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_LeaveTypes_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id)
);
GO

CREATE TABLE LeaveBalances (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    EmployeeId      UNIQUEIDENTIFIER NOT NULL,
    LeaveTypeId     UNIQUEIDENTIFIER NOT NULL,
    Year            INT              NOT NULL,
    TotalDays       DECIMAL(5,1)     NOT NULL DEFAULT 0,
    UsedDays        DECIMAL(5,1)     NOT NULL DEFAULT 0,
    RemainingDays   DECIMAL(5,1)     NOT NULL DEFAULT 0,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT UQ_LeaveBalances UNIQUE (EmployeeId, LeaveTypeId, Year),
    CONSTRAINT FK_LeaveBalances_Employee
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
    CONSTRAINT FK_LeaveBalances_LeaveType
        FOREIGN KEY (LeaveTypeId) REFERENCES LeaveTypes(Id)
);
GO

CREATE TABLE LeaveRequests (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    EmployeeId      UNIQUEIDENTIFIER NOT NULL,
    LeaveTypeId     UNIQUEIDENTIFIER NOT NULL,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    StartDate       DATE             NOT NULL,
    EndDate         DATE             NOT NULL,
    TotalDays       DECIMAL(5,1)     NOT NULL,
    Reason          NVARCHAR(500)    NULL,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'PENDING',
                    -- PENDING | APPROVED | REJECTED | CANCELLED
    ApprovedById    UNIQUEIDENTIFIER NULL,
    ApprovedAt      DATETIME2        NULL,
    RejectedById    UNIQUEIDENTIFIER NULL,
    RejectedAt      DATETIME2        NULL,
    RejectionNote   NVARCHAR(500)    NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT FK_LeaveRequests_Employee
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
    CONSTRAINT FK_LeaveRequests_LeaveType
        FOREIGN KEY (LeaveTypeId) REFERENCES LeaveTypes(Id),
    CONSTRAINT FK_LeaveRequests_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_LeaveRequests_ApprovedBy
        FOREIGN KEY (ApprovedById) REFERENCES Users(Id),
    CONSTRAINT FK_LeaveRequests_RejectedBy
        FOREIGN KEY (RejectedById) REFERENCES Users(Id)
);
GO

-- Indexes
CREATE INDEX IX_LeaveRequests_EmployeeId ON LeaveRequests(EmployeeId);
CREATE INDEX IX_LeaveRequests_Status     ON LeaveRequests(Status);
CREATE INDEX IX_LeaveRequests_StartDate  ON LeaveRequests(StartDate);
GO

PRINT '007 — Leave schema created successfully.';
GO
