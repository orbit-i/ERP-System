USE OrbitERP;
GO

-- =============================================
-- TASK 8: Payroll Schema
-- ORBIT ERP — Human Resource Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE SalaryComponents (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    Name            NVARCHAR(100)    NOT NULL,
    Code            NVARCHAR(50)     NOT NULL,
    ComponentType   NVARCHAR(20)     NOT NULL DEFAULT 'EARNING',
                    -- EARNING | DEDUCTION | TAX
    CalculationType NVARCHAR(20)     NOT NULL DEFAULT 'FIXED',
                    -- FIXED | PERCENTAGE
    DefaultValue    DECIMAL(18,2)    NOT NULL DEFAULT 0,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT UQ_SalaryComponents_Code UNIQUE (Code, CompanyId),
    CONSTRAINT FK_SalaryComponents_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id)
);
GO

CREATE TABLE EmployeeSalaryStructure (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    EmployeeId      UNIQUEIDENTIFIER NOT NULL,
    ComponentId     UNIQUEIDENTIFIER NOT NULL,
    Value           DECIMAL(18,2)    NOT NULL DEFAULT 0,
    EffectiveFrom   DATE             NOT NULL,
    EffectiveTo     DATE             NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_ESS_Employee
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
    CONSTRAINT FK_ESS_Component
        FOREIGN KEY (ComponentId) REFERENCES SalaryComponents(Id)
);
GO

CREATE TABLE PayrollPeriods (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    Name            NVARCHAR(100)    NOT NULL,
    StartDate       DATE             NOT NULL,
    EndDate         DATE             NOT NULL,
    PaymentDate     DATE             NULL,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'DRAFT',
                    -- DRAFT | PROCESSING | COMPLETED | CANCELLED
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,
    CreatedBy       UNIQUEIDENTIFIER NULL,

    CONSTRAINT FK_PayrollPeriods_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_PayrollPeriods_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);
GO

CREATE TABLE PayrollSlips (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    PayrollPeriodId UNIQUEIDENTIFIER NOT NULL,
    EmployeeId      UNIQUEIDENTIFIER NOT NULL,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    BasicSalary     DECIMAL(18,2)    NOT NULL DEFAULT 0,
    TotalEarnings   DECIMAL(18,2)    NOT NULL DEFAULT 0,
    TotalDeductions DECIMAL(18,2)    NOT NULL DEFAULT 0,
    NetSalary       DECIMAL(18,2)    NOT NULL DEFAULT 0,
    WorkingDays     INT              NOT NULL DEFAULT 0,
    PresentDays     INT              NOT NULL DEFAULT 0,
    AbsentDays      INT              NOT NULL DEFAULT 0,
    OvertimeHours   DECIMAL(4,2)     NOT NULL DEFAULT 0,
    OvertimeAmount  DECIMAL(18,2)    NOT NULL DEFAULT 0,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'DRAFT',
                    -- DRAFT | APPROVED | PAID | CANCELLED
    PaidAt          DATETIME2        NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT UQ_PayrollSlips UNIQUE (PayrollPeriodId, EmployeeId),
    CONSTRAINT FK_PayrollSlips_Period
        FOREIGN KEY (PayrollPeriodId) REFERENCES PayrollPeriods(Id),
    CONSTRAINT FK_PayrollSlips_Employee
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
    CONSTRAINT FK_PayrollSlips_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id)
);
GO

CREATE TABLE PayrollSlipDetails (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    PayrollSlipId   UNIQUEIDENTIFIER NOT NULL,
    ComponentId     UNIQUEIDENTIFIER NOT NULL,
    ComponentName   NVARCHAR(100)    NOT NULL,
    ComponentType   NVARCHAR(20)     NOT NULL,
    Amount          DECIMAL(18,2)    NOT NULL DEFAULT 0,

    CONSTRAINT FK_PayrollSlipDetails_Slip
        FOREIGN KEY (PayrollSlipId) REFERENCES PayrollSlips(Id),
    CONSTRAINT FK_PayrollSlipDetails_Component
        FOREIGN KEY (ComponentId) REFERENCES SalaryComponents(Id)
);
GO

-- Indexes
CREATE INDEX IX_PayrollSlips_EmployeeId  ON PayrollSlips(EmployeeId);
CREATE INDEX IX_PayrollSlips_PeriodId    ON PayrollSlips(PayrollPeriodId);
CREATE INDEX IX_PayrollSlips_Status      ON PayrollSlips(Status);
CREATE INDEX IX_PayrollSlips_CompanyId   ON PayrollSlips(CompanyId);
GO

PRINT '008 — Payroll schema created successfully.';
GO
