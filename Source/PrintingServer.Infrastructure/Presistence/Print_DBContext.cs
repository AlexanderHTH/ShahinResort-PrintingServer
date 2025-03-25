using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;

namespace PrintingServer.Infrastructure.Presistence;

public partial class Print_DBContext(DbContextOptions<Print_DBContext> options) : DbContext(options)
{
    #region printing setup
    internal DbSet<ClientReport> ClientReports { get; set; }
    internal DbSet<Client> Clients { get; set; }
    internal DbSet<Country> Countries { get; set; }
    internal DbSet<Printed> Printeds { get; set; }
    internal DbSet<PrinterErrorLog> PrinterErrorLogs { get; set; }
    internal DbSet<Printer> Printers { get; set; }
    internal DbSet<Report> Reports { get; set; }
    #endregion
    #region Reports
    internal DbSet<AccoumodationList> AccoumodationLists { get; set; }
    internal DbSet<AccoumodationListItem> AccoumodationListItems { get; set; }
    internal DbSet<CashPayment> CashPayments { get; set; }
    internal DbSet<CheckInList> CheckInLists { get; set; }
    internal DbSet<CheckInListItem> CheckInListItems { get; set; }
    internal DbSet<CheckOutList> CheckOutLists { get; set; }
    internal DbSet<CheckOutListItem> CheckOutListItems { get; set; }
    internal DbSet<DepartureList> DepartureLists { get; set; }
    internal DbSet<DepartureListItem> DepartureListItems { get; set; }
    internal DbSet<Accoumodation> Accoumodations { get; set; }
    internal DbSet<AccoumodationItem> AccoumodationItems { get; set; }
    internal DbSet<ExtendedArrivalList> ExtendedArrivalLists { get; set; }
    internal DbSet<Folio> Folios { get; set; }
    internal DbSet<AllRegistration> AllRegistrations { get; set; }
    internal DbSet<ConfirmedPayment> ConfirmedPayments { get; set; }
    internal DbSet<UnConfirmedPayment> UnConfirmedPayments { get; set; }
    internal DbSet<ConfirmedINS> ConfirmedINSs { get; set; }
    internal DbSet<UnConfirmedINS> UnConfirmedINSs { get; set; }
    internal DbSet<Transfare> Transfares { get; set; }
    internal DbSet<RequiredItem> RequiredItems { get; set; }
    internal DbSet<Invoice> Invoices { get; set; }
    internal DbSet<InvoiceItem> InvoiceItems { get; set; }
    internal DbSet<InvoiceTax> InvoiceTaxs { get; set; }
    internal DbSet<InvoiceItemTax> InvoiceItemTaxes { get; set; }
    internal DbSet<InvoicesList> InvoicesLists { get; set; }
    internal DbSet<InvoicesListItem> InvoicesListItems { get; set; }
    internal DbSet<ReceptionPayment> ReceptionPayments { get; set; }
    internal DbSet<Reservation> Reservations { get; set; }
    internal DbSet<ReservationItem> ReservationItems { get; set; }
    internal DbSet<RoomNumber> RoomNumbers { get; set; }
    internal DbSet<Voucher> Vouchers { get; set; }
    internal DbSet<VoucherItem> VoucherItems { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseCollation("Arabic_CI_AI");
        #region Printing Setup
        modelBuilder.Entity<ClientReport>(entity =>
        {
            entity.ToTable("ClientReport_TB");

            //entity.Property(e => e.Id).HasColumnName("ID");
            //entity.Property(e => e.ClientId).HasColumnName("ClientID");
            //entity.Property(e => e.ClientIp).IsRequired().IsUnicode(false).HasColumnName("ClientIP");
            //entity.Property(e => e.ClientName).IsRequired().IsUnicode(false);
            //entity.Property(e => e.PrinterId).HasColumnName("PrinterID");
            //entity.Property(e => e.PrinterIp).IsRequired().IsUnicode(false).HasColumnName("PrinterIP");
            //entity.Property(e => e.PrinterName).IsRequired().IsUnicode(false);
            //entity.Property(e => e.ReportId).HasColumnName("ReportID");
            //entity.Property(e => e.ReportName).IsRequired().IsUnicode(false);

            entity.HasOne(d => d.Client)
                  .WithMany(p => p.ClientReports)
                  .HasForeignKey(d => d.ClientId)
                  .HasConstraintName("FK_ClientReport_TB_Client_TB");

            entity.HasOne(d => d.Printer)
                  .WithMany(p => p.ClientReports)
                  .HasForeignKey(d => d.PrinterId)
                  .HasConstraintName("FK_ClientReport_TB_Printer_TB");

            entity.HasOne(d => d.Report)
                  .WithMany(p => p.ClientReports)
                  .HasForeignKey(d => d.ReportId)
                  .HasConstraintName("FK_ClientReport_TB_Report_TB");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client_TB");

            //entity.Property(e => e.Id).HasColumnName("ID");
            //entity.Property(e => e.ClientIp)
            //    .IsRequired()
            //    .HasMaxLength(15)
            //    .IsUnicode(false)
            //    .HasColumnName("ClientIP");
            //entity.Property(e => e.ClientName)
            //    .IsRequired()
            //    .HasMaxLength(150)
            //    .IsUnicode(false);
            //entity.Property(e => e.Notes).HasColumnType("text");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country_TB");

            entity.Property(e => e.Id).HasColumnName("ID").HasColumnType("uniqueidentifier");
            //entity.Property(e => e.CountryArName)
            //    .IsRequired()
            //    .HasMaxLength(100)
            //    .IsUnicode(false)
            //    .HasDefaultValue("")
            //    .HasColumnName("Country_arName");
            //entity.Property(e => e.CountryArNationality)
            //    .IsRequired()
            //    .HasMaxLength(100)
            //    .IsUnicode(false)
            //    .HasDefaultValue("")
            //    .HasColumnName("Country_arNationality");
            //entity.Property(e => e.CountryCode)
            //    .IsRequired()
            //    .HasMaxLength(2)
            //    .IsUnicode(false)
            //    .HasDefaultValue("")
            //    .HasColumnName("Country_code");
            //entity.Property(e => e.CountryEnName)
            //    .IsRequired()
            //    .HasMaxLength(100)
            //    .IsUnicode(false)
            //    .HasDefaultValue("")
            //    .HasColumnName("Country_enName");
            //entity.Property(e => e.CountryEnNationality)
            //    .IsRequired()
            //    .HasMaxLength(100)
            //    .IsUnicode(false)
            //    .HasDefaultValue("")
            //    .HasColumnName("Country_enNationality");
        });

        modelBuilder.Entity<Printed>(entity =>
        {
            entity.ToTable("Printed_TB");

            //entity.Property(e => e.Id).HasColumnName("ID");
            //entity.Property(e => e.ClientReportId).HasColumnName("ClientReportID");
            //entity.Property(e => e.MainObjectGuid)
            //    .IsUnicode(false)
            //    .HasColumnName("MainObjectGUID");
            //entity.Property(e => e.Notes).HasColumnType("text");
            //entity.Property(e => e.PrintedBy).IsUnicode(false);
            //entity.Property(e => e.PrintedTime).HasColumnType("datetime");

            //entity.HasOne(d => d.ClientReport).WithMany(p => p.Printeds)
            //    .HasForeignKey(d => d.ClientReportId)
            //    .HasConstraintName("FK_Printed_TB_ClientReport_TB");
        });

        modelBuilder.Entity<PrinterErrorLog>(entity =>
        {
            entity.ToTable("PrinterErrorLog_TB");

            //entity.Property(e => e.Id).HasColumnName("ID");
            //entity.Property(e => e.Details).HasColumnType("text");
            //entity.Property(e => e.ErrorDate).HasColumnType("datetime");
            //entity.Property(e => e.PrintedId).HasColumnName("PrintedID");

            entity.HasOne(d => d.Printer).WithMany(p => p.PrinterErrorLogs)
                .HasForeignKey(d => d.PrinterId)
                .HasConstraintName("FK_PrinterErrorLog_TB_Printer_TB");
        });

        modelBuilder.Entity<Printer>(entity =>
        {
            entity.ToTable("Printer_TB");

            //entity.Property(e => e.Id).HasColumnName("ID");
            //entity.Property(e => e.Notes).HasColumnType("text");
            //entity.Property(e => e.PrinterIp)
            //    .IsRequired()
            //    .HasMaxLength(15)
            //    .IsUnicode(false)
            //    .HasColumnName("PrinterIP");
            //entity.Property(e => e.PrinterName)
            //    .IsRequired()
            //    .IsUnicode(false);
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.ToTable("Report_TB");

            //entity.Property(e => e.Id).HasColumnName("ID");
            //entity.Property(e => e.Notes).HasColumnType("text");
            //entity.Property(e => e.PrinterId).HasColumnName("PrinterID");
            //entity.Property(e => e.ReportName)
            //    .IsRequired()
            //    .HasMaxLength(50)
            //    .IsUnicode(false);
            //entity.Property(e => e.ReportPath)
            //    .IsRequired()
            //    .IsUnicode(false);
        });
        #endregion
        #region Reports
        modelBuilder.Entity<AccoumodationList>(entity =>
        {
            entity.ToTable("Report.AccoumodationList_TB");
            entity.HasMany(p => p.TheList).WithOne().HasForeignKey(p => p.ListID);
        });
        modelBuilder.Entity<AccoumodationListItem>(entity =>
        {
            entity.ToTable("Report.AccoumodationListItem_TB");
        });
        modelBuilder.Entity<CashPayment>(entity =>
        {
            entity.ToTable("Report.CashPayment_TB");
        });
        modelBuilder.Entity<CheckInList>(entity =>
        {
            entity.ToTable("Report.CheckInList_TB");
            entity.HasMany(p => p.TheList).WithOne().HasForeignKey(p => p.ListID);
        });
        modelBuilder.Entity<CheckInListItem>(entity =>
        {
            entity.ToTable("Report.CheckInListItem_TB");
        });
        modelBuilder.Entity<CheckOutList>(entity =>
        {
            entity.ToTable("Report.CheckOutList_TB");
            entity.HasMany(p => p.TheList).WithOne().HasForeignKey(p => p.ListID);
        });
        modelBuilder.Entity<CheckOutListItem>(entity =>
        {
            entity.ToTable("Report.CheckOutListItem_TB");
        });
        modelBuilder.Entity<DepartureList>(entity =>
        {
            entity.ToTable("Report.DepartureList_TB");
            entity.HasMany(p => p.TheList).WithOne().HasForeignKey(p => p.ListID);
        });
        modelBuilder.Entity<DepartureListItem>(entity =>
        {
            entity.ToTable("Report.DepartureListItem_TB");
        });
        modelBuilder.Entity<Accoumodation>(entity =>
        {
            entity.ToTable("Report.Accoumodation_TB");
            entity.HasMany(p => p.Items).WithOne().HasForeignKey(p => p.AccDtoID);
        });
        modelBuilder.Entity<AccoumodationItem>(entity =>
        {
            entity.ToTable("Report.AccoumodationItem_TB");
        });
        modelBuilder.Entity<ExtendedArrivalList>(entity =>
        {
            entity.ToTable("Report.ExtendedArrivalList_TB");
        });
        modelBuilder.Entity<Folio>(entity =>
        {
            entity.ToTable("Report.Folio_TB");
            entity.HasMany(p => p.AllRegistration).WithOne().HasForeignKey(p => p.FolioID);
            entity.HasMany(p => p.ConfirmedPayment).WithOne().HasForeignKey(p => p.FolioID);
            entity.HasMany(p => p.UnconfirmedPayment).WithOne().HasForeignKey(p => p.FolioID);
            entity.HasMany(p => p.ConfirmedINS).WithOne().HasForeignKey(p => p.FolioID);
            entity.HasMany(p => p.UnconfirmedINS).WithOne().HasForeignKey(p => p.FolioID);
            entity.HasMany(p => p.Transfare).WithOne().HasForeignKey(p => p.FolioID);
            entity.HasMany(p => p.AllRequired).WithOne().HasForeignKey(p => p.FolioID);
        });
        modelBuilder.Entity<AllRegistration>(entity =>
        {
            entity.ToTable("Report.AllRegistration_TB");
        });
        modelBuilder.Entity<ConfirmedPayment>(entity =>
        {
            entity.ToTable("Report.ConfirmedPayment_TB");
        });
        modelBuilder.Entity<UnConfirmedPayment>(entity =>
        {
            entity.ToTable("Report.UnConfirmedPayment_TB");
        });
        modelBuilder.Entity<ConfirmedINS>(entity =>
        {
            entity.ToTable("Report.ConfirmedINS_TB");
        });
        modelBuilder.Entity<UnConfirmedINS>(entity =>
        {
            entity.ToTable("Report.UnConfirmedINS_TB");
        });
        modelBuilder.Entity<Transfare>(entity =>
        {
            entity.ToTable("Report.Transfare_TB");
        });
        modelBuilder.Entity<RequiredItem>(entity =>
        {
            entity.ToTable("Report.RequiredItem_TB");
        });
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Report.Invoice_TB");
            entity.HasMany(p => p.Items).WithOne().HasForeignKey(p => p.InvoiceID);
            entity.HasMany(p => p.Taxs).WithOne().HasForeignKey(p => p.InvoiceID);
        });
        modelBuilder.Entity<InvoiceTax>(entity =>
        {
            entity.ToTable("Report.InvoiceTax_TB");
        });
        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.ToTable("Report.InvoiceItem_TB");
            entity.HasMany(p => p.invoiceItemTaxes).WithOne().HasForeignKey(p => p.InvoiceItemID);
        });
        modelBuilder.Entity<InvoiceItemTax>(entity =>
        {
            entity.ToTable("Report.InvoiceItemTax_TB");
        });
        modelBuilder.Entity<InvoicesList>(entity =>
        {
            entity.ToTable("Report.InvoicesList_TB");
            entity.HasMany(p => p.Items).WithOne().HasForeignKey(p => p.ListID);
        });
        modelBuilder.Entity<InvoicesListItem>(entity =>
        {
            entity.ToTable("Report.InvoicesListItem_TB");
        });
        modelBuilder.Entity<ReceptionPayment>(entity =>
        {
            entity.ToTable("Report.ReceptionPayment_TB");
        });
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Report.Reservation_TB");
            entity.HasMany(p => p.Items).WithOne().HasForeignKey(p => p.ReservationID);
        });
        modelBuilder.Entity<ReservationItem>(entity =>
        {
            entity.ToTable("Report.ReservationItem_TB");
        });
        modelBuilder.Entity<RoomNumber>(entity =>
        {
            entity.ToTable("Report.RoomNumber_TB");
        });
        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.ToTable("Report.Voucher_TB");
            entity.HasMany(p => p.Vouchers).WithOne().HasForeignKey(p => p.VoucherID);
        });
        modelBuilder.Entity<VoucherItem>(entity =>
        {
            entity.ToTable("Report.VoucherItem_TB");
        });
        #endregion
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
