using PrintingServer.Domain.Entities.UserEntities;

namespace PrintingServer.Domain.Constants
{
    public enum ReportType
    {
        InvoiceReport = 0,
        FolioReport = 1,
        DepartureListReport = 2,
        CheckInListReport = 3,
        CheckOutListReport = 4,
        AccoumodationList = 5,
        ExtendArrivalReport = 6,
        ReceptionPaymentCopuneReportCompany = 7,
        ReceptionPaymentCopuneReportCustomer = 8,
        ShortFolioReport = 9,
        InvoicesReport = 10,
        CashPayment = 11,
        ReservationReport = 12,
        RoomNumber = 13,
        Voucher = 14,
        TestingInvoiceReport = 100,
        TestingFolioReport = 101,
        TestingDepartureListReport = 102,
        TestingCheckInListReport = 103,
        TestingCheckOutListReport = 104,
        TestingAccoumodationListReport = 105,
        TestingExtendArrivalReport = 106,
        TestingReceptionPaymentCopuneReportCompany = 107,
        TestingReceptionPaymentCopuneReportCustomer = 108,
        TestingShortFolioReport = 109,
        TestingInvoicesReport = 110,
        TestingCashPayment = 111,
        TestingReservationReport = 112,
        TestingRoomNumber = 113,
        TestingVoucher = 114
    }
    public enum AccomodationBillType
    {
        Total = 0,
        Detailed = 1,
        Groups = 2
    }
    public enum ResourceOperation
    {
        Read = 0,
        Create = 1,
        Update = 2,
        Delete = 3,
        DeleteAll = 4
    }
    public static class UserRoles
    {
        public const string Manager = "Manager";
        public const string Administrator = "Administrator";
        public const string Printer = "Printer";
        public static readonly List<string> Roles =
        [
            UserRoles.Manager,
            UserRoles.Administrator,
            UserRoles.Printer
        ];

    };
    public static class AccountSettings
    {
        public const string DomainName = "TQSystems.com";
        public const string DefaultPassword = "P@$$w0rd";
        public static string GetEmail(string email)
        {
            return string.Format("{0}@{1}", email, DomainName);
        }
    }
}
