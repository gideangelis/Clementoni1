using System;
using System.Collections.Generic;

namespace Clementoni1WebAPI.Models.DB;

public partial class Person
{
    public int Id { get; set; }
    public string? Nome { get; set; }

    public string? Cognome { get; set; }

    public string? NumeroTelefonico { get; set; }

    public int? IdComune { get; set; }

    public virtual Comune? IdComuneNavigation { get; set; }
}
