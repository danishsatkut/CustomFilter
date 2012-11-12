using System.Collections.Generic;
using HierarchyModel.Model;

namespace HierarchyModel.Hierarchy
{
    public class DivisionHierarchy : IHierarchyLevel
    {
        private readonly List<IHierarchyLevel> _districts = new List<IHierarchyLevel>();

        #region Implementation of IHierarchyLevel

        public IModel CurrentModel { get; set; }
        public List<IHierarchyLevel> ChildCollection { get { return _districts; } }

        #endregion
    }
}