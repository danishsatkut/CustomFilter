using System.Collections.Generic;
using HierarchyModel.Model;

namespace HierarchyModel.Hierarchy
{
    public class DistrictHierarchy
    {
        private List<Store> _stores = new List<Store>();
        public District CurrentDistrict { get; set; }

        public List<Store> Stores
        {
            get { return _stores; }
            set { _stores = value; }
        }
    }
}