namespace utilityAccounting.Types
{
    public class IStage
    {
        public int BuildingId { get; set; }
        public int[] Tariffs { get; set; }
        public int[] Payments { get; set; }
    }
}
