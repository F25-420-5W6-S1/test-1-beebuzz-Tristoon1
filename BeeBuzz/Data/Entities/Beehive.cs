namespace BeeBuzz.Data.Entities
{
    public class Beehive
    {
        public int BeehiveId { get; set; }

        public string Address { get; set; }

        public BeehiveStatus Status { get; set; }

        public string DeactivationReason { get; set; }
    }

    public enum BeehiveStatus
    {
        Active,
        Inactive
    }
}
