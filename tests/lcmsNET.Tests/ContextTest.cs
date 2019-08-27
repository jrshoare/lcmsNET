using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ContextTest
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
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            {
                // Assert
                Assert.IsNotNull(context);
            }
        }

        [TestMethod()]
        public void DuplicateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            {
                using (var duplicate = context.Duplicate(userData))
                {
                    // Assert
                    Assert.IsNotNull(duplicate);
                    Assert.AreNotSame(duplicate, context);
                }
            }
        }

        [TestMethod()]
        public void GetUserData()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            byte[] bytes = new byte[] { 0xff, 0xaa, 0xdd, 0xee };
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            IntPtr expected = handle.AddrOfPinnedObject();
            try
            {
                // Act
                using (var context = Context.Create(plugin, expected))
                {
                    IntPtr actual = context.UserData;

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                handle.Free();
            }
        }
    }
}
