# Screening Struct

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Defines a set of viewing conditions.

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

## Methods
## FromHandle(IntPtr) Method

```csharp
public static Screening FromHandle(IntPtr handle)
```

Marshals data from an unmanaged block of memory to a newly allocated `Screening` object.

### Parameters

`handle` IntPtr  
A handle to the unmanaged block of memory.

### Returns

`Screening`  
A new `Screening` instance.
