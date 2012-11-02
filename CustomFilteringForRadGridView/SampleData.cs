namespace CustomFilteringForRadGridView
{
    public class StoreData
    {
        public string StoreName { get; set; }

        public int RegionId { get; set; }

        public int Value { get; set; }

        public int DivisionId { get; set; }
    }

    public class RegionData
    {
        public string RegionName { get; set; }

        public int RegionId { get; set; }
    }

    public class DivisionData
    {
        public string DivisionName { get; set; }

        public int DivisionId { get; set; }

        public int RegionId { get; set; }
    }
}