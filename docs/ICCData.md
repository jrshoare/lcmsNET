# ICCData Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a data structure that contains either 7-bit ASCII or binary data.

```csharp
public class ICCData
```

Inheritance Object → ICCData

## Examples

The following code example demonstrates how to read and write an ICC data
tag containing binary data to a profile.

```csharp
using lcmsNet;

using (var profile = Profile.CreatePlaceholder(null))
{
    var expected = new byte[] { 17, 99, 0, 253, 122, 19 };
    var iccData = new ICCData(expected);

    // do not use TagSignature.Data as this is not supported
    profile.WriteTag(TagSignature.Ps2CRD0, iccData);

    var iccData2 = ICCData.FromHandle(profile.ReadTag(TagSignature.Ps2CRD0));
    var actual = (byte[])iccData2;
}
```

The following code example demonstrates the same behaviour but uses the generic
`ReadTag<T>` method to automatically return an ICCData and then explicitly
convert this to a string.

```csharp
using lcmsNet;

using (var profile = Profile.CreatePlaceholder(null))
{
    var expected = "ascii text";
    var iccData = new ICCData(expected);

    // do not use TagSignature.Data as this is not supported
    profile.WriteTag(TagSignature.Ps2CRD1, iccData);
    var actual = (string)profile.ReadTag<ICCData>(TagSignature.Ps2CRD1);
}
```

## Constructors
## ICCData(byte[])

```csharp
public ICCData(byte[] bytes)
```

Initialises a new instance of the ICCData class with binary data.

### Parameters

`bytes` byte[]  
An array of byte values.

---
## ICCData(string)

```csharp
public ICCData(string s)
```

Initialises a new instance of the ICCData class with ASCII data.

### Parameters

`s` string  
A string. Non-ASCII characters are replaced with the value 63 which is the ASCII
character code for '?'.

## Methods
## explicit operator byte[](ICCData) Method

```csharp
public static explicit operator byte[](ICCData iccData)
```

Explicitly converts an ICCData to a byte array.

### Parameters

'iccData' [ICCData](./ICCData.md)  
The ICCData to be converted.

---
## explicit operator string(ICCData) Method

```csharp
public static explicit operator string(ICCData iccData)
```

Explicitly converts an ICCData to a string.

### Parameters

'iccData' [ICCData](./ICCData.md)  
The ICCData to be converted.

---
## FromHandle(IntPtr) Method

```csharp
public static ICCData FromHandle(IntPtr handle)
```

Creates an instance of the ICCData class from the supplied handle.

### Parameters

`handle` IntPtr  
A handle returned from a call to `Profile.ReadTag(TagSignature.Ps2*)`.
