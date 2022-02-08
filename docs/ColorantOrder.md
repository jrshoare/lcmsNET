# ColorantOrder Struct

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents the laydown order that colorants will be printed on an n-colorant device.

```csharp
public struct ColorantOrder
```

Inheritance Object → ValueType → ColorantOrder

## Examples

The following code example demonstrates how to read and write a colorant order
tag to a profile.

```csharp
using lcmsNet;

using (var profile = Profile.CreatePlaceholder(null))
{
    byte[] expected = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
    var target = (ColorantOrder)expected; // explicit conversion

    profile.WriteTag(TagSignature.ColorantOrder, target);

    byte[] actual = profile.ReadTag<ColorantOrder>(TagSignature.ColorantOrder);
}
```

The following code example demonstrates the same behaviour but uses the generic
`ReadTag<T>` method to automatically return a ColorantOrder and then implicitly
convert this to a byte array.

```csharp
using lcmsNet;

using (var profile = Profile.CreatePlaceholder(null))
{
    byte[] expected = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

    profile.WriteTag(TagSignature.ColorantOrder, (ColorantOrder)expected);
    byte[] actual = profile.ReadTag<ColorantOrder>(TagSignature.ColorantOrder);
}
```

## Constructors
## ColorantOrder(byte[])

```csharp
public ColorantOrder(byte[] bytes)
```

Initialises a new instance of the ColorantOrder class.

### Parameters

`bytes` byte[]  
An array of 16 values.

## Methods
## implicit operator byte[]\(ColorantOrder) Method

```csharp
public static implicit operator byte[](ColorantOrder colorantOrder)
```

Implicitly converts a ColorantOrder to a byte array.

### Parameters

`colorantOrder` [ColorantOrder](./ColorantOrder)  
The ColorantOrder to be converted.

---
## explicit operator ColorantOrder(byte[]) Method

```csharp
public static explicit operator ColorantOrder(byte[] bytes)
```

Explicitly converts a byte array of 16 values to a ColorantOrder.

### Parameters

'bytes' byte[]  
The byte array to be converted.
