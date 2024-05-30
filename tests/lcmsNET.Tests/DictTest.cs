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
using System.Linq;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class DictTest
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
        public void CreateTest()
        {
            // Arrange

            // Act
            using var dict = Dict.Create(null);

            // Assert
            Assert.IsNotNull(dict);
        }

        [TestMethod()]
        public void DuplicateTest()
        {
            // Arrange

            // Act
            using var dict = Dict.Create(null);
            using var duplicate = dict.Duplicate();

            // Assert
            Assert.IsNotNull(duplicate);
        }

        [TestMethod()]
        public void AddTest()
        {
            // Arrange
            string name = "name";
            string value = "value";

            using var dict = Dict.Create(null);
            using var displayName = MultiLocalizedUnicode.Create(null, 0);
            displayName.SetWide("en", "US", "Hello");

            // Act
            bool added = dict.Add(name, value, displayName, null);

            // Assert
            Assert.IsTrue(added);
        }

        [TestMethod()]
        public void EnumerateTest()
        {
            // Arrange
            using var dict = Dict.Create(null);
            using var mlu = MultiLocalizedUnicode.Create(null, 0);
            mlu.SetASCII("en", "GB", "Hello");

            dict.Add("first", null, null, null);
            dict.Add("second", "second-value", null, null);
            dict.Add("third", "third-value", mlu, null);
            int expected = 3;

            // Act
            var actual = dict.Count();

            var list = dict.ToArray();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            int expected = 3;

            using var profile = Profile.CreatePlaceholder(null);
            using (var dict = Dict.Create(null))
            using (var mlu = MultiLocalizedUnicode.Create(null, 0))
            {
                mlu.SetASCII("en", "GB", "Hello");

                dict.Add("first", null, null, null);
                dict.Add("second", "second-value", null, null);
                dict.Add("third", "third-value", mlu, null);

                profile.WriteTag(TagSignature.Meta, dict);
            }

            // Act
            // implicit call to FromHandle
            using var roDict = profile.ReadTag<Dict>(TagSignature.Meta);
            // Assert
            int actual = roDict.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTagTest()
        {
            // Arrange
            int expected = 3;

            using var profile = Profile.CreatePlaceholder(null);
            using (var dict = Dict.Create(null))
            using (var mlu = MultiLocalizedUnicode.Create(null, 0))
            {
                mlu.SetASCII("en", "GB", "Hello");

                dict.Add("first", null, null, null);
                dict.Add("second", "second-value", null, null);
                dict.Add("third", "third-value", mlu, null);

                profile.WriteTag(TagSignature.Meta, dict);
            }

            // Act
            using (var dict = profile.ReadTag<Dict>(TagSignature.Meta))
            {
                // Assert
                int actual = dict.Count();
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
