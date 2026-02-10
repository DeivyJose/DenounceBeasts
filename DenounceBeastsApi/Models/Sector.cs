using System;
using System.Collections.Generic;

namespace DenounceBeastsApi.Models;

public partial class Sector
{
    public int SectorId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Municipality> Municipalities { get; set; } = new List<Municipality>();
}
