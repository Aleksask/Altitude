//
// Copyright (C) 2011 Thomas Mitchell
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
//
//

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Altitude.Air;
using NUnit.Framework;

namespace AltitudeTests.Air
{
    /// <summary>
    /// Summary description for AltitudeTest
    /// </summary>
    [TestFixture]
    public class AirTest
    {
        [Test]
        public static void TestCase1()
        {
            // Test update

            var config = new AirConfiguration()
            {
            };

            using (var air = new Altitude.Air.Air(config))
            {
                air.Connect();

                NodeDef personNodeDef = air.CreateNodeDef("Person");

                DateTime startTime = DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromSeconds(10);
                while (DateTime.Now < startTime + timeSpan)
                {
                    air.CreateNode(personNodeDef);
                }

                Console.WriteLine(string.Format("{0} nodes created in {1} milliseconds", air.GetNodes().Count, timeSpan.TotalMilliseconds));
            }
        }

        [Test]
        public static void TestCase2()
        {
            var config = new AirConfiguration()
            {
            };

            using (var air = new Altitude.Air.Air(config))
            {
                air.Connect();

                NodeDef personNodeDef = air.CreateNodeDef("Person");

                DateTime startTime = DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromSeconds(10);
                while (DateTime.Now < startTime + timeSpan)
                {
                    // altitude.CreateNodes();
                }

                Console.WriteLine(string.Format("{0} nodes created in {1} milliseconds", air.GetNodes().Count, timeSpan.TotalMilliseconds));
            }
        }
    }
}
