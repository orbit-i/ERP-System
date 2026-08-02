using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OrbitERP.Application.Common.Interfaces;
using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Entities.HR;
using OrbitERP.Domain.Entities.Finance;
using OrbitERP.Domain.Entities.AI;
using OrbitERP.Domain.Entities.Reporting;
using OrbitERP.Domain.Entities.Inventory;
using OrbitERP.Domain.Entities.Procurement;
using OrbitERP.Domain.Entities.Sales;

namespace OrbitERP.Persistence.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Administration Module
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    // HR Module
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Designation> Designations => Set<Designation>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<AttendanceShift> AttendanceShifts => Set<AttendanceShift>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();
    public DbSet<LeaveBalance> LeaveBalances => Set<LeaveBalance>();
    public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
    public DbSet<SalaryComponent> SalaryComponents => Set<SalaryComponent>();
    public DbSet<EmployeeSalaryStructure> EmployeeSalaryStructures => Set<EmployeeSalaryStructure>();
    public DbSet<PayrollPeriod> PayrollPeriods => Set<PayrollPeriod>();
    public DbSet<PayrollSlip> PayrollSlips => Set<PayrollSlip>();
    public DbSet<PayrollSlipDetail> PayrollSlipDetails => Set<PayrollSlipDetail>();
    public DbSet<JobPosting> JobPostings => Set<JobPosting>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<Interview> Interviews => Set<Interview>();
    public DbSet<PerformanceTemplate> PerformanceTemplates => Set<PerformanceTemplate>();
    public DbSet<PerformanceCriteria> PerformanceCriteria => Set<PerformanceCriteria>();
    public DbSet<PerformanceReview> PerformanceReviews => Set<PerformanceReview>();
    public DbSet<PerformanceReviewDetail> PerformanceReviewDetails => Set<PerformanceReviewDetail>();
    public DbSet<Goal> Goals => Set<Goal>();

    // Inventory Module
    public DbSet<UnitOfMeasure> UnitsOfMeasure => Set<UnitOfMeasure>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<ProductAttribute> ProductAttributes => Set<ProductAttribute>();
    public DbSet<ProductAttributeValue> ProductAttributeValues => Set<ProductAttributeValue>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<StorageLocation> StorageLocations => Set<StorageLocation>();
    public DbSet<StockLevel> StockLevels => Set<StockLevel>();
    public DbSet<StockMovementType> StockMovementTypes => Set<StockMovementType>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();
    public DbSet<StockMovementLine> StockMovementLines => Set<StockMovementLine>();
    public DbSet<Barcode> Barcodes => Set<Barcode>();
    public DbSet<SerialNumber> SerialNumbers => Set<SerialNumber>();
    public DbSet<BatchNumber> BatchNumbers => Set<BatchNumber>();

    // Sales & CRM Module
    public DbSet<CustomerGroup> CustomerGroups => Set<CustomerGroup>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerAddress> CustomerAddresses => Set<CustomerAddress>();
    public DbSet<CustomerContact> CustomerContacts => Set<CustomerContact>();
    public DbSet<CRMStage> CRMStages => Set<CRMStage>();
    public DbSet<CRMLead> CRMLeads => Set<CRMLead>();
    public DbSet<Quotation> Quotations => Set<Quotation>();
    public DbSet<QuotationLine> QuotationLines => Set<QuotationLine>();
    public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
    public DbSet<SalesOrderLine> SalesOrderLines => Set<SalesOrderLine>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceLine> InvoiceLines => Set<InvoiceLine>();
    public DbSet<InvoicePayment> InvoicePayments => Set<InvoicePayment>();

    // Procurement Module
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<VendorContact> VendorContacts => Set<VendorContact>();
    public DbSet<VendorBankAccount> VendorBankAccounts => Set<VendorBankAccount>();
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<PurchaseOrderLine> PurchaseOrderLines => Set<PurchaseOrderLine>();
    public DbSet<VendorPayment> VendorPayments => Set<VendorPayment>();
    public DbSet<GoodsReceivingNote> GoodsReceivingNotes => Set<GoodsReceivingNote>();
    public DbSet<GoodsReceivingLine> GoodsReceivingLines => Set<GoodsReceivingLine>();
    public DbSet<GoodsReturnNote> GoodsReturnNotes => Set<GoodsReturnNote>();
    public DbSet<GoodsReturnLine> GoodsReturnLines => Set<GoodsReturnLine>();

    // Finance Module
    public DbSet<AccountsPayable> AccountsPayables => Set<AccountsPayable>();
    public DbSet<AccountsPayablePayment> AccountsPayablePayments => Set<AccountsPayablePayment>();
    public DbSet<AccountsReceivable> AccountsReceivables => Set<AccountsReceivable>();
    public DbSet<AccountsReceivablePayment> AccountsReceivablePayments => Set<AccountsReceivablePayment>();
    public DbSet<ChartOfAccount> ChartOfAccounts => Set<ChartOfAccount>();
    public DbSet<FiscalYear> FiscalYears => Set<FiscalYear>();
    public DbSet<AccountingPeriod> AccountingPeriods => Set<AccountingPeriod>();
    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
    public DbSet<JournalEntryLine> JournalEntryLines => Set<JournalEntryLine>();
    public DbSet<Budget> Budgets => Set<Budget>();
    public DbSet<BudgetLine> BudgetLines => Set<BudgetLine>();
    public DbSet<BudgetRevision> BudgetRevisions => Set<BudgetRevision>();
    public DbSet<BudgetAlert> BudgetAlerts => Set<BudgetAlert>();

    // AI Module
    public DbSet<AIConversation> AIConversations => Set<AIConversation>();
    public DbSet<AIMessage> AIMessages => Set<AIMessage>();
    public DbSet<AIInsight> AIInsights => Set<AIInsight>();
    public DbSet<AIReportRequest> AIReportRequests => Set<AIReportRequest>();
    public DbSet<AIPrediction> AIPredictions => Set<AIPrediction>();
    public DbSet<AIUsageLog> AIUsageLogs => Set<AIUsageLog>();

    // Reporting Module
    public DbSet<ReportTemplate> ReportTemplates => Set<ReportTemplate>();
    public DbSet<ReportSchedule> ReportSchedules => Set<ReportSchedule>();
    public DbSet<ReportExecutionLog> ReportExecutionLogs => Set<ReportExecutionLog>();
    public DbSet<Dashboard> Dashboards => Set<Dashboard>();
    public DbSet<DashboardWidget> DashboardWidgets => Set<DashboardWidget>();
    public DbSet<KPIMetric> KPIMetrics => Set<KPIMetric>();
    public DbSet<UserDashboardPreference> UserDashboardPreferences => Set<UserDashboardPreference>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}