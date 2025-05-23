﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PrintingServer.Domain.Entities;

public partial class PrinterErrorLog : BaseEntity
{
    public Guid PrinterId { get; set; }

    public DateTime ErrorDate { get; set; }

    public string Details { get; set; }

    public virtual Printer Printer { get; set; }
    public PrinterErrorLog() { }
    public PrinterErrorLog(Guid userId, Guid printerId, DateTime errorDate, string details)
    {
        AppUserId = userId;
        PrinterId = printerId;
        ErrorDate = errorDate;
        Details = details;
    }
    public void Update(Guid userId, Guid printerId, DateTime errorDate, string details)
    {
        AppUserId = userId;
        PrinterId = printerId;
        ErrorDate = errorDate;
        Details = details;
        base.Update(IsDeleted, IsActive, Name, Description);
    }
    public void Update(Guid userId, Guid printerId, DateTime errorDate, string details, bool isdeleted, bool isactive, string name = "", string description = "")
    {
        AppUserId = userId;
        PrinterId = printerId;
        ErrorDate = errorDate;
        Details = details;
        base.Update(isdeleted, isactive, name, description);
    }
    public override string ToString() => JsonSerializer.Serialize(this);
}