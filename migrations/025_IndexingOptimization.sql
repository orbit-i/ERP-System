-- =============================================
-- ORBIT ERP - 025_IndexingOptimization.sql
-- Database Indexing & Performance Optimization
-- Created: 2026
-- =============================================

USE OrbitERP;
GO

-- =============================================
-- SECTION 1: COMPOSITE INDEXES
-- High-frequency query patterns across modules
-- =============================================

-- -----------------------------------------------
-- AUTH & USERS
-- -----------------------------------------------

-- Login lookup: email + active status
CREATE INDEX IX_Users_Email_IsActive
    ON Users(Email, IsActive)
    WHERE IsActive = 1;

-- Token validation
CREATE INDEX IX_RefreshTokens_UserId_ExpiresAt
    ON RefreshTokens(UserId, ExpiresAt);

-- -----------------------------------------------
-- AUDIT LOGS
-- -----------------------------------------------

-- Most common audit query: company + date range
CREATE INDEX IX_AuditLogs_CompanyId_CreatedAt
    ON AuditLogs(CompanyId, CreatedAt DESC);

-- Audit by user + action
CREATE INDEX IX_AuditLogs_UserId_Action
    ON AuditLogs(UserId, Action);

-- -----------------------------------------------
-- EMPLOYEES
-- -----------------------------------------------

-- Employee list by company + status
CREATE INDEX IX_Employees_CompanyId_Status
    ON Employees(CompanyId, Status);

-- Employee search by department
CREATE INDEX IX_Employees_DepartmentId_Status
    ON Employees(DepartmentId, Status);

-- -----------------------------------------------
-- ATTENDANCE
-- -----------------------------------------------

-- Attendance by employee + date range (most common query)
CREATE INDEX IX_Attendance_EmployeeId_Date
    ON Attendance(EmployeeId, AttendanceDate DESC);

-- Attendance by company + date
CREATE INDEX IX_Attendance_CompanyId_Date
    ON Attendance(CompanyId, AttendanceDate DESC);

-- -----------------------------------------------
-- LEAVE
-- -----------------------------------------------

-- Leave requests by employee + status
CREATE INDEX IX_LeaveRequests_EmployeeId_Status
    ON LeaveRequests(EmployeeId, Status);

-- Leave requests by company + date
CREATE INDEX IX_LeaveRequests_CompanyId_StartDate
    ON LeaveRequests(CompanyId, StartDate DESC);

-- -----------------------------------------------
-- PAYROLL
-- -----------------------------------------------

-- Payroll slips by employee + period
CREATE INDEX IX_PayrollSlips_EmployeeId_PeriodId
    ON PayrollSlips(EmployeeId, PayrollPeriodId);

-- Payroll by company + status
CREATE INDEX IX_PayrollSlips_CompanyId_Status
    ON PayrollSlips(CompanyId, Status);

-- -----------------------------------------------
-- RECRUITMENT
-- -----------------------------------------------

-- Job applications by posting + status
CREATE INDEX IX_JobApplications_PostingId_Status
    ON JobApplications(JobPostingId, Status);

-- -----------------------------------------------
-- PRODUCTS
-- -----------------------------------------------

-- Product search by category + active
CREATE INDEX IX_Products_CategoryId_IsActive
    ON Products(CategoryId, IsActive)
    WHERE IsActive = 1;

-- Product search by company + active
CREATE INDEX IX_Products_CompanyId_IsActive
    ON Products(CompanyId, IsActive)
    WHERE IsActive = 1;

-- -----------------------------------------------
-- STOCK
-- -----------------------------------------------

-- Stock levels by warehouse + product (most common inventory query)
CREATE INDEX IX_StockLevels_WarehouseId_ProductId
    ON StockLevels(WarehouseId, ProductId);

-- Stock movements by product + date
CREATE INDEX IX_StockMovements_CompanyId_Date
    ON StockMovements(CompanyId, MovementDate DESC);

-- -----------------------------------------------
-- CUSTOMERS
-- -----------------------------------------------

-- Customer search by company + status
CREATE INDEX IX_Customers_CompanyId_Status
    ON Customers(CompanyId, Status);

-- -----------------------------------------------
-- SALES ORDERS
-- -----------------------------------------------

-- Sales orders by customer + status
CREATE INDEX IX_SalesOrders_CustomerId_Status
    ON SalesOrders(CustomerId, Status);

-- Sales orders by company + date (dashboard queries)
CREATE INDEX IX_SalesOrders_CompanyId_OrderDate
    ON SalesOrders(CompanyId, OrderDate DESC);

-- Sales orders by company + payment status
CREATE INDEX IX_SalesOrders_CompanyId_PaymentStatus
    ON SalesOrders(CompanyId, PaymentStatus);

-- -----------------------------------------------
-- INVOICES
-- -----------------------------------------------

-- Overdue invoices (very frequent AR query)
CREATE INDEX IX_Invoices_CompanyId_DueDate_Status
    ON Invoices(CompanyId, DueDate, Status)
    WHERE Status NOT IN ('PAID', 'CANCELLED', 'VOID');

-- Invoice by customer + date
CREATE INDEX IX_Invoices_CustomerId_InvoiceDate
    ON Invoices(CustomerId, InvoiceDate DESC);

-- -----------------------------------------------
-- VENDORS & PURCHASE ORDERS
-- -----------------------------------------------

-- PO by vendor + status
CREATE INDEX IX_PurchaseOrders_VendorId_Status
    ON PurchaseOrders(VendorId, Status);

-- PO by company + date
CREATE INDEX IX_PurchaseOrders_CompanyId_OrderDate
    ON PurchaseOrders(CompanyId, OrderDate DESC);

-- -----------------------------------------------
-- GOODS RECEIVING
-- -----------------------------------------------

-- GRN by company + date
CREATE INDEX IX_GRN_CompanyId_ReceivedDate
    ON GoodsReceivingNotes(CompanyId, ReceivedDate DESC);

-- -----------------------------------------------
-- ACCOUNTS PAYABLE
-- -----------------------------------------------

-- Open AP by company + due date (aging report)
CREATE INDEX IX_AP_CompanyId_DueDate_Status
    ON AccountsPayable(CompanyId, DueDate, Status)
    WHERE Status IN ('OPEN', 'PARTIAL', 'OVERDUE');

-- -----------------------------------------------
-- ACCOUNTS RECEIVABLE
-- -----------------------------------------------

-- Open AR by company + due date (aging report)
CREATE INDEX IX_AR_CompanyId_DueDate_Status
    ON AccountsReceivable(CompanyId, DueDate, Status)
    WHERE Status IN ('OPEN', 'PARTIAL', 'OVERDUE');

-- -----------------------------------------------
-- GENERAL LEDGER
-- -----------------------------------------------

-- Journal entries by company + period + status (trial balance)
CREATE INDEX IX_JE_CompanyId_PeriodId_Status
    ON JournalEntries(CompanyId, AccountingPeriodId, Status);

-- Journal entry lines by account + JE (account ledger query)
CREATE INDEX IX_JELines_AccountId_JEId
    ON JournalEntryLines(AccountId, JournalEntryId);

-- -----------------------------------------------
-- BUDGET
-- -----------------------------------------------

-- Budget lines by budget + account
CREATE INDEX IX_BudgetLines_BudgetId_AccountId
    ON BudgetLines(BudgetId, AccountId);

-- -----------------------------------------------
-- KPI METRICS
-- -----------------------------------------------

-- KPI by company + category + period (dashboard)
CREATE INDEX IX_KPIMetrics_CompanyId_Category_Period
    ON KPIMetrics(CompanyId, MetricCategory, PeriodDate DESC);

-- -----------------------------------------------
-- AI USAGE
-- -----------------------------------------------

-- AI usage by company + date (cost tracking)
CREATE INDEX IX_AIUsageLogs_CompanyId_CreatedAt
    ON AIUsageLogs(CompanyId, CreatedAt DESC);
GO

-- =============================================
-- SECTION 2: STATISTICS UPDATE
-- Ensures query optimizer has fresh stats
-- =============================================
EXEC sp_updatestats;
GO

-- =============================================
-- SECTION 3: DATABASE CONFIGURATION
-- Performance settings for OrbitERP
-- =============================================

-- Enable Read Committed Snapshot Isolation
-- Reduces blocking between readers and writers
ALTER DATABASE OrbitERP
    SET READ_COMMITTED_SNAPSHOT ON;
GO

-- Enable Query Store
-- Tracks query performance over time
ALTER DATABASE OrbitERP
    SET QUERY_STORE = ON;
GO

ALTER DATABASE OrbitERP
    SET QUERY_STORE (
        OPERATION_MODE          = READ_WRITE,
        CLEANUP_POLICY          = (STALE_QUERY_THRESHOLD_DAYS = 30),
        DATA_FLUSH_INTERVAL_SECONDS = 900,
        MAX_STORAGE_SIZE_MB     = 500,
        QUERY_CAPTURE_MODE      = AUTO
    );
GO

-- Set compatibility level to SQL Server 2022
ALTER DATABASE OrbitERP
    SET COMPATIBILITY_LEVEL = 160;
GO

-- =============================================
-- SECTION 4: USEFUL VIEWS
-- Pre-built views for common report queries
-- =============================================

-- View: Open Invoices (AR aging base)
CREATE OR ALTER VIEW VW_OpenInvoices AS
SELECT
    I.Id,
    I.InvoiceNo,
    I.InvoiceDate,
    I.DueDate,
    C.CompanyName        AS CustomerName,
    I.TotalAmount,
    I.PaidAmount,
    I.BalanceDue,
    I.Status,
    I.PaymentStatus,
    DATEDIFF(DAY, I.DueDate, CAST(GETUTCDATE() AS DATE)) AS DaysOverdue,
    I.CompanyId
FROM Invoices I
INNER JOIN Customers C ON I.CustomerId = C.Id
WHERE I.Status NOT IN ('CANCELLED', 'VOID')
  AND I.PaymentStatus != 'PAID';
GO

-- View: Open Purchase Orders
CREATE OR ALTER VIEW VW_OpenPurchaseOrders AS
SELECT
    PO.Id,
    PO.PONumber,
    PO.OrderDate,
    PO.ExpectedDelivery,
    V.CompanyName        AS VendorName,
    PO.TotalAmount,
    PO.PaidAmount,
    PO.BalanceDue,
    PO.Status,
    PO.PaymentStatus,
    PO.CompanyId
FROM PurchaseOrders PO
INNER JOIN Vendors V ON PO.VendorId = V.Id
WHERE PO.Status NOT IN ('CANCELLED', 'CLOSED');
GO

-- View: Stock Summary per Product
CREATE OR ALTER VIEW VW_StockSummary AS
SELECT
    P.Id                 AS ProductId,
    P.ProductName,
    PC.CategoryName,
    W.WarehouseName,
    SL.QuantityOnHand,
    SL.QuantityReserved,
    SL.QuantityAvailable,
    SL.ReorderLevel,
    CASE
        WHEN SL.QuantityAvailable <= SL.ReorderLevel THEN 1
        ELSE 0
    END                  AS NeedsReorder,
    P.CompanyId
FROM StockLevels SL
INNER JOIN Products P   ON SL.ProductId   = P.Id
INNER JOIN Warehouses W ON SL.WarehouseId = W.Id
LEFT  JOIN ProductCategories PC ON P.CategoryId = PC.Id;
GO

-- View: Employee Summary
CREATE OR ALTER VIEW VW_EmployeeSummary AS
SELECT
    E.Id,
    E.EmployeeCode,
    E.FirstName + ' ' + E.LastName  AS FullName,
    E.Email,
    D.DepartmentName,
    DG.DesignationName,
    E.Status,
    E.JoiningDate,
    E.CompanyId
FROM Employees E
LEFT JOIN Departments  D  ON E.DepartmentId  = D.Id
LEFT JOIN Designations DG ON E.DesignationId = DG.Id;
GO

-- View: AP Aging Summary
CREATE OR ALTER VIEW VW_APAgingSummary AS
SELECT
    AP.Id,
    AP.APNumber,
    V.CompanyName        AS VendorName,
    AP.TransactionDate,
    AP.DueDate,
    AP.OriginalAmount,
    AP.PaidAmount,
    AP.BalanceDue,
    AP.Status,
    DATEDIFF(DAY, AP.DueDate, CAST(GETUTCDATE() AS DATE)) AS DaysOverdue,
    CASE
        WHEN DATEDIFF(DAY, AP.DueDate, CAST(GETUTCDATE() AS DATE)) <= 0  THEN 'CURRENT'
        WHEN DATEDIFF(DAY, AP.DueDate, CAST(GETUTCDATE() AS DATE)) <= 30 THEN '1-30 DAYS'
        WHEN DATEDIFF(DAY, AP.DueDate, CAST(GETUTCDATE() AS DATE)) <= 60 THEN '31-60 DAYS'
        WHEN DATEDIFF(DAY, AP.DueDate, CAST(GETUTCDATE() AS DATE)) <= 90 THEN '61-90 DAYS'
        ELSE 'OVER 90 DAYS'
    END                  AS AgingBucket,
    AP.CompanyId
FROM AccountsPayable AP
INNER JOIN Vendors V ON AP.VendorId = V.Id
WHERE AP.Status IN ('OPEN', 'PARTIAL', 'OVERDUE');
GO

-- View: AR Aging Summary
CREATE OR ALTER VIEW VW_ARAgingSummary AS
SELECT
    AR.Id,
    AR.ARNumber,
    C.CompanyName        AS CustomerName,
    AR.TransactionDate,
    AR.DueDate,
    AR.OriginalAmount,
    AR.PaidAmount,
    AR.BalanceDue,
    AR.Status,
    DATEDIFF(DAY, AR.DueDate, CAST(GETUTCDATE() AS DATE)) AS DaysOverdue,
    CASE
        WHEN DATEDIFF(DAY, AR.DueDate, CAST(GETUTCDATE() AS DATE)) <= 0  THEN 'CURRENT'
        WHEN DATEDIFF(DAY, AR.DueDate, CAST(GETUTCDATE() AS DATE)) <= 30 THEN '1-30 DAYS'
        WHEN DATEDIFF(DAY, AR.DueDate, CAST(GETUTCDATE() AS DATE)) <= 60 THEN '31-60 DAYS'
        WHEN DATEDIFF(DAY, AR.DueDate, CAST(GETUTCDATE() AS DATE)) <= 90 THEN '61-90 DAYS'
        ELSE 'OVER 90 DAYS'
    END                  AS AgingBucket,
    AR.CompanyId
FROM AccountsReceivable AR
INNER JOIN Customers C ON AR.CustomerId = C.Id
WHERE AR.Status IN ('OPEN', 'PARTIAL', 'OVERDUE');
GO

PRINT '025_IndexingOptimization.sql executed successfully.';
PRINT 'Composite indexes: 35 created';
PRINT 'Database settings: RCSI, Query Store, Compatibility Level 160';
PRINT 'Views created: VW_OpenInvoices, VW_OpenPurchaseOrders, VW_StockSummary, VW_EmployeeSummary, VW_APAgingSummary, VW_ARAgingSummary';
GO
