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

namespace lcmsNET.Tests
{
    [TestClass()]
    public class SignatureTest
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
            uint expected = 0x237F9AB1;

            // Act
            var target = new Signature(expected);

            // Assert
            uint actual = target;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UintOperatorTest()
        {
            // Arrange
            uint expected = 0x12345982;

            // Act
            var target = (Signature)expected;

            // Assert
            uint actual = target;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SignatureOperatorTest()
        {
            // Arrange
            uint expected = 0x75DF3108;

            // Act
            var target = (Signature)expected;
            uint actual = target;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            using var profile = Profile.CreatePlaceholder(null);
            var expected = (Signature)0xF32794E2;

            profile.WriteTag(TagSignature.Technology, expected);

            // Act
            // implicit call to FromHandle
            var actual = profile.ReadTag<Signature>(TagSignature.Technology);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FromHandleTest2()
        {
            // Arrange
            using var profile = Profile.CreatePlaceholder(null);
            var expected = new Signature(0x962E1457);

            profile.WriteTag(TagSignature.Technology, expected);

            // Act
            // implicit call to FromHandle
            var actual = profile.ReadTag<Signature>(TagSignature.Technology);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
