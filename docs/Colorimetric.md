# Colorimetric Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Provides static methods for color space conversion. This class cannot be inherited.

```csharp
public static class Colorimetric
```

Inheritance Object → Colorimetric

## Examples

The following code example demonstrates how to convert a [CIEXYZ](./CIEXYZ) value to [CIELAB](./CIELab) and that value to [CIELCh](./CIELCh) and back to [CIEXYZ](./CIEXYZ).

```csharp
using lcmsNET;

CIEXYZ whitePoint = new CIEXYZ { X = 0.9642, Y = 1.0, Z = 0.8249 }; // D50 XYZ normalized to Y = 1.0
CIEXYZ xyz = new CIEXYZ { X = 0.9642, Y = 1.0, Z = 0.8249 };

CIELab lab = Colorimetric.XYZ2Lab(whitePoint, xyz);
CIELCh lch = Colorimetric.Lab2LCh(lab);
CIEXYZ xyz2 = Colorimetric.Lab2XYZ(whitePoint, lab);
```

## Properties
## D50_XYZ Property

```csharp
public static CIEXYZ D50_XYZ { get; }
```

### Property Value

`D50_XYZ`  
Gets the D50 theoretical light source as a [CIEXYZ](./CIEXYZ) value.

---
## D50_xyY Property

```csharp
public static CIExyY D50_xyY { get; }
```

### Property Value

`D50_XYZ`  
Gets the D50 theoretical light source as a [CIExyY](./CIExyY) value.

## Methods
## XYZ2Lab(CIEXYZ, CIEXYZ) Method

```csharp
public static CIELab XYZ2Lab(in CIEXYZ whitePoint, in CIEXYZ xyz)
```

Converts a CIE 1931 XYZ color space value to CIELAB.

### Parameters
`whitePoint` [CIEXYZ](./CIEXYZ)  
The white point to use in the conversion.

`xyz` [CIEXYZ](./CIEXYZ)  
The CIE 1931 XYZ color space value to be converted.

---
## Lab2XYZ(CIEXYZ, CIELab) Method

```csharp
public static CIEXYZ Lab2XYZ(in CIEXYZ whitePoint, in CIELab lab)
```

Converts a CIELAB color space value to CIE 1931 XYZ.

### Parameters
`whitePoint` [CIEXYZ](./CIEXYZ)  
The white point to use in the conversion.

`lab` [CIELab](./CIELab)  
The CIELAB color space value to be converted.

---
## Lab2LCh(CIELab) Method

```csharp
public static CIELCh Lab2LCh(in CIELab lab)
```

Converts a CIELAB color space value to CIE LCh.

### Parameters
`lab` [CIELab](./CIELab)  
The CIELAB color space value to be converted.

---
## LCh2Lab(CIELCh) Method

```csharp
public static CIELab LCh2Lab(in CIELCh lch)
```

Converts a CIE LCh color space value to CIELAB.

### Parameters
`lch` [CIELCh](./CIELCh)  
The CIE LCh color space value to be converted.

---
## LabEncoded2Float(ushort[]) Method

```csharp
public static CIELab LabEncoded2Float(ushort[] wLab)
```

Converts an Lab value encoded in ICC v4 convention to a CIELAB value.

### Parameters
`wLab` `ushort[]`  
Array of 3 unsigned shorts holding the encoded values.

### Exceptions
`ArgumentException`  
Array size must equal 3.

---
## Float2LabEncoded(CIELab) Method

```csharp
public static ushort[] Float2LabEncoded(in CIELab lab)
```

Encodes a CIELAB value to ICC v4 convention.

### Parameters
`lab` [CIELab](./CIELab)  
The CIELAB color space value to be encoded.

---
## LabEncoded2FloatV2(ushort[]) Method

```csharp
public static CIELab LabEncoded2FloatV2(ushort[] wLab)
```

Converts an Lab value encoded in ICC v2 convention to a CIELAB value.

### Parameters
`wLab` `ushort[]`  
Array of 3 unsigned shorts holding the encoded values.

### Exceptions
`ArgumentException`  
Array size must equal 3.

---
## Float2LabEncodedV2(CIELab) Method

```csharp
public static ushort[] Float2LabEncodedV2(in CIELab lab)
```

Encodes a CIELAB value to ICC v2 convention.

### Parameters
`lab` [CIELab](./CIELab)  
The CIELAB color space value to be encoded.

---
## XYZEncoded2Float(ushort[]) Method

```csharp
public static CIELab XYZEncoded2Float(ushort[] xyz)
```

Converts an XYZ value encoded in ICC convention to a CIELAB value.

### Parameters
`xyz` `ushort[]`  
Array of 3 unsigned shorts holding the encoded values.

### Exceptions
`ArgumentException`  
Array size must equal 3.

---
## Float2XYZEncoded(CIEXYZ) Method

```csharp
public static ushort[] Float2XYZEncoded(in CIEXYZ fxyz)
```

Encodes a CIE 1931 XYZ value to ICC convention.

### Parameters
`fxyz` [CIEXYZ](./CIEXYZ)  
The CIE 1931 XYZ color space value to be encoded.

---
## Desaturate(ref CIELab, double, double, double, double) Method

```csharp
public static bool Desaturate(ref CIELab lab, double aMax, double aMin, double bMax, double bMin)
```

Performs gamut mapping on the supplied CIELAB value.

### Parameters
`lab` [CIELab](./CIELab)  
The CIELAB color space value to be mapped.

`aMax` `double`  
The maximum a* gamut boundary.

`aMin` `double`  
The minimum a* gamut boundary.

`bMax` `double`  
The maximum b* gamut boundary.

`bMin` `double`  
The minimum b* gamut boundary.
