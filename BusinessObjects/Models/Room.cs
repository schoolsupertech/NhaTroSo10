using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomName { get; set; } = null!;

    public int? Quantity { get; set; }

    public int MaxQuantity { get; set; }

    public decimal PriceOfRoom { get; set; }

    public decimal CostOfSharedRoom { get; set; }

    public decimal CostOfElectric { get; set; }

    public decimal CostOfWater { get; set; }

    public decimal CostOfServices { get; set; }

    public DateOnly Payday { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}
