# Pipeline Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents an ordered collection of [Stage](./Stage.md)
instances where each stage performs a single operation on
image data. The output of a first stage provides the input
to the next and so on through the pipeline. This class cannot be inherited.

```csharp
public sealed class Pipeline : IEnumerable<Stage>, IDisposable
```

Inheritance Object → Pipeline

Implements IEnumerable\<Stage> and IDisposable

## Properties
## Context Property

```csharp
public Context Context { get; }
```

### Property Value

`Context`  
The `Context` supplied to create this instance.

---
## Handle Property

```csharp
public IntPtr Handle { get; }
```

### Property Value

`Handle` IntPtr  
Gets the handle to the piepline.

---
## InputChannels Property

```csharp
public uint InputChannels { get; }
```

### Property Value

`InputChannels` uint  
Gets the number of input channels for the pipeline.

---
## OutputChannels Property

```csharp
public uint OutputChannels { get; }
```

### Property Value

`OutputChannels` uint  
Gets the number of output channels for the pipeline.

---
## StageCount Property

```csharp
public uint StageCount { get; }
```

### Property Value

`StageCount` uint  
Gets the number of stages in the pipeline.

## Methods
## Append(Pipeline) Method

```csharp
public bool Append(Pipeline other)
```

Appends the supplied pipeline to the end of this piepline.

### Parameters

`other` [Pipeline](./Pipeline.md)  
The pipeline to be appended to end of this pipeline.

### Returns

`bool`  
`true` if appended successfully, otherwise `false`.

---
## Create(Context, uint, uint) Method

```csharp
public static Pipeline Create(Context context, uint inputChannels, uint outputChannels)
```

Creates a new instance of the `Pipeline` class.

### Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

`inputChannels` uint  
The number of input channels.

`outputChannels` uint  
The number of output channels.

### Returns

`Pipeline`  
A new `Pipeline` instance.

---
## Dispose() Method

```csharp
public void Dispose()
```

Disposes this instance.

---
## Duplicate() Method

```csharp
public Pipeline Duplicate()
```

Duplicates a pipeline.

### Returns

`Pipeline`  
A new `Pipeline` instance.

---
## Evaluate(float[]) Method

```csharp
public float[] Evaluate(float[] values)
```

Evaluates the pipeline using the supplied floating point
values.

### Parameters

`values` float[]  
The values to supply to the pipeline.

### Returns

`float[]`  
The values resulting from evaluation of the pipeline.

---
## Evaluate(ushort[]) Method

```csharp
public ushort[] Evaluate(ushort[] values)
```

Evaluates the pipeline using the supplied unsigned 16-bit
integer values.

### Parameters

`values` ushort[]  
The values to supply to the pipeline.

### Returns

`ushort[]`  
The values resulting from evaluation of the pipeline.

---
## EvaluateReverse(float[], float[], out bool) Method

```csharp
public float[] EvaluateReverse(float[] values, float[] hint, out bool success)
```

Evaluates the pipeline in the reverse direction using the
supplied floating point values using Newton's method.

### Parameters

`float` float[]  
The values to supply to the pipeline.

`hint` float[]  
An array of hint values where to begin the search. Can be
`null`.

`success` bool  
Returns `true` if the pipeline was evaluated successfully,
otherwise `false`.

### Returns

`float[]`  
The values resulting from evaluation of the pipeline.

---
## GetEnumerator() Method

```csharp
public IEnumerator<Stage> GetEnumerator()
```

Returns an enumerator that iterates through the stages
in the pipeline.

### Returns

`IEnumerator<Stage>`  
An enumerator that can be used to iterate through each [Stage](./Stage.md) in the pipeline.

---
## Insert(Stage, StageLoc) Method

```csharp
public bool Insert(Stage stage, StageLoc location)
```

Inserts a stage to the start or end of the pipeline.

### Parameters

`stage` [Stage](./Stage.md)  
The stage to be inserted.

`location` [StageLoc](./StageLoc.md)  
The location where the stage is to be inserted.

### Returns

`bool`  
`true` if the stage was inserted, otherwise `false`.

### Remarks

Ownership of the stage passes to the pipeline so its
resources will be freed when the pipeline is disposed.

---
## SetAs8BitsFlag(bool) Method

```csharp
public bool SetAs8BitsFlag(bool on)
```

Sets an internal flag that marks the pipeline to be saved
in 8-bit or 16-bit precision.

### Parameters

`on` bool  
`true` sets 8-bit precision, `false` sets 16-bit precision.

### Returns

`bool`  
`true` if set successfully, otherwise `false`.

---
## Unlink(StageLoc) Method

```csharp
public Stage Unlink(StageLoc location)
```

Removes a stage from the start or end of the pipeline.

### Parameters

`location` StageLoc   
The location from where the stage is to be removed.

### Returns

`Stage`  
The stage that has been removed. Can be `null`.

### Remarks

The caller is responsible for disposing any stage removed
using this method.

---
## UnlinkAndDispose(StageLoc) Method

```csharp
public void UnlinkAndDispose(StageLoc location)
```

Removes and disposes a stage from the start or end of the pipeline.

### Parameters

`location` StageLoc   
The location from where the stage is to be removed.
