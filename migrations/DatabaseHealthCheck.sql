USE OrbitERP;

-- Total table count
SELECT COUNT(*) AS TotalTables
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';

-- Migration history
SELECT MigrationId, Description, AppliedAt
FROM MigrationHistory
ORDER BY Id;

-- Health snapshot
SELECT * FROM DatabaseHealthLogs;
