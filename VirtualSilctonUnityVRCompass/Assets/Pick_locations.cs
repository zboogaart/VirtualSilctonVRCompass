using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Pick_locations : MonoBehaviour
{

    private Vector3 debugrayend;
    private RaycastHit hit;
    public Vector3 point1;
    public Vector3 point2;
    public int counter=1;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
      // transform.position=new Vector3(-37.4f,2.6f,-73.8f);
      // transform.position=new Vector3(-37.4f,260f,-73.8f);
    }

    // Update is called once per frame
    void Update()
    {
      // Save();
      if(Input.GetMouseButtonDown(0)){
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast (ray, out hit))
        {
           Debug.DrawLine (hit.point, debugrayend, Color.red);

             //Debug.Log(hit.point);
             if(counter==1){
               point1=hit.point;
               counter=2;
               //Thread.Sleep(100);
             }else{
               point2=hit.point;
               counter=3;
             }
             Debug.Log("Point 1 is: "+ point1);
             Debug.Log("Point 2 is: "+ point2);
        }
      }
      // if(counter==3){
      //   GoToNewPosition();
      // }
      // Debug.Log("Counter: "+ counter);
    }

    // private void GoToNewPosition()
    // {
    //   transform.position=new Vector3(-37.4f,2.6f,-73.8f);
    //   NavMeshAgent agent = GetComponent<NavMeshAgent>();
    //   transform.position=point1 + new Vector3(0,20f,0);
    //   agent.destination=point2;
    //
    // }
    // public void Save(){
    //   BinaryFormatter bf = new BinaryFormatter();
    //   FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
    //
    //   PlayerData data = new PlayerData();
    //   data.point1 = point1;
    //   data.point2 = point2;
    //   data.counter = counter;
    //
    //   bf.Serialize(file,data);
    //   file.Close();
    // }
}

// [Serializable]
// class PlayerData{
//   public Vector3 point1;
//   public Vector3 point2;
//   public int counter;
// }
