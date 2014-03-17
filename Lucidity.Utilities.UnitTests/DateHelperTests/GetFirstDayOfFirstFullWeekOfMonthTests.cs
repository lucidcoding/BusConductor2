using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Lucidity.Utilities.UnitTests.DateHelperTests
{
    [TestFixture]
    public class GetFirstDayOfFirstFullWeekOfMonthTests
    {
        [Test]
        [TestCase(2000, 1, 2000, 1, 3)]
        [TestCase(2000, 5, 2000, 5, 1)]
        [TestCase(2000, 6, 2000, 6, 5)]
        [TestCase(2005, 1, 2005, 1, 3)]
        [TestCase(2005, 6, 2005, 6, 6)]
        [TestCase(2005, 8, 2005, 8, 1)]
        [TestCase(2010, 1, 2010, 1, 4)]
        [TestCase(2010, 6, 2010, 6, 7)]
        [TestCase(2013, 12, 2013, 12, 2)]
        public void ReturnsCorrectFirstFullWeekOfMonth(
            int year, 
            int month, 
            int expectedResultYear, 
            int expectedResultMonth, 
            int expectedResultDay)
        {
            var expectedResult = new DateTime(expectedResultYear, expectedResultMonth, expectedResultDay);
            var actualResult = DateHelper.GetFirstDayOfFirstFullWeekOfMonth(year, month);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(2000, 1, 1999, 12, 27)]
        [TestCase(2000, 5, 2000, 5, 1)]
        [TestCase(2000, 6, 2000, 5, 29)]
        [TestCase(2005, 1, 2004, 12, 27)]
        [TestCase(2005, 6, 2005, 5, 30)]
        [TestCase(2005, 8, 2005, 8, 1)]
        [TestCase(2010, 1, 2009, 12, 28)]
        [TestCase(2010, 6, 2010, 5, 31)]
        [TestCase(2013, 12, 2013, 11, 25)]
        public void ReturnsCorrectFirstPartWeekOfMonth(
            int year,
            int month,
            int expectedResultYear,
            int expectedResultMonth,
            int expectedResultDay)
        {
            var expectedResult = new DateTime(expectedResultYear, expectedResultMonth, expectedResultDay);
            var actualResult = DateHelper.GetFirstDayOfFirstPartWeekOfMonth(year, month);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
