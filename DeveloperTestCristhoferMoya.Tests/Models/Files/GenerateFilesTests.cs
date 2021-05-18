using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeveloperTestCristhoferMoya.Models.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperTestCristhoferMoya.Models;
using System.IO;
using System.Xml.Serialization;

namespace DeveloperTestCristhoferMoya.Tests
{
    [TestClass()]
    public class GenerateFilesTests
    {
        [TestMethod()]
        public void GenerateToCSVTest()
        {
            GenerateFiles files = GenerateFiles.getInstance();

            List<Region> testList = new List<Region>();
            testList.Add(new Region { iso = "P1", name = "prueba1" });
            testList.Add(new Region { iso = "P2", name = "prueba2" });
            StringBuilder sList = new StringBuilder();
            sList.Append("iso,name");
            sList.Append(Environment.NewLine);
            sList.Append("P1,prueba1");
            sList.Append(Environment.NewLine);
            sList.Append("P2,prueba2");
            sList.Append(Environment.NewLine);
            var result = files.GenerateToCSV(testList);

            Assert.AreEqual(result,sList.ToString());

        }

        [TestMethod()]
        public void GenerateToXMLTest()
        {
            GenerateFiles files = GenerateFiles.getInstance();

            List<Region> testList = new List<Region>();
            testList.Add(new Region("P1", "prueba1"));
            testList.Add(new Region("P2", "prueba2"));
            StringWriter sw = new StringWriter();
            XmlSerializer s = new XmlSerializer(testList.GetType(), new XmlRootAttribute("Data"));
            s.Serialize(sw, testList);
            var stest= sw.ToString();

            var result = files.GenerateToXML<List<Region>>(testList);

            Assert.AreEqual(result, stest);

        }
    }
}