namespace HierarchyModel.Model
{
    public class Store : IModel
    {
        #region Implementation of IModel

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion

        public int Value { get; set; }

        public int RegionId { get; set; }

        public int DivisionId { get; set; }

        public int DistrictId { get; set; }

        public int BsomId { get; set; }
    }
}
