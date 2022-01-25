# ViewingConditions Struct

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Defines the viewing conditions for a [CAM02](./CAM02.md).

```csharp
public struct ViewingConditions
```

Inheritance Object → ValueType → ViewingConditions

## Fields

|  |  |  |
| --- | --- | --- |
| whitePoint | [CIEXYZ](./CIEXYZ.md) | The white point. |
| Yb | `double` | Yb. |
| La | `double` | La. |
| surround | [Surround](./Surround.md) | Surround. |
| D_value | `double` | Degree of chromatic adaptation. |

## Remarks

A value of -1 for `D_value` causes it to be calculated,
otherwise specify in the range 0..1.0.
