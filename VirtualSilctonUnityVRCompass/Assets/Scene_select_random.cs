using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_select_random : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
      int test_randNum1 = UnityEngine.Random.Range(1,3);
      int test_randNum2 = UnityEngine.Random.Range(1,3);
      int randNum1=test_randNum1;
      int randNum2=test_randNum2;
      int compassDirection;
      PlayerPrefs.SetInt("A_or_B",randNum1);
      PlayerPrefs.SetInt("C1_or_C2",randNum2);
      if(test_randNum1 == 1){
        compassDirection = 147;
        PlayerPrefs.SetInt("CompassDirection", compassDirection);
      }else if(test_randNum1 == 2){
        compassDirection = 80;
        PlayerPrefs.SetInt("CompassDirection", compassDirection);
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
