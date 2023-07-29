using System;
using System.Collections.Generic;

namespace WebApi.Persistence.Entities;

public partial class Seller
{
    public int SellerId { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
