USE OrbitERP;
GO

-- =============================================
-- TASK 6: Attendance Schema
-- ORBIT ERP — Human Resource Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE AttendanceShifts (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    Name            NVARCHAR(100)    NOT NULL,
    StartTime       TIME             NOT NULL,
    EndTime         TIME             NOT NULL,
    WorkingHours    DECIMAL(4,2)     NOT NULL DEFAULT 8,
    IsNightShift    BIT              NOT NULL DEFAULT 0,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_AttendanceShifts_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id)
);
GO

CREATE TABLE Attendance (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    EmployeeId      UNIQUEIDENTIFIER NOT NULL,
    ShiftId         UNIQUEIDENTIFIER NULL,
    CompanyId       UNIQUEIDENTIFIER NOT NULL,
    AttendanceDate  DATE             NOT NULL,
    CheckIn         DATETIME2        NULL,
    CheckOut        DATETIME2        NULL,
    WorkedHours     DECIMAL(4,2)     NOT NULL DEFAULT 0,
    OvertimeHours   DECIMAL(4,2)     NOT NULL DEFAULT 0,
    Status          NVARCHAR(50)     NOT NULL DEFAULT 'PRESENT',
                    -- PRESENT | ABSENT | LATE | HALF_DAY | ON_LEAVE | HOLIDAY
    Notes           NVARCHAR(500)    NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT UQ_Attendance UNIQUE (EmployeeId, AttendanceDate),
    CONSTRAINT FK_Attendance_Employee
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
    CONSTRAINT FK_Attendance_Shift
        FOREIGN KEY (ShiftId) REFERENCES AttendanceShifts(Id),
    CONSTRAINT FK_Attendance_Company
        FOREIGN KEY (CompanyId) REFERENCES Companies(Id)
);
GO

-- Indexes
CREATE INDEX IX_Attendance_EmployeeId ON Attendance(EmployeeId);
CREATE INDEX IX_Attendance_Date       ON Attendance(AttendanceDate);
CREATE INDEX IX_Attendance_Status     ON Attendance(Status);
GO

PRINT '006 — Attendance schema created successfully.';
GO
