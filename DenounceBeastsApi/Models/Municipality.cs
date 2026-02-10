using System;
using System.Collections.Generic;

namespace DenounceBeastsApi.Models;

public partial class Municipality
{
    public int MunicipalityId { get; set; }

    public string Name { get; set; } = null!;

    public int SectorId { get; set; }

    public virtual Sector Sector { get; set; } = null!;
}
