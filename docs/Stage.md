# Stage Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a stage in a pipeline. This class cannot be inherited.

```csharp
public sealed class Stage : CmsHandle<Stage>
```

Inheritance Object → CmsHandle<Stage> → Stage

Implements IDisposable

## Properties
## Context Property

```csharp
public Context Context { get; }
```

### Property Value

`Context`  
The `Context` supplied to create this instance. Inherited from `CmsHandle<T>`.

---
## InputChannels Property

```csharp
public uint InputChannels { get; }
```

### Property Value

`InputChannels`  
Gets the number of input channels in the stage.

---
## IsDisposed Property

```csharp
public bool IsDisposed { get; }
```

### Property Value

`IsDisposed`  
Gets a value indicating whether the instance has been disposed.

---
## OutputChannels Property

```csharp
public uint OutputChannels { get; }
```

### Property Value

`OutputChannels`  
Gets the number of output channels in the stage.

---
## StageType Property

```csharp
public StageSignature StageType { get; }
```

### Property Value

`StageType`  
Gets the stage type.

## Methods
## Create(Context, double[,], double[]) Method

```csharp
public static Stage Create(Context context, double[,] matrix, double[] offset)
```

Creates an instance of the `Stage` class that contains a matrix and optional offset.

### Parameters

`context` [Context](./Context)  
A `Context`, or null for the global context.

`matrix` double[,]  
A matrix of [rows, columns].

`offset` double[]  
A vector of [columns] offsets, or null if no offset is to be applied.

### Returns

`Stage`  
A new `Stage` instance.

---
## Create(Context, uint) Method

```csharp
public static Stage Create(Context context, uint nChannels)
```

Creates an instance of the `Stage` class for an empty stage that performs no operation.

### Parameters

`context` [Context](./Context)  
A `Context`, or null for the global context.

`nChannels` uint  
The number of channels.

### Returns

`Stage`  
A new `Stage` instance.

---
## Create(Context, uint, ToneCurve[]) Method

```csharp
public static Stage Create(Context context, uint nChannels, ToneCurve[] curves)
```

Creates an instance of the `Stage` class that contains tone curves.

### Parameters

`context` [Context](./Context)  
A `Context`, or null for the global context.

`nChannels` uint  
The number of tone curves.

`curves` [ToneCurve[]](./ToneCurve.md)  
An array of `nChannels` tone curves, or null to use identity curves.

### Returns

`Stage`  
A new `Stage` instance.

---
## Create(Context, uint, uint, uint, float[]) Method

```csharp
public static Stage Create(Context context, uint nGridPoints, uint inputChannels, uint outputChannels, float[] table)
```

Creates an instance of the `Stage` class that contains a floating point multi-dimensional lookup table (CLUT) where each dimension has the same size.

### Parameters

`context` [Context](./Context)  
A `Context`, or null for the global context.

`nGridPoints` uint  
The number of nodes (same for each component).

`inputChannels` uint  
The number of input channels.

`outputChannels` uint  
The number of output channels.

`table` float[]  
An array of initial values for the nodes, or null if the CLUT is to be initialised to zero.

### Returns

`Stage`  
A new `Stage` instance.

---
## Create(Context, uint, uint, uint, float[]) Method

```csharp
public static Stage Create(Context context, uint nGridPoints, uint inputChannels, uint outputChannels, ushort[] table)
```

Creates an instance of the `Stage` class that contains a 16 bit multi-dimensional lookup table (CLUT) where each dimension has the same size.

### Parameters

`context` [Context](./Context)  
A `Context`, or null for the global context.

`nGridPoints` uint  
The number of nodes (same for each component).

`inputChannels` uint  
The number of input channels.

`outputChannels` uint  
The number of output channels.

`table` ushort[]  
An array of initial values for the nodes, or null if the CLUT is to be initialised to zero.

### Returns

`Stage`  
A new `Stage` instance.

---
## Create(Context, uint[], uint, float[]) Method

```csharp
public static Stage Create(Context context, uint[] cLutPoints, uint outputChannels, float[] table)
```

Creates an instance of the `Stage` class that contains a floating point multi-dimensional lookup table (CLUT) where each dimension can have a different size.

### Parameters

`context` [Context](./Context)  
A `Context`, or null for the global context.

`cLutPoints` uint[]  
An array of [inputChannels] values containing the number of node for each component.

`outputChannels` uint  
The number of output channels.

`table` float[]  
An array of initial values for the nodes, or null if the CLUT is to be initialised to zero.

### Returns

`Stage`  
A new `Stage` instance.

---
## Create(Context, uint[], uint, ushort[]) Method

```csharp
public static Stage Create(Context context, uint[] cLutPoints, uint outputChannels, ushort[] table)
```

Creates an instance of the `Stage` class that contains a 16 bit multi-dimensional lookup table (CLUT) where each dimension can have a different size.

### Parameters

`context` [Context](./Context)  
A `Context`, or null for the global context.

`cLutPoints` uint[]  
An array of [inputChannels] values containing the number of node for each component.

`outputChannels` uint  
The number of output channels.

`table` ushort[]  
An array of initial values for the nodes, or null if the CLUT is to be initialised to zero.

### Returns

`Stage`  
A new `Stage` instance.

---
## Dispose Method

```csharp
public void Dispose()
```

Disposes this instance.

---
## Duplicate() Method

```csharp
public Stage Duplicate()
```

Duplicates a stage.

### Returns

`Stage`  
A new `Stage` instance.

---
## SampleCLUT(Sampler16, IntPtr, StageSamplingFlags) Method

```csharp
public bool SampleCLUT(Sampler16 sampler, IntPtr cargo, StageSamplingFlags flags)
```

Iterates on all noeds of the stage calling a 16-bit sampler on each node.

### Parameters

`sampler` [Sampler16](./Sampler16.md)  
The callback to be executed on each node.

`cargo` IntPtr  
A user-supplied value to be passed to the callback.

`flags` [StageSamplingFlags](./StageSamplingFlags.md)  
Flags to control stage sampling.

### Returns

`bool`  
`true` if successful, otherwise `false`.

---
## SampleCLUT(SamplerFloat, IntPtr, StageSamplingFlags) Method

```csharp
public bool SampleCLUT(SamplerFloat sampler, IntPtr cargo, StageSamplingFlags flags)
```

Iterates on all noeds of the stage calling a floating point sampler on each node.

### Parameters

`sampler` [SamplerFloat](./SamplerFloat.md)  
The callback to be executed on each node.

`cargo` IntPtr  
A user-supplied value to be passed to the callback.

`flags` [StageSamplingFlags](./StageSamplingFlags.md)  
Flags to control stage sampling.

### Returns

`bool`  
`true` if successful, otherwise `false`.

---
## SliceSpace(uint[] Sampler16, IntPtr) Method

```csharp
public static bool SliceSpace(uint[] cLutPoints, Sampler16 sampler, IntPtr cargo)
```

Slices the target space calling a 16-bit sampler on each node.

### Parameters

`cLutPoints` uint[]  
An array holding the division slices for each component.

`sampler` [Sampler16](./Sampler16.md)  
The callback to be executed on each node.

`cargo` IntPtr  
A user-supplied value to be passed to the callback.

### Returns

`bool`  
`true` if successful, otherwise `false`.

---
## SliceSpace(uint[] SamplerFloat, IntPtr) Method

```csharp
public static bool SliceSpace(uint[] cLutPoints, SamplerFloat sampler, IntPtr cargo)
```

Slices the target space calling a 16-bit sampler on each node.

### Parameters

`cLutPoints` uint[]  
An array holding the division slices for each component.

`sampler` [SamplerFloat](./SamplerFloat.md)  
The callback to be executed on each node.

`cargo` IntPtr  
A user-supplied value to be passed to the callback.

### Returns

`bool`  
`true` if successful, otherwise `false`.
