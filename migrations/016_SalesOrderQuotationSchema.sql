USE OrbitERP;
GO

-- =============================================
-- TASK 16: Sales Order & Quotation Schema
-- ORBIT ERP — Sales & CRM Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE Quotations (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    QuotationNo     NVARCHAR(50)     NOT NULL,
    CustomerId      UNIQUEIDENTIFIER NOT NULL,
    LeadId          UNIQUEIDENTIFIER NULL,
    QuotationDate   DATE             NOT NULL,
    ExpiryDate      DATE             NULL,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'DRAFT',
                    -- DRAFT | SENT | ACCEPTED | REJECTED | EXPIRED
    SubTotal        DECIMAL(18,2)    NOT NULL DEFAULT 0,
    DiscountAmount  DECIMAL(18,2)    NOT NULL DEFAULT 0,
    TaxAmount       DECIMAL(18,2)    NOT NULL DEFAULT 0,
    TotalAmount     DECIMAL(18,2)    NOT NULL DEFAULT 0,
    Notes           NVARCHAR(1000)   NULL,
    TermsConditions NVARCHAR(2000)   NULL,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    BranchId        UNIQUEIDENTIFIER NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,
    CreatedBy       UNIQUEIDENTIFIER NULL,
    UpdatedBy       UNIQUEIDENTIFIER NULL,

    CONSTRAINT UQ_Quotations_No    UNIQUE (QuotationNo),
    CONSTRAINT FK_Quotations_Customer  FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    CONSTRAINT FK_Quotations_Lead      FOREIGN KEY (LeadId)     REFERENCES CRMLeads(Id),
    CONSTRAINT FK_Quotations_Company   FOREIGN KEY (CompanyId)  REFERENCES Companies(Id),
    CONSTRAINT FK_Quotations_Branch    FOREIGN KEY (BranchId)   REFERENCES Branches(Id),
    CONSTRAINT FK_Quotations_CreatedBy FOREIGN KEY (CreatedBy)  REFERENCES Users(Id),
    CONSTRAINT FK_Quotations_UpdatedBy FOREIGN KEY (UpdatedBy)  REFERENCES Users(Id)
);
GO

CREATE TABLE QuotationLines (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    QuotationId     UNIQUEIDENTIFIER NOT NULL,
    ProductId       UNIQUEIDENTIFIER NOT NULL,
    VariantId       UNIQUEIDENTIFIER NULL,
    Description     NVARCHAR(500)    NULL,
    Quantity        DECIMAL(18,4)    NOT NULL,
    UomId           UNIQUEIDENTIFIER NOT NULL,
    UnitPrice       DECIMAL(18,4)    NOT NULL DEFAULT 0,
    DiscountPct     DECIMAL(5,2)     NOT NULL DEFAULT 0,
    DiscountAmount  DECIMAL(18,2)    NOT NULL DEFAULT 0,
    TaxPct          DECIMAL(5,2)     NOT NULL DEFAULT 0,
    TaxAmount       DECIMAL(18,2)    NOT NULL DEFAULT 0,
    LineTotal       DECIMAL(18,2)    NOT NULL DEFAULT 0,
    SortOrder       INT              NOT NULL DEFAULT 0,

    CONSTRAINT FK_QuotationLines_Quotation FOREIGN KEY (QuotationId) REFERENCES Quotations(Id),
    CONSTRAINT FK_QuotationLines_Product   FOREIGN KEY (ProductId)   REFERENCES Products(Id),
    CONSTRAINT FK_QuotationLines_Variant   FOREIGN KEY (VariantId)   REFERENCES ProductVariants(Id),
    CONSTRAINT FK_QuotationLines_Uom       FOREIGN KEY (UomId)       REFERENCES UnitsOfMeasure(Id)
);
GO

CREATE TABLE SalesOrders (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    OrderNo         NVARCHAR(50)     NOT NULL,
    CustomerId      UNIQUEIDENTIFIER NOT NULL,
    QuotationId     UNIQUEIDENTIFIER NULL,
    OrderDate       DATE             NOT NULL,
    DeliveryDate    DATE             NULL,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'DRAFT',
                    -- DRAFT | CONFIRMED | PROCESSING | SHIPPED | DELIVERED | CANCELLED
    PaymentStatus   NVARCHAR(50)     NOT NULL DEFAULT 'UNPAID',
                    -- UNPAID | PARTIAL | PAID
    ShippingAddress NVARCHAR(500)    NULL,
    SubTotal        DECIMAL(18,2)    NOT NULL DEFAULT 0,
    DiscountAmount  DECIMAL(18,2)    NOT NULL DEFAULT 0,
    TaxAmount       DECIMAL(18,2)    NOT NULL DEFAULT 0,
    ShippingCost    DECIMAL(18,2)    NOT NULL DEFAULT 0,
    TotalAmount     DECIMAL(18,2)    NOT NULL DEFAULT 0,
    PaidAmount      DECIMAL(18,2)    NOT NULL DEFAULT 0,
    Notes           NVARCHAR(1000)   NULL,
    TermsConditions NVARCHAR(2000)   NULL,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    BranchId        UNIQUEIDENTIFIER NULL,
    SalesRepId      UNIQUEIDENTIFIER NULL,
    WarehouseId     UNIQUEIDENTIFIER NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,
    CreatedBy       UNIQUEIDENTIFIER NULL,
    UpdatedBy       UNIQUEIDENTIFIER NULL,

    CONSTRAINT UQ_SalesOrders_No      UNIQUE (OrderNo),
    CONSTRAINT FK_SO_Customer   FOREIGN KEY (CustomerId)  REFERENCES Customers(Id),
    CONSTRAINT FK_SO_Quotation  FOREIGN KEY (QuotationId) REFERENCES Quotations(Id),
    CONSTRAINT FK_SO_Company    FOREIGN KEY (CompanyId)   REFERENCES Companies(Id),
    CONSTRAINT FK_SO_Branch     FOREIGN KEY (BranchId)    REFERENCES Branches(Id),
    CONSTRAINT FK_SO_SalesRep   FOREIGN KEY (SalesRepId)  REFERENCES Users(Id),
    CONSTRAINT FK_SO_Warehouse  FOREIGN KEY (WarehouseId) REFERENCES Warehouses(Id),
    CONSTRAINT FK_SO_CreatedBy  FOREIGN KEY (CreatedBy)   REFERENCES Users(Id),
    CONSTRAINT FK_SO_UpdatedBy  FOREIGN KEY (UpdatedBy)   REFERENCES Users(Id)
);
GO

CREATE TABLE SalesOrderLines (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    OrderId         UNIQUEIDENTIFIER NOT NULL,
    ProductId       UNIQUEIDENTIFIER NOT NULL,
    VariantId       UNIQUEIDENTIFIER NULL,
    Description     NVARCHAR(500)    NULL,
    OrderedQty      DECIMAL(18,4)    NOT NULL,
    DeliveredQty    DECIMAL(18,4)    NOT NULL DEFAULT 0,
    UomId           UNIQUEIDENTIFIER NOT NULL,
    UnitPrice       DECIMAL(18,4)    NOT NULL DEFAULT 0,
    DiscountPct     DECIMAL(5,2)     NOT NULL DEFAULT 0,
    DiscountAmount  DECIMAL(18,2)    NOT NULL DEFAULT 0,
    TaxPct          DECIMAL(5,2)     NOT NULL DEFAULT 0,
    TaxAmount       DECIMAL(18,2)    NOT NULL DEFAULT 0,
    LineTotal       DECIMAL(18,2)    NOT NULL DEFAULT 0,
    SortOrder       INT              NOT NULL DEFAULT 0,

    CONSTRAINT FK_SOL_Order   FOREIGN KEY (OrderId)   REFERENCES SalesOrders(Id),
    CONSTRAINT FK_SOL_Product FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT FK_SOL_Variant FOREIGN KEY (VariantId) REFERENCES ProductVariants(Id),
    CONSTRAINT FK_SOL_Uom     FOREIGN KEY (UomId)     REFERENCES UnitsOfMeasure(Id)
);
GO

CREATE INDEX IX_Quotations_CustomerId    ON Quotations(CustomerId);
CREATE INDEX IX_Quotations_Status        ON Quotations(Status);
CREATE INDEX IX_SalesOrders_CustomerId   ON SalesOrders(CustomerId);
CREATE INDEX IX_SalesOrders_Status       ON SalesOrders(Status);
CREATE INDEX IX_SalesOrders_OrderDate    ON SalesOrders(OrderDate);
CREATE INDEX IX_SalesOrders_PayStatus    ON SalesOrders(PaymentStatus);
GO

PRINT '016 — Sales Order & Quotation schema created successfully.';
GO
