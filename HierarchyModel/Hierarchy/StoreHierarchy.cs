using System.Collections.Generic;
using HierarchyModel.Model;

namespace HierarchyModel.Hierarchy
{
    public class StoreHierarchy : IHierarchyLevel
    {
        public StoreHierarchy()
        {
            ChildCollection = null;
        }

        #region Implementation of IHierarchyLevel

        public IModel CurrentModel { get; set; }
        public List<IHierarchyLevel> ChildCollection { get; private set; }

        #endregion
    }
}
