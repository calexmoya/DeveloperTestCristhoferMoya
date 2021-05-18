using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace DeveloperTestCristhoferMoya.Models.Files
{
    public class GenerateFiles
    {
        //Singleton 
        private static GenerateFiles instance;
        private GenerateFiles() { }
        public static GenerateFiles getInstance()
        {
            if (instance == null)
            {
                instance = new GenerateFiles();
            }
            return instance;
        }
        //End Singleton
        public string GenerateToCSV<T>(IEnumerable<T> list)
        {
            StringBuilder sList = new StringBuilder();

            Type type = typeof(T);
            var props = type.GetProperties();
            sList.Append(string.Join(",", 
                props.Select(p => p.Name)));
            sList.Append(Environment.NewLine);

            foreach (var element in list)
            {
                sList.Append(string.Join(",", props.Select(p => p.GetValue(element, null))));
                sList.Append(Environment.NewLine);
            }

            return sList.ToString();
        }
        public string GenerateToXML<T>(T genericObject) {
            var ser = new XmlSerializer(typeof(T), new XmlRootAttribute("Data"));

            var stringwriter = new System.IO.StringWriter();
            ser.Serialize(stringwriter, genericObject);

            return stringwriter.ToString();
        }
    }
}