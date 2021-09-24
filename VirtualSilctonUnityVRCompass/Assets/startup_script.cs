using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    public class startup_script : MonoBehaviour{

        public string participantID;
        public static string newID;
        static bool startup = false;
        static int firstset = 0;
        static bool compass = false;
        public static int randNum1;
        public static int randNum2;
        public static int CompassOrNoCompass;
        static int milli = 500;
        public Button Compass_Scenes, No_Compass_Scenes;
        // static int compass_count = 0;

        // Start is called before the first frame update
        void Start(){
          // int test_randNum1 = UnityEngine.Random.Range(1,3);
          // int test_randNum2 = UnityEngine.Random.Range(1,3);
          // startup_script.randNum1=test_randNum1;
          // startup_script.randNum2=test_randNum2;
          // PlayerPrefs.SetInt("A_or_B",randNum1);
          // PlayerPrefs.SetInt("C1_or_C2",randNum2);

          startup_script.randNum1 = PlayerPrefs.GetInt("A_or_B");
          startup_script.randNum2 = PlayerPrefs.GetInt("C1_or_C2");

          if(startup == false){


            var input = gameObject.GetComponent<InputField>();
            var enter= new InputField.SubmitEvent();
            enter.AddListener(SubmitID);
            input.onEndEdit = enter;



            Compass_Scenes.onClick.AddListener(TaskOnCompassClick);
            No_Compass_Scenes.onClick.AddListener(TaskOnNoCompassClick);

            startup = true;
          }
        }

        public void SubmitID(string participantID){
          // Debug.Log("This is the participant ID: ");
          // Debug.Log(participantID);
          newID = participantID;

        }

        void TaskOnCompassClick(){
          Debug.Log("You have selected the compass task.");
          compass = true;
        }

        void TaskOnNoCompassClick(){
          Debug.Log("You have selected the non-compass task.");
          compass = false;
        }

        // Update is called once per frame
        void Update()
        {
          // Debug.Log("This is the first random number: ");
          // Debug.Log(randNum1);
          // Debug.Log("This is the second random number: ");
          // Debug.Log(randNum2);
          if(Input.GetKeyDown(KeyCode.Return)){
            Thread.Sleep(startup_script.milli);
              if(firstset < 2){
                  if(startup_script.randNum1==1){
                    if(compass == false){
                      SceneManager.LoadScene("Silcton_A_Arrows_VR");
                      startup_script.firstset += 1;
                      // startup_script.randNum1 = 2;
                      PlayerPrefs.SetInt("A_or_B",2);
                      Debug.Log("Should be A");

                    }else{
                      SceneManager.LoadScene("Silcton_A_Arrows_VR_compass");
                      startup_script.firstset += 1;
                      // startup_script.randNum1 = 2;
                      PlayerPrefs.SetInt("A_or_B",2);
                      Debug.Log("Should be A with compass");
                      // int Scene_Selection_A = 1;
                      // if(compass_count == 0){
                      //   int compass_direction_A = 147;
                      //   PlayerPrefs.SetInt("CompassDirection", compass_direction_A);
                      //   compass_count += 1;
                      // }

                      // PlayerPrefs.SetInt("Scene", Scene_Selection_A);
                    }
                  }else if(startup_script.randNum1==2){
                    if(compass == false){
                      SceneManager.LoadScene("Silcton_B_Arrows_VR");
                      startup_script.firstset += 1;
                      // startup_script.randNum1 = 1;
                      PlayerPrefs.SetInt("A_or_B",1);
                      Debug.Log("Should be B");
                    }else{
                      SceneManager.LoadScene("Silcton_B_Arrows_VR_compass");
                      startup_script.firstset += 1;
                      // startup_script.randNum1 = 1;
                      PlayerPrefs.SetInt("A_or_B",1);
                      Debug.Log("Should be B with compass");
                      // if(compass_count == 0){
                      //   int compass_direction_B = 80;
                      //   PlayerPrefs.SetInt("CompassDirection", compass_direction_B);
                      //   compass_count += 1;
                      // }

                      // PlayerPrefs.SetInt("Scene", Scene_Selection_B);
                    }
                  }
              }else if (firstset < 4){
                  if(startup_script.randNum2==1){
                    if(compass == false){
                      SceneManager.LoadScene("Silcton_C1_Arrows_VR");
                      startup_script.firstset += 1;
                      // startup_script.randNum2 = 2;
                      PlayerPrefs.SetInt("C1_or_C2",2);
                      Debug.Log("Should be C1");
                    }else{
                      SceneManager.LoadScene("Silcton_C1_Arrows_VR_Compass");
                      startup_script.firstset += 1;
                      // startup_script.randNum2 = 2;
                      PlayerPrefs.SetInt("C1_or_C2",2);
                      Debug.Log("Should be C1 with Compass");
                    }
                  }else if(startup_script.randNum2==2){
                    if(compass == false){
                      SceneManager.LoadScene("Silcton_C2_Arrows_VR");
                      startup_script.firstset += 1;
                      // startup_script.randNum2 = 1;
                      PlayerPrefs.SetInt("C1_or_C2",1);
                      Debug.Log("Should be C2");
                    }else{
                      SceneManager.LoadScene("Silcton_C2_Arrows_VR_Compass");
                      startup_script.firstset += 1;
                      // startup_script.randNum2 = 1;
                      PlayerPrefs.SetInt("C1_or_C2",1);
                      Debug.Log("Should be C2 with compass");
                    }
                  }
              }else{
                if(compass == false){
                  SceneManager.LoadScene("Silcton_Pointing_VR");
                }else{
                  SceneManager.LoadScene("Silcton_Pointing_VR_Compass");
                }

              }
          }
        }
    }
