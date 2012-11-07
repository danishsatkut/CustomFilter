using System.Collections.Generic;

namespace HierarchyModel.Hierarchy
{
    public class NationalHierarchy
    {
        private List<RegionHierarchy> _regions = new List<RegionHierarchy>();
        public List<RegionHierarchy> Regions
        {
            get { return _regions; }
            set { _regions = value; }
        }
    }
}
