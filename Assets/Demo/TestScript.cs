using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

    public TestScriptable data;

	// Use this for initialization
	void Start () {
        var newobj = Activator.CreateInstance(data.classToSpawn);
        foreach (var item in data.componentsToAdd) {
            gameObject.AddComponent(item);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
