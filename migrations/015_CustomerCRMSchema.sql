USE OrbitERP;
GO

-- =============================================
-- TASK 15: Customer & CRM Schema
-- ORBIT ERP — Sales & CRM Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE CustomerGroups (
    Id          UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Name        NVARCHAR(100)    NOT NULL,
    Description NVARCHAR(500)    NULL,
    Discount    DECIMAL(5,2)     NOT NULL DEFAULT 0,
    IsActive    BIT              NOT NULL DEFAULT 1,
    CreatedAt   DATETIME2        NOT NULL DEFAULT GETUTCDATE()
);
GO

CREATE TABLE Customers (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CustomerCode    NVARCHAR(50)     NOT NULL,
    CustomerType    NVARCHAR(20)     NOT NULL DEFAULT 'INDIVIDUAL',
                    -- INDIVIDUAL | COMPANY
    FirstName       NVARCHAR(100)    NULL,
    LastName        NVARCHAR(100)    NULL,
    CompanyName     NVARCHAR(200)    NULL,
    GroupId         UNIQUEIDENTIFIER NULL,
    Email           NVARCHAR(200)    NULL,
    Phone           NVARCHAR(50)     NULL,
    Mobile          NVARCHAR(50)     NULL,
    TaxNumber       NVARCHAR(100)    NULL,
    CreditLimit     DECIMAL(18,2)    NOT NULL DEFAULT 0,
    CreditDays      INT              NOT NULL DEFAULT 0,
    OpeningBalance  DECIMAL(18,2)    NOT NULL DEFAULT 0,
    IsActive        BIT              NOT NULL DEFAULT 1,
    Notes           NVARCHAR(1000)   NULL,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    BranchId        UNIQUEIDENTIFIER NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,
    CreatedBy       UNIQUEIDENTIFIER NULL,
    UpdatedBy       UNIQUEIDENTIFIER NULL,

    CONSTRAINT UQ_Customers_Code UNIQUE (CustomerCode),
    CONSTRAINT FK_Customers_Group
        FOREIGN KEY (GroupId) REFERENCES CustomerGroups(Id),
    CONSTRAINT FK_Customers_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_Customers_Branch
        FOREIGN KEY (BranchId) REFERENCES Branches(Id),
    CONSTRAINT FK_Customers_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    CONSTRAINT FK_Customers_UpdatedBy
        FOREIGN KEY (UpdatedBy) REFERENCES Users(Id)
);
GO

CREATE TABLE CustomerAddresses (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CustomerId      UNIQUEIDENTIFIER NOT NULL,
    AddressType     NVARCHAR(20)     NOT NULL DEFAULT 'BILLING',
                    -- BILLING | SHIPPING | BOTH
    AddressLine1    NVARCHAR(200)    NOT NULL,
    AddressLine2    NVARCHAR(200)    NULL,
    City            NVARCHAR(100)    NULL,
    State           NVARCHAR(100)    NULL,
    PostalCode      NVARCHAR(20)     NULL,
    Country         NVARCHAR(100)    NULL,
    IsDefault       BIT              NOT NULL DEFAULT 0,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_CustomerAddresses_Customer
        FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);
GO

CREATE TABLE CustomerContacts (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CustomerId      UNIQUEIDENTIFIER NOT NULL,
    FirstName       NVARCHAR(100)    NOT NULL,
    LastName        NVARCHAR(100)    NULL,
    Designation     NVARCHAR(100)    NULL,
    Email           NVARCHAR(200)    NULL,
    Phone           NVARCHAR(50)     NULL,
    IsPrimary       BIT              NOT NULL DEFAULT 0,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_CustomerContacts_Customer
        FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);
GO

CREATE TABLE CRMStages (
    Id          UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Name        NVARCHAR(100)    NOT NULL,
    Sequence    INT              NOT NULL DEFAULT 0,
    Probability DECIMAL(5,2)     NOT NULL DEFAULT 0,
    IsActive    BIT              NOT NULL DEFAULT 1
);
GO

CREATE TABLE CRMLeads (
    Id                  UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Title               NVARCHAR(200)    NOT NULL,
    CustomerId          UNIQUEIDENTIFIER NULL,
    ContactName         NVARCHAR(200)    NULL,
    ContactEmail        NVARCHAR(200)    NULL,
    ContactPhone        NVARCHAR(50)     NULL,
    StageId             UNIQUEIDENTIFIER NOT NULL,
    ExpectedValue       DECIMAL(18,2)    NOT NULL DEFAULT 0,
    ExpectedCloseDate   DATE             NULL,
    AssignedTo          UNIQUEIDENTIFIER NULL,
    Priority            NVARCHAR(20)     NOT NULL DEFAULT 'MEDIUM',
                        -- LOW | MEDIUM | HIGH
    Status              NVARCHAR(50)     NOT NULL DEFAULT 'OPEN',
                        -- OPEN | WON | LOST | CANCELLED
    LostReason          NVARCHAR(500)    NULL,
    CompanyId           UNIQUEIDENTIFIER NOT NULL,
    CreatedAt           DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt           DATETIME2        NULL,
    CreatedBy           UNIQUEIDENTIFIER NULL,

    CONSTRAINT FK_CRMLeads_Customer
        FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    CONSTRAINT FK_CRMLeads_Stage
        FOREIGN KEY (StageId) REFERENCES CRMStages(Id),
    CONSTRAINT FK_CRMLeads_AssignedTo
        FOREIGN KEY (AssignedTo) REFERENCES Users(Id),
    CONSTRAINT FK_CRMLeads_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    CONSTRAINT FK_CRMLeads_CreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);
GO

INSERT INTO CRMStages (Name, Sequence, Probability) VALUES
    ('New Lead',      1,  10),
    ('Qualified',     2,  25),
    ('Proposal Sent', 3,  50),
    ('Negotiation',   4,  75),
    ('Closed Won',    5, 100),
    ('Closed Lost',   6,   0);
GO

CREATE INDEX IX_Customers_Code      ON Customers(CustomerCode);
CREATE INDEX IX_Customers_CompanyId ON Customers(CompanyId);
CREATE INDEX IX_CRMLeads_Stage      ON CRMLeads(StageId);
CREATE INDEX IX_CRMLeads_Status     ON CRMLeads(Status);
CREATE INDEX IX_CRMLeads_AssignedTo ON CRMLeads(AssignedTo);
GO

PRINT '015 — Customer & CRM schema created successfully.';
GO
