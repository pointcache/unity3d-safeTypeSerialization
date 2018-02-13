using System.Collections;
using System.Collections.Generic;
using pointcache.SerializedType;
using UnityEngine;

public class TestScriptable : ScriptableObject {

    [SpecifyBaseType(typeof(Ammo))]
    public SerializedType classToSpawn;

    public SerializedType asdasd;

    public List<SerializedType> componentsToAdd = new List<SerializedType>();

}
