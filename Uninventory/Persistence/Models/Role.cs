using System;
using System.Collections.Generic;

namespace Uninventory.Persistence.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<User> User { get; set; } = new List<User>();
}
