using System.Collections.Generic;
using HierarchyModel.Model;

namespace HierarchyModel.Hierarchy
{
    public class DivisionHierarchy
    {
        private List<DistrictHierarchy> _districts = new List<DistrictHierarchy>();
        public Division CurrentDivision { get; set; }

        public List<DistrictHierarchy> Districts
        {
            get { return _districts; }
            set { _districts = value; }
        }
    }
}