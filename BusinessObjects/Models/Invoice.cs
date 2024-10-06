using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int RoomId { get; set; }

    public decimal Price { get; set; }

    public int CapacityOfElectric { get; set; }

    public decimal? PriceOfElectric { get; set; }

    public int CapacityOfWater { get; set; }

    public decimal? PriceOfWater { get; set; }

    public decimal PriceOfServices { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime InvoiceDate { get; set; }

    public virtual Room Room { get; set; } = null!;
}
