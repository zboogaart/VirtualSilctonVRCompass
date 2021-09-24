using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Time.deltaTime* 0.05f  , 0.5f, 1);
        transform.Rotate(0.5f, Time.deltaTime * 0.05f, 1, Space.World);
    }
}
