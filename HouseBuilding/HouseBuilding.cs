using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using NUnit.Framework;
namespace HouseBuilding
{
    public class HouseBuilding
    {
        public Int32 GetMinimum(params String[] args)
        {
            ICollection<Int32> converted;
            var average = GetAverage(args,out converted);
            var pivot = (int)average;

            var effort = GetEffort(converted, pivot);
            return Math.Min(effort[0], effort[1]);
        }

        private int[] GetEffort(IEnumerable<int> args, int pivot)
        {
            var max = pivot + 1;
            var min = pivot - 1;
            var result = 0;
            var result1 = 0;

            foreach (var s in args)
            {
                result1 += CalcWithPivotAsMax(pivot, s, min);
                result += CalcWithPivotAsMin(pivot, s, max);
            }

            return new[] { result, result1 };
        }

        private static int CalcWithPivotAsMin(int pivot, int s, int max)
        {
            int result = 0;
            if (s < pivot)
            {
                result += pivot - s;
            }
            else if (s > max)
            {
                result += s - max;
            }
            return result;
        }

        private static int CalcWithPivotAsMax(int pivot, int s, int min)
        {
            int result = 0;
            if (s < min)
            {
                result += min - s;
            }
            else if (s > pivot)
            {
                result += s - pivot;
            }
            return result;
        }

        private Double GetAverage(IEnumerable<String> args, out ICollection<Int32> converted)
        {
            var sum = 0;
            var i = 0;
            converted = new Collection<int>();

            foreach (var s in args)
            {
                foreach (var c in s)
                {
                    var tmp = Int32.Parse(c.ToString(CultureInfo.InvariantCulture));
                    sum += tmp;
                    i++;
                    converted.Add(tmp);
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
