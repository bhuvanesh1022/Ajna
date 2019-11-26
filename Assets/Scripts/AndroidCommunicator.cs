using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidCommunicator : MonoBehaviour
{	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("onResume Recieved");
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("onBackPressed");
        }
	}
}
