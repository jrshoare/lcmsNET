// Copyright(c) 2019-2021 John Stevenson-Hoare
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Buffers.Binary;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class TmTest
    {
        [TestMethod()]
        public void Tm_WhenInstantiatedFromDateTime_ShouldHaveValuesSet()
        {
            // Arrange
            var date = new DateTime(2021, 1, 8, 10, 4, 32);
            var expectedYear = date.Year - 1900;
            var expectedMon = date.Month - 1;
            var expectedMday = date.Day;
            var expectedHour = date.Hour;
            var expectedMin = date.Minute;
            var expectedSec = date.Second;
            var expectedWday = (int)date.DayOfWeek;
            var expectedYday = date.DayOfYear - 1;

            // Act
            var sut = new Tm(date);

            // Assert
            var actualYear = sut.year;
            var actualMon = sut.mon;
            var actualMday = sut.mday;
            var actualHour = sut.hour;
            var actualMin = sut.min;
            var actualSec = sut.sec;
            var actualWday = sut.wday;
            var actualYday = sut.yday;
            var actualIsdst = sut.isdst;

            Assert.AreEqual(expectedYear, actualYear);
            Assert.AreEqual(expectedMon, actualMon);
            Assert.AreEqual(expectedMday, actualMday);
            Assert.AreEqual(expectedHour, actualHour);
            Assert.AreEqual(expectedMin, actualMin);
            Assert.AreEqual(expectedSec, actualSec);
            Assert.AreEqual(expectedWday, actualWday);
            Assert.AreEqual(expectedYday, actualYday);
            Assert.IsTrue(actualIsdst < 0);
        }

        [TestMethod()]
        public void Tm_WhenInstantiatedFromDateTimeNumber_ShouldHaveValuesSet()
        {
            // Arrange
            ushort year = 2021;
            ushort month = 1;
            ushort day = 8;
            ushort hour = 10;
            ushort minute = 4;
            ushort second = 32;
            DateTimeNumber date = new()
            {
                year = BinaryPrimitives.ReverseEndianness(year),
                month = BinaryPrimitives.ReverseEndianness(month),
                day = BinaryPrimitives.ReverseEndianness(day),
                hours = BinaryPrimitives.ReverseEndianness(hour),
                minutes = BinaryPrimitives.ReverseEndianness(minute),
                seconds = BinaryPrimitives.ReverseEndianness(second)
            };
            var expectedYear = year - 1900;
            var expectedMon = month - 1;
            var expectedMday = day;
            var expectedHour = hour;
            var expectedMin = minute;
            var expectedSec = second;
            var expectedWday = -1;
            var expectedYday = -1;
            var expectedIsDst = 0;

            // Act
            var sut = new Tm(date);

            // Assert
            var actualYear = sut.year;
            var actualMon = sut.mon;
            var actualMday = sut.mday;
            var actualHour = sut.hour;
            var actualMin = sut.min;
            var actualSec = sut.sec;
            var actualWday = sut.wday;
            var actualYday = sut.yday;
            var actualIsDst = sut.isdst;

            Assert.AreEqual(expectedYear, actualYear);
            Assert.AreEqual(expectedMon, actualMon);
            Assert.AreEqual(expectedMday, actualMday);
            Assert.AreEqual(expectedHour, actualHour);
            Assert.AreEqual(expectedMin, actualMin);
            Assert.AreEqual(expectedSec, actualSec);
            Assert.AreEqual(expectedWday, actualWday);
            Assert.AreEqual(expectedYday, actualYday);
            Assert.AreEqual(expectedIsDst, actualIsDst);
        }

        [TestMethod()]
        public void ImplicitOperatorDateTime_WhenInvoked_ShouldConvertToDateTime()
        {
            // Arrange
            var expected = new DateTime(2021, 1, 8, 10, 4, 32);
            var sut = new Tm(expected);

            // Act
            DateTime actual = sut;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ImplicitOperatorDateTimeNumber_WhenInvoked_ShouldConvertToDateTimeNumber()
        {
            // Arrange
            var expected = new DateTime(2022, 08, 03, 11, 49, 53);
            Tm sut = new(expected);

            // Act
            DateTimeNumber actual = sut;

            // Assert
            Assert.AreEqual(expected.Year, BinaryPrimitives.ReverseEndianness(actual.year));
            Assert.AreEqual(expected.Month, BinaryPrimitives.ReverseEndianness(actual.month));
            Assert.AreEqual(expected.Day, BinaryPrimitives.ReverseEndianness(actual.day));
            Assert.AreEqual(expected.Hour, BinaryPrimitives.ReverseEndianness(actual.hours));
            Assert.AreEqual(expected.Minute, BinaryPrimitives.ReverseEndianness(actual.minutes));
            Assert.AreEqual(expected.Second, BinaryPrimitives.ReverseEndianness(actual.seconds));
        }
    }
}
