using System;
using System.Collections.Generic;

namespace Betacomare.ModelsBetacomare;

public partial class ShoppingCart
{
    public string Username { get; set; } = null!;

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Qty { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;
}
