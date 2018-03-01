using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XmlFileLoad
{
    enum TestTypeResult
    {
        Failed,
        Passed,
        Skipped
    }
    class Program
    {
        public static List<string> GetXmlAttibutes(string fileName, TestTypeResult type = TestTypeResult.Failed)
        {
            XDocument doc = XDocument.Load(fileName);
            IEnumerable <XElement> cats = doc.Root.Element("test-suite").Element("test-suite").Element("test-suite").Element("test-suite").Elements("test-suite");
            List<string> methods = new List<string>();
            foreach (var cat in cats)
            {
                // XPath expression  
                IEnumerable<XElement> parameterizedMethods = cat.XPathSelectElements("./test-suite");
                IEnumerable<XElement> caseMethods = cat.XPathSelectElements("./test-case");

                foreach (XElement elp in parameterizedMethods)
                {
                    IEnumerable<XElement> tmpCaseMethods = elp.XPathSelectElements("./test-case");
                    foreach (var el in tmpCaseMethods)
                    {
                        if (el.Attribute("result").Value.Equals(type.ToString()))
                        {
                            methods.Add(el.Attribute("name").Value);
                        }
                    }                    
                }

                foreach (XElement el in caseMethods)
                {
                    if (el.Attribute("result").Value.Equals(type.ToString()))
                    {
                        methods.Add(el.Attribute("name").Value);
                    }
                }               
            }
            return methods;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("List of attributes from xml");
            GetXmlAttibutes("TestResult.xml");
            Console.ReadLine();
        }
    }
}
