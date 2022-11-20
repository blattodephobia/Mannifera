namespace Mannifera.DataStructures
{
    public class SnapshotDto
    {
        public PricePoint[]? Bids { get; init; }
        public PricePoint[]? Asks { get; init; }
    }
}