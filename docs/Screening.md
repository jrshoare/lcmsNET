# Screening Struct

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents screening information.

```csharp
public struct Screening
```

Inheritance Object → ValueType → Screening

## Fields

|  |  |  |
| --- | --- | --- |
| Flag | [ScreeningFlags](./ScreeningFlags.md) | Screening flags. |
| nChannels | `uint` | Number of screening channels defined for use. |
| Channels | [ScreeningChannel[]](./ScreeningChannel.md) | Screening channels. |

## Constructors
## Screening(ScreeningFlags, uint, ScreeningChannel[]) Constructor

```csharp
public Screening(ScreeningFlags flag, uint nchannels, ScreeningChannels[] channels)
```

Initialises a new instance of the `Screening` struct.

### Parameters

`flag` [ScreeningFlags](./ScreeningFlags.md)  
The screening flags.

`nchannels` uint  
The number of screening channels defined for use.

`channels` [ScreeningChannel[]](./ScreeningChannel.md)  
An array of 16 screening channels.
