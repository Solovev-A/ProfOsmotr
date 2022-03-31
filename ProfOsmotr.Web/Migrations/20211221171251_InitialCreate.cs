using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfOsmotr.Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckupResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckupResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsBlocked = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployerData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeesTotal = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeesWomen = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeesUnder18 = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeesPersistentlyDisabled = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkingWithHarmfulFactorsTotal = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkingWithHarmfulFactorsWomen = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkingWithHarmfulFactorsUnder18 = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkingWithHarmfulFactorsPersistentlyDisabled = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkingWithJobTypesTotal = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkingWithJobTypesWomen = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkingWithJobTypesUnder18 = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkingWithJobTypesPersistentlyDisabled = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ICD10Chapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Block = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICD10Chapters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAvailabilityGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAvailabilityGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClinicDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClinicId = table.Column<int>(type: "INTEGER", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    ShortName = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicDetails_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HeadLastName = table.Column<string>(type: "TEXT", nullable: true),
                    HeadFirstName = table.Column<string>(type: "TEXT", nullable: true),
                    HeadPatronymicName = table.Column<string>(type: "TEXT", nullable: true),
                    HeadPosition = table.Column<string>(type: "TEXT", nullable: true),
                    ClinicId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employers_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    PatronymicName = table.Column<string>(type: "TEXT", nullable: false),
                    GenderId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    ClinicId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemProfession",
                columns: table => new
                {
                    OrderItemsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfessionsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemProfession", x => new { x.OrderItemsId, x.ProfessionsId });
                    table.ForeignKey(
                        name: "FK_OrderItemProfession_OrderItems_OrderItemsId",
                        column: x => x.OrderItemsId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemProfession_Professions_ProfessionsId",
                        column: x => x.ProfessionsId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClinicId = table.Column<int>(type: "INTEGER", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_Username", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Users_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderExaminations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsMandatory = table.Column<bool>(type: "INTEGER", nullable: false),
                    TargetGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    DefaultServiceDetailsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderExaminations_ServiceDetails_DefaultServiceDetailsId",
                        column: x => x.DefaultServiceDetailsId,
                        principalTable: "ServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderExaminations_TargetGroup_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "TargetGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerDepartment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerDepartment_Employers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClinicId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsModified = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calculations_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calculations_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicRegisterRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    ShortName = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    SenderId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Processed = table.Column<bool>(type: "INTEGER", nullable: false),
                    Approved = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicRegisterRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicRegisterRequests_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeriodicMedicalExaminations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployerDataId = table.Column<int>(type: "INTEGER", nullable: true),
                    ExaminationYear = table.Column<int>(type: "INTEGER", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ClinicId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployerId = table.Column<int>(type: "INTEGER", nullable: true),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: false),
                    Recommendations = table.Column<string>(type: "TEXT", nullable: true),
                    LastEditorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodicMedicalExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodicMedicalExaminations_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeriodicMedicalExaminations_EmployerData_EmployerDataId",
                        column: x => x.EmployerDataId,
                        principalTable: "EmployerData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeriodicMedicalExaminations_Employers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeriodicMedicalExaminations_Users_LastEditorId",
                        column: x => x.LastEditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreliminaryMedicalExaminations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClinicId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployerId = table.Column<int>(type: "INTEGER", nullable: true),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: false),
                    Recommendations = table.Column<string>(type: "TEXT", nullable: true),
                    LastEditorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreliminaryMedicalExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreliminaryMedicalExaminations_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreliminaryMedicalExaminations_Employers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreliminaryMedicalExaminations_Users_LastEditorId",
                        column: x => x.LastEditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Position = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfile_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationResultIndexes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderExaminationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    UnitOfMeasure = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationResultIndexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationResultIndexes_OrderExaminations_OrderExaminationId",
                        column: x => x.OrderExaminationId,
                        principalTable: "OrderExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderExaminationOrderItem",
                columns: table => new
                {
                    OrderExaminationsId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderExaminationOrderItem", x => new { x.OrderExaminationsId, x.OrderItemsId });
                    table.ForeignKey(
                        name: "FK_OrderExaminationOrderItem_OrderExaminations_OrderExaminationsId",
                        column: x => x.OrderExaminationsId,
                        principalTable: "OrderExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderExaminationOrderItem_OrderItems_OrderItemsId",
                        column: x => x.OrderItemsId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderExaminationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClinicId = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceDetailsId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    ServiceAvailabilityGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => new { x.Id, x.OrderExaminationId, x.ClinicId });
                    table.ForeignKey(
                        name: "FK_Services_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_OrderExaminations_OrderExaminationId",
                        column: x => x.OrderExaminationId,
                        principalTable: "OrderExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_ServiceAvailabilityGroups_ServiceAvailabilityGroupId",
                        column: x => x.ServiceAvailabilityGroupId,
                        principalTable: "ServiceAvailabilityGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_ServiceDetails_ServiceDetailsId",
                        column: x => x.ServiceDetailsId,
                        principalTable: "ServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalculationSource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CalculationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfessionId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfPersons = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfPersonsOver40 = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfWomen = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfWomenOver40 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationSource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalculationSource_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalculationSource_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContingentCheckupStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PeriodicMedicalExaminationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CheckupStarted = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDisabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    NeedExaminationAtOccupationalPathologyCenter = table.Column<bool>(type: "INTEGER", nullable: false),
                    NeedOutpatientExamunationAndTreatment = table.Column<bool>(type: "INTEGER", nullable: false),
                    NeedInpatientExamunationAndTreatment = table.Column<bool>(type: "INTEGER", nullable: false),
                    NeedSpaTreatment = table.Column<bool>(type: "INTEGER", nullable: false),
                    NeedDispensaryObservation = table.Column<bool>(type: "INTEGER", nullable: false),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployerDepartmentId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProfessionId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateOfCompletion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CheckupResultId = table.Column<int>(type: "INTEGER", nullable: true),
                    MedicalReport = table.Column<string>(type: "TEXT", nullable: true),
                    RegistrationJournalEntryNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    LastEditorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContingentCheckupStatuses", x => x.Id);
                    table.UniqueConstraint("AK_ContingentCheckupStatuses_PeriodicMedicalExaminationId_PatientId", x => new { x.PeriodicMedicalExaminationId, x.PatientId });
                    table.ForeignKey(
                        name: "FK_ContingentCheckupStatuses_CheckupResult_CheckupResultId",
                        column: x => x.CheckupResultId,
                        principalTable: "CheckupResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContingentCheckupStatuses_EmployerDepartment_EmployerDepartmentId",
                        column: x => x.EmployerDepartmentId,
                        principalTable: "EmployerDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContingentCheckupStatuses_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContingentCheckupStatuses_PeriodicMedicalExaminations_PeriodicMedicalExaminationId",
                        column: x => x.PeriodicMedicalExaminationId,
                        principalTable: "PeriodicMedicalExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContingentCheckupStatuses_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContingentCheckupStatuses_Users_LastEditorId",
                        column: x => x.LastEditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualCheckupStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PreliminaryMedicalExaminationId = table.Column<int>(type: "INTEGER", nullable: false),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployerDepartmentId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProfessionId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateOfCompletion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CheckupResultId = table.Column<int>(type: "INTEGER", nullable: true),
                    MedicalReport = table.Column<string>(type: "TEXT", nullable: true),
                    RegistrationJournalEntryNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    LastEditorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualCheckupStatuses", x => x.Id);
                    table.UniqueConstraint("AK_IndividualCheckupStatuses_PreliminaryMedicalExaminationId_PatientId", x => new { x.PreliminaryMedicalExaminationId, x.PatientId });
                    table.ForeignKey(
                        name: "FK_IndividualCheckupStatuses_CheckupResult_CheckupResultId",
                        column: x => x.CheckupResultId,
                        principalTable: "CheckupResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualCheckupStatuses_EmployerDepartment_EmployerDepartmentId",
                        column: x => x.EmployerDepartmentId,
                        principalTable: "EmployerDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualCheckupStatuses_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualCheckupStatuses_PreliminaryMedicalExaminations_PreliminaryMedicalExaminationId",
                        column: x => x.PreliminaryMedicalExaminationId,
                        principalTable: "PreliminaryMedicalExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualCheckupStatuses_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualCheckupStatuses_Users_LastEditorId",
                        column: x => x.LastEditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActualClinicService",
                columns: table => new
                {
                    ClinicId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderExaminationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActualClinicService", x => new { x.OrderExaminationId, x.ClinicId });
                    table.ForeignKey(
                        name: "FK_ActualClinicService_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActualClinicService_OrderExaminations_OrderExaminationId",
                        column: x => x.OrderExaminationId,
                        principalTable: "OrderExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActualClinicService_Services_ServiceId_OrderExaminationId_ClinicId",
                        columns: x => new { x.ServiceId, x.OrderExaminationId, x.ClinicId },
                        principalTable: "Services",
                        principalColumns: new[] { "Id", "OrderExaminationId", "ClinicId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalculationResultItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CalculationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceOrderExaminationId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceClinicId = table.Column<int>(type: "INTEGER", nullable: true),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationResultItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalculationResultItems_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalculationResultItems_Services_ServiceId_ServiceOrderExaminationId_ServiceClinicId",
                        columns: x => new { x.ServiceId, x.ServiceOrderExaminationId, x.ServiceClinicId },
                        principalTable: "Services",
                        principalColumns: new[] { "Id", "OrderExaminationId", "ClinicId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContingentCheckupIndexValue",
                columns: table => new
                {
                    ExaminationResultIndexId = table.Column<int>(type: "INTEGER", nullable: false),
                    ContingentCheckupStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContingentCheckupIndexValue", x => new { x.ContingentCheckupStatusId, x.ExaminationResultIndexId });
                    table.ForeignKey(
                        name: "FK_ContingentCheckupIndexValue_ContingentCheckupStatuses_ContingentCheckupStatusId",
                        column: x => x.ContingentCheckupStatusId,
                        principalTable: "ContingentCheckupStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContingentCheckupIndexValue_ExaminationResultIndexes_ExaminationResultIndexId",
                        column: x => x.ExaminationResultIndexId,
                        principalTable: "ExaminationResultIndexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewlyDiagnosedChronicSomaticDisease",
                columns: table => new
                {
                    ContingentCheckupStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    ICD10ChapterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cases = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewlyDiagnosedChronicSomaticDisease", x => new { x.ContingentCheckupStatusId, x.ICD10ChapterId });
                    table.ForeignKey(
                        name: "FK_NewlyDiagnosedChronicSomaticDisease_ContingentCheckupStatuses_ContingentCheckupStatusId",
                        column: x => x.ContingentCheckupStatusId,
                        principalTable: "ContingentCheckupStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewlyDiagnosedChronicSomaticDisease_ICD10Chapters_ICD10ChapterId",
                        column: x => x.ICD10ChapterId,
                        principalTable: "ICD10Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewlyDiagnosedOccupationalDisease",
                columns: table => new
                {
                    ContingentCheckupStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    ICD10ChapterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cases = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewlyDiagnosedOccupationalDisease", x => new { x.ContingentCheckupStatusId, x.ICD10ChapterId });
                    table.ForeignKey(
                        name: "FK_NewlyDiagnosedOccupationalDisease_ContingentCheckupStatuses_ContingentCheckupStatusId",
                        column: x => x.ContingentCheckupStatusId,
                        principalTable: "ContingentCheckupStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewlyDiagnosedOccupationalDisease_ICD10Chapters_ICD10ChapterId",
                        column: x => x.ICD10ChapterId,
                        principalTable: "ICD10Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualCheckupIndexValue",
                columns: table => new
                {
                    ExaminationResultIndexId = table.Column<int>(type: "INTEGER", nullable: false),
                    IndividualCheckupStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualCheckupIndexValue", x => new { x.IndividualCheckupStatusId, x.ExaminationResultIndexId });
                    table.ForeignKey(
                        name: "FK_IndividualCheckupIndexValue_ExaminationResultIndexes_ExaminationResultIndexId",
                        column: x => x.ExaminationResultIndexId,
                        principalTable: "ExaminationResultIndexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualCheckupIndexValue_IndividualCheckupStatuses_IndividualCheckupStatusId",
                        column: x => x.IndividualCheckupStatusId,
                        principalTable: "IndividualCheckupStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActualClinicService_ClinicId",
                table: "ActualClinicService",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_ActualClinicService_ServiceId_OrderExaminationId_ClinicId",
                table: "ActualClinicService",
                columns: new[] { "ServiceId", "OrderExaminationId", "ClinicId" });

            migrationBuilder.CreateIndex(
                name: "IX_CalculationResultItems_CalculationId",
                table: "CalculationResultItems",
                column: "CalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculationResultItems_ServiceId_ServiceOrderExaminationId_ServiceClinicId",
                table: "CalculationResultItems",
                columns: new[] { "ServiceId", "ServiceOrderExaminationId", "ServiceClinicId" });

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_ClinicId",
                table: "Calculations",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_CreatorId",
                table: "Calculations",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculationSource_CalculationId",
                table: "CalculationSource",
                column: "CalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculationSource_ProfessionId",
                table: "CalculationSource",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicDetails_ClinicId",
                table: "ClinicDetails",
                column: "ClinicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicRegisterRequests_SenderId",
                table: "ClinicRegisterRequests",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ContingentCheckupIndexValue_ExaminationResultIndexId",
                table: "ContingentCheckupIndexValue",
                column: "ExaminationResultIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_ContingentCheckupStatuses_CheckupResultId",
                table: "ContingentCheckupStatuses",
                column: "CheckupResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ContingentCheckupStatuses_EmployerDepartmentId",
                table: "ContingentCheckupStatuses",
                column: "EmployerDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContingentCheckupStatuses_LastEditorId",
                table: "ContingentCheckupStatuses",
                column: "LastEditorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContingentCheckupStatuses_PatientId",
                table: "ContingentCheckupStatuses",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ContingentCheckupStatuses_ProfessionId",
                table: "ContingentCheckupStatuses",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerDepartment_ParentId",
                table: "EmployerDepartment",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_ClinicId",
                table: "Employers",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationResultIndexes_OrderExaminationId",
                table: "ExaminationResultIndexes",
                column: "OrderExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualCheckupIndexValue_ExaminationResultIndexId",
                table: "IndividualCheckupIndexValue",
                column: "ExaminationResultIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualCheckupStatuses_CheckupResultId",
                table: "IndividualCheckupStatuses",
                column: "CheckupResultId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualCheckupStatuses_EmployerDepartmentId",
                table: "IndividualCheckupStatuses",
                column: "EmployerDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualCheckupStatuses_LastEditorId",
                table: "IndividualCheckupStatuses",
                column: "LastEditorId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualCheckupStatuses_PatientId",
                table: "IndividualCheckupStatuses",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualCheckupStatuses_PreliminaryMedicalExaminationId",
                table: "IndividualCheckupStatuses",
                column: "PreliminaryMedicalExaminationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndividualCheckupStatuses_ProfessionId",
                table: "IndividualCheckupStatuses",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_NewlyDiagnosedChronicSomaticDisease_ICD10ChapterId",
                table: "NewlyDiagnosedChronicSomaticDisease",
                column: "ICD10ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_NewlyDiagnosedOccupationalDisease_ICD10ChapterId",
                table: "NewlyDiagnosedOccupationalDisease",
                column: "ICD10ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderExaminationOrderItem_OrderItemsId",
                table: "OrderExaminationOrderItem",
                column: "OrderItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderExaminations_DefaultServiceDetailsId",
                table: "OrderExaminations",
                column: "DefaultServiceDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderExaminations_TargetGroupId",
                table: "OrderExaminations",
                column: "TargetGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemProfession_ProfessionsId",
                table: "OrderItemProfession",
                column: "ProfessionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ClinicId",
                table: "Patients",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_GenderId",
                table: "Patients",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicMedicalExaminations_ClinicId",
                table: "PeriodicMedicalExaminations",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicMedicalExaminations_EmployerDataId",
                table: "PeriodicMedicalExaminations",
                column: "EmployerDataId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicMedicalExaminations_EmployerId",
                table: "PeriodicMedicalExaminations",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicMedicalExaminations_LastEditorId",
                table: "PeriodicMedicalExaminations",
                column: "LastEditorId");

            migrationBuilder.CreateIndex(
                name: "IX_PreliminaryMedicalExaminations_ClinicId",
                table: "PreliminaryMedicalExaminations",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_PreliminaryMedicalExaminations_EmployerId",
                table: "PreliminaryMedicalExaminations",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_PreliminaryMedicalExaminations_LastEditorId",
                table: "PreliminaryMedicalExaminations",
                column: "LastEditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ClinicId",
                table: "Services",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_OrderExaminationId",
                table: "Services",
                column: "OrderExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceAvailabilityGroupId",
                table: "Services",
                column: "ServiceAvailabilityGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceDetailsId",
                table: "Services",
                column: "ServiceDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserId",
                table: "UserProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClinicId",
                table: "Users",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActualClinicService");

            migrationBuilder.DropTable(
                name: "CalculationResultItems");

            migrationBuilder.DropTable(
                name: "CalculationSource");

            migrationBuilder.DropTable(
                name: "ClinicDetails");

            migrationBuilder.DropTable(
                name: "ClinicRegisterRequests");

            migrationBuilder.DropTable(
                name: "ContingentCheckupIndexValue");

            migrationBuilder.DropTable(
                name: "IndividualCheckupIndexValue");

            migrationBuilder.DropTable(
                name: "NewlyDiagnosedChronicSomaticDisease");

            migrationBuilder.DropTable(
                name: "NewlyDiagnosedOccupationalDisease");

            migrationBuilder.DropTable(
                name: "OrderExaminationOrderItem");

            migrationBuilder.DropTable(
                name: "OrderItemProfession");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Calculations");

            migrationBuilder.DropTable(
                name: "ExaminationResultIndexes");

            migrationBuilder.DropTable(
                name: "IndividualCheckupStatuses");

            migrationBuilder.DropTable(
                name: "ContingentCheckupStatuses");

            migrationBuilder.DropTable(
                name: "ICD10Chapters");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ServiceAvailabilityGroups");

            migrationBuilder.DropTable(
                name: "OrderExaminations");

            migrationBuilder.DropTable(
                name: "PreliminaryMedicalExaminations");

            migrationBuilder.DropTable(
                name: "CheckupResult");

            migrationBuilder.DropTable(
                name: "EmployerDepartment");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "PeriodicMedicalExaminations");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "ServiceDetails");

            migrationBuilder.DropTable(
                name: "TargetGroup");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "EmployerData");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
