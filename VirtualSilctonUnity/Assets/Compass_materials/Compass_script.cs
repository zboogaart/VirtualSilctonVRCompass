using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass_script : MonoBehaviour
{

    //Attempt to write my own code for this:
    public Transform desired_target;
    public Transform compass;
    public Transform game_object;
    public Transform person;

    public bool SceneA;
    public bool SceneB;
    public bool SceneC1;
    public bool SceneC2;

    Vector3 vector;
    float desired_angle;
    int compass_direction;


    // Start is called before the first frame update
    void Awake()
    {
      // Vector3 desired_vector = desired_target.position - person.position;
      // if(SceneA){
      //   desired_angle = Vector3.Angle(desired_vector, compass.transform.forward);
      // }else if(SceneB){
      //   desired_angle = Vector3.Angle(desired_vector, compass.transform.forward);
      // }else if(SceneC1){
      //   desired_angle = Vector3.Angle(desired_vector, compass.transform.forward);
      // }else if(SceneC2){
      //   desired_angle = Vector3.Angle(desired_vector, compass.transform.forward);
      // }
      //
      // compass.transform.Rotate(0f,0f,desired_angle,Space.Self);
      //
      // Debug.Log("The desired angle: " + desired_angle);

      compass_direction = PlayerPrefs.GetInt("CompassDirection");

      compass.transform.Rotate(0f,0f,compass_direction,Space.Self);




    }

    // Update is called once per frame
    void Update()
    {
        // Debug.DrawRay(compass.position, compass.transform.forward*10, Color.blue);
        vector.z = game_object.eulerAngles.y;
        transform.localEulerAngles = vector;

        // Debug.Log("The compass direction is: " + compass_direction);
        // Debug.Log("The Z angle: " + vector.z);

    }
}
