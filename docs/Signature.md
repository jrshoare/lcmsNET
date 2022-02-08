# Signature Struct

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a signature.

```csharp
public struct Signature
```

Inheritance Object → ValueType → Signature

## Constructors
## Signature(uint) Constructor

```csharp
public Signature(uint u)
```

Initialises a new instance of the `Signature` struct.

### Parameters

`u` uint  
The signature value.

## Methods
## implicit operator uint(Signature) Method

```csharp
public static implicit operator uint(Signature signature)
```

Implicitly converts a `Signature` to an unsigned integer.

### Parameters

`signature` [Signature](./Signature.md)  
The signature to be converted.

### Returns

`uint`  
The unsigned integer value of the signature.

---
## explicit operator Signature(uint) Method

```csharp
public static explicit operator Signature(uint u)
```

Explicitly converts an unsigned integer to a `Signature`.

### Parameters

`u` uint  
The unsigned integer value to be converted.

### Returns

`Signature`  
A new `Signature` instance.
