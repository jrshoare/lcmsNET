# Dict Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a dictionary of name-value pairs. This class cannot be inherited.

```csharp
public sealed class Dict : IEnumerable<DictEntry>, IDisposable
```

Inheritance Object → Dict

Implements IEnumerable<DictEntry>, IDisposable

## Examples

The following code example demonstrates how to construct a new Dict and add a name-value pair.

```csharp
using lcmsNET;

string name = "name";
string value = "value";

using (var dict = Dict.Create(null))
using (var displayName = MultiLocalizedUnicode.Create(null, 0))
{
    displayName.SetWide("en", "US", "Hello");

    bool added = dict.Add(name, value, displayName, null);
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
## Add(string, string, MultiLocalizedUnicode, MultiLocalizedUnicode) Method

```csharp
public bool Add(string name, string value, MultiLocalizedUnicode displayName, MultiLocalizedUnicode displayValue)
```

Adds a name-value pair item to the dictionary.

### Parameters

`name` `string`  
The name of the item to be added.

`value` `string`  
The value of the item to be added.

`displayName` [MultiLocalizedUnicode](./MultiLocalizedUnicode)  
The display name of the item to be added. Can be null.

`displayValue` [MultiLocalizedUnicode](./MultiLocalizedUnicode)  
The display value of the item to be added. Can be null.

### Returns

`bool`  
`true` if the item was added successfully, otherwise `false`.

---
## Create(Context) Method

```csharp
public static Dict Create(Context context)
```

Creates an instance of the Dict class.

### Parameters

`context` [Context](./Context)  
User defined context.

### Returns

`Dict`  
The new `Dict` that this method creates.

---
## Duplicate() Method

```csharp
public Dict Duplicate()
```

Creates a duplicate instance of the Dict class.

### Returns

`Dict`  
The duplicate `Dict` that this method creates.

---
## FromHandle(IntPtr) Method

```csharp
public static Dict FromHandle(IntPtr handle)
```

Creates an instance of the Dict class from the supplied handle.

### Parameters

`handle` `IntPtr`  
A handle to an existing dictionary.

### Returns

`Dict`  
The new `Dict` that this method creates.

### Examples

The following example adds a dictionary containing three items to a profile and uses the value of the tag read back to create a read-only copy of the dictionary.

```csharp
using (var profile = Profile.CreatePlaceholder(null))
{
    using (var dict = Dict.Create(null))
    using (var mlu = MultiLocalizedUnicode.Create(null, 0))
    {
        mlu.SetASCII("en", "GB", "Hello");

        dict.Add("first", null, null, null);
        dict.Add("second", "second-value", null, null);
        dict.Add("third", "third-value", mlu, null);

        profile.WriteTag(TagSignature.Meta, dict.Handle);
    }

    using (var roDict = Dict.FromHandle(profile.ReadTag(TagSignature.Meta)))
    {
        int count = roDict.Count(); // -> 3
    }
}
```

### Remarks

The instance created should be considered read-only for `handle` values returned from `Profile.ReadTag(TagSignature)`.

---
## GetEnumerator() Method

```csharp
public IEnumerator<DictEntry> GetEnumerator()
```

Returns an enumerator that iterates through the dictionary.

### Returns

`IEnumerator<DictEntry>`  
An enumerator that can be used to iterate through each [DictEntry](./DictEntry) item in the dictionary.

---
## IEnumerable.GetEnumerator() Method

```csharp
IEnumerator IEnumerable.GetEnumerator()
```

Returns an enumerator that iterates through the dictionary.

### Returns

`IEnumerator`  
An enumerator that can be used to iterate through each item in the dictionary.
