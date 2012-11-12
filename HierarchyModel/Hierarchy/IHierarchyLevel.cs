using System.Collections.Generic;
using HierarchyModel.Model;

namespace HierarchyModel.Hierarchy
{
    public interface IHierarchyLevel
    {
        IModel CurrentModel { get; set; }

        List<IHierarchyLevel> ChildCollection { get; }
    }
}
