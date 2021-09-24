using UnityEngine;
using System.Collections.Generic;


public class WebGL_LevelChange : MonoBehaviour
{
    private void Start() {
        Application.ExternalCall("unityIsReady");
    }

    private void ChangeLevel(string level) {
        if (level == "A") {
            Application.LoadLevel("Silcton_A_Arrows");
        }
        else if (level == "B") {
            Application.LoadLevel("Silcton_B_Arrows");
        }
        else if (level == "C1") {
            Application.LoadLevel("Silcton_C1_Arrows");
        }
        else if (level == "C2") {
            Application.LoadLevel("Silcton_C2_Arrows");
        }
        else if (level == "pointing") {
            Application.LoadLevel("Silcton_Pointing");
        }
    }
}