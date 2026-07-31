USE OrbitERP;
GO

-- =============================================
-- TASK 5: Employee Schema
-- ORBIT ERP — Human Resource Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE Departments (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    BranchId        UNIQUEIDENTIFIER NULL,
    Name            NVARCHAR(100)    NOT NULL,
    Description     NVARCHAR(255)    NULL,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT FK_Departments_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_Departments_Branch
        FOREIGN KEY (BranchId) REFERENCES Branches(Id)
);
GO

CREATE TABLE Designations (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    DepartmentId    UNIQUEIDENTIFIER NULL,
    Name            NVARCHAR(100)    NOT NULL,
    Description     NVARCHAR(255)    NULL,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_Designations_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_Designations_Department
        FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
);
GO

CREATE TABLE Employees (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    EmployeeCode    NVARCHAR(50)     NOT NULL,
    UserId          UNIQUEIDENTIFIER NULL,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    BranchId        UNIQUEIDENTIFIER NULL,
    DepartmentId    UNIQUEIDENTIFIER NULL,
    DesignationId   UNIQUEIDENTIFIER NULL,
    FirstName       NVARCHAR(100)    NOT NULL,
    LastName        NVARCHAR(100)    NOT NULL,
    Email           NVARCHAR(200)    NULL,
    Phone           NVARCHAR(50)     NULL,
    DateOfBirth     DATE             NULL,
    Gender          NVARCHAR(20)     NULL,
    NationalId      NVARCHAR(100)    NULL,
    Address         NVARCHAR(500)    NULL,
    City            NVARCHAR(100)    NULL,
    Country         NVARCHAR(100)    NULL,
    JoiningDate     DATE             NOT NULL,
    TerminationDate DATE             NULL,
    EmploymentType  NVARCHAR(50)     NOT NULL DEFAULT 'FULL_TIME',
                    -- FULL_TIME | PART_TIME | CONTRACT | INTERN
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'ACTIVE',
                    -- ACTIVE | INACTIVE | TERMINATED | ON_LEAVE
    ProfilePhotoUrl NVARCHAR(500)    NULL,
    BasicSalary     DECIMAL(18,2)    NOT NULL DEFAULT 0,
    BankName        NVARCHAR(100)    NULL,
    BankAccountNo   NVARCHAR(100)    NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT UQ_Employees_Code UNIQUE (EmployeeCode),
    CONSTRAINT FK_Employees_User
        FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_Employees_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_Employees_Branch
        FOREIGN KEY (BranchId) REFERENCES Branches(Id),
    CONSTRAINT FK_Employees_Department
        FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
    CONSTRAINT FK_Employees_Designation
        FOREIGN KEY (DesignationId) REFERENCES Designations(Id)
);
GO

-- Indexes
CREATE INDEX IX_Employees_Code         ON Employees(EmployeeCode);
CREATE INDEX IX_Employees_CompanyId    ON Employees(CompanyId);
CREATE INDEX IX_Employees_DepartmentId ON Employees(DepartmentId);
CREATE INDEX IX_Employees_Status       ON Employees(Status);
GO

PRINT '005 — Employee schema created successfully.';
GO
