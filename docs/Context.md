# Context Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a structure that keeps track of plug-ins and static data needed by the THR corresponding function. This class cannot be inherited.

```csharp
public sealed class Context : IDisposable
```

Inheritance Object → Context

Implements IDisposable

## Examples

The following code example demonstrates how to construct a new Context and get its ID.

```csharp
using lcmsNET;

IntPtr plugin = IntPtr.Zero;
IntPtr userData = IntPtr.Zero;

using (var context = Context.Create(plugin, userData))
{
    IntPtr id = context.ID;
}
```
## Properties

## AdaptationState Property

```csharp
public double AdaptationState { get; set; }
```

### Property Value

`AdaptationState`  
Gets or sets the adaptation state for absolute colorimetric intent in the context.

---
## AlarmCodes Property

```csharp
public ushort[] AlarmCodes { get; set; }
```

### Property Value

`AlarmCodes`  
Gets or sets the current codes used to mark out-out-gamut on Proofing transforms for the context.

### Exceptions
`ArgumentException`  
Array size must equal 16.

---
## ID Property

```csharp
public IntPtr ID { get; }
```

### Property Value

`ID`  
Gets the ID for the context.

---
## UserData Property

```csharp
public IntPtr UserData { get; }
```

### Property Value

`UserData`  
Gets the user data associated with the context.

## Methods

## Create(IntPtr, IntPtr) Method
```csharp
public static Context Create(IntPtr plugin, IntPtr userData)
```

Creates an instance of the Context class.

### Parameters
`plugin` `IntPtr`  
Pointer to plug-in collection. Set to `IntPtr.Zero` for no plug-ins.

`userData` `IntPtr`  
Pointer to user-defined data that will be forwarded to plug-ins and logger. Set to `IntPtr.Zero` for none.

### Returns

`Context`  
The new `Context` that this method creates.

---
## Duplicate(IntPtr) Method
```csharp
public static Context Duplicate(IntPtr userData)
```

Creates a duplicate instance of the Context class.

### Parameters
`userData` `IntPtr`  
Pointer to user-defined data that will be forwarded to plug-ins and logger. Set to `IntPtr.Zero` for none.

### Returns

`Context`  
The new `Context` that this method creates.

---
## RegisterPlugins(IntPtr) Method
```csharp
public bool RegisterPlugins(IntPtr plugin)
```

Installs a plug-in bundle in the context.

### Parameters
`plugin` `IntPtr`  
Pointer to plug-in bundle.

### Returns

`bool`  
`true` if successful, otherwise `false`.

---
## UnregisterPlugins() Method
```csharp
public void UnregisterPlugins()
```

Returns the given context to its default pristine state, as if no plug-ins were declared.

---
## SetErrorHandler(ErrorHandler) Method
```csharp
void SetErrorHandler(ErrorHandler handler)
```

Set a specific error handler for the context. The handler replaces any
existing handler.

### Parameters
`handler` [ErrorHandler](./ErrorHandler)  
The error handler.
