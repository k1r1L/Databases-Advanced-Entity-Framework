namespace PhotographyWorkshops.Models.Contracts
{
    public interface ICamera : IMakeable
    {
        string Model { get; set; }

        bool IsFullFrame { get; set; }

        int MinIso { get; set; }

        int MaxIso { get; set; }
    }
}
