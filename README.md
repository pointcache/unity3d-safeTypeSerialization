# unity3d-safeTypeSerialization
Serialize type fields using ID attributes

## How to use

Add a ExposeTypeAttribute to every class that should be exposed to the type serializer and assign it a custom ID.

```cs
using pointcache.SerializedType;

[ExposeType ("anything can be here, some GUID or whatever you like, just make sure you dont change it")]
public class Weapon {}
```

The ID makes sure that the serialization retains even if you change the type name or namespace as long as the id stays the same.

To get a reference to a type you use the SerializedType type.

```cs
using UnityEngine;
using pointcache.SerializedType;

public class WeaponAsset : ScriptableObject
{
  // Will show only types derived from WeaponBase in inspector
  [SpecifyBaseType(typeof(WeaponBase))]
  public SerializedType WeaponType;
}
```

![How it looks in the inspector](https://i.imgur.com/pzL32gW.png)

To access the type itself you can simply do something like this (because it has an implicit conversion to System.Type):

```cs
System.Activator.CreateInstance (WeaponType);
```
