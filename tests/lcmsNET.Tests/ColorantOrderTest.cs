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

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ColorantOrderTest
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
            byte[] expected = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            // Act
            var target = new ColorantOrder(expected);

            // Assert
            var actual = (byte[])target;
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConstructorTestNullArrayArgument()
        {
            // Arrange
            byte[] expected = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new ColorantOrder(expected));
        }

        [TestMethod()]
        public void ConstructorTestInvalidArrayLength()
        {
            // Arrange
            const int invalidLength = 13;
            byte[] expected = new byte[invalidLength];

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new ColorantOrder(expected));
        }

        [TestMethod()]
        public void ByteArrayOperatorTest()
        {
            // Arrange
            byte[] expected = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            // Act
            var target = new ColorantOrder(expected);

            // Assert
            byte[] actual = target;
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ColorantOrderOperatorTest()
        {
            // Arrange
            byte[] expected = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            // Act
            var target = (ColorantOrder)expected;

            // Assert
            byte[] actual = target;
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            using (var profile = Profile.CreatePlaceholder(null))
            {
                byte[] expected = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

                profile.WriteTag(TagSignature.ColorantOrder, (ColorantOrder)expected);

                // Act
                // implicit call to FromHandle
                byte[] actual = profile.ReadTag<ColorantOrder>(TagSignature.ColorantOrder);

                // Assert
                CollectionAssert.AreEqual(expected, actual);
            }
        }
    }
}
