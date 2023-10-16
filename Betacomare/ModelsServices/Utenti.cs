using System;
using System.Collections.Generic;

namespace Betacomare.ModelsServices;

public partial class Utenti
{
    public int CustomerId { get; set; }

    public string Username { get; set; } = null!;

    public string PswHash { get; set; } = null!;

    public string Salt { get; set; } = null!;
}
