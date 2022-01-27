# LcmsNETException Class

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Serves as the base class for lcmsNET exceptions.

```csharp
public class LcmsNETException : Exception
```

Inheritance Object → Exception → LcmsNETException

## Constructors
## LcmsNETException()

```csharp
public LcmsNETException()
```

Initialises a new instance of the LcmsNETException class.

---
## LcmsNETException(string)

```csharp
public LcmsNETException(string message)
```

Initialises a new instance of the LcmsNETException class with
the specified message.

### Parameters

`message` string  
The message that describes the error.

---
## LcmsNETException(string, Exception)

```csharp
public LcmsNETException(string message, Exception innerException)
```

Initialises a new instance of the LcmsNETException class with
the specified message and a reference to the inner exception
that is the cause of this exception.

### Parameters

`message` string  
The message that describes the error.

`innerException` Exception  
The exception that is the cause of the current exception. If
the inner exception is not `null`, the current exception is
raised in a catch block that handles the inner exception.

---
## LcmsNETException(SerializationInfo, StreamingContext)

```csharp
protected LcmsNETException(SerializationInfo info, StreamingContext context)
```

Initialises a new instance of the LcmsNETException class
with serialised data.

### Parameters

`info` SerializationInfo  
The object that holds the serialised object data.

`context` StreamingContext  
The contextual information about the source or destination.
