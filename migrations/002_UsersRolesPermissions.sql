USE OrbitERP;
GO

-- =============================================
-- TASK 2: Users, Roles, Permissions Schema
-- ORBIT ERP — Administration Module
-- GUIDs for all PKs
-- =============================================

CREATE TABLE Roles (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Name            NVARCHAR(100)    NOT NULL,
    Description     NVARCHAR(500)    NULL,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT UQ_Roles_Name UNIQUE (Name)
);
GO

CREATE TABLE Permissions (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    Name            NVARCHAR(100)    NOT NULL,
    Code            NVARCHAR(100)    NOT NULL,
    Module          NVARCHAR(100)    NOT NULL,
    Description     NVARCHAR(500)    NULL,
    IsActive        BIT              NOT NULL DEFAULT 1,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT UQ_Permissions_Code UNIQUE (Code)
);
GO

CREATE TABLE RolePermissions (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    RoleId          UNIQUEIDENTIFIER NOT NULL,
    PermissionId    UNIQUEIDENTIFIER NOT NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT UQ_RolePermissions UNIQUE (RoleId, PermissionId),
    CONSTRAINT FK_RolePermissions_Role
        FOREIGN KEY (RoleId) REFERENCES Roles(Id),
    CONSTRAINT FK_RolePermissions_Permission
        FOREIGN KEY (PermissionId) REFERENCES Permissions(Id)
);
GO

CREATE TABLE Users (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    FirstName       NVARCHAR(100)    NOT NULL,
    LastName        NVARCHAR(100)    NOT NULL,
    Email           NVARCHAR(200)    NOT NULL,
    PasswordHash    NVARCHAR(500)    NOT NULL,
    PhoneNumber     NVARCHAR(50)     NULL,
    RoleId          UNIQUEIDENTIFIER NOT NULL,
    IsActive        BIT              NOT NULL DEFAULT 1,
    IsEmailVerified BIT              NOT NULL DEFAULT 0,
    LastLoginAt     DATETIME2        NULL,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2        NULL,

    CONSTRAINT UQ_Users_Email UNIQUE (Email),
    CONSTRAINT FK_Users_Role
        FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);
GO

CREATE TABLE RefreshTokens (
    Id              UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    UserId          UNIQUEIDENTIFIER NOT NULL,
    Token           NVARCHAR(500)    NOT NULL,
    ExpiresAt       DATETIME2        NOT NULL,
    IsRevoked       BIT              NOT NULL DEFAULT 0,
    CreatedAt       DATETIME2        NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_RefreshTokens_User
        FOREIGN KEY (UserId) REFERENCES Users(Id)
);
GO

-- Indexes
CREATE INDEX IX_Users_Email        ON Users(Email);
CREATE INDEX IX_Users_RoleId       ON Users(RoleId);
CREATE INDEX IX_RefreshTokens_User ON RefreshTokens(UserId);
GO

PRINT '002 — Users, Roles, Permissions schema created successfully.';
GO
