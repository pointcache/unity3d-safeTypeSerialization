using System.Collections;
using System.Collections.Generic;
using pointcache.TypeField;
using UnityEngine;

public class TestScriptable : ScriptableObject {

    public TypeField classToSpawn;

    public List<TypeField> componentsToAdd = new List<TypeField>();
}
