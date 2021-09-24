using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintToScreen : MonoBehaviour
{

    float pointingAngle;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pointingAngle = PlayerPrefs.GetFloat("pointingAngle");

        text.text = pointingAngle.ToString();
    }
}
