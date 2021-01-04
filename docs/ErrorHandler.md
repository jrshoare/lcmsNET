# ErrorHandler Delegate

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Specifies a delegate to handle errors reported by the wrappered library.

```csharp
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void ErrorHandler(IntPtr contextID, [MarshalAs(UnmanagedType.U4)] int errorCode, [MarshalAs(UnmanagedType.LPStr)] string errorText);
```

Inheritance Object → Delegate → ErrorHandler

### Parameters

`contextID` `IntPtr`  
The ID of the context from which the delegate is called. A value of `IntPtr.Zero` is used for the global context.

`errorCode` `int`  
An error code.

`errorText` `string`  
An English string containing information about the error.
