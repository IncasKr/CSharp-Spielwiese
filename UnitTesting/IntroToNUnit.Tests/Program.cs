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
using System;
using System.Reflection;

namespace NUnitLite.Tests
{
    public class Program
    {
        private static string currentResultFilename;

        /// <summary>
        /// The main program executes the tests. Output may be routed to
        /// various locations, depending on the arguments passed.
        /// </summary>
        /// <remarks>Run with --help for a full list of arguments supported</remarks>
        /// <param name="args"></param>
        public static int Main(string[] args)
        {
            string currentResultFilename = "ICCResult.xml";
            args = new string[]
            {
                "--noh",
                "--workers=10",
                "--encoding=ascii",
                "--labels=After",
                "--trace=Off",
                $"--result={currentResultFilename}.xml",
                "--where:method=~/CanConvert*/ && cat==Base || cat==Fast"
            };
            //var test = new AutoRun(Assembly.LoadFrom(@"C:\Users\Douabalet\Google Drive\INCAS\IncasIntern\UnitTesting\LogAnPattern.Tests\bin\Debug\LogAnPattern.Tests.dll")).Execute(args);
            /*var test = new AutoRun().Execute(args);
            return test;*/
            using (var writer = new ExtendedTextWrapper(Console.Out))
            {                
                var test = new AutoRun(Assembly.GetExecutingAssembly())
                    .Execute(args, writer, Console.In);
                Console.Clear();
                return 0;
            }             
        }
    }
}