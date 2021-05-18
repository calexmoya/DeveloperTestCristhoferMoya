using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DeveloperTestCristhoferMoya.Controllers;
using DeveloperTestCristhoferMoya.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeveloperTestCristhoferMoya.Tests.Controllers
{
    [TestClass]
    public class CovidStatsControllerTest
    {
        [TestMethod]
        public async Task TopCasesCovidTestNull()
        {
            // Arrange
            CovidStatsController controller = new CovidStatsController();

            // Act
            ViewResult result = await controller.TopCasesCovid() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TopCasesCovidTestCorrectlyView()
        {
            CovidStatsController controller = new CovidStatsController();

            ViewResult result = await controller.TopCasesCovid() as ViewResult;

            Assert.AreEqual("TopCasesCovid", result.ViewName);
        }

        [TestMethod]
        public async Task TableTopRegionTestNull()
        {
            CovidStatsController controller = new CovidStatsController();

            ViewResult result = await controller.TableTopRegion() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TableTopRegionCorrectlyView()
        {
            CovidStatsController controller = new CovidStatsController();

            ViewResult result = await controller.TableTopRegion() as ViewResult;

            Assert.AreEqual("TableTopRegion", result.ViewName);
        }

        [TestMethod]
        public async Task TableTopProvinceTestNull()
        {
            CovidStatsController controller = new CovidStatsController();

            ViewResult result = await controller.TableTopProvince("US") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TableTopProvinceCorrectlyView()
        {
            CovidStatsController controller = new CovidStatsController();

            ViewResult result = await controller.TableTopProvince("USA") as ViewResult;
            List<CasesRegion> data = (List<CasesRegion>)result.ViewData.Model;

            Assert.AreEqual(10, data.Count);
        }

        [TestMethod]
        public async Task TableTopProvinceTestNegative()
        {
            CovidStatsController controller = new CovidStatsController();

            ViewResult result = await controller.TableTopProvince("USA") as ViewResult;

            Assert.AreEqual("TableTopProvince", result.ViewName);
        }
    }
}
