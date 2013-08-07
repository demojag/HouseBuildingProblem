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
            var pivot = (int)average;

            var effort = GetEffort(args, pivot);
            return Math.Min(effort[0], effort[1]);
        }

        private int[] GetEffort(IEnumerable<string> args, int pivot)
        {
            var max = pivot + 1;
            var min = pivot - 1;
            int result = 0;
            int result1 = 0;

            foreach (var s in args)
                foreach (var tmp in s.Select(c => int.Parse(c.ToString())))
                {
                    if (tmp < min)
                    {
                        result1 += min - tmp;
                    }
                    else if (tmp > pivot)
                    {
                        result1 += tmp - pivot;
                    }

                    if (tmp < pivot)
                    {
                        result += pivot - tmp;
                    }
                    else if (tmp > max )
                    {
                        result += tmp - max;
                    }
                }

            return new []{result, result1};
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
