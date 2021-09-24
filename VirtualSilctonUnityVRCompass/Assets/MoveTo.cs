using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;

public class MoveTo : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform goal;
    public Vector3 point1;
    public Vector3 point2;
    public int counter;
    Pick_locations my_script;
    Camera camera_1;
    public Camera camera_2;
    NavMeshAgent agent;
    private int counter_2 = 3;
    public bool face_forward;


      void Awake(){
        my_script = GameObject.Find("FPSController").GetComponent<Pick_locations>();
      }

       void Start ()
       {


         // Load();
         // GoToDestination();
          // NavMeshAgent agent = GetComponent<NavMeshAgent>();
          // // transform.position = point1;
          // agent.destination = goal.position;
          //transform.position = point1 + new Vector3(0,5f,0);
          // transform.position =new Vector3(44f,5f,118f);
          camera_1 = Camera.main;
          camera_1.enabled = true;
          camera_2.enabled = false;
          // GetComponent<NavMeshAgent>().updatePosition = false;
          agent = GetComponent<NavMeshAgent>();

       }

       public void GoToStartDestination()
       {
         // Debug.Log("Walk");
         // agent.updatePosition=false;
         // transform.position = point1;
         //
         // // agent.Warp(point1 + new Vector3(0,5f,0));
         // agent.updatePosition = true;
         // agent.nextPosition = point1;
         // agent.SetDestination(point2);


         agent.speed = 500f;
         Debug.Log("Is this running?");
         agent.destination=point1 + new Vector3(0,5f,0);


         // agent.destination=point2;
         //agent.destination = goal.position;
         // agent.isStopped = false;

         // Debug.Log("Walk more");
       }


       // public void Load(){
       //   if(File.Exists(Application.persistentDataPath + "/playerInfo.dat")){
       //     BinaryFormatter bf = new BinaryFormatter();
       //     FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
       //     PlayerData data = (PlayerData)bf.Deserialize(file);
       //     file.Close();
       //
       //     point1 = data.point1;
       //     point2 = data.point2;
       //     counter = data.counter;
       //   }
       // }


       void Update()
       {
         point1 = my_script.point1;
         point2 = my_script.point2;
         counter = my_script.counter;

         // Debug.Log("My point 1 is : " + point1);
         // Debug.Log("My point 2 is : " + point2);
         // Load();
         // Debug.Log("Counter: "+ counter);
         if(counter==3 && counter_2 < 50){
           // GoToStartDestination();

           agent.acceleration = 500000f;
           agent.speed = 5000f;
           Debug.Log("Is this running?");
           // agent.destination=point1 + new Vector3(0,5f,0);
           agent.destination=point1;
           //transform.position = point1 + new Vector3(0,5f,0);
           //agent.nextPosition = transform.position;
           //agent.updatePosition = true;


           // agent = GetComponent<NavMeshAgent>();
           counter_2 += 1;
           //agent.destination = goal.position;
           // agent = GetComponent<NavMeshAgent>();
           //agent.velocity = new Vector3(3f,3f,3f);
           //agent.destination=point2;

         }else if(counter_2>=50){
           camera_2.enabled = true;
           camera_1.enabled = false;
           agent.acceleration = 8f;
           agent.speed = 5f;
           transform.position=transform.position + new Vector3(0f,15f,0f);
           if(!face_forward)
           {
              camera_2.transform.localEulerAngles=new Vector3(90f,0f,0f);
           }
           agent.destination = point2;
         }
        // Debug.Log("Does the agent have a path?" + agent.hasPath);
        // Debug.Log("Is the agent on the NavMesh?" + agent.isOnNavMesh);
        // Debug.Log("Is the path stale?" + agent.isPathStale);
        // Debug.Log("Is the agent on an off NavMesh?" + agent.isOnOffMeshLink);
        // Debug.Log("Path status?" + agent.pathStatus);
        // Debug.Log("Remaining Distance: "+agent.remainingDistance);
        // Debug.Log("Is the agent stopped?"+agent.isStopped);
        // Debug.Log("counter value: "+counter);
       }
}
