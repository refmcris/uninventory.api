using System;
using System.Collections.Generic;

namespace Uninventory.Persistence.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? UserPassword { get; set; }

    public int? UserRole { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Delete { get; set; }

    public virtual ICollection<Loans> Loans { get; set; } = new List<Loans>();

    public virtual Role? UserRoleNavigation { get; set; }
}
