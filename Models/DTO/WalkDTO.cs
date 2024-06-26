namespace NZWalks.API.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageURL { get; set; }

        // No longer need the following two because of the last two
        /* 
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
        */

        public RegionDTO Region { get; set; }
        public DifficultyDTO Difficulty { get; set; }
    }
}
