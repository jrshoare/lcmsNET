using System;

namespace lcmsNET
{
    /// <summary>
    /// Defines the intents.
    /// </summary>
    public enum Intent : int
    {
        #region ICC intents
        Perceptual = 0,
        RelativeColorimetric = 1,
        Saturation = 2,
        AbsoluteColorimetric = 3,
        #endregion

        #region Non-ICC intents
        PreserveKOnlyPerceptual = 10,
        PreserveKOnlyRelativeColorimetric = 11,
        PreserveKOnlySaturation = 12,
        PreserveKPlanePerceptual = 13,
        PreserveKPlaneRelativeColorimetric = 14,
        PreserveKPlaneSaturation = 15
        #endregion
    }

    /// <summary>
    /// Defines flags to command the whole process.
    /// </summary>
    /// <remarks>
    /// To fine-tune control over the number of grid points, values can be
    /// logically ORed with (((n) & 0xFF) &lt;&lt; 16).
    /// </remarks>
    [Flags]
    public enum CmsFlags : int
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
    public enum PixelType : int
    {
        Any = 0,    // Don't check colorspace
                    // Enumeration values 1 & 2 are reserved
        Gray = 3,
        RGB = 4,
        CMY = 5,
        CMYK = 6,
        YCbCr = 7,
        YUV = 8,    // Lu'v'
        XYZ = 9,
        Lab = 10,
        YUVK = 11,  // Lu'v'K
        HSV = 12,
        HLS = 13,
        Yxy = 14,
        MCH1 = 15,
        MCH2 = 16,
        MCH3 = 17,
        MCH4 = 18,
        MCH5 = 19,
        MCH6 = 20,
        MCH7 = 21,
        MCH8 = 22,
        MCH9 = 23,
        MCH10 = 24,
        MCH11 = 25,
        MCH12 = 26,
        MCH13 = 27,
        MCH14 = 28,
        MCH15 = 29,
        LabV2 = 30
    }

    /// <summary>
    /// Defines the base ICC tag definitions.
    /// </summary>
    public enum TagSignature : int
    {
        AToB0 = 0x41324230,  // 'A2B0'
        AToB1 = 0x41324231,  // 'A2B1'
        AToB2 = 0x41324232,  // 'A2B2'
        BlueColorant = 0x6258595A,  // 'bXYZ'
        BlueMatrixColumn = 0x6258595A,  // 'bXYZ'
        BlueTRC = 0x62545243,  // 'bTRC'
        BToA0 = 0x42324130,  // 'B2A0'
        BToA1 = 0x42324131,  // 'B2A1'
        BToA2 = 0x42324132,  // 'B2A2'
        CalibrationDateTime = 0x63616C74,  // 'calt'
        CharTarget = 0x74617267,  // 'targ'
        ChromaticAdaptation = 0x63686164,  // 'chad'
        Chromaticity = 0x6368726D,  // 'chrm'
        ColorantOrder = 0x636C726F,  // 'clro'
        ColorantTable = 0x636C7274,  // 'clrt'
        ColorantTableOut = 0x636C6F74,  // 'clot'
        ColorimetricIntentImageState = 0x63696973,  // 'ciis'
        Copyright = 0x63707274,  // 'cprt'
        CrdInfo = 0x63726469,  // 'crdi'
        Data = 0x64617461,  // 'data'
        DateTime = 0x6474696D,  // 'dtim'
        DeviceMfgDesc = 0x646D6E64,  // 'dmnd'
        DeviceModelDesc = 0x646D6464,  // 'dmdd'
        DeviceSettings = 0x64657673,  // 'devs'
        DToB0 = 0x44324230,  // 'D2B0'
        DToB1 = 0x44324231,  // 'D2B1'
        DToB2 = 0x44324232,  // 'D2B2'
        DToB3 = 0x44324233,  // 'D2B3'
        BToD0 = 0x42324430,  // 'B2D0'
        BToD1 = 0x42324431,  // 'B2D1'
        BToD2 = 0x42324432,  // 'B2D2'
        BToD3 = 0x42324433,  // 'B2D3'
        Gamut = 0x67616D74,  // 'gamt'
        GrayTRC = 0x6b545243,  // 'kTRC'
        GreenColorant = 0x6758595A,  // 'gXYZ'
        GreenMatrixColumn = 0x6758595A,  // 'gXYZ'
        GreenTRC = 0x67545243,  // 'gTRC'
        Luminance = 0x6C756d69,  // 'lumi'
        Measurement = 0x6D656173,  // 'meas'
        MediaBlackPoint = 0x626B7074,  // 'bkpt'
        MediaWhitePoint = 0x77747074,  // 'wtpt'
        NamedColor = 0x6E636f6C,  // 'ncol' // Deprecated by the ICC
        NamedColor2 = 0x6E636C32,  // 'ncl2'
        OutputResponse = 0x72657370,  // 'resp'
        PerceptualRenderingIntentGamut = 0x72696730,  // 'rig0'
        Preview0 = 0x70726530,  // 'pre0'
        Preview1 = 0x70726531,  // 'pre1'
        Preview2 = 0x70726532,  // 'pre2'
        ProfileDescription = 0x64657363,  // 'desc'
        ProfileDescriptionML = 0x6473636d,  // 'dscm'
        ProfileSequenceDesc = 0x70736571,  // 'pseq'
        ProfileSequenceId = 0x70736964,  // 'psid'
        Ps2CRD0 = 0x70736430,  // 'psd0'
        Ps2CRD1 = 0x70736431,  // 'psd1'
        Ps2CRD2 = 0x70736432,  // 'psd2'
        Ps2CRD3 = 0x70736433,  // 'psd3'
        Ps2CSA = 0x70733273,  // 'ps2s'
        Ps2RenderingIntent = 0x70733269,  // 'ps2i'
        RedColorant = 0x7258595A,  // 'rXYZ'
        RedMatrixColumn = 0x7258595A,  // 'rXYZ'
        RedTRC = 0x72545243,  // 'rTRC'
        SaturationRenderingIntentGamut = 0x72696732,  // 'rig2'
        ScreeningDesc = 0x73637264,  // 'scrd'
        Screening = 0x7363726E,  // 'scrn'
        Technology = 0x74656368,  // 'tech'
        UcrBg = 0x62666420,  // 'bfd '
        ViewingCondDesc = 0x76756564,  // 'vued'
        ViewingConditions = 0x76696577,  // 'view'
        Vcgt = 0x76636774,  // 'vcgt'
        Meta = 0x6D657461,  // 'meta'
        ArgyllArts = 0x61727473   // 'arts'
    }

    /// <summary>
    /// Defines the base ICC type definitions.
    /// </summary>
    public enum TagTypeSignature : int
    {
        Chromaticity = 0x6368726D,  // 'chrm'
        ColorantOrder = 0x636C726F,  // 'clro'
        ColorantTable = 0x636C7274,  // 'clrt'
        CrdInfo = 0x63726469,  // 'crdi'
        Curve = 0x63757276,  // 'curv'
        Data = 0x64617461,  // 'data'
        Dict = 0x64696374,  // 'dict'
        DateTime = 0x6474696D,  // 'dtim'
        DeviceSettings = 0x64657673,  // 'devs'
        Lut16 = 0x6d667432,  // 'mft2'
        Lut8 = 0x6d667431,  // 'mft1'
        LutAtoB = 0x6d414220,  // 'mAB '
        LutBtoA = 0x6d424120,  // 'mBA '
        Measurement = 0x6D656173,  // 'meas'
        MultiLocalizedUnicode = 0x6D6C7563,  // 'mluc'
        MultiProcessElement = 0x6D706574,  // 'mpet'
        NamedColor = 0x6E636f6C,  // 'ncol' -- DEPRECATED!
        NamedColor2 = 0x6E636C32,  // 'ncl2'
        ParametricCurve = 0x70617261,  // 'para'
        ProfileSequenceDesc = 0x70736571,  // 'pseq'
        ProfileSequenceId = 0x70736964,  // 'psid'
        ResponseCurveSet16 = 0x72637332,  // 'rcs2'
        S15Fixed16Array = 0x73663332,  // 'sf32'
        Screening = 0x7363726E,  // 'scrn'
        Signature = 0x73696720,  // 'sig '
        Text = 0x74657874,  // 'text'
        TextDescription = 0x64657363,  // 'desc'
        U16Fixed16Array = 0x75663332,  // 'uf32'
        UcrBg = 0x62666420,  // 'bfd '
        UInt16Array = 0x75693136,  // 'ui16'
        UInt32Array = 0x75693332,  // 'ui32'
        UInt64Array = 0x75693634,  // 'ui64'
        UInt8Array = 0x75693038,  // 'ui08'
        Vcgt = 0x76636774,  // 'vcgt'
        ViewingConditions = 0x76696577,  // 'view'
        XYZ = 0x58595A20   // 'XYZ '
    }

    public sealed class Cms
    {
        public static int EncodedCMMVersion => Interop.GetEncodedCMMVersion();

        #region Representations
        public static readonly int TYPE_GRAY_8
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(1);
        public static readonly int TYPE_GRAY_8_REV
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(1)|FLAVOR_SH(1);
        public static readonly int TYPE_GRAY_16
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(2);
        public static readonly int TYPE_GRAY_16_REV
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(2)|FLAVOR_SH(1);
        public static readonly int TYPE_GRAY_16_SE
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly int TYPE_GRAYA_8
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(1);
        public static readonly int TYPE_GRAYA_16
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(2);
        public static readonly int TYPE_GRAYA_16_SE
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly int TYPE_GRAYA_8_PLANAR
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_GRAYA_16_PLANAR
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(2)|PLANAR_SH(1);

        public static readonly int TYPE_RGB_8
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(1);
        public static readonly int TYPE_RGB_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_BGR_8
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly int TYPE_BGR_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_RGB_16
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly int TYPE_RGB_16_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|PLANAR_SH(1);
        public static readonly int TYPE_RGB_16_SE
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly int TYPE_BGR_16
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly int TYPE_BGR_16_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_BGR_16_SE
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly int TYPE_RGBA_8
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1);
        public static readonly int TYPE_RGBA_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_RGBA_16
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly int TYPE_RGBA_16_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|PLANAR_SH(1);
        public static readonly int TYPE_RGBA_16_SE
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|ENDIAN16_SH(1);

        public static readonly int TYPE_ARGB_8
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|SWAPFIRST_SH(1);
        public static readonly int TYPE_ARGB_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|SWAPFIRST_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_ARGB_16
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|SWAPFIRST_SH(1);

        public static readonly int TYPE_ABGR_8
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly int TYPE_ABGR_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_ABGR_16
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly int TYPE_ABGR_16_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_ABGR_16_SE
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly int TYPE_BGRA_8
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1)|SWAPFIRST_SH(1);
        public static readonly int TYPE_BGRA_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1)|SWAPFIRST_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_BGRA_16
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|SWAPFIRST_SH(1);
        public static readonly int TYPE_BGRA_16_SE
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|SWAPFIRST_SH(1)|ENDIAN16_SH(1);

        public static readonly int TYPE_CMY_8
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(1);
        public static readonly int TYPE_CMY_8_PLANAR
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_CMY_16
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly int TYPE_CMY_16_PLANAR
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(2)|PLANAR_SH(1);
        public static readonly int TYPE_CMY_16_SE
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(2)|ENDIAN16_SH(1);

        public static readonly int TYPE_CMYK_8
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1);
        public static readonly int TYPE_CMYKA_8
                = COLORSPACE_SH(PixelType.CMYK)|EXTRA_SH(1)|CHANNELS_SH(4)|BYTES_SH(1);
        public static readonly int TYPE_CMYK_8_REV
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|FLAVOR_SH(1);
        public static readonly int TYPE_YUVK_8
                = TYPE_CMYK_8_REV;
        public static readonly int TYPE_CMYK_8_PLANAR
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_CMYK_16
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2);
        public static readonly int TYPE_CMYK_16_REV
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|FLAVOR_SH(1);
        public static readonly int TYPE_YUVK_16
                = TYPE_CMYK_16_REV;
        public static readonly int TYPE_CMYK_16_PLANAR
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|PLANAR_SH(1);
        public static readonly int TYPE_CMYK_16_SE
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|ENDIAN16_SH(1);

        public static readonly int TYPE_KYMC_8
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC_16
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC_16_SE
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly int TYPE_KCMY_8
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|SWAPFIRST_SH(1);
        public static readonly int TYPE_KCMY_8_REV
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|FLAVOR_SH(1)|SWAPFIRST_SH(1);
        public static readonly int TYPE_KCMY_16
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|SWAPFIRST_SH(1);
        public static readonly int TYPE_KCMY_16_REV
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|FLAVOR_SH(1)|SWAPFIRST_SH(1);
        public static readonly int TYPE_KCMY_16_SE
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|ENDIAN16_SH(1)|SWAPFIRST_SH(1);

        public static readonly int TYPE_CMYK5_8
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(1);
        public static readonly int TYPE_CMYK5_16
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(2);
        public static readonly int TYPE_CMYK5_16_SE
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly int TYPE_KYMC5_8
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC5_16
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC5_16_SE
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly int TYPE_CMYK6_8
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(1);
        public static readonly int TYPE_CMYK6_8_PLANAR
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_CMYK6_16
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(2);
        public static readonly int TYPE_CMYK6_16_PLANAR
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(2)|PLANAR_SH(1);
        public static readonly int TYPE_CMYK6_16_SE
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(2)|ENDIAN16_SH(1);

        public static readonly int TYPE_CMYK7_8
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(1);
        public static readonly int TYPE_CMYK7_16
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(2);
        public static readonly int TYPE_CMYK7_16_SE
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly int TYPE_KYMC7_8
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC7_16
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC7_16_SE
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly int TYPE_CMYK8_8
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(1);
        public static readonly int TYPE_CMYK8_16
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(2);
        public static readonly int TYPE_CMYK8_16_SE
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly int TYPE_KYMC8_8
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC8_16
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC8_16_SE
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly int TYPE_CMYK9_8
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(1);
        public static readonly int TYPE_CMYK9_16
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(2);
        public static readonly int TYPE_CMYK9_16_SE
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly int TYPE_KYMC9_8
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC9_16
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC9_16_SE
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly int TYPE_CMYK10_8
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(1);
        public static readonly int TYPE_CMYK10_16
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(2);
        public static readonly int TYPE_CMYK10_16_SE
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly int TYPE_KYMC10_8
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC10_16
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC10_16_SE
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly int TYPE_CMYK11_8
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(1);
        public static readonly int TYPE_CMYK11_16
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(2);
        public static readonly int TYPE_CMYK11_16_SE
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly int TYPE_KYMC11_8
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC11_16
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC11_16_SE
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly int TYPE_CMYK12_8
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(1);
        public static readonly int TYPE_CMYK12_16
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(2);
        public static readonly int TYPE_CMYK12_16_SE
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly int TYPE_KYMC12_8
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC12_16
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly int TYPE_KYMC12_16_SE
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        // Colorimetric
        public static readonly int TYPE_XYZ_16
                = COLORSPACE_SH(PixelType.XYZ)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly int TYPE_Lab_8
                = COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(1);
        public static readonly int TYPE_LabV2_8
                = COLORSPACE_SH(PixelType.LabV2)|CHANNELS_SH(3)|BYTES_SH(1);

        public static readonly int TYPE_ALab_8
                = COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(1)|EXTRA_SH(1)|SWAPFIRST_SH(1);
        public static readonly int TYPE_ALabV2_8
                = COLORSPACE_SH(PixelType.LabV2)|CHANNELS_SH(3)|BYTES_SH(1)|EXTRA_SH(1)|SWAPFIRST_SH(1);
        public static readonly int TYPE_Lab_16
                = COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly int TYPE_LabV2_16
                = COLORSPACE_SH(PixelType.LabV2)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly int TYPE_Yxy_16
                = COLORSPACE_SH(PixelType.Yxy)|CHANNELS_SH(3)|BYTES_SH(2);

        // YCbCr
        public static readonly int TYPE_YCbCr_8
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(1);
        public static readonly int TYPE_YCbCr_8_PLANAR
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly int TYPE_YCbCr_16
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly int TYPE_YCbCr_16_PLANAR
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(2)|PLANAR_SH(1);
        public static readonly int TYPE_YCbCr_16_SE
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(2)|ENDIAN16_SH(1);

        // YUV
        public static readonly int TYPE_YUV_8
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(1);
        public static readonly int TYPE_YUV_8_PLANAR
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(1) | PLANAR_SH(1);
        public static readonly int TYPE_YUV_16
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(2);
        public static readonly int TYPE_YUV_16_PLANAR
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(2) | PLANAR_SH(1);
        public static readonly int TYPE_YUV_16_SE
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(2) | ENDIAN16_SH(1);

        // HLS
        public static readonly int TYPE_HLS_8
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(1);
        public static readonly int TYPE_HLS_8_PLANAR
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(1) | PLANAR_SH(1);
        public static readonly int TYPE_HLS_16
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(2);
        public static readonly int TYPE_HLS_16_PLANAR
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(2) | PLANAR_SH(1);
        public static readonly int TYPE_HLS_16_SE
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(2) | ENDIAN16_SH(1);

        // HSV
        public static readonly int TYPE_HSV_8
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(1);
        public static readonly int TYPE_HSV_8_PLANAR
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(1) | PLANAR_SH(1);
        public static readonly int TYPE_HSV_16
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(2);
        public static readonly int TYPE_HSV_16_PLANAR
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(2) | PLANAR_SH(1);
        public static readonly int TYPE_HSV_16_SE
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(2) | ENDIAN16_SH(1);

        // Named color index. Only 16 bits allowed (don't check colorspace)
        public static readonly int TYPE_NAMED_COLOR_INDEX
                = CHANNELS_SH(1)|BYTES_SH(2);

        // Floating point formatters
        public static readonly int TYPE_XYZ_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.XYZ)|CHANNELS_SH(3)|BYTES_SH(4);
        public static readonly int TYPE_Lab_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(4);
        public static readonly int TYPE_LabA_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Lab)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4);
        public static readonly int TYPE_GRAY_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(4);
        public static readonly int TYPE_RGB_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(4);

        public static readonly int TYPE_RGBA_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4);
        public static readonly int TYPE_ARGB_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4)|SWAPFIRST_SH(1);
        public static readonly int TYPE_BGR_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(4)|DOSWAP_SH(1);
        public static readonly int TYPE_BGRA_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4)|DOSWAP_SH(1)|SWAPFIRST_SH(1);
        public static readonly int TYPE_ABGR_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4)|DOSWAP_SH(1);

        public static readonly int TYPE_CMYK_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(4);

        // Double precision floating point formatters
        // NOTE: The 'bytes' field is set to zero to avoid overflowing the bitfield
        public static readonly int TYPE_XYZ_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.XYZ)|CHANNELS_SH(3)|BYTES_SH(0);
        public static readonly int TYPE_Lab_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(0);
        public static readonly int TYPE_GRAY_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(0);
        public static readonly int TYPE_RGB_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(0);
        public static readonly int TYPE_BGR_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(0)|DOSWAP_SH(1);
        public static readonly int TYPE_CMYK_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(0);

        // IEEE 754-2008 "half"
        public static readonly int TYPE_GRAY_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(2);
        public static readonly int TYPE_RGB_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly int TYPE_RGBA_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly int TYPE_CMYK_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2);

        public static readonly int TYPE_ARGB_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|SWAPFIRST_SH(1);
        public static readonly int TYPE_BGR_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly int TYPE_BGRA_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|SWAPFIRST_SH(1);
        public static readonly int TYPE_ABGR_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1);
        #endregion

        private static int FLOAT_SH(int s) { return s << 22; }
        private static int OPTIMIZED_SH(int s) { return s << 21; }
        private static int COLORSPACE_SH(PixelType s) { return Convert.ToInt32(s) << 16; }
        private static int SWAPFIRST_SH(int s) { return s << 14; }
        private static int FLAVOR_SH(int s) { return s << 13; }
        private static int PLANAR_SH(int s) { return s << 12; }
        private static int ENDIAN16_SH(int s) { return s << 11; }
        private static int DOSWAP_SH(int s) { return s << 10; }
        private static int EXTRA_SH(int s) { return s << 7; }
        private static int CHANNELS_SH(int s) { return s << 3; }
        private static int BYTES_SH(int s) { return s; }
    }
}
