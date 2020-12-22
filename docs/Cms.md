# Cms Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Provides static methods for global use with this library. This class cannot be inherited.

```csharp
public sealed class Cms
```

Inheritance Object → CAM02

## Examples

The following code example demonstrates how to get the value of the
`LCMS_VERSION` constant defined in the `lcms2.h` header file.

```csharp
using lcmsNET;

int lcms_version = Cms.EncodedCMMVersion;
```

## Properties
## AdaptationState Property

```csharp
public static double AdaptationState { get; set; }
```

### Property Value

`AdaptationState`  
Gets or sets the global adaptation state for absolute colorimetric intent.

---
## AlarmCodes Property

```csharp
public static ushort[] AlarmCodes { get; set; }
```

### Property Value

`AlarmCodes`  
Gets or sets the global codes used to mark out-out-gamut on Proofing transforms.

### Exceptions
`ArgumentException`  
Array size must equal 16.

---
## EncodedCMMVersion Property

```csharp
public static int EncodedCMMVersion { get; }
```

### Property Value

`EncodedCMMVersion`  
The value of the `LCMS_VERSION` constant defined in the `lcms2.h` header file.


## Methods
## SetErrorHandler(ErrorHandler) Method

```csharp
public static void SetErrorHandler(ErrorHandler handler)
```

Set the global error handler. The handler replaces any existing handler.

### Parameters
`handler` [ErrorHandler](./ErrorHandler)  
The error handler.

---
## ToColorSpaceSignature(PixelType) Method

```csharp
public static ColorSpaceSignature ToColorSpaceSignature(PixelType pixelType)
```

Converts from Little CMS color space notation to ICC color space notation.

### Parameters
`pixelType` [PixelType](./PixelType)  
The value to be converted in Little CMS color space notation.

### Returns

`ColorSpaceSignature`  
The value converted to ICC color space notation.

---
## ToPixelType(ColorSpaceSignature) Method

```csharp
public static PixelType ToPixelType(ColorSpaceSignature space)
```

Converts from ICC color space notation to Little CMS color space notation.

### Parameters
`space` [ColorSpaceSignature](./ColorSpaceSignature)  
The value to be converted in ICC color space notation.

### Returns

`PixelType`  
The value converted to Little CMS color space notation.

---
## ChannelsOf(ColorSpaceSignature) Method

```csharp
public static uint ChannelsOf(ColorSpaceSignature space)
```

Returns the channel count for a given ICC color space.

### Parameters
`space` [ColorSpaceSignature](./ColorSpaceSignature)  
The ICC color space.

### Returns

`uint`  
The number of channels.

---
## WhitePointFromTemp(out CIExyY, double) Method

```csharp
public static bool WhitePointFromTemp(out CIExyY xyY, double tempK)
```

Correlates a black body chromaticity from given temperature in ºK. Valid range is 4000K-25000K.

### Parameters
`xyY` [CIExyY](./CIExyY)  
On return contains the resulting chromaticity.

`tempK` `double`  
The temperature in ºK.

### Returns

`bool`  
`true` if successful, otherwise `false`.

---
## TempFromWhitePoint(out double, CIExyY) Method

```csharp
public static bool TempFromWhitePoint(out double tempK, in CIExyY xyY)
```

Correlates a black body temperature in ºK from given chromaticity.

### Parameters
`xyY` [CIExyY](./CIExyY)  
The target chromaticity.

`tempK` `double`  
The resulting temperature in ºK.

### Returns

`bool`  
`true` if successful, otherwise `false`.
