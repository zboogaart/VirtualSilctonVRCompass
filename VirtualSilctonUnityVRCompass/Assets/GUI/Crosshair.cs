using UnityEngine;


public class Crosshair : MonoBehaviour
{

    public Texture2D crosshairTexture;
    private Rect position;



    void Start ()
    {
        float xMin = (Screen.width - crosshairTexture.width) / 2;
        float yMin = (Screen.height - crosshairTexture.height) / 2;
        float width = crosshairTexture.width;
        float height = crosshairTexture.height;
        position = new Rect(xMin,yMin, width, height); 
        
    }

    void OnGUI()
    {
        GUI.DrawTexture(position, crosshairTexture);
    }

}