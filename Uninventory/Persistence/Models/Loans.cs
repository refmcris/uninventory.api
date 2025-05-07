using System;
using System.Collections.Generic;

namespace Uninventory.Persistence.Models;

public partial class Loans
{
    public int LoanId { get; set; }

    public int EquipmentId { get; set; }

    public int UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? Status { get; set; }

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
