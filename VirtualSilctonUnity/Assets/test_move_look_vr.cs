using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class test_move_look_vr : MonoBehaviour
{

    public Transform Reference_Frame;
    public GameObject game_object;


    // Start is called before the first frame update
    void Start()
    {
      int randNum = UnityEngine.Random.Range(0,360);
      game_object.transform.eulerAngles = new Vector3(0,randNum,0);
      Debug.Log("The random number that was chosen for the starting global rotation is: " + randNum);
    }

    // Update is called once per frame
    void Update()
    {

        //We should actually use the global rotation, does not matter reference frame in the physical world, will always start at same location, 0 degrees is pointing towards trees ground lights
        Debug.Log("This is the angle of rotation for the Camera Reference globally");
        Debug.Log(Reference_Frame.transform.rotation.eulerAngles.y);
        Debug.Log("This is the angle of rotation for the Camera Reference locally");
        Debug.Log(Reference_Frame.transform.localRotation.eulerAngles.y);

    }
}
