namespace HierarchyModel.Model
{
    public class Bsom : IModel
    {
        #region Implementation of IModel

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion

        public int RegionId { get; set; }
    }
}
