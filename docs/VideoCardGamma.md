# VideoCardGamma Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a video card gamma table. This class cannot be inherited.

```csharp
public sealed class VideoCardGamma
```

Inheritance Object → VideoCardGamma

## Properties
## Blue Property

```csharp
public ToneCurve Blue { get; }
```

### Property Value

`Blue` [ToneCurve](./ToneCurve.md)  
Gets the video card gamma table blue tone curve.

---
## Green Property

```csharp
public ToneCurve Green { get; }
```

### Property Value

`Green` [ToneCurve](./ToneCurve.md)  
Gets the video card gamma table green tone curve.

---
## Red Property

```csharp
public ToneCurve Red { get; }
```

### Property Value

`Red` [ToneCurve](./ToneCurve.md)  
Gets the video card gamma table red tone curve.

## Constructors
## VideoCardGamma(ToneCurve, ToneCurve, ToneCurve)

```csharp
public VideoCardGamma(ToneCurve red, ToneCurve green, ToneCurve blue)
```

Initialises a new instance of the `VideoCardGamma` class.

### Parameters

`red` [ToneCurve](./ToneCurve.md)  
A tone curve for the red component.

`green` [ToneCurve](./ToneCurve.md)  
A tone curve for the green component.

`blue` [ToneCurve](./ToneCurve.md)  
A tone curve for the blue component.
