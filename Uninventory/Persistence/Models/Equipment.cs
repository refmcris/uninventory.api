using System;
using System.Collections.Generic;

namespace Uninventory.Persistence.Models;

public partial class Equipment
{
    public int EquipmentId { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public string SerialNumber { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateTime? PurchaseDate { get; set; }

    public DateTime? WarrantyDate { get; set; }

    public DateTime CreatedAt { get; set; }
}
