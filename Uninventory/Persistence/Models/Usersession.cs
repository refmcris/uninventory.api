using System;
using System.Collections.Generic;

namespace Uninventory.Persistence.Models;

public partial class Usersession
{
    public Guid SessionId { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public bool Inactive { get; set; }

    public DateTime ExpiresOn { get; set; }

    public int InsertBy { get; set; }

    public DateTime InsertOn { get; set; }

    public virtual User InsertByNavigation { get; set; } = null!;

    public virtual User? User { get; set; }
}
