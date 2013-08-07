using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
namespace HouseBuilding
{
    public class HouseBuilding
    {
        public Int32 GetMinimum(params String[] args)
        {
            var average = GetAverage(args);
            var min = (int)average;
            var max = min + 1;

            var min1 = min - 1;
            var max1 = min;

            var effort1 = GetEffort(args, min, max);
            var effort2 = GetEffort(args, min1, max1);
            return Math.Min(effort1, effort2);
        }

        private int GetEffort(string[] args, int min, int max)
        {
            int result = 0;
            foreach (var s in args)
                foreach (var tmp in s.Select(c => int.Parse(c.ToString())))
                {
                    if (tmp < min)
                    {
                        result += min - tmp;
                    }
                    else if (tmp > max)
                    {
                        result += tmp - max;
                    }
                }

            return result;
        }

        private Double GetAverage(IEnumerable<string> args)
        {
            var sum = 0;
            var i = 0;
            foreach (var s in args)
            {
                foreach (var c in s)
                {
                    var tmp = int.Parse(c.ToString());
                    sum += tmp;
                    i++;
                }
            }

            return (Double)sum / i;
        }
    }

    [TestFixture]
    public class HouseBuildingTests
    {

        [TestCase(7, new[] { "54454", "61551" })]
        [TestCase(2, new[] { "10", "31" })]
        [TestCase(0, new[] { "989" })]
        [TestCase(8, new[] { "90" })]
        [TestCase(53, new[] { "5781252", "2471255", "0000291", "1212489" })]
        [Test]
        public void UseCases(Int32 expected, params String[] values)
        {
            var instance = new HouseBuilding();
            var result = instance.GetMinimum(values);
            Assert.AreEqual(expected, result);
        }
    }
}
