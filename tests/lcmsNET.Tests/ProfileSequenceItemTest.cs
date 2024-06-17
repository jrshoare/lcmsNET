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

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ProfileSequenceItemTest
    {
        [TestMethod()]
        public void DeviceMfg_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            uint expected = 0x6A727368;

            using var psd = ProfileSequenceDescriptor.Create(context: null, nItems: 3);

            ProfileSequenceItem sut = psd[1];
            sut.DeviceMfg = expected;

            // Act
            uint actual = sut.DeviceMfg;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DeviceModel_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            uint expected = 0x6D6F646C;

            using var psd = ProfileSequenceDescriptor.Create(context: null, nItems: 3);

            ProfileSequenceItem sut = psd[1];
            sut.DeviceModel = expected;

            // Act
            uint actual = sut.DeviceModel;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Attributes_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            DeviceAttributes expected = DeviceAttributes.Transparency | DeviceAttributes.Matte;

            using var psd = ProfileSequenceDescriptor.Create(context: null, nItems: 3);

            ProfileSequenceItem sut = psd[2];
            sut.Attributes = expected;

            // Act
            DeviceAttributes actual = sut.Attributes;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Technology_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            TechnologySignature expected = TechnologySignature.ThermalWaxPrinter;

            using var psd = ProfileSequenceDescriptor.Create(context: null, nItems: 3);

            ProfileSequenceItem sut = psd[2];
            sut.Technology = expected;

            // Act
            TechnologySignature actual = sut.Technology;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProfileID_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            using var profile = Profile.CreatePlaceholder(context: null);
            using var psd = ProfileSequenceDescriptor.Create(context: null, nItems: 3);

            ProfileSequenceItem sut = psd[2];

            profile.ComputeMD5();
            byte[] expected = profile.HeaderProfileID;
            sut.ProfileID = expected;

            // Act
            byte[] actual = sut.ProfileID;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Manufacturer_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            string expected = "manufacturer";

            using var psd = ProfileSequenceDescriptor.Create(context: null, nItems: 3);
            MultiLocalizedUnicodeUtils.DisplayName displayName = new(expected);
            using var mlu = MultiLocalizedUnicodeUtils.CreateAsASCII(displayName);

            ProfileSequenceItem sut = psd[1];
            sut.Manufacturer = mlu;

            // Act
            string actual = sut.Manufacturer.GetASCII(displayName.LanguageCode, displayName.CountryCode);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Model_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            string expected = "model";

            using var psd = ProfileSequenceDescriptor.Create(context: null, nItems: 3);
            MultiLocalizedUnicodeUtils.DisplayName displayName = new(expected);
            using var mlu = MultiLocalizedUnicodeUtils.CreateAsASCII(displayName);

            ProfileSequenceItem sut = psd[2];
            sut.Model = mlu;

            // Act
            string actual = sut.Model.GetASCII(displayName.LanguageCode, displayName.CountryCode);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Description_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            string expected = "description";

            using var psd = ProfileSequenceDescriptor.Create(context: null, nItems: 3);
            MultiLocalizedUnicodeUtils.DisplayName displayName = new(expected);
            using var mlu = MultiLocalizedUnicodeUtils.CreateAsASCII(displayName);

            ProfileSequenceItem sut = psd[0];
            sut.Description = mlu;

            // Act
            string actual = sut.Description.GetASCII(displayName.LanguageCode, displayName.CountryCode);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
