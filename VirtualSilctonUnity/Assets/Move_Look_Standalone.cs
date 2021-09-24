using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Move_Look_Standalone : MonoBehaviour {

    public GameObject m_gameObject;
    public float interval = .1F; // save positions each 1.0 second
    public float tSample = 1.0F; // sampling starts after 10 seconds
    private string fileName = "log_" + DateTime.Now.ToString("dd-MM-yyyy_hhmmss") + ".txt";
    private string debugfileName = "debuglog_" + DateTime.Now.ToString("dd-MM-yyyy_hhmmss") + ".txt";

    private void Start () {
      InvokeRepeating("RecordPosition", tSample, interval);
      // Create file to store pointing data
      string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      string pathString = System.IO.Path.Combine(path, "SilctonStandaloneData");
      System.IO.Directory.CreateDirectory(pathString);
      string fullFileName = System.IO.Path.Combine(pathString,fileName);
      string header = "x,y,z,angle\n";
      using (System.IO.StreamWriter sw = System.IO.File.CreateText(fullFileName))
      {
        sw.WriteLine(header);
      }
      //debug purposes only
      //string debugfullFileName = System.IO.Path.Combine(pathString,debugfileName);
      //string debugheader = "angle1,angle2\n";
      //using (System.IO.StreamWriter sw = System.IO.File.CreateText(debugfileName))
      //{
      //  sw.WriteLine(debugheader);
      //}

    }

    private void Stop () {
    CancelInvoke("RecordPosition");
    }


    private string FormatString(Vector3 position, float yRotation)
    {
        return System.String.Format("{0,3:f2};{1,3:f2};{2,3:f2};{3,3:f2}\r\n", position.x, position.y, position.z, yRotation);
    }


    private void RecordPosition () {

        // get formatted string
        string positionString = FormatString(m_gameObject.transform.position, transform.rotation.eulerAngles.y);

        // Debug.Log(transform.rotation.eulerAngles.y);
        // Debug.Log(FormatString(m_gameObject.transform.position, transform.rotation.eulerAngles.y));

        // send pointing angle to the text file
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string pathString = System.IO.Path.Combine(path, "SilctonStandaloneData");
        System.IO.Directory.CreateDirectory(pathString);
        string fullFileName = System.IO.Path.Combine(pathString,fileName);

        using (System.IO.StreamWriter sw = System.IO.File.AppendText(fullFileName))
        {
          sw.WriteLine(positionString);
        }

        //string debugfullFileName = System.IO.Path.Combine(pathString,debugfileName);
        //using (System.IO.StreamWriter sw = System.IO.File.AppendText(debugfullFileName))
        //{
        //  sw.WriteLine(System.String.Format("{0,3:f3};{1,3:f4}\r\n", transform.rotation.eulerAngles.y, m_gameObject.transform.rotation.y));
        //}

        }




}
