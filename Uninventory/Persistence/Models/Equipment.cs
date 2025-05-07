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

    public string? Model { get; set; }

    public string? Description { get; set; }

    public string? Specifications { get; set; }

    public string? Image { get; set; }

    public virtual Categories Category { get; set; } = null!;

    public virtual ICollection<Loans> Loans { get; set; } = new List<Loans>();
}
