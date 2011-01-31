using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Altitude.Tests
{
    /// <summary>
    /// Summary description for AltitudeTest
    /// </summary>
    [TestFixture]
    public class AltitudeTest
    {
        [Test]
        public static void TestCase1()
        {
            AltitudeConfiguration config = new AltitudeConfiguration()
            {
            };

            using (Altitude altitude = new Altitude(config))
            {
                altitude.Connect();

                NodeDef personNodeDef = altitude.CreateNodeDef("Person");

                DateTime startTime = DateTime.Now;
                TimeSpan timeSpan =  TimeSpan.FromSeconds(10);
                while (DateTime.Now < startTime + timeSpan)
                {
                    altitude.CreateNode(personNodeDef);
                }

                Console.WriteLine(string.Format("{0} nodes created in {1} milliseconds", altitude.GetNodes().Count, timeSpan.TotalMilliseconds));
            }
        }

        [Test]
        public static void TestCase2()
        {
            AltitudeConfiguration config = new AltitudeConfiguration()
            {
            };

            using (Altitude altitude = new Altitude(config))
            {
                altitude.Connect();

                NodeDef personNodeDef = altitude.CreateNodeDef("Person");

                DateTime startTime = DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromSeconds(10);
                while (DateTime.Now < startTime + timeSpan)
                {
                    // altitude.CreateNodes();
                }

                Console.WriteLine(string.Format("{0} nodes created in {1} milliseconds", altitude.GetNodes().Count, timeSpan.TotalMilliseconds));
            }
        }
    }
}
