﻿using System;
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
        public static readonly List<Store> Stores = new List<Store>();

        private static readonly List<RegionHierarchy> CompleteHierarchy = new List<RegionHierarchy>();

        public static List<RegionHierarchy> GenerateDataWithHierarchy()
        {
            GenerateData();
            CreateHierarchy();
            return CompleteHierarchy;
        }

        private static void CreateHierarchy()
        {
            foreach (var region in Regions)
            {
                var regionHierarchy = new RegionHierarchy {CurrentRegion = region};

                foreach (var divisionHierarchy in from division in Divisions 
                                                  where division.RegionId == region.RegionId
                                                  select new DivisionHierarchy {CurrentDivision = division})
                {
                    foreach (var districtHiearchy in from district in Districts
                                                         where district.DivisionId == divisionHierarchy.CurrentDivision.DivisionId
                                                         select new DistrictHierarchy {CurrentDistrict = district})
                    {
                        foreach (var store in from store in Stores
                                                  where store.DistrictId == districtHiearchy.CurrentDistrict.DistrictId
                                                  select store)
                        {
                            districtHiearchy.Stores.Add(store);
                        }

                        divisionHierarchy.Districts.Add(districtHiearchy);
                    }

                    regionHierarchy.Divisions.Add(divisionHierarchy);
                }

                CompleteHierarchy.Add(regionHierarchy);
            }
        }

        private static void GenerateData()
        {
            var random = new Random();

            for (int i = 0; i < 100; i++)
            {
                Stores.Add(new Store
                {
                    StoreName = "Store " + i,
                    StoreId = i + 1,
                    DistrictId = (i / 5) + 1,
                    DivisionId = (i / 10) + 1,
                    RegionId = (i / 20) + 1,
                    Value = random.Next(0, 99999999)
                });

                if (i % 5 == 0)
                {
                    Districts.Add(new District
                                       {
                                           DistrictName = string.Format("District {0}", ((i / 5) + 1)),
                                           DistrictId = (i / 5) + 1,
                                           DivisionId = (i / 10) + 1,
                                           RegionId = (i / 20) + 1
                                       });
                }

                if (i % 10 == 0)
                {
                    Divisions.Add(new Division
                                       {
                                           DivisionName = string.Format("Division {0}", ((i / 10) + 1)),
                                           DivisionId = (i / 10) + 1,
                                           RegionId = (i / 20) + 1
                                       });
                }

                if (i % 20 == 0)
                {
                    Regions.Add(new Region
                                     {
                                         RegionName = string.Format("Region {0}", ((i / 20) + 1)),
                                         RegionId = (i / 20) + 1
                                     });
                }
            }
        }
    }
}
