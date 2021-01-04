# IOHandler Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents an abstraction used to deal with files or streams. This class cannot be inherited.

```csharp
public sealed class IOHandler : IDisposable
```

Inheritance Object → IOHandler

Implements IDisposable

## Examples

The following code example demonstrates how to construct a new IOHandler from both a file and null.

```csharp
using lcmsNET;

using (var context1 = Context.Create(IntPtr.Zero, IntPtr.Zero))
using (var iohandlerFile = IOHandler.Open(context, "file-path", "r"))
{
    // reads from file at 'file-path'
}

using (var context2 = Context.Create(IntPtr.Zero, IntPtr.Zero))
using (var iohandlerNull = IOHandler.Open(context))
{
    // read & write to null
}
```

## Properties
## Context Property

```csharp
public Context Context { get; }
```

### Property Value

`Context`  
The `Context` supplied to create this instance.

## Methods
## Open(Context) Method

```csharp
public static IOHandler Open(Context context)
```

Creates an instance of the IOHandler class that reads & write to null. All read operations return 0 bytes and set the EOF flag. All write operations discard the given data.

### Parameters

`context` [Context](./Context)  
User defined context.

### Returns

`IOHandler`  
The new `IOHandler` that this method creates.

---
## Open(Context, string, string) Method

```csharp
public static IOHandler Open(Context context, string filepath, string access)
```

Creates an IOHandler instance from a disk-based file.

### Parameters

`context` [Context](./Context)  
User defined context.

`filepath` `string`  
Full path to the file.

`access` `string`  
"r" to read, "w" to write.

### Returns

`IOHandler`  
The new `IOHandler` that this method creates.
