# GamutBoundaryDescriptor class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a gamut boundary description. This class cannot be inherited.

```csharp
public sealed class GamutBoundaryDescriptor : IDisposable
```

Inheritance Object → GamutBoundaryDescriptor

Implements IDisposable

## Examples

The following code example demonstrates how to construct a new GamutBoundaryDescriptor, add points, compute and check whether a Lab value is inside.

```csharp
using lcmsNET;

using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
using (var gbd = GamutBoundaryDescriptor.Create(context))
{
    CIELab add = new CIELab { };
    for (int L = 0; L <= 100; L += 10)
        for (int a = -128; a <= 128; a += 5)
            for (int b = -128; b <= 128; b+= 5)
            {
                add.L = L;
                add.a = a;
                add.b = b;
                gbd.AddPoint(add);
            }

    gbd.Compute();

    CIELab check = new CIELab { };
    for (int L = 10; L <= 90; L += 25)
        for (int a = -120; a <= 120; a += 25)
            for (int b = -120; b <= 120; b += 25)
            {
                check.L = L;
                check.a = a;
                check.b = b;

                bool isInsideGamut = gbd.CheckPoint(check);
            }
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
## AddPoint(CIELab) Method

```csharp
public bool AddPoint(in CIELab lab)
```

Adds a new sample point for computing the gamut boundary descriptor.

### Parameters

`lab` [CIELab](./CIELab)
A Lab value to be added to the boundary descriptor.

### Returns
`true` if the point is added successfully, otherwise `false`.

---
## CheckPoint(CIELab) Method

```csharp
public bool CheckPoint(in CIELab lab)
```

Checks whatever a Lab value is inside a given gamut boundary descriptor.

### Parameters

`lab` [CIELab](./CIELab)
The point to be checked.

### Returns
`true` if the point is inside the gamut, otherwise `false`.

---
## Compute(uint) Method

```csharp
public bool Compute(uint flags = 0)
```

Computes the gamut boundary descriptor using all know points and interpolating any missing sector(s). Call this function after adding all known points and before invoking `CheckPoint(CIELab)`.

### Parameters

`flags` `uint`  
Reserved. Set to 0.

### Returns
`true` if computation is successful, otherwise `false`.

---
## Create(Context) Method

```csharp
public static GamutBoundaryDescriptor Create(Context context)
```

Creates an instance of the GamutBoundaryDescriptor class.

### Parameters

`context` [Context](./Context)  
User defined context.

### Returns

`GamutBoundaryDescriptor`  
The new `GamutBoundaryDescriptor` that this method creates.
