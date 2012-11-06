using System.Collections.Generic;
using HierarchyModel.Model;

namespace HierarchyModel.Hierarchy
{
    public class RegionHierarchy
    {
        private List<DivisionHierarchy> _divisions = new List<DivisionHierarchy>();
        public Region CurrentRegion { get; set; }

        public List<DivisionHierarchy> Divisions
        {
            get { return _divisions; }
            set { _divisions = value; }
        }
    }
}
