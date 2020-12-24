# DeltaE Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Provides static methods for calculating various forms of color difference. This class cannot be inherited.

```csharp
public sealed class DeltaE
```

Inheritance Object → DeltaE

## Examples

The following code example demonstrates how to calculate CIE76, CMC, CIEDE2000, BFD and CIE94 color difference for a pair of CIELAB values.

```csharp
using lcmsNET;

CIELab lab1 = new CIELab { L = 100.0, a = 0.0, b = 0.0 };
CIELab lab2 = new CIELab { L = 87.5, a = 3.21, b = -16.7 };
double l = 2.0;
double c = 1.0;

var de76 = DeltaE.DE76(lab1, lab2);
var cmc = DeltaE.CMC(lab1, lab2, l, c);
var de2K = DeltaE.CIEDE2000(lab1, lab2);
var bfd = DeltaE.BFD(lab1, lab2);
var de76 = DeltaE.CIE94(lab1, lab2);
```

## Methods
## DE76(CIELab, CIELab) Method

```csharp
public static double DE76(in CIELab lab1, in CIELab lab2)
```

Calculates the difference between two color values using the delta-E 1976 method.

### Parameters
`lab1` [CIELab](./CIELab)  
The first color value.

`lab2` [CIELab](./CIELab)  
The second color value.

### Returns

`double`  
The dE76 metric value.

---
## CMC(CIELab, CIELab, double, double) Method

```csharp
public static double CMC(in CIELab lab1, in CIELab lab2, double l, double c)
```

Calculates the difference between two color values using the CMC method.

### Parameters
`lab1` [CIELab](./CIELab)  
The first color value.

`lab2` [CIELab](./CIELab)  
The second color value.

`l` `double`  
Lightness.

`c` `double`  
Chroma.

### Returns

`double`  
The dE CMC metric value.

---
## BFD(CIELab, CIELab) Method

```csharp
public static double BFD(in CIELab lab1, in CIELab lab2)
```

Calculates the difference between two color values using the BFD method.

### Parameters
`lab1` [CIELab](./CIELab)  
The first color value.

`lab2` [CIELab](./CIELab)  
The second color value.

### Returns

`double`  
The dE BFD metric value.

---
## CIE94(CIELab, CIELab) Method

```csharp
public static double CIE94(in CIELab lab1, in CIELab lab2)
```

Calculates the difference between two color values using the CIE94 method.

### Parameters
`lab1` [CIELab](./CIELab)  
The first color value.

`lab2` [CIELab](./CIELab)  
The second color value.

### Returns

`double`  
The dE CIE94 metric value.

---
## CIEDE2000(CIELab, CIELab, double, double, double) Method

```csharp
public static double CIEDE2000(in CIELab lab1, in CIELab lab2, double kL = 1.0, double kC = 1.0, double kH = 1.0)
```

Calculates the difference between two color values using the CIEDE2000 method.

### Parameters
`lab1` [CIELab](./CIELab)  
The first color value.

`lab2` [CIELab](./CIELab)  
The second color value.

`kL` `double`  
Usually 1.0.

`kC` `double`  
Usually 1.0.

`kH` `double`  
Usually 1.0.

### Returns

`double`  
The dE CIE2000 metric value.
