# NamedColorList Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a named color list. This class cannot be inherited.

```csharp
public sealed class NamedColorList : IDisposable
```

Inheritance Object → NamedColorList

Implements IDisposable

## Properties
## Context Property

```csharp
public Context Context { get; }
```

### Property Value

`Context`  
The `Context` supplied to create this instance.

---
## Count Property

```csharp
public uint Count { get; }
```

### Property Value

`Count`  
Gets the number of spot colors in the named color list.

---
## Handle Property

```csharp
public IntPtr Handle { get; }
```

### Property Value

`Handle` IntPtr  
Gets the handle to the named color list.

## Methods
## Add(string, ushort[] ushort[]) Method

```csharp
public bool Add(string name, ushort[] pcs, ushort[] colorant)
```

Adds a new spot color to the list.

### Parameters

`name` string  
The spot color name.

`pcs` ushort[]  
An array of 3 values encoding the PCS coordinates.

`colorant` ushort[]  
An array of 16 values encoding the device colorant.

### Returns

`bool`  
`true` if the spot color was added to the list, otherwise `false`.

---
## Create(Context, uint, uint, string, string) Method

```csharp
public static NamedColorList Create(Context context, uint n, uint colorantCount, string prefix, string suffix)
```

Creates a new instance of the `NamedColorList` class.

### Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

`n` uint  
The initial number of spot colors in the list.

`colorantCount` uint  
The number of channels in the device space.

`prefix` string  
Prefix for all spot color names.

`suffix` string  
Suffix for all spot color names.

### Returns

`NamedColorList`  
A new `NamedColorList` instance.

---
## Dispose() Method

```csharp
public void Dispose()
```

Disposes this instance.

---
## Duplicate() Method

```csharp
public NamedColorList Duplicate()
```

Duplicates a named color list.

### Returns

`NamedColorList`  
A new `NamedColorList` instance.

---
## FromHandle(intPtr) Method

```csharp
public static NamedColorList FromHandle(IntPtr handle)
```

Creates a named color list from the supplied handle.

### Parameters

`handle` IntPtr  
A handle to an existing named color list.

### Returns

`NamedColorList`  
A new `NamedColorList` instance referencing an
existing named color list.

---
## GetInfo(uint, out string, out string, out string, out ushort[] out ushort[]) Method

```csharp
public string GetInfo(uint nColor, out string name, out string prefix, out string suffix, out ushort[] pcs, out ushort[] colorant)
```

Gets information for a spot color with the given index.

### Parameters

`nColor` uint  
The index of the spot color.

`name` string  
Returns the name of the spot color.

`prefix` string  
Returns the prefix for the spot color.

`suffix` string  
Returns the suffix for the spot color.

`pcs` ushort[]  
Returns an array of 3 values encoding the PCS coordinates for the spot color.

`colorant` ushort[]  
Returns an array of 16 values encoding the device colorant for the spot color.

### Returns

`bool`  
`true` if successful, otherwise `false`.

---
## this[string] Method

```csharp
public int this[string name]
```

Gets the index of the given spot color name.

### Parameters

`name` string  
The name of the spot color.

### Returns

`int`  
The zero-based index of the spot color, or -1 if not found.
