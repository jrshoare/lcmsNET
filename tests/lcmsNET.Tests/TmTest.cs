﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class TmTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void ConstructorTest()
        {
            // Arrange
            var year = 2021;
            var month = 1;
            var day = 8;
            var hour = 10;
            var minute = 4;
            var second = 32;
            var date = new DateTime(2021, 1, 8, 10, 4, 32);

            // Act
            var target = new Tm(date);
            var expectedYear = year - 1900;
            var expectedMon = month - 1;
            var expectedMday = day;
            var expectedHour = hour;
            var expectedMin = minute;
            var expectedSec = second;
            var expectedWday = (int)date.DayOfWeek;
            var expectedYday = date.DayOfYear - 1;

            // Assert
            var actualYear = target.year;
            var actualMon = target.mon;
            var actualMday = target.mday;
            var actualHour = target.hour;
            var actualMin = target.min;
            var actualSec = target.sec;
            var actualWday = target.wday;
            var actualYday = target.yday;
            var actualIsdst = target.isdst;

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
        public void OperatorDateTimeTest()
        {
            // Arrange
            var expected = new DateTime(2021, 1, 8, 10, 4, 32);
            var target = new Tm(expected);

            // Act
            DateTime actual = target;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                var expected = new DateTime(2021, 1, 8, 10, 4, 32);
                Tm tm = new Tm(expected);

                profile.WriteTag(TagSignature.CalibrationDateTime, tm);
                var tag = profile.ReadTag(TagSignature.CalibrationDateTime);

                // Act
                var target = Tm.FromHandle(tag);
                DateTime actual = target;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
