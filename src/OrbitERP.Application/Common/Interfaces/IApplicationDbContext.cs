using Microsoft.EntityFrameworkCore;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Entities.HR;
using OrbitERP.Domain.Entities.Finance;
using OrbitERP.Domain.Entities.AI;
using OrbitERP.Domain.Entities.Reporting;
using OrbitERP.Domain.Entities.Inventory;
using OrbitERP.Domain.Entities.Procurement;
using OrbitERP.Domain.Entities.Sales;

namespace OrbitERP.Application.Common.Interfaces;

/// <summary>
/// Defines the contract for the ApplicationDbContext that the Application layer uses.
/// This prevents the Application layer from depending directly on EF Core's concrete implementation.
/// <para>
/// Note: DbSet properties will be added here incrementally as we build out the modules.
/// </para>
/// </summary>
public interface IApplicationDbContext
{
    // Administration Module
    DbSet<Role> Roles { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<User> Users { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<Company> Companies { get; }
    DbSet<Branch> Branches { get; }
    DbSet<AuditLog> AuditLogs { get; }

    // HR Module
    DbSet<Department> Departments { get; }
    DbSet<Designation> Designations { get; }
    DbSet<Employee> Employees { get; }
    DbSet<AttendanceShift> AttendanceShifts { get; }
    DbSet<Attendance> Attendances { get; }
    DbSet<LeaveType> LeaveTypes { get; }
    DbSet<LeaveBalance> LeaveBalances { get; }
    DbSet<LeaveRequest> LeaveRequests { get; }
    DbSet<SalaryComponent> SalaryComponents { get; }
    DbSet<EmployeeSalaryStructure> EmployeeSalaryStructures { get; }
    DbSet<PayrollPeriod> PayrollPeriods { get; }
    DbSet<PayrollSlip> PayrollSlips { get; }
    DbSet<PayrollSlipDetail> PayrollSlipDetails { get; }
    DbSet<JobPosting> JobPostings { get; }
    DbSet<JobApplication> JobApplications { get; }
    DbSet<Interview> Interviews { get; }
    DbSet<PerformanceTemplate> PerformanceTemplates { get; }
    DbSet<PerformanceCriteria> PerformanceCriteria { get; }
    DbSet<PerformanceReview> PerformanceReviews { get; }
    DbSet<PerformanceReviewDetail> PerformanceReviewDetails { get; }
    DbSet<Goal> Goals { get; }

    // Inventory Module
    DbSet<UnitOfMeasure> UnitsOfMeasure { get; }
    DbSet<ProductCategory> ProductCategories { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductVariant> ProductVariants { get; }
    DbSet<ProductAttribute> ProductAttributes { get; }
    DbSet<ProductAttributeValue> ProductAttributeValues { get; }
    DbSet<Warehouse> Warehouses { get; }
    DbSet<StorageLocation> StorageLocations { get; }
    DbSet<StockLevel> StockLevels { get; }
    DbSet<StockMovementType> StockMovementTypes { get; }
    DbSet<StockMovement> StockMovements { get; }
    DbSet<StockMovementLine> StockMovementLines { get; }
    DbSet<Barcode> Barcodes { get; }
    DbSet<SerialNumber> SerialNumbers { get; }
    DbSet<BatchNumber> BatchNumbers { get; }

    // Sales & CRM Module
    DbSet<CustomerGroup> CustomerGroups { get; }
    DbSet<Customer> Customers { get; }
    DbSet<CustomerAddress> CustomerAddresses { get; }
    DbSet<CustomerContact> CustomerContacts { get; }
    DbSet<CRMStage> CRMStages { get; }
    DbSet<CRMLead> CRMLeads { get; }
    DbSet<Quotation> Quotations { get; }
    DbSet<QuotationLine> QuotationLines { get; }
    DbSet<SalesOrder> SalesOrders { get; }
    DbSet<SalesOrderLine> SalesOrderLines { get; }
    DbSet<Invoice> Invoices { get; }
    DbSet<InvoiceLine> InvoiceLines { get; }
    DbSet<InvoicePayment> InvoicePayments { get; }

    // Procurement Module
    DbSet<Vendor> Vendors { get; }
    DbSet<VendorContact> VendorContacts { get; }
    DbSet<VendorBankAccount> VendorBankAccounts { get; }
    DbSet<PurchaseOrder> PurchaseOrders { get; }
    DbSet<PurchaseOrderLine> PurchaseOrderLines { get; }
    DbSet<VendorPayment> VendorPayments { get; }
    DbSet<GoodsReceivingNote> GoodsReceivingNotes { get; }
    DbSet<GoodsReceivingLine> GoodsReceivingLines { get; }
    DbSet<GoodsReturnNote> GoodsReturnNotes { get; }
    DbSet<GoodsReturnLine> GoodsReturnLines { get; }

    // Finance Module
    DbSet<AccountsPayable> AccountsPayables { get; }
    DbSet<AccountsPayablePayment> AccountsPayablePayments { get; }
    DbSet<AccountsReceivable> AccountsReceivables { get; }
    DbSet<AccountsReceivablePayment> AccountsReceivablePayments { get; }
    DbSet<ChartOfAccount> ChartOfAccounts { get; }
    DbSet<FiscalYear> FiscalYears { get; }
    DbSet<AccountingPeriod> AccountingPeriods { get; }
    DbSet<JournalEntry> JournalEntries { get; }
    DbSet<JournalEntryLine> JournalEntryLines { get; }
    DbSet<Budget> Budgets { get; }
    DbSet<BudgetLine> BudgetLines { get; }
    DbSet<BudgetRevision> BudgetRevisions { get; }
    DbSet<BudgetAlert> BudgetAlerts { get; }

    // AI Module
    DbSet<AIConversation> AIConversations { get; }
    DbSet<AIMessage> AIMessages { get; }
    DbSet<AIInsight> AIInsights { get; }
    DbSet<AIReportRequest> AIReportRequests { get; }
    DbSet<AIPrediction> AIPredictions { get; }
    DbSet<AIUsageLog> AIUsageLogs { get; }

    // Reporting Module
    DbSet<ReportTemplate> ReportTemplates { get; }
    DbSet<ReportSchedule> ReportSchedules { get; }
    DbSet<ReportExecutionLog> ReportExecutionLogs { get; }
    DbSet<Dashboard> Dashboards { get; }
    DbSet<DashboardWidget> DashboardWidgets { get; }
    DbSet<KPIMetric> KPIMetrics { get; }
    DbSet<UserDashboardPreference> UserDashboardPreferences { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}