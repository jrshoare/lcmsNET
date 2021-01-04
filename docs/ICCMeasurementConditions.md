# ICCMeasurementConditions Struct

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents ICC measurement conditions.

```csharp
public struct ICCMeasurementConditions
```

Inheritance Object → ValueType → ICCMeasurementConditions

## Fields

|  |  |  |
| --- | --- | --- |
| Observer | `uint` | 0 = unknown, 1 = CIE 1931, 2 = CIE 1964. |
| Backing | [CIEXYZ](./CIEXYZ) | Value of backing. |
| Geometry | `uint` | 0 = unknown, 1 = 45/0 0/45, 2 = 0d d/0. |
| Flare | `float` | 0 .. 1.0 |
| IlluminantType | `uint` | |
