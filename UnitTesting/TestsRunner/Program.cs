using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace TestsRunner
{
    class Program
    {
        private static string xMLFilePath;

        /// <summary>
        /// Retrieve the value of the attribute of the node in the configuration file
        /// </summary>
        /// <param name="xPathString">XPath node search expression</param>
        /// <param name="attribute">Attribute to search</param>
        /// <returns>A List containing the attributes searched</returns>
        private static string SearchXPathNavigator(string attribute, string xPathString = "/test-run")
        {
            // Initializing variables
            XPathDocument xpathDoc;
            XPathNavigator xpathNavigator;
            XPathNodeIterator xpathNodeIterator;
            XPathExpression expr;
            List<string> listOfAttributes = new List<string>();

            try
            {
                xpathDoc = new XPathDocument(xMLFilePath);
                xpathNavigator = xpathDoc.CreateNavigator();

                expr = xpathNavigator.Compile(xPathString);
                xpathNodeIterator = xpathNavigator.Select(expr);

                while (xpathNodeIterator.MoveNext())
                {
                    // Gets the attribut
                    listOfAttributes.Add(xpathNodeIterator.Current.GetAttribute(attribute, ""));
                }
            }
            catch
            {
                return null;
            }
            return listOfAttributes[0];
        }

        static void Main(string[] args)
        {
            Console.Title = "ICCT Tool";
            Console.TreatControlCAsInput = true;
            Console.SetWindowSize(102, 25);
            xMLFilePath = "ICCResult.xml";
            WriteHeader();
            WriteResult("IntracallClient");

        }

        private static void WriteHeader()
        {
            ICWriteLine("ICTestsTool v1.0.0", ConsoleColor.Green);
            ICWriteLine($"Copyright (c) 2017 INCAS GmbH", ConsoleColor.Green);
            ICWriteLine("\nRuntime Environment", ConsoleColor.Cyan);
            WirteProperty("OS Version", "os-version", "/test-run/test-suite/environment");
            WirteProperty("OS Platform", "platform", "/test-run/test-suite/environment");
            WirteProperty("Machine name", "machine-name", "/test-run/test-suite/environment");
            WirteProperty("User", "user", "/test-run/test-suite/environment");
            WirteProperty("User Domain", "user-domain", "/test-run/test-suite/environment");
            Console.WriteLine($"\n\n{new string('#', 100)}");
        }

        private static void WriteResult(string testName)
        {
            ICWriteLine("\nTest Files", ConsoleColor.Cyan);
            WirteProperty(null, "name");
            ICWriteLine("\nTest Run Summary", ConsoleColor.Cyan);
            WirteProperty("Overall result", "result");
            WriteParameters(new string[] { "Total", "Passed", "Failed", "Warnings", "Skipped", "Inconclusive" }, "/test-run/test-suite");
            WirteProperty("Start time", "start-time");
            WirteProperty("End time", "end-time");
            WirteProperty("Duration", "duration");
            Console.ReadLine();
        }

        private static void WirteProperty(string label, string value, string node = "/test-run", ConsoleColor labelColor = ConsoleColor.Green, ConsoleColor valueColor = ConsoleColor.White)
        {
            ICWrite(label != null ? $"\t{label}: " : "\t", labelColor);
            string vTmp = SearchXPathNavigator(value, node);
            if (label != null && label.Equals("Duration"))
            {
                int vTicks = (int)(decimal.Parse(vTmp) * 1000);
                TimeSpan vTime = new TimeSpan(0, 0, 0, 0, vTicks);
                ICWriteLine(string.Format("{0:g}", vTime), valueColor);
            }
            else
            {
                ICWriteLine(vTmp, valueColor);
            }
        }

        private static void WriteParameters(string[] labels, string node = "/test-run", ConsoleColor labelColor = ConsoleColor.Green, int numberOfTab = 1)
        {
            string result = string.Empty;

            ICWrite(new string('\t', numberOfTab));
            foreach (var label in labels)
            {
                ICWrite($"{label}: ", labelColor);
                switch (label)
                {
                    case "Failed":
                    case "Failures":
                    case "Error":
                    case "Invalid":
                        ICWrite(SearchXPathNavigator(label.ToLower(), node), ConsoleColor.Red);
                        break;
                    case "Skipped":
                    case "Ignored":
                    case "Explicit":
                    case "Other":
                        ICWrite(SearchXPathNavigator(label.ToLower(), node), ConsoleColor.Yellow);
                        break;
                    case "Warnings":
                        ICWrite(SearchXPathNavigator(label.ToLower(), node), ConsoleColor.Magenta);
                        break;
                    case "Inconclusive":
                        ICWrite(SearchXPathNavigator(label.ToLower(), node), ConsoleColor.Blue);
                        break;
                    default:
                        ICWrite(SearchXPathNavigator(label.ToLower(), node));
                        break;
                }
                if (!labels[labels.Length - 1].Equals(label))
                {
                    ICWrite(",  ", ConsoleColor.Green);
                }
            }
            ICWriteLine();
        }

        private static void ICWrite(string value, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ResetColor();
        }

        private static void ICWriteLine(string value = "", ConsoleColor color = ConsoleColor.White)
        {
            ICWrite($"{value}\n", color);
        }

        private static string FormatWall(string textToinsert = "")
        {
            string wallString = new string('#', 80);
            if (string.IsNullOrWhiteSpace(textToinsert))
            {
                return wallString;
            }
            else if (textToinsert.Length <= wallString.Length - 4)
            {
                return wallString.Remove(0, textToinsert.Length).Insert(10, textToinsert);
            }
            else
            {
                var t = textToinsert.Remove(5, (textToinsert.Length - wallString.Length) + 5).Insert(5, "~");
                return FormatWall(t);
            }
        }
    }
}
