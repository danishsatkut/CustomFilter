using System;
using System.Collections.Generic;
using System.Linq;
using HierarchyModel.Hierarchy;
using HierarchyModel.Model;

namespace HierarchyModel
{
    public static class SampleDataGenerator
    {
        private static readonly List<Region> Regions = new List<Region>();
        private static readonly List<Division> Divisions = new List<Division>();
        private static readonly List<District> Districts = new List<District>();
        private static readonly List<Bsom> Bsoms = new List<Bsom>();
        public static readonly List<Store> Stores = new List<Store>();

        private static List<NationalHierarchy> _completeHierarchy = null;

        public static List<NationalHierarchy> GenerateDataWithHierarchy()
        {
            if (_completeHierarchy == null)
            {
                _completeHierarchy = new List<NationalHierarchy>();
                GenerateData();
                CreateHierarchy();    
            }
            return _completeHierarchy;
        }

        private static void CreateHierarchy()
        {
            var nationalHierarchy = new NationalHierarchy();

            foreach (var region in Regions)
            {
                var regionHierarchy = new RegionHierarchy { CurrentModel =  region };
                var divisionHierarchyLevel = new RegionChildHierarchyLevel { CurrentModel = new RegionChild { Id = 1, Name = "Divisions in Region"}};
                var bsomHierarchyLevel = new RegionChildHierarchyLevel
                                             {CurrentModel = new RegionChild {Id = 2, Name = "BSOMs in Region"}};

                #region Division Hierarchy for this region
                // For Division Hierarchy
                foreach (var divisionHierarchy in from division in Divisions 
                                                  where division.RegionId == region.Id
                                                  select new DivisionHierarchy {CurrentModel = division})
                {
                    foreach (var districtHiearchy in from district in Districts
                                                         where district.DivisionId == divisionHierarchy.CurrentModel.Id
                                                         select new DistrictHierarchy {CurrentModel = district})
                    {
                        foreach (var store in from store in Stores
                                                  where store.DistrictId == districtHiearchy.CurrentModel.Id
                                                  select new StoreHierarchy { CurrentModel = store})
                        {
                            districtHiearchy.ChildCollection.Add(store);
                        }

                        divisionHierarchy.ChildCollection.Add(districtHiearchy);
                    }

                    // Change
                    divisionHierarchyLevel.ChildCollection.Add(divisionHierarchy);
                }

                #endregion

                #region BSOM Hierachy for this region
                // For BSOM hierarchy
                foreach (var bsomHierarchy in from bsom in Bsoms
                                                  where bsom.RegionId == region.Id
                                                  select new BsomHierarchy() { CurrentModel = bsom })
                {
                        foreach (var store in from store in Stores
                                              where store.BsomId == bsomHierarchy.CurrentModel.Id
                                              select new StoreHierarchy { CurrentModel = store })
                        {
                            bsomHierarchy.ChildCollection.Add(store);
                        }

                    bsomHierarchyLevel.ChildCollection.Add(bsomHierarchy);
                }
                #endregion

                // Change
                regionHierarchy.ChildCollection.Add(divisionHierarchyLevel);
                regionHierarchy.ChildCollection.Add(bsomHierarchyLevel);

                nationalHierarchy.Regions.Add(regionHierarchy);
            }

            _completeHierarchy.Add(nationalHierarchy);
        }

        private static void GenerateData()
        {
            var random = new Random();

            for (int i = 0; i < 100; i++)
            {
                Stores.Add(new Store
                {
                    Name = "Store " + i,
                    Id = i + 1,
                    DistrictId = (i / 5) + 1,
                    DivisionId = (i / 10) + 1,
                    BsomId = 0,
                    RegionId = (i / 20) + 1,
                    Value = random.Next(0, 99999999)
                });

                if (i % 5 == 0)
                {
                    Districts.Add(new District
                                       {
                                           Name = string.Format("District {0}", ((i / 5) + 1)),
                                           Id = (i / 5) + 1,
                                           DivisionId = (i / 10) + 1,
                                           RegionId = (i / 20) + 1
                                       });
                }

                if (i % 10 == 0)
                {
                    Divisions.Add(new Division
                                       {
                                           Name = string.Format("Division {0}", ((i / 10) + 1)),
                                           Id = (i / 10) + 1,
                                           RegionId = (i / 20) + 1
                                       });

                    Bsoms.Add(new Bsom
                                  {
                                      Name = string.Format("BSOM {0}", ((i / 10) + 1)),
                                      Id = (i / 10) + 1,
                                      RegionId = (i / 20) + 1
                                  });
                }

                if (i % 20 == 0)
                {
                    Regions.Add(new Region
                                     {
                                         Name = string.Format("Region {0}", ((i / 20) + 1)),
                                         Id = (i / 20) + 1
                                     });
                }
            }

            for (int i = 0; i < 50; i++)
            {
                Stores.Add(new Store
                {
                    Name = "Store " + (i + 100),
                    Id = i + 101,
                    DistrictId = 0,
                    DivisionId = 0,
                    BsomId = (i / 10) + 1,
                    RegionId = (i / 20) + 1,
                    Value = random.Next(0, 99999999)
                });
            }
        }
    }
}
