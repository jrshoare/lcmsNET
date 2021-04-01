using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class IT8Test
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

        /// <summary>
        /// Extracts the named resource and saves to the specified file path.
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="path"></param>
        private void Save(string resourceName, string path)
        {
            var thisExe = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(thisExe.FullName);
            using (var s = thisExe.GetManifestResourceStream(assemblyName.Name + resourceName))
            {
                using (var fs = File.Create(path))
                {
                    s.CopyTo(fs);
                }
            }
        }

        private MemoryStream Save(string resourceName)
        {
            MemoryStream ms = new MemoryStream();
            var thisExe = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(thisExe.FullName);
            using (var s = thisExe.GetManifestResourceStream(assemblyName.Name + resourceName))
            {
                s.CopyTo(ms);
            }
            return ms;
        }

        const string NUMBER_OF_FIELDS = "NUMBER_OF_FIELDS";
        const string NUMBER_OF_SETS = "NUMBER_OF_SETS";

        [TestMethod()]
        public void CreateTest()
        {
            // Arrange

            // Act
            using (var it8 = IT8.Create(null))
            {
                // Assert
                Assert.IsNotNull(it8);
            }
        }

        [TestMethod()]
        public void TableCountTest()
        {
            // Arrange
            uint expected = 1; // empty CGATS.17 object has a single table

            using (var it8 = IT8.Create(null))
            {
                // Act
                uint actual = it8.TableCount;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void SetTableTest()
        {
            // Arrange
            uint expectedCount = 2; // empty CGATS.17 object has a single table and we add 1
            uint nTable = 1;

            using (var it8 = IT8.Create(null))
            {
                // Act
                int currentTable = it8.SetTable(nTable);

                // Assert
                Assert.AreEqual(nTable, Convert.ToUInt32(currentTable));
                uint actualCount = it8.TableCount;
                Assert.AreEqual(expectedCount, actualCount);
            }
        }

        [TestMethod()]
        public void SetTableOutOfRangeTest()
        {
            // Arrange
            uint nTable = 2; // empty CGATS.17 object has a single table so index two greater (zero-based)
            int expected = -1; // error returns -1

            using (var it8 = IT8.Create(null))
            {
                // Act
                int actual = it8.SetTable(nTable);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void OpenTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            try
            {
                var it8path = Path.Combine(tempPath, "it8.txt");
                Save(".Resources.IT8.txt", it8path);

                // Act
                using (var context = Context.Create(plugin, userData))
                using (var it8 = IT8.Open(context, it8path))
                {
                    // Assert
                    Assert.IsNotNull(it8);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void OpenTest2()
        {
            // Arrange
            using (MemoryStream ms = Save(".Resources.IT8.txt"))
            {
                // Act
                using (var it8 = IT8.Open(null, ms.GetBuffer()))
                {
                    // Assert
                    Assert.IsNotNull(it8);
                }
            }
        }

        [TestMethod()]
        public void SaveTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var it8path = Path.Combine(tempPath, "it8.txt");
                Save(".Resources.IT8.txt", it8path);

                // Act
                using (var it8 = IT8.Open(null, it8path))
                {
                    var savepath = Path.Combine(tempPath, "saved.txt");
                    bool saved = it8.Save(savepath);

                    // Assert
                    Assert.IsTrue(saved);
                    Assert.IsTrue(File.Exists(savepath));
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void SaveTest2()
        {
            // Arrange
            using (MemoryStream ms = Save(".Resources.IT8.txt"))
            {
                byte[] memory = ms.GetBuffer();
                using (var it8 = IT8.Open(null, memory))
                {
                    it8.Save(null, out int expected);
                    byte[] mem = new byte[expected];

                    // Act
                    var result = it8.Save(mem, out int actual);

                    // Assert
                    Assert.IsTrue(result);
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod()]
        public void SaveTest3()
        {
            // Arrange
            using (MemoryStream ms = Save(".Resources.IT8.txt"))
            {
                byte[] memory = ms.GetBuffer();
                using (var it8 = IT8.Open(null, memory))
                {
                    int notExpected = 0;

                    // Act
                    var result = it8.Save(null, out int actual);

                    // Assert
                    Assert.IsTrue(result);
                    Assert.AreNotEqual(notExpected, actual);
                }
            }
        }

        [TestMethod()]
        public void ContextTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            try
            {
                var it8path = Path.Combine(tempPath, "it8.txt");
                Save(".Resources.IT8.txt", it8path);

                // Act
                using (var expected = Context.Create(plugin, userData))
                using (var it8 = IT8.Open(expected, it8path))
                {
                    var actual = it8.Context;

                    // Assert
                    Assert.AreSame(expected, actual);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void SheetTypeGetTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            string expected = "ISO28178";

            try
            {
                var it8path = Path.Combine(tempPath, "it8.txt");
                Save(".Resources.IT8.txt", it8path);

                // Act
                using (var it8 = IT8.Open(null, it8path))
                {
                    var actual = it8.SheetType;

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void SheetTypeSetTest()
        {
            // Arrange
            var expected = "SHEET_TYPE";

            using (var it8 = IT8.Create(null))
            {
                // Act
                it8.SheetType = expected;
                string actual = it8.SheetType;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void AddCommentTest()
        {
            // Arrange
            var comment = "Comment";

            using (var it8 = IT8.Create(null))
            {
                // Act
                bool added = it8.AddComment(comment);

                // Assert
                Assert.IsTrue(added);
            }
        }

        [TestMethod()]
        public void SetPropertyTest()
        {
            // Arrange
            string name = "Name";
            string value = "Value";

            using (var it8 = IT8.Create(null))
            {
                // Act
                bool isSet = it8.SetProperty(name, value);

                // Assert
                Assert.IsTrue(isSet);
            }
        }

        [TestMethod()]
        public void SetPropertyDoubleTest()
        {
            // Arrange
            string name = "Name";
            double value = 12.345;

            using (var it8 = IT8.Create(null))
            {
                // Act
                bool isSet = it8.SetProperty(name, value);

                // Assert
                Assert.IsTrue(isSet);
            }
        }

        [TestMethod()]
        public void SetPropertyHexTest()
        {
            // Arrange
            string name = "Name";
            uint value = 0x12345;

            using (var it8 = IT8.Create(null))
            {
                // Act
                bool isSet = it8.SetProperty(name, value);

                // Assert
                Assert.IsTrue(isSet);
            }
        }

        [TestMethod()]
        public void SetUncookedPropertyTest()
        {
            // Arrange
            string name = "Name";
            string value = "Uncooked";

            using (var it8 = IT8.Create(null))
            {
                // Act
                bool isSet = it8.SetUncookedProperty(name, value);

                // Assert
                Assert.IsTrue(isSet);
            }
        }

        [TestMethod()]
        public void SetMultiPropertyTest()
        {
            // Arrange
            string key = "Key";
            string subkey = "Subkey";
            string value = "Value";

            using (var it8 = IT8.Create(null))
            {
                // Act
                bool isSet = it8.SetProperty(key, subkey, value);

                // Assert
                Assert.IsTrue(isSet);
            }
        }

        [TestMethod()]
        public void GetPropertyTest()
        {
            // Arrange
            string name = "Name";
            string expected = "Value";

            using (var it8 = IT8.Create(null))
            {
                bool isSet = it8.SetProperty(name, expected);

                // Act
                string actual = it8.GetProperty(name);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void GetPropertyDoubleTest()
        {
            // Arrange
            string name = "Name";
            double expected = 12.345;

            using (var it8 = IT8.Create(null))
            {
                bool isSet = it8.SetProperty(name, expected);

                // Act
                double actual = it8.GetDoubleProperty(name);

                // Assert
                Assert.AreEqual(expected, actual, double.Epsilon);
            }
        }

        [TestMethod()]
        public void PropertiesTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var it8path = Path.Combine(tempPath, "it8.txt");
                Save(".Resources.IT8.txt", it8path);

                using (var it8 = IT8.Open(null, it8path))
                {
                    // Act
                    var actual = it8.Properties;

                    // Assert
                    Assert.IsNotNull(actual);
                    Assert.AreNotEqual(0, actual.Count());
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void GetPropertiesTest()
        {
            // Arrange
            string key = "Key";
            string subkey = "Subkey";
            string value = "Value";

            using (var it8 = IT8.Create(null))
            {
                bool isSet = it8.SetProperty(key, subkey, value);

                // Act
                var actual = it8.GetProperties(key);

                // Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(subkey, actual.First());
            }
        }

        [TestMethod()]
        public void GetDataRowColTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var it8path = Path.Combine(tempPath, "it8.txt");
                Save(".Resources.IT8.txt", it8path);

                using (var it8 = IT8.Open(null, it8path))
                {
                    int row = 10, column = 1;
                    string expected = "PicricAcid_5L";

                    // Act
                    string actual = it8.GetData(row, column);

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void GetDataTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var it8path = Path.Combine(tempPath, "it8.txt");
                Save(".Resources.IT8.txt", it8path);

                using (var it8 = IT8.Open(null, it8path))
                {
                    string patch = "A17", sample = "PatchName";
                    string expected = "NeutralRed_M";

                    // Act
                    string actual = it8.GetData(patch, sample);

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void GetDoubleDataRowColTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var it8path = Path.Combine(tempPath, "it8.txt");
                Save(".Resources.IT8.txt", it8path);

                using (var it8 = IT8.Open(null, it8path))
                {
                    int row = 16, column = 2;
                    double expected = 15.2;

                    // Act
                    double actual = it8.GetDoubleData(row, column);

                    // Assert
                    Assert.AreEqual(expected, actual, double.Epsilon);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void GetDataDoubleTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var it8path = Path.Combine(tempPath, "it8.txt");
                Save(".Resources.IT8.txt", it8path);

                using (var it8 = IT8.Open(null, it8path))
                {
                    string patch = "A17", sample = "PatchX";
                    double expected = 15.2;

                    // Act
                    double actual = it8.GetDoubleData(patch, sample);

                    // Assert
                    Assert.AreEqual(expected, actual, double.Epsilon);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void SetDataTest()
        {
            // Arrange
            int row = 3, column = 2;
            string value = "Value";

            using (var it8 = IT8.Create(null))
            {
                it8.SetProperty(NUMBER_OF_FIELDS, 3);
                it8.SetProperty(NUMBER_OF_SETS, 4);

                // Act
                bool isSet = it8.SetData(row, column, value);

                // Assert
                Assert.IsTrue(isSet);
            }
        }

        [TestMethod()]
        public void SetDataTest2()
        {
            // Arrange
            string[] sampleNames = new string[] { "SAMPLE_ID", "PatchName", "PatchX", "PatchY" };
            string[] strValues = new string[] { "A2", "HL" };
            double[] dblValues = new double[] { 3.1, 2.7 };
            string patch = strValues[0];

            using (var it8 = IT8.Create(null))
            {
                it8.SetProperty(NUMBER_OF_FIELDS, sampleNames.Length);
                it8.SetProperty(NUMBER_OF_SETS, 1);

                int n = 0;
                foreach (var sampleName in sampleNames)
                {
                    it8.SetDataFormat(n++, sampleName);
                }

                // Act
                bool isSet = it8.SetData(patch, sampleNames[0], strValues[0]);
                isSet = isSet && it8.SetData(patch, sampleNames[1], strValues[1]);
                isSet = isSet && it8.SetData(patch, sampleNames[2], dblValues[0]);
                isSet = isSet && it8.SetData(patch, sampleNames[3], dblValues[1]);

                // Assert
                Assert.IsTrue(isSet);
            }
        }

        [TestMethod()]
        public void SetDataDoubleTest()
        {
            // Arrange
            int row = 0, column = 1;
            double value = 17.43;

            using (var it8 = IT8.Create(null))
            {
                it8.SetProperty(NUMBER_OF_FIELDS, 3);
                it8.SetProperty(NUMBER_OF_SETS, 4);

                // Act
                bool isSet = it8.SetData(row, column, value);

                // Assert
                Assert.IsTrue(isSet);
            }
        }

        [TestMethod()]
        public void FindDataFormatTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var it8path = Path.Combine(tempPath, "it8.txt");
                Save(".Resources.IT8.txt", it8path);

                using (var it8 = IT8.Open(null, it8path))
                {
                    string sample = "SPECTRAL_400";
                    int expected = 8;

                    // Act
                    int actual = it8.FindDataFormat(sample);

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void SetDataFormatTest()
        {
            // Arrange
            int column = 6;
            string sample = "COLUMN_6";

            using (var it8 = IT8.Create(null))
            {
                it8.SetProperty(NUMBER_OF_FIELDS, 12);

                // Act
                bool isSet = it8.SetDataFormat(column, sample);

                // Assert
                Assert.IsTrue(isSet);
            }
        }

        [TestMethod()]
        public void SampleNamesTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var it8path = Path.Combine(tempPath, "it8.txt");
                Save(".Resources.IT8.txt", it8path);

                using (var it8 = IT8.Open(null, it8path))
                {
                    // Act
                    var actual = it8.SampleNames;

                    // Assert
                    Assert.IsNotNull(actual);
                    Assert.AreNotEqual(0, actual.Count());
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void GetPatchNameTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var it8path = Path.Combine(tempPath, "it8.txt");
                Save(".Resources.IT8.txt", it8path);

                using (var it8 = IT8.Open(null, it8path))
                {
                    int nPatch = 10;
                    string expected = "A11";

                    // Act
                    string actual = it8.GetPatchName(nPatch);

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod]
        public void DoubleFormatTest()
        {
            // Arrange
            using (var it8 = IT8.Create(null))
            {
                // Act
                it8.DoubleFormat = "%.8g";

                // Assert
            }
        }
    }
}
