# SamplerFloat Delegate

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Defines a delegate that can be used to populate CLUT stages in a way that is independent of the number of nodes.

```csharp
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SamplerFloat(
    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeConst = Stage.MAX_INPUT_DIMENSIONS + 1), In] float[] input,
    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeConst = Stage.MAX_STAGE_CHANNELS)] float[] output,
    IntPtr cargo);
```

Inheritance Object → Delegate → SamplerFloat

### Parameters

`input` float[]  
The node coordinates.

`output` float[]  
The contents of the CLUT on the node.

`cargo` IntPtr  
User supplied content.
