using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Add_WorkingInfo_UserTable_StatusTracking_WorkingInfoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkingInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seniority = table.Column<double>(type: "float", nullable: false),
                    BaseSalary = table.Column<double>(type: "float", nullable: false),
                    HealthInsurance = table.Column<double>(type: "float", nullable: false),
                    SocialInsurance = table.Column<double>(type: "float", nullable: false),
                    UnemploymentInsurance = table.Column<double>(type: "float", nullable: false),
                    HousingAllowance = table.Column<double>(type: "float", nullable: false),
                    LunchAllowance = table.Column<double>(type: "float", nullable: false),
                    FuelAllowance = table.Column<double>(type: "float", nullable: false),
                    MaternityAllowance = table.Column<double>(type: "float", nullable: false),
                    OtherAllowance = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReasonForDecision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecisionMakerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Modifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingInfo_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingInfo_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingInfo_Users_DecisionMakerId",
                        column: x => x.DecisionMakerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkingInfo_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingInfo_DecisionMakerId",
                table: "WorkingInfo",
                column: "DecisionMakerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingInfo_DepartmentId",
                table: "WorkingInfo",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingInfo_EmployeeId",
                table: "WorkingInfo",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingInfo_PositionId",
                table: "WorkingInfo",
                column: "PositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingInfo");
        }
    }
}
