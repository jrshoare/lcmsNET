# CAM02 Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents the CIE CAM02 color appearance model. This class cannot be inherited.

```csharp
public sealed class CAM02 : IDisposable
```

Inheritance Object → CAM02

Implements IDisposable

## Examples

The following code example demonstrates how to construct a new CAM02 and evaluate the model in forward and reverse directions.

```csharp
using lcmsNET;

IntPtr plugin = IntPtr.Zero;
IntPtr userData = IntPtr.Zero;
ViewingConditions conditions = new ViewingConditions
{
    whitePoint = Colorimetric.D50_XYZ,
    Yb = 1.0,
    La = 0.0,
    surround = 3,
    D_value = 0.75
};
CIEXYZ xyz = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 };

using (var context = Context.Create(plugin, userData))
using (var cam02 = CAM02.Create(context, conditions))
{
    cam02.Forward(xyz, out JCh jch);

    cam02.Reverse(jch, out CIEXYZ xyz2);
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
## Create(Context, ViewingConditions) Method

```csharp
public static CAM02 Create(Context context, in ViewingConditions conditions)
```

Creates an instance of the CAM02 class.

### Parameters

`context` [Context](./Context)  
User defined context.

`conditions` [ViewingConditions](./ViewingConditions)  
Viewing conditions.

### Returns

`CAM02`  
The new `CAM02` that this method creates.

---
## Forward(CIEXYZ, out JCh) Method

```csharp
public void Forward(in CIEXYZ xyz, out JCh jch)
```

Evaluates the model in the forward direction XYZ → JCh.

### Parameters

`xyz` [CIEXYZ](./CIEXYZ)  
The input XYZ value.

`jch` [JCh](./JCh)
The output JCh value.

---
## Reverse(JCh, out CIEXYZ) Method

```csharp
public void Reverse(in JCh jch, out CIEXYZ xyz)
```

Evaluates the model in the reverse direction JCh → XYZ.

### Parameters

`jch` [JCh](./JCh)
The input JCh value.

`xyz` [CIEXYZ](./CIEXYZ)  
The output XYZ value.  
