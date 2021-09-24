using UnityEngine;
using System.Collections.Generic;


public class Move_Look_For_Web : MonoBehaviour {

    public GameObject m_gameObject;
    public float interval = .1F; // save positions each 1.0 second
    public float tSample = 1.0F; // sampling starts after 10 seconds


    private void Start () {
      InvokeRepeating("RecordPosition", tSample, interval);
    }

    private void Stop () {
    CancelInvoke("RecordPosition");
    }


    private string FormatString(Vector3 position, float headingAngle)
    {
        return System.String.Format("{0,3:f2};{1,3:f2};{2,3:f2};{3,3:f2}\r\n", m_gameObject.transform.position.x, m_gameObject.transform.position.y, m_gameObject.transform.position.z, headingAngle);
    }


    private void RecordPosition () {

        string positionString = FormatString(m_gameObject.transform.position, transform.rotation.eulerAngles.y);
        // send pointing angle to the browser
        Application.ExternalCall("recordPositionLine", positionString);
        }




}
