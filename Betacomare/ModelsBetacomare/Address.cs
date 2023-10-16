using System;
using System.Collections.Generic;

namespace Betacomare.ModelsBetacomare;

public partial class Address
{
    public int AddressId { get; set; }

    public string Username { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string City { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public string? StateProvince { get; set; }

    public string? CountryRegion { get; set; }

    public string State { get; set; } = null!;

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; } = new List<ShoppingCart>();

    public virtual Customer UsernameNavigation { get; set; } = null!;
}
