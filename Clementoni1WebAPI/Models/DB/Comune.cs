using System;
using System.Collections.Generic;

namespace Clementoni1WebAPI.Models.DB;

public partial class Comune
{
    public int Id { get; set; }

    public string nome { get; set; } = null!;

    public virtual ICollection<Person> Person { get; set; } = new List<Person>();
}
