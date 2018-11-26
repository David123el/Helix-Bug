using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour {

    public static int targetFrameRate;

    // Use this for initialization
    void Start () {

        Application.targetFrameRate = 60;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
