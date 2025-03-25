using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsModified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryEnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryArName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryEnNationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryArNationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsModified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Printer_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrinterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrinterIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseTime = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsModified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printer_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrinterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsModified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.AccoumodationList_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.AccoumodationList_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.CashPayment_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepitAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepitValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.CashPayment_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.CheckInList_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.CheckInList_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.CheckOutList_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.CheckOutList_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.DepartureList_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartureDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.DepartureList_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.ExtendedArrivalList_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.ExtendedArrivalList_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.Folio_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuiteNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rquired = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAllRegistration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalConfirmedPayment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalUnconfirmedPayment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalConfirmedINS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalUnconfirmedINS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTransfare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAllRequired = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.Folio_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.Invoice_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IncrementalNumber = table.Column<int>(type: "int", nullable: false),
                    IncrementalCheckInNumber = table.Column<int>(type: "int", nullable: false),
                    InvoceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSch = table.Column<bool>(type: "bit", nullable: false),
                    IsPrintedOnce = table.Column<bool>(type: "bit", nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NightCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostWithoutTax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalCost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QRCode = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAfterGivenDiscount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckOut = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.Invoice_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.InvoicesList_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrintedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintedAt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalInvoices = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTax1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTax2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTax3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TTotal = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.InvoicesList_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.ReceptionPayment_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PType = table.Column<bool>(type: "bit", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.ReceptionPayment_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.Reservation_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalForg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.Reservation_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.RoomNumber_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomNu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.RoomNumber_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report.Voucher_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuiteNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.Voucher_TB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrinterErrorLog_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrintedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ErrorDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsModified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterErrorLog_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrinterErrorLog_TB_Printer_TB",
                        column: x => x.PrintedId,
                        principalTable: "Printer_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientReport_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrinterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrinterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrinterIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsModified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientReport_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientReport_TB_Client_TB",
                        column: x => x.ClientId,
                        principalTable: "Client_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientReport_TB_Printer_TB",
                        column: x => x.PrinterId,
                        principalTable: "Printer_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientReport_TB_Report_TB",
                        column: x => x.ReportId,
                        principalTable: "Report_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.AccoumodationListItem_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    POB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LivingIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommingFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommingDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeavingFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeavingDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PD_Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PD_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PD_Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.AccoumodationListItem_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.AccoumodationListItem_TB_Report.AccoumodationList_TB_ListID",
                        column: x => x.ListID,
                        principalTable: "Report.AccoumodationList_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.CheckInListItem_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefferanceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.CheckInListItem_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.CheckInListItem_TB_Report.CheckInList_TB_ListID",
                        column: x => x.ListID,
                        principalTable: "Report.CheckInList_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.CheckOutListItem_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefferanceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtraField = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.CheckOutListItem_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.CheckOutListItem_TB_Report.CheckOutList_TB_ListID",
                        column: x => x.ListID,
                        principalTable: "Report.CheckOutList_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.DepartureListItem_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NightCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomState = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.DepartureListItem_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.DepartureListItem_TB_Report.DepartureList_TB_ListID",
                        column: x => x.ListID,
                        principalTable: "Report.DepartureList_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.Accoumodation_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfAdult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfChild = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfYoung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefranceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtendedArrivalListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.Accoumodation_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.Accoumodation_TB_Report.ExtendedArrivalList_TB_ExtendedArrivalListId",
                        column: x => x.ExtendedArrivalListId,
                        principalTable: "Report.ExtendedArrivalList_TB",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Report.AllRegistration_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.AllRegistration_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.AllRegistration_TB_Report.Folio_TB_FolioID",
                        column: x => x.FolioID,
                        principalTable: "Report.Folio_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.ConfirmedINS_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.ConfirmedINS_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.ConfirmedINS_TB_Report.Folio_TB_FolioID",
                        column: x => x.FolioID,
                        principalTable: "Report.Folio_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.ConfirmedPayment_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.ConfirmedPayment_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.ConfirmedPayment_TB_Report.Folio_TB_FolioID",
                        column: x => x.FolioID,
                        principalTable: "Report.Folio_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.RequiredItem_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Offer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RateBeforeTax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RateAfterTax = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.RequiredItem_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.RequiredItem_TB_Report.Folio_TB_FolioID",
                        column: x => x.FolioID,
                        principalTable: "Report.Folio_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.Transfare_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.Transfare_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.Transfare_TB_Report.Folio_TB_FolioID",
                        column: x => x.FolioID,
                        principalTable: "Report.Folio_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.UnConfirmedINS_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.UnConfirmedINS_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.UnConfirmedINS_TB_Report.Folio_TB_FolioID",
                        column: x => x.FolioID,
                        principalTable: "Report.Folio_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.UnConfirmedPayment_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.UnConfirmedPayment_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.UnConfirmedPayment_TB_Report.Folio_TB_FolioID",
                        column: x => x.FolioID,
                        principalTable: "Report.Folio_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.InvoiceItem_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalCost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.InvoiceItem_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.InvoiceItem_TB_Report.Invoice_TB_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Report.Invoice_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.InvoiceTax_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.InvoiceTax_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.InvoiceTax_TB_Report.Invoice_TB_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Report.Invoice_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.InvoicesListItem_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoicesNumbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoicesValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tax1Total = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tax2Total = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tax3Total = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.InvoicesListItem_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.InvoicesListItem_TB_Report.InvoicesList_TB_ListID",
                        column: x => x.ListID,
                        principalTable: "Report.InvoicesList_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.ReservationItem_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuiteNu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StayDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SingleNightWithTax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalWithTax = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.ReservationItem_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.ReservationItem_TB_Report.Reservation_TB_ReservationID",
                        column: x => x.ReservationID,
                        principalTable: "Report.Reservation_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.VoucherItem_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoucherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.VoucherItem_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.VoucherItem_TB_Report.Voucher_TB_VoucherID",
                        column: x => x.VoucherID,
                        principalTable: "Report.Voucher_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Printed_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrintedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MainObjectGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrintedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsModified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printed_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Printed_TB_ClientReport_TB_ClientReportId",
                        column: x => x.ClientReportId,
                        principalTable: "ClientReport_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.AccoumodationItem_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccDtoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefferanceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.AccoumodationItem_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.AccoumodationItem_TB_Report.Accoumodation_TB_AccDtoID",
                        column: x => x.AccDtoID,
                        principalTable: "Report.Accoumodation_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report.InvoiceItemTax_TB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report.InvoiceItemTax_TB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report.InvoiceItemTax_TB_Report.InvoiceItem_TB_InvoiceItemID",
                        column: x => x.InvoiceItemID,
                        principalTable: "Report.InvoiceItem_TB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientReport_TB_ClientId",
                table: "ClientReport_TB",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientReport_TB_PrinterId",
                table: "ClientReport_TB",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientReport_TB_ReportId",
                table: "ClientReport_TB",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Printed_TB_ClientReportId",
                table: "Printed_TB",
                column: "ClientReportId");

            migrationBuilder.CreateIndex(
                name: "IX_PrinterErrorLog_TB_PrintedId",
                table: "PrinterErrorLog_TB",
                column: "PrintedId");

            migrationBuilder.CreateIndex(
                name: "IX_Report.Accoumodation_TB_ExtendedArrivalListId",
                table: "Report.Accoumodation_TB",
                column: "ExtendedArrivalListId");

            migrationBuilder.CreateIndex(
                name: "IX_Report.AccoumodationItem_TB_AccDtoID",
                table: "Report.AccoumodationItem_TB",
                column: "AccDtoID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.AccoumodationListItem_TB_ListID",
                table: "Report.AccoumodationListItem_TB",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.AllRegistration_TB_FolioID",
                table: "Report.AllRegistration_TB",
                column: "FolioID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.CheckInListItem_TB_ListID",
                table: "Report.CheckInListItem_TB",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.CheckOutListItem_TB_ListID",
                table: "Report.CheckOutListItem_TB",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.ConfirmedINS_TB_FolioID",
                table: "Report.ConfirmedINS_TB",
                column: "FolioID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.ConfirmedPayment_TB_FolioID",
                table: "Report.ConfirmedPayment_TB",
                column: "FolioID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.DepartureListItem_TB_ListID",
                table: "Report.DepartureListItem_TB",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.InvoiceItem_TB_InvoiceID",
                table: "Report.InvoiceItem_TB",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.InvoiceItemTax_TB_InvoiceItemID",
                table: "Report.InvoiceItemTax_TB",
                column: "InvoiceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.InvoicesListItem_TB_ListID",
                table: "Report.InvoicesListItem_TB",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.InvoiceTax_TB_InvoiceID",
                table: "Report.InvoiceTax_TB",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.RequiredItem_TB_FolioID",
                table: "Report.RequiredItem_TB",
                column: "FolioID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.ReservationItem_TB_ReservationID",
                table: "Report.ReservationItem_TB",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.Transfare_TB_FolioID",
                table: "Report.Transfare_TB",
                column: "FolioID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.UnConfirmedINS_TB_FolioID",
                table: "Report.UnConfirmedINS_TB",
                column: "FolioID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.UnConfirmedPayment_TB_FolioID",
                table: "Report.UnConfirmedPayment_TB",
                column: "FolioID");

            migrationBuilder.CreateIndex(
                name: "IX_Report.VoucherItem_TB_VoucherID",
                table: "Report.VoucherItem_TB",
                column: "VoucherID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Country_TB");

            migrationBuilder.DropTable(
                name: "Printed_TB");

            migrationBuilder.DropTable(
                name: "PrinterErrorLog_TB");

            migrationBuilder.DropTable(
                name: "Report.AccoumodationItem_TB");

            migrationBuilder.DropTable(
                name: "Report.AccoumodationListItem_TB");

            migrationBuilder.DropTable(
                name: "Report.AllRegistration_TB");

            migrationBuilder.DropTable(
                name: "Report.CashPayment_TB");

            migrationBuilder.DropTable(
                name: "Report.CheckInListItem_TB");

            migrationBuilder.DropTable(
                name: "Report.CheckOutListItem_TB");

            migrationBuilder.DropTable(
                name: "Report.ConfirmedINS_TB");

            migrationBuilder.DropTable(
                name: "Report.ConfirmedPayment_TB");

            migrationBuilder.DropTable(
                name: "Report.DepartureListItem_TB");

            migrationBuilder.DropTable(
                name: "Report.InvoiceItemTax_TB");

            migrationBuilder.DropTable(
                name: "Report.InvoicesListItem_TB");

            migrationBuilder.DropTable(
                name: "Report.InvoiceTax_TB");

            migrationBuilder.DropTable(
                name: "Report.ReceptionPayment_TB");

            migrationBuilder.DropTable(
                name: "Report.RequiredItem_TB");

            migrationBuilder.DropTable(
                name: "Report.ReservationItem_TB");

            migrationBuilder.DropTable(
                name: "Report.RoomNumber_TB");

            migrationBuilder.DropTable(
                name: "Report.Transfare_TB");

            migrationBuilder.DropTable(
                name: "Report.UnConfirmedINS_TB");

            migrationBuilder.DropTable(
                name: "Report.UnConfirmedPayment_TB");

            migrationBuilder.DropTable(
                name: "Report.VoucherItem_TB");

            migrationBuilder.DropTable(
                name: "ClientReport_TB");

            migrationBuilder.DropTable(
                name: "Report.Accoumodation_TB");

            migrationBuilder.DropTable(
                name: "Report.AccoumodationList_TB");

            migrationBuilder.DropTable(
                name: "Report.CheckInList_TB");

            migrationBuilder.DropTable(
                name: "Report.CheckOutList_TB");

            migrationBuilder.DropTable(
                name: "Report.DepartureList_TB");

            migrationBuilder.DropTable(
                name: "Report.InvoiceItem_TB");

            migrationBuilder.DropTable(
                name: "Report.InvoicesList_TB");

            migrationBuilder.DropTable(
                name: "Report.Reservation_TB");

            migrationBuilder.DropTable(
                name: "Report.Folio_TB");

            migrationBuilder.DropTable(
                name: "Report.Voucher_TB");

            migrationBuilder.DropTable(
                name: "Client_TB");

            migrationBuilder.DropTable(
                name: "Printer_TB");

            migrationBuilder.DropTable(
                name: "Report_TB");

            migrationBuilder.DropTable(
                name: "Report.ExtendedArrivalList_TB");

            migrationBuilder.DropTable(
                name: "Report.Invoice_TB");
        }
    }
}
