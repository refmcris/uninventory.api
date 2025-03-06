using System;
using System.Collections.Generic;

namespace Uninventory.Persistence.Models;

public partial class Categories
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
