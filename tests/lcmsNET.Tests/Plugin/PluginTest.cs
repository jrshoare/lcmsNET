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
using lcmsNET.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;
using System.Text;
using static lcmsNET.Tests.TestUtils.MultiLocalizedUnicodeUtils;

namespace lcmsNET.Tests.Plugin
{
    [TestClass()]
    public class PluginTest
    {
        [TestMethod()]
        public void PluginTag_WhenUsingToReadWrite_ShouldSucceed()
        {
            // Arrange
            var sut = PluginTagUtils.CreatePluginTag();
            sut.Descriptor.SupportedTypes[0] = TagTypeSignature.Text;

            string expected = "PluginTagTest";

            // Act
            MemoryUtils.UsingMemoryFor(sut, (plugin) =>
            {
                using var context = Context.Create(plugin, userData: IntPtr.Zero);
                using var profile = Profile.CreatePlaceholder(context);
                var displayName = new DisplayName(expected);
                using (var mlu = CreateAsASCII(displayName))
                {
                    profile.WriteTag(Constants.PluginTag.Signature, mlu);
                }

                using (var mlu = profile.ReadTag<MultiLocalizedUnicode>(Constants.PluginTag.Signature))
                {
                    var actual = mlu.GetASCII(displayName.LanguageCode, displayName.CountryCode);

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            });
        }

        [TestMethod()]
        public void PluginTag_WhenUsingToReadWriteWithDecider_ShouldSucceed()
        {
            // Arrange
            // ensure delegates are not garbage collected from managed code
            var decide = new DecideType(PluginTagUtils.Decide);

            var sut = PluginTagUtils.CreatePluginTag();
            sut.Descriptor.SupportedTypes[0] = TagTypeSignature.Text;
            sut.Descriptor.Decider = Marshal.GetFunctionPointerForDelegate(decide);

            string expected = "PluginTagWithDeciderTest";

            // Act
            MemoryUtils.UsingMemoryFor(sut, (plugin) =>
            {
                using var context = Context.Create(plugin, userData: IntPtr.Zero);
                using var profile = Profile.CreatePlaceholder(context);
                var displayName = new DisplayName(expected);
                using (var mlu = CreateAsASCII(displayName))
                {
                    profile.WriteTag(Constants.PluginTag.Signature, mlu);
                }

                using (var mlu = profile.ReadTag<MultiLocalizedUnicode>(Constants.PluginTag.Signature))
                {
                    var actual = mlu.GetASCII(displayName.LanguageCode, displayName.CountryCode);

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            });
        }

        [TestMethod()]
        public void PluginTagType_WhenUsingToReadWrite_ShouldSucceed()
        {
            // Arrange
            const TagSignature SigInt = (TagSignature)0x74747448;  // 'tttH'
            const TagTypeSignature SigIntType = (TagTypeSignature)0x74747448;  // 'tttH'

            var tag = PluginTagUtils.CreatePluginTag();
            tag.Signature = SigInt;
            tag.Descriptor.SupportedTypes[0] = SigIntType;

            MemoryUtils.UsingMemoryFor(tag, (tagPlugin) =>
            {
                var handler = PluginTagTypeUtils.CreateTagTypeHandler();
                var sut = PluginTagTypeUtils.CreatePluginTagType(handler);
                sut.Base.Next = tagPlugin;
                sut.Handler.Signature = SigIntType;

                // Act
                MemoryUtils.UsingMemoryFor(sut, (tagTypePlugin) =>
                {
                    using var context = Context.Create(tagTypePlugin, IntPtr.Zero);
                    using var profile = Profile.CreatePlaceholder(context);
                    var errorHandler = new ErrorHandler(PluginTagTypeUtils.HandleError);
                    context.SetErrorHandler(errorHandler);

                    uint expected = 1234;
                    bool written = profile.WriteTag(SigInt, expected);
                    Assert.IsTrue(written);

                    profile.Save(null, out uint bytesNeeded);
                    Assert.AreNotEqual(0u, bytesNeeded);
                    byte[] profileMemory = new byte[bytesNeeded];

                    bool saved = profile.Save(profileMemory, out uint bytesWritten);
                    Assert.IsTrue(saved);
                    // close original profile to flush caches
                    profile.Close();

                    // re-open profile from memory
                    using var profile2 = Profile.Open(context, profileMemory);
                    IntPtr data = profile2.ReadTag(SigInt);
                    uint[] u = new uint[1];
                    Marshal.Copy(data, (int[])(object)u, 0, 1);
                    uint actual = u[0];

                    // Assert
                    Assert.AreEqual(expected, actual);
                });
            });
        }

        [TestMethod()]
        public void PluginMemoryHandler_WhenUsingMallocReallocFree_ShouldSucceed()
        {
            // Arrange
            PluginMemoryHandler sut = PluginMemoryHandlerUtils.CreatePluginMemoryHandler();

            MemoryUtils.UsingMemoryFor(sut, (memoryHandlerPlugin) =>
            {
                using var context = Context.Create(memoryHandlerPlugin, IntPtr.Zero);

                IntPtr mallocPtr = Memory.Malloc(context, 0x200);

                // Assert
                Assert.AreNotEqual(IntPtr.Zero, mallocPtr);

                IntPtr reallocPtr = Memory.Realloc(context, mallocPtr, 0x300);
                Assert.AreNotEqual(IntPtr.Zero, mallocPtr);

                Memory.Free(context, reallocPtr);
            });
        }

        [TestMethod()]
        public void PluginInterpolation_WhenInterpolating1D_ShouldSucceed()
        {
            // Arrange
            var factory = new InterpolatorsFactory(PluginInterpolationUtils.Factory);

            PluginInterpolation sut = PluginInterpolationUtils.CreatePluginInterpolation(factory);

            MemoryUtils.UsingMemoryFor(sut, (interpolationPlugin) =>
            {
                using var context = Context.Create(interpolationPlugin, IntPtr.Zero);
                // a straight line
                float[] tabulated = [0.0f, 0.10f, 0.20f, 0.30f, 0.40f, 0.50f, 0.60f, 0.70f, 0.80f, 0.90f, 1.00f];

                using var toneCurve = ToneCurve.BuildTabulated(context, tabulated);
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
            });
        }

        [TestMethod()]
        public void PluginInterpolation_WhenInterpolating3D_ShouldSucceed()
        {
            // Arrange
            var factory = new InterpolatorsFactory(PluginInterpolationUtils.Factory);

            PluginInterpolation sut = PluginInterpolationUtils.CreatePluginInterpolation(factory);

            MemoryUtils.UsingMemoryFor(sut, (interpolationPlugin) =>
            {
                using var context = Context.Create(interpolationPlugin, IntPtr.Zero);
                using var pipeline = Pipeline.Create(context, 3, 3);
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

                using var clut = Stage.Create(context, 2, 3, 3, identity);
                pipeline.Insert(clut, StageLoc.At_Begin);

                // do some interpolations with the plugin
                ushort[] input = [0, 0, 0];
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
            });
        }

        [TestMethod]
        public void PluginParametricCurves_WhenUsingRec709_ShouldSucceed()
        {
            // Arrange
            var rec709Math = new ParametricCurveEvaluator(PluginParametricCurvesUtils.Rec709Math);
            PluginParametricCurves sut = PluginParametricCurvesUtils.CreatePluginParametricCurves(nFunctions: 1);
            sut.Evaluator = Marshal.GetFunctionPointerForDelegate(rec709Math);
            sut.FunctionTypes[0] = Constants.PluginParametricCurves.TYPE_709;
            sut.ParameterCount[0] = 5;

            MemoryUtils.UsingMemoryFor(sut, (rec709Plugin) =>
            {
                var myFns = new ParametricCurveEvaluator(PluginParametricCurvesUtils.MyFns);
                PluginParametricCurves curveSample = PluginParametricCurvesUtils.CreatePluginParametricCurves(nFunctions: 2);
                curveSample.Evaluator = Marshal.GetFunctionPointerForDelegate(myFns);
                curveSample.FunctionTypes[0] = Constants.PluginParametricCurves.TYPE_SIN;
                curveSample.FunctionTypes[1] = Constants.PluginParametricCurves.TYPE_COS;
                curveSample.ParameterCount[0] = 1;
                curveSample.ParameterCount[1] = 1;

                MemoryUtils.UsingMemoryFor(curveSample, (curveSamplePlugin) =>
                {
                    var myFns2 = new ParametricCurveEvaluator(PluginParametricCurvesUtils.MyFns2);
                    PluginParametricCurves curveSample2 = PluginParametricCurvesUtils.CreatePluginParametricCurves(nFunctions: 1);
                    curveSample2.Evaluator = Marshal.GetFunctionPointerForDelegate(myFns2);
                    curveSample2.FunctionTypes[0] = Constants.PluginParametricCurves.TYPE_TAN;
                    curveSample2.ParameterCount[0] = 1;

                    MemoryUtils.UsingMemoryFor(curveSample2, (curveSample2Plugin) =>
                    {
                        using var ctx = Context.Create(curveSamplePlugin, IntPtr.Zero);
                        using var cpy = ctx.Duplicate(IntPtr.Zero);
                        using var cpy2 = cpy.Duplicate(IntPtr.Zero);
                        cpy.RegisterPlugins(curveSample2Plugin);
                        cpy2.RegisterPlugins(rec709Plugin);

                        double[] scale = { 1.0 };

                        // Act
                        using var sinus = ToneCurve.BuildParametric(cpy, Constants.PluginParametricCurves.TYPE_SIN, scale);
                        using var cosinus = ToneCurve.BuildParametric(cpy, Constants.PluginParametricCurves.TYPE_COS, scale);
                        using var tangent = ToneCurve.BuildParametric(cpy, Constants.PluginParametricCurves.TYPE_TAN, scale);
                        using var reverse_sinus = sinus.Reverse();
                        using var reverse_cosinus = cosinus.Reverse();
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
                    });
                });
            });
        }

        [TestMethod]
        public void PluginFormatters_WhenFormatting_ShouldSucceed()
        {
            // Arrange
            var myFormatterFactory = new FormatterFactory(PluginFormattersUtils.MyFormatterFactory);
            PluginFormatters formattersSample = PluginFormattersUtils.CreatePluginFormatters();
            formattersSample.FormattersFactory = Marshal.GetFunctionPointerForDelegate(myFormatterFactory);

            MemoryUtils.UsingMemoryFor(formattersSample, (formattersSamplePlugin) =>
            {
                var myFormatterFactory2 = new FormatterFactory(PluginFormattersUtils.MyFormatterFactory2);
                PluginFormatters formattersSample2 = PluginFormattersUtils.CreatePluginFormatters();
                formattersSample2.FormattersFactory = Marshal.GetFunctionPointerForDelegate(myFormatterFactory2);

                MemoryUtils.UsingMemoryFor(formattersSample2, (formattersSample2Plugin) =>
                {
                    // Act
                    using var ctx = Context.Create(formattersSamplePlugin, IntPtr.Zero);
                    using var cpy = ctx.Duplicate(IntPtr.Zero);
                    cpy.RegisterPlugins(formattersSample2Plugin);

                    using var cpy2 = cpy.Duplicate(IntPtr.Zero);
                    using var transform = Transform.Create(cpy2, null, Constants.PluginFormatters.TYPE_RGB_565,
                            null, Constants.PluginFormatters.TYPE_RGB_565, Intent.Perceptual, CmsFlags.NullTransform);
                    byte[] stream = [0xFF, 0xFF, 0x34, 0x12, 0x00, 0x00, 0xDD, 0x33];
                    byte[] result = new byte[stream.Length];

                    transform.DoTransform(stream, result, 4);

                    // Assert
                    for (int i = 0; i < stream.Length; i++)
                    {
                        Assert.AreEqual(stream[i], result[i]);
                    }
                });
            });
        }

        [TestMethod]
        public void PluginIntent_WhenTransforming_ShouldSucceed()
        {
            // Arrange
            PluginIntent sut = PluginIntentUtils.CreatePluginIntent();
            var description = "bypass gray to gray rendering intent";
            Encoding.ASCII.GetBytes(description, 0, description.Length, sut.Description, 0);

            MemoryUtils.UsingMemoryFor(sut, (intentSamplePlugin) =>
            {
                // Act
                using var ctx = Context.Create(intentSamplePlugin, IntPtr.Zero);
                using var cpy = ctx.Duplicate(IntPtr.Zero);
                using var cpy2 = cpy.Duplicate(IntPtr.Zero);
                using var linear1 = ToneCurve.BuildGamma(cpy2, 3.0);
                using var linear2 = ToneCurve.BuildGamma(cpy2, 1.0);
                using var h1 = Profile.CreateLinearizationDeviceLink(cpy2, ColorSpaceSignature.GrayData, [linear1]);
                using var h2 = Profile.CreateLinearizationDeviceLink(cpy2, ColorSpaceSignature.GrayData, [linear2]);
                using var xform = Transform.Create(cpy2, h1, Cms.TYPE_GRAY_8, h2, Cms.TYPE_GRAY_8,
                        (Intent)Constants.PluginIntent.INTENT_DECEPTIVE, CmsFlags.None);
                byte[] inBuffer = [10, 20, 30, 40];
                byte[] outBuffer = new byte[inBuffer.Length];

                xform.DoTransform(inBuffer, outBuffer, inBuffer.Length);

                // Assert
                for (var i = 0; i < inBuffer.Length; i++)
                {
                    Assert.AreEqual(inBuffer[i], outBuffer[i]);
                }
            });
        }

        [TestMethod()]
        public void PluginStage_WhenUsingToSaveRestore_ShouldSucceed()
        {
            // Arrange
            PluginMultiProcessElement sut = PluginMultiProcessElementUtils.CreatePluginMultiProcessElement();

            MemoryUtils.UsingMemoryFor(sut, (mpePlugin) =>
            {

                // Act
                using var ctx = Context.Create(mpePlugin, IntPtr.Zero);
                using var cpy = ctx.Duplicate(IntPtr.Zero);
                using var cpy2 = cpy.Duplicate(IntPtr.Zero);
                byte[] data = null;

                using (var profile = Profile.CreatePlaceholder(cpy2))
                {
                    using (var pipe = Pipeline.Create(cpy2, 3, 3))
                    {
                        pipe.Insert(Stage.FromHandle(PluginMultiProcessElementUtils.StageAllocNegate(cpy2.Handle)), StageLoc.At_Begin);

                        float[] In = [0.3f, 0.2f, 0.9f];
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

                using var profile3 = Profile.Open(cpy2, data);
                using (var pipe2 = profile3.ReadTag<Pipeline>(TagSignature.DToB3))
                {
                    float[] In = [0.3f, 0.2f, 0.9f];
                    var actual = pipe2.Evaluate(In);

                    // Assert
                    Assert.AreEqual(1.0 - In[0], actual[0], 0.001);
                    Assert.AreEqual(1.0 - In[1], actual[1], 0.001);
                    Assert.AreEqual(1.0 - In[2], actual[2], 0.001);
                }
            });
        }

        [TestMethod()]
        public void PluginOptimization_WhenOptimizing_ShouldSucceed()
        {
            // Arrange
            PluginOptimization sut = PluginOptimizationUtils.CreatePluginOptimization();

            MemoryUtils.UsingMemoryFor(sut, (optimizationPlugin) =>
            {
                // Act
                using var ctx = Context.Create(optimizationPlugin, IntPtr.Zero);
                using var cpy = ctx.Duplicate(IntPtr.Zero);
                using var cpy2 = cpy.Duplicate(IntPtr.Zero);
                using var linear = ToneCurve.BuildGamma(cpy2, 1.0);
                using var profile = Profile.CreateLinearizationDeviceLink(cpy2, ColorSpaceSignature.GrayData, [linear]);
                using var xform = Transform.Create(cpy2, profile, Cms.TYPE_GRAY_8, profile, Cms.TYPE_GRAY_8, Intent.Perceptual, CmsFlags.None);
                byte[] In = [120, 20, 30, 40];
                byte[] Out = new byte[In.Length];

                xform.DoTransform(In, Out, In.Length);

                // Assert
                for (int i = 0; i < In.Length; i++)
                {
                    Assert.AreEqual(In[i], Out[i]);
                }
            });
        }

        [TestMethod()]
        public void PluginTransform_WhenTransforming_ShouldSucceed()
        {
            // Arrange
            PluginTransform sut = PluginTransformUtils.CreatePluginTransform();

            MemoryUtils.UsingMemoryFor(sut, (transformPlugin) =>
            {
                // Act
                using var ctx = Context.Create(transformPlugin, IntPtr.Zero);
                using var cpy = ctx.Duplicate(IntPtr.Zero);
                using var cpy2 = cpy.Duplicate(IntPtr.Zero);
                using var linear = ToneCurve.BuildGamma(cpy2, 1.0);
                using var profile = Profile.CreateLinearizationDeviceLink(cpy2, ColorSpaceSignature.GrayData, [linear]);
                using var xform = Transform.Create(cpy2, profile, Cms.TYPE_GRAY_8, profile, Cms.TYPE_GRAY_8, Intent.Perceptual, CmsFlags.None);

                byte[] In = [10, 20, 30, 40];
                byte[] Out = new byte[In.Length];

                xform.DoTransform(In, Out, In.Length);

                // Assert
                for (int i = 0; i < In.Length; i++)
                {
                    Assert.AreEqual((byte)42, Out[i]);
                }
            });
        }

        [TestMethod()]
        public void PluginMutex_WhenMutualExclusion_ShouldSucceed()
        {
            // Arrange
            PluginMutex sut = PluginMutexUtils.CreatePluginMutex();

            MemoryUtils.UsingMemoryFor(sut, (mutexPlugin) =>
            {
                // Act
                using var ctx = Context.Create(mutexPlugin, IntPtr.Zero);
                using var cpy = ctx.Duplicate(IntPtr.Zero);
                using var cpy2 = cpy.Duplicate(IntPtr.Zero);
                using var linear = ToneCurve.BuildGamma(cpy2, 1.0);
                using var profile = Profile.CreateLinearizationDeviceLink(cpy2, ColorSpaceSignature.GrayData, [linear]);
                using var xform = Transform.Create(cpy2, profile, Cms.TYPE_GRAY_8, profile, Cms.TYPE_GRAY_8, Intent.Perceptual, CmsFlags.None);
                byte[] In = [10, 20, 30, 40];
                byte[] Out = new byte[In.Length];

                xform.DoTransform(In, Out, In.Length);

                // Assert
                for (int i = 0; i < In.Length; i++)
                {
                    Assert.AreEqual(In[i], Out[i]);
                }
            });
        }
    }
}
