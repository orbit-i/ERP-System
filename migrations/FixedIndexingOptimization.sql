USE OrbitERP;

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_PayrollSlips_PeriodId' AND object_id = OBJECT_ID('PayrollSlips'))
    CREATE INDEX IX_PayrollSlips_PeriodId ON PayrollSlips(PayrollPeriodId);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_PayrollSlips_Company_Period' AND object_id = OBJECT_ID('PayrollSlips'))
    CREATE INDEX IX_PayrollSlips_Company_Period ON PayrollSlips(CompanyId, PayrollPeriodId) INCLUDE (NetSalary, Status);

PRINT 'Task 25 — Fixed. All indexes complete.';
