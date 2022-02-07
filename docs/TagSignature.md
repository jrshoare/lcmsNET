# TagSignature Enum

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Defines the base ICC tag definitions.

```csharp
public enum TagSignature : uint
```

Inheritance Object → ValueType → Enum → TagSignature

## Fields

| | | |
| ---- | ----:| ---- |
| ArgyllArts | 0x61727473 | 'arts' - read/write using [CIEXYZTRIPLE](./CIEXYZTRIPLE.md). |
| AToB0 | 0x41324230 | 'A2B0' - read/write using [Pipeline](./Pipeline.md). |
| AToB1 | 0x41324231 | 'A2B1' - read/write using [Pipeline](./Pipeline.md). |
| AToB2 | 0x41324232 | 'A2B2' - read/write using [Pipeline](./Pipeline.md). |
| BlueColorant | 0x6258595A | 'bXYZ' - read/write using [CIEXYZ](./CIEXYZ.md). |
| BlueMatrixColumn | 0x6258595A | 'bXYZ' - read/write using [CIEXYZ](./CIEXYZ.md). |
| BlueTRC | 0x62545243 | 'bTRC' - read/write using [ToneCurve](./ToneCurve.md). |
| BToA0 | 0x42324130 | 'B2A0' - read/write using [Pipeline](./Pipeline.md). |
| BToA1 | 0x42324131 | 'B2A1' - read/write using [Pipeline](./Pipeline.md). |
| BToA2 | 0x42324132 | 'B2A2' - read/write using [Pipeline](./Pipeline.md). |
| BToD0 | 0x42324430 | 'B2D0' - read/write using [Pipeline](./Pipeline.md). |
| BToD1 | 0x42324431 | 'B2D1' - read/write using [Pipeline](./Pipeline.md). |
| BToD2 | 0x42324432 | 'B2D2' - read/write using [Pipeline](./Pipeline.md). |
| BToD3 | 0x42324433 | 'B2D3' - read/write using [Pipeline](./Pipeline.md). |
| CalibrationDateTime | 0x63616C74 | 'calt' - read/write using [Tm](./Tm.md). |
| CharTarget | 0x74617267 | 'targ' - read/write using [MultiLocalizedUnicode](./MultiLocalizedUnicode.md). |
| ChromaticAdaptation | 0x63686164 | 'chad' - read/write using [CIEXYZTRIPLE](./CIEXYZTRIPLE.md). |
| Chromaticity | 0x6368726D | 'chrm' - read/write using [CIExyYTRIPLE](./CIExyYTRIPLE.md). |
| ColorantOrder | 0x636C726F | 'clro' - read/write using [ColorantOrder](./ColorantOrder.md). |
| ColorantTable | 0x636C7274 | 'clrt' - read/write using [NamedColorList](./NamedColorList.md). |
| ColorantTableOut | 0x636C6F74 | 'clot' - read/write using [NamedColorList](./NamedColorList.md). |
| ColorimetricIntentImageState | 0x63696973 | 'ciis' - read/write using [Signature](./Signature.md). |
| Copyright | 0x63707274 | 'cprt' - read/write using [MultiLocalizedUnicode](./MultiLocalizedUnicode.md). |
| CrdInfo | 0x63726469 | 'crdi' - read/write using [NamedColorList](./NamedColorList.md). |
| Data | 0x64617461 | 'data' - Not supported. |
| DateTime | 0x6474696D | 'dtim' - read/write using [Tm](./Tm.md). |
| DeviceMfgDesc | 0x646D6E64 | 'dmnd' - read/write using [MultiLocalizedUnicode](./MultiLocalizedUnicode.md). |
| DeviceModelDesc | 0x646D6464 | 'dmdd' - read/write using [MultiLocalizedUnicode](./MultiLocalizedUnicode.md). |
| DeviceSettings | 0x64657673 | 'devs' - Not supported. |
| DToB0 | 0x44324230 | 'D2B0' - read/write using [Pipeline](./Pipeline.md). |
| DToB1 | 0x44324231 | 'D2B1' - read/write using [Pipeline](./Pipeline.md). |
| DToB2 | 0x44324232 | 'D2B2' - read/write using [Pipeline](./Pipeline.md). |
| DToB3 | 0x44324233 | 'D2B3' - read/write using [Pipeline](./Pipeline.md). |
| Gamut | 0x67616D74 | 'gamt' - read/write using [Pipeline](./Pipeline.md). |
| GrayTRC | 0x6b545243 | 'kTRC' - read/write using [ToneCurve](./ToneCurve.md). |
| GreenColorant | 0x6758595A | 'gXYZ' - read/write using [CIEXYZ](./CIEXYZ.md). |
| GreenMatrixColumn | 0x6758595A | 'gXYZ' - read/write using [CIEXYZ](./CIEXYZ.md). |
| GreenTRC | 0x67545243 | 'gTRG' - read/write using [ToneCurve](./ToneCurve.md). |
| Luminance | 0x6C756d69 | 'lumi' - read/write using [CIEXYZ](./CIEXYZ.md). |
| Measurement | 0x6D656173 | 'meas' - read/write using [ICCMeasurementConditions](./ICCMeasurementConditions.md). |
| MediaBlackPoint | 0x626B7074 | 'bkpt' - read/write using [CIEXYZ](./CIEXYZ.md). |
| MediaWhitePoint | 0x77747074 | 'wtpt' - read/write using [CIEXYZ](./CIEXYZ.md). |
| Meta | 0x6D657461 | 'meta' - read/write using [Dict](./Dict.md). |
| NamedColor | 0x6E636f6C | 'ncol' - Not supported. |
| NamedColor2 | 0x6E636C32 | 'ncl2' - read/write using [NamedColorList](./NamedColorList.md). |
| OutputResponse | 0x72657370 | 'resp' - Not supported. |
| PerceptualRenderingIntentGamut | 0x72696730 | 'rig0' - read/write using [Signature](./Signature.md). |
| Preview0 | 0x70726530 | 'pre0' - read/write using [Pipeline](./Pipeline.md). |
| Preview1 | 0x70726531 | 'pre1' - read/write using [Pipeline](./Pipeline.md). |
| Preview2 | 0x70726532 | 'pre2' - read/write using [Pipeline](./Pipeline.md). |
| ProfileDescription | 0x64657363 | 'desc' - read/write using [MultiLocalizedUnicode](./MultiLocalizedUnicode.md). |
| ProfileDescriptionML | 0x6473636d | 'dscm' - read/write using [MultiLocalizedUnicode](./MultiLocalizedUnicode.md). |
| ProfileSequenceDesc | 0x70736571 | 'pseq' - read/write using [ProfileSequenceDescriptor](./ProfileSequenceDescriptor.md). |
| ProfileSequenceId | 0x70736964 | 'psid' - read/write using [ProfileSequenceDescriptor](./ProfileSequenceDescriptor.md). |
| Ps2CRD0 | 0x70736430 | 'psd0' - read/write using [ICCData](./ICCData.md). |
| Ps2CRD1 | 0x70736431 | 'psd1' - read/write using [ICCData](./ICCData.md). |
| Ps2CRD2 | 0x70736432 | 'psd2' - read/write using [ICCData](./ICCData.md). |
| Ps2CRD3 | 0x70736433 | 'psd3' - read/write using [ICCData](./ICCData.md). |
| Ps2CSA | 0x70733273 | 'ps2s' - read/write using [ICCData](./ICCData.md). |
| Ps2RenderingIntent | 0x70733269 | 'ps2i' - read/write using [ICCData](./ICCData.md). |
| RedColorant | 0x7258595A | 'rXYZ' - read/write using [CIEXYZ](./CIEXYZ.md). |
| RedMatrixColumn | 0x7258595A | 'rXYZ' - read/write using [CIEXYZ](./CIEXYZ.md). |
| RedTRC | 0x72545243 | 'rTRC' - read/write using [ToneCurve](./ToneCurve.md). |
| SaturationRenderingIntentGamut | 0x72696732 | 'rig2' - read/write using [Signature](./Signature.md). |
| Screening | 0x7363726E | 'scrn' - read/write using [Screening](./Screening.md). |
| ScreeningDesc | 0x73637264 | 'scrd' - read/write using [MultiLocalizedUnicode](./MultiLocalizedUnicode.md). |
| Technology | 0x74656368 | 'tech' - read/write using [Signature](./Signature.md). |
| UcrBg | 0x62666420 | 'bfd ' - read/write using [UcrBg](./UcrBg.md). |
| Vcgt | 0x76636774 | 'vcgt' - read/write using [VideoCardGamma](./VideoCardGamma.md). |
| ViewingCondDesc | 0x76756564 | 'vued' - read/write using [MultiLocalizedUnicode](./MultiLocalizedUnicode.md). |
| ViewingConditions | 0x76696577 | 'view' - read/write using [ICCViewingConditions](./ICCViewingConditions.md). |
