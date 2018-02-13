using System.Collections;
using System.Collections.Generic;
using pointcache.TypeField;
using UnityEngine;

public class TestScriptable : ScriptableObject {

    public SerializedType classToSpawn;

    public List<SerializedType> componentsToAdd = new List<SerializedType>();
}
