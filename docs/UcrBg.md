# UcrBg Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents an under color removal and black generation. This class cannot be inherited.

```csharp
public sealed class UcrBg
```

Inheritance Object → UcrBg

## Properties
## Bg Property

```csharp
public ToneCurve Bg { get; }
```

### Property Value

`Bg` [ToneCurve](./ToneCurve.md)  
Gets the black generation tone curve.

---
## Desc Property

```csharp
public MultiLocalizedUnicode Desc { get; }
```

### Property Value

`Desc` [MultiLocalizedUnicode](./MultiLocalizedUnicode.md)  
Gets the description of the method used for under color removal and black generation.

---
## Ucr Property

```csharp
public ToneCurve Ucr { get; }
```

### Property Value

`Ucr` [ToneCurve](./ToneCurve.md)  
Gets under color removal tone curve.

## Constructors
## UcrBg(ToneCurve, ToneCurve, MultiLocalizedUnicode)

```csharp
public UcrBg(ToneCurve ucr, ToneCurve bg, MultLocalizedUnicode desc)
```

Initialises a new instance of the `UcrBg` class.

### Parameters

`ucr` [ToneCurve](./ToneCurve.md)  
A tone curve for under color removal.

`bg` [ToneCurve](./ToneCurve.md)  
A tone curve for black generation.

`desc` [MultLocalizedUnicode](./MultLocalizedUnicode.md)  
A description of the method used for under color removal and black generation.

## Methods
## FromHandle(IntPtr) Method

```csharp
public static UcrBg FromHandle(IntPtr handle)
```

Creates an under color removal and black generation from the supplied handle.

### Parameters

`handle` IntPtr  
A handle to an existing under color removal and black generation.

### Returns

`UcrBg`  
A new `UcrBg` instance referencing an
existing under color removal and black generation.
