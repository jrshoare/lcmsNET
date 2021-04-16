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

using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    /// <summary>
    /// Defines a delegate that can be registered using <see cref="Cms.SetErrorHandler(ErrorHandler)"/>
    /// or <see cref="Context.SetErrorHandler(ErrorHandler)"/> to receive errors from Little CMS.
    /// </summary>
    /// <param name="contextID">The handle to the <see cref="Context"/> from which the error is reported.</param>
    /// <param name="errorCode">The error code.</param>
    /// <param name="errorText">An english string containing the error text.</param>
    /// <remarks>
    /// A <see cref="Context"/> value of <see cref="IntPtr.Zero"/> identifies the global context.
    /// </remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ErrorHandler(IntPtr contextID, [MarshalAs(UnmanagedType.U4)] int errorCode, [MarshalAs(UnmanagedType.LPStr)] string errorText);

    /// <summary>
    /// Defines ICC and non-ICC intents.
    /// </summary>
    public enum Intent : uint
    {
        #region ICC intents
        /// <summary>
        /// Perceptual.
        /// </summary>
        Perceptual = 0,
        /// <summary>
        /// Relative colorimetric.
        /// </summary>
        RelativeColorimetric = 1,
        /// <summary>
        /// Saturation.
        /// </summary>
        Saturation = 2,
        /// <summary>
        /// Absolute colorimetric.
        /// </summary>
        AbsoluteColorimetric = 3,
        #endregion

        #region Non-ICC intents
        /// <summary>
        /// Preserve K only perceptual.
        /// </summary>
        PreserveKOnlyPerceptual = 10,
        /// <summary>
        /// Preserve K only relative colorimetric.
        /// </summary>
        PreserveKOnlyRelativeColorimetric = 11,
        /// <summary>
        /// Preserve K only saturation.
        /// </summary>
        PreserveKOnlySaturation = 12,
        /// <summary>
        /// Preserve K plane perceptual.
        /// </summary>
        PreserveKPlanePerceptual = 13,
        /// <summary>
        /// Preserve K plane relative colorimetric.
        /// </summary>
        PreserveKPlaneRelativeColorimetric = 14,
        /// <summary>
        /// Preserve K plane saturation.
        /// </summary>
        PreserveKPlaneSaturation = 15
        #endregion
    }

    /// <summary>
    /// Defines flags to command the whole process.
    /// </summary>
    /// <remarks>
    /// To fine-tune control over the number of grid points, values can be
    /// logically ORed with (((n) &amp; 0xFF) &lt;&lt; 16).
    /// </remarks>
    [Flags]
    public enum CmsFlags : uint
    {
        #region General
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// Inhibit 1-pixel cache.
        /// </summary>
        NoCache = 0x0040,
        /// <summary>
        /// Inhibit optimizations.
        /// </summary>
        NoOptimize = 0x0100,
        /// <summary>
        /// Don't transform anyway.
        /// </summary>
        NullTransform = 0x0200,
        #endregion

        #region Proofing flags
        /// <summary>
        /// Out of gamut alarm.
        /// </summary>
        GamutCheck = 0x1000,
        /// <summary>
        /// Do softproofing.
        /// </summary>
        SoftProofing = 0x4000,
        #endregion

        #region Miscellaneous flags
        /// <summary>
        /// Black point compensation.
        /// </summary>
        BlackPointCompensation = 0x2000,
        /// <summary>
        /// Don't fix scum dot.
        /// </summary>
        NoWhiteOnWhiteFixUp = 0x0004,
        /// <summary>
        /// Use more memory to give better accuracy. Use on linear XYZ.
        /// </summary>
        HighResPreCalc = 0x0400,
        /// <summary>
        /// Use less memory to minimize resources.
        /// </summary>
        LowResPreCalc = 0x0800,
        #endregion

        #region For device link creation
        /// <summary>
        /// Create 8 bit device links.
        /// </summary>
        EightBitsDeviceLink = 0x0008,
        /// <summary>
        /// Guess device class (for transform2devicelink).
        /// </summary>
        GuessDeviceClass = 0x0020,
        /// <summary>
        /// Keep profile sequence for device link creation.
        /// </summary>
        KeepSequence = 0x0080,
        #endregion

        #region Specific to a particular optimization
        /// <summary>
        /// Force CLUT optimization.
        /// </summary>
        ForceCLut = 0x0002,
        /// <summary>
        /// Create post-linearization tables if possible.
        /// </summary>
        CLutPostLinearization = 0x0001,
        /// <summary>
        /// Create pre-linearization tables if possible.
        /// </summary>
        CLutPreLinearization = 0x0010,
        #endregion

        #region Unbounded mode control
        /// <summary>
        /// Prevent negative numbers in floating point transforms.
        /// </summary>
        NoNegatives = 0x8000,
        #endregion

        #region CRD special
        /// <summary>
        /// No default resource definition.
        /// </summary>
        NoDefaultResourceDef = 0x01000000
        #endregion
    }

    /// <summary>
    /// Defines the pixel types.
    /// </summary>
    [Flags]
    public enum PixelType : uint
    {
        /// <summary>
        /// Don't check color space.
        /// </summary>
        Any = 0,
        // Enumeration values 1 & 2 are reserved
        /// <summary>
        /// Grayscale.
        /// </summary>
        Gray = 3,
        /// <summary>
        /// Red Green Blue.
        /// </summary>
        RGB = 4,
        /// <summary>
        /// Cyan Magenta Yellow.
        /// </summary>
        CMY = 5,
        /// <summary>
        /// Cyan Magenta Yellow blacK.
        /// </summary>
        CMYK = 6,
        /// <summary>
        /// Y Cb Cr.
        /// </summary>
        YCbCr = 7,
        /// <summary>
        /// L u'v'.
        /// </summary>
        YUV = 8,
        /// <summary>
        /// CIE XYZ.
        /// </summary>
        XYZ = 9,
        /// <summary>
        /// CIE L*a*b.
        /// </summary>
        Lab = 10,
        /// <summary>
        /// L u'v'K.
        /// </summary>
        YUVK = 11,
        /// <summary>
        /// H S V.
        /// </summary>
        HSV = 12,
        /// <summary>
        /// H L S.
        /// </summary>
        HLS = 13,
        /// <summary>
        /// Y x y.
        /// </summary>
        Yxy = 14,
        /// <summary>
        /// 1 unspecified channel.
        /// </summary>
        MCH1 = 15,
        /// <summary>
        /// 2 unspecified channels.
        /// </summary>
        MCH2 = 16,
        /// <summary>
        /// 3 unspecified channels.
        /// </summary>
        MCH3 = 17,
        /// <summary>
        /// 4 unspecified channels.
        /// </summary>
        MCH4 = 18,
        /// <summary>
        /// 5 unspecified channels.
        /// </summary>
        MCH5 = 19,
        /// <summary>
        /// 6 unspecified channels.
        /// </summary>
        MCH6 = 20,
        /// <summary>
        /// 7 unspecified channels.
        /// </summary>
        MCH7 = 21,
        /// <summary>
        /// 8 unspecified channels.
        /// </summary>
        MCH8 = 22,
        /// <summary>
        /// 9 unspecified channels.
        /// </summary>
        MCH9 = 23,
        /// <summary>
        /// 10 unspecified channels.
        /// </summary>
        MCH10 = 24,
        /// <summary>
        /// 11 unspecified channels.
        /// </summary>
        MCH11 = 25,
        /// <summary>
        /// 12 unspecified channels.
        /// </summary>
        MCH12 = 26,
        /// <summary>
        /// 13 unspecified channels.
        /// </summary>
        MCH13 = 27,
        /// <summary>
        /// 14 unspecified channels.
        /// </summary>
        MCH14 = 28,
        /// <summary>
        /// 15 unspecified channels.
        /// </summary>
        MCH15 = 29,
        /// <summary>
        /// Identical to <see cref="Lab"/> but using the V2 old encoding.
        /// </summary>
        LabV2 = 30
    }

    /// <summary>
    /// Defines the base ICC tag definitions.
    /// </summary>
    public enum TagSignature : uint
    {
        /// <summary>
        /// 'A2B0' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        AToB0 = 0x41324230,
        /// <summary>
        /// 'A2B1' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        AToB1 = 0x41324231,
        /// <summary>
        /// 'A2B2' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        AToB2 = 0x41324232,
        /// <summary>
        /// 'bXYZ' - read/write using <see cref="CIEXYZ"/>.
        /// </summary>
        BlueColorant = 0x6258595A,
        /// <summary>
        /// 'bXYZ' - read/write using <see cref="CIEXYZ"/>.
        /// </summary>
        BlueMatrixColumn = 0x6258595A,
        /// <summary>
        /// 'bTRC' - read/write using <see cref="ToneCurve"/>.
        /// </summary>
        BlueTRC = 0x62545243,
        /// <summary>
        /// 'B2A0' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        BToA0 = 0x42324130,
        /// <summary>
        /// 'B2A1' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        BToA1 = 0x42324131,
        /// <summary>
        /// 'B2A2' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        BToA2 = 0x42324132,
        /// <summary>
        /// 'calt' - read/write using <see cref="Tm"/>.
        /// </summary>
        CalibrationDateTime = 0x63616C74,
        /// <summary>
        /// 'targ' - read/write using <see cref="MultiLocalizedUnicode"/>.
        /// </summary>
        CharTarget = 0x74617267,
        /// <summary>
        /// 'chad' - read/write using <see cref="CIEXYZTRIPLE"/>.
        /// </summary>
        ChromaticAdaptation = 0x63686164,
        /// <summary>
        /// 'chrm' - read/write using <see cref="CIExyYTRIPLE"/>.
        /// </summary>
        Chromaticity = 0x6368726D,
        /// <summary>
        /// 'clro' - read/write using <see cref="lcmsNET.ColorantOrder"/>.
        /// </summary>
        ColorantOrder = 0x636C726F,
        /// <summary>
        /// 'clrt' - read/write using <see cref="NamedColorList"/>.
        /// </summary>
        ColorantTable = 0x636C7274,
        /// <summary>
        /// 'clot' - read/write using <see cref="NamedColorList"/>.
        /// </summary>
        ColorantTableOut = 0x636C6F74,
        /// <summary>
        /// 'ciis' - read/write using <see cref="Signature"/>.
        /// </summary>
        ColorimetricIntentImageState = 0x63696973,
        /// <summary>
        /// 'cprt' - read/write using <see cref="MultiLocalizedUnicode"/>.
        /// </summary>
        Copyright = 0x63707274,
        /// <summary>
        /// 'crdi' - read/write using <see cref="NamedColorList"/>.
        /// </summary>
        CrdInfo = 0x63726469,
        /// <summary>
        /// 'data' - Not supported.
        /// </summary>
        Data = 0x64617461,
        /// <summary>
        /// 'dtim' - read/write using <see cref="Tm"/>.
        /// </summary>
        DateTime = 0x6474696D,
        /// <summary>
        /// 'dmnd' - read/write using <see cref="MultiLocalizedUnicode"/>.
        /// </summary>
        DeviceMfgDesc = 0x646D6E64,
        /// <summary>
        /// 'dmdd' - read/write using <see cref="MultiLocalizedUnicode"/>.
        /// </summary>
        DeviceModelDesc = 0x646D6464,
        /// <summary>
        /// 'devs' - Not supported.
        /// </summary>
        DeviceSettings = 0x64657673,
        /// <summary>
        /// 'D2B0' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        DToB0 = 0x44324230,
        /// <summary>
        /// 'D2B1' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        DToB1 = 0x44324231,
        /// <summary>
        /// 'D2B2' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        DToB2 = 0x44324232,
        /// <summary>
        /// 'D2B3' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        DToB3 = 0x44324233,
        /// <summary>
        /// 'B2D0' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        BToD0 = 0x42324430,
        /// <summary>
        /// 'B2D1' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        BToD1 = 0x42324431,
        /// <summary>
        /// 'B2D2' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        BToD2 = 0x42324432,
        /// <summary>
        /// 'B2D3' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        BToD3 = 0x42324433,
        /// <summary>
        /// 'gamt' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        Gamut = 0x67616D74,
        /// <summary>
        /// 'kTRC' - read/write using <see cref="ToneCurve"/>.
        /// </summary>
        GrayTRC = 0x6b545243,
        /// <summary>
        /// 'gXYZ' - read/write using <see cref="CIEXYZ"/>.
        /// </summary>
        GreenColorant = 0x6758595A,
        /// <summary>
        /// 'gXYZ' - read/write using <see cref="CIEXYZ"/>.
        /// </summary>
        GreenMatrixColumn = 0x6758595A,
        /// <summary>
        /// 'gTRC' - read/write using <see cref="ToneCurve"/>.
        /// </summary>
        GreenTRC = 0x67545243,
        /// <summary>
        /// 'lumi' - read/write using <see cref="CIEXYZ"/>.
        /// </summary>
        Luminance = 0x6C756d69,
        /// <summary>
        /// 'meas' - read/write using <see cref="ICCMeasurementConditions"/>.
        /// </summary>
        Measurement = 0x6D656173,
        /// <summary>
        /// 'bkpt' - read/write using <see cref="CIEXYZ"/>.
        /// </summary>
        MediaBlackPoint = 0x626B7074,
        /// <summary>
        /// 'wtpt' - read/write using <see cref="CIEXYZ"/>.
        /// </summary>
        MediaWhitePoint = 0x77747074,
        /// <summary>
        /// 'ncol' - Not supported.
        /// </summary>
        NamedColor = 0x6E636f6C,
        /// <summary>
        /// 'ncl2' - read/write using <see cref="NamedColorList"/>.
        /// </summary>
        NamedColor2 = 0x6E636C32,
        /// <summary>
        /// 'resp' - Not supported.
        /// </summary>
        OutputResponse = 0x72657370,
        /// <summary>
        /// 'rig0' - read/write using <see cref="Signature"/>.
        /// </summary>
        PerceptualRenderingIntentGamut = 0x72696730,
        /// <summary>
        /// 'pre0' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        Preview0 = 0x70726530,
        /// <summary>
        /// 'pre1' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        Preview1 = 0x70726531,
        /// <summary>
        /// 'pre2' - read/write using <see cref="Pipeline"/>.
        /// </summary>
        Preview2 = 0x70726532,
        /// <summary>
        /// 'desc' - read/write using <see cref="MultiLocalizedUnicode"/>.
        /// </summary>
        ProfileDescription = 0x64657363,
        /// <summary>
        /// 'dscm' - read/write using <see cref="MultiLocalizedUnicode"/>.
        /// </summary>
        ProfileDescriptionML = 0x6473636d,
        /// <summary>
        /// 'pseq' - read/write using <see cref="ProfileSequenceDescriptor"/>.
        /// </summary>
        ProfileSequenceDesc = 0x70736571,
        /// <summary>
        /// 'psid' - read/write using <see cref="ProfileSequenceDescriptor"/>.
        /// </summary>
        ProfileSequenceId = 0x70736964,
        /// <summary>
        /// 'psd0' - read/write using <see cref="ICCData"/>.
        /// </summary>
        Ps2CRD0 = 0x70736430,
        /// <summary>
        /// 'psd1' - read/write using <see cref="ICCData"/>.
        /// </summary>
        Ps2CRD1 = 0x70736431,
        /// <summary>
        /// 'psd2' - read/write using <see cref="ICCData"/>.
        /// </summary>
        Ps2CRD2 = 0x70736432,
        /// <summary>
        /// 'psd3' - read/write using <see cref="ICCData"/>.
        /// </summary>
        Ps2CRD3 = 0x70736433,
        /// <summary>
        /// 'ps2s' - read/write using <see cref="ICCData"/>.
        /// </summary>
        Ps2CSA = 0x70733273,
        /// <summary>
        /// 'ps2i' - read/write using <see cref="ICCData"/>.
        /// </summary>
        Ps2RenderingIntent = 0x70733269,
        /// <summary>
        /// 'rXYZ' - read/write using <see cref="CIEXYZ"/>.
        /// </summary>
        RedColorant = 0x7258595A,
        /// <summary>
        /// 'rXYZ' - read/write using <see cref="CIEXYZ"/>.
        /// </summary>
        RedMatrixColumn = 0x7258595A,
        /// <summary>
        /// 'rTRC' - read/write using <see cref="ToneCurve"/>.
        /// </summary>
        RedTRC = 0x72545243,
        /// <summary>
        /// 'rig2' - read/write using <see cref="Signature"/>.
        /// </summary>
        SaturationRenderingIntentGamut = 0x72696732,
        /// <summary>
        /// 'scrd' - read/write using <see cref="MultiLocalizedUnicode"/>.
        /// </summary>
        ScreeningDesc = 0x73637264,
        /// <summary>
        /// 'scrn' - read/write using <see cref="lcmsNET.Screening"/>.
        /// </summary>
        Screening = 0x7363726E,
        /// <summary>
        /// 'tech' - read/write using <see cref="Signature"/>.
        /// </summary>
        Technology = 0x74656368,
        /// <summary>
        /// 'bfd ' - read/write using <see cref="lcmsNET.UcrBg"/>.
        /// </summary>
        UcrBg = 0x62666420,
        /// <summary>
        /// 'vued' - read/write using <see cref="MultiLocalizedUnicode"/>.
        /// </summary>
        ViewingCondDesc = 0x76756564,
        /// <summary>
        /// 'view' - read/write using <see cref="ICCViewingConditions"/>.
        /// </summary>
        ViewingConditions = 0x76696577,
        /// <summary>
        /// 'vcgt' - read/write using <see cref="lcmsNET.VideoCardGamma"/>.
        /// </summary>
        Vcgt = 0x76636774,
        /// <summary>
        /// 'meta' - read/write using <see cref="Dict"/>.
        /// </summary>
        Meta = 0x6D657461,
        /// <summary>
        /// 'arts' - read/write using <see cref="CIEXYZTRIPLE"/>.
        /// </summary>
        ArgyllArts = 0x61727473
    }

    /// <summary>
    /// Defines the base ICC type definitions.
    /// </summary>
    public enum TagTypeSignature : uint
    {
        /// <summary>
        /// 'chrm'
        /// </summary>
        Chromaticity = 0x6368726D,
        /// <summary>
        /// 'clro'
        /// </summary>
        ColorantOrder = 0x636C726F,
        /// <summary>
        /// 'clrt'
        /// </summary>
        ColorantTable = 0x636C7274,
        /// <summary>
        /// 'crdi'
        /// </summary>
        CrdInfo = 0x63726469,
        /// <summary>
        /// 'curv'
        /// </summary>
        Curve = 0x63757276,
        /// <summary>
        /// 'data'
        /// </summary>
        Data = 0x64617461,
        /// <summary>
        /// 'dict'
        /// </summary>
        Dict = 0x64696374,
        /// <summary>
        /// 'dtim'
        /// </summary>
        DateTime = 0x6474696D,
        /// <summary>
        /// 'devs'
        /// </summary>
        DeviceSettings = 0x64657673,
        /// <summary>
        /// 'mft2'
        /// </summary>
        Lut16 = 0x6d667432,
        /// <summary>
        /// 'mft1'
        /// </summary>
        Lut8 = 0x6d667431,
        /// <summary>
        /// 'mAB '
        /// </summary>
        LutAtoB = 0x6d414220,
        /// <summary>
        /// 'mBA '
        /// </summary>
        LutBtoA = 0x6d424120,
        /// <summary>
        /// 'meas'
        /// </summary>
        Measurement = 0x6D656173,
        /// <summary>
        /// 'mluc'
        /// </summary>
        MultiLocalizedUnicode = 0x6D6C7563,
        /// <summary>
        /// 'mpet'
        /// </summary>
        MultiProcessElement = 0x6D706574,
        /// <summary>
        /// 'ncol' -- DEPRECATED!
        /// </summary>
        NamedColor = 0x6E636f6C,
        /// <summary>
        /// 'ncl2'
        /// </summary>
        NamedColor2 = 0x6E636C32,
        /// <summary>
        /// 'para'
        /// </summary>
        ParametricCurve = 0x70617261,
        /// <summary>
        /// 'pseq'
        /// </summary>
        ProfileSequenceDesc = 0x70736571,
        /// <summary>
        /// 'psid'
        /// </summary>
        ProfileSequenceId = 0x70736964,
        /// <summary>
        /// 'rcs2'
        /// </summary>
        ResponseCurveSet16 = 0x72637332,
        /// <summary>
        /// 'sf32'
        /// </summary>
        S15Fixed16Array = 0x73663332,
        /// <summary>
        /// 'scrn'
        /// </summary>
        Screening = 0x7363726E,
        /// <summary>
        /// 'sig '
        /// </summary>
        Signature = 0x73696720,
        /// <summary>
        /// 'text'
        /// </summary>
        Text = 0x74657874,
        /// <summary>
        /// 'desc'
        /// </summary>
        TextDescription = 0x64657363,
        /// <summary>
        /// 'uf32'
        /// </summary>
        U16Fixed16Array = 0x75663332,
        /// <summary>
        /// 'bfd '
        /// </summary>
        UcrBg = 0x62666420,
        /// <summary>
        /// 'ui16'
        /// </summary>
        UInt16Array = 0x75693136,
        /// <summary>
        /// 'ui32'
        /// </summary>
        UInt32Array = 0x75693332,
        /// <summary>
        /// 'ui64'
        /// </summary>
        UInt64Array = 0x75693634,
        /// <summary>
        /// 'ui08'
        /// </summary>
        UInt8Array = 0x75693038,
        /// <summary>
        /// 'vcgt'
        /// </summary>
        Vcgt = 0x76636774,
        /// <summary>
        /// 'view'
        /// </summary>
        ViewingConditions = 0x76696577,
        /// <summary>
        /// 'XYZ '
        /// </summary>
        XYZ = 0x58595A20
    }

    /// <summary>
    /// Defines the ICC color spaces.
    /// </summary>
    public enum ColorSpaceSignature : uint
    {
        /// <summary>
        /// 'XYZ '
        /// </summary>
        XYZData = 0x58595A20,
        /// <summary>
        /// 'Lab '
        /// </summary>
        LabData = 0x4C616220,
        /// <summary>
        /// 'Luv '
        /// </summary>
        LuvData = 0x4C757620,
        /// <summary>
        /// 'YCbr'
        /// </summary>
        YCbCrData = 0x59436272,
        /// <summary>
        /// 'Yxy '
        /// </summary>
        YxyData = 0x59787920,
        /// <summary>
        /// 'RGB '
        /// </summary>
        RgbData = 0x52474220,
        /// <summary>
        /// 'GRAY'
        /// </summary>
        GrayData = 0x47524159,
        /// <summary>
        /// 'HSV '
        /// </summary>
        HsvData = 0x48535620,
        /// <summary>
        /// 'HLS '
        /// </summary>
        HlsData = 0x484C5320,
        /// <summary>
        /// 'CMYK'
        /// </summary>
        CmykData = 0x434D594B,
        /// <summary>
        /// 'CMY '
        /// </summary>
        CmyData = 0x434D5920,
        /// <summary>
        /// 'MCH1'
        /// </summary>
        MCH1Data = 0x4D434831,
        /// <summary>
        /// 'MCH2'
        /// </summary>
        MCH2Data = 0x4D434832,
        /// <summary>
        /// 'MCH3'
        /// </summary>
        MCH3Data = 0x4D434833,
        /// <summary>
        /// 'MCH4'
        /// </summary>
        MCH4Data = 0x4D434834,
        /// <summary>
        /// 'MCH5'
        /// </summary>
        MCH5Data = 0x4D434835,
        /// <summary>
        /// 'MCH6'
        /// </summary>
        MCH6Data = 0x4D434836,
        /// <summary>
        /// 'MCH7'
        /// </summary>
        MCH7Data = 0x4D434837,
        /// <summary>
        /// 'MCH8'
        /// </summary>
        MCH8Data = 0x4D434838,
        /// <summary>
        /// 'MCH9'
        /// </summary>
        MCH9Data = 0x4D434839,
        /// <summary>
        /// 'MCHA'
        /// </summary>
        MCHAData = 0x4D434841,
        /// <summary>
        /// 'MCHB'
        /// </summary>
        MCHBData = 0x4D434842,
        /// <summary>
        /// 'MCHC'
        /// </summary>
        MCHCData = 0x4D434843,
        /// <summary>
        /// 'MCHD'
        /// </summary>
        MCHDData = 0x4D434844,
        /// <summary>
        /// 'MCHE'
        /// </summary>
        MCHEData = 0x4D434845,
        /// <summary>
        /// 'MCHF'
        /// </summary>
        MCHFData = 0x4D434846,
        /// <summary>
        /// 'nmcl'
        /// </summary>
        NamedData = 0x6e6d636c,
        /// <summary>
        /// '1CLR'
        /// </summary>
        _1colorData = 0x31434C52,
        /// <summary>
        /// '2CLR'
        /// </summary>
        _2colorData = 0x32434C52,
        /// <summary>
        /// '3CLR'
        /// </summary>
        _3colorData = 0x33434C52,
        /// <summary>
        /// '4CLR'
        /// </summary>
        _4colorData = 0x34434C52,
        /// <summary>
        /// '5CLR'
        /// </summary>
        _5colorData = 0x35434C52,
        /// <summary>
        /// '6CLR'
        /// </summary>
        _6colorData = 0x36434C52,
        /// <summary>
        /// '7CLR'
        /// </summary>
        _7colorData = 0x37434C52,
        /// <summary>
        /// '8CLR'
        /// </summary>
        _8colorData = 0x38434C52,
        /// <summary>
        /// '9CLR'
        /// </summary>
        _9colorData = 0x39434C52,
        /// <summary>
        /// 'ACLR'
        /// </summary>
        _10colorData = 0x41434C52,
        /// <summary>
        /// 'BCLR'
        /// </summary>
        _11colorData = 0x42434C52,
        /// <summary>
        /// 'CCLR'
        /// </summary>
        _12colorData = 0x43434C52,
        /// <summary>
        /// 'DCLR'
        /// </summary>
        _13colorData = 0x44434C52,
        /// <summary>
        /// 'ECLR'
        /// </summary>
        _14colorData = 0x45434C52,
        /// <summary>
        /// 'FCLR'
        /// </summary>
        _15colorData = 0x46434C52,
        /// <summary>
        /// 'LuvK'
        /// </summary>
        LuvKData = 0x4C75764B
    }

    /// <summary>
    /// Defines the ICC profile class.
    /// </summary>
    public enum ProfileClassSignature : uint
    {
        /// <summary>
        /// 'scnr'
        /// </summary>
        Input = 0x73636E72,
        /// <summary>
        /// 'mntr'
        /// </summary>
        Display = 0x6D6E7472,
        /// <summary>
        /// 'prtr'
        /// </summary>
        Output = 0x70727472,
        /// <summary>
        /// 'link'
        /// </summary>
        Link = 0x6C696E6B,
        /// <summary>
        /// 'abst'
        /// </summary>
        Abstract = 0x61627374,
        /// <summary>
        /// 'spac'
        /// </summary>
        ColorSpace = 0x73706163,
        /// <summary>
        /// 'nmcl'
        /// </summary>
        NamedColor = 0x6e6d636c
    }

    /// <summary>
    /// Defines the ICC technology tag.
    /// </summary>
    public enum TechnologySignature : uint
    {
        /// <summary>
        /// 'dcam'
        /// </summary>
        DigitalCamera = 0x6463616D,
        /// <summary>
        /// 'fscn'
        /// </summary>
        FilmScanner = 0x6673636E,
        /// <summary>
        /// 'rscn'
        /// </summary>
        ReflectiveScanner = 0x7273636E,
        /// <summary>
        /// 'ijet'
        /// </summary>
        InkJetPrinter = 0x696A6574,
        /// <summary>
        /// 'twax'
        /// </summary>
        ThermalWaxPrinter = 0x74776178,
        /// <summary>
        /// 'epho'
        /// </summary>
        ElectrophotographicPrinter = 0x6570686F,
        /// <summary>
        /// 'esta'
        /// </summary>
        ElectrostaticPrinter = 0x65737461,
        /// <summary>
        /// 'dsub'
        /// </summary>
        DyeSublimationPrinter = 0x64737562,
        /// <summary>
        /// 'rpho'
        /// </summary>
        PhotographicPaperPrinter = 0x7270686F,
        /// <summary>
        /// 'fprn'
        /// </summary>
        FilmWriter = 0x6670726E,
        /// <summary>
        /// 'vidm'
        /// </summary>
        VideoMonitor = 0x7669646D,
        /// <summary>
        /// 'vidc'
        /// </summary>
        VideoCamera = 0x76696463,
        /// <summary>
        /// 'pjtv'
        /// </summary>
        ProjectionTelevision = 0x706A7476,
        /// <summary>
        /// 'CRT '
        /// </summary>
        CRTDisplay = 0x43525420,
        /// <summary>
        /// 'PMD '
        /// </summary>
        PMDisplay = 0x504D4420,
        /// <summary>
        /// 'AMD '
        /// </summary>
        AMDisplay = 0x414D4420,
        /// <summary>
        /// 'KPCD'
        /// </summary>
        PhotoCD = 0x4B504344,
        /// <summary>
        /// 'imgs'
        /// </summary>
        PhotoImageSetter = 0x696D6773,
        /// <summary>
        /// 'grav'
        /// </summary>
        Gravure = 0x67726176,
        /// <summary>
        /// 'offs'
        /// </summary>
        OffsetLithography = 0x6F666673,
        /// <summary>
        /// 'silk'
        /// </summary>
        Silkscreen = 0x73696C6B,
        /// <summary>
        /// 'flex'
        /// </summary>
        Flexography = 0x666C6578,
        /// <summary>
        /// 'mpfs'
        /// </summary>
        MotionPictureFilmScanner = 0x6D706673,
        /// <summary>
        /// 'mpfr'
        /// </summary>
        MotionPictureFilmRecorder = 0x6D706672,
        /// <summary>
        /// 'dmpc'
        /// </summary>
        DigitalMotionPictureCamera = 0x646D7063,
        /// <summary>
        /// 'dcpj'
        /// </summary>
        DigitalCinemaProjector = 0x64636A70
    }

    /// <summary>
    /// Defines the ICC platforms.
    /// </summary>
    public enum PlatformSignature : uint
    {
        /// <summary>
        /// 'APPL'
        /// </summary>
        Macintosh = 0x4150504C,
        /// <summary>
        /// 'MSFT'
        /// </summary>
        Microsoft = 0x4D534654,
        /// <summary>
        /// 'SUNW'
        /// </summary>
        Solaris = 0x53554E57,
        /// <summary>
        /// 'SGI '
        /// </summary>
        SGI = 0x53474920,
        /// <summary>
        /// 'TGNT'
        /// </summary>
        Taligent = 0x54474E54,
        /// <summary>
        /// '*nix' - From argyll -- Not official
        /// </summary>
        Unices = 0x2A6E6978
    }

    /// <summary>
    /// Defines the multi process element types.
    /// </summary>
    public enum StageSignature : uint
    {
        /// <summary>
        /// 'cvst'
        /// </summary>
        CurveSetElemType = 0x63767374,
        /// <summary>
        /// 'matf'
        /// </summary>
        MatrixElemType = 0x6D617466,
        /// <summary>
        /// 'clut'
        /// </summary>
        CLutElemType = 0x636C7574,
        /// <summary>
        /// 'bACS'
        /// </summary>
        BAcsElemType = 0x62414353,
        /// <summary>
        /// 'eACS'
        /// </summary>
        EAcsElemType = 0x65414353,

        // Custom from here, not in the ICC Spec
        /// <summary>
        /// 'l2x '
        /// </summary>
        XYZ2LabElemType = 0x6C327820,
        /// <summary>
        /// 'x2l '
        /// </summary>
        Lab2XYZElemType = 0x78326C20,
        /// <summary>
        /// 'ncl '
        /// </summary>
        NamedColorElemType = 0x6E636C20,
        /// <summary>
        /// '2 4 '
        /// </summary>
        LabV2toV4 = 0x32203420,
        /// <summary>
        /// '4 2 '
        /// </summary>
        LabV4toV2 = 0x34203220,

        // Identities
        /// <summary>
        /// 'idn '
        /// </summary>
        IdentityElemType = 0x69646E20,

        // Float to floatPCS
        /// <summary>
        /// 'd2l '
        /// </summary>
        Lab2FloatPCS = 0x64326C20,
        /// <summary>
        /// 'l2d '
        /// </summary>
        FloatPCS2Lab = 0x6C326420,
        /// <summary>
        /// 'd2x '
        /// </summary>
        XYZ2FloatPCS = 0x64327820,
        /// <summary>
        /// 'x2d '
        /// </summary>
        FloatPCS2XYZ = 0x78326420,
        /// <summary>
        /// 'clp '
        /// </summary>
        ClipNegativesElemType = 0x636C7020
    }

    /// <summary>
    /// Defines the types of curve elements.
    /// </summary>
    public enum CurveSegSignature : uint
    {
        /// <summary>
        /// 'parf'
        /// </summary>
        FormulaCurveSeg = 0x70617266,
        /// <summary>
        /// 'samf'
        /// </summary>
        SampledCurveSeg = 0x73616D66,
        /// <summary>
        /// 'curf'
        /// </summary>
        SegmentedCurve = 0x63757266
    }

    /// <summary>
    /// Localized information.
    /// </summary>
    public enum InfoType : uint
    {
        /// <summary>
        /// Description.
        /// </summary>
        Description = 0,
        /// <summary>
        /// Manufacturer.
        /// </summary>
        Manufacturer = 1,
        /// <summary>
        /// Model.
        /// </summary>
        Model = 2,
        /// <summary>
        /// Copyright.
        /// </summary>
        Copyright = 3
    }

    /// <summary>
    /// Defines the device attributes, correspond to the low 4 bytes of the 8 bytes attribute quantity.
    /// </summary>
    [Flags]
    public enum DeviceAttributes : ulong
    {
        /// <summary>
        /// Reflective.
        /// </summary>
        Reflective = 0,
        /// <summary>
        /// Transparency.
        /// </summary>
        Transparency = 1,
        /// <summary>
        /// Glossy.
        /// </summary>
        Glossy = 0,
        /// <summary>
        /// Matte.
        /// </summary>
        Matte = 2
    }

    /// <summary>
    /// Defines the directions used for a CLUT in a profile.
    /// </summary>
    public enum UsedDirection : uint
    {
        /// <summary>
        /// Used as input.
        /// </summary>
        AsInput = 0,
        /// <summary>
        /// Used as output.
        /// </summary>
        AsOutput = 1,
        /// <summary>
        /// Used as proof.
        /// </summary>
        AsProof = 2
    }

    /// <summary>
    /// Defines the PostScript resource types.
    /// </summary>
    public enum PostScriptResourceType : uint
    {
        /// <summary>
        /// Color space array.
        /// </summary>
        ColorSpaceArray = 0,
        /// <summary>
        /// Color rendering dictionary.
        /// </summary>
        ColorRenderingDictionary = 1
    }

    /// <summary>
    /// Defines the standard observer encodings.
    /// </summary>
    public enum Observer : uint
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// CIE 1931 standard colorimetric observer.
        /// </summary>
        CIE1931 = 1,
        /// <summary>
        /// CIE 1964 standard colorimetric observer.
        /// </summary>
        CIE1964 = 2
    }

    /// <summary>
    /// Defines the measurement geometry encodings.
    /// </summary>
    public enum MeasurementGeometry : uint
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 0°:45° or 45°:0°
        /// </summary>
        ZeroFortyFiveOrFortyFiveZero = 1,
        /// <summary>
        /// 0°:d or d:0°.
        /// </summary>
        ZeroDOrDZero = 2
    }

    /// <summary>
    /// Defines the standard illuminant encodings.
    /// </summary>
    public enum IlluminantType : uint
    {
        /// <summary>
        /// Unknown illuminant.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// D50 illuminant.
        /// </summary>
        D50 = 1,
        /// <summary>
        /// D65 illuminant.
        /// </summary>
        D65 = 2,
        /// <summary>
        /// D93 illuminant.
        /// </summary>
        D93 = 3,
        /// <summary>
        /// F2 illuminant.
        /// </summary>
        F2 = 4,
        /// <summary>
        /// D55 illuminant.
        /// </summary>
        D55 = 5,
        /// <summary>
        /// A illuminant.
        /// </summary>
        A = 6,
        /// <summary>
        /// Equi-Power (E) illuminant.
        /// </summary>
        E = 7,  // Equi-Power
        /// <summary>
        /// F8 illuminant.
        /// </summary>
        F8 = 8
    }

    /// <summary>
    /// Defines a measurement specification.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ICCMeasurementConditions
    {
        /// <summary>
        /// The standard observer.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public Observer Observer;
        /// <summary>
        /// <see cref="CIEXYZ"/> tristimulus values for measurement backing.
        /// </summary>
        public CIEXYZ Backing;
        /// <summary>
        /// The measurement geometry.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public MeasurementGeometry Geometry;
        /// <summary>
        /// The measurement flare in the range 0 (0%) to 1.0 (100%).
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double Flare;
        /// <summary>
        /// The standard illuminant type.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public IlluminantType IlluminantType;

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated <see cref="ICCMeasurementConditions"/> object.
        /// </summary>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <returns>A new <see cref="ICCMeasurementConditions"/> instance.</returns>
        public static ICCMeasurementConditions FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<ICCMeasurementConditions>(handle);
        }
    }

    /// <summary>
    /// Defines a set of viewing conditions.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ICCViewingConditions
    {
        /// <summary>
        /// Un-normalized CIEXYZ values for illuminant (in which Y is in cd/m²).
        /// </summary>
        public CIEXYZ IlluminantXYZ;
        /// <summary>
        /// Un-normalized CIEXYZ values for surround (in which Y is in cd/m²).
        /// </summary>
        public CIEXYZ SurroundXYZ;
        /// <summary>
        /// The standard illuminant type.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public IlluminantType IlluminantType;

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated <see cref="ICCViewingConditions"/> object.
        /// </summary>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <returns>A new <see cref="ICCViewingConditions"/> instance.</returns>
        public static ICCViewingConditions FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<ICCViewingConditions>(handle);
        }
    }

    /// <summary>
    /// Defines static constants, properties and utility methods.
    /// </summary>
    public sealed class Cms
    {
        /// <summary>
        /// Gets the value of the LCMS_VERSION constant defined in the lcms.h header file.
        /// </summary>
        /// <remarks>
        /// Requires Little CMS version 2.8 or later.
        /// </remarks>
        public static int EncodedCMMVersion => Interop.GetEncodedCMMVersion();

        /// <summary>
        /// Sets the error handler for the global context.
        /// </summary>
        /// <param name="handler">The error handler to be set or null to reset to default.</param>
        /// <remarks>
        /// The default error handler does nothing.
        /// </remarks>
        public static void SetErrorHandler(ErrorHandler handler)
        {
            Interop.SetErrorHandler(handler);
        }

        /// <summary>
        /// Converts a <see cref="PixelType"/> to a <see cref="ColorSpaceSignature"/>.
        /// </summary>
        /// <param name="pixelType">The <see cref="PixelType"/> to be converted.</param>
        /// <returns>The equivalent <see cref="ColorSpaceSignature"/>.</returns>
        public static ColorSpaceSignature ToColorSpaceSignature(PixelType pixelType) =>
                (ColorSpaceSignature)Interop.GetICCColorSpace(Convert.ToUInt32(pixelType));

        /// <summary>
        /// Converts a <see cref="ColorSpaceSignature"/> to a <see cref="PixelType"/>.
        /// </summary>
        /// <param name="space">The <see cref="ColorSpaceSignature"/> to be converted.</param>
        /// <returns>The equivalent <see cref="PixelType"/>.</returns>
        public static PixelType ToPixelType(ColorSpaceSignature space) => (PixelType)Interop.GetLCMSColorSpace(Convert.ToUInt32(space));

        /// <summary>
        /// Gets the channel count for a <see cref="ColorSpaceSignature"/>.
        /// </summary>
        /// <param name="space">The <see cref="ColorSpaceSignature"/>.</param>
        /// <returns>The number of channels.</returns>
        public static uint ChannelsOf(ColorSpaceSignature space) => Interop.ChannelsOf(Convert.ToUInt32(space));

        /// <summary>
        /// Correlates a black body chromaticity from a given temperature in °K.
        /// </summary>
        /// <param name="xyY">On return contains the resulting chromaticity.</param>
        /// <param name="tempK">The temperature in °K.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public static bool WhitePointFromTemp(out CIExyY xyY, double tempK)
        {
            return Interop.WhitePointFromTemp(out xyY, tempK) != 0;
        }

        /// <summary>
        /// Correlates a black body temperature in °K from a given chromaticity.
        /// </summary>
        /// <param name="tempK">On return contains the resulting temperature in °K.</param>
        /// <param name="xyY">The target chromaticity.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public static bool TempFromWhitePoint(out double tempK, in CIExyY xyY)
        {
            return Interop.TempFromWhitePoint(out tempK, xyY) != 0;
        }

        /// <summary>
        /// Gets or sets the codes used to mark out-of-gamut on proofing transforms
        /// for the global context.
        /// </summary>
        /// <value>
        /// An array of 16 values.
        /// </value>
        public static ushort[] AlarmCodes
        {
            get
            {
                ushort[] alarmCodes = new ushort[16];
                Interop.GetAlarmCodes(alarmCodes);
                return alarmCodes;
            }
            set
            {
                if (value?.Length != 16) throw new ArgumentException($"'{nameof(value)}' array size must equal 16.");

                Interop.SetAlarmCodes(value);
            }
        }

        /// <summary>
        /// Gets or sets the adaptation state for absolute colorimetric intent for the global context.
        /// </summary>
        /// <value>
        /// <list type="bullet">
        /// <item>d = Degree on adaptation.</item>
        /// <item>0 = Not adapted.</item>
        /// <item>1 = Complete adaptation.</item>
        /// <item>in-between = Partial adaptation.</item>
        /// </list>
        /// </value>
        /// <remarks>
        /// Ignored for transforms created using
        /// <see cref="Transform.Create(Context, Profile[], bool[], Intent[], double[], Profile, int, uint, uint, CmsFlags)"/>.
        /// </remarks>
        public static double AdaptationState
        {
            get
            {
                return Interop.SetAdaptationState(-1.0);
            }
            set
            {
                Interop.SetAdaptationState(value);
            }
        }

        #region Pre-defined formatters
        /// <summary>
        /// Grayscale 8 bits formatter.
        /// </summary>
        public static readonly uint TYPE_GRAY_8
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(1);
        /// <summary>
        /// Grayscale 8 bits, reversed formatter.
        /// </summary>
        public static readonly uint TYPE_GRAY_8_REV
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(1)|FLAVOR_SH(1);
        /// <summary>
        /// Grayscale 16 bits formatter.
        /// </summary>
        public static readonly uint TYPE_GRAY_16
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(2);
        /// <summary>
        /// Grayscale 16 bits, reversed formatter.
        /// </summary>
        public static readonly uint TYPE_GRAY_16_REV
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(2)|FLAVOR_SH(1);
        /// <summary>
        /// Grayscale 16 bits, swapped-endian formatter.
        /// </summary>
        public static readonly uint TYPE_GRAY_16_SE
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// Grayscale + ignored alpha 8 bits formatter.
        /// </summary>
        public static readonly uint TYPE_GRAYA_8
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(1);
        /// <summary>
        /// Grayscale + ignored alpha 16 bits formatter.
        /// </summary>
        public static readonly uint TYPE_GRAYA_16
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(2);
        /// <summary>
        /// Grayscale + ignored alpha 16 bits, swapped-endian formatter.
        /// </summary>
        public static readonly uint TYPE_GRAYA_16_SE
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// Grayscale 8 bits, single plane formatter.
        /// </summary>
        public static readonly uint TYPE_GRAYA_8_PLANAR
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// Grayscale 16 bits, single plane formatter.
        /// </summary>
        public static readonly uint TYPE_GRAYA_16_PLANAR
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(2)|PLANAR_SH(1);
        /// <summary>
        /// RGB 8 bits formatter.
        /// </summary>
        public static readonly uint TYPE_RGB_8
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(1);
        /// <summary>
        /// RGB 8 bits, stored as contiguous planes formatter.
        /// </summary>
        public static readonly uint TYPE_RGB_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// BGR 8 bits formatter.
        /// </summary>
        public static readonly uint TYPE_BGR_8
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1);
        /// <summary>
        /// BGR 8 bits, stored as contiguous planes formatter.
        /// </summary>
        public static readonly uint TYPE_BGR_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// RGB 16 bits formatter.
        /// </summary>
        public static readonly uint TYPE_RGB_16
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2);
        /// <summary>
        /// RGB 16 bits, stored as contiguous planes formatter.
        /// </summary>
        public static readonly uint TYPE_RGB_16_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|PLANAR_SH(1);
        /// <summary>
        /// RGB 16 bits, swapped-endian formatter.
        /// </summary>
        public static readonly uint TYPE_RGB_16_SE
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// BGR 16 bits formatter.
        /// </summary>
        public static readonly uint TYPE_BGR_16
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1);
        /// <summary>
        /// BGR 16 bits, stored as contiguous planes formatter.
        /// </summary>
        public static readonly uint TYPE_BGR_16_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// BGR 16 bits, swapped-endian formatter.
        /// </summary>
        public static readonly uint TYPE_BGR_16_SE
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);
        /// <summary>
        /// RGB 8 bits plus an Alpha channel (which is ignored) formatter.
        /// </summary>
        public static readonly uint TYPE_RGBA_8
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1);
        /// <summary>
        /// RGBA 8 bits, stored as contiguous planes formatter.
        /// </summary>
        public static readonly uint TYPE_RGBA_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// RGB 16 bits plus an Alpha channel (which is ignored) formatter.
        /// </summary>
        public static readonly uint TYPE_RGBA_16
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2);
        /// <summary>
        /// RGBA 16 bits, stored as contiguous planes formatter.
        /// </summary>
        public static readonly uint TYPE_RGBA_16_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|PLANAR_SH(1);
        /// <summary>
        /// RGBA 16 bits, swapped-endian formatter.
        /// </summary>
        public static readonly uint TYPE_RGBA_16_SE
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// An ignored Alpha channel plus RGB in 8 bits formatter.
        /// </summary>
        public static readonly uint TYPE_ARGB_8
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_ARGB_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|SWAPFIRST_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// An ignored Alpha channel plus RGB in 8 bits formatter.
        /// </summary>
        public static readonly uint TYPE_ARGB_16
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|SWAPFIRST_SH(1);
        /// <summary>
        /// An ignored Alpha channel plus BGR in 8 bits formatter.
        /// </summary>
        public static readonly uint TYPE_ABGR_8
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1);
        /// <summary>
        /// An ignored Alpha channel plus BGR in separate 8 bit planes formatter.
        /// </summary>
        public static readonly uint TYPE_ABGR_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// An ignored Alpha channel plus BGR in 16 bits formatter.
        /// </summary>
        public static readonly uint TYPE_ABGR_16
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1);
        /// <summary>
        /// An ignored Alpha channel plus BGR in separate 16 bit planes formatter.
        /// </summary>
        public static readonly uint TYPE_ABGR_16_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_ABGR_16_SE
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_BGRA_8
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_BGRA_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1)|SWAPFIRST_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_BGRA_16
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_BGRA_16_SE
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|SWAPFIRST_SH(1)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMY_8
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMY_8_PLANAR
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMY_16
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMY_16_PLANAR
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(2)|PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMY_16_SE
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK_8
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYKA_8
                = COLORSPACE_SH(PixelType.CMYK)|EXTRA_SH(1)|CHANNELS_SH(4)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK_8_REV
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|FLAVOR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_YUVK_8
                = TYPE_CMYK_8_REV;
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK_8_PLANAR
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK_16
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK_16_REV
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|FLAVOR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_YUVK_16
                = TYPE_CMYK_16_REV;
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK_16_PLANAR
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK_16_SE
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC_8
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC_16
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC_16_SE
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KCMY_8
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KCMY_8_REV
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|FLAVOR_SH(1)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KCMY_16
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KCMY_16_REV
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|FLAVOR_SH(1)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KCMY_16_SE
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|ENDIAN16_SH(1)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK5_8
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK5_16
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK5_16_SE
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC5_8
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(1)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC5_16
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(2)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC5_16_SE
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK6_8
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK6_8_PLANAR
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK6_16
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK6_16_PLANAR
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(2)|PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK6_16_SE
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK7_8
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK7_16
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK7_16_SE
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC7_8
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(1)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC7_16
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(2)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC7_16_SE
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK8_8
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK8_16
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK8_16_SE
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC8_8
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(1)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC8_16
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(2)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC8_16_SE
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK9_8
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK9_16
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK9_16_SE
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC9_8
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(1)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC9_16
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(2)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC9_16_SE
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK10_8
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK10_16
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK10_16_SE
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC10_8
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(1)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC10_16
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(2)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC10_16_SE
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK11_8
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK11_16
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK11_16_SE
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC11_8
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(1)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC11_16
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(2)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC11_16_SE
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK12_8
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK12_16
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK12_16_SE
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC12_8
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(1)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC12_16
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(2)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_KYMC12_16_SE
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_XYZ_16
                = COLORSPACE_SH(PixelType.XYZ)|CHANNELS_SH(3)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_Lab_8
                = COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_LabV2_8
                = COLORSPACE_SH(PixelType.LabV2)|CHANNELS_SH(3)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_ALab_8
                = COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(1)|EXTRA_SH(1)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_ALabV2_8
                = COLORSPACE_SH(PixelType.LabV2)|CHANNELS_SH(3)|BYTES_SH(1)|EXTRA_SH(1)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_Lab_16
                = COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_LabV2_16
                = COLORSPACE_SH(PixelType.LabV2)|CHANNELS_SH(3)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_Yxy_16
                = COLORSPACE_SH(PixelType.Yxy)|CHANNELS_SH(3)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_YCbCr_8
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_YCbCr_8_PLANAR
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(1)|PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_YCbCr_16
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_YCbCr_16_PLANAR
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(2)|PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_YCbCr_16_SE
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(2)|ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_YUV_8
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_YUV_8_PLANAR
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(1) | PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_YUV_16
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_YUV_16_PLANAR
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(2) | PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_YUV_16_SE
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(2) | ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_HLS_8
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_HLS_8_PLANAR
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(1) | PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_HLS_16
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_HLS_16_PLANAR
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(2) | PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_HLS_16_SE
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(2) | ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_HSV_8
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_HSV_8_PLANAR
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(1) | PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_HSV_16
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_HSV_16_PLANAR
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(2) | PLANAR_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_HSV_16_SE
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(2) | ENDIAN16_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_NAMED_COLOR_INDEX
                = CHANNELS_SH(1)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_XYZ_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.XYZ)|CHANNELS_SH(3)|BYTES_SH(4);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_Lab_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(4);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_LabA_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Lab)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_GRAY_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(4);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_RGB_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(4);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_RGBA_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_ARGB_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_BGR_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(4)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_BGRA_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4)|DOSWAP_SH(1)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_ABGR_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(4);

        // Double precision floating point formatters
        // NOTE: The 'bytes' field is set to zero to avoid overflowing the bitfield

        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_XYZ_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.XYZ)|CHANNELS_SH(3)|BYTES_SH(0);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_Lab_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(0);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_GRAY_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(0);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_RGB_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(0);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_BGR_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(0)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(0);

        // IEEE 754-2008 "half"

        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_GRAY_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_RGB_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_RGBA_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_CMYK_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_ARGB_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_BGR_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_BGRA_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|SWAPFIRST_SH(1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly uint TYPE_ABGR_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1);
        #endregion

        private static uint FLOAT_SH(uint s) { return s << 22; }
        private static uint OPTIMIZED_SH(uint s) { return s << 21; }
        private static uint COLORSPACE_SH(PixelType s) { return Convert.ToUInt32(s) << 16; }
        private static uint SWAPFIRST_SH(uint s) { return s << 14; }
        private static uint FLAVOR_SH(uint s) { return s << 13; }
        private static uint PLANAR_SH(uint s) { return s << 12; }
        private static uint ENDIAN16_SH(uint s) { return s << 11; }
        private static uint DOSWAP_SH(uint s) { return s << 10; }
        private static uint EXTRA_SH(uint s) { return s << 7; }
        private static uint CHANNELS_SH(uint s) { return s << 3; }
        private static uint BYTES_SH(uint s) { return s; }
    }
}
