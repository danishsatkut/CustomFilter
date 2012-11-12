﻿namespace HierarchyModel.Model
{
    public class District : IModel
    {
        #region Implementation of IModel

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion

        public int RegionId { get; set; }

        public int DivisionId { get; set; }
    }
}
