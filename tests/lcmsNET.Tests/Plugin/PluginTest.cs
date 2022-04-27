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
using System.Text;

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

        [TestMethod()]
        public void PluginInterpolationTest1D()
        {
            // Arrange
            // ensure delegates are not garbage collected from managed code
            var fake1Dfloat = new InterpFnFloat(Fake1DFloat);
            IntPtr fake1DfloatPtr = Marshal.GetFunctionPointerForDelegate(fake1Dfloat);
            var fake3D16 = new InterpFn16(Fake3D16);
            IntPtr fake3D16Ptr = Marshal.GetFunctionPointerForDelegate(fake3D16);
            var factory = new InterpolatorsFactory(Factory);

            PluginInterpolation interpolation = new PluginInterpolation
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)Cms.EncodedCMMVersion,  // >= 2.8
                    Type = PluginType.Interpolation,
                    Next = IntPtr.Zero
                },
                Factory = Marshal.GetFunctionPointerForDelegate(factory)
            };

            int rawsize = Marshal.SizeOf(interpolation);
            IntPtr interpolationPlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(interpolation, interpolationPlugin, false);

            // Act
            try
            {
                using (var context = Context.Create(interpolationPlugin, IntPtr.Zero))
                {
                    // a straight line
                    float[] tabulated = new float[]
                    {
                        0.0f, 0.10f, 0.20f, 0.30f, 0.40f, 0.50f, 0.60f, 0.70f, 0.80f, 0.90f, 1.00f
                    };

                    using (var toneCurve = ToneCurve.BuildTabulated(context, tabulated))
                    {
                        // Assert
                        // do some interpolations with the plug-in
                        var actual = toneCurve.Evaluate(0.1f);
                        Assert.AreEqual(0.10f, actual, float.Epsilon);
                        actual = toneCurve.Evaluate(0.13f);
                        Assert.AreEqual(0.10f, actual, float.Epsilon);
                        actual = toneCurve.Evaluate(0.55f);
                        Assert.AreEqual(0.50f, actual, float.Epsilon);
                        actual = toneCurve.Evaluate(0.9999f);
                        Assert.AreEqual(0.90f, actual, float.Epsilon);
                    }
                }
            }
            finally
            {
                Marshal.DestroyStructure(interpolationPlugin, typeof(PluginInterpolation));
                Marshal.FreeHGlobal(interpolationPlugin);
            }

            // this fake interpolation always takes the closest lower node in the interpolation table for 1D
            void Fake1DFloat(IntPtr input, IntPtr output, IntPtr p)
            {
                int encodedVersion = 2070;
                // next call throws if less than 2.8
                try { encodedVersion = Cms.EncodedCMMVersion; } catch {}

                unsafe
                {
                    float* LutTable = null;
                    uint[] Domain = null;

                    if (encodedVersion >= 2120)
                    {
                        var interpParams = Marshal.PtrToStructure<InterpolationParamsV2>(p);
                        LutTable = (float*)interpParams.Table.ToPointer();
                        Domain = interpParams.Domain;
                    }
                    else
                    {
                        var interpParams = Marshal.PtrToStructure<InterpolationParamsV1>(p);
                        LutTable = (float*)interpParams.Table.ToPointer();
                        Domain = interpParams.Domain;
                    }


                    float* pInput = (float*)input.ToPointer();
                    float* pOutput = (float*)output.ToPointer();

                    if (pInput[0] >= 1.0)
                    {
                        pOutput[0] = LutTable[Domain[0]];
                        return;
                    }

                    float val = Domain[0] * pInput[0];
                    int cell = (int)Math.Floor(val);
                    pOutput[0] = LutTable[cell];
                }
            }

            // this fake interpolation just uses scrambled negated indexes for output
            void Fake3D16(IntPtr input, IntPtr output, IntPtr p)
            {
                const ushort _ = 0xFFFF;
                unsafe
                {
                    ushort* pInput = (ushort*)input.ToPointer();
                    ushort* pOutput = (ushort*)output.ToPointer();

                    pOutput[0] = (ushort)(_ - pInput[2]);
                    pOutput[1] = (ushort)(_ - pInput[1]);
                    pOutput[2] = (ushort)(_ - pInput[0]);
                }
            }

            InterpolationFunction Factory(uint nInputChannels, uint nOutputChannels, LerpFlags flags)
            {
                InterpolationFunction interpFn = new InterpolationFunction
                {
                    Interpolator = IntPtr.Zero
                };

                bool isFloat = (flags & LerpFlags.FloatingPoint) != 0;

                if (nInputChannels == 1 && nOutputChannels == 1 && isFloat)
                {
                    interpFn.Interpolator = fake1DfloatPtr;
                }
                else if (nInputChannels == 3 && nOutputChannels == 3 && !isFloat)
                {
                    interpFn.Interpolator = fake3D16Ptr;
                }

                return interpFn;
            }
        }

        [TestMethod()]
        public void PluginInterpolationTest3D()
        {
            // Arrange
            // ensure delegates are not garbage collected from managed code
            var fake1Dfloat = new InterpFnFloat(Fake1DFloat);
            IntPtr fake1DfloatPtr = Marshal.GetFunctionPointerForDelegate(fake1Dfloat);
            var fake3D16 = new InterpFn16(Fake3D16);
            IntPtr fake3D16Ptr = Marshal.GetFunctionPointerForDelegate(fake3D16);
            var factory = new InterpolatorsFactory(Factory);

            PluginInterpolation interpolation = new PluginInterpolation
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)Cms.EncodedCMMVersion,  // >= 2.8
                    Type = PluginType.Interpolation,
                    Next = IntPtr.Zero
                },
                Factory = Marshal.GetFunctionPointerForDelegate(factory)
            };

            int rawsize = Marshal.SizeOf(interpolation);
            IntPtr interpolationPlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(interpolation, interpolationPlugin, false);

            // Act
            try
            {
                using (var context = Context.Create(interpolationPlugin, IntPtr.Zero))
                using (var pipeline = Pipeline.Create(context, 3, 3))
                {
                    ushort[] identity =
                    {
                       0,       0,       0,
                       0,       0,       0xffff,
                       0,       0xffff,  0,
                       0,       0xffff,  0xffff,
                       0xffff,  0,       0,
                       0xffff,  0,       0xffff,
                       0xffff,  0xffff,  0,
                       0xffff,  0xffff,  0xffff
                    };

                    using (var clut = Stage.Create(context, 2, 3, 3, identity))
                    {
                        pipeline.Insert(clut, StageLoc.At_Begin);

                        // do some interpolations with the plugin
                        ushort[] input = new ushort[] { 0, 0, 0 };
                        ushort[] output = pipeline.Evaluate(input);

                        // Assert
                        Assert.AreEqual((ushort)(0xFFFF - 0), output[0]);
                        Assert.AreEqual((ushort)(0xFFFF - 0), output[1]);
                        Assert.AreEqual((ushort)(0xFFFF - 0), output[2]);

                        input[0] = 0x1234; input[1] = 0x5678; input[2] = 0x9ABC;
                        output = pipeline.Evaluate(input);

                        Assert.AreEqual((ushort)(0xFFFF - 0x9ABC), output[0]);
                        Assert.AreEqual((ushort)(0xFFFF - 0x5678), output[1]);
                        Assert.AreEqual((ushort)(0xFFFF - 0x1234), output[2]);
                    }
                }
            }
            finally
            {
                Marshal.DestroyStructure(interpolationPlugin, typeof(PluginInterpolation));
                Marshal.FreeHGlobal(interpolationPlugin);
            }

            // this fake interpolation always takes the closest lower node in the interpolation table for 1D
            void Fake1DFloat(IntPtr input, IntPtr output, IntPtr p)
            {
                int encodedVersion = 2070;
                // next call throws if less than 2.8
                try { encodedVersion = Cms.EncodedCMMVersion; } catch { }

                unsafe
                {
                    float* LutTable = null;
                    uint[] Domain = null;

                    if (encodedVersion >= 2120)
                    {
                        var interpParams = Marshal.PtrToStructure<InterpolationParamsV2>(p);
                        LutTable = (float*)interpParams.Table.ToPointer();
                        Domain = interpParams.Domain;
                    }
                    else
                    {
                        var interpParams = Marshal.PtrToStructure<InterpolationParamsV1>(p);
                        LutTable = (float*)interpParams.Table.ToPointer();
                        Domain = interpParams.Domain;
                    }

                    float *pInput = (float *)input.ToPointer();
                    float *pOutput = (float *)output.ToPointer();

                    if (pInput[0] >= 1.0)
                    {
                        pOutput[0] = LutTable[Domain[0]];
                        return;
                    }

                    float val = Domain[0] * pInput[0];
                    int cell = (int)Math.Floor(val);
                    pOutput[0] = LutTable[cell];
                }
            }

            // this fake interpolation just uses scrambled negated indexes for output
            void Fake3D16(IntPtr input, IntPtr output, IntPtr p)
            {
                const ushort _ = 0xFFFF;
                unsafe
                {
                    ushort* pInput = (ushort*)input.ToPointer();
                    ushort* pOutput = (ushort*)output.ToPointer();

                    pOutput[0] = (ushort)(_ - pInput[2]);
                    pOutput[1] = (ushort)(_ - pInput[1]);
                    pOutput[2] = (ushort)(_ - pInput[0]);
                }
            }

            InterpolationFunction Factory(uint nInputChannels, uint nOutputChannels, LerpFlags flags)
            {
                InterpolationFunction interpFn = new InterpolationFunction
                {
                    Interpolator = IntPtr.Zero
                };

                bool isFloat = (flags & LerpFlags.FloatingPoint) != 0;

                if (nInputChannels == 1 && nOutputChannels == 1 && isFloat)
                {
                    interpFn.Interpolator = fake1DfloatPtr;
                }
                else if (nInputChannels == 3 && nOutputChannels == 3 && !isFloat)
                {
                    interpFn.Interpolator = fake3D16Ptr;
                }

                return interpFn;
            }
        }

        [TestMethod]
        public void PluginParametricCurvesTest()
        {
            // Arrange
            const int TYPE_SIN = 1000;
            const int TYPE_COS = 1010;
            const int TYPE_TAN = 1020;
            const int TYPE_709 = 709;

            var rec709Math = new ParametricCurveEvaluator(Rec709Math);
            PluginParametricCurves rec709 = new PluginParametricCurves
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)2060,
                    Type = PluginType.ParametricCurve,
                    Next = IntPtr.Zero
                },
                nFunctions = 1,
                FunctionTypes = new uint[PluginParametricCurves.MAX_TYPES_IN_LCMS_PLUGIN],
                ParameterCount = new uint[PluginParametricCurves.MAX_TYPES_IN_LCMS_PLUGIN],
                Evaluator = Marshal.GetFunctionPointerForDelegate(rec709Math)
            };
            rec709.FunctionTypes[0] = TYPE_709;
            rec709.ParameterCount[0] = 5;

            int rawsize = Marshal.SizeOf(rec709);
            IntPtr rec709Plugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(rec709Plugin, rec709Plugin, false);

            var myFns = new ParametricCurveEvaluator(MyFns);
            PluginParametricCurves curveSample = new PluginParametricCurves
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)2060,
                    Type = PluginType.ParametricCurve,
                    Next = IntPtr.Zero
                },
                nFunctions = 2,
                FunctionTypes = new uint[PluginParametricCurves.MAX_TYPES_IN_LCMS_PLUGIN],
                ParameterCount = new uint[PluginParametricCurves.MAX_TYPES_IN_LCMS_PLUGIN],
                Evaluator = Marshal.GetFunctionPointerForDelegate(myFns)
            };
            curveSample.FunctionTypes[0] = TYPE_SIN; curveSample.FunctionTypes[1] = TYPE_COS;
            curveSample.ParameterCount[0] = 1; curveSample.ParameterCount[1] = 1;

            rawsize = Marshal.SizeOf(curveSample);
            IntPtr curveSamplePlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(curveSample, curveSamplePlugin, false);

            var myFns2 = new ParametricCurveEvaluator(MyFns2);
            PluginParametricCurves curveSample2 = new PluginParametricCurves
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)2060,
                    Type = PluginType.ParametricCurve,
                    Next = IntPtr.Zero
                },
                nFunctions = 1,
                FunctionTypes = new uint[PluginParametricCurves.MAX_TYPES_IN_LCMS_PLUGIN],
                ParameterCount = new uint[PluginParametricCurves.MAX_TYPES_IN_LCMS_PLUGIN],
                Evaluator = Marshal.GetFunctionPointerForDelegate(myFns2)
            };
            curveSample2.FunctionTypes[0] = TYPE_TAN;
            curveSample2.ParameterCount[0] = 1;

            rawsize = Marshal.SizeOf(curveSample2);
            IntPtr curveSample2Plugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(curveSample2, curveSample2Plugin, false);

            using (var ctx = Context.Create(curveSamplePlugin, IntPtr.Zero))
            using (var cpy = ctx.Duplicate(IntPtr.Zero))
            using (var cpy2 = cpy.Duplicate(IntPtr.Zero))
            {
                cpy.RegisterPlugins(curveSample2Plugin);
                cpy2.RegisterPlugins(rec709Plugin);

                double[] scale = { 1.0 };

                // Act
                using (var sinus = ToneCurve.BuildParametric(cpy, TYPE_SIN, scale))
                using (var cosinus = ToneCurve.BuildParametric(cpy, TYPE_COS, scale))
                using (var tangent = ToneCurve.BuildParametric(cpy, TYPE_TAN, scale))
                using (var reverse_sinus = sinus.Reverse())
                using (var reverse_cosinus = cosinus.Reverse())
                {
                    // Assert
                    var actual = sinus.Evaluate(0.1f);
                    Assert.AreEqual(Math.Sin(0.1 * Math.PI), actual, 0.001);
                    actual = sinus.Evaluate(0.6f);
                    Assert.AreEqual(Math.Sin(0.6 * Math.PI), actual, 0.001);
                    actual = sinus.Evaluate(0.9f);
                    Assert.AreEqual(Math.Sin(0.9 * Math.PI), actual, 0.001);

                    actual = cosinus.Evaluate(0.1f);
                    Assert.AreEqual(Math.Cos(0.1 * Math.PI), actual, 0.001);
                    actual = cosinus.Evaluate(0.6f);
                    Assert.AreEqual(Math.Cos(0.6 * Math.PI), actual, 0.001);
                    actual = cosinus.Evaluate(0.9f);
                    Assert.AreEqual(Math.Cos(0.9 * Math.PI), actual, 0.001);

                    actual = tangent.Evaluate(0.1f);
                    Assert.AreEqual(Math.Tan(0.1 * Math.PI), actual, 0.001);
                    actual = tangent.Evaluate(0.6f);
                    Assert.AreEqual(Math.Tan(0.6 * Math.PI), actual, 0.001);
                    actual = tangent.Evaluate(0.9f);
                    Assert.AreEqual(Math.Tan(0.9 * Math.PI), actual, 0.001);

                    actual = reverse_sinus.Evaluate(0.1f);
                    Assert.AreEqual(Math.Asin(0.1) / Math.PI, actual, 0.001);
                    actual = reverse_sinus.Evaluate(0.6f);
                    Assert.AreEqual(Math.Asin(0.6) / Math.PI, actual, 0.001);
                    actual = reverse_sinus.Evaluate(0.9f);
                    Assert.AreEqual(Math.Asin(0.9) / Math.PI, actual, 0.001);

                    actual = reverse_cosinus.Evaluate(0.1f);
                    Assert.AreEqual(Math.Acos(0.1) / Math.PI, actual, 0.001);
                    actual = reverse_cosinus.Evaluate(0.6f);
                    Assert.AreEqual(Math.Acos(0.6) / Math.PI, actual, 0.001);
                    actual = reverse_cosinus.Evaluate(0.9f);
                    Assert.AreEqual(Math.Acos(0.9) / Math.PI, actual, 0.001);
                }
            }

            double MyFns(int Type, double[] Params, double R)
            {
                switch (Type)
                {
                    case TYPE_SIN:
                        return Params[0] * Math.Sin(R * Math.PI);
                    case -TYPE_SIN:
                        return Math.Asin(R) / (Math.PI * Params[0]);
                    case TYPE_COS:
                        return Params[0] * Math.Cos(R * Math.PI);
                    case -TYPE_COS:
                        return Math.Acos(R) / (Math.PI * Params[0]);
                    default:
                        return -1.0;
                }
            }

            double MyFns2(int Type, double[] Params, double R)
            {
                switch (Type)
                {
                    case TYPE_TAN:
                        return Params[0] * Math.Tan(R * Math.PI);
                    case -TYPE_TAN:
                        return Math.Atan(R) / (Math.PI * Params[0]);
                    default:
                        return -1.0;
                }
            }

            double Rec709Math(int Type, double[] Params, double R)
            {
                double Fun = 0;

                switch (Type)
                {
                    case TYPE_709:
                        if (R <= (Params[3] * Params[4])) Fun = R / Params[3];
                        else Fun = Math.Pow(((R - Params[2]) / Params[1]), Params[0]);
                        break;
                    case -TYPE_709:
                        if (R <= Params[4]) Fun = R * Params[3];
                        else Fun = Params[1] * Math.Pow(R, (1 / Params[0])) + Params[2];
                        break;
                }

                return Fun;
            }
        }

        [TestMethod]
        public void PluginFormattersTest()
        {
            // Arrange
            uint TYPE_RGB_565 = COLORSPACE_SH(PixelType.RGB) | CHANNELS_SH(3) | BYTES_SH(0) | (1 << 23);
            var myUnroll565 = new Formatter16(MyUnroll565);
            var myPack565 = new Formatter16(MyPack565);
            var myFormatterFactory = new FormatterFactory(MyFormatterFactory);
            var myFormatterFactory2 = new FormatterFactory(MyFormatterFactory2);

            PluginFormatters formattersSample = new PluginFormatters
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)2060,
                    Type = PluginType.Formatters,
                    Next = IntPtr.Zero
                },
                FormattersFactory = Marshal.GetFunctionPointerForDelegate(myFormatterFactory)
            };

            int rawsize = Marshal.SizeOf(formattersSample);
            IntPtr formattersSamplePlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(formattersSample, formattersSamplePlugin, false);

            PluginFormatters formattersSample2 = new PluginFormatters
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)2060,
                    Type = PluginType.Formatters,
                    Next = IntPtr.Zero
                },
                FormattersFactory = Marshal.GetFunctionPointerForDelegate(myFormatterFactory2)
            };

            rawsize = Marshal.SizeOf(formattersSample2);
            IntPtr formattersSample2Plugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(formattersSample2, formattersSample2Plugin, false);

            // Act
            using (var ctx = Context.Create(formattersSamplePlugin, IntPtr.Zero))
            using (var cpy = ctx.Duplicate(IntPtr.Zero))
            {
                cpy.RegisterPlugins(formattersSample2Plugin);

                using (var cpy2 = cpy.Duplicate(IntPtr.Zero))
                using (var transform = Transform.Create(cpy2, null, TYPE_RGB_565,
                        null, TYPE_RGB_565, Intent.Perceptual, CmsFlags.NullTransform))
                {
                    byte[] stream = { 0xFF, 0xFF, 0x34, 0x12, 0x00, 0x00, 0xDD, 0x33 };
                    byte[] result = new byte[stream.Length];

                    transform.DoTransform(stream, result, 4);

                    // Assert
                    for (int i = 0; i < stream.Length; i++)
                    {
                        Assert.AreEqual(stream[i], result[i]);
                    }
                }
            }

            Formatter MyFormatterFactory(uint Type, FormatterDirection Dir, uint dwFlags)
            {
                Formatter result = new Formatter
                {
                    Fmt = IntPtr.Zero
                };

                if ((Type == TYPE_RGB_565) &&
                    ((dwFlags & PluginFormatters.PACK_FLAGS_FLOAT) == 0) &&
                    (Dir == FormatterDirection.Input))
                {
                    result.Fmt = Marshal.GetFunctionPointerForDelegate(myUnroll565);
                }

                return result;
            }

            Formatter MyFormatterFactory2(uint Type, FormatterDirection Dir, uint dwFlags)
            {
                Formatter result = new Formatter
                {
                    Fmt = IntPtr.Zero
                };

                if ((Type == TYPE_RGB_565) &&
                    ((dwFlags & PluginFormatters.PACK_FLAGS_FLOAT) == 0) &&
                    (Dir == FormatterDirection.Output))
                {
                    result.Fmt = Marshal.GetFunctionPointerForDelegate(myPack565);
                }

                return result;
            }

            IntPtr MyUnroll565(IntPtr CMMCargo, IntPtr Values, IntPtr Buffer, uint Stride)
            {
                unsafe
                {
                    ushort pixel = *(ushort*)Buffer.ToPointer();

                    double r = Math.Floor((pixel & 31) * 65535.0 / 31.0 + 0.5);
                    double g = Math.Floor((((pixel >> 5) & 63) * 65535.0) / 63.0 + 0.5);
                    double b = Math.Floor((((pixel >> 11) & 31) * 65535.0) / 31.0 + 0.5);

                    ushort *wIn = (ushort*)Values.ToPointer();
                    wIn[2] = (ushort)r;
                    wIn[1] = (ushort)g;
                    wIn[0] = (ushort)b;

                    return Buffer + 2;
                }
            }

            IntPtr MyPack565(IntPtr CMMCargo, IntPtr Values, IntPtr Buffer, uint Stride)
            {
                unsafe
                {
                    ushort *wOut = (ushort*)Values.ToPointer();

                    int r = (int)Math.Floor((wOut[2] * 31) / 65535.0 + 0.5);
                    int g = (int)Math.Floor((wOut[1] * 63) / 65535.0 + 0.5);
                    int b = (int)Math.Floor((wOut[0] * 31) / 65535.0 + 0.5);

                    ushort pixel = (ushort)((r & 31) | ((g & 63) << 5) | ((b & 31) << 11));

                    ushort *output = (ushort*)Buffer.ToPointer();
                    *output = pixel;

                    return Buffer + 2;
                }
            }

            uint COLORSPACE_SH(PixelType s) { return Convert.ToUInt32(s) << 16; }
            uint CHANNELS_SH(uint s) { return s << 3; }
            uint BYTES_SH(uint s) { return s; }
        }

        [TestMethod]
        public void PluginIntentTest()
        {
            // Arrange
            const uint INTENT_DECEPTIVE = 300;
            var myIntent = new IntentFn(MyNewIntent);

            PluginIntent intentSample = new PluginIntent
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)2060,
                    Type = PluginType.RenderingIntent,
                    Next = IntPtr.Zero
                },
                Intent = INTENT_DECEPTIVE,
                Link = Marshal.GetFunctionPointerForDelegate(myIntent),
                Description = new byte[256]
            };
            var description = "bypass gray to gray rendering intent";
            Encoding.ASCII.GetBytes(description, 0, description.Length, intentSample.Description, 0);

            int rawsize = Marshal.SizeOf(intentSample);
            IntPtr intentSamplePlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(intentSample, intentSamplePlugin, false);

            // Act
            using (var ctx = Context.Create(intentSamplePlugin, IntPtr.Zero))
            using (var cpy = ctx.Duplicate(IntPtr.Zero))
            using (var cpy2 = cpy.Duplicate(IntPtr.Zero))
            using (var linear1 = ToneCurve.BuildGamma(cpy2, 3.0))
            using (var linear2 = ToneCurve.BuildGamma(cpy2, 1.0))
            using (var h1 = Profile.CreateLinearizationDeviceLink(cpy2, ColorSpaceSignature.GrayData, new ToneCurve[] { linear1 }))
            using (var h2 = Profile.CreateLinearizationDeviceLink(cpy2, ColorSpaceSignature.GrayData, new ToneCurve[] { linear2 }))
            {
                using (var xform = Transform.Create(cpy2, h1, Cms.TYPE_GRAY_8, h2, Cms.TYPE_GRAY_8, (Intent)INTENT_DECEPTIVE, CmsFlags.None))
                {
                    byte[] inBuffer = new byte[] { 10, 20, 30, 40 };
                    byte[] outBuffer = new byte[inBuffer.Length];

                    xform.DoTransform(inBuffer, outBuffer, inBuffer.Length);

                    // Assert
                    for (var i = 0; i < inBuffer.Length; i++)
                    {
                        Assert.AreEqual(inBuffer[i], outBuffer[i]);
                    }
                }
            }

            IntPtr MyNewIntent(IntPtr contextID,
                uint nProfiles,
                IntPtr intents,             // uint[]
                IntPtr hProfiles,           // IntPtr[]
                IntPtr BPC,                 // int[]
                IntPtr AdaptationStates,    // double[]
                uint flags)
            {
                Intent[] ICCIntents = new Intent[nProfiles];
                Profile[] profiles = new Profile[nProfiles];
                bool[] bpc = new bool[nProfiles];
                double[] adaptationStates = new double[nProfiles];
                Context context = Context.FromHandle(contextID);

                unsafe
                {
                    uint* theIntents = (uint*)intents.ToPointer();
                    IntPtr* pProfiles = (IntPtr*)hProfiles.ToPointer();
                    int* pBPC = (int*)BPC.ToPointer();
                    double* pAdaptationStates = (double*)AdaptationStates.ToPointer();

                    for (uint i = 0; i < nProfiles; i++)
                    {
                        ICCIntents[i] = (theIntents[i] == INTENT_DECEPTIVE)
                                ? Intent.Perceptual : (Intent)theIntents[i];
                        profiles[i] = Profile.FromHandle(pProfiles[i]);
                        bpc[i] = pBPC[i] != 0;
                        adaptationStates[i] = pAdaptationStates[i];
                    }

                    if (profiles[0].ColorSpace != ColorSpaceSignature.GrayData ||
                        profiles[nProfiles-1].ColorSpace != ColorSpaceSignature.GrayData)
                    {
                        return Pipeline.DefaultICCIntents(context,
                                ICCIntents, profiles, bpc, adaptationStates, (CmsFlags)flags).Handle;
                    }

                    Pipeline result = Pipeline.Create(context, 1, 1);
                    result.Insert(Stage.Create(context, 1), StageLoc.At_Begin);
                    return result.Release();
                }
            }
        }

        [TestMethod()]
        public void PluginStageTest()
        {
            // Arrange
            const TagTypeSignature SigNegateType = (TagTypeSignature)0x6E202020;  // 'n   '
            var read = new TagTypeRead(NegateRead);
            var write = new TagTypeWrite(NegateWrite);

            PluginMultiProcessElement mpeSample = new PluginMultiProcessElement
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)2060,
                    Type = PluginType.MultiProcessElement,
                    Next = IntPtr.Zero
                },
                Handler = new TagTypeHandler
                {
                    Signature = SigNegateType,
                    Read = Marshal.GetFunctionPointerForDelegate(read),
                    Write = Marshal.GetFunctionPointerForDelegate(write),
                    Duplicate = IntPtr.Zero,
                    Free = IntPtr.Zero
                }
            };

            var rawsize = Marshal.SizeOf(mpeSample);
            IntPtr mpePlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(mpeSample, mpePlugin, false);

            // Act
            using (var ctx = Context.Create(mpePlugin, IntPtr.Zero))
            using (var cpy = ctx.Duplicate(IntPtr.Zero))
            using (var cpy2 = cpy.Duplicate(IntPtr.Zero))
            {
                byte[] data = null;

                using (var profile = Profile.CreatePlaceholder(cpy2))
                {
                    using (var pipe = Pipeline.Create(cpy2, 3, 3))
                    {
                        pipe.Insert(Stage.FromHandle(StageAllocNegate(cpy2.Handle)), StageLoc.At_Begin);

                        float[] In = new float[] { 0.3f, 0.2f, 0.9f };
                        var actual = pipe.Evaluate(In);

                        // Assert
                        Assert.AreEqual(1.0 - In[0], actual[0], 0.001);
                        Assert.AreEqual(1.0 - In[1], actual[1], 0.001);
                        Assert.AreEqual(1.0 - In[2], actual[2], 0.001);

                        profile.WriteTag(TagSignature.DToB3, pipe);
                    }

                    profile.Save(null, out uint bytesNeeded);
                    data = new byte[bytesNeeded];
                    profile.Save(data, out bytesNeeded);
                }

                using (var profile2 = Profile.Open(data))
                {
                    // unsupported stage in global context
                    var expected = IntPtr.Zero;
                    var actual = profile2.ReadTag(TagSignature.DToB3);
                    Assert.AreEqual(expected, actual);
                }

                using (var profile3 = Profile.Open(cpy2, data))
                {
                    using (var pipe2 = profile3.ReadTag<Pipeline>(TagSignature.DToB3))
                    {
                        float[] In = new float[] { 0.3f, 0.2f, 0.9f };
                        var actual = pipe2.Evaluate(In);

                        // Assert
                        Assert.AreEqual(1.0 - In[0], actual[0], 0.001);
                        Assert.AreEqual(1.0 - In[1], actual[1], 0.001);
                        Assert.AreEqual(1.0 - In[2], actual[2], 0.001);
                    }
                }
            }

            void EvaluateNegate(IntPtr In, IntPtr Out, IntPtr mpe)
            {
                unsafe
                {
                    float* pOut = (float*)Out.ToPointer();
                    float* pIn = (float *)In.ToPointer();

                    pOut[0] = 1.0f - pIn[0];
                    pOut[1] = 1.0f - pIn[1];
                    pOut[2] = 1.0f - pIn[2];
                }
            }

            IntPtr StageAllocNegate(IntPtr contextID)
            {
                using (var context = Context.FromHandle(contextID))
                {
                    return Stage.AllocPlaceholder(context, (StageSignature)SigNegateType, 3, 3,
                            EvaluateNegate, null, null, IntPtr.Zero).Release();
                }
            }

            IntPtr NegateRead(in TagTypeHandler self, IntPtr io, out uint nItems, uint tagSize)
            {
                nItems = 1;

                using (var iohandler = IOHandler.FromHandle(io))
                {
                    ushort chans = 0;
                    if (!iohandler.Read(ref chans)) return IntPtr.Zero;
                    if (chans != 3) return IntPtr.Zero;
                }

                return StageAllocNegate(self.ContextID);
            }

            int NegateWrite(in TagTypeHandler self, IntPtr io, IntPtr ptr, uint nItems)
            {
                using (var iohandler = IOHandler.FromHandle(io))
                {
                    const ushort chans = 3;
                    return iohandler.Write(chans) ? 1 : 0;
                }
            }
        }

        [TestMethod()]
        public void PluginOptimizationTest()
        {
            // Arrange
            var optimize = new OptimizeFn(MyOptimize);

            PluginOptimization optimizationSample = new PluginOptimization
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)2060,
                    Type = PluginType.Optimization,
                    Next = IntPtr.Zero
                },
                Optimize = Marshal.GetFunctionPointerForDelegate(optimize)
            };

            var rawsize = Marshal.SizeOf(optimizationSample);
            IntPtr optimizationPlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(optimizationSample, optimizationPlugin, false);

            // Act
            using (var ctx = Context.Create(optimizationPlugin, IntPtr.Zero))
            using (var cpy = ctx.Duplicate(IntPtr.Zero))
            using (var cpy2 = cpy.Duplicate(IntPtr.Zero))
            {
                using (var linear = ToneCurve.BuildGamma(cpy2, 1.0))
                using (var profile = Profile.CreateLinearizationDeviceLink(cpy2,
                        ColorSpaceSignature.GrayData, new ToneCurve[] { linear }))
                using (var xform = Transform.Create(cpy2, profile, Cms.TYPE_GRAY_8, profile, Cms.TYPE_GRAY_8, Intent.Perceptual, CmsFlags.None))
                {
                    byte[] In = new byte[] { 120, 20, 30, 40 };
                    byte[] Out = new byte[In.Length];

                    xform.DoTransform(In, Out, In.Length);

                    // Assert
                    for (int i = 0; i < In.Length; i++)
                    {
                        Assert.AreEqual(In[i], Out[i]);
                    }
                }
            }

            void FastEvaluteCurves(IntPtr In, IntPtr Out, IntPtr Data)
            {
                unsafe
                {
                    ushort* pIn = (ushort *)In.ToPointer();
                    ushort* pOut = (ushort *)Out.ToPointer();

                    pOut[0] = pIn[0];
                }
            }

            int MyOptimize(IntPtr lut, uint intent, IntPtr inputFormat, IntPtr outputFormat, IntPtr dwFlags)
            {
                unsafe
                {
                    IntPtr *pLut = (IntPtr *)lut.ToPointer();
                    using (var pipeline = Pipeline.FromHandle(pLut[0]))
                    {
                        foreach (var stage in pipeline)
                        {
                            if (stage.StageType != StageSignature.CurveSetElemType) return 0;

                            StageToneCurveData *data = (StageToneCurveData *)stage.Data;
                            if (data->nCurves != 1) return 0;
                            IntPtr* theCurves = (IntPtr*)data->TheCurves;
                            using (var toneCurve = ToneCurve.FromHandle(theCurves[0]))
                            {
                                if (toneCurve.EstimateGamma(0.1) > 1.0) return 0;
                            }
                        }

                        uint* flags = (uint*)dwFlags.ToPointer();
                        *flags |= (uint)CmsFlags.NoCache;

                        pipeline.SetOptimizationParameters(FastEvaluteCurves, IntPtr.Zero, null, null);
                    }
                }

                return 1;
            }
        }

#pragma warning disable CS0649
        private struct StageToneCurveData
        {
            public uint nCurves;
            public IntPtr TheCurves;    // cmsToneCurve**
        }
#pragma warning restore CS0649

        [TestMethod()]
        public void PluginTransform2Test()
        {
            // Arrange
            var factory = new TransformFactory(MyTransformFactory);

            PluginTransform transformSample = new PluginTransform
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)2080,
                    Type = PluginType.Transform,
                    Next = IntPtr.Zero
                },
                Factory = Marshal.GetFunctionPointerForDelegate(factory)
            };

            var rawsize = Marshal.SizeOf(transformSample);
            IntPtr transformPlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(transformSample, transformPlugin, false);

            // Act
            using (var ctx = Context.Create(transformPlugin, IntPtr.Zero))
            using (var cpy = ctx.Duplicate(IntPtr.Zero))
            using (var cpy2 = cpy.Duplicate(IntPtr.Zero))
            {
                using (var linear = ToneCurve.BuildGamma(cpy2, 1.0))
                using (var profile = Profile.CreateLinearizationDeviceLink(cpy2,
                        ColorSpaceSignature.GrayData, new ToneCurve[] { linear }))
                using (var xform = Transform.Create(cpy2, profile, Cms.TYPE_GRAY_8, profile, Cms.TYPE_GRAY_8, Intent.Perceptual, CmsFlags.None))
                {
                    byte[] In = new byte[] { 10, 20, 30, 40 };
                    byte[] Out = new byte[In.Length];

                    xform.DoTransform(In, Out, In.Length);

                    // Assert
                    for (int i = 0; i < In.Length; i++)
                    {
                        Assert.AreEqual((byte)42, Out[i]);
                    }
                }
            }

            // only works for gray8 as output and always returns '42'
            void TranscendentalTransform2(IntPtr CMMCargo, IntPtr InputBuffer, IntPtr OutputBuffer, uint PixelsPerLine,
                    uint LineCount, IntPtr Stride)
            {
                unsafe
                {
                    byte* pOut = (byte*)OutputBuffer.ToPointer();
                    Stride* stride = (Stride*)Stride.ToPointer();

                    for (uint line = 0; line < LineCount; line++)
                    {
                        for (uint pixel = 0; pixel < PixelsPerLine; pixel++)
                        {
                            *pOut++ = 42;
                        }
                        pOut += stride->BytesPerLineOut - PixelsPerLine;
                    }
                }
            }

            int MyTransformFactory(IntPtr xformPtr, IntPtr UserData, IntPtr FreePrivateDataFn, IntPtr Lut,
                    IntPtr InputFormat, IntPtr OutputFormat, IntPtr dwFlags)
            {
                unsafe
                {
                    uint* outputFormat = (uint*)OutputFormat.ToPointer();
                    if (*outputFormat == Cms.TYPE_GRAY_8)
                    {
                        IntPtr* ptr = (IntPtr*)xformPtr.ToPointer();
                        *ptr = Marshal.GetFunctionPointerForDelegate(new Transform2Fn(TranscendentalTransform2));
                        return 1;
                    }

                    return 0;
                }
            }
        }

        [TestMethod()]
        public void PluginTransformTest()
        {
            // Arrange
            var factory = new TransformFactory(MyTransformFactory);

            PluginTransform transformSample = new PluginTransform
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)2060,
                    Type = PluginType.Transform,
                    Next = IntPtr.Zero
                },
                Factory = Marshal.GetFunctionPointerForDelegate(factory)
            };

            var rawsize = Marshal.SizeOf(transformSample);
            IntPtr transformPlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(transformSample, transformPlugin, false);

            // Act
            using (var ctx = Context.Create(transformPlugin, IntPtr.Zero))
            using (var cpy = ctx.Duplicate(IntPtr.Zero))
            using (var cpy2 = cpy.Duplicate(IntPtr.Zero))
            {
                using (var linear = ToneCurve.BuildGamma(cpy2, 1.0))
                using (var profile = Profile.CreateLinearizationDeviceLink(cpy2,
                        ColorSpaceSignature.GrayData, new ToneCurve[] { linear }))
                using (var xform = Transform.Create(cpy2, profile, Cms.TYPE_GRAY_8, profile, Cms.TYPE_GRAY_8, Intent.Perceptual, CmsFlags.None))
                {
                    byte[] In = new byte[] { 10, 20, 30, 40 };
                    byte[] Out = new byte[In.Length];

                    xform.DoTransform(In, Out, In.Length);

                    // Assert
                    for (int i = 0; i < In.Length; i++)
                    {
                        Assert.AreEqual((byte)42, Out[i]);
                    }
                }
            }

            // only works for gray8 as output and always returns '42'
            void TranscendentalTransform(IntPtr CMMCargo, IntPtr InputBuffer, IntPtr OutputBuffer, uint Size, uint Stride)
            {
                unsafe
                {
                    byte* pOut = (byte*)OutputBuffer.ToPointer();

                    for (uint i = 0; i < Size; i++)
                    {
                        pOut[i] = 42;
                    }
                }
            }

            int MyTransformFactory(IntPtr xformPtr, IntPtr UserData, IntPtr FreePrivateDataFn, IntPtr Lut,
                    IntPtr InputFormat, IntPtr OutputFormat, IntPtr dwFlags)
            {
                unsafe
                {
                    uint* outputFormat = (uint*)OutputFormat.ToPointer();
                    if (*outputFormat == Cms.TYPE_GRAY_8)
                    {
                        IntPtr* ptr = (IntPtr*)xformPtr.ToPointer();
                        *ptr = Marshal.GetFunctionPointerForDelegate(new TransformFn(TranscendentalTransform));
                        return 1;
                    }

                    return 0;
                }
            }
        }

        [TestMethod()]
        public void PluginMutexTest()
        {
            // Arrange
            var create = new CreateMutexFn(MyCreateMutex);
            var destroy = new DestroyMutexFn(MyDestroyMutex);
            var locker = new LockMutexFn(MyLockMutex);
            var unlocker = new UnlockMutexFn(MyUnlockMutex);

            PluginMutex mutexSample = new PluginMutex
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)2060,
                    Type = PluginType.Mutex,
                    Next = IntPtr.Zero
                },
                Create = Marshal.GetFunctionPointerForDelegate(create),
                Destroy = Marshal.GetFunctionPointerForDelegate(destroy),
                Lock = Marshal.GetFunctionPointerForDelegate(locker),
                Unlock = Marshal.GetFunctionPointerForDelegate(unlocker)
            };

            var rawsize = Marshal.SizeOf(mutexSample);
            IntPtr mutexPlugin = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(mutexSample, mutexPlugin, false);

            // Act
            using (var ctx = Context.Create(mutexPlugin, IntPtr.Zero))
            using (var cpy = ctx.Duplicate(IntPtr.Zero))
            using (var cpy2 = cpy.Duplicate(IntPtr.Zero))
            {
                using (var linear = ToneCurve.BuildGamma(cpy2, 1.0))
                using (var profile = Profile.CreateLinearizationDeviceLink(cpy2,
                        ColorSpaceSignature.GrayData, new ToneCurve[] { linear }))
                using (var xform = Transform.Create(cpy2, profile, Cms.TYPE_GRAY_8, profile, Cms.TYPE_GRAY_8, Intent.Perceptual, CmsFlags.None))
                {
                    byte[] In = new byte[] { 10, 20, 30, 40 };
                    byte[] Out = new byte[In.Length];

                    xform.DoTransform(In, Out, In.Length);

                    // Assert
                    for (int i = 0; i < In.Length; i++)
                    {
                        Assert.AreEqual(In[i], Out[i]);
                    }
                }
            }

            IntPtr MyCreateMutex(IntPtr ContextID)
            {
                TestContext.WriteLine($"Creating mutex in context {ContextID}");

                var myMutex = new MyMutex();
                int size = Marshal.SizeOf(myMutex);
                IntPtr ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(myMutex, ptr, false);

                return ptr;
            }

            void MyDestroyMutex(IntPtr ContextID, IntPtr mutex)
            {
                TestContext.WriteLine($"Destroying mutex in context {ContextID}");

                MyMutex myMutex = Marshal.PtrToStructure<MyMutex>(mutex);
                if (myMutex.nLocks != 0) TestContext.WriteLine("Destroying mutex when number of locks is non-zero.");
                Marshal.FreeHGlobal(mutex);
            }

            int MyLockMutex(IntPtr ContextID, IntPtr mutex)
            {
                TestContext.WriteLine($"Locking mutex in context {ContextID}");

                MyMutex myMutex = Marshal.PtrToStructure<MyMutex>(mutex);
                myMutex.nLocks++;

                return 1;
            }

            void MyUnlockMutex(IntPtr ContextID, IntPtr mutex)
            {
                TestContext.WriteLine($"Unlocking mutex in context {ContextID}");

                MyMutex myMutex = Marshal.PtrToStructure<MyMutex>(mutex);
                myMutex.nLocks--;
            }
        }

        private struct MyMutex
        {
            public int nLocks;
        }
    }
}
