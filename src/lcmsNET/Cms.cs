using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ErrorHandler(IntPtr contextID, [MarshalAs(UnmanagedType.U4)] int errorCode, [MarshalAs(UnmanagedType.LPStr)] string errorText);

    /// <summary>
    /// Defines the intents.
    /// </summary>
    public enum Intent : uint
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
    public enum TagSignature : uint
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
    public enum TagTypeSignature : uint
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

    /// <summary>
    /// Defines the ICC color spaces.
    /// </summary>
    public enum ColorSpaceSignature : uint
    {
        XYZData = 0x58595A20,  // 'XYZ '
        LabData = 0x4C616220,  // 'Lab '
        LuvData = 0x4C757620,  // 'Luv '
        YCbCrData = 0x59436272,  // 'YCbr'
        YxyData = 0x59787920,  // 'Yxy '
        RgbData = 0x52474220,  // 'RGB '
        GrayData = 0x47524159,  // 'GRAY'
        HsvData = 0x48535620,  // 'HSV '
        HlsData = 0x484C5320,  // 'HLS '
        CmykData = 0x434D594B,  // 'CMYK'
        CmyData = 0x434D5920,  // 'CMY '
        MCH1Data = 0x4D434831,  // 'MCH1'
        MCH2Data = 0x4D434832,  // 'MCH2'
        MCH3Data = 0x4D434833,  // 'MCH3'
        MCH4Data = 0x4D434834,  // 'MCH4'
        MCH5Data = 0x4D434835,  // 'MCH5'
        MCH6Data = 0x4D434836,  // 'MCH6'
        MCH7Data = 0x4D434837,  // 'MCH7'
        MCH8Data = 0x4D434838,  // 'MCH8'
        MCH9Data = 0x4D434839,  // 'MCH9'
        MCHAData = 0x4D434841,  // 'MCHA'
        MCHBData = 0x4D434842,  // 'MCHB'
        MCHCData = 0x4D434843,  // 'MCHC'
        MCHDData = 0x4D434844,  // 'MCHD'
        MCHEData = 0x4D434845,  // 'MCHE'
        MCHFData = 0x4D434846,  // 'MCHF'
        NamedData = 0x6e6d636c,  // 'nmcl'
        _1colorData = 0x31434C52,  // '1CLR'
        _2colorData = 0x32434C52,  // '2CLR'
        _3colorData = 0x33434C52,  // '3CLR'
        _4colorData = 0x34434C52,  // '4CLR'
        _5colorData = 0x35434C52,  // '5CLR'
        _6colorData = 0x36434C52,  // '6CLR'
        _7colorData = 0x37434C52,  // '7CLR'
        _8colorData = 0x38434C52,  // '8CLR'
        _9colorData = 0x39434C52,  // '9CLR'
        _10colorData = 0x41434C52,  // 'ACLR'
        _11colorData = 0x42434C52,  // 'BCLR'
        _12colorData = 0x43434C52,  // 'CCLR'
        _13colorData = 0x44434C52,  // 'DCLR'
        _14colorData = 0x45434C52,  // 'ECLR'
        _15colorData = 0x46434C52,  // 'FCLR'
        LuvKData = 0x4C75764B   // 'LuvK'
    }

    /// <summary>
    /// Defines the ICC profile class.
    /// </summary>
    public enum ProfileClassSignature : uint
    {
        Input = 0x73636E72,  // 'scnr'
        Display = 0x6D6E7472,  // 'mntr'
        Output = 0x70727472,  // 'prtr'
        Link = 0x6C696E6B,  // 'link'
        Abstract = 0x61627374,  // 'abst'
        ColorSpace = 0x73706163,  // 'spac'
        NamedColor = 0x6e6d636c   // 'nmcl'
    }

    /// <summary>
    /// Defines the ICC technology tag.
    /// </summary>
    public enum TechnologySignature : uint
    {
        DigitalCamera = 0x6463616D,  // 'dcam'
        FilmScanner = 0x6673636E,  // 'fscn'
        ReflectiveScanner = 0x7273636E,  // 'rscn'
        InkJetPrinter = 0x696A6574,  // 'ijet'
        ThermalWaxPrinter = 0x74776178,  // 'twax'
        ElectrophotographicPrinter = 0x6570686F,  // 'epho'
        ElectrostaticPrinter = 0x65737461,  // 'esta'
        DyeSublimationPrinter = 0x64737562,  // 'dsub'
        PhotographicPaperPrinter = 0x7270686F,  // 'rpho'
        FilmWriter = 0x6670726E,  // 'fprn'
        VideoMonitor = 0x7669646D,  // 'vidm'
        VideoCamera = 0x76696463,  // 'vidc'
        ProjectionTelevision = 0x706A7476,  // 'pjtv'
        CRTDisplay = 0x43525420,  // 'CRT '
        PMDisplay = 0x504D4420,  // 'PMD '
        AMDisplay = 0x414D4420,  // 'AMD '
        PhotoCD = 0x4B504344,  // 'KPCD'
        PhotoImageSetter = 0x696D6773,  // 'imgs'
        Gravure = 0x67726176,  // 'grav'
        OffsetLithography = 0x6F666673,  // 'offs'
        Silkscreen = 0x73696C6B,  // 'silk'
        Flexography = 0x666C6578,  // 'flex'
        MotionPictureFilmScanner = 0x6D706673,  // 'mpfs'
        MotionPictureFilmRecorder = 0x6D706672,  // 'mpfr'
        DigitalMotionPictureCamera = 0x646D7063,  // 'dmpc'
        DigitalCinemaProjector = 0x64636A70   // 'dcpj'
    }

    /// <summary>
    /// Defines the ICC platforms.
    /// </summary>
    public enum PlatformSignature : uint
    {
        Macintosh = 0x4150504C,  // 'APPL'
        Microsoft = 0x4D534654,  // 'MSFT'
        Solaris = 0x53554E57,  // 'SUNW'
        SGI = 0x53474920,  // 'SGI '
        Taligent = 0x54474E54,  // 'TGNT'
        Unices = 0x2A6E6978   // '*nix'   // From argyll -- Not official
    }

    /// <summary>
    /// Defines the multi process element types.
    /// </summary>
    public enum StageSignature : uint
    {
        CurveSetElemType = 0x63767374,  //'cvst'
        MatrixElemType = 0x6D617466,  //'matf'
        CLutElemType = 0x636C7574,  //'clut'

        BAcsElemType = 0x62414353,  // 'bACS'
        EAcsElemType = 0x65414353,  // 'eACS'

        // Custom from here, not in the ICC Spec
        XYZ2LabElemType = 0x6C327820,  // 'l2x '
        Lab2XYZElemType = 0x78326C20,  // 'x2l '
        NamedColorElemType = 0x6E636C20,  // 'ncl '
        LabV2toV4 = 0x32203420,  // '2 4 '
        LabV4toV2 = 0x34203220,  // '4 2 '

        // Identities
        IdentityElemType = 0x69646E20,  // 'idn '

        // Float to floatPCS
        Lab2FloatPCS = 0x64326C20,  // 'd2l '
        FloatPCS2Lab = 0x6C326420,  // 'l2d '
        XYZ2FloatPCS = 0x64327820,  // 'd2x '
        FloatPCS2XYZ = 0x78326420,  // 'x2d '  
        ClipNegativesElemType = 0x636c7020   // 'clp '
    }

    /// <summary>
    /// Defines the types of curve elements.
    /// </summary>
    public enum CurveSegSignature : uint
    {
        FormulaCurveSeg = 0x70617266, // 'parf'
        SampledCurveSeg = 0x73616D66, // 'samf'
        SegmentedCurve = 0x63757266  // 'curf'
    }

    /// <summary>
    /// Localized information.
    /// </summary>
    public enum InfoType : uint
    {
        Description = 0,
        Manufacturer = 1,
        Model = 2,
        Copyright = 3
    }

    /// <summary>
    /// Defines the device attributes, correspond to the low 4 bytes of the 8 bytes attribute quantity.
    /// </summary>
    [Flags]
    public enum DeviceAttributes : ulong
    {
        Reflective = 0,
        Transparency = 1,
        Glossy = 0,
        Matte = 2
    }

    /// <summary>
    /// Defines the directions used for a CLUT in a profile.
    /// </summary>
    public enum UsedDirection : uint
    {
        AsInput = 0,
        AsOutput = 1,
        AsProof = 2
    }

    public sealed class Cms
    {
        public static int EncodedCMMVersion => Interop.GetEncodedCMMVersion();

        public static void SetErrorHandler(ErrorHandler handler)
        {
            Interop.SetErrorHandler(handler);
        }

        public static ColorSpaceSignature ToColorSpaceSignature(PixelType pixelType) =>
                (ColorSpaceSignature)Interop.GetICCColorSpace(Convert.ToUInt32(pixelType));

        public static PixelType ToPixelType(ColorSpaceSignature space) => (PixelType)Interop.GetLCMSColorSpace(Convert.ToUInt32(space));

        public static uint ChannelsOf(ColorSpaceSignature space) => Interop.ChannelsOf(Convert.ToUInt32(space));

        public static ushort[] AlarmCodes
        {
            get
            {
                ushort[] alarmCodes = new ushort[16];
                Interop.GetAlarmCodes(ref alarmCodes);
                return alarmCodes;
            }
            set
            {
                if (value?.Length != 16) throw new ArgumentException($"'{nameof(value)}' array size must equal 16.");

                Interop.SetAlarmCodes(value);
            }
        }

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

        #region Representations
        public static readonly uint TYPE_GRAY_8
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(1);
        public static readonly uint TYPE_GRAY_8_REV
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(1)|FLAVOR_SH(1);
        public static readonly uint TYPE_GRAY_16
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(2);
        public static readonly uint TYPE_GRAY_16_REV
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(2)|FLAVOR_SH(1);
        public static readonly uint TYPE_GRAY_16_SE
                = COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly uint TYPE_GRAYA_8
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(1);
        public static readonly uint TYPE_GRAYA_16
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(2);
        public static readonly uint TYPE_GRAYA_16_SE
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly uint TYPE_GRAYA_8_PLANAR
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_GRAYA_16_PLANAR
                = COLORSPACE_SH(PixelType.Gray)|EXTRA_SH(1)|CHANNELS_SH(1)|BYTES_SH(2)|PLANAR_SH(1);

        public static readonly uint TYPE_RGB_8
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(1);
        public static readonly uint TYPE_RGB_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_BGR_8
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly uint TYPE_BGR_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_RGB_16
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly uint TYPE_RGB_16_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|PLANAR_SH(1);
        public static readonly uint TYPE_RGB_16_SE
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly uint TYPE_BGR_16
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly uint TYPE_BGR_16_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_BGR_16_SE
                = COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly uint TYPE_RGBA_8
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1);
        public static readonly uint TYPE_RGBA_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_RGBA_16
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly uint TYPE_RGBA_16_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|PLANAR_SH(1);
        public static readonly uint TYPE_RGBA_16_SE
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|ENDIAN16_SH(1);

        public static readonly uint TYPE_ARGB_8
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|SWAPFIRST_SH(1);
        public static readonly uint TYPE_ARGB_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|SWAPFIRST_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_ARGB_16
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|SWAPFIRST_SH(1);

        public static readonly uint TYPE_ABGR_8
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly uint TYPE_ABGR_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_ABGR_16
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly uint TYPE_ABGR_16_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_ABGR_16_SE
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly uint TYPE_BGRA_8
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1)|SWAPFIRST_SH(1);
        public static readonly uint TYPE_BGRA_8_PLANAR
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(1)|DOSWAP_SH(1)|SWAPFIRST_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_BGRA_16
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|SWAPFIRST_SH(1);
        public static readonly uint TYPE_BGRA_16_SE
                = COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|SWAPFIRST_SH(1)|ENDIAN16_SH(1);

        public static readonly uint TYPE_CMY_8
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(1);
        public static readonly uint TYPE_CMY_8_PLANAR
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_CMY_16
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly uint TYPE_CMY_16_PLANAR
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(2)|PLANAR_SH(1);
        public static readonly uint TYPE_CMY_16_SE
                = COLORSPACE_SH(PixelType.CMY)|CHANNELS_SH(3)|BYTES_SH(2)|ENDIAN16_SH(1);

        public static readonly uint TYPE_CMYK_8
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1);
        public static readonly uint TYPE_CMYKA_8
                = COLORSPACE_SH(PixelType.CMYK)|EXTRA_SH(1)|CHANNELS_SH(4)|BYTES_SH(1);
        public static readonly uint TYPE_CMYK_8_REV
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|FLAVOR_SH(1);
        public static readonly uint TYPE_YUVK_8
                = TYPE_CMYK_8_REV;
        public static readonly uint TYPE_CMYK_8_PLANAR
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_CMYK_16
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2);
        public static readonly uint TYPE_CMYK_16_REV
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|FLAVOR_SH(1);
        public static readonly uint TYPE_YUVK_16
                = TYPE_CMYK_16_REV;
        public static readonly uint TYPE_CMYK_16_PLANAR
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|PLANAR_SH(1);
        public static readonly uint TYPE_CMYK_16_SE
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|ENDIAN16_SH(1);

        public static readonly uint TYPE_KYMC_8
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC_16
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC_16_SE
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly uint TYPE_KCMY_8
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|SWAPFIRST_SH(1);
        public static readonly uint TYPE_KCMY_8_REV
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(1)|FLAVOR_SH(1)|SWAPFIRST_SH(1);
        public static readonly uint TYPE_KCMY_16
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|SWAPFIRST_SH(1);
        public static readonly uint TYPE_KCMY_16_REV
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|FLAVOR_SH(1)|SWAPFIRST_SH(1);
        public static readonly uint TYPE_KCMY_16_SE
                = COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2)|ENDIAN16_SH(1)|SWAPFIRST_SH(1);

        public static readonly uint TYPE_CMYK5_8
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(1);
        public static readonly uint TYPE_CMYK5_16
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(2);
        public static readonly uint TYPE_CMYK5_16_SE
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly uint TYPE_KYMC5_8
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC5_16
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC5_16_SE
                = COLORSPACE_SH(PixelType.MCH5)|CHANNELS_SH(5)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly uint TYPE_CMYK6_8
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(1);
        public static readonly uint TYPE_CMYK6_8_PLANAR
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_CMYK6_16
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(2);
        public static readonly uint TYPE_CMYK6_16_PLANAR
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(2)|PLANAR_SH(1);
        public static readonly uint TYPE_CMYK6_16_SE
                = COLORSPACE_SH(PixelType.MCH6)|CHANNELS_SH(6)|BYTES_SH(2)|ENDIAN16_SH(1);

        public static readonly uint TYPE_CMYK7_8
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(1);
        public static readonly uint TYPE_CMYK7_16
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(2);
        public static readonly uint TYPE_CMYK7_16_SE
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly uint TYPE_KYMC7_8
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC7_16
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC7_16_SE
                = COLORSPACE_SH(PixelType.MCH7)|CHANNELS_SH(7)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly uint TYPE_CMYK8_8
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(1);
        public static readonly uint TYPE_CMYK8_16
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(2);
        public static readonly uint TYPE_CMYK8_16_SE
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly uint TYPE_KYMC8_8
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC8_16
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC8_16_SE
                = COLORSPACE_SH(PixelType.MCH8)|CHANNELS_SH(8)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly uint TYPE_CMYK9_8
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(1);
        public static readonly uint TYPE_CMYK9_16
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(2);
        public static readonly uint TYPE_CMYK9_16_SE
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly uint TYPE_KYMC9_8
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC9_16
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC9_16_SE
                = COLORSPACE_SH(PixelType.MCH9)|CHANNELS_SH(9)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly uint TYPE_CMYK10_8
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(1);
        public static readonly uint TYPE_CMYK10_16
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(2);
        public static readonly uint TYPE_CMYK10_16_SE
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly uint TYPE_KYMC10_8
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC10_16
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC10_16_SE
                = COLORSPACE_SH(PixelType.MCH10)|CHANNELS_SH(10)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly uint TYPE_CMYK11_8
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(1);
        public static readonly uint TYPE_CMYK11_16
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(2);
        public static readonly uint TYPE_CMYK11_16_SE
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly uint TYPE_KYMC11_8
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC11_16
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC11_16_SE
                = COLORSPACE_SH(PixelType.MCH11)|CHANNELS_SH(11)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        public static readonly uint TYPE_CMYK12_8
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(1);
        public static readonly uint TYPE_CMYK12_16
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(2);
        public static readonly uint TYPE_CMYK12_16_SE
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(2)|ENDIAN16_SH(1);
        public static readonly uint TYPE_KYMC12_8
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(1)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC12_16
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly uint TYPE_KYMC12_16_SE
                = COLORSPACE_SH(PixelType.MCH12)|CHANNELS_SH(12)|BYTES_SH(2)|DOSWAP_SH(1)|ENDIAN16_SH(1);

        // Colorimetric
        public static readonly uint TYPE_XYZ_16
                = COLORSPACE_SH(PixelType.XYZ)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly uint TYPE_Lab_8
                = COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(1);
        public static readonly uint TYPE_LabV2_8
                = COLORSPACE_SH(PixelType.LabV2)|CHANNELS_SH(3)|BYTES_SH(1);

        public static readonly uint TYPE_ALab_8
                = COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(1)|EXTRA_SH(1)|SWAPFIRST_SH(1);
        public static readonly uint TYPE_ALabV2_8
                = COLORSPACE_SH(PixelType.LabV2)|CHANNELS_SH(3)|BYTES_SH(1)|EXTRA_SH(1)|SWAPFIRST_SH(1);
        public static readonly uint TYPE_Lab_16
                = COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly uint TYPE_LabV2_16
                = COLORSPACE_SH(PixelType.LabV2)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly uint TYPE_Yxy_16
                = COLORSPACE_SH(PixelType.Yxy)|CHANNELS_SH(3)|BYTES_SH(2);

        // YCbCr
        public static readonly uint TYPE_YCbCr_8
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(1);
        public static readonly uint TYPE_YCbCr_8_PLANAR
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(1)|PLANAR_SH(1);
        public static readonly uint TYPE_YCbCr_16
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly uint TYPE_YCbCr_16_PLANAR
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(2)|PLANAR_SH(1);
        public static readonly uint TYPE_YCbCr_16_SE
                = COLORSPACE_SH(PixelType.YCbCr)|CHANNELS_SH(3)|BYTES_SH(2)|ENDIAN16_SH(1);

        // YUV
        public static readonly uint TYPE_YUV_8
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(1);
        public static readonly uint TYPE_YUV_8_PLANAR
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(1) | PLANAR_SH(1);
        public static readonly uint TYPE_YUV_16
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(2);
        public static readonly uint TYPE_YUV_16_PLANAR
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(2) | PLANAR_SH(1);
        public static readonly uint TYPE_YUV_16_SE
                = COLORSPACE_SH(PixelType.YUV) | CHANNELS_SH(3) | BYTES_SH(2) | ENDIAN16_SH(1);

        // HLS
        public static readonly uint TYPE_HLS_8
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(1);
        public static readonly uint TYPE_HLS_8_PLANAR
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(1) | PLANAR_SH(1);
        public static readonly uint TYPE_HLS_16
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(2);
        public static readonly uint TYPE_HLS_16_PLANAR
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(2) | PLANAR_SH(1);
        public static readonly uint TYPE_HLS_16_SE
                = COLORSPACE_SH(PixelType.HLS) | CHANNELS_SH(3) | BYTES_SH(2) | ENDIAN16_SH(1);

        // HSV
        public static readonly uint TYPE_HSV_8
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(1);
        public static readonly uint TYPE_HSV_8_PLANAR
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(1) | PLANAR_SH(1);
        public static readonly uint TYPE_HSV_16
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(2);
        public static readonly uint TYPE_HSV_16_PLANAR
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(2) | PLANAR_SH(1);
        public static readonly uint TYPE_HSV_16_SE
                = COLORSPACE_SH(PixelType.HSV) | CHANNELS_SH(3) | BYTES_SH(2) | ENDIAN16_SH(1);

        // Named color index. Only 16 bits allowed (don't check colorspace)
        public static readonly uint TYPE_NAMED_COLOR_INDEX
                = CHANNELS_SH(1)|BYTES_SH(2);

        // Floating point formatters
        public static readonly uint TYPE_XYZ_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.XYZ)|CHANNELS_SH(3)|BYTES_SH(4);
        public static readonly uint TYPE_Lab_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(4);
        public static readonly uint TYPE_LabA_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Lab)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4);
        public static readonly uint TYPE_GRAY_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(4);
        public static readonly uint TYPE_RGB_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(4);

        public static readonly uint TYPE_RGBA_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4);
        public static readonly uint TYPE_ARGB_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4)|SWAPFIRST_SH(1);
        public static readonly uint TYPE_BGR_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(4)|DOSWAP_SH(1);
        public static readonly uint TYPE_BGRA_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4)|DOSWAP_SH(1)|SWAPFIRST_SH(1);
        public static readonly uint TYPE_ABGR_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(4)|DOSWAP_SH(1);

        public static readonly uint TYPE_CMYK_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(4);

        // Double precision floating point formatters
        // NOTE: The 'bytes' field is set to zero to avoid overflowing the bitfield
        public static readonly uint TYPE_XYZ_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.XYZ)|CHANNELS_SH(3)|BYTES_SH(0);
        public static readonly uint TYPE_Lab_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Lab)|CHANNELS_SH(3)|BYTES_SH(0);
        public static readonly uint TYPE_GRAY_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(0);
        public static readonly uint TYPE_RGB_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(0);
        public static readonly uint TYPE_BGR_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(0)|DOSWAP_SH(1);
        public static readonly uint TYPE_CMYK_DBL
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(0);

        // IEEE 754-2008 "half"
        public static readonly uint TYPE_GRAY_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.Gray)|CHANNELS_SH(1)|BYTES_SH(2);
        public static readonly uint TYPE_RGB_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly uint TYPE_RGBA_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2);
        public static readonly uint TYPE_CMYK_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.CMYK)|CHANNELS_SH(4)|BYTES_SH(2);

        public static readonly uint TYPE_ARGB_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|SWAPFIRST_SH(1);
        public static readonly uint TYPE_BGR_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1);
        public static readonly uint TYPE_BGRA_HALF_FLT
                = FLOAT_SH(1)|COLORSPACE_SH(PixelType.RGB)|EXTRA_SH(1)|CHANNELS_SH(3)|BYTES_SH(2)|DOSWAP_SH(1)|SWAPFIRST_SH(1);
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
