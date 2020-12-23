# CmsFlags Enum

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Specifies a set of flags to command the whole process.


```csharp
[Flags]
public enum CmsFlags : uint
```

Inheritance Object → ValueType → Enum → CmsFlags

## Fields

| | | |
| --- | ---:| --- |
| None | 0x0000 | No flags. |
| NoCache | 0x0040 | Inhibit 1-pixel cache. |
| NoOptimize | 0x0100 | Inhibit optimizations. |
| NullTransform | 0x0200 | Don't transform anyway. |
| GamutCheck | 0x1000 | Out of gamut alarm. |
| SoftProofing | 0x4000 | Do softproofing. |
| BlackPointCompensation | 0x2000 | Black point compensation. |
| NoWhiteOnWhiteFixUp | 0x0004 | Don't fix scum dot. |
| HighResPreCalc | 0x0400 | Use more memory to give better accuracy. Use on linear XYZ. |
| LowResPreCalc | 0x0800 | Use less memory to minimize resources. |
| EightBitsDeviceLink | 0x0008 | Create 8 bit device links. |
| GuessDeviceClass | 0x0020 | Guess device class (for transform2devicelink). |
| KeepSequence | 0x0080 | Keep profile sequence for device link creation. |
| ForceCLut | 0x0002 | Force CLUT optimization. |
| CLutPostLinearization | 0x0001 | Create post-linearization tables if possible. |
| CLutPreLinearization | 0x0010 | Create pre-linearization tables if possible. |
| NoNegatives | 0x8000 | Prevent negative numbers in floating point transforms. |
| NoDefaultResourceDef | 0x01000000 | No default resource definition. |

## Remarks

To fine tune control over the number of grid points, `n`, use a value for `CmsFlags` of `(((n) & 0xFF) << 16)`.