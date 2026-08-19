using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CRMStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Probability = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRMStages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Module = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockMovementTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovementTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitsOfMeasure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitsOfMeasure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LegalName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RegistrationNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AIConversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AIModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Module = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TotalMessages = table.Column<int>(type: "int", nullable: false),
                    TotalTokensUsed = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastMessageAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIConversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIConversations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AIConversations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AIInsights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    InsightType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DetailedContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AIModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Confidence = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Module = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReferenceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValidFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ValidUntil = table.Column<DateOnly>(type: "date", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneratedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DismissedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DismissedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIInsights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIInsights_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AIInsights_Users_DismissedBy",
                        column: x => x.DismissedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AIInsights_Users_GeneratedBy",
                        column: x => x.GeneratedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AIPredictions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PredictionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PredictionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PredictedValue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    ActualValue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    Accuracy = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: true),
                    Confidence = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: true),
                    AIModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModelVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InputData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIPredictions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AIReportRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ReportType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequestedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prompt = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    GeneratedSQL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneratedReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AIModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TokensUsed = table.Column<int>(type: "int", nullable: false),
                    ProcessingTimeMs = table.Column<int>(type: "int", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIReportRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIReportRequests_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AIReportRequests_Users_RequestedBy",
                        column: x => x.RequestedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AIUsageLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AIModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FeatureType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TokensInput = table.Column<int>(type: "int", nullable: false),
                    TokensOutput = table.Column<int>(type: "int", nullable: false),
                    TotalTokens = table.Column<int>(type: "int", nullable: false),
                    EstimatedCostUSD = table.Column<decimal>(type: "DECIMAL(10,6)", nullable: false),
                    ResponseTimeMs = table.Column<int>(type: "int", nullable: true),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    ErrorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIUsageLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIUsageLogs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AIUsageLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceShifts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    WorkingHours = table.Column<decimal>(type: "DECIMAL(4,2)", nullable: false),
                    IsNightShift = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceShifts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsHeadOffice = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChartOfAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AccountCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AccountSubType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalBalance = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsHeader = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSystemAccount = table.Column<bool>(type: "bit", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartOfAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChartOfAccounts_ChartOfAccounts_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "ChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartOfAccounts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Discount = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerGroups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dashboards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DashboardName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DashboardCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    LayoutConfig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dashboards_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dashboards_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiscalYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    FiscalYearName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiscalYears_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaxDaysPerYear = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayrollPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollPeriods_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceTemplates_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategories_ProductCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ReportName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ReportCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReportCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReportType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReportSQL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilterConfig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnConfig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChartConfig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    ExportFormats = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTemplates_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalaryComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CalculationType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DefaultValue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryComponents_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AIMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokensUsed = table.Column<int>(type: "int", nullable: false),
                    ResponseTimeMs = table.Column<int>(type: "int", nullable: true),
                    IsError = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIMessages_AIConversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "AIConversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Module = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KPIMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    MetricName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MetricCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MetricCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MetricValue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PreviousValue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    TargetValue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    ChangePercent = table.Column<decimal>(type: "DECIMAL(8,2)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PeriodType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PeriodDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPIMetrics_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KPIMetrics_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    VendorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AlternatePhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegistrationNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VendorType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreditLimit = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaymentTerms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendors_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendors_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CustomerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreditLimit = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    CreditDays = table.Column<int>(type: "int", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "CustomerGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DashboardWidgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DashboardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WidgetName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WidgetType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DataSource = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RefreshInterval = table.Column<int>(type: "int", nullable: false),
                    PositionX = table.Column<int>(type: "int", nullable: false),
                    PositionY = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Config = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilterConfig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardWidgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DashboardWidgets_Dashboards_DashboardId",
                        column: x => x.DashboardId,
                        principalTable: "Dashboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDashboardPreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DashboardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CustomLayout = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDashboardPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDashboardPreferences_Dashboards_DashboardId",
                        column: x => x.DashboardId,
                        principalTable: "Dashboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDashboardPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountingPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    FiscalYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PeriodNumber = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountingPeriods_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingPeriods_FiscalYears_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BudgetName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BudgetCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FiscalYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BudgetType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TotalBudgetAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TotalActualAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TotalVariance = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ApprovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RejectedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Budgets_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Budgets_FiscalYears_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Budgets_Users_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Budgets_Users_RejectedBy",
                        column: x => x.RejectedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceCriteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MaxScore = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceCriteria_PerformanceTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "PerformanceTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ProductType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CostPrice = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    ReorderLevel = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    ReorderQty = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsSerialized = table.Column<bool>(type: "bit", nullable: false),
                    IsBatchTracked = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitsOfMeasure_UomId",
                        column: x => x.UomId,
                        principalTable: "UnitsOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ReportTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: true),
                    DayOfMonth = table.Column<int>(type: "int", nullable: true),
                    RunTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ExportFormat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Recipients = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastRunAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextRunAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportSchedules_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportSchedules_ReportTemplates_ReportTemplateId",
                        column: x => x.ReportTemplateId,
                        principalTable: "ReportTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Designations_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PONumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpectedDelivery = table.Column<DateOnly>(type: "date", nullable: true),
                    ActualDelivery = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SubTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    BalanceDue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PaymentTerms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TermsConditions = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorBankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AccountTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorBankAccounts_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorContacts_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MovementTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MovementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfirmedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConfirmedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockMovements_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_StockMovementTypes_MovementTypeId",
                        column: x => x.MovementTypeId,
                        principalTable: "StockMovementTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_Users_ConfirmedBy",
                        column: x => x.ConfirmedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_Warehouses_FromWarehouseId",
                        column: x => x.FromWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_Warehouses_ToWarehouseId",
                        column: x => x.ToWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StorageLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LocationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageLocations_StorageLocations_ParentId",
                        column: x => x.ParentId,
                        principalTable: "StorageLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StorageLocations_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CRMLeads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpectedValue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    ExpectedCloseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AssignedTo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LostReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRMLeads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CRMLeads_CRMStages_StageId",
                        column: x => x.StageId,
                        principalTable: "CRMStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CRMLeads_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CRMLeads_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CRMLeads_Users_AssignedTo",
                        column: x => x.AssignedTo,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    JournalNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PostingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FiscalYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountingPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReferenceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TotalDebit = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TotalCredit = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    IsBalanced = table.Column<bool>(type: "bit", nullable: false),
                    IsReversed = table.Column<bool>(type: "bit", nullable: false),
                    ReversedByJEId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PostedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PostedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntries_AccountingPeriods_AccountingPeriodId",
                        column: x => x.AccountingPeriodId,
                        principalTable: "AccountingPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntries_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntries_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntries_FiscalYears_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntries_JournalEntries_ReversedByJEId",
                        column: x => x.ReversedByJEId,
                        principalTable: "JournalEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntries_Users_PostedBy",
                        column: x => x.PostedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountingPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BudgetedAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    ActualAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Variance = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    VariancePercent = table.Column<decimal>(type: "DECIMAL(8,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetLines_AccountingPeriods_AccountingPeriodId",
                        column: x => x.AccountingPeriodId,
                        principalTable: "AccountingPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetLines_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetLines_ChartOfAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "ChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetRevisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RevisionNumber = table.Column<int>(type: "int", nullable: false),
                    RevisionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PreviousAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    RevisedAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ApprovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetRevisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetRevisions_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetRevisions_Users_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetRevisions_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_ProductAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CostPrice = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportExecutionLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ReportTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExecutedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReportScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExecutedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionTimeMs = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExportFormat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ResultRowCount = table.Column<int>(type: "int", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FilterParams = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportExecutionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportExecutionLogs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportExecutionLogs_ReportSchedules_ReportScheduleId",
                        column: x => x.ReportScheduleId,
                        principalTable: "ReportSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportExecutionLogs_ReportTemplates_ReportTemplateId",
                        column: x => x.ReportTemplateId,
                        principalTable: "ReportTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportExecutionLogs_Users_ExecutedBy",
                        column: x => x.ExecutedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EmployeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DesignationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    JoiningDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TerminationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EmploymentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfilePhotoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BasicSalary = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankAccountNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Designations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobPostings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DesignationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SalaryMin = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    SalaryMax = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    Vacancies = table.Column<int>(type: "int", nullable: false),
                    PostedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ClosingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobPostings_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPostings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPostings_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPostings_Designations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceivingNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    GRNNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DeliveryNote = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InvoiceReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QualityStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TotalQuantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    TotalValue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceivedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InspectedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InspectedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceivingNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceivingNotes_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivingNotes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivingNotes_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivingNotes_Users_InspectedBy",
                        column: x => x.InspectedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivingNotes_Users_ReceivedBy",
                        column: x => x.ReceivedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivingNotes_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivingNotes_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Quantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    ReceivedQuantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TaxPercent = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    UomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLines_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLines_UnitsOfMeasure_UomId",
                        column: x => x.UomId,
                        principalTable: "UnitsOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ChequeNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorPayments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorPayments_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorPayments_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quotations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    QuotationNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuotationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TermsConditions = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotations_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotations_CRMLeads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "CRMLeads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    JournalEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DebitAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntryLines_ChartOfAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "ChartOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntryLines_JournalEntries_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetAlerts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BudgetLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AlertType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ThresholdPercent = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    CurrentPercent = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    IsTriggered = table.Column<bool>(type: "bit", nullable: false),
                    TriggeredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAcknowledged = table.Column<bool>(type: "bit", nullable: false),
                    AcknowledgedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AcknowledgedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetAlerts_BudgetLines_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "BudgetLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetAlerts_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetAlerts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetAlerts_Users_AcknowledgedBy",
                        column: x => x.AcknowledgedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Barcodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BarcodeValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BarcodeType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barcodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Barcodes_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Barcodes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Barcodes_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BatchNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatchNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    ManufactureDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchNumbers_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchNumbers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchNumbers_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SerialNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PurchaseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerialNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerialNumbers_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SerialNumbers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SerialNumbers_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    ReservedQty = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    AvailableQty = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false, computedColumnSql: "[Quantity] - [ReservedQty]"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockLevels_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockLevels_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockLevels_StorageLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "StorageLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockLevels_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockMovementLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    MovementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    UomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostPrice = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    BatchNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovementLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockMovementLines_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovementLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovementLines_StockMovements_MovementId",
                        column: x => x.MovementId,
                        principalTable: "StockMovements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMovementLines_StorageLocations_FromLocationId",
                        column: x => x.FromLocationId,
                        principalTable: "StorageLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovementLines_StorageLocations_ToLocationId",
                        column: x => x.ToLocationId,
                        principalTable: "StorageLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovementLines_UnitsOfMeasure_UomId",
                        column: x => x.UomId,
                        principalTable: "UnitsOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkedHours = table.Column<decimal>(type: "DECIMAL(4,2)", nullable: false),
                    OvertimeHours = table.Column<decimal>(type: "DECIMAL(4,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_AttendanceShifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "AttendanceShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendance_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendance_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryStructure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    EffectiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryStructure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryStructure_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryStructure_SalaryComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "SalaryComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TargetDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CompletionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Goals_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveBalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    TotalDays = table.Column<decimal>(type: "DECIMAL(5,1)", nullable: false),
                    UsedDays = table.Column<decimal>(type: "DECIMAL(5,1)", nullable: false),
                    RemainingDays = table.Column<decimal>(type: "DECIMAL(5,1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveBalances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveBalances_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalDays = table.Column<decimal>(type: "DECIMAL(5,1)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApprovedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RejectedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Users_RejectedById",
                        column: x => x.RejectedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayrollSlips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PayrollPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TotalEarnings = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TotalDeductions = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    NetSalary = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    WorkingDays = table.Column<int>(type: "int", nullable: false),
                    PresentDays = table.Column<int>(type: "int", nullable: false),
                    AbsentDays = table.Column<int>(type: "int", nullable: false),
                    OvertimeHours = table.Column<decimal>(type: "DECIMAL(4,2)", nullable: false),
                    OvertimeAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollSlips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollSlips_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollSlips_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollSlips_PayrollPeriods_PayrollPeriodId",
                        column: x => x.PayrollPeriodId,
                        principalTable: "PayrollPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewPeriod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReviewDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReviewedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalScore = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    MaxScore = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    ScorePercent = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_PerformanceTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "PerformanceTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_Users_ReviewedById",
                        column: x => x.ReviewedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    JobPostingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResumeUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CoverLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentSalary = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    ExpectedSalary = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    NoticePeriod = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AppliedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplications_JobPostings_JobPostingId",
                        column: x => x.JobPostingId,
                        principalTable: "JobPostings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountsPayable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    APNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GRNId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TransactionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OriginalAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    BalanceDue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    VendorInvoiceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsPayable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsPayable_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountsPayable_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountsPayable_GoodsReceivingNotes_GRNId",
                        column: x => x.GRNId,
                        principalTable: "GoodsReceivingNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountsPayable_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountsPayable_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReturnNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ReturnNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GRNId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalQuantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    TotalValue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReturnNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReturnNotes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReturnNotes_GoodsReceivingNotes_GRNId",
                        column: x => x.GRNId,
                        principalTable: "GoodsReceivingNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReturnNotes_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceivingLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    GRNId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OrderedQuantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    ReceivedQuantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    AcceptedQuantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    RejectedQuantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    UomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BatchNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    QualityStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    QualityNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceivingLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceivingLines_GoodsReceivingNotes_GRNId",
                        column: x => x.GRNId,
                        principalTable: "GoodsReceivingNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsReceivingLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivingLines_PurchaseOrderLines_PurchaseOrderLineId",
                        column: x => x.PurchaseOrderLineId,
                        principalTable: "PurchaseOrderLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivingLines_StorageLocations_StorageLocationId",
                        column: x => x.StorageLocationId,
                        principalTable: "StorageLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivingLines_UnitsOfMeasure_UomId",
                        column: x => x.UomId,
                        principalTable: "UnitsOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuotationLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    QuotationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Quantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    UomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    DiscountPct = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TaxPct = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationLines_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationLines_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationLines_UnitsOfMeasure_UomId",
                        column: x => x.UomId,
                        principalTable: "UnitsOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    OrderNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuotationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DeliveryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SubTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TermsConditions = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SalesRepId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Users_SalesRepId",
                        column: x => x.SalesRepId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayrollSlipDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PayrollSlipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollSlipDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollSlipDetails_PayrollSlips_PayrollSlipId",
                        column: x => x.PayrollSlipId,
                        principalTable: "PayrollSlips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollSlipDetails_SalaryComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "SalaryComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceReviewDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriteriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviewDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceReviewDetails_PerformanceCriteria_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "PerformanceCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceReviewDetails_PerformanceReviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "PerformanceReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InterviewType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ScheduledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MeetingLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InterviewerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interviews_JobApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "JobApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interviews_Users_InterviewerId",
                        column: x => x.InterviewerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountsPayablePayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    APId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ChequeNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsPayablePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsPayablePayments_AccountsPayable_APId",
                        column: x => x.APId,
                        principalTable: "AccountsPayable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountsPayablePayments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReturnLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ReturnNoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GRNLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReturnLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReturnLines_GoodsReceivingLines_GRNLineId",
                        column: x => x.GRNLineId,
                        principalTable: "GoodsReceivingLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReturnLines_GoodsReturnNotes_ReturnNoteId",
                        column: x => x.ReturnNoteId,
                        principalTable: "GoodsReturnNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsReturnLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    InvoiceNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InvoiceType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InvoiceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    BalanceDue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaymentTerms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BillingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ShippingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TermsConditions = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OrderedQty = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    DeliveredQty = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    UomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    DiscountPct = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TaxPct = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderLines_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLines_SalesOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrderLines_UnitsOfMeasure_UomId",
                        column: x => x.UomId,
                        principalTable: "UnitsOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountsReceivable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ARNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SalesOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TransactionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OriginalAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    BalanceDue = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsReceivable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsReceivable_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountsReceivable_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountsReceivable_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountsReceivable_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountsReceivable_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Quantity = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TaxPercent = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePayments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountsReceivablePayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ARId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ChequeNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsReceivablePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsReceivablePayments_AccountsReceivable_ARId",
                        column: x => x.ARId,
                        principalTable: "AccountsReceivable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountsReceivablePayments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingPeriods_CompanyId",
                table: "AccountingPeriods",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingPeriods_FiscalYearId",
                table: "AccountingPeriods",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingPeriods_Status",
                table: "AccountingPeriods",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayable_APNumber",
                table: "AccountsPayable",
                column: "APNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayable_BranchId",
                table: "AccountsPayable",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayable_CompanyId",
                table: "AccountsPayable",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayable_DueDate",
                table: "AccountsPayable",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayable_GRNId",
                table: "AccountsPayable",
                column: "GRNId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayable_PurchaseOrderId",
                table: "AccountsPayable",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayable_Status",
                table: "AccountsPayable",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayable_TransactionDate",
                table: "AccountsPayable",
                column: "TransactionDate");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayable_VendorId",
                table: "AccountsPayable",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayablePayments_APId",
                table: "AccountsPayablePayments",
                column: "APId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayablePayments_CompanyId",
                table: "AccountsPayablePayments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayablePayments_PaymentDate",
                table: "AccountsPayablePayments",
                column: "PaymentDate");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivable_ARNumber",
                table: "AccountsReceivable",
                column: "ARNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivable_BranchId",
                table: "AccountsReceivable",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivable_CompanyId",
                table: "AccountsReceivable",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivable_CustomerId",
                table: "AccountsReceivable",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivable_DueDate",
                table: "AccountsReceivable",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivable_InvoiceId",
                table: "AccountsReceivable",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivable_SalesOrderId",
                table: "AccountsReceivable",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivable_Status",
                table: "AccountsReceivable",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivable_TransactionDate",
                table: "AccountsReceivable",
                column: "TransactionDate");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivablePayments_ARId",
                table: "AccountsReceivablePayments",
                column: "ARId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivablePayments_CompanyId",
                table: "AccountsReceivablePayments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsReceivablePayments_PaymentDate",
                table: "AccountsReceivablePayments",
                column: "PaymentDate");

            migrationBuilder.CreateIndex(
                name: "IX_AIConversations_CompanyId",
                table: "AIConversations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AIConversations_CreatedAt",
                table: "AIConversations",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AIConversations_SessionId",
                table: "AIConversations",
                column: "SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AIConversations_UserId",
                table: "AIConversations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AIInsights_CompanyId",
                table: "AIInsights",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AIInsights_DismissedBy",
                table: "AIInsights",
                column: "DismissedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AIInsights_GeneratedBy",
                table: "AIInsights",
                column: "GeneratedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AIInsights_InsightType",
                table: "AIInsights",
                column: "InsightType");

            migrationBuilder.CreateIndex(
                name: "IX_AIInsights_Priority",
                table: "AIInsights",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_AIInsights_Status",
                table: "AIInsights",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AIInsights_ValidUntil",
                table: "AIInsights",
                column: "ValidUntil");

            migrationBuilder.CreateIndex(
                name: "IX_AIMessages_ConversationId",
                table: "AIMessages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_AIMessages_CreatedAt",
                table: "AIMessages",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AIPredictions_CompanyId",
                table: "AIPredictions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AIPredictions_EntityId",
                table: "AIPredictions",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AIPredictions_PredictionDate",
                table: "AIPredictions",
                column: "PredictionDate");

            migrationBuilder.CreateIndex(
                name: "IX_AIPredictions_PredictionType",
                table: "AIPredictions",
                column: "PredictionType");

            migrationBuilder.CreateIndex(
                name: "IX_AIReportRequests_CompanyId",
                table: "AIReportRequests",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AIReportRequests_RequestedBy",
                table: "AIReportRequests",
                column: "RequestedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AIReportRequests_Status",
                table: "AIReportRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AIUsageLogs_CompanyId",
                table: "AIUsageLogs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AIUsageLogs_CreatedAt",
                table: "AIUsageLogs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AIUsageLogs_UserId",
                table: "AIUsageLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_CompanyId",
                table: "Attendance",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_EmployeeId_AttendanceDate",
                table: "Attendance",
                columns: new[] { "EmployeeId", "AttendanceDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_ShiftId",
                table: "Attendance",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceShifts_CompanyId",
                table: "AttendanceShifts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_BranchId",
                table: "AuditLogs",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_CompanyId",
                table: "AuditLogs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_BarcodeValue",
                table: "Barcodes",
                column: "BarcodeValue",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_CreatedBy",
                table: "Barcodes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_ProductId",
                table: "Barcodes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_VariantId",
                table: "Barcodes",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchNumbers_BatchNo_ProductId_WarehouseId",
                table: "BatchNumbers",
                columns: new[] { "BatchNo", "ProductId", "WarehouseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BatchNumbers_ExpiryDate",
                table: "BatchNumbers",
                column: "ExpiryDate");

            migrationBuilder.CreateIndex(
                name: "IX_BatchNumbers_ProductId",
                table: "BatchNumbers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchNumbers_VariantId",
                table: "BatchNumbers",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchNumbers_WarehouseId",
                table: "BatchNumbers",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_Code_CompanyId",
                table: "Branches",
                columns: new[] { "Code", "CompanyId" },
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CompanyId",
                table: "Branches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAlerts_AcknowledgedBy",
                table: "BudgetAlerts",
                column: "AcknowledgedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAlerts_BudgetId",
                table: "BudgetAlerts",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAlerts_BudgetLineId",
                table: "BudgetAlerts",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAlerts_CompanyId",
                table: "BudgetAlerts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAlerts_IsTriggered",
                table: "BudgetAlerts",
                column: "IsTriggered");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLines_AccountId",
                table: "BudgetLines",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLines_AccountingPeriodId",
                table: "BudgetLines",
                column: "AccountingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLines_BudgetId",
                table: "BudgetLines",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetRevisions_ApprovedBy",
                table: "BudgetRevisions",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetRevisions_BudgetId",
                table: "BudgetRevisions",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetRevisions_CreatedBy",
                table: "BudgetRevisions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ApprovedBy",
                table: "Budgets",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_BranchId",
                table: "Budgets",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_BudgetCode_CompanyId",
                table: "Budgets",
                columns: new[] { "BudgetCode", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_BudgetType",
                table: "Budgets",
                column: "BudgetType");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CompanyId",
                table: "Budgets",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_FiscalYearId",
                table: "Budgets",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_RejectedBy",
                table: "Budgets",
                column: "RejectedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_Status",
                table: "Budgets",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccounts_AccountCode_CompanyId",
                table: "ChartOfAccounts",
                columns: new[] { "AccountCode", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccounts_AccountType",
                table: "ChartOfAccounts",
                column: "AccountType");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccounts_CompanyId",
                table: "ChartOfAccounts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccounts_IsActive",
                table: "ChartOfAccounts",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccounts_ParentAccountId",
                table: "ChartOfAccounts",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatedBy",
                table: "Companies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CRMLeads_AssignedTo",
                table: "CRMLeads",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_CRMLeads_CompanyId",
                table: "CRMLeads",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMLeads_CustomerId",
                table: "CRMLeads",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMLeads_StageId",
                table: "CRMLeads",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_CRMLeads_Status",
                table: "CRMLeads",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerId",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroups_CompanyId",
                table: "CustomerGroups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BranchId",
                table: "Customers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CompanyId",
                table: "Customers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerCode",
                table: "Customers",
                column: "CustomerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GroupId",
                table: "Customers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_CompanyId",
                table: "Dashboards",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_DashboardCode_CompanyId",
                table: "Dashboards",
                columns: new[] { "DashboardCode", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_OwnerId",
                table: "Dashboards",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardWidgets_DashboardId",
                table: "DashboardWidgets",
                column: "DashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_BranchId",
                table: "Departments",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Designations_CompanyId",
                table: "Designations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Designations_DepartmentId",
                table: "Designations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationId",
                table: "Employees",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeCode",
                table: "Employees",
                column: "EmployeeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryStructure_ComponentId",
                table: "EmployeeSalaryStructure",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryStructure_EmployeeId",
                table: "EmployeeSalaryStructure",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_CompanyId",
                table: "FiscalYears",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_FiscalYearName_CompanyId",
                table: "FiscalYears",
                columns: new[] { "FiscalYearName", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_IsCurrent",
                table: "FiscalYears",
                column: "IsCurrent");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_CompanyId",
                table: "Goals",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_EmployeeId",
                table: "Goals",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_Status",
                table: "Goals",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingLines_GRNId",
                table: "GoodsReceivingLines",
                column: "GRNId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingLines_ProductId",
                table: "GoodsReceivingLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingLines_PurchaseOrderLineId",
                table: "GoodsReceivingLines",
                column: "PurchaseOrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingLines_StorageLocationId",
                table: "GoodsReceivingLines",
                column: "StorageLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingLines_UomId",
                table: "GoodsReceivingLines",
                column: "UomId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingNotes_BranchId",
                table: "GoodsReceivingNotes",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingNotes_CompanyId",
                table: "GoodsReceivingNotes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingNotes_GRNNumber",
                table: "GoodsReceivingNotes",
                column: "GRNNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingNotes_InspectedBy",
                table: "GoodsReceivingNotes",
                column: "InspectedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingNotes_PurchaseOrderId",
                table: "GoodsReceivingNotes",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingNotes_ReceivedBy",
                table: "GoodsReceivingNotes",
                column: "ReceivedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingNotes_ReceivedDate",
                table: "GoodsReceivingNotes",
                column: "ReceivedDate");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingNotes_Status",
                table: "GoodsReceivingNotes",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingNotes_VendorId",
                table: "GoodsReceivingNotes",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivingNotes_WarehouseId",
                table: "GoodsReceivingNotes",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReturnLines_GRNLineId",
                table: "GoodsReturnLines",
                column: "GRNLineId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReturnLines_ProductId",
                table: "GoodsReturnLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReturnLines_ReturnNoteId",
                table: "GoodsReturnLines",
                column: "ReturnNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReturnNotes_CompanyId",
                table: "GoodsReturnNotes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReturnNotes_GRNId",
                table: "GoodsReturnNotes",
                column: "GRNId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReturnNotes_ReturnNumber",
                table: "GoodsReturnNotes",
                column: "ReturnNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReturnNotes_Status",
                table: "GoodsReturnNotes",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReturnNotes_VendorId",
                table: "GoodsReturnNotes",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_ApplicationId",
                table: "Interviews",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_InterviewerId",
                table: "Interviews",
                column: "InterviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_InvoiceId",
                table: "InvoiceLines",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_ProductId",
                table: "InvoiceLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayments_InvoiceId",
                table: "InvoicePayments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayments_PaymentDate",
                table: "InvoicePayments",
                column: "PaymentDate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BranchId",
                table: "Invoices",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CompanyId",
                table: "Invoices",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DueDate",
                table: "Invoices",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceDate",
                table: "Invoices",
                column: "InvoiceDate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNo",
                table: "Invoices",
                column: "InvoiceNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentStatus",
                table: "Invoices",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SalesOrderId",
                table: "Invoices",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Status",
                table: "Invoices",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobPostingId",
                table: "JobApplications",
                column: "JobPostingId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_Status",
                table: "JobApplications",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_BranchId",
                table: "JobPostings",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_CompanyId",
                table: "JobPostings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_DepartmentId",
                table: "JobPostings",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_DesignationId",
                table: "JobPostings",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_Status",
                table: "JobPostings",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_AccountingPeriodId",
                table: "JournalEntries",
                column: "AccountingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_BranchId",
                table: "JournalEntries",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_CompanyId",
                table: "JournalEntries",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_EntryDate",
                table: "JournalEntries",
                column: "EntryDate");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_EntryType",
                table: "JournalEntries",
                column: "EntryType");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_FiscalYearId",
                table: "JournalEntries",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_JournalNumber",
                table: "JournalEntries",
                column: "JournalNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_PostedBy",
                table: "JournalEntries",
                column: "PostedBy");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_ReferenceId",
                table: "JournalEntries",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_ReversedByJEId",
                table: "JournalEntries",
                column: "ReversedByJEId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_Status",
                table: "JournalEntries",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLines_AccountId",
                table: "JournalEntryLines",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLines_JournalEntryId",
                table: "JournalEntryLines",
                column: "JournalEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIMetrics_BranchId",
                table: "KPIMetrics",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIMetrics_CompanyId",
                table: "KPIMetrics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIMetrics_MetricCategory",
                table: "KPIMetrics",
                column: "MetricCategory");

            migrationBuilder.CreateIndex(
                name: "IX_KPIMetrics_MetricCode",
                table: "KPIMetrics",
                column: "MetricCode");

            migrationBuilder.CreateIndex(
                name: "IX_KPIMetrics_MetricCode_PeriodDate_CompanyId",
                table: "KPIMetrics",
                columns: new[] { "MetricCode", "PeriodDate", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KPIMetrics_PeriodDate",
                table: "KPIMetrics",
                column: "PeriodDate");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveBalances_EmployeeId_LeaveTypeId_Year",
                table: "LeaveBalances",
                columns: new[] { "EmployeeId", "LeaveTypeId", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveBalances_LeaveTypeId",
                table: "LeaveBalances",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ApprovedById",
                table: "LeaveRequests",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_CompanyId",
                table: "LeaveRequests",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_RejectedById",
                table: "LeaveRequests",
                column: "RejectedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTypes_CompanyId",
                table: "LeaveTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollPeriods_CompanyId",
                table: "PayrollPeriods",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollSlipDetails_ComponentId",
                table: "PayrollSlipDetails",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollSlipDetails_PayrollSlipId",
                table: "PayrollSlipDetails",
                column: "PayrollSlipId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollSlips_CompanyId",
                table: "PayrollSlips",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollSlips_EmployeeId",
                table: "PayrollSlips",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollSlips_PayrollPeriodId",
                table: "PayrollSlips",
                column: "PayrollPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollSlips_PayrollPeriodId_EmployeeId",
                table: "PayrollSlips",
                columns: new[] { "PayrollPeriodId", "EmployeeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayrollSlips_Status",
                table: "PayrollSlips",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceCriteria_TemplateId",
                table: "PerformanceCriteria",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviewDetails_CriteriaId",
                table: "PerformanceReviewDetails",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviewDetails_ReviewId",
                table: "PerformanceReviewDetails",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_CompanyId",
                table: "PerformanceReviews",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_EmployeeId",
                table: "PerformanceReviews",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_ReviewedById",
                table: "PerformanceReviews",
                column: "ReviewedById");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_Status",
                table: "PerformanceReviews",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_TemplateId",
                table: "PerformanceReviews",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceTemplates_CompanyId",
                table: "PerformanceTemplates",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Code",
                table: "Permissions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_AttributeId",
                table: "ProductAttributeValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductId",
                table: "ProductAttributeValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CompanyId",
                table: "ProductCategories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ParentId",
                table: "ProductCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                table: "Products",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_IsActive",
                table: "Products",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UomId",
                table: "Products",
                column: "UomId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_SKU",
                table: "ProductVariants",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLines_ProductId",
                table: "PurchaseOrderLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLines_PurchaseOrderId",
                table: "PurchaseOrderLines",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLines_UomId",
                table: "PurchaseOrderLines",
                column: "UomId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ApprovedBy",
                table: "PurchaseOrders",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_BranchId",
                table: "PurchaseOrders",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_CompanyId",
                table: "PurchaseOrders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_OrderDate",
                table: "PurchaseOrders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PaymentStatus",
                table: "PurchaseOrders",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PONumber",
                table: "PurchaseOrders",
                column: "PONumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Status",
                table: "PurchaseOrders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_VendorId",
                table: "PurchaseOrders",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationLines_ProductId",
                table: "QuotationLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationLines_QuotationId",
                table: "QuotationLines",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationLines_UomId",
                table: "QuotationLines",
                column: "UomId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationLines_VariantId",
                table: "QuotationLines",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_BranchId",
                table: "Quotations",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_CompanyId",
                table: "Quotations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_CustomerId",
                table: "Quotations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_LeadId",
                table: "Quotations",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_QuotationNo",
                table: "Quotations",
                column: "QuotationNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_Status",
                table: "Quotations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutionLogs_CompanyId",
                table: "ReportExecutionLogs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutionLogs_ExecutedAt",
                table: "ReportExecutionLogs",
                column: "ExecutedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutionLogs_ExecutedBy",
                table: "ReportExecutionLogs",
                column: "ExecutedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutionLogs_ReportScheduleId",
                table: "ReportExecutionLogs",
                column: "ReportScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutionLogs_ReportTemplateId",
                table: "ReportExecutionLogs",
                column: "ReportTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportExecutionLogs_Status",
                table: "ReportExecutionLogs",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSchedules_CompanyId",
                table: "ReportSchedules",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSchedules_IsActive",
                table: "ReportSchedules",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSchedules_NextRunAt",
                table: "ReportSchedules",
                column: "NextRunAt");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSchedules_ReportTemplateId",
                table: "ReportSchedules",
                column: "ReportTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplates_CompanyId",
                table: "ReportTemplates",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplates_IsActive",
                table: "ReportTemplates",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplates_ReportCategory",
                table: "ReportTemplates",
                column: "ReportCategory");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplates_ReportCode_CompanyId",
                table: "ReportTemplates",
                columns: new[] { "ReportCode", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId_PermissionId",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalaryComponents_Code_CompanyId",
                table: "SalaryComponents",
                columns: new[] { "Code", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalaryComponents_CompanyId",
                table: "SalaryComponents",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLines_OrderId",
                table: "SalesOrderLines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLines_ProductId",
                table: "SalesOrderLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLines_UomId",
                table: "SalesOrderLines",
                column: "UomId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLines_VariantId",
                table: "SalesOrderLines",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_BranchId",
                table: "SalesOrders",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_CompanyId",
                table: "SalesOrders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_CustomerId",
                table: "SalesOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_OrderDate",
                table: "SalesOrders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_OrderNo",
                table: "SalesOrders",
                column: "OrderNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_PaymentStatus",
                table: "SalesOrders",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_QuotationId",
                table: "SalesOrders",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_SalesRepId",
                table: "SalesOrders",
                column: "SalesRepId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_Status",
                table: "SalesOrders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_WarehouseId",
                table: "SalesOrders",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_ProductId",
                table: "SerialNumbers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_SerialNo",
                table: "SerialNumbers",
                column: "SerialNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_Status",
                table: "SerialNumbers",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_VariantId",
                table: "SerialNumbers",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_WarehouseId",
                table: "SerialNumbers",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockLevels_LocationId",
                table: "StockLevels",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockLevels_ProductId",
                table: "StockLevels",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockLevels_VariantId",
                table: "StockLevels",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_StockLevels_WarehouseId",
                table: "StockLevels",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockLevels_WarehouseId_ProductId_VariantId_LocationId",
                table: "StockLevels",
                columns: new[] { "WarehouseId", "ProductId", "VariantId", "LocationId" },
                unique: true,
                filter: "[VariantId] IS NOT NULL AND [LocationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovementLines_FromLocationId",
                table: "StockMovementLines",
                column: "FromLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovementLines_MovementId",
                table: "StockMovementLines",
                column: "MovementId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovementLines_ProductId",
                table: "StockMovementLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovementLines_ToLocationId",
                table: "StockMovementLines",
                column: "ToLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovementLines_UomId",
                table: "StockMovementLines",
                column: "UomId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovementLines_VariantId",
                table: "StockMovementLines",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_CompanyId",
                table: "StockMovements",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ConfirmedBy",
                table: "StockMovements",
                column: "ConfirmedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_FromWarehouseId",
                table: "StockMovements",
                column: "FromWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_MovementDate",
                table: "StockMovements",
                column: "MovementDate");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_MovementTypeId",
                table: "StockMovements",
                column: "MovementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ReferenceNo",
                table: "StockMovements",
                column: "ReferenceNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_Status",
                table: "StockMovements",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ToWarehouseId",
                table: "StockMovements",
                column: "ToWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovementTypes_Code",
                table: "StockMovementTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageLocations_Code_WarehouseId",
                table: "StorageLocations",
                columns: new[] { "Code", "WarehouseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageLocations_ParentId",
                table: "StorageLocations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageLocations_WarehouseId",
                table: "StorageLocations",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitsOfMeasure_Code",
                table: "UnitsOfMeasure",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDashboardPreferences_DashboardId",
                table: "UserDashboardPreferences",
                column: "DashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDashboardPreferences_UserId",
                table: "UserDashboardPreferences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDashboardPreferences_UserId_DashboardId",
                table: "UserDashboardPreferences",
                columns: new[] { "UserId", "DashboardId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorBankAccounts_VendorId",
                table: "VendorBankAccounts",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorContacts_VendorId",
                table: "VendorContacts",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPayments_CompanyId",
                table: "VendorPayments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPayments_PaymentDate",
                table: "VendorPayments",
                column: "PaymentDate");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPayments_PurchaseOrderId",
                table: "VendorPayments",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPayments_VendorId",
                table: "VendorPayments",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_BranchId",
                table: "Vendors",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CompanyId",
                table: "Vendors",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_Status",
                table: "Vendors",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_VendorCode",
                table: "Vendors",
                column: "VendorCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_VendorType",
                table: "Vendors",
                column: "VendorType");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_BranchId",
                table: "Warehouses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_Code_CompanyId",
                table: "Warehouses",
                columns: new[] { "Code", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CompanyId",
                table: "Warehouses",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsPayablePayments");

            migrationBuilder.DropTable(
                name: "AccountsReceivablePayments");

            migrationBuilder.DropTable(
                name: "AIInsights");

            migrationBuilder.DropTable(
                name: "AIMessages");

            migrationBuilder.DropTable(
                name: "AIPredictions");

            migrationBuilder.DropTable(
                name: "AIReportRequests");

            migrationBuilder.DropTable(
                name: "AIUsageLogs");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Barcodes");

            migrationBuilder.DropTable(
                name: "BatchNumbers");

            migrationBuilder.DropTable(
                name: "BudgetAlerts");

            migrationBuilder.DropTable(
                name: "BudgetRevisions");

            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.DropTable(
                name: "CustomerContacts");

            migrationBuilder.DropTable(
                name: "DashboardWidgets");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryStructure");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "GoodsReturnLines");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "InvoiceLines");

            migrationBuilder.DropTable(
                name: "InvoicePayments");

            migrationBuilder.DropTable(
                name: "JournalEntryLines");

            migrationBuilder.DropTable(
                name: "KPIMetrics");

            migrationBuilder.DropTable(
                name: "LeaveBalances");

            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "PayrollSlipDetails");

            migrationBuilder.DropTable(
                name: "PerformanceReviewDetails");

            migrationBuilder.DropTable(
                name: "ProductAttributeValues");

            migrationBuilder.DropTable(
                name: "QuotationLines");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "ReportExecutionLogs");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SalesOrderLines");

            migrationBuilder.DropTable(
                name: "SerialNumbers");

            migrationBuilder.DropTable(
                name: "StockLevels");

            migrationBuilder.DropTable(
                name: "StockMovementLines");

            migrationBuilder.DropTable(
                name: "UserDashboardPreferences");

            migrationBuilder.DropTable(
                name: "VendorBankAccounts");

            migrationBuilder.DropTable(
                name: "VendorContacts");

            migrationBuilder.DropTable(
                name: "VendorPayments");

            migrationBuilder.DropTable(
                name: "AccountsPayable");

            migrationBuilder.DropTable(
                name: "AccountsReceivable");

            migrationBuilder.DropTable(
                name: "AIConversations");

            migrationBuilder.DropTable(
                name: "AttendanceShifts");

            migrationBuilder.DropTable(
                name: "BudgetLines");

            migrationBuilder.DropTable(
                name: "GoodsReceivingLines");

            migrationBuilder.DropTable(
                name: "GoodsReturnNotes");

            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "JournalEntries");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "PayrollSlips");

            migrationBuilder.DropTable(
                name: "SalaryComponents");

            migrationBuilder.DropTable(
                name: "PerformanceCriteria");

            migrationBuilder.DropTable(
                name: "PerformanceReviews");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "ReportSchedules");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "StockMovements");

            migrationBuilder.DropTable(
                name: "Dashboards");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "ChartOfAccounts");

            migrationBuilder.DropTable(
                name: "PurchaseOrderLines");

            migrationBuilder.DropTable(
                name: "StorageLocations");

            migrationBuilder.DropTable(
                name: "GoodsReceivingNotes");

            migrationBuilder.DropTable(
                name: "JobPostings");

            migrationBuilder.DropTable(
                name: "AccountingPeriods");

            migrationBuilder.DropTable(
                name: "PayrollPeriods");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "PerformanceTemplates");

            migrationBuilder.DropTable(
                name: "ReportTemplates");

            migrationBuilder.DropTable(
                name: "StockMovementTypes");

            migrationBuilder.DropTable(
                name: "SalesOrders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "FiscalYears");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "Quotations");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "UnitsOfMeasure");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "CRMLeads");

            migrationBuilder.DropTable(
                name: "CRMStages");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "CustomerGroups");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
