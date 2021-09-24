using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Path_selection : MonoBehaviour
{
    [SerializeField]
    public GameObject Button;
    public VideoPlayer myVideoPlayer;


    public static int counter;
    public static string ButtonPress1;
    public static string ButtonPress2;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonOnOff);
        counter = 1;
        // GameObject camera = GameObject.Find("Main Camera");
        // var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        // videoPlayer.playOnAwake = false;


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ButtonOnOff()
    {
      Debug.Log("Counter value before: "+ counter);
      Debug.Log(Button + " Pressed");
      if(Button.gameObject.name=="Button_1"){
        if(counter==1){
          ButtonPress1="Button_1";
          counter=2;
        }else{
          ButtonPress2="Button_1";
        }


      }
      if(Button.gameObject.name=="Button_2"){
        if(counter==1){
          ButtonPress1="Button_2";
          counter=2;
        }else{
          ButtonPress2="Button_2";
        }
      }
      if(Button.gameObject.name=="Button_3"){
        if(counter==1){
          ButtonPress1="Button_3";
          counter=2;
        }else{
          ButtonPress2="Button_3";
        }
      }
      if(Button.gameObject.name=="Button_4"){
        if(counter==1){
          ButtonPress1="Button_4";
          counter=2;
        }else{
          ButtonPress2="Button_4";
        }
      }
      if(Button.gameObject.name=="Button_5"){
        if(counter==1){
          ButtonPress1="Button_5";
          counter=2;
        }else{
          ButtonPress2="Button_5";
        }
      }
      Debug.Log("Counter value after: "+ counter);
      Debug.Log("ButtonPress1 value: " + ButtonPress1);
      Debug.Log("ButtonPress2 value: " + ButtonPress2);
      if(ButtonPress1=="Button_2" && ButtonPress2=="Button_1"){
        Debug.Log("Entering here");
        myVideoPlayer.url = "/Users/zboogaart/Documents/Unity projects/VirtualSilctonUnity/Recordings/003.mp4";
        myVideoPlayer.Play();
      }

    }
}
