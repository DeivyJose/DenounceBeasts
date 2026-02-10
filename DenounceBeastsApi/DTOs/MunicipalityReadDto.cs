namespace DenounceBeastsApi.DTOs
{
    public class MunicipalityReadDto
    {
        public int MunicipalityId { get; set; }
        public string Name { get; set; } = null!;
        public int SectorId { get; set; }
    }
}
