# ProfileSequenceItem Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents an item in a profile sequence. This class cannot be inherited.

```csharp
public sealed class ProfileSequenceItem
```

Inheritance Object → ProfileSequenceItem

## Properties
## Attributes Property

```csharp
public DeviceAttributes Attributes { get; set; }
```

### Property Value

`Attributes` [DeviceAttributes](./DeviceAttributes.md)  
Gets or sets the attributes unique to the particular device setup for which the profile is applicable.

---
## Description Property

```csharp
public MultiLocalizedUnicode Description { get; set; }
```

### Property Value

`Description` [MultiLocalizedUnicode](./MultiLocalizedUnicode.md)  
Gets or sets the description of the profile.

### Remarks

Responsibility for freeing resources associated with the `MultiLocalizedUnicode` object used to set the value is passed to the `ProfileSequenceItem`.

---
## DeviceMfg Property

```csharp
public uint DeviceMfg { get; set; }
```

### Property Value

`DeviceMfg` uint  
Gets or sets the signature of the device manufacturer of the profile.

---
## DeviceModel Property

```csharp
public uint DeviceModel { get; set; }
```

### Property Value

`DeviceModel` uint  
Gets or sets the signature of the device model of the profile.

---
## Manufacturer Property

```csharp
public MultiLocalizedUnicode Manufacturer { get; set; }
```

### Property Value

`Manufacturer` [MultiLocalizedUnicode](./MultiLocalizedUnicode.md)  
Gets or sets the manufacturer string of the profile.

### Remarks

Responsibility for freeing resources associated with the `MultiLocalizedUnicode` object used to set the value is passed to the `ProfileSequenceItem`.

---
## Model Property

```csharp
public MultiLocalizedUnicode Model { get; set; }
```

### Property Value

`Model` [MultiLocalizedUnicode](./MultiLocalizedUnicode.md)  
Gets or sets the model string of the profile.

### Remarks

Responsibility for freeing resources associated with the `MultiLocalizedUnicode` object used to set the value is passed to the `ProfileSequenceItem`.

---
## ProfileID Property

```csharp
public byte[] ProfileID { get; set; }
```

### Property Value

`ProfileID` byte[]  
Gets or sets the profile ID of the profile.

---
## Technology Property

```csharp
public TechnologySignature Technology { get; set; }
```

### Property Value

`Technology` [TechnologySignature](./TechnologySignature.md)  
Gets or sets the ICC technology of the profile.
