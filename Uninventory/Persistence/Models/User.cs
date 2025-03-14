﻿using System;
using System.Collections.Generic;

namespace Uninventory.Persistence.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? UserPassword { get; set; }

    public string? UserRole { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Delete { get; set; }
}
