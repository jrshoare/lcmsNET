# Transform Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a color transform. This class cannot be inherited.

```csharp
public sealed class Transform : CmsHandle<Transform>
```

Inheritance Object → CmsHandle\<Transform> → Transform

Implements IDisposable

## Properties
## Context Property

```csharp
public Context Context { get; }
```

### Property Value

`Context` [Context](./Context.md)  
Gets the context in which the instance was created. Inherited from `CmsHandle<T>`.

---
## InputFormat Property

```csharp
public uint InputFormat { get; }
```

### Property Value

`InputFormat` uint  
Gets the input format of the transform, or 0 if the instance has been disposed.

### Remarks

Requires Little CMS version 2.2 or later.

---
## IsDisposed Property

```csharp
public bool IsDisposed { get; }
```

### Property Value

`IsDisposed` bool  
Gets a value indicating whether the instance has been disposed.

---
## NamedColorList Property

```csharp
public NamedColorList NamedColorList { get; }
```

### Property Value

`NamedColorList` [NamedColorList](./NamedColorList.md)  
Gets a named color list from the transform.

---
## OutputFormat Property

```csharp
public uint OutputFormat { get; }
```

### Property Value

`OutputFormat` uint  
Gets the output format of the transform, or 0 if the instance has been disposed.

## Methods
## ChangeBuffersFormat(uint, uint) Method

```csharp
public bool ChangeBuffersFormat(uint inputFormat, uint outputFormat)
```

Changes the encoding of buffers in a transform originally created with at least 16 bits of precision.

### Parameters

`inputFormat` uint  
The input format.

`outputFormat` uint  
The output format.

### Returns

`bool`  
`true` if successful, otherwise `false`.

---
## Create(Context, Profile, uint, Profile, uint, Intent, CmsFlags) Method

```csharp
public static Transform Create(Context context, Profile input, uint inputFormat, Profile output, uint outputFormat, Intent intent, CmsFlags flags)
```

Creates a new instance of the Transform class.

### Parameters

`context` [Context](./Context.md)  
A Context, or null for the global context.

`input` [Profile](./Profile.md)  
A profile capable to work in the input direction.

`inputFormat` uint  
The input format e.g. `Cms.TYPE_RGB_8`.

`output` [Profile](./Profile.md)  
A profile capable to work in the output direction.

`outputFormat` uint  
The output format e.g. `Cms.TYPE_Lab_8`.

`intent` [Intent](./Intent.md)  
The intent.

`flags` [CmsFlags](./CmsFlags.md)  
The flags to control the process.

### Returns

`Transform`  
A new `Transform` instance.

---
## Create(Context, Profile, uint, Profile, uint, Profile, Intent, Intent, CmsFlags) Method

```csharp
public static Transform Create(Context context, Profile input, uint inputFormat, Profile output, uint outputFormat, Profile proofing, Intent intent, Intent proofingIntent, CmsFlags flags)
```

Creates a new instance of the Transform class for a proofing transform.

### Parameters

`context` [Context](./Context.md)  
A Context, or null for the global context.

`input` [Profile](./Profile.md)  
A profile capable to work in the input direction.

`inputFormat` uint  
The input format e.g. `Cms.TYPE_RGB_8`.

`output` [Profile](./Profile.md)  
A profile capable to work in the output direction.

`outputFormat` uint  
The output format e.g. `Cms.TYPE_Lab_8`.

`proofing` [Profile](./Profile.md)  
A proofing profile.

`intent` [Intent](./Intent.md)  
The intent.

`proofingIntent` [Intent](./Intent.md)  
The proofing intent.

`flags` [CmsFlags](./CmsFlags.md)  
The flags to control the process.

### Returns

`Transform`  
A new `Transform` instance.

### Remarks

To enable proofing and gamut check include `CmsFlags.SoftProofing | CmsFlags.GamutCheck`.

---
## Create(Context, Profile[], bool[], Intent[], double[], Profile, int, uint, uint, CmsFlags) Method

```csharp
public static Transform Create(Context context, Profile[]profiles, bool[] bpc, Intent[] intents, double[] adaptationStates, Profile gamut, int gamutPCSPosition, uint inputFormat, uint outputFormat, CmsFlags flags)
```

Creates a new instance of the Transform class for a proofing transform.

### Parameters

`context` [Context](./Context.md)  
A Context, or null for the global context.

`profiles` [Profile[]](./Profile.md)  
An array of profiles.

`bpc` bool[]  
An array of black point compensation states.

`intents` [Intent[]](./Intent.md)  
An array of intents.

`adaptationStates` double[]  
An array of adaptation states.

`gamut` [Profile](./Profile.md)  
A profile holding gamut information for a gamut check, can be null.

`gamutPCSPosition` int  
Position in the chain of Lab/XYZ PCS to check gamut.

`inputFormat` uint  
The input format, e.g. `Cms.TYPE_RGB_8`.

`outputFormat` uint  
The output format, e.g. `Cms.TYPE_XYZ_16`.

`flags` [CmsFlags](./CmsFlags.md)  
The flags to control the process.

### Returns

`Transform`  
A new `Transform` instance.

### Remarks

`gamut` and `gamutPCSPosition` are only used if `flags` includes `CmsFlags.GamutCheck`.

---
## Create(Context, Profile[], uint, uint, Intent, CmsFlags) Method

```csharp
public static Transform Create(Context context, Profile[] profiles, uint inputFormat, uint outputFormat, Intent intent, CmsFlags flags)
```

Creates a new instance of the Transform class for a multi-profile transform.

### Parameters

`context` [Context](./Context.md)  
A Context, or null for the global context.

`profiles` [Profile[]](./Profile.md)  
An array of profiles.

`inputFormat` uint  
The input format, e.g. `Cms.TYPE_RGB_8`.

`outputFormat` uint  
The output format, e.g. `Cms.TYPE_Lab_8`.

`intent` [Intent](./Intent.md)  
The intent.

`flags` [CmsFlags](./CmsFlags.md)  
The flags to control the process.

### Returns

`Transform`  
A new `Transform` instance.

---
## Create(Profile, uint, Profile, uint, Intent, CmsFlags) Method

```csharp
public static Transform Create(Profile input, uint inputFormat, Profile output, uint outputFormat, Intent intent, CmsFlags flags)
```

Creates a new instance of the Transform class.

### Parameters

`input` [Profile](./Profile.md)  
A profile capable to work in the input direction.

`inputFormat` uint  
The input format e.g. `Cms.TYPE_RGB_8`.

`output` [Profile](./Profile.md)  
A profile capable to work in the output direction.

`outputFormat` uint  
The output format e.g. `Cms.TYPE_Lab_8`.

`intent` [Intent](./Intent.md)  
The intent.

`flags` [CmsFlags](./CmsFlags.md)  
The flags to control the process.

### Returns

`Transform`  
A new `Transform` instance.

### Remarks

Creates the instance in the global context.

---
## Create(Profile, uint, Profile, uint, Profile, Intent, Intent, CmsFlags) Method

```csharp
public static Transform Create(Profile input, uint inputFormat, Profile output, uint outputFormat, Profile proofing, Intent intent, Intent proofingIntent, CmsFlags flags)
```

Creates a new instance of the Transform class for a proofing transform.

### Parameters

`input` [Profile](./Profile.md)  
A profile capable to work in the input direction.

`inputFormat` uint  
The input format e.g. `Cms.TYPE_RGB_8`.

`output` [Profile](./Profile.md)  
A profile capable to work in the output direction.

`outputFormat` uint  
The output format e.g. `Cms.TYPE_Lab_8`.

`proofing` [Profile](./Profile.md)  
A proofing profile.

`intent` [Intent](./Intent.md)  
The intent.

`proofingIntent` [Intent](./Intent.md)  
The proofing intent.

`flags` [CmsFlags](./CmsFlags.md)  
The flags to control the process.

### Returns

`Transform`  
A new `Transform` instance.

### Remarks

To enable proofing and gamut check include `CmsFlags.SoftProofing | CmsFlags.GamutCheck`.

Creates the instance in the global context.

---
## Create(Context, Profile[], uint, uint, Intent, CmsFlags) Method

```csharp
public static Transform Create(Profile[] profiles, uint inputFormat, uint outputFormat, Intent intent, CmsFlags flags)
```

Creates a new instance of the Transform class for a multi-profile transform.

### Parameters

`profiles` [Profile[]](./Profile.md)  
An array of profiles.

`inputFormat` uint  
The input format, e.g. `Cms.TYPE_RGB_8`.

`outputFormat` uint  
The output format, e.g. `Cms.TYPE_Lab_8`.

`intent` [Intent](./Intent.md)  
The intent.

`flags` [CmsFlags](./CmsFlags.md)  
The flags to control the process.

### Returns

`Transform`  
A new `Transform` instance.

### Remarks

Creates the instance in the global context.

---
## Dispose() Method

```csharp
public void Dispose()
```

Disposes this instance.

---
## DoTransform(byte[], byte[], int) Method

```csharp
public void DoTransform(byte[] inputBuffer, byte[] outputBuffer, int pixelCount)
```

Translates a bitmap according to the parameters setup when creating the transform.

### Parameters

`inputBuffer` byte[]  
An array of bytes containing the input bitmap.

`outputBuffer` byte[]  
An array of bytes to contain the output bitmap.

`pixelCount` int  
The number of pixels to be transformed.

---
## DoTransform(byte[], byte[], int, int, int, int, int, int) Method

```csharp
public void DoTransform(byte[] inputBuffer, byte[] outputBuffer, int pixelsPerLine, int lineCount, int bytesPerLineIn, int bytesPerLineOut, int bytesPerPlaneIn, int bytesPerPlaneOut)
```

Translates a bitmap according to the parameters setup when creating the transform.

### Parameters

`inputBuffer` byte[]  
An array of bytes containing the input bitmap.

`outputBuffer` byte[]  
An array of bytes to contain the output bitmap.

`pixelsPerLine` int  
The number of pixels per line; same on input and in output.

`lineCount` int  
The number of lines; same on input as in output.

`bytesPerLineIn` int  
The distance in bytes from one line to the next on the input bitmap.

`bytesPerLineOut` int  
The distance in bytes from one line to the next in the output bitmap.

`bytesPerPlaneIn` int  
The distance in bytes from one plane to the next inside a line on the input bitmap.

`bytesPerPlaneOut` int  
The distance in bytes from one plane to the next inside a line in the output bitmap.

### Remarks

`bytesPerPlaneIn` and `bytesPerPlaneOut` are only used in planar formats.

Requires Little CMS version 2.8 or later.
