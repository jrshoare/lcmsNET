# lcmsNET
[![GitHub license](https://img.shields.io/static/v1?label=license&message=MIT&color=green)](https://github.com/jrshoare/lcmsNET/blob/master/LICENSE)
[![Build status](https://travis-ci.org/jrshoare/lcmsNET.svg?branch=master)](https://travis-ci.org/jrshoare/lcmsNET)

.NET bindings for Little CMS (http://www.littlecms.com/)

# Requirements
The following software is required if you want only to use this library:  
* [Little CMS](http://www.littlecms.com/download.html) >= 2.9

The following applications are needed to build this library:

* [.NET Core SDK](https://dotnet.microsoft.com/download/visual-studio-sdks)

To use on Windows, make sure that `lcms2.dll` is in the [DLL search path](https://docs.microsoft.com/en-gb/windows/win32/dlls/dynamic-link-library-search-order), e.g. the PATH environment variable.

To use on Linux see the [API Reference](http://www.littlecms.com/LittleCMS2.9%20API.pdf) documentation on the Little CMS web site for installation instructions.

This library is available as a [NuGet package](https://www.nuget.org/packages/lcmsNET/) too.

# How to build

To build this library you need to install the [.NET Core SDK](https://dotnet.microsoft.com/download/visual-studio-sdks).

To build the library, run the following command in the project folder:
```
dotnet build -c Release
```
A successful build will put the resulting library in
```
..\..\bin\netstandard2.0
```

# How to use

To use this library in your project add the following statement to your source code:
```csharp
using lcmsNET;
```

## Print Little CMS library version
```csharp
Console.WriteLine(Cms.EncodedCMMVersionTest);
```

## Set error handler
```csharp
void HandleError(IntPtr contextID, int errorCode, string errorText)
{
    // do something with the error, e.g. log it to file
}

Cms.SetErrorHandler(HandleError);

...

// restore default error handler
Cms.SetErrorHandler(null);
```

## Open a profile for reading
```csharp
using (var profile = Profile.Open(profilePath, "r"))
{
    // do something with the profile
}
```

## Create and use a transform
```csharp
using (var input = Profile.Open(inputProfilePath, "r"))
using (var output = Profile.Open(outputProfilePath, "r"))
using (var transform = Transform.Create(input, Cms.TYPE_RGB_8, output, Cms.TYPE_RGB_8,
        Intent.Perceptual, CmsFlags.None))
{
    byte[] source = ....
    byte[] dest = new byte[source.Length];
    transform.DoTransform(source, dest, pixelCount);
}

```

## Other
See the unit tests for examples of how to invoke each supported method and property.

# Future work
* Plug ins
* IO handlers
* PostScript generation
* CGATS.17-200x handling
* Conversion of return from `Profile.ReadTag(TagSignature)` to class instance  
(Note that conversion to `struct` is already supported via `Marshal.PtrToStructure<T>(IntPtr)`)