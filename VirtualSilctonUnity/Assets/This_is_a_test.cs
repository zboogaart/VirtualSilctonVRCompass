using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class This_is_a_test : MonoBehaviour
{

    public Transform Camera;

    // Start is called before the first frame update
    void Start()
    {
        Camera.transform.Rotate(0.0f, 0.0f, 0.0f, Space.World);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
