// ***********************************************************************
// Copyright (c) 2015 Charlie Poole, Rob Prouse
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using NUnit.Common;
using NUnitLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml.XPath;

namespace NUnitLite.Tests
{
    public class Program
    {
        private static string currentResultFilename;

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
                xpathDoc = new XPathDocument(currentResultFilename);
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

        private static void Init(ref string[] arguments, string currResultFilename)
        {
            arguments = new string[]
            {
                "--noh",
                "--workers=10",
                "--labels=After",
                "--trace=Off",
                "--pause",
                $"--result={currResultFilename}",
                "--where:method=~/CanConvert*/ && cat==Base || cat==Fast"
            };
        }

        /// <summary>
        /// The main program executes the tests. Output may be routed to
        /// various locations, depending on the arguments passed.
        /// </summary>
        /// <remarks>Run with --help for a full list of arguments supported</remarks>
        /// <param name="args"></param>
        public static int Main(string[] args)
        {
            string[] listTests = null;
            if ((args[0].ToLower().Equals("/f") || args[0].ToLower().Equals("/file")) && File.Exists(args[1]))
            {
                listTests = File.ReadAllLines(args[1]);
            }
            Console.Title = "ICCT Tool";
            Console.TreatControlCAsInput = true;
            Console.SetWindowSize(102, 25);
            currentResultFilename = "ICCResult1.xml";
            Init(ref args, currentResultFilename);
            int test1;
            int test2;
            using(var writer = new ExtendedTextWrapper(TextWriter.Null))
            {
                test1 = new AutoRun(Assembly.LoadFrom(@"LogAnPattern.Tests.dll")).Execute(args, writer, Console.In);                               
            }
            WriteHeader();
            WriteResult();
            /*var test = new AutoRun().Execute(args);
            Console.Clear();
            WriteHeader();
            WriteResult("IntracallClient");
            return test;*/
            currentResultFilename = "ICCResult2.xml";
            Init(ref args, currentResultFilename);
            using (var writer = new ExtendedTextWrapper(TextWriter.Null))
            {                
                test2 = new AutoRun(Assembly.GetExecutingAssembly()).Execute(args, writer, Console.In);                              
            }
            WriteResult();
            Console.ReadLine();
            return test1 + test2;
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
        }

        private static void WriteResult()
        {
            Console.WriteLine($"\n{new string('#', 100)}\n");
            ICWriteLine("\nTest Files", ConsoleColor.Cyan);
            WirteProperty(null, "name");
            ICWriteLine("\nTest Run Summary", ConsoleColor.Cyan);
            WirteProperty("Overall result", "result");
            WriteParameters(new string[] { "Total", "Passed", "Failed", "Warnings", "Skipped", "Inconclusive" }, "/test-run/test-suite");
            WirteProperty("Start time", "start-time");
            WirteProperty("End time", "end-time");
            WirteProperty("Duration", "duration");      
        }

        private static void WirteProperty(string label, string value, string node = "/test-run", ConsoleColor labelColor = ConsoleColor.Green, ConsoleColor valueColor = ConsoleColor.White)
        {
            ICWrite(label != null ? $"\t{label}: ": "\t", labelColor);
            string vTmp = SearchXPathNavigator(value, node);
            if (label != null && label.Equals("Duration"))
            {
                int vTicks = (int)(decimal.Parse(vTmp.Replace('.', CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToCharArray()[0])) * 1000);
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

        private static string FormatWall(string textToinsert="")
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