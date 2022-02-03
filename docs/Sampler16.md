# Sampler16 Delegate

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Defines a delegate that can be used to populate CLUT stages in a way that is independent of the number of nodes.

```csharp
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void Sampler16(
    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = Stage.MAX_INPUT_DIMENSIONS + 1), In] ushort[] input,
    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = Stage.MAX_STAGE_CHANNELS), In] ushort[] output,
    IntPtr cargo);
```

Inheritance Object → Delegate → Sampler16

### Parameters

`input` ushort[]  
The node coordinates.

`output` ushort[]  
The contents of the CLUT on the node.

`cargo` IntPtr  
User supplied content.
