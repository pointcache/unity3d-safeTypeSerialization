﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

    public TestScriptable data;

	// Use this for initialization
	void Start () {
        var newobj = Activator.CreateInstance(data.classToSpawn);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
