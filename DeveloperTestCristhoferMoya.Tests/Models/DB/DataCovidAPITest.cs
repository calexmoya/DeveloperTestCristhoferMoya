using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeveloperTestCristhoferMoya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTestCristhoferMoya.Tests
{
    [TestClass]
    public class DataCovidAPITest
    {
       

        [TestMethod]
        public async Task GetRegionsTest() {

            DataCovidAPI api = DataCovidAPI.getInstance();

            var result = await api.GetRegions();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public async Task GetCasesRegionTest() {

            DataCovidAPI api = DataCovidAPI.getInstance();

            var result = await api.GetCasesRegion("USA");

            Assert.AreEqual(10, result.Count);

        }

        [TestMethod]
        public async Task GetCasesRegionTestIn()
        {

            DataCovidAPI api = DataCovidAPI.getInstance();

            var result = await api.GetCasesRegion("Guatemala");

            Assert.AreEqual(0, result.Count);

        }

        [TestMethod]
        public async Task GetCasesWorldTest()
        {

            DataCovidAPI api = DataCovidAPI.getInstance();

            var result = await api.GetCasesWorld();

            Assert.IsInstanceOfType(result, typeof(List<TopRegionWorld>));

        }

        [TestMethod]
        public async Task GetCasesWorldTestQty()
        {

            DataCovidAPI api = DataCovidAPI.getInstance();

            var result = await api.GetCasesWorld();

            Assert.AreEqual(10, result.Count);


        }
        [TestMethod]
        public async Task GetRegionsComboTest()
        {

            DataCovidAPI api = DataCovidAPI.getInstance();

            var result = await api.GetRegionsCombo();

            Assert.AreNotEqual(0, result.Count);


        }

    }
}