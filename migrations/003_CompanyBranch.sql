USE OrbitERP;
GO

-- =============================================
-- TASK 3: Company & Branch Schema
-- ORBIT ERP — Administration Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE Companies (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Name            NVARCHAR(200)    NOT NULL,
    LegalName       NVARCHAR(200)    NULL,
    RegistrationNo  NVARCHAR(100)    NULL,
    TaxNumber       NVARCHAR(100)    NULL,
    Email           NVARCHAR(200)    NULL,
    Phone           NVARCHAR(50)     NULL,
    Website         NVARCHAR(200)    NULL,
    AddressLine1    NVARCHAR(200)    NULL,
    AddressLine2    NVARCHAR(200)    NULL,
    City            NVARCHAR(100)    NULL,
    State           NVARCHAR(100)    NULL,
    PostalCode      NVARCHAR(20)     NULL,
    Country         NVARCHAR(100)    NULL,
    LogoUrl         NVARCHAR(500)    NULL,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,
    CreatedBy       UNIQUEIDENTIFIER NULL,

    CONSTRAINT FK_Companies_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);
GO

CREATE TABLE Branches (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    Name            NVARCHAR(200)    NOT NULL,
    Code            NVARCHAR(50)     NULL,
    Email           NVARCHAR(200)    NULL,
    Phone           NVARCHAR(50)     NULL,
    AddressLine1    NVARCHAR(200)    NULL,
    AddressLine2    NVARCHAR(200)    NULL,
    City            NVARCHAR(100)    NULL,
    State           NVARCHAR(100)    NULL,
    PostalCode      NVARCHAR(20)     NULL,
    Country         NVARCHAR(100)    NULL,
    IsHeadOffice    BIT              NOT NULL DEFAULT 0,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,
    CreatedBy       UNIQUEIDENTIFIER NULL,

    CONSTRAINT UQ_Branches_Code UNIQUE (Code, CompanyId),
    CONSTRAINT FK_Branches_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_Branches_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);
GO

-- Indexes
CREATE INDEX IX_Branches_CompanyId ON Branches(CompanyId);
GO

PRINT '003 — Company & Branch schema created successfully.';
GO
