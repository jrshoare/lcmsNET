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
            using (var dict = Dict.Create(null))
            {
                // Assert
                Assert.IsNotNull(dict);
            }
        }

        [TestMethod()]
        public void DuplicateTest()
        {
            // Arrange

            // Act
            using (var dict = Dict.Create(null))
            using (var duplicate = dict.Duplicate())
            {
                // Assert
                Assert.IsNotNull(duplicate);
            }
        }

        [TestMethod()]
        public void AddTest()
        {
            // Arrange
            string name = "name";
            string value = "value";

            using (var dict = Dict.Create(null))
            using (var displayName = MultiLocalizedUnicode.Create(null, 0))
            {
                displayName.SetWide("en", "US", "Hello");

                // Act
                bool added = dict.Add(name, value, displayName, null);

                // Assert
                Assert.IsTrue(added);
            }
        }

        [TestMethod()]
        public void EnumerateTest()
        {
            // Arrange
            using (var dict = Dict.Create(null))
            using (var mlu = MultiLocalizedUnicode.Create(null, 0))
            {
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
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            int expected = 3;

            using (var profile = Profile.CreatePlaceholder(null))
            {
                using (var dict = Dict.Create(null))
                using (var mlu = MultiLocalizedUnicode.Create(null, 0))
                {
                    mlu.SetASCII("en", "GB", "Hello");

                    dict.Add("first", null, null, null);
                    dict.Add("second", "second-value", null, null);
                    dict.Add("third", "third-value", mlu, null);

                    profile.WriteTag(TagSignature.Meta, dict.Handle);
                }

                // Act
                using (var roDict = Dict.FromHandle(profile.ReadTag(TagSignature.Meta)))
                {
                    // Assert
                    int actual = roDict.Count();
                    Assert.AreEqual(expected, actual);
                }
            }
        }
    }
}
