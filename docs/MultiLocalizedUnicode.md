# MultiLocalizedUnicode Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a multi-localized Unicode string. This class cannot be inherited.

```csharp
public sealed class MultiLocalizedUnicode : IDisposable
```

Inheritance Object → MultiLocalizedUnicode

Implements IDisposable

## Constants
## NoLanguage Constant

```csharp
public const string NoLanguage = "\0\0"
```

The language code for 'no language'.

---
## NoCountry Constant

```csharp
public const string NoCountry = "\0\0"
```

The country code for 'no country'.

## Properties
## Context Property

```csharp
public Context Context { get; }
```

### Property Value

`Context`  
The `Context` supplied to create this instance.

---
## Handle Property

```csharp
public IntPtr Handle { get; }
```

### Property Value

`Handle` IntPtr  
Gets the handle to the multi-localized Unicode string.

---
## TranslationsCount Property

```csharp
public uint TranslationsCount { get; }
```

### Property Value

`TranslationsCount` uint  
Gets the number of translations stored in the multi-localized
string.

### Remarks

Requires Little CMS 2.5 or later.

## Methods
## Create(Context, uint) Method

```csharp
public static MultiLocalizedUnicode Create(Context context, uint nItems=0)
```

Creates a new instance of the `MultiLocalizedUnicode` class.

## Parameters

`context` [Context](./Context.md)  
A context, or null for the global context.

`nItems` uint  
The initial number of items to be allocated.

## Returns

`MultiLocalizedUnicode`  
A new `MultiLocalizedUnicode` instance.

---
## Dispose Method

```csharp
public void Dispose()
```

Disposes this instance.

---
## Duplicate() Method

```csharp
public MultiLocalizedUnicode Duplicate()
```

Duplicates a multi-localized Unicode string.

## Returns

`MultiLocalizedUnicode`  
A new `MultiLocalizedUnicode` instance.

---
## FromHandle(intPtr) Method

```csharp
public static MultiLocalizedUnicode FromHandle(IntPtr handle)
```

Creates a multi-localized Unicode string from the supplied handle.

### Parameters

`handle` IntPtr  
A handle to an existing multi-localized Unicode string.

### Returns

`MultiLocalizedUnicode`  
A new `MultiLocalizedUnicode` instance referencing an
existing multi-localized Unicode string.

---
## GetASCII(string, string) Method

```csharp
public string GetASCII(string languageCode, string countryCode)
```

Gets the ASCII (7 bit) entry for the given language and
country code.

### Parameters

`languageCode` string  
The ISO 639-1 language code.

`countryCode` string  
The ISO 3166-1 country code.

### Returns

`string`  
The entry, or `null` if not found.

---
## GetTranslation(string, string, out string, out string) Method

```csharp
public bool GetTranslation(string languageCode, string countryCode, out string translationLanguage, out string translationCountry)
```

Gets the translation rule for the given language and country code.

### Parameters

`languageCode` string  
The ISO 639-1 language code.

`countryCode` string  
The ISO 3166-1 country code.

`translationLanguage` string  
Returns the ISO 639-1 language code obtained if successful,
otherwise `null`.

`translationCountry` string  
Returns the ISO 3166-1 country code obtained if successful,
otherwise `null`.

### Returns

`bool`  
`true` if a translation exists, otherwise `false`.

### Remarks

The algorithm searches for an exact match of language and
country code. if not found it attempts to match the
language code, and if this does not yield a result the first
entry is returned.

---
## GetWide(string, string) Method

```csharp
public bool GetWide(string languageCode, string countryCode)
```

Gets the Unicode wide character (16 bit) entry for the given
language and country code.

### Parameters

`languageCode` string  
The ISO 639-1 language code.

`countryCode` string  
The ISO 3166-1 country code.

### Returns

`string`  
The entry, or `null` if not found.

---
## SetASCII(string, string, string) Method

```csharp
public bool SetASCII(string languageCode, string countryCode, string value)
```

Sets an ASCII (7 bit) entry for the given language and
country code.

### Parameters

`languageCode` string  
The ISO 639-1 language code.

`countryCode` string  
The ISO 3166-1 country code.

`value` string  
The value to be set.

### Returns

`bool`  
`true` if the value is set successfully, otherwise `false`.

---
## SetWide(string, string, string) Method

```csharp
public bool SetWide(string languageCode, string countryCode, string value)
```

Sets a Unicode wide character (16 bit) entry for the given
language and country code.

### Parameters

`languageCode` string  
The ISO 639-1 language code.

`countryCode` string  
The ISO 3166-1 country code.

`value` string  
The value to be set.

### Returns

`bool`  
`true` if the value is set successfully, otherwise `false`.

---
### TranslationCode(uint, out string, out string) Method

```csharp
public bool TranslaionCodes(uint index, out string languageCode, out string countryCode)
```

Gets the language and country codes for the given translation
index.

### Parameters

`index` uint  
The zero-based index of the translation.

`languageCode` string  
Returns the ISO 639-1 language code obtained if successful,
otherwise `null`.

`countryCode` string  
Returns the ISO 3166-1 country code obtained if successful,
otherwise `null`.

### Returns

`bool`  
`true` if successful, otherwise `false`.
