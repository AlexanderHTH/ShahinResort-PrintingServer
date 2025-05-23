﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PrintingServer.Domain.Entities;

public partial class Client : BaseEntity
{
    public string ClientName { get; set; }

    public string ClientIp { get; set; }

    public virtual ICollection<ClientReport> ClientReports { get; set; } = new List<ClientReport>();
    public Client()
    {
    }
    public Client(Guid userId, string clientName, string clientIp)
    {
        ClientName = clientName;
        ClientIp = clientIp;
        IsActive = true;
        CreatedOn = DateTime.Now;
        AppUserId = userId;
        Notes = "Created on " + CreatedOn.ToString("dd/MM/yyyy hh:mm:ss tt");
    }
    public void Update(Guid userId, string clientName, string clientIp, bool isdeleted, bool isactive, string name = "", string description = "")
    {
        AppUserId = userId;
        ClientName = clientName;
        ClientIp = clientIp;
        base.Update(isdeleted, isactive, name, description);
    }
    public void Update(Guid userId, string clientName, string clientIp)
    {
        AppUserId = userId;
        ClientName = clientName;
        ClientIp = clientIp;
        base.Update(this.IsDeleted, this.IsActive, this.Name, this.Description);
    }

    public override bool Equals(object obj)
    {
        return obj is Client client &&
               ClientName == client.ClientName &&
               ClientIp == client.ClientIp;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ClientName, ClientIp);
    }
    public override string ToString() => JsonSerializer.Serialize(this);
}