﻿using System;
using System.Collections.Generic;

namespace WebApi.Persistence.Entities;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public decimal Price { get; set; }

    public int SellerId { get; set; }

    public int? SoldToCustomer { get; set; }

    public virtual Seller Seller { get; set; } = null!;
}
