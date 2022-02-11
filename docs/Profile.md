# Profile Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents an International Color Consortium Profile. This class cannot be inherited.

```csharp
public sealed class Profile : CmsHandle<Profile>
```

Inheritance Object → CmsHandle\<Profile> → Profile

Implements IDisposable

## Properties
## ColorSpace Property

```csharp
public ColorSpaceSignature ColorSpace { get; set; }
```

### Property Value

`ColorSpace` [ColorSpaceSignature](./ColorSpaceSignature.md)  
Gets or sets the color space used by the profile.

---
## Context Property

```csharp
public Context Context { get; }
```

### Property Value

`Context` [Context](./Context.md)  
Gets the context in which the instance was created. Inherited from `CmsHandle<T>`.

---
## DeviceClass Property

```csharp
public ProfileClassSignature DeviceClass { get; set; }
```

### Property Value

`DeviceClass` [ProfileClassSignature](./ProfileClassSignature.md)  
Gets or sets the device class signature in the header of the profile.

---
## EncodedICCVersion Property

```csharp
public uint EncodedICCVersion { get; set; }
```

### Property Value

`EncodedICCVersion` uint  
Gets or sets the ICCversion in the format as stored in the header of the profile.

---
## HeaderAttributes Property

```csharp
public DeviceAttributes HeaderAttributes { get; set; }
```

### Property Value

`HeaderAttributes` [DeviceAttributes](./DeviceAttributes.md)  
Gets or sets the attributes unique to the particular device setup for which the profile is applicable in the header of the profile.

---
## HeaderFlags Property

```csharp
public uint HeaderFlags { get; set; }
```

### Property Value

`HeaderFlags` uint  
Gets or sets the flags in the header of the profile.

---
## HeaderManufacturer Property

```csharp
public uint HeaderManufacturer { get; }
```

### Property Value

`HeaderManufacturer` uint  
Gets or sets the signature of the device manufacturer in the header of the profile.

---
## HeaderModel Property

```csharp
public uint HeaderFlags { get; set; }
```

### Property Value

`HeaderModel` uint  
Gets or sets the signature of the device model in the header of the profile.

---
## HeaderProfileID Property

```csharp
public byte[] HeaderProfileID { get; set; }
```

### Property Value

`HeaderProfileID` byte[]  
Gets or sets the profile ID in the header of the profile.
An array of 16 bytes defining the computed MD5 value for the profile.

### Remarks

The profile ID shall be calculated using the MD5 fingerprinting method as defined in Internet RFC 1321.

The profile ID can be calculated and set using `ComputeMD5()`.

---
## HeaderRenderingIntent Property

```csharp
public Intent HeaderRenderingIntent { get; set; }
```

### Property Value

`HeaderRenderingIntent` [Intent](./Intent.md)  
Gets or sets the rendering intent in the header of the profile.

---
## IOHandler Property

```csharp
public IOHandler IOHandler { get; }
```

### Property Value

`IOHandler` [IOHandler](./IOHandler.md)  
Gets the I/O handler used by the profile.

---
## IsDisposed Property

```csharp
public bool IsDisposed { get; }
```

### Property Value

`IsDisposed` bool  
Gets a value indicating whether the instance has been disposed.

---
## IsMatrixShaper  Property

```csharp
public bool IsMatrixShaper { get; }
```

### Property Value

`IsMatrixShaper` bool  
Gets a value indicating whether a matrix shaper is present in the profile.

---
## PCS Property

```csharp
public ColorSpaceSignature PCS { get; }
```

### Property Value

`PCS` [ColorSpaceSignature](./ColorSpaceSignature.md)  
Gets or sets the profile connection space used by the profile.

---
## TagCount Property

```csharp
public int TagCount { get; }
```

### Property Value

`TagCount` int  
Gets the number of tags in the profile.

---
## TotalAreaCoverage Property

```csharp
public double TotalAreaCoverage { get; }
```

### Property Value

`TotalAreaCoverage` double  
Estimates the percentage total area coverage (total dot percentage) for the profile.

### Remarks

Non-output profiles yield a value of 0%.

---
## Version Property

```csharp
public double Version { get; set; }
```

### Property Value

`Version` double  
Gets or sets the version in the header of the profile in floating point format.

## Methods
## ComputeMD5() Method

```csharp
public bool ComputeMD5()
```

Calculates and sets the profile ID in the header of the profile.

### Returns

`bool`  
`true` if successful, otherwise `false`.

---
## Create_sRGB(Context) Method

```csharp
public static Profile Create(Context context = null)
```

Creates a new instance of the `Profile` class for the
sRGB color space in the given context.

### Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

### Returns

`Profile`  
A new `Profile` instance.

---
## Create_BCHSWabstract(int, double, double, double, double, int, int) Method

```csharp
public static Profile Create(int nLutPoints, double bright, double contrast, double hue, double saturation, int tempSrc, int tempDest)
```

Creates a new instance of the `Profile` class for an
abstract device link profile operating in Lab for brightness/contrast/hue/saturation and white point translation.

### Parameters

`nLutPoints` int  
Number of LUT points.

`bright` double  
Brightness increment, can be negative.

`contrast` double  
Contrast increment, can be negative.

`hue` double  
Hue displacement in degrees.

`saturation` double  
Saturation increment, can be negative.

`tempSrc` int  
Source white point temperature in °K.

`tempDest` int  
Destination white point temperature in °K.

### Returns

`Profile`  
A new `Profile` instance.

### Remarks

Creates the instance in the global context.

---
## Create_BCHSWabstract(Context, int, double, double, double, double, int, int) Method

```csharp
public static Profile Create(Context context, int nLutPoints, double bright, double contrast, double hue, double saturation, int tempSrc, int tempDest)
```

Creates a new instance of the `Profile` class for an
abstract device link profile operating in Lab for brightness/contrast/hue/saturation and white point translation.

### Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

`nLutPoints` int  
Number of LUT points.

`bright` double  
Brightness increment, can be negative.

`contrast` double  
Contrast increment, can be negative.

`hue` double  
Hue displacement in degrees.

`saturation` double  
Saturation increment, can be negative.

`tempSrc` int  
Source white point temperature in °K.

`tempDest` int  
Destination white point temperature in °K.

### Returns

`Profile`  
A new `Profile` instance.

---
## CreateDeviceLink(Transform, double, CmsFlags) Method

```csharp
public static CreateDeviceLink(Transform transform, double version, CmsFlags flags)
```

Creates a new instance of the `Profile` class for a device link profile from a given color transform.

### Parameters

`transform` [Transform](./Transform.md)  
A transform.

`version` double  
The target device link version number in the range 1 .. 4.3.

`flags` [CmsFlags](./CmsFlags.md)  
Bit-wise combination of flags.

### Returns

`Profile`  
A new `Profile` instance.

### Remarks

Creates the instance in the same context as the transform.

---
## CreateGray(CIExyY, ToneCurve) Method

```csharp
public static CreateGray(CIExyY whitePoint, ToneCurve transferFunction)
```

Creates a new instance of the `Profile` class for a gray profile.

### Parameters

`whitePoint` [CIExyY](./CIExyY.md)  
The white point of the gray device or color space.

`transferFunction` [ToneCurve](./ToneCurve.md)  
A tone curve describing the device or color space gamma.

### Returns

`Profile`  
A new `Profile` instance.

### Remarks

Creates the instance in the global context.

---
## CreateGray(Context, CIExyY, ToneCurve) Method

```csharp
public static CreateGray(Context context, CIExyY whitePoint, ToneCurve transferFunction)
```

Creates a new instance of the `Profile` class for a gray profile.

### Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

`whitePoint` [CIExyY](./CIExyY.md)  
The white point of the gray device or color space.

`transferFunction` [ToneCurve](./ToneCurve.md)  
A tone curve describing the device or color space gamma.

### Returns

`Profile`  
A new `Profile` instance.

---
## CreateInkLimitingDeviceLink(ColorSpaceSiganture, double) Method

```csharp
public static CreateInkLimitingDeviceLink(ColorSpaceSignature space, double limit)
```

Creates a new instance of the `Profile` class for a device
link profile operating in CMYK for ink limiting.

### Parameters

`space` [ColorSpaceSignature](./ColorSpaceSignature.md)  
The color space.

`limit` double  
The amount of ink limiting in % in the range 0 .. 400%.

### Returns

`Profile`  
A new `Profile` instance.

### Remarks

Only `ColorSpaceSignature.CmykData` is supported.

Creates the instance in the global context.

---
## CreateInkLimitingDeviceLink(Context, ColorSpaceSiganture, double) Method

```csharp
public static CreateInkLimitingDeviceLink(Context context, ColorSpaceSignature space, double limit)
```

Creates a new instance of the `Profile` class for a device
link profile operating in CMYK for ink limiting.

### Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

`space` [ColorSpaceSignature](./ColorSpaceSignature.md)  
The color space.

`limit` double  
The amount of ink limiting in % in the range 0 .. 400%.

### Returns

`Profile`  
A new `Profile` instance.

### Remarks

Only `ColorSpaceSignature.CmykData` is supported.

---
## CreateLab2(CIExyY) Method

```csharp
public static CreateLab2(CIExyY whitePoint)
```

Creates a new instance of the `Profile` class for a Lab → Lab identity v2 profile.

### Parameters

`whitePoint` [CIExyY](./CIExyY.md)  
Lab reference white.

### Returns

`Profile`  
A new `Profile` instance.

### Remarks

Creates the instance in the global context.

---
## CreateLab2(Context, CIExyY) Method

```csharp
public static CreateLab2(Context context, CIExyY whitePoint)
```

Creates a new instance of the `Profile` class for a Lab → Lab identity v2 profile.

### Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

`whitePoint` [CIExyY](./CIExyY.md)  
Lab reference white.

### Returns

`Profile`  
A new `Profile` instance.

---
## CreateLab4(CIExyY) Method

```csharp
public static CreateLab4(CIExyY whitePoint)
```

Creates a new instance of the `Profile` class for a Lab → Lab identity v4 profile.

### Parameters

`whitePoint` [CIExyY](./CIExyY.md)  
Lab reference white.

### Returns

`Profile`  
A new `Profile` instance.

### Remarks

Creates the instance in the global context.

---
## CreateLab4(Context, CIExyY) Method

```csharp
public static CreateLab4(Context context, CIExyY whitePoint)
```

Creates a new instance of the `Profile` class for a Lab → Lab identity v4 profile.

### Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

`whitePoint` [CIExyY](./CIExyY.md)  
Lab reference white.

### Returns

`Profile`  
A new `Profile` instance.

---
## CreateLinearizationDeviceLink(ColorSpaceSignature, ToneCurve[]) Method

```csharp
public static CreateLinearizationDeviceLink(ColorSpaceSignature space, ToneCurve[] transferFunction)
```

Creates a new instance of the `Profile` class for a device link profile.

### Parameters

`space` [ColorSpaceSignature](./ColorSpaceSignature.md)  
The color space.

`transferFunction` [ToneCurve[]](./ToneCurve.md)  
An array of tone curves describing the device or color space linearization.

### Returns

`Profile`  
A new `Profile` instance.

### Remarks

Creates the instance in the global context.

---
## CreateLinearizationDeviceLink(Context, ColorSpaceSignature, ToneCurve[]) Method

```csharp
public static CreateLinearizationDeviceLink(Context context, ColorSpaceSignature space, ToneCurve[] transferFunction)
```

Creates a new instance of the `Profile` class for a device link profile.

### Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

`space` [ColorSpaceSignature](./ColorSpaceSignature.md)  
The color space.

`transferFunction` [ToneCurve[]](./ToneCurve.md)  
An array of tone curves describing the device or color space linearization.

### Returns

`Profile`  
A new `Profile` instance.

---
## CreateNull(Context) Method

```csharp
public static CreateNull(Context context = null)
```

Creates a new instance of the `Profile` class for a null profile in the given context.

### Parameters

`Context` [Context](./Context.md)  
A context, or null for the global context.

### Returns

`Profile`  
A new `Profile` instance.

---
## CreatePlaceholder(Context) Method

```csharp
public static CreatePlaceholder(Context context = null)
```

Creates a new instance of the `Profile` class for an empty profile.

### Parameters

`Context` [Context](./Context.md)  
A context, or null for the global context.

### Returns

`Profile`  
A new `Profile` instance.

### Remarks

The profile must be populated before it can be used.

---
## CreateRGB(CIExyY, CIExyYTRIPLE, ToneCurve[]) Method

```csharp
public static CreateRGB(CIExyY whitePoint, CIExyYTRIPLE primaries, ToneCurve[] transferFunction)
```

Creates a new instance of the `Profile` class for a display RGB profile.

### Parameters

`whitePoint` [CIExyY](./CIExyY.md)  
The white point of the RGB device or color space.

`primaries` [CIExyYTRIPLE](./CIExyYTRIPLE.md)  
The primaries in xyY of the device or color space.

`transferFunction` [ToneCurve[]](./ToneCurve.md)  
An array of 3 tone curves describing the device or color space gamma.

### Returns

`Profile`  
A new `Profile` instance.

### Remarks

Creates the instance in the global context.

---
## CreateRGB(Context, CIExyY, CIExyYTRIPLE, ToneCurve[]) Method

```csharp
public static CreateRGB(Context context, CIExyY whitePoint, CIExyYTRIPLE primaries, ToneCurve[] transferFunction)
```

Creates a new instance of the `Profile` class for a display RGB profile.

### Parameters

`Context` [Context](./Context.md)  
A context, or null for the global context.

`whitePoint` [CIExyY](./CIExyY.md)  
The white point of the RGB device or color space.

`primaries` [CIExyYTRIPLE](./CIExyYTRIPLE.md)  
The primaries in xyY of the device or color space.

`transferFunction` [ToneCurve[]](./ToneCurve.md)  
An array of 3 tone curves describing the device or color space gamma.

### Returns

`Profile`  
A new `Profile` instance.

---
## CreateXYZ(Context) Method

```csharp
public static CreateXYZ(Context context)
```

Creates a new instance of the `Profile` class for a
XYZ → XYZ identity v4 profile in the given context.

### Parameters

`Context` [Context](./Context.md)  
A context, or null for the global context.

### Returns

`Profile`  
A new `Profile` instance.

---
## DetectBlackPoint(CIEXYZ, Intent, CmsFlags) Method

```csharp
public bool DetectBlackPoint(out CIEXYZ blackPoint, Intent intent, CmsFlags flags = 0)
```

Estimates the black point of the profile.

### Parameters

`blackPoint` [CIEXYZ](./CIEXYZ.md)  
Returns the black point.

`intent` [Intent](./Intent.md)  
The intent.

`flags` [CmsFlags](./CmsFlags.md)  
Reserved (unused). Set to `CmsFlags.None`.

### Returns

`bool`
`true` if estimated successfully, otherwise `false`.

---
## DetectDestinationBlackPoint(CIEXYZ, Intent, CmsFlags) Method

```csharp
public bool DetectDestinationBlackPoint(out CIEXYZ blackPoint, Intent intent, CmsFlags flags = 0)
```

Estimates the black point of the profile by using the ICC black point compensation algorithm.

### Parameters

`blackPoint` [CIEXYZ](./CIEXYZ.md)  
Returns the black point.

`intent` [Intent](./Intent.md)  
The intent.

`flags` [CmsFlags](./CmsFlags.md)  
Reserved (unused). Set to `CmsFlags.None`.

### Returns

`bool`
`true` if estimated successfully, otherwise `false`.

---
## DetectRGBGamma(double) Method

```csharp
public double DetectRGBGamma(double threshold)
```

Detects whether the profile works in linear (gamma 1.0) space.

### Parameters

`threshold` double  
The standard deviation above which gamma is returned.

### Returns

`double`  
Estimated gamma of the RGB space on success, -1 on error.

### Remarks

Only RGB profiles, and only those that can be got in both directions.

Requires Little CMS 2.13 or later.

---
## Dispose() Method

```csharp
public void Dispose()
```

Disposes this instance.

---
## GetHeaderCreationDateTime(DateTime) Method

```csharp
public bool GetHeaderCreatonDateTime(out DateTime created)
```

Gets the date and time when the profile was created.

### Parameters

`created` DateTime  
Returns the date and time the profile was created.

### Returns

`bool`  
`true` if successful, otherwise `false`.

---
## GetPostScriptColorRenderingDictionary(Context, Intent, CmsFlags) Method

```csharp
public byte[] GetPostScriptColorRenderingDictionary(Context context, Intent intent, CmsFlags flags)
```

Creates and returns a contiguous block of memory containing a PostScript color rendering dictionary.

### Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

`intent` [Intent](./Intent.md)  
The intent.

`flags` [CmsFlags](./CmsFlags.md)  
The flags.

### Returns

`byte[]`  
An array of bytes containing the PostScript color rendering dictionary, or `null` on error.

---
## GetPostScriptColorResource(Context, PostScriptResourceType, Intent, CmsFlags, IOHandler) Method

```csharp
public uint GetPostScriptColorResource(Context context, PostScriptResourceType type, Intent intent, CmsFlags flags, IOHandler handler)
```

Creates a PostScript color resource.

### Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

`type` [PostScriptResourceType](./PostScriptResourceType.md)  
The PostScript resource type to be created.

`intent` [Intent](./Intent.md)  
The intent.

`flags` [CmsFlags](./CmsFlags.md)  
The flags.

`handler` [IOHandler](./IOHandler.md)  
An I/O handler.

### Returns

`uint`  
The size of the resource in bytes, or 0 on error.

---
## GetPostScriptColorSpaceArray(Context, Intent, CmsFlags) Method

```csharp
public byte[] GetPostScriptColorSpaceArray(Context context, Intent intent, CmsFlags flags)
```

Creates and returns a contiguous block of memory containing a PostScript color space array.

### Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

`intent` [Intent](./Intent.md)  
The intent.

`flags` [CmsFlags](./CmsFlags.md)  
The flags.

### Returns

`byte[]`  
An array of bytes containing the PostScript color space array, or `null` on error.

---
## GetProfileInfo(InfoType, string, string) Method

```csharp
public string GetProfileInfo(InfoType info, string languageCode, string countryCode)
```

Gets a Unicode (16 bit) string containing the requested information from the profile for a given language and country code.

### Parameters

`info` [InfoType](./InfoType.md)  
The information to be obtained.

`languageCode` string  
The ISO 639-1 language code.

`countryCode` string  
The ISO 3166-1 country code.

### Returns

`string`  
A string containing the requested information, or null if not found.

---
## GetProfileInfoASCII(InfoType, string, string) Method

```csharp
public string GetProfileInfoASCII(InfoType info, string languageCode, string countryCode)
```

Gets an ASCII (7 bit) string containing the requested information from the profile for a given language and country code.

### Parameters

`info` [InfoType](./InfoType.md)  
The information to be obtained.

`languageCode` string  
The ISO 639-1 language code.

`countryCode` string  
The ISO 3166-1 country code.

### Returns

`string`  
A string containing the requested information, or null if not found.

---
## GetTag(uint) Method

```csharp
public TagSignature GetTag(uint n)
```

Gets the tag for the given index.

### Parameters

`n` uint  
The zero-based index of the tag.

### Returns

`TagSignature`  
The tag.

---
## HasTag(TagSignature) Method

```csharp
public bool HasTag(TagSignature tag)
```

Gets a value indicating whether the tag is present in the profile.

### Parameters

`tag` [TagSignature](./TagSignature.md)  
The tag signature.

### Returns

`bool`  
`true` if the tag is present, otherwise `false`.

---
## IsCLUT(Intent, UsedDirection) Method

```csharp
public bool IsCLUT(Intent intent, UsedDirection direction)
```

Determines whether the profile contains a CLUT for the given intent and direction.

### Parameters

`intent` [Intent](./Intent.md)  
The intent.

`direction` [UsedDirection](./UsedDirection.md)  
The direction.

### Returns

`bool`  
`true` if a CLUT is present, otherwise `false`.

---
## IsIntentSupported(Intent, UsedDirection) Method

```csharp
public bool IsIntentSupported(Intent intent, UsedDirection direction)
```

Gets a value indicating whether the given intent is implemented in the profile for the supplied direction.

### Parameters

`intent` [Intent](./Intent.md)  
The intent.

`direction` [UsedDirection](./UsedDirection.md)  
The direction.

### Returns

`bool`  
`true` if implemented, otherwise `false`.

---
## LinkTag(TagSignature, TagSignature) Method

```csharp
public bool LinkTag(TagSignature tag, TagSignature dest)
```

Creates a directory entry on tag `tag` that points to the same location as tag `dest` to collapse several tag entries to the same block in the profile.

### Parameters

`tag` [TagSignature](./TagSignature.md)  
The tag signature of the linking tag.

`dest` [TagSignature](./TagSignature.md)  
The tag signature of the linked tag.

### Returns

`bool`  
`true` if linked successfully, otherwise `false`.

---
## Open(byte[]) Method

```csharp
public static Profile Open(byte[] memory)
```

Creates a new instance of the `Profile` class from the supplied memory block.

### Parameters

`memory` byte[]  
A block of contiguous memory containing the entire ICC profile.

### Returns

`Profile`  
A new `Profile` instance.

### Remarks

Creates the instance in the global context.

---
## Open(Context, byte[]) Method

```csharp
public static Profile Open(Context context, byte[] memory)
```

Creates a new instance of the `Profile` class from the supplied memory block in the given context.

### Parameters

`Context` [Context](./Context.md)  
A context, or null for the global context.

`memory` byte[]  
A block of contiguous memory containing the entire ICC profile.

### Returns

`Profile`  
A new `Profile` instance.

---
## Open(Context, IOHandler) Method

```csharp
public static Profile Open(Context context, IOHandler iohandler)
```

Creates a new instance of the `Profile` class where profile access is described by an I/O handler.

### Parameters

`Context` [Context](./Context.md)  
A context, or null for the global context.

`iohandler` [IOHandler](./IOHandler.md)  
An I/O handler.

### Returns

`Profile`  
A new `Profile` instance.

---
## Open(Context, IOHandler, bool) Method

```csharp
public static Profile Open(Context context, IOHandler iohandler, bool writeable)
```

Creates a new instance of the `Profile` class where profile access is described by an I/O handler that also allows write access to be specified.

### Parameters

`Context` [Context](./Context.md)  
A context, or null for the global context.

`iohandler` [IOHandler](./IOHandler.md)  
An I/O handler.

`writeable` bool  
`true` to grant write access, of `false` to open the I/O handler as read-only.

### Returns

`Profile`  
A new `Profile` instance.

---
## Open(Context, string, string) Method

```csharp
public static Profile Open(Context context, string filepath, string access)
```

Creates a new instance of the `Profile` class for a file-based ICC profile in the given context.

### Parameters

`Context` [Context](./Context.md)  
A context, or null for the global context.

`filepath` string  
The full path to the file.

`access` string  
"r" for normal operation, or "w" to create the file.

### Returns

`Profile`  
A new `Profile` instance.

---
## Open(string, string) Method

```csharp
public static Profile Open(string filepath, string access)
```

Creates a new instance of the `Profile` class for a file-based ICC profile in the global context.

### Parameters

`filepath` string  
The full path to the file.

`access` string  
"r" for normal operation, or "w" to create the file.

### Returns

`Profile`  
A new `Profile` instance.

---
## ReadTag(TagSignature) Method

```csharp
public IntPtr ReadTag(TagSignature tag)
```

Gets a pointer to a tag with the given tag signature.

### Parameters

`tag` [TagSignature](./TagSignature.md)  
The tag signature.

### Returns

`IntPtr`  
A pointer to the tag, or `IntPtr.Zero` if not found.

---
## ReadTag<T>(TagSignature) Method

```csharp
public T ReadTag<T>(TagSignature tag)
```

Gets a new instance of type T that represents the tag with the given tag signature.

### Type Parameters

`T`  
The type for the given tag.

### Parameters

`tag` [TagSignature](./TagSignature.md)  
The tag signature.

### Returns

`T`  
A new instance of type T.

### Remarks

The type T must contain a non-public static method with signature `T FromHandle(IntPtr)`.

---
## Save(byte[], uint) Method

```csharp
public bool Save(byte[] memory, out uint bytesNeeded)
```

Saves the profile to a contiguous block of memory.

### Parameters

`memory` byte[]  
An array of bytes large enough to hold the profile, or null to calculate the required size.

`bytesNeeded` uint  
Returns the number of bytes written or required.

### Returns

`bool`  
`true` if successful, otherwise `false`.

### Remarks

The calculated size ignores the zero ('\0') terminator saved as the last byte in the memory block, so add 1 to ensure that the memory to save the profile is sufficiently sized.

---
## Save(IOHandler) Method

```csharp
public uint Save(IOHandler iohandler)
```

Saves the profile to the given I/O handler.

### Parameters

`iohandler` [IOHandler](./IOHandler.md)  
An I/O handler or null to calculate size only.

### Returns

`uint`  
The number of bytes used to save the profile, or zero on error.

---
## Save(string) Method

```csharp
public bool Save(string filepath)
```

Saves the profile to file.

### Parameters

`filepath` string  
The full path to the file.

### Returns

`bool`  
`true` if saved successfully, otherwise `false`.

---
## TagLinkedTo(TagSignature) Method

```csharp
public TagSignature TagLinkedTo(TagSignature tag)
```

Gets the tag signature of the tag linked to the given tag.

### Parameters

`tag` [TagSignature](./TagSignature.md)  
The tag signature of the linking tag.

### Returns

`TagSignature`  
The tag signature of the linked tag, or `(TagSignature)0` if not linked.

---
## WriteTag(TagSignature, ICCData) Method

```csharp
public bool WriteTag(TagSignature tag, ICCData data)
```

Writes an `ICCData` instance to the profile using the given tag signature.

### Parameters

`tag` [TagSignature](./TagSignature.md)  
The tag signature.

`data` [ICCData](./ICCData.md)  
The ICC data.

### Returns

`bool`  
`true` if written successfully, otherwise `false`.

---
## WriteTag(TagSignature, UcrBg) Method

```csharp
public bool WriteTag(TagSignature tag, UcrBg ucrBg)
```

Writes a `UcrBg` instance to the profile using the given tag signature.

### Parameters

`tag` [TagSignature](./TagSignature.md)  
The tag signature.

`ucrBg` [UcrBg](./UcrBg.md)  
The under color removal and black generation instance.

### Returns

`bool`  
`true` if written successfully, otherwise `false`.

---
## WriteTag(TagSignature, VideoCardGamma) Method

```csharp
public bool WriteTag(TagSignature tag, VideoCardGamma vcgt)
```

Writes a `VideoCardGamma` instance to the profile using the given tag signature.

### Parameters

`tag` [TagSignature](./TagSignature.md)  
The tag signature.

`vcgt` [VideoCardGamma](./VideoCardGamma.md)  
The video card gamma table instance.

### Returns

`bool`  
`true` if written successfully, otherwise `false`.

---
## WriteTag\<T>(TagSignature, TagBase\<T>) Method

```csharp
public bool WriteTag(TagSignature tag, TagBase<T> t)
```

Writes an object to the profile using the given tag signature.

### Type Parameters

`T`  
The tag type to be written.

### Parameters

`tag` [TagSignature](./TagSignature.md)  
The tag signature.

`t` TagBase\<T>  
A type derived from `TagBase<T>`.

### Returns

`bool`  
`true` if written successfully, otherwise `false`.

---
## WriteTag\<T>(TagSignature, T) Method

```csharp
public bool WriteTag<T>(TagSignature tag, T data) where T: struct
```

Writes a structure to the profile using the given tag signature.

### Type Parameters

`T`  
The structure type.

### Parameters

`tag` [TagSignature](./TagSignature.md)  
The tag signature.

`data` T  
The structure.

### Returns

`bool`  
`true` if written successfully, otherwise `false`.

### Remarks

The layout of the structure must correspond with the definitions in Little CMS.
