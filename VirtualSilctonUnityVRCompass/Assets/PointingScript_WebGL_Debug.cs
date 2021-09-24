using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class PointingScript_WebGL_Debug : MonoBehaviour {

    private List<string> buildingNames = new List<string>();
    private List<string> names = new List<string>();
    public int pointingDiamondIndex;
    public int facingDiamondIndex;
    public int targetBuildingIndex;
    public List<int> targetBuildingIndicesRemaining = new List<int>();
    public int targetBuildingIndicesRemainingIndex;
    public GameObject pointingPromptObject;
    public GameObject pointingAngleObject;
    public string waitingForClick;
    public Camera mainCamera;
    private Ray screenRay;
    public Vector3 currentPosition;
    public Vector3 facingDiamondPosition;
    public float pointingAngle;
    public float pointingAngleWRONG;
    private GameObject navigator;


    private void Start() {
        pointingPromptObject = GameObject.Find("PointingPrompt");
	      pointingAngleObject = GameObject.Find("PointingAngle");
        mainCamera = Camera.main;

        // populate arrays


        names.Add("Batty diamond");
        names.Add("Lynch diamond");
        names.Add("Harris diamond");
        names.Add("Harvey diamond");
        names.Add("Golledge diamond");
        names.Add("Snow diamond");
        names.Add("Sauer diamond");
        names.Add("Tobler diamond");

        buildingNames.Add("Batty House");
        buildingNames.Add("Lynch Station");
        buildingNames.Add("Harris Hall");
        buildingNames.Add("Harvey House");
        buildingNames.Add("Golledge Hall");
        buildingNames.Add("Snow Church");
        buildingNames.Add("Sauer Center");
        buildingNames.Add("Tobler Museum");


        startPointingSet(0);
    }

    void startPointingSet(int startLandmarkIndex) {
        if (startLandmarkIndex <= 7) {
            pointingDiamondIndex = startLandmarkIndex;

            // move to diamond
            Debug.Log(names[startLandmarkIndex]);
            navigator = GameObject.Find("FPSController");
            Vector3 diamondPosition = GameObject.Find(names[startLandmarkIndex]).transform.position;
            Debug.Log("Diamond Position" + diamondPosition);
            diamondPosition.y = 2.7f;
            navigator.transform.position = diamondPosition;
            if (startLandmarkIndex == 0) facingDiamondIndex = 1;
            else if (startLandmarkIndex == 1) facingDiamondIndex = 2;
            else if (startLandmarkIndex == 2) facingDiamondIndex = 3;
            else if (startLandmarkIndex == 3) facingDiamondIndex = 2;
            else if (startLandmarkIndex == 4) facingDiamondIndex = 5;
            else if (startLandmarkIndex == 5) facingDiamondIndex = 6;
            else if (startLandmarkIndex == 6) facingDiamondIndex = 7;
            else if (startLandmarkIndex == 7) facingDiamondIndex = 6;

            targetBuildingIndicesRemaining = new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7 });
            targetBuildingIndicesRemaining.RemoveAt(startLandmarkIndex);
            targetBuildingIndicesRemaining = Shuffle(targetBuildingIndicesRemaining);

            //Debug.Log("startPointingSet() targetBuildingIndicesRemaining: " + targetBuildingIndicesRemaining.join(","));
            targetBuildingIndicesRemaining = Shuffle(targetBuildingIndicesRemaining);

            showPointingQuestion();
        }
        else {
            // all done -- return to browser
            Debug.Log("all done -- return to browser");
            Application.ExternalCall("doneWithPointing");
        }
    }

    void showPointingQuestion() {
        if (targetBuildingIndicesRemaining.Count > 0) {
            targetBuildingIndex = targetBuildingIndicesRemaining[0];
            // show instructions
            pointingPromptObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Point to " + buildingNames[targetBuildingIndex];
        }
        else {
            startPointingSet(pointingDiamondIndex + 1);
        }
    }




    void Update() {
      screenRay = mainCamera.ScreenPointToRay(new Vector3((mainCamera.pixelWidth / 2), (mainCamera.pixelHeight / 2), 0));
      Vector3 currentPosition = GetComponent<CharacterController>().transform.position;
      facingDiamondPosition = GameObject.Find(names[facingDiamondIndex]).transform.position;
      currentPosition.y = facingDiamondPosition.y;
      Debug.DrawRay(currentPosition, screenRay.direction * 2000, Color.red);
      Debug.DrawLine(currentPosition, facingDiamondPosition, Color.blue);

      if (Input.GetMouseButtonDown(1)) {
        // Give the pointingAngle the same definition as below.
        pointingAngle = Vector3.SignedAngle((facingDiamondPosition - currentPosition), screenRay.direction, Vector3.up);
        pointingAngleObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Angle " + pointingAngle;
     }

      if (Input.GetMouseButtonDown(0)) {
          targetBuildingIndicesRemaining.RemoveAt(0);
          //Debug.Log("OnGUI() targetBuildingIndicesRemaining: " + targetBuildingIndicesRemaining.join(","));

          pointingAngleWRONG = Vector3.Angle((facingDiamondPosition-currentPosition), screenRay.direction);
          pointingAngle = Vector3.SignedAngle((facingDiamondPosition - currentPosition), screenRay.direction, Vector3.up);
          Debug.Log("pointing angle: " + pointingAngle);
          Debug.Log("pointing angleWRONG: " + pointingAngleWRONG);
          // send pointing angle to the file

          // send pointing angle to the browser
          Application.ExternalCall("recordPointingQuestion", pointingDiamondIndex, facingDiamondIndex, targetBuildingIndex, pointingAngle);

          Input.ResetInputAxes(); // we don't want this repeated multiple times
          showPointingQuestion();
        }
    }



    public static List<T> Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 0)
        {
            n--;
            int k = UnityEngine.Random.Range(n, list.Count);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

}
