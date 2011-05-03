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
