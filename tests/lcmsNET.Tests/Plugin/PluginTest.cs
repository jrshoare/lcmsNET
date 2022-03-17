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
                    Decider = IntPtr.Zero
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

            // ensure delegates are not garbage collected from managed code
            var decide = new DecideType(Decide);

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
                    Decider = Marshal.GetFunctionPointerForDelegate(decide)
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
                    Decider = IntPtr.Zero
                }
            };
            tag.Descriptor.SupportedTypes[0] = SigIntType;

            int rawsize = Marshal.SizeOf(tag);
            IntPtr tagPlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(tag, tagPlugin, false);

            // ensure delegates are not garbage collected from managed code
            var read = new TagTypeRead(Read);
            var write = new TagTypeWrite(Write);
            var duplicate = new TagTypeDuplicate(Duplicate);
            var free = new TagTypeFree(Free);

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
                    Read = Marshal.GetFunctionPointerForDelegate(read),
                    Write = Marshal.GetFunctionPointerForDelegate(write),
                    Duplicate = Marshal.GetFunctionPointerForDelegate(duplicate),
                    Free = Marshal.GetFunctionPointerForDelegate(free)
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
                    var errorHandler = new ErrorHandler(HandleError);
                    context.SetErrorHandler(errorHandler);

                    uint expected = 1234;
                    bool written = profile.WriteTag(SigInt, expected);
                    Assert.IsTrue(written);

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
            IntPtr Read(in TagTypeHandler self, IntPtr io, out uint nItems, uint tagSize)
            {
                TestContext.WriteLine($"Read(self: {self}, io: 0x{io:X}, out nItems, tagSize: {tagSize})");

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
            int Write(in TagTypeHandler self, IntPtr io, IntPtr ptr, uint nItems)
            {
                TestContext.WriteLine($"Write(self: {self}, io: 0x{io:X}, ptr: 0x{ptr:X}, nItems: {nItems})");

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
            IntPtr Duplicate(in TagTypeHandler self, IntPtr ptr, uint n)
            {
                TestContext.WriteLine($"Duplicate(self: {self}, ptr: 0x{ptr:X}, n: {n})");

                using (var context = Context.FromHandle(self.ContextID))
                {
                    return Memory.Duplicate(context, ptr, n * sizeof(uint));
                }
            }

            // frees the unmanaged memory 'ptr'
            void Free(in TagTypeHandler self, IntPtr ptr)
            {
                TestContext.WriteLine($"Free(self: {self}, ptr: 0x{ptr:X})");

                using (var context = Context.FromHandle(self.ContextID))
                {
                    Memory.Free(context, ptr);
                }
            }

            void HandleError(IntPtr contextID, int errorCode, string errorText)
            {
                TestContext.WriteLine($"Error!!! contextID: {contextID}, errorCode: {errorCode}, errorText: '{errorText}'");
            }

        }

        [TestMethod()]
        public void PluginMemoryHandlerTest()
        {
            // Arrange
            // ensure delegates are not garbage collected from managed code
            var malloc = new MemoryMalloc(Malloc);
            var free = new MemoryFree(Free);
            var realloc = new MemoryRealloc(Realloc);
            var nonContextualMalloc = new MemoryNonContextualMalloc(NonContextualMalloc);
            var nonContextualFree = new MemoryNonContextualFree(NonContextualFree);

            PluginMemoryHandler memoryHandler = new PluginMemoryHandler
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)Cms.EncodedCMMVersion,  // >= 2.8
                    Type = PluginType.MemoryHandler,
                    Next = IntPtr.Zero
                },
                Malloc = Marshal.GetFunctionPointerForDelegate(malloc),
                Free = Marshal.GetFunctionPointerForDelegate(free),
                Realloc = Marshal.GetFunctionPointerForDelegate(realloc),
                MallocZero = IntPtr.Zero,   // optional
                Calloc = IntPtr.Zero,       // optional
                Duplicate = IntPtr.Zero,    // optional
                NonContextualMalloc = Marshal.GetFunctionPointerForDelegate(nonContextualMalloc),
                NonContextualFree = Marshal.GetFunctionPointerForDelegate(nonContextualFree)
            };

            int rawsize = Marshal.SizeOf(memoryHandler);
            IntPtr memoryHandlerPlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(memoryHandler, memoryHandlerPlugin, false);

            // Act
            try
            {
                using (var context = Context.Create(memoryHandlerPlugin, IntPtr.Zero))
                {
                    // Assert
                    IntPtr mallocPtr = Memory.Malloc(context, 0x200);
                    Assert.AreNotEqual(IntPtr.Zero, mallocPtr);

                    IntPtr reallocPtr = Memory.Realloc(context, mallocPtr, 0x300);
                    Assert.AreNotEqual(IntPtr.Zero, mallocPtr);
                    Memory.Free(context, reallocPtr);
                }
            }
            finally
            {
                Marshal.DestroyStructure(memoryHandlerPlugin, typeof(PluginMemoryHandler));
                Marshal.FreeHGlobal(memoryHandlerPlugin);
            }

            // allocates memory
            IntPtr Malloc(IntPtr contextID, uint size)
            {
                TestContext.WriteLine($"Malloc(contextId: {contextID}, size: {size})");
                try
                {
                    return Marshal.AllocHGlobal((int)size);
                }
                catch (Exception)
                {
                    return IntPtr.Zero;
                }
            }

            // frees memory
            void Free(IntPtr contextID, IntPtr ptr)
            {
                TestContext.WriteLine($"Free(contextId: {contextID}, ptr: 0x{ptr:X})");
                Marshal.FreeHGlobal(ptr);
            }

            // reallocates memory
            IntPtr Realloc(IntPtr contextID, IntPtr ptr, uint newSize)
            {
                TestContext.WriteLine($"Realloc(contextId: {contextID}, ptr: 0x{ptr:X}, newSize: {newSize})");
                try
                {
                    return Marshal.ReAllocHGlobal(ptr, (IntPtr)newSize);
                }
                catch (Exception)
                {
                    return IntPtr.Zero;
                }
            }

            // allocates non-contextual memory
            IntPtr NonContextualMalloc(IntPtr userData, uint size)
            {
                TestContext.WriteLine($"NonContextualMalloc(userData: {userData}, size: {size})");
                try
                {
                    return Marshal.AllocHGlobal((int)size);
                }
                catch (Exception)
                {
                    return IntPtr.Zero;
                }
            }

            // frees non-contextual memory
            void NonContextualFree(IntPtr userData, IntPtr ptr)
            {
                TestContext.WriteLine($"NonContextualFree(userData: {userData}, ptr: 0x{ptr:X})");
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
