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

using lcmsNET.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using static lcmsNET.Tests.TestUtils.Constants;
using System.Text.RegularExpressions;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class IT8Test
    {
        [TestMethod()]
        public void Create_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = IT8.Create(context);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNonNullContext_ShouldHaveNonNullContext()
        {
            // Arrange
            using var expected = ContextUtils.CreateContext();

            // Act
            using var sut = IT8.Create(expected);
            var actual = sut.Context;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNullContext_ShouldHaveNullContext()
        {
            // Act
            using var sut = IT8.Create(context: null);
            var actual = sut.Context;

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void TableCount_WhenEmpty_ShouldHaveDefaultValueOfOne()
        {
            // Arrange
            uint expected = 1; // empty CGATS.17 object has a single table

            using var it8 = IT8.Create(context: null);

            // Act
            uint actual = it8.TableCount;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SetTable_WhenOneAddedToEmpty_ShouldHaveTableCountOfTwo()
        {
            // Arrange
            uint expectedCount = 2; // empty CGATS.17 object has a single table and we add 1
            uint nTable = 1;

            using var it8 = IT8.Create(context: null);

            // Act
            int currentTable = it8.SetTable(nTable);

            // Assert
            Assert.AreEqual(nTable, Convert.ToUInt32(currentTable));
            uint actualCount = it8.TableCount;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod()]
        public void SetTable_WhenEmptyAddingTwoAtOnce_ShouldReturnError()
        {
            // Arrange
            uint nTable = 2; // empty CGATS.17 object has a single table so index two greater (zero-based)
            int expected = Constants.IT8.SetTableError;

            using var it8 = IT8.Create(null);

            // Act
            int actual = it8.SetTable(nTable);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Open_WhenFromFile_ShouldHaveValidHandle()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                using var context = ContextUtils.CreateContext();

                // Act
                using var sut = IT8.Open(context, it8Path);

                // Assert
                Assert.IsFalse(sut.IsInvalid);
            });
        }

        [TestMethod()]
        public void Open_WhenFromFileWithNonNullContext_ShouldHaveNonNullContext()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                using var expected = ContextUtils.CreateContext();

                // Act
                using var sut = IT8.Open(expected, it8Path);
                var actual = sut.Context;

                // Assert
                Assert.AreSame(expected, actual);
            });
        }

        [TestMethod()]
        public void Open_WhenFromFileWithNullContext_ShouldHaveNullContext()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                // Act
                using var sut = IT8.Open(context: null, it8Path);
                var actual = sut.Context;

                // Assert
                Assert.IsNull(actual);
            });
        }

        [TestMethod()]
        public void Open_WhenFromMemory_ShouldHaveValidHandle()
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.IT8.txt");

            // Act
            using var sut = IT8.Open(context: null, ms.GetBuffer());

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Open_WhenFromMemoryWithNonNullContext_ShouldHaveNonNullContext()
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.IT8.txt");
            using var expected = ContextUtils.CreateContext();

            // Act
            using var sut = IT8.Open(expected, ms.GetBuffer());
            var actual = sut.Context;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod()]
        public void Open_WhenFromMemoryWithNullContext_ShouldHaveNullContext()
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.IT8.txt");

            // Act
            using var sut = IT8.Open(context: null, ms.GetBuffer());
            var actual = sut.Context;

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void Save_WhenToFile_ShouldSucceed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                using var sut = IT8.Open(context: null, it8Path);
                var tempPath = Path.GetDirectoryName(it8Path);
                var savePath = Path.Combine(tempPath, "saved.txt");

                // Act
                bool result = sut.Save(savePath);

                // Assert
                Assert.IsTrue(result);
                Assert.IsTrue(File.Exists(savePath));
            });
        }

        [TestMethod()]
        public void Save_WhenToMemory_ShouldSucceed()
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.IT8.txt");
            byte[] memory = ms.GetBuffer();

            using var sut = IT8.Open(null, memory);
            var expected = IT8Utils.BytesNeededToSaveToMemory(sut);
            byte[] mem = new byte[expected];

            // Act
            var result = sut.Save(mem, out uint actual);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Save_WhenCalculatingMemorySizeNeeded_ShouldReturnNonZero()
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.IT8.txt");
            byte[] memory = ms.GetBuffer();

            using var sut = IT8.Open(null, memory);
            uint notExpected = 0;

            // Act
            var actual = IT8Utils.BytesNeededToSaveToMemory(sut);

            // Assert
            Assert.AreNotEqual(notExpected, actual);
        }

        [TestMethod()]
        public void SheetType_WhenGetting_ShouldReturnExpectedValue()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                string expected = "ISO28178";   // from Resources/IT8.txt
                using var it8 = IT8.Open(context: null, it8Path);

                // Act
                var actual = it8.SheetType;

                // Assert
                Assert.AreEqual(expected, actual);
            });
        }

        [TestMethod()]
        public void SheetType_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            using var sut = IT8.Create(context: null);

            var expected = "sheet_type";

            // Act
            sut.SheetType = expected;
            string actual = sut.SheetType;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddComment_WhenValid_ShouldSucceed()
        {
            // Arrange
            using var sut = IT8.Create(context: null);

            // Act
            bool added = sut.AddComment("comment");

            // Assert
            Assert.IsTrue(added);
        }

        [TestMethod()]
        public void SetProperty_WhenString_ShouldSucceed()
        {
            // Arrange
            using var sut = IT8.Create(context: null);

            // Act
            bool result = sut.SetProperty(name: "name", value: "value");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SetProperty_WhenDouble_ShouldSucceed()
        {
            // Arrange
            using var sut = IT8.Create(context: null);

            // Act
            bool result = sut.SetProperty(name: "name", value: 12.345);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SetProperty_WhenHexConstant_ShouldSucceed()
        {
            // Arrange
            using var sut = IT8.Create(context: null);

            // Act
            bool result = sut.SetProperty(name: "name", value: 0x12345);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SetUncooked_WhenValid_ShouldSucceed()
        {
            // Arrange
            using var sut = IT8.Create(context: null);

            // Act
            bool result = sut.SetUncookedProperty(name: "name", value: "uncooked");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SetProperty_WhenSubProperty_ShouldSucceed()
        {
            // Arrange
            using var sut = IT8.Create(context: null);

            // Act
            bool result = sut.SetProperty(key: "key", subkey: "subkey", value: "value");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GetProperty_WhenString_ShouldReturnValueSet()
        {
            // Arrange
            string name = "name";
            string expected = "value";

            using var sut = IT8.Create(context: null);
            sut.SetProperty(name, expected);

            // Act
            string actual = sut.GetProperty(name);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetProperty_WhenDouble_ShouldReturnValueSet()
        {
            // Arrange
            string name = "name";
            double expected = 12.345;

            using var sut = IT8.Create(context: null);
            bool isSet = sut.SetProperty(name, expected);

            // Act
            double actual = sut.GetDoubleProperty(name);

            // Assert
            Assert.AreEqual(expected, actual, double.Epsilon);
        }

        [TestMethod()]
        public void Properties_WhenValidForSavedResource_ShouldHaveNonZeroCount()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                using var sut = IT8.Open(context: null, it8Path);

                // Act
                var actual = sut.Properties;

                // Assert
                Assert.AreNotEqual(0, actual.Count());
            });
        }

        [TestMethod()]
        public void GetProperties_WhenValid_ShouldReturnEnumerableOfSubkeys()
        {
            // Arrange
            string key = "key";
            string expected = "subkey";

            using var it8 = IT8.Create(null);
            bool isSet = it8.SetProperty(key, expected, value: "value");

            // Act
            var actual = it8.GetProperties(key)?.FirstOrDefault();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetData_WhenRowColumnAsString_ShouldReturnExpectedValue()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                using var sut = IT8.Open(context: null, it8Path);
                string expected = "PicricAcid_5L";  // from Resources/IT8.txt data row 10 column 1 (zero-based)

                // Act
                string actual = sut.GetData(row: 10, column: 1);

                // Assert
                Assert.AreEqual(expected, actual);
            });
        }

        [TestMethod()]
        public void GetData_WhenPatchSampleAsString_ShouldReturnExpectedValue()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                using var sut = IT8.Open(context: null, it8Path);
                string expected = "NeutralRed_M";  // from Resources/IT8.txt data with 'PatchName' = 'A17'

                // Act
                string actual = sut.GetData(patch: "A17", sample: "PatchName");

                // Assert
                Assert.AreEqual(expected, actual);
            });
        }

        [TestMethod()]
        public void GetDoubleData_WhenRowColumn_ShouldReturnExpectedValue()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                using var sut = IT8.Open(context: null, it8Path);
                double expected = 15.2; // from Resources/IT8.txt data row 16, column 2 (zero-based)

                // Act
                double actual = sut.GetDoubleData(row: 16, column: 2);

                // Assert
                Assert.AreEqual(expected, actual);
            });
        }

        [TestMethod()]
        public void GetDoubleData_WhenPatchSample_ShouldReturnExpectedValue()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                using var sut = IT8.Open(context: null, it8Path);
                double expected = 15.2;  // from Resources/IT8.txt data with 'PatchName' = 'A17'

                // Act
                double actual = sut.GetDoubleData(patch: "A17", sample: "PatchX");

                // Assert
                Assert.AreEqual(expected, actual);
            });
        }

        [TestMethod()]
        public void SetData_WhenForRowColumnAsString_ShouldSucceed()
        {
            // Arrange
            using var sut = IT8.Create(context: null);

            sut.SetProperty(Constants.IT8.NumberOfFields, 3);   // => data columns
            sut.SetProperty(Constants.IT8.NumberOfSets, 4); // => data rows

            // Act
            bool result = sut.SetData(row: 3, column: 2, value: "value");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SetData_WhenForPatchSample_ShouldSucceed()
        {
            // Arrange
            string[] samples = ["SAMPLE_ID", "PatchName", "PatchX", "PatchY"];
            string[] strValues = ["A2", "HL"];
            double[] dblValues = [3.1, 2.7];
            string patch = strValues[0];

            using var sut = IT8.Create(context: null);
            sut.SetProperty(Constants.IT8.NumberOfFields, samples.Length);
            sut.SetProperty(Constants.IT8.NumberOfSets, 1);

            int n = 0;
            foreach (var sampleName in samples)
            {
                sut.SetDataFormat(n++, sampleName);
            }

            // Act
            bool result = sut.SetData(patch, samples[0], strValues[0]);
            result = result && sut.SetData(patch, samples[1], strValues[1]);
            result = result && sut.SetData(patch, samples[2], dblValues[0]);
            result = result && sut.SetData(patch, samples[3], dblValues[1]);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SetData_WhenForRowColumnAsDouble_ShouldSucceed()
        {
            // Arrange
            using var sut = IT8.Create(context: null);
            sut.SetProperty(Constants.IT8.NumberOfFields, 3);
            sut.SetProperty(Constants.IT8.NumberOfSets, 4);

            // Act
            bool result = sut.SetData(row: 0, column: 1, value: 17.43);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void FindDataFormat_WhenValidForSavedResource_ShouldReturnExpectedValue()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                using var sut = IT8.Open(null, it8Path);
                int expected = 8;   // from Resources/IT8.txt line 10

                // Act
                int actual = sut.FindDataFormat(sample: "SPECTRAL_400");

                // Assert
                Assert.AreEqual(expected, actual);
            });
        }

        [TestMethod()]
        public void SetDataFormat_WhenValid_ShouldSucceed()
        {
            // Arrange
            using var sut = IT8.Create(context: null);
            sut.SetProperty(Constants.IT8.NumberOfFields, 12);

            // Act
            bool result = sut.SetDataFormat(column: 6, sample: "COLUMN_6");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SampleNames_WhenValidForSavedResource_ShouldReturnNonZeroCount()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                using var sut = IT8.Open(context: null, it8Path);

                // Act
                var actual = sut.SampleNames?.Count();

                // Assert
                Assert.AreNotEqual(0, actual);
            });
        }

        [TestMethod()]
        public void GetPatchName_WhenValidForSavedResource_ShouldReturnExpectedValue()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.IT8.txt", "it8.txt", (it8Path) =>
            {
                using var sut = IT8.Open(context: null, it8Path);
                string expected = "A11";    // from Resources/IT8.txt data row 10 (zero-based)

                // Act
                string actual = sut.GetPatchName(nPatch: 10);

                // Assert
                Assert.AreEqual(expected, actual);
            });
        }

        [TestMethod]
        public void DoubleFormat_WhenSetValid_ShouldSucceed()
        {
            // Arrange
            using var it8 = IT8.Create(null);

            // Act
            it8.DoubleFormat = "%.8g";
        }
    }
}
