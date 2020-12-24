# CurveSegment Struct

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a segment of a segmented curve.

```csharp
public struct CurveSegment
```

Inheritance Object → ValueType → CurveSegment

## Fields

|  |  |  |
| --- | --- | --- |
x0 | float | Domain for x0 < x
x1 | float | Domain for x <= x1
type | int | Parametric type; 0 = sampled segment. Negative numbers are reserved
parameters | double[] | Parameters if 'type' != 0
nGridPoints | int | Number of grid points if 'type' == 0
sampledPoints | IntPtr | Pointer to a float array of size 'nGridPoints' if 'type' == 0.
