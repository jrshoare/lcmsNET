# ToneCurve Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a tone curve. This class cannot be inherited.

```csharp
public sealed class ToneCurve : IDisposable
```

Inheritance Object → ToneCurve

Implements IDisposable

## Properties
## Context Property

```csharp
public Context Context { get; }
```

### Property Value

`Context` [Context](./Context.md)  
Gets the context in which the instance was created.

---
## EstimatedTable Property

```csharp
public IntPtr EstimatedTable { get; }
```

### Property Value

`EstimatedTable` IntPtr  
Gets a pointer to the maintained shadow low-resolution tabulated representation of the tone curve.

---
## EstimatedTableEntries Property

```csharp
public uint EstimatedTableEntries { get; }
```

### Property Value

`EstimatedTableEntries` uint  
Gets the number of entries in the maintained shadow low-resolution tabulated representation of the tone curve.

---
## Handle Property

```csharp
public IntPtr Handle { get; }
```

### Property Value

`Handle` IntPtr  
Gets the handle to the tone curve.

---
## IsDescending Property

```csharp
public bool IsDescending { get; }
```

### Property Value

`IsDescending` bool  
Returns `true` if (0) > ƒ(1), otherwise `false`.

---
## IsDisposed Property

```csharp
public bool IsDisposed { get; }
```

### Property Value

`IsDisposed` bool  
Gets a value indicating whether the instance has been disposed.

---
## IsLinear Property

```csharp
public bool IsLinear { get; }
```

### Property Value

`IsLinear` bool  
Gets a value indicating whether the tone curve is linear.

### Remarks

This is just a coarse approximation with no mathematical validity that does not take unbounded parts into account.

---
## IsMonotonic Property

```csharp
public bool IsMonotonic { get; }
```

### Property Value

`IsMonotonic` bool  
Gets a valueindicating whether the tone curve is monotonic.

### Remarks

This is just a coarse approximation with no mathematical validity that does not take unbounded parts into account.

---
## IsMultiSegment Property

```csharp
public bool IsMultiSegment { get; }
```

### Property Value

`IsMultiSegment` bool  
Gets a value indicating whether the tone curve contains more than one segment.

## Methods
## BuildGamma(Context, double) Method

```csharp
public static ToneCurve BuildGamma(Context context, double gamma)
```

Creates a new instance of the ToneCurve class for a gamma tone curve.

### Parameters

`context` [Context](./Context.md)  
A Context, or null for the global context.

`gamma` double  
The value of the gamma exponent.

### Returns

`ToneCurve`  
A new `ToneCurve` instance.

---
## BuildParametric(Context, int, double[]) Method

```csharp
public static ToneCurve BuildParametric(Context context, int type, double[] parameters)
```

Creates a new instance of the ToneCurve class for a parametric curve.

### Parameters

`context` [Context](./Context.md)  
A Context, or null for the global context.

`type` int  
The parametric tone curve number according to Table 52 in Liitle CMS API, or other if a tone curve plug-in is being used.

`parameters` double[]  
An array of 10 values defining the tone curve parameters for the given type.

### Returns

`ToneCurve`  
A new `ToneCurve` instance.

---
## BuildSegmented(Context, CurveSegment[]) Method

```csharp
public static ToneCurve BuildSegmented(Context context, CurveSegment[] segments)
```

Creates a new instance of the ToneCurve class for the given segment information.

### Parameters

`context` [Context](./Context.md)  
A Context, or null for the global context.

`segments` [CurveSegment[]](./CurveSegment.md)  
An array of segments.

### Returns

`ToneCurve`  
A new `ToneCurve` instance.

---
## BuildTabulated(Context, float[]) Method

```csharp
public static ToneCurve BuildTabulated(Context context, float[] values)
```

Creates a new instance of the ToneCurve class from the given floating point values.

### Parameters

`context` [Context](./Context.md)  
A Context, or null for the global context.

`values` float[]  
An array of floating point values.

### Returns

`ToneCurve`  
A new `ToneCurve` instance.

---
## BuildTabulated(Context, ushort[]) Method

```csharp
public static ToneCurve BuildTabulated(Context context, ushort[] values)
```

Creates a new instance of the ToneCurve class from the given 16-bit values.

### Parameters

`context` [Context](./Context.md)  
A Context, or null for the global context.

`values` ushort[]  
An array of 16-bit values.

### Returns

`ToneCurve`  
A new `ToneCurve` instance.

---
## Dispose() Method

```csharp
public void Dispose()
```

Disposes this instance.

---
## Duplicate() Method
```csharp
public static ToneCurve Duplicate()
```

Creates a duplicate instance of the ToneCurve class.

### Returns

`ToneCurve`  
The new `ToneCurve` that this method creates.

---
## EstimateGamma(double) Method

```csharp
public double EstimateGamma(double precision)
```

Estimates the apparent gamma of the tone curve using a least squares fit to a pure exponents expression in the ƒ(x)=x^𝛾.

### Parameters

`precision` double  
The maximum standard deviation allowed on the residuals.

### Returns

`double`  
The estimated gamma at the given precision, or -1 if the fitting has less precision.

### Remarks

A value of 0.01 is a fair value for precision. Set to a large number to fit any curve no matter how good the fit.

The 𝛾 is estimated at the given precision.

---
## Evaluate(float) Method

```csharp
public float Evaluate(float v)
```

Evaluates the given floating point number across the tone curve.

### Parameters

`v` float  
The value to evaluate.

### Returns

`float`  
The value evaluated across the tone curve.

---
## Evaluate(ushort) Method

```csharp
public float Evaluate(ushort v)
```

Evaluates the given 16-bit number across the tone curve.

### Parameters

`v` ushort  
The value to evaluate.

### Returns

`ushort`  
The value evaluated across the tone curve.

### Remarks

This function is significantly faster than `Evaluate(float)` since it uses a pre-computed 16-bit lookup table.

---
## FromHandle(IntPtr) Method

```csharp
public static ToneCurve FromHandle(IntPtr handle)
```

Creates an instance of the ToneCurve class from the supplied handle.

### Parameters

`handle` `IntPtr`  
A handle to an existing tone curve.

### Returns

`ToneCurve`  
The new `ToneCurve` that this method creates.

---
## Join(Context, ToneCurve, int) Method

```csharp
public ToneCurve Join(Context context, ToneCurve other, int nPoints)
```

Composites two tone curves in the form Y⁻¹(X(t)).

### Parameters

`context` [Context](./Context.md)  
A Context, or null for the global context.

`other` [ToneCurve](./ToneCurve.md)  
The other tone curve 'X'.

`nPoints` int  
Sample rate for the resulting tone curve.

### Returns

A new `ToneCurve` instance.

---
## Reverse() Method

```csharp
public ToneCurve Reverse()
```

Creates a tone curve that is the inverse ƒ⁻¹ of this instance.

### Returns

`ToneCurve`  
A new `ToneCurve` instance.

---
## Reverse(int) Method

```csharp
public ToneCurve Reverse(int nResultSamples)
```

Creates a tone curve that is the inverse ƒ⁻¹ of this instance or a tabulated curve of nResultSamples if it could not be reversed analytically.

### Parameters

`nResultSamples` int  
Number of samples to use in case the tone curve cannot be reversed analytically.

### Returns

`ToneCurve`  
A new `ToneCurve` instance.

---
## Smooth(double) Method

```csharp
public bool Smooth(double lambda)
```

Smoothes the tone curve according to the lambda parameter.

### Parameters

`lambda` double  
The degree of smoothing.

### Returns

`bool`  
`true` if smoothing was successful, otherwise `false`.
