USE master;
GO

IF DB_ID('OrbitERP') IS NOT NULL
BEGIN
    ALTER DATABASE OrbitERP SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE OrbitERP;
    PRINT 'OrbitERP dropped successfully.';
END
ELSE
BEGIN
    PRINT 'OrbitERP does not exist - skipping drop.';
END
GO

CREATE DATABASE OrbitERP
    COLLATE SQL_Latin1_General_CP1_CI_AS;
GO

PRINT '001 — OrbitERP database created successfully.';
GO
