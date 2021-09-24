using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace Valve.VR.Extras
{

public class PointingScript_VR : MonoBehaviour {

    public SteamVR_Behaviour_Pose pose;
    public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");

    public bool active = true;
    public Color color;
    public float thickness = 0.002f;
    public Color clickColor = Color.green;
    public GameObject holder;
    public GameObject pointer;
    bool isActive = false;
    // public bool addRigidBody = false;
    public Transform reference;
    public event PointerEventHandlerNew PointerIn;
    public event PointerEventHandlerNew PointerOut;
    public event PointerEventHandlerNew PointerClick;

    Transform previousContact = null;

    private List<string> buildingNames = new List<string>();
    private List<string> names = new List<string>();
    public int pointingDiamondIndex;
    public int facingDiamondIndex;
    public int targetBuildingIndex;
    public List<int> targetBuildingIndicesRemaining = new List<int>();
    public int targetBuildingIndicesRemainingIndex;
    public GameObject pointingPromptObject;
    string participantID = startup_script.newID;

    // public string waitingForClick;
    // // public Camera mainCamera;
    // private Ray screenRay;
    public Vector3 currentPosition;
    public Vector3 facingDiamondPosition;
    public float pointingAngle;
    public float pointingAngle_wrong;
    public GameObject navigator;
    private string fileName = "out_" + DateTime.Now.ToString("dd-MM-yyyy_hhmmss") + ".txt";

    private void Start() {

      if (pose == null)
          pose = this.GetComponent<SteamVR_Behaviour_Pose>();
      if (pose == null)
          Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

      if (interactWithUI == null)
          Debug.LogError("No ui interaction action has been set on this component.", this);

          holder = new GameObject();
          holder.transform.parent = this.transform;
          holder.transform.localPosition = Vector3.zero;
          holder.transform.localRotation = Quaternion.identity;

          pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
          pointer.transform.parent = holder.transform;
          pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
          pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
          pointer.transform.localRotation = Quaternion.identity;
          BoxCollider collider = pointer.GetComponent<BoxCollider>();
          // if (addRigidBody)
          // {
          //     if (collider)
          //     {
          //         collider.isTrigger = true;
          //     }
          //     Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
          //     rigidBody.isKinematic = true;
          // }
          // else
          // {
          //     if (collider)
          //     {
          //         Object.Destroy(collider);
          //     }
          // }
          Material newMaterial = new Material(Shader.Find("Unlit/Color"));
          newMaterial.SetColor("_Color", color);
          pointer.GetComponent<MeshRenderer>().material = newMaterial;


        // Create file to store pointing data
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string pathString = System.IO.Path.Combine(path, "SilctonVRData");
        string fullFileName = System.IO.Path.Combine(pathString,fileName);

        // Get Scene Name from Unity
        string sceneName = SceneManager.GetActiveScene().name;
        string sceneNameWrite = "Scene Name is " + sceneName + "\n";

        // Assemble participantID AND quit the application if none is entered.
        string participantIDWrite = "Participant ID is " + participantID + "\n";
        if (string.IsNullOrEmpty(participantID)) {
          Debug.Log("You forgot a participant ID");
          Application.Quit();
          // UnityEditor.EditorApplication.isPlaying = false;
        }


        System.IO.Directory.CreateDirectory(pathString);
        string header = "pointingDiamondIndex,facingDiamondIndex,targetBuildingIndex,pointingAngle\n";
        string fileNameWrite = fileName + "\n";
        using (System.IO.StreamWriter sw = System.IO.File.CreateText(fullFileName))
        {
          sw.WriteLine(fileNameWrite);
          sw.WriteLine(sceneNameWrite);
          sw.WriteLine(participantIDWrite);
          sw.WriteLine(header);
        }


      	pointingPromptObject = GameObject.Find("PointingPrompt");

        // mainCamera = Camera.main;

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

    public virtual void OnPointerIn(PointerEventArgsNew e)
    {
        if (PointerIn != null)
            PointerIn(this, e);
    }

    public virtual void OnPointerClick(PointerEventArgsNew e)
    {
        if (PointerClick != null)
            PointerClick(this, e);
    }

    public virtual void OnPointerOut(PointerEventArgsNew e)
    {
        if (PointerOut != null)
            PointerOut(this, e);
    }

    void startPointingSet(int startLandmarkIndex) {
        if (startLandmarkIndex <= 7) {
            pointingDiamondIndex = startLandmarkIndex;

            // move to diamond
            // Debug.Log(names[startLandmarkIndex]);
            navigator = GameObject.Find("OmniCharacterController");
            Vector3 diamondPosition = GameObject.Find(names[startLandmarkIndex]).transform.position;
            Debug.Log("Diamond Position" + diamondPosition);
            // diamondPosition.y = 2.7f;
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

            //  // Debug.Log("startPointingSet() targetBuildingIndicesRemaining: " + targetBuildingIndicesRemaining.join(","));
            targetBuildingIndicesRemaining = Shuffle(targetBuildingIndicesRemaining);

            showPointingQuestion();
        }
        else {
            // all done -- return to browser
            Debug.Log("all done -- return to browser");
            Application.Quit();
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
      // screenRay = mainCamera.ScreenPointToRay(new Vector3((mainCamera.pixelWidth / 2), (mainCamera.pixelHeight / 2), 0));

      // Debug.DrawRay(currentPosition, screenRay.direction * 2000, Color.red);
      // Debug.DrawLine(currentPosition, facingDiamondPosition, Color.blue);
      if(Input.GetKeyDown(KeyCode.Return)){
        Application.Quit();
      }


      if (!isActive)
      {
          isActive = true;
          this.transform.GetChild(0).gameObject.SetActive(true);
      }

      float dist = 1000f;

      Ray raycast = new Ray(transform.position, transform.forward);
      Vector3 pointSpot = raycast.GetPoint(10);

      RaycastHit hit;
      bool bHit = Physics.Raycast(raycast, out hit);
      //Stuff that was added
      Vector3 currentPosition = navigator.transform.position;
      Vector3 laserpointer = hit.point;
      facingDiamondPosition = GameObject.Find(names[facingDiamondIndex]).transform.position;
      currentPosition.y = facingDiamondPosition.y;
      laserpointer.y = facingDiamondPosition.y;
      pointSpot.y = facingDiamondPosition.y;
      Vector3 controllerPosition = transform.position;

      controllerPosition.y = facingDiamondPosition.y;
      //
      //
      // if (previousContact && previousContact != hit.transform)
      // {
      //     PointerEventArgsNew args = new PointerEventArgsNew();
      //     args.fromInputSource = pose.inputSource;
      //     args.distance = 0f;
      //     args.flags = 0;
      //     args.target = previousContact;
      //     OnPointerOut(args);
      //     previousContact = null;
      // }
      // if (bHit && previousContact != hit.transform)
      // {
      //     PointerEventArgsNew argsIn = new PointerEventArgsNew();
      //     argsIn.fromInputSource = pose.inputSource;
      //     argsIn.distance = hit.distance;
      //     argsIn.flags = 0;
      //     argsIn.target = hit.transform;
      //     OnPointerIn(argsIn);
      //     previousContact = hit.transform;
      // }
      // if (!bHit)
      // {
      //     previousContact = null;
      // }
      // if (bHit && hit.distance < 100f)
      // {
      //     dist = hit.distance;
      // }

      // if (bHit && interactWithUI.GetStateUp(pose.inputSource))
      // {
      //     PointerEventArgsNew argsClick = new PointerEventArgsNew();
      //     argsClick.fromInputSource = pose.inputSource;
      //     argsClick.distance = hit.distance;
      //     argsClick.flags = 0;
      //     argsClick.target = hit.transform;
      //     OnPointerClick(argsClick);
      //
      //
      //
      // }
      //
      pointingAngle = Vector3.SignedAngle((facingDiamondPosition - currentPosition), ((pointSpot-currentPosition)-(controllerPosition - currentPosition)), Vector3.up);
      Debug.Log("pointing angle: " + pointingAngle);
      PlayerPrefs.SetFloat("pointingAngle", pointingAngle);

      if (interactWithUI.GetStateUp(pose.inputSource))
      {
          PointerEventArgsNew argsClick = new PointerEventArgsNew();
          argsClick.fromInputSource = pose.inputSource;
          argsClick.distance = hit.distance;
          argsClick.flags = 0;
          argsClick.target = hit.transform;
          OnPointerClick(argsClick);

          //Testing old pointing code
          targetBuildingIndicesRemaining.RemoveAt(0);
          //Debug.Log("OnGUI() targetBuildingIndicesRemaining: " + targetBuildingIndicesRemaining.join(","));
          pointingAngle = Vector3.SignedAngle((facingDiamondPosition - currentPosition), ((pointSpot-currentPosition)-(controllerPosition - currentPosition)), Vector3.up);
          // pointingAngle_wrong = Vector3.Angle((facingDiamondPosition - currentPosition), screenRay.direction);
          Debug.Log("pointing angle: " + pointingAngle);
          // send pointing angle to the file
          // Create file to store pointing data
          string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
          string pathString = System.IO.Path.Combine(path, "SilctonVRData");
          string fullFileName = System.IO.Path.Combine(pathString,fileName);

          using (System.IO.StreamWriter sw = System.IO.File.AppendText(fullFileName))
          {
            sw.WriteLine(buildingNames[pointingDiamondIndex]+","+ buildingNames[facingDiamondIndex]+","+buildingNames[targetBuildingIndex]+","+pointingAngle+"\n");
          }



          Input.ResetInputAxes(); // we don't want this repeated multiple times
          showPointingQuestion();


      }


      if (interactWithUI != null && interactWithUI.GetState(pose.inputSource)) {

        pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
        pointer.GetComponent<MeshRenderer>().material.color = clickColor;

          // targetBuildingIndicesRemaining.RemoveAt(0);
          // //Debug.Log("OnGUI() targetBuildingIndicesRemaining: " + targetBuildingIndicesRemaining.join(","));
          // pointingAngle = Vector3.SignedAngle((facingDiamondPosition - currentPosition), screenRay.direction, Vector3.up);
          // pointingAngle_wrong = Vector3.Angle((facingDiamondPosition - currentPosition), screenRay.direction);
          // Debug.Log("pointing angle: " + pointingAngle);
          // // send pointing angle to the file
          // string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
          // string pathString = System.IO.Path.Combine(path, "SilctonVRData");
          // System.IO.Directory.CreateDirectory(pathString);
          // string fullFileName = System.IO.Path.Combine(pathString,fileName);
          //
          //
          // using (System.IO.StreamWriter sw = System.IO.File.AppendText(fullFileName))
          // {
          //   sw.WriteLine(buildingNames[pointingDiamondIndex]+","+ buildingNames[facingDiamondIndex]+","+buildingNames[targetBuildingIndex]+","+pointingAngle+"\n");
          // }
          //
          //
          // Input.ResetInputAxes(); // we don't want this repeated multiple times
          // showPointingQuestion();
        }
        else
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            pointer.GetComponent<MeshRenderer>().material.color = color;
        }
        pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
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
public struct PointerEventArgsNew
{
    public SteamVR_Input_Sources fromInputSource;
    public uint flags;
    public float distance;
    public Transform target;
}

public delegate void PointerEventHandlerNew(object sender, PointerEventArgsNew e);
}
