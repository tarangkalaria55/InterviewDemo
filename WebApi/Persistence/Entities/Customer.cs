using System;
using System.Collections.Generic;

namespace WebApi.Persistence.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
