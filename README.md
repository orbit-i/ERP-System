# ORBIT ERP — Database Schema Documentation

## Project Overview
ORBIT ERP is a centralized, cloud-based ERP platform for SMEs built on ASP.NET Core 9,
SQL Server 2022, and Entity Framework Core. This folder contains the complete database
schema for all modules.

---

## Environment Setup

| Item | Detail |
|---|---|
| Database | SQL Server 2022 |
| Host | Docker Container |
| Container Name | orbit-sqlserver |
| Server | localhost,1433 |
| Username | sa |
| Database Name | OrbitERP |
| GUI Tool | VS Code + mssql extension |

---

## Quick Start

### 1. Start the database container
```powershell
docker start orbit-sqlserver
2. Verify it is running
docker ps --filter name=orbit-sqlserver
3. Connect in VS Code
Open VS Code
Click SQL Server icon in left sidebar
Connect to localhost,1433 with sa credentials
Restore from Backup
If the container is lost or corrupted, restore from backup:

# Step 1 — Copy backup file into container
docker cp D:\OrbitERP\database\backups\OrbitERP_final.bak orbit-sqlserver:/var/opt/mssql/OrbitERP_final.bak

# Step 2 — Restore the database
docker exec orbit-sqlserver /opt/mssql-tools18/bin/sqlcmd `
  -S localhost -U sa -P "Orbit@2026" -No `
  -Q "RESTORE DATABASE OrbitERP FROM DISK = '/var/opt/mssql/OrbitERP_final.bak' WITH REPLACE"
Take a New Backup
# Step 1 — Backup inside container
docker exec orbit-sqlserver /opt/mssql-tools18/bin/sqlcmd `
  -S localhost -U sa -P "Orbit@2026" -No `
  -Q "BACKUP DATABASE OrbitERP TO DISK = '/var/opt/mssql/OrbitERP_new.bak' WITH FORMAT, INIT, STATS = 10"

# Step 2 — Copy to local drive
docker cp orbit-sqlserver:/var/opt/mssql/OrbitERP_new.bak D:\OrbitERP\database\backups\OrbitERP_new.bak
Migration Order
Run scripts in numerical order on a fresh database. Each script is idempotent-safe when run in sequence.

#	File	Module	Tables Created
001	001_DatabaseSetup.sql	Setup	OrbitERP database
002	002_UsersRolesPermissions.sql	Administration	Roles, Permissions, RolePermissions, Users, RefreshTokens
003	003_CompanyBranch.sql	Administration	Companies, Branches
004	004_AuditLog.sql	Administration	AuditLogs
005	005_EmployeeSchema.sql	HR	Departments, Designations, Employees
006	006_AttendanceSchema.sql	HR	AttendanceShifts, Attendance
007	007_LeaveSchema.sql	HR	LeaveTypes, LeaveBalances, LeaveRequests
008	008_PayrollSchema.sql	HR	SalaryComponents, EmployeeSalaryStructure, PayrollPeriods, PayrollSlips, PayrollSlipDetails
009	009_RecruitmentSchema.sql	HR	JobPostings, JobApplications, Interviews
010	010_PerformanceEvaluation.sql	HR	PerformanceCriteria, PerformanceReviews, PerformanceReviewDetails, PerformanceTemplates, Goals
011	011_ProductCategorySchema.sql	Inventory	UnitsOfMeasure, ProductCategories, Products, ProductVariants, ProductAttributes, ProductAttributeValues
012	012_WarehouseSchema.sql	Inventory	Warehouses, StorageLocations, StockLevels
013	013_StockMovementSchema.sql	Inventory	StockMovementTypes, StockMovements, StockMovementLines
014	014_BarcodeSchema.sql	Inventory	Barcodes, SerialNumbers, BatchNumbers
015	015_CustomerCRMSchema.sql	Sales & CRM	CustomerGroups, Customers, CustomerAddresses, CustomerContacts, CRMStages, CRMLeads
016	016_SalesOrderQuotationSchema.sql	Sales & CRM	Quotations, QuotationLines, SalesOrders, SalesOrderLines
017	017_InvoiceSchema.sql	Sales & CRM	Invoices, InvoiceLines, InvoicePayments, CreditNotes
018	018_VendorPurchaseOrderSchema.sql	Procurement	Vendors, VendorAddresses, VendorContacts, PurchaseOrders, PurchaseOrderLines
019	019_GoodsReceivingSchema.sql	Procurement	GoodsReceipts, GoodsReceiptLines, VendorBills, VendorPayments
020	020_AccountsPayableReceivable.sql	Finance	AccountTypes, Accounts, AccountsReceivable, AccountsPayable
021	021_GeneralLedgerSchema.sql	Finance	FiscalYears, AccountingPeriods, JournalEntries, JournalEntryLines, CostCenters
022	022_BudgetSchema.sql	Finance	Budgets, BudgetLines, BudgetRevisions, ExpenseClaims, ExpenseClaimLines
023	023_AILogsInsightsSchema.sql	AI	AIFeatureTypes, AIRequestLogs, AIInsights, AISalesPredictions, AIInventoryForecasts, AIChatSessions, AIChatMessages
024	024_ReportingDashboardSchema.sql	Reporting	ReportTemplates, SavedReports, ReportExecutionLogs, KPISnapshots, DashboardConfigs, DashboardWidgets
025	025_IndexingOptimization.sql	Optimization	40+ performance indexes across all modules
026	026_BackupMigrationStrategy.sql	Maintenance	MigrationHistory, BackupLog, DatabaseHealthLogs
Database Statistics
Metric	Count
Total Tables	70+
Total Migrations	26
Total Indexes	80+
Modules Covered	8
Module Summary
Administration — Multi-tenant architecture. Every table links to CompanyId. RBAC security with granular permissions. Full audit trail on all actions.

HR — Complete employee lifecycle. Attendance, leave approval workflow, payroll processing, recruitment pipeline, performance reviews.

Inventory — Product catalog with variants and attributes. Multi-warehouse with storage locations. Stock movements, barcode/serial/batch tracking.

Sales & CRM — Lead pipeline, quotations, sales orders, invoices with payment tracking and credit notes.

Procurement — Vendor management, purchase orders, goods receiving, vendor bills and payments.

Finance — Chart of accounts, double-entry general ledger, accounts payable/receivable, fiscal years, budgets with variance tracking.

AI — Request logging with token tracking, stored insights, sales predictions, inventory forecasts, chat session history.

Reporting — 10 pre-built system reports, KPI snapshots, scheduled reports, per-user customizable dashboards.