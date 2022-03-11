// Copyright(c) 2019-2022 John Stevenson-Hoare
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

using lcmsNET.Plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;

namespace lcmsNET.Tests.Plugin
{
    [TestClass()]
    public class PluginTest
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
        public void PluginTagTest()
        {
            // Arrange
            const TagSignature SignaturelNET = (TagSignature)0x6C4E4554;  // 'lNET'

            PluginTag tag = new PluginTag
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)Cms.EncodedCMMVersion,    // >= 2.8
                    Type = PluginType.Tag,
                    Next = IntPtr.Zero
                },
                Signature = SignaturelNET,
                Descriptor = new TagDescriptor
                {
                    ElemCount = 1,
                    nSupportedTypes = 1,
                    SupportedTypes = new TagTypeSignature[TagDescriptor.MAX_TYPES_IN_LCMS_PLUGIN],
                    Decider = null
                }
            };
            tag.Descriptor.SupportedTypes[0] = TagTypeSignature.Text;

            string expected = "PluginTagTest";

            // Act
            int rawsize = Marshal.SizeOf(tag);
            IntPtr plugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(tag, plugin, false);
            try
            {
                using (var context = Context.Create(plugin, IntPtr.Zero))
                using (var profile = Profile.CreatePlaceholder(context))
                {
                    using (var mlu = MultiLocalizedUnicode.Create(context))
                    {
                        mlu.SetASCII(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry, expected);
                        bool written = profile.WriteTag(SignaturelNET, mlu);
                        Assert.IsTrue(written);
                    }

                    using (var mlu = profile.ReadTag<MultiLocalizedUnicode>(SignaturelNET))
                    {
                        var actual = mlu.GetASCII(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry);

                        // Assert
                        Assert.AreEqual(expected, actual);
                    }
                }
            }
            finally
            {
                Marshal.DestroyStructure(plugin, typeof(PluginTag));
                Marshal.FreeHGlobal(plugin);
            }
        }

        [TestMethod()]
        public void PluginTagWithDeciderTest()
        {
            // Arrange
            const TagSignature SignaturelNET = (TagSignature)0x6C4E4554;  // 'lNET'

            PluginTag tag = new PluginTag
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)Cms.EncodedCMMVersion,    // >= 2.8
                    Type = PluginType.Tag,
                    Next = IntPtr.Zero
                },
                Signature = SignaturelNET,
                Descriptor = new TagDescriptor
                {
                    ElemCount = 1,
                    nSupportedTypes = 1,
                    SupportedTypes = new TagTypeSignature[TagDescriptor.MAX_TYPES_IN_LCMS_PLUGIN],
                    Decider = Decide
                }
            };
            tag.Descriptor.SupportedTypes[0] = TagTypeSignature.Text;

            string expected = "PluginTagWithDeciderTest";

            // Act
            int rawsize = Marshal.SizeOf(tag);
            IntPtr plugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(tag, plugin, false);
            try
            {
                using (var context = Context.Create(plugin, IntPtr.Zero))
                using (var profile = Profile.CreatePlaceholder(context))
                {
                    using (var mlu = MultiLocalizedUnicode.Create(context))
                    {
                        mlu.SetASCII(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry, expected);
                        bool written = profile.WriteTag(SignaturelNET, mlu);
                        Assert.IsTrue(written);
                    }

                    using (var mlu = profile.ReadTag<MultiLocalizedUnicode>(SignaturelNET))
                    {
                        var actual = mlu.GetASCII(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry);

                        // Assert
                        Assert.AreEqual(expected, actual);
                    }
                }
            }
            finally
            {
                Marshal.DestroyStructure(plugin, typeof(PluginTag));
                Marshal.FreeHGlobal(plugin);
            }

            TagTypeSignature Decide(double iccVersion, IntPtr data)
            {
                TestContext.WriteLine($"iccVersion: {iccVersion}, data: 0x{data:X}");

                using (var mlu = MultiLocalizedUnicode.FromHandle(data))
                {
                    var text = mlu.GetASCII(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry);
                    TestContext.WriteLine($"text: {text}");
                }

                return TagTypeSignature.Text;
            }
        }

        [TestMethod()]
        public void PluginTagTypeTest()
        {
            // Arrange
            const TagSignature SigInt = (TagSignature)0x74747448;  // 'tttH'
            const TagTypeSignature SigIntType = (TagTypeSignature)0x74747448;  // 'tttH'

            PluginTag tag = new PluginTag
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)Cms.EncodedCMMVersion,    // >= 2.8
                    Type = PluginType.Tag,
                    Next = IntPtr.Zero
                },
                Signature = SigInt,
                Descriptor = new TagDescriptor
                {
                    ElemCount = 1,
                    nSupportedTypes = 1,
                    SupportedTypes = new TagTypeSignature[TagDescriptor.MAX_TYPES_IN_LCMS_PLUGIN],
                    Decider = null
                }
            };
            tag.Descriptor.SupportedTypes[0] = SigIntType;

            int rawsize = Marshal.SizeOf(tag);
            IntPtr tagPlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(tag, tagPlugin, false);

            PluginTagType tagType = new PluginTagType
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)Cms.EncodedCMMVersion,    // >= 2.8
                    Type = PluginType.TagType,
                    Next = tagPlugin
                },
                Handler = new TagTypeHandler
                {
                    Signature = SigIntType,
                    Read = Read,
                    Write = Write,
                    Duplicate = Duplicate,
                    Free = Free
                }
            };

            rawsize = Marshal.SizeOf(tagType);
            IntPtr tagTypePlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(tagType, tagTypePlugin, false);

            // Act
            try
            {
                using (var context = Context.Create(tagTypePlugin, IntPtr.Zero))
                using (var profile = Profile.CreatePlaceholder(context))
                {
                    uint expected = 1234;
                    profile.WriteTag(SigInt, expected);

                    profile.Save(null, out uint bytesNeeded);
                    Assert.AreNotEqual(0, bytesNeeded);
                    byte[] profileMemory = new byte[bytesNeeded];

                    bool saved = profile.Save(profileMemory, out uint bytesWritten);
                    Assert.IsTrue(saved);
                    // close original profile to flush caches
                    profile.Close();

                    // re-open profile from memory
                    using (var profile2 = Profile.Open(context, profileMemory))
                    {
                        IntPtr data = profile2.ReadTag(SigInt);
                        uint[] u = new uint[1];
                        Marshal.Copy(data, (int[])(object)u, 0, 1);
                        uint actual = u[0];

                        // Assert
                        Assert.AreEqual(expected, actual);
                    }
                }
            }
            finally
            {
                Marshal.DestroyStructure(tagPlugin, typeof(PluginTag));
                Marshal.FreeHGlobal(tagPlugin);

                Marshal.DestroyStructure(tagTypePlugin, typeof(PluginTagType));
                Marshal.FreeHGlobal(tagTypePlugin);
            }

            // allocates unmanaged memory for a single 'uint' and reads from i/o handler into it
            IntPtr Read(TagTypeHandler self, IntPtr io, out uint nItems, uint tagSize)
            {
                using (var context = Context.FromHandle(self.ContextID))
                using (var iohandler = IOHandler.FromHandle(io))
                {
                    nItems = 1;
                    IntPtr ptr = Memory.Malloc(context, sizeof(uint));
                    if (ptr == IntPtr.Zero) return IntPtr.Zero;

                    // unsafe, but faster...
                    unsafe
                    {
                        if (!iohandler.Read(ref *(uint*)ptr)) return IntPtr.Zero;
                    }

                    // - or -
                    // verifiable, but slower...

                    //uint[] arr = new uint[1];
                    //if (!iohandler.Read(ref arr[0])) return IntPtr.Zero;
                    //Marshal.Copy((int[])(object)arr, 0, ptr, 1);

                    return ptr;
                }
            }

            // uses the i/o handler to write a single 'uint' read from unmanaged memory 'ptr'
            int Write(TagTypeHandler self, IntPtr io, IntPtr ptr, uint nItems)
            {
                using (var iohandler = IOHandler.FromHandle(io))
                {
                    // unsafe, but faster...
                    unsafe
                    {
                        return iohandler.Write(*(uint*)ptr) ? 1 : 0;
                    }

                    // - or -
                    // verifiable, but slower...

                    //uint[] arr = new uint[1];
                    //Marshal.Copy(ptr, (int[])(object)arr, 0, 1);
                    //return iohandler.Write(arr[0]) ? 1 : 0;
                }
            }

            // duplicates the unmanaged memory 'ptr' into a new block of size 'n x sizeof(uint)'
            IntPtr Duplicate(TagTypeHandler self, IntPtr ptr, uint n)
            {
                using (var context = Context.FromHandle(self.ContextID))
                {
                    return Memory.Duplicate(context, ptr, n * sizeof(uint));
                }
            }

            // frees the unmanaged memory 'ptr'
            void Free(TagTypeHandler self, IntPtr ptr)
            {
                using (var context = Context.FromHandle(self.ContextID))
                {
                    Memory.Free(context, ptr);
                }
            }
        }
    }
}
