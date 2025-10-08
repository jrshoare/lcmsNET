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
using System.Runtime.InteropServices;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class TransformTest
    {
        [TestMethod()]
        public void Create_WhenWithoutContext_ShouldHaveValidHandle()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.Lab.icc", "lab.icc", (labPath) =>
                {
                    using var srgb = Profile.Open(srgbPath, "r");
                    using var lab = Profile.Open(labPath, "r");

                    // Act
                    using var sut = Transform.Create(srgb, Cms.TYPE_RGB_8, lab,
                                Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None);

                    // Assert
                    Assert.IsFalse(sut.IsInvalid);
                })
            );
        }

        [TestMethod()]
        public void Create_WhenWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.Lab.icc", "lab.icc", (labPath) =>
                {
                    using var srgb = Profile.Open(srgbPath, "r");
                    using var lab = Profile.Open(labPath, "r");
                    using var context = ContextUtils.CreateContext();

                    // Act
                    using var sut = Transform.Create(context, srgb, Cms.TYPE_RGB_8, lab,
                                Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None);

                    // Assert
                    Assert.IsFalse(sut.IsInvalid);
                })
            );
        }

        [TestMethod()]
        public void Create_WhenForProofingWithoutContext_ShouldHaveValidHandle()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.Lab.icc", "lab.icc", (labPath) =>
                    ResourceUtils.SaveTemporarily(".Resources.D50_XYZ.icc", "D50_XYZ.icc", (xyzPath) =>
                    {
                        using var srgb = Profile.Open(srgbPath, "r");
                        using var lab = Profile.Open(labPath, "r");
                        using var proofing = Profile.Open(xyzPath, "r");

                        // Act
                        using var sut = Transform.Create(srgb, Cms.TYPE_RGB_8, lab,
                                    Cms.TYPE_Lab_8, proofing, Intent.Perceptual, Intent.AbsoluteColorimetric, CmsFlags.None);

                        // Assert
                        Assert.IsFalse(sut.IsInvalid);
                    })
                )
            );
        }

        [TestMethod()]
        public void Create_WhenForProofingWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.Lab.icc", "lab.icc", (labPath) =>
                    ResourceUtils.SaveTemporarily(".Resources.D50_XYZ.icc", "D50_XYZ.icc", (xyzPath) =>
                    {
                        using var srgb = Profile.Open(srgbPath, "r");
                        using var lab = Profile.Open(labPath, "r");
                        using var proofing = Profile.Open(xyzPath, "r");
                        using var context = ContextUtils.CreateContext();

                        // Act
                        using var sut = Transform.Create(context, srgb, Cms.TYPE_RGB_8, lab,
                                    Cms.TYPE_Lab_8, proofing, Intent.Perceptual, Intent.AbsoluteColorimetric, CmsFlags.None);

                        // Assert
                        Assert.IsFalse(sut.IsInvalid);
                    })
                )
            );
        }

        [TestMethod()]
        public void Create_WhenMultipleProfilesWithoutContext_ShouldHaveValidHandle()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                // Act
                Profile[] profiles = new Profile[1];
                using (profiles[0] = Profile.Open(srgbPath, "r"))
                using (var sut = Transform.Create(profiles, Cms.TYPE_RGB_8, Cms.TYPE_XYZ_16,
                            Intent.RelativeColorimetric, CmsFlags.None))
                {
                    // Assert
                    Assert.IsFalse(sut.IsInvalid);
                }
            });
        }

        [TestMethod()]
        public void Create_WhenMultipleProfilesWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                // Act
                Profile[] profiles = new Profile[1];
                using var context = ContextUtils.CreateContext();

                using (profiles[0] = Profile.Open(srgbPath, "r"))
                using (var sut = Transform.Create(context, profiles, Cms.TYPE_RGB_8, Cms.TYPE_XYZ_16,
                            Intent.RelativeColorimetric, CmsFlags.None))
                {
                    // Assert
                    Assert.IsFalse(sut.IsInvalid);
                }
            });
        }

        [TestMethod()]
        public void Create_WhenExtended_ShouldHaveValidHandle()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                // Act
                Profile[] profiles = new Profile[1];
                using var context = ContextUtils.CreateContext();

                using (profiles[0] = Profile.Open(srgbPath, "r"))
                using (var sut = Transform.Create(context, profiles, bpc: [true], intents: [Intent.RelativeColorimetric],
                        adaptationStates: [1.0], gamut: null, gamutPCSPosition: 0, Cms.TYPE_RGB_8, Cms.TYPE_XYZ_16, CmsFlags.None))
                {
                    // Assert
                    Assert.IsFalse(sut.IsInvalid);
                }
            });
        }

        [TestMethod()]
        public void DoTransform_WhenSrgbToXLab_ShouldSucceed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.Lab.icc", "lab.icc", (labPath) =>
                {
                    using var srgb = Profile.Open(srgbPath, "r");
                    using var lab = Profile.Open(labPath, "r");
                    using var sut = Transform.Create(srgb, Cms.TYPE_RGB_8, lab,
                                Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None);

                    byte[] inputBuffer = [255, 147, 226];
                    byte[] outputBuffer = new byte[3];

                    // Act
                    sut.DoTransform(inputBuffer, outputBuffer, 1);
                })
            );
        }

        [TestMethod()]
        public void DoTransform_Span_WhenSrgbToXLab_ShouldSucceed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.Lab.icc", "lab.icc", (labPath) =>
                {
                    using var srgb = Profile.Open(srgbPath, "r");
                    using var lab = Profile.Open(labPath, "r");
                    using var sut = Transform.Create(srgb, Cms.TYPE_RGB_8, lab,
                                Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None);

                    byte[] inputBuffer = [255, 147, 226];
                    byte[] outputBuffer = new byte[3];

                    // Act
                    sut.DoTransform(inputBuffer.AsSpan(), outputBuffer.AsSpan(), 1);
                })
            );
        }

        [TestMethod()]
        public void DoTransform_WhenSrgbToXyz_ShouldSucceed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.D50_XYZ.icc", "xyz.icc", (xyzPath) =>
                {
                    using var srgb = Profile.Open(srgbPath, "r");
                    using var xyz = Profile.Open(xyzPath, "r");
                    using var sut = Transform.Create(srgb, Cms.TYPE_RGB_8, xyz,
                                Cms.TYPE_RGB_8, Intent.Perceptual, CmsFlags.None);

                    byte[] inputBuffer = [255, 255, 255];
                    byte[] outputBuffer = new byte[3];

                    // Act
                    sut.DoTransform(inputBuffer, outputBuffer, 1);
                })
            );
        }

        [TestMethod()]
        public void DoTransform_Span_WhenSrgbToXyz_ShouldSucceed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.D50_XYZ.icc", "xyz.icc", (xyzPath) =>
                {
                    using var srgb = Profile.Open(srgbPath, "r");
                    using var xyz = Profile.Open(xyzPath, "r");
                    using var sut = Transform.Create(srgb, Cms.TYPE_RGB_8, xyz,
                                Cms.TYPE_RGB_8, Intent.Perceptual, CmsFlags.None);

                    byte[] inputBuffer = [255, 255, 255];
                    byte[] outputBuffer = new byte[3];

                    // Act
                    sut.DoTransform(inputBuffer.AsSpan(), outputBuffer.AsSpan(), 1);
                })
            );
        }

        [TestMethod()]
        public void DoTransform_WhenLineStride_ShouldSucceed()
        {
            try
            {
                // Arrange
                ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                    ResourceUtils.SaveTemporarily(".Resources.D50_XYZ.icc", "xyz.icc", (xyzPath) =>
                    {
                        using var srgb = Profile.Open(srgbPath, "r");
                        using var xyz = Profile.Open(xyzPath, "r");
                        using var sut = Transform.Create(srgb, Cms.TYPE_RGB_8, xyz,
                                    Cms.TYPE_RGB_8, Intent.Perceptual, CmsFlags.None);

                        const int pixelsPerLine = 3;
                        const int lineCount = 1;
                        const int bytesPerLineIn = pixelsPerLine * 3 + 4;
                        const int bytesPerLineOut = pixelsPerLine * 3 + 2;
                        byte[] inputBuffer = new byte[bytesPerLineIn * lineCount]; // uninitialised is ok
                        byte[] outputBuffer = new byte[bytesPerLineOut * lineCount];

                        // Act
                        sut.DoTransform(inputBuffer, outputBuffer, pixelsPerLine, lineCount,
                                bytesPerLineIn, bytesPerLineOut, bytesPerPlaneIn: 0, bytesPerPlaneOut: 0);    // >= 2.8
                    })
                );
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.8 or later.");
            }
        }

        [TestMethod()]
        public void DoTransform_Span_WhenLineStride_ShouldSucceed()
        {
            try
            {
                // Arrange
                ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                    ResourceUtils.SaveTemporarily(".Resources.D50_XYZ.icc", "xyz.icc", (xyzPath) =>
                    {
                        using var srgb = Profile.Open(srgbPath, "r");
                        using var xyz = Profile.Open(xyzPath, "r");
                        using var sut = Transform.Create(srgb, Cms.TYPE_RGB_8, xyz,
                                    Cms.TYPE_RGB_8, Intent.Perceptual, CmsFlags.None);

                        const int pixelsPerLine = 3;
                        const int lineCount = 1;
                        const int bytesPerLineIn = pixelsPerLine * 3 + 4;
                        const int bytesPerLineOut = pixelsPerLine * 3 + 2;
                        byte[] inputBuffer = new byte[bytesPerLineIn * lineCount]; // uninitialised is ok
                        byte[] outputBuffer = new byte[bytesPerLineOut * lineCount];

                        // Act
                        sut.DoTransform(inputBuffer.AsSpan(), outputBuffer.AsSpan(), pixelsPerLine, lineCount,
                                bytesPerLineIn, bytesPerLineOut, bytesPerPlaneIn: 0, bytesPerPlaneOut: 0);    // >= 2.8
                    })
                );
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.8 or later.");
            }
        }

        [TestMethod()]
        public void DoTransform_WhenMultipleProfiles_ShouldSucceed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                Profile[] profiles = new Profile[1];
                using (profiles[0] = Profile.Open(srgbPath, "r"))
                using (var sut = Transform.Create(profiles, Cms.TYPE_RGB_8, Cms.TYPE_XYZ_16,
                        Intent.RelativeColorimetric, CmsFlags.None))
                {
                    byte[] inputBuffer = [255, 255, 255];
                    byte[] outputBuffer = new byte[6];

                    // Act
                    sut.DoTransform(inputBuffer, outputBuffer, 1);
                }
            });
        }

        [TestMethod()]
        public void Context_WhenCreatedWithContext_ShouldReturnValuePassedToCreate()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.Lab.icc", "lab.icc", (labPath) =>
                {
                    using var srgb = Profile.Open(srgbPath, "r");
                    using var lab = Profile.Open(labPath, "r");
                    using var expected = ContextUtils.CreateContext();
                    using var sut = Transform.Create(expected, srgb, Cms.TYPE_RGB_8, lab,
                                Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None);

                    // Act
                    var actual = sut.Context;

                    // Assert
                    Assert.AreSame(expected, actual);
                })
            );
        }

        [TestMethod()]
        public void Context_WhenCreatedMultipleWithContext_ShouldReturnValuePassedToCreate()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                Profile[] profiles = new Profile[1];

                using var expected = ContextUtils.CreateContext();
                using (profiles[0] = Profile.Open(srgbPath, "r"))
                using (var sut = Transform.Create(expected, profiles, Cms.TYPE_RGB_8, Cms.TYPE_XYZ_16,
                            Intent.RelativeColorimetric, CmsFlags.None))
                {
                    // Act
                    var actual = sut.Context;

                    // Assert
                    Assert.AreSame(expected, actual);
                }
            });
        }

        [TestMethod()]
        public void Context_WhenCreatedForProofingWithContext_ShouldReturnValuePassedToCreate()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.Lab.icc", "lab.icc", (labPath) =>
                    ResourceUtils.SaveTemporarily(".Resources.D50_XYZ.icc", "D50_XYZ.icc", (xyzPath) =>
                    {
                        using var srgb = Profile.Open(srgbPath, "r");
                        using var lab = Profile.Open(labPath, "r");
                        using var proofing = Profile.Open(xyzPath, "r");

                        using var expected = ContextUtils.CreateContext();
                        using var sut = Transform.Create(expected, srgb, Cms.TYPE_RGB_8, lab,
                                    Cms.TYPE_Lab_8, proofing, Intent.Perceptual, Intent.AbsoluteColorimetric, CmsFlags.None);

                        // Act
                        var actual = sut.Context;

                        // Assert
                        Assert.AreSame(expected, actual);
                    })
                )
            );
        }

        [TestMethod()]
        public void InputFormat_WhenInvoked_ShouldReturnValuePassedToCreate()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.Lab.icc", "lab.icc", (labPath) =>
                {
                    uint expected = Cms.TYPE_RGB_8;

                    using var srgb = Profile.Open(srgbPath, "r");
                    using var lab = Profile.Open(labPath, "r");
                    using var sut = Transform.Create(srgb, inputFormat: expected, lab,
                                Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None);

                    // Act
                    uint actual = sut.InputFormat;

                    // Assert
                    Assert.AreEqual(expected, actual);
                })
            );
        }

        [TestMethod()]
        public void OutputFormat_WhenInvoked_ShouldReturnValuePassedToCreate()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.Lab.icc", "lab.icc", (labPath) =>
                {
                    uint expected = Cms.TYPE_Lab_8;

                    using var srgb = Profile.Open(srgbPath, "r");
                    using var lab = Profile.Open(labPath, "r");
                    using var sut = Transform.Create(srgb, Cms.TYPE_RGB_8, lab,
                                outputFormat: expected, Intent.Perceptual, CmsFlags.None);

                    // Act
                    uint actual = sut.OutputFormat;

                    // Assert
                    Assert.AreEqual(expected, actual);
                })
            );
        }

        [TestMethod()]
        public void ChangeBuffersFormat_WhenInvokedFor16BitPrecision_ShouldChangeBufferEncoding()
        {
            // Arrange
            using var profile = Profile.Create_sRGB();
            using var transform = Transform.Create(profile, Cms.TYPE_RGB_16, profile, Cms.TYPE_RGB_16, Intent.Perceptual, CmsFlags.None);
            profile.Dispose();

            // Act
            var changed = transform.ChangeBuffersFormat(Cms.TYPE_BGR_16, Cms.TYPE_RGB_16);

            // Assert
            Assert.IsTrue(changed);
        }

        [TestMethod()]
        public void NamedColorList_WhenNamedColors_ShouldGetNamedColorList()
        {
            // TODO:
            // Need to understand how to create a transform where the cmsStage type
            // is cmsSigNamedColorElemType. See 'cmsGetNamedColorList' in cmsnamed.c.
        }

        [TestMethod()]
        public void UserData_WhenInvoked_ShouldReturnPointerToUserData()
        {
            // Arrange
            IntPtr expected = Marshal.AllocHGlobal(99);

            try
            {
                using var context = Context.Create(plugin: IntPtr.Zero, userData: expected);
                using var profile = Profile.Create_sRGB(context);
                using var transform = Transform.Create(context, profile, Cms.TYPE_RGB_16, profile, Cms.TYPE_RGB_16, Intent.Perceptual, CmsFlags.None);
                profile.Dispose();
                transform.SetUserData(expected, FreeUserData);

                // Act
                var actual = transform.UserData;

                // Assert
                Assert.AreEqual(expected, actual);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.4 or later.");
            }

            static void FreeUserData(IntPtr contextID, IntPtr userData)
            {
                Marshal.FreeHGlobal(userData);
            }
        }

        [TestMethod()]
        public void Flags_WhenInvoked_ShouldReturnFlagsInEffectForTransform()
        {
            try
            {
                // Arrange
                ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                    ResourceUtils.SaveTemporarily(".Resources.Lab.icc", "lab.icc", (labPath) =>
                    {
                        using var srgb = Profile.Open(srgbPath, "r");
                        using var lab = Profile.Open(labPath, "r");
                        using var sut = Transform.Create(srgb, Cms.TYPE_RGB_8, lab,
                                    Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.NoOptimize | CmsFlags.BlackPointCompensation);

                        // Act
                        CmsFlags actual = sut.Flags;

                        // Assert
                        // transform creation may add or remove flags so do not assert equality with values used to create
                    })
                );
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.12 or later.");
            }
        }
    }
}
