using System;
using System.Collections.Generic;

namespace Betacomare.ModelsBetacomare;

public partial class Customer
{
    public string Username { get; set; } = null!;

    public string? Title { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Status { get; set; }

    public string? Phone { get; set; }

    public DateTime? BirthDay { get; set; }

    public virtual ICollection<ModelsBetacomare.Address> Addresses { get; } = new List<ModelsBetacomare.Address>();
}
