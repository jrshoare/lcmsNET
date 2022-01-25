# IT8 Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Defines methods and properties for reading and writing ANSI
CGATS.17 text files. This class cannot be inherited.

```csharp
public sealed class IT8 : IDisposable
```

Inheritance Object → IOHandler

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
### DoubleFormat Property

```csharp
public string DoubleFormat { set; }
```

### Property Value

`DoubleFormat`  
Sets the format string for floating point numbers using
the "C" sprintf convention.

### Remarks

The default format is "%.10g".

---
## Properties Property

```csharp
public IEnumerable<string> Properties { get; }
```

### Property Value

`Properties`  
Gets an object that can be used to enumerate all
properties in the current table.

---
## SampleNames Property

```csharp
public IEnumerable<string> SampleNames { get; }
```

### Property Value

`SampleNames`  
Gets an object that can be used to enumerate the sample
names for the columns in the current table.

---
## SheetType Property

```csharp
public string SheetType { get; set; }
```

### Property Value

`SheetType`  
Gets or sets the identifier placed on the very first line
of the CGATS.17 file.

---
## TableCount Property

```csharp
public uint TableCount { get; }
```

### Property Value

`TableCount`  
Gets the number of tables in this instance.

### Remarks

An `IT8` instance created with `Create(Context)` initially
has one table allocated.

## Methods
## AddComment(string) Method

```csharp
public bool AddComment(string comment)
```

Adds a comment to the file.

### Parameters

`commment` string  
The comment to be inserted.

### Returns
`true` if added successfully, otherwise `false`.

### Remarks

Successive calls to this method add comments in the same
order as invoked.

---
## Create(Context) Method

```csharp
public static IT8 Create(Context context)
```

Creates a new instance of the IT8 class

### Parameters

`context` [Context](./Context)  
User defined context. Can be `null` to create the instance
in the global context.

### Returns

`IT8`  
The new `IT8` that this method creates.

---
## Dispose Method

```csharp
public void Dispose()
```

Disposes this instance.

---
## FindDataFormat(string) Method

```csharp
public int FindDataFormat(string sample)
```

Gets the zero-based column position of a given data sample
name in the current table.

### Parameters

`sample` string  
The sample name.

### Returns

`int`  
The column number, or -1 if not found.

---
## GetData(int, int) Method

```csharp
public string GetData(int row, int column)
```

Gets a cell [row, column] value as a literal string in
the current table.

### Parameters

`row` int  
The zero-based row index.

`column` int
The zero-based column index.

### Returns

`string`  
The literal string value of the cell, or `null` on error.

---
## GetData(string, string) Method

```csharp
public string GetData(string patch, string sample)
```

Gets a cell [patch, sample] value as a literal string in
the current table.

### Parameters

`patch` string  
The intended patch name (row).

`sample` string  
The intended sample name (column).

### Returns

`string`  
The literal string value of the cell, or `null` on error.

---
## GetDoubleData(int, int) Method

```csharp
public double GetDoubleData(int row, int column)
```

Gets a cell [row, column] value as a floating point number in
the current table.

### Parameters

`row` int  
The zero-based row index.

`column` int
The zero-based column index.

### Returns

`double`  
The floating point value of the cell, or 0 on error.

---
## GetDoubleData(string, string) Method

```csharp
public double GetDoubleData(string patch, string sample)
```

Gets a cell [patch, sample] value as a floating point number
in the current table.

### Parameters

`patch` string  
The intended patch name (row).

`sample` string  
The intended sample name (column).

### Returns

`double`  
The floating point value of the cell, or 0 on error.

---
## GetDoubleProperty(string) Method

```csharp
public double GetProperty(string name)
```

Gets the value of a property in the current table as a
floating point number.

### Parameters

`name` string  
The name of the property.

### Returns

`double`  
The floating point value of the property, or 0 on error.

---
## GetPatchName(int) Method

```csharp
public string GetPatchName(int nPatch)
```

Gets the value of the first column (patch name) for the
given set number.

## Parameters

`nPatch` int  
The zero-based set number.

### Returns

`string`  
The patch name, or null on error.

---
## GetProperty(string) Method

```csharp
public string GetProperty(string name)
```

Gets the value of a property in the current table as a
literal string.

### Parameters

`name` string  
The name of the property.

### Returns

`string`  
The literal string value of the property, or `null` on error.

---
## GetProperties(string) Method

```csharp
public IEnumerable<string> GetProperties(string name)
```

Gets an object that can be used to enumerate all
sub-property names for a multi-value property in the
current table.

### Parameters

`name` string  
The name of the multi-value property.

### Returns

`IEnumerable<string>`  
An object that can be used to enumerate the sub-property
names.

---
## Open(Context, string) Method

```csharp
public static IT8 Open(Context context, string filepath)
```

Opens and populates a new instance of the `IT8` class from
the contents of an existing CGATS file.

### Parameters

`context` [Context](./Context.md)  
A context, or `null` for the global context.

`filepath` string  
The full path to the file.

### Returns

`IT8`  
A new `IT8` instance.

---
## Open(Context, byte[]) Method

```csharp
public static IT8 Open(Context context, byte[] memory)
```

Opens and populates a new instance of the `IT8` class from
the supplied memory block.

### Parameters

`context` [Context](./Context.md)  
A context, or `null` for the global context.

`memory` byte[]  
A block of contiguous memory containing the CGATS.17 data.

### Returns

`IT8`  
A new `IT8` instance.

---
## Save(string) Method

```csharp
public bool Save(string filepath)
```

Saves the instance to file in CGATS.17 format.

### Parameters

`filepath` string  
The full path to the file.

### Returns

`bool`  
`true` if saved successfully, otherwise `false`.

---
## Save(byte[], out uint) Method

```csharp
public bool Save(byte[] it8, out uint bytesNeeded)
```

Saves the instance to a contiguous memory block in CGATS.17 format.

### Parameters

`it8` byte[]  
A byte array sufficiently sized to contain the data, or
`null` to calculate the required size.

`bytesNeeded` uint  
On return defines the number of bytes required to contain
the data.

### Returns

`bool`  
`true` if saved successfully, otherwise `false`.

---
## SetData(int, int, string) Method

```csharp
public bool SetData(int row, int column, string value)
```

Sets a cell [row, column] value to a literal string in the
current table.

### Parameters

`row` int  
The zero-based row index.

`column` int  
The zero-based column index.

`value` string  
The literal string value to be set.

### Returns

`bool`  
`true` if set successfully, otherwise `false`.

---
## SetData(string, string, string) Method

```csharp
public bool SetData(string patch, string sample, string value)
```

Sets a cell [patch, sample] value to a literal string in the
current table.

### Parameters

`patch` string  
The intended patch name (row).

`sample` string  
The intended sample name (column).

`value` string  
The literal string value to be set.

### Returns

`bool`  
`true` if set successfully, otherwise `false`.

---
## SetData(int, int, double) Method

```csharp
public bool SetData(int row, int column, double value)
```

Sets a cell [row, column] value to a floating point number
in the current table.

### Parameters

`row` int  
The zero-based row index.

`column` int  
The zero-based column index.

`value` double  
The floating point value to be set.

### Returns

`bool`  
`true` if set successfully, otherwise `false`.

---
## SetData(string, string, double) Method

```csharp
public bool SetData(string patch, string sample, double value)
```

Sets a cell [patch, sample] value to a floating point number
in the current table.

### Parameters

`patch` string  
The intended patch name (row).

`sample` string  
The intended sample name (column).

`value` double  
The floating point value to be set.

### Returns

`bool`  
`true` if set successfully, otherwise `false`.

---
## SetDataFormat(int, string) Method

```csharp
public bool SetDataFormat(int column, string sample)
```

Sets the column names in the current table.

### Parameters

`column` int  
The zero-based column number.

`sample` string  
The sample name for the column.

### Returns

`bool`  
`true` if set successfully, otherwise `false`.

### Remarks

The first column is "SAMPLE_ID".

The special property "NUMBER_OF_FIELDS" must be set for
the current table before calling this method.

---
## SetProperty(string, double) Method

```csharp
public bool SetProperty(string name, double value)
```

Sets a floating point property in the current table.

### Parameters

`name` string  
The name of the property.

`value` double  
The property value to be set.

### Returns

`bool`  
`true` if set successfully, otherwise `false`.

---
## SetProperty(string, string) Method

```csharp
public bool SetProperty(string name, string value)
```

Sets a string property in the current table.

### Parameters

`name` string  
The name of the property.

`value` string  
The property value to be set.

### Returns

`bool`  
`true` if set successfully, otherwise `false`.

### Remarks

Sub-properties are allowed if the value is a string in
the form:  

"SUBPROP1,1;SBPROP2,2;..."
---
## SetProperty(string, string, string) Method

```csharp
public bool SetProperty(string key, string subkey, string value)
```

Adds a new sub-property to an existing property in the current table.

### Parameters

`key` string  
An existing property in the current table.

`subkey` string  
The name of the sub-property.

`value` string  
The property value to be set.

### Returns

`bool`  
`true` if set successfully, otherwise `false`.

---
## SetProperty(string, uint) Method

```csharp
public bool SetProperty(string name, uint hex)
```

Sets a hexadecimal constant (appends 0x) property in the current table.

### Parameters

`name` string  
The name of the property.

`value` uint  
The property value to be set.

### Returns

`bool`  
`true` if set successfully, otherwise `false`.

---
## SetTable(uint) Method

```csharp
public static int SetTable(uint nTable)
```

Sets the current table.

### Parameters

`nTable` uint  
Zero-based index of the table.

### Returns

`int`  
The index of the current table, or -1 on error.

### Remarks

Set `nTable` to `TableCount` to add a new table.

---
## SetUncookedProperty(string, string) Method

```csharp
public bool SetUncookedProperty(string name, string value)
```

Sets a property with no interpretation in the current table.

### Parameters

`name` string  
The name of the property.

`value` string  
The property value to be set.

### Returns

`bool`  
`true` if set successfully, otherwise `false`.

### Remarks

Special prefixes:  
- 0b: Binary  
- 0x: Hexadecimal
